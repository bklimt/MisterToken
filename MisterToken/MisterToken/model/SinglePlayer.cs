﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class SinglePlayer : Game {
        public SinglePlayer(PlayerIndex player, int level, GameListener listener) {
            this.player = player;
            this.level = Levels.GetLevel(level);
            this.listener = listener;
            this.paused = false;
            nextTokenReadiness = 0.0f;
            board = new Board();
            tokenGenerator = new TokenGenerator(board, this.level);
            dumps = new CellColor[Constants.COLUMNS];
            matches = new List<CellColor>();
            state = State.SETTING_UP_BOARD;

            pauseMenu = new Menu(delegate() {});
            pauseMenu.Add("Continue", delegate() {
                paused = false;
                listener.OnPaused(player, paused);
            });
            pauseMenu.Add("Exit", delegate() {
                paused = false;
                listener.OnFinished(player);
            });
        }

        public void Dump(List<CellColor> colors) {
            Random random = new Random();
            List<int> freeSpots = new List<int>();
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                if (dumps[i] == CellColor.BLACK) {
                    freeSpots.Add(i);
                }
            }
            int color = 0;
            while (freeSpots.Count > 0 && color < colors.Count) {
                int freeSpotIndex = random.Next(freeSpots.Count);
                int freeSpot = freeSpots[freeSpotIndex];
                freeSpots[freeSpotIndex] = freeSpots[freeSpots.Count - 1];
                freeSpots.RemoveRange(freeSpots.Count - 1, 1);
                dumps[freeSpot] = colors[color++];
            }
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch) {
            device.Clear(Color.Black);

            // Determine the board position.
            Rectangle boardRect = new Rectangle();
            if (player == PlayerIndex.One) {
                boardRect.X = Constants.BOARD_ONE_RECT_X;
            } else {
                boardRect.X = Constants.BOARD_TWO_RECT_X;
            }
            boardRect.Y = Constants.BOARD_RECT_Y;
            boardRect.Width = Constants.COLUMNS * Constants.CELL_SIZE;
            boardRect.Height = Constants.ROWS * Constants.CELL_SIZE;
            Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, boardRect, spriteBatch);
            Sprites.DrawLayer(SpriteHook.SCREEN_50_LAYER, boardRect, spriteBatch);

            // Draw the stripe where the piece will be.
            Rectangle stripe;
            stripe.X = boardRect.X + Constants.CELL_SIZE * Constants.TOKEN_START_COLUMN;
            stripe.Y = boardRect.Y;
            stripe.Width = Constants.CELL_SIZE * 2;
            stripe.Height = boardRect.Height;
            Sprites.DrawLayer(SpriteHook.BACKGROUND_LAYER, stripe, spriteBatch);
            Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, stripe, spriteBatch);

            // Draw the board.
            board.DrawRect(boardRect, spriteBatch);

            // Draw the border around the board.
            {
                // sides.
                for (int row = 0; row < Constants.ROWS; ++row) {
                    Cell side = new Cell();
                    side.color = CellColor.WHITE;
                    side.direction = (row == 0) ? Cell.Direction.DOWN : (Cell.Direction.UP | Cell.Direction.DOWN);
                    side.DrawRect(Board.GetCellPosition(boardRect, row, -1), spriteBatch);
                    side.DrawRect(Board.GetCellPosition(boardRect, row, Constants.COLUMNS), spriteBatch);
                }
                // top and bottom.
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    Cell bottom = new Cell();
                    bottom.color = CellColor.WHITE;
                    bottom.direction = Cell.Direction.LEFT | Cell.Direction.RIGHT;
                    // bottom.DrawRect(Board.GetCellPosition(boardRect, -1, column), spriteBatch);
                    bottom.DrawRect(Board.GetCellPosition(boardRect, Constants.ROWS, column), spriteBatch);
                }
                Cell cell = new Cell();
                cell.color = CellColor.WHITE;
                // bottom left.
                cell.direction = Cell.Direction.UP | Cell.Direction.RIGHT;
                cell.DrawRect(Board.GetCellPosition(boardRect, Constants.ROWS, -1), spriteBatch);
                // bottom right.
                cell.direction = Cell.Direction.UP | Cell.Direction.LEFT;
                cell.DrawRect(Board.GetCellPosition(boardRect, Constants.ROWS, Constants.COLUMNS), spriteBatch);
                // top right.
                // cell.direction = Cell.Direction.LEFT | Cell.Direction.DOWN;
                // cell.DrawRect(Board.GetCellPosition(boardRect, -1, Constants.COLUMNS), spriteBatch);
                // top left.
                // cell.direction = Cell.Direction.RIGHT | Cell.Direction.DOWN;
                // cell.DrawRect(Board.GetCellPosition(boardRect, -1, -1), spriteBatch);
            }

            // Draw the token in play.
            if (tokenGenerator.GetCurrentToken() != null) {
                tokenGenerator.GetCurrentToken().DrawRect(boardRect, spriteBatch);
            }

            // Draw the winner/loser state.
            if (state == State.WON) {
                Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, boardRect, spriteBatch);
                Sprites.DrawCentered(SpriteHook.WINNER, boardRect, spriteBatch);
            } else if (state == State.FAILED) {
                Sprites.DrawLayer(SpriteHook.SPLATTER_LAYER, boardRect, spriteBatch);
                Sprites.DrawLayer(SpriteHook.SCREEN_50_LAYER, boardRect, spriteBatch);
                Sprites.DrawCentered(SpriteHook.LOSER, boardRect, spriteBatch);
            }

            // Draw the next token.
            Rectangle nextRect = new Rectangle();
            int nextTokenEndX = boardRect.X + (boardRect.Width / Constants.COLUMNS) * Constants.TOKEN_START_COLUMN;
            int nextTokenStartX = nextTokenEndX - 200;
            nextRect.X = (int)(nextTokenStartX + (nextTokenEndX - nextTokenStartX) * nextTokenReadiness);
            nextRect.Y = boardRect.Y - (boardRect.Height / Constants.ROWS);
            nextRect.Width = boardRect.Width;
            nextRect.Height = boardRect.Height;
            tokenGenerator.Draw(nextRect, spriteBatch);

            Rectangle dumpRect = new Rectangle();
            dumpRect.X = boardRect.X;
            dumpRect.Y = boardRect.Y - (boardRect.Height / Constants.ROWS);
            dumpRect.Width = (boardRect.Width / Constants.COLUMNS);
            dumpRect.Height = (boardRect.Height / Constants.ROWS);
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                if (dumps[i] != CellColor.BLACK) {
                    Cell piece = new Cell();
                    piece.color = dumps[i];
                    piece.DrawRect(dumpRect, spriteBatch);
                }
                dumpRect.X += (boardRect.Width / Constants.COLUMNS);
            }

            if (paused) {
                Sprites.DrawLayer(SpriteHook.SCREEN_50_LAYER, boardRect, spriteBatch);
                pauseMenu.Draw(boardRect, true, spriteBatch);
            }
        }

        public void Update(GameTime gameTime) {
            if (paused) {
                pauseMenu.Update();
                return;
            }
            if (state != State.FAILED && state != State.WON &&
                (Input.IsDown(BooleanInputHook.MENU_ENTER) || Input.IsDown(BooleanInputHook.MENU_BACK))) {
                paused = !paused;
                listener.OnPaused(player, paused);
            }
            switch (state) {
                case State.SETTING_UP_BOARD:
                    DoSettingUpBoard(gameTime);
                    break;
                case State.DUMPING:
                    DoDumping(gameTime);
                    break;
                case State.WAITING_FOR_TOKEN:
                    DoWaitingForToken(gameTime);
                    break;
                case State.MOVING_TOKEN:
                    DoMovingToken(gameTime);
                    break;
                case State.CLEARING:
                    DoClearing(gameTime);
                    break;
                case State.FALLING:
                    DoFalling(gameTime);
                    break;
                case State.FAILED:
                    DoFailed(gameTime);
                    break;
                case State.WON:
                    DoWon(gameTime);
                    break;
            }
        }

        public void Win() {
            state = State.WON;
        }

        public void Fail() {
            state = State.FAILED;
        }

        public void SetPaused(bool paused) {
            this.paused = paused;
        }

        private void HandleMovement() {
            Token currentToken = tokenGenerator.GetCurrentToken();
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_RIGHT.ForPlayer(player))) {
                if (level.Wrap() && (currentToken == null || currentToken.Move(true, 0, 1, true))) {
                    board.ShiftRight();
                }
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_LEFT.ForPlayer(player))) {
                if (level.Wrap() && (currentToken == null || currentToken.Move(true, 0, -1, true))) {
                    board.ShiftLeft();
                }
            }
        }

        private void DoSettingUpBoard(GameTime gameTime) {
            board.Setup(level);
            state = State.FALLING;
            timeToNextFall = 0;
            timeToNextToken = Constants.MILLIS_PER_TOKEN;
        }

        private void DoDumping(GameTime gameTime) {
            HandleMovement();

            state = State.WAITING_FOR_TOKEN;
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                if (dumps[i] != CellColor.BLACK) {
                    board.GetCell(0, i).color = dumps[i];
                    dumps[i] = CellColor.BLACK;
                    timeToNextFall = 0;
                    state = State.FALLING;
                }
            }
        }

        private void DoWaitingForToken(GameTime gameTime) {
            HandleMovement();

            timeToNextToken -= gameTime.ElapsedGameTime.Milliseconds;
            nextTokenReadiness = 1.0f - ((float)timeToNextToken / Constants.MILLIS_PER_TOKEN);
            if (timeToNextToken <= 0) {
                tokenGenerator.LoadNextToken();
                nextTokenReadiness = 0.0f;
                Token token = tokenGenerator.GetCurrentToken();
                token.Move(false, 0, Constants.TOKEN_START_COLUMN, level.Wrap(), true);
                if (!token.IsValid()) {
                    // Game over!
                    token.Commit();
                    state = State.FAILED;
                    listener.OnFailed(player);
                } else {
                    timeUntilNextAdvance = Constants.MILLIS_PER_ADVANCE;
                    state = State.MOVING_TOKEN;
                }
            }
        }

        private void DoMovingToken(GameTime gameTime) {
            Token currentToken = tokenGenerator.GetCurrentToken();
            if (currentToken == null) {
                throw new InvalidOperationException("Should never be in MovingTokenState with null current token.");
            }

            HandleMovement();
            if (!level.Wrap() && Input.IsDown(PerPlayerBooleanInputHook.TOKEN_RIGHT.ForPlayer(player))) {
                currentToken.Move(false, 0, 1, level.Wrap());
            }
            if (!level.Wrap() && Input.IsDown(PerPlayerBooleanInputHook.TOKEN_LEFT.ForPlayer(player))) {
                currentToken.Move(false, 0, -1, level.Wrap());
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.ROTATE_LEFT.ForPlayer(player))) {
                currentToken.RotateLeft(false, level.Wrap());
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.ROTATE_RIGHT.ForPlayer(player))) {
                currentToken.RotateRight(false, level.Wrap());
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_DOWN.ForPlayer(player))) {
                timeUntilNextAdvance = 0;
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_SLAM.ForPlayer(player))) {
                timeUntilNextAdvance = 0;
                Sound.Play(SoundHook.SLAM);
                while (currentToken.Move(false, 1, 0, level.Wrap())) {
                    // Do nothing.
                }
            }

            timeUntilNextAdvance -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeUntilNextAdvance <= 0) {
                timeUntilNextAdvance = Constants.MILLIS_PER_ADVANCE;
                // If there's a current token, move it down.
                if (!currentToken.Move(false, 1, 0, false)) {
                    currentToken.Commit();
                    tokenGenerator.ClearCurrentToken();
                    timeToClear = 0;
                    state = State.CLEARING;
                }
            }
        }

        private void DoClearing(GameTime gameTime) {
            HandleMovement();
            if (timeToClear > 0) {
                timeToClear -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToClear <= 0) {
                board.ClearMatches();
                List<CellColor> newMatches = board.MarkMatches();
                matches.AddRange(newMatches);
                if (newMatches.Count > 0) {
                    if (matches.Count == 1) {
                        Sound.Play(SoundHook.CLEAR_1);
                    } else if (matches.Count == 2) {
                        Sound.Play(SoundHook.CLEAR_2);
                    } else {
                        Sound.Play(SoundHook.CLEAR_3);
                    }
                }
                if (board.GetLockedCount() == 0) {
                    state = State.WON;
                    int[] previous = Storage.GetSaveData().completed;
                    if (!previous.Contains(level.GetId())) {
                        int[] current = new int[previous.Length + 1];
                        current[0] = level.GetId();
                        previous.CopyTo(current, 1);
                        Storage.GetSaveData().completed = current;
                        Storage.Save();
                    }
                    listener.OnWon(player);
                } else if (newMatches.Count > 0) {
                    timeToClear = Constants.MILLIS_PER_CLEAR;
                    listener.OnClear(player);
                } else {
                    timeToNextFall = 0;
                    state = State.FALLING;
                }
            }
        }

        private void DoFalling(GameTime gameTime) {
            HandleMovement();
            if (timeToNextFall > 0) {
                timeToNextFall -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToNextFall <= 0) {
                bool anythingFell = board.MoveLoose();
                bool anythingLoose = board.MarkLoose();
                if (anythingLoose) {
                    timeToNextFall = Constants.MILLIS_PER_FALL;
                } else if (anythingFell) {
                    timeToClear = 0;
                    state = State.CLEARING;
                } else {
                    timeToNextToken = Constants.MILLIS_PER_TOKEN;
                    if (matches.Count > 1) {
                       listener.OnDump(player, matches);
                    }
                    matches.Clear();
                    state = State.DUMPING;
                }
            }
        }

        private void DoFailed(GameTime gameTime) {
            if (Input.IsDown(BooleanInputHook.MENU_ENTER) || Input.IsDown(BooleanInputHook.MENU_BACK)) {
                listener.OnFinished(player);
            }
        }

        private void DoWon(GameTime gameTime) {
            if (Input.IsDown(BooleanInputHook.MENU_ENTER) || Input.IsDown(BooleanInputHook.MENU_BACK)) {
                listener.OnFinished(player);
            }
        }

        // Game state.
        private enum State {
            SETTING_UP_BOARD,
            DUMPING,
            WAITING_FOR_TOKEN,
            MOVING_TOKEN,
            CLEARING,
            FALLING,
            FAILED,
            WON,
        }
        private State state;
        private GameListener listener;

        // Pause state.
        private bool paused;
        private Menu pauseMenu;

        // Waiting for token.
        private int timeToNextToken;

        // Moving token.
        private int timeUntilNextAdvance;

        // Clearing.
        private int timeToClear;
        private List<CellColor> matches;

        // Falling.
        private int timeToNextFall;

        // Internal state.
        private PlayerIndex player;
        private Board board;
        private Level level;
        private TokenGenerator tokenGenerator;
        private float nextTokenReadiness;
        private CellColor[] dumps;
    }
}
