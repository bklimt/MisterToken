using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class SinglePlayer {
        public SinglePlayer(PlayerIndex player, GameListener listener) {
            this.player = player;
            this.level = new Level();
            this.listener = listener;
            nextTokenReadiness = 0.0f;
            board = new Board();
            tokenGenerator = new TokenGenerator(board, level);
            dumps = new Cell.Color[Constants.COLUMNS];
            matches = new List<Cell.Color>();
            Start();
        }

        public void Start() {
            nextTokenReadiness = 0.0f;
            state = State.SETTING_UP_BOARD;
        }

        public void NextLevel() {
            level.Next();
        }

        public void Dump(List<Cell.Color> colors) {
            Random random = new Random();
            List<int> freeSpots = new List<int>();
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                if (dumps[i] == Cell.Color.BLACK) {
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
            device.Clear(Color.DarkBlue);

            // Draw the board.
            Rectangle boardRect = new Rectangle();
            if (player == PlayerIndex.One) {
                boardRect.X = Constants.BOARD_ONE_RECT_X;
            } else {
                boardRect.X = Constants.BOARD_TWO_RECT_X;
            }
            boardRect.Y = Constants.BOARD_RECT_Y;
            boardRect.Width = Constants.BOARD_RECT_WIDTH;
            boardRect.Height = Constants.BOARD_RECT_HEIGHT;
            board.DrawRect(boardRect, spriteBatch);

            // Draw the token in play.
            if (tokenGenerator.GetCurrentToken() != null) {
                tokenGenerator.GetCurrentToken().DrawRect(boardRect, spriteBatch);
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
                if (dumps[i] != Cell.Color.BLACK) {
                    Cell piece = new Cell();
                    piece.color = dumps[i];
                    piece.DrawRect(dumpRect, spriteBatch);
                }
                dumpRect.X += (boardRect.Width / Constants.COLUMNS);
            }

            if (state == State.WON) {
                Sprites.DrawLayer(SpriteHook.CLOUD_LAYER, boardRect, spriteBatch);
                Sprites.DrawCentered(SpriteHook.WINNER, boardRect, spriteBatch);
            } else if (state == State.FAILED) {
                Sprites.DrawLayer(SpriteHook.SPLATTER_LAYER, boardRect, spriteBatch);
                Sprites.DrawCentered(SpriteHook.LOSER, boardRect, spriteBatch);
            }
        }

        public void Update(GameTime gameTime) {
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

        private void DoSettingUpBoard(GameTime gameTime) {
            board.Randomize(level);
            state = State.WAITING_FOR_TOKEN;
        }

        private void DoDumping(GameTime gameTime) {
            state = State.WAITING_FOR_TOKEN;
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                if (dumps[i] != Cell.Color.BLACK) {
                    board.GetCell(0, i).color = dumps[i];
                    dumps[i] = Cell.Color.BLACK;
                    state = State.FALLING;
                }
            }
        }

        private void DoWaitingForToken(GameTime gameTime) {
            timeToNextToken -= gameTime.ElapsedGameTime.Milliseconds;
            nextTokenReadiness = 1.0f - ((float)timeToNextToken / Constants.MILLIS_PER_TOKEN);
            if (timeToNextToken <= 0) {
                tokenGenerator.LoadNextToken();
                nextTokenReadiness = 0.0f;
                Token token = tokenGenerator.GetCurrentToken();
                token.Move(0, Constants.TOKEN_START_COLUMN);
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

            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_RIGHT.ForPlayer(player))) {
                if (currentToken.CanMove(0, 1)) {
                    board.ShiftRight();
                }
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_LEFT.ForPlayer(player))) {
                if (currentToken.CanMove(0, -1)) {
                    board.ShiftLeft();
                }
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.ROTATE_LEFT.ForPlayer(player))) {
                if (currentToken.CanRotateLeft())
                    currentToken.RotateLeft();
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.ROTATE_RIGHT.ForPlayer(player))) {
                if (currentToken.CanRotateRight())
                    currentToken.RotateRight();
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_DOWN.ForPlayer(player))) {
                timeUntilNextAdvance = 0;
            }
            if (Input.IsDown(PerPlayerBooleanInputHook.TOKEN_SLAM.ForPlayer(player))) {
                timeUntilNextAdvance = 0;
                while (currentToken.CanMove(1, 0)) {
                    currentToken.Move(1, 0);
                }
            }

            timeUntilNextAdvance -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeUntilNextAdvance <= 0) {
                timeUntilNextAdvance = Constants.MILLIS_PER_ADVANCE;
                // If there's a current token, move it down.
                if (!currentToken.CanMove(1, 0)) {
                    currentToken.Commit();
                    tokenGenerator.ClearCurrentToken();
                    timeToClear = 0;
                    state = State.CLEARING;
                } else {
                    currentToken.Move(1, 0);
                }
            }
        }

        private void DoClearing(GameTime gameTime) {
            if (timeToClear > 0) {
                timeToClear -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToClear <= 0) {
                board.ClearMatches();
                List<Cell.Color> newMatches = board.MarkMatches();
                matches.AddRange(newMatches);
                if (board.GetLockedCount() == 0) {
                    state = State.WON;
                    listener.OnWon(player);
                } else if (newMatches.Count > 0) {
                    timeToClear = Constants.MILLIS_PER_CLEAR;
                    listener.OnClear(player);
                } else {
                    timeToNextFall = 0;
                    anythingFell = false;
                    state = State.FALLING;
                }
            }
        }

        private void DoFalling(GameTime gameTime) {
            if (timeToNextFall > 0) {
                timeToNextFall -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToNextFall <= 0) {
                if (board.ApplyGravity()) {
                    anythingFell = true;
                    timeToNextFall = Constants.MILLIS_PER_FALL;
                } else {
                    if (anythingFell) {
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
        }

        private void DoFailed(GameTime gameTime) {
            if (Input.IsDown(PerPlayerBooleanInputHook.START.ForPlayer(player))) {
                listener.OnFinished(player);
            }
        }

        private void DoWon(GameTime gameTime) {
            if (Input.IsDown(PerPlayerBooleanInputHook.START.ForPlayer(player))) {
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

        // Waiting for token.
        private int timeToNextToken;

        // Moving token.
        private int timeUntilNextAdvance;

        // Clearing.
        private int timeToClear;
        private List<Cell.Color> matches;

        // Falling.
        private bool anythingFell;
        private int timeToNextFall;

        // Internal state.
        private PlayerIndex player;
        private Board board;
        private Level level;
        private TokenGenerator tokenGenerator;
        private float nextTokenReadiness;
        private Cell.Color[] dumps;
    }
}
