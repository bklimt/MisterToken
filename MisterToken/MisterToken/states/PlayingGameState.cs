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
    public abstract class PlayingGameState : GameState {
        public PlayingGameState(PlayingGameModel model) {
            this.model = model;
        }

        public void LoadContent(ContentManager content, GraphicsDevice device) {
            LoadExtraContent(content, device);
        }

        public virtual void LoadExtraContent(ContentManager content, GraphicsDevice device) {
        }

        public abstract void Start();

        public abstract GameState PlayingUpdate(GameTime gameTime);

        public GameState Update(GameTime gameTime) {
            model.input.Update(gameTime);
            model.quadDrawer.MoveCamera(model.input.GetDelta(AnalogInputHook.CAMERA_POSITION_X),
                                        model.input.GetDelta(AnalogInputHook.CAMERA_POSITION_Y),
                                        model.input.GetDelta(AnalogInputHook.INCREASE_FOV) - model.input.GetDelta(AnalogInputHook.DECREASE_FOV));
            model.quadDrawer.MoveLookAt(model.input.GetDelta(AnalogInputHook.LOOK_AT_X),
                                        model.input.GetDelta(AnalogInputHook.LOOK_AT_Y),
                                        0);
            if (model.input.IsDown(BooleanInputHook.SWITCH_MODE)) {
                model.circleMode = !model.circleMode;
            }

            return PlayingUpdate(gameTime);
        }

        // Get rid of this!
        public abstract Color GetBackgroundColor();

        public void Draw(GraphicsDevice device, GameTime gameTime) {
            device.Clear(GetBackgroundColor());

            model.quadDrawer.SetupCamera();
            model.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);

            if (model.circleMode) {
                // Draw the board.
                Rectangle boardRect = new Rectangle();
                boardRect.X = Constants.BOARD_CIRCLE_X;
                boardRect.Y = Constants.BOARD_CIRCLE_Y;
                boardRect.Width = Constants.BOARD_CIRCLE_WIDTH;
                boardRect.Height = Constants.BOARD_CIRCLE_HEIGHT;
                model.board.DrawCircle(boardRect, model.quadDrawer);

                // Draw the token in play.
                if (model.tokenGenerator.GetCurrentToken() != null) {
                    model.tokenGenerator.GetCurrentToken().DrawCircle(boardRect, model.quadDrawer);
                }

                // Draw the next token.
                Rectangle nextRect = new Rectangle();
                nextRect.X = (int)(model.nextTokenReadiness * Constants.BOARD_RECT_X + (boardRect.Width / Constants.COLUMNS) * Constants.TOKEN_START_COLUMN);
                nextRect.Y = boardRect.Y - (boardRect.Height / Constants.ROWS);
                nextRect.Width = boardRect.Width;
                nextRect.Height = boardRect.Height;
                model.tokenGenerator.Draw(nextRect, model.spriteBatch);
            } else {
                // Draw the board.
                Rectangle boardRect = new Rectangle();
                boardRect.X = Constants.BOARD_RECT_X;
                boardRect.Y = Constants.BOARD_RECT_Y;
                boardRect.Width = Constants.BOARD_RECT_WIDTH;
                boardRect.Height = Constants.BOARD_RECT_HEIGHT;
                model.board.DrawRect(boardRect, model.spriteBatch);

                // Draw the token in play.
                if (model.tokenGenerator.GetCurrentToken() != null) {
                    model.tokenGenerator.GetCurrentToken().DrawRect(boardRect, model.spriteBatch);
                }

                // Draw the next token.
                Rectangle nextRect = new Rectangle();
                nextRect.X = (int)(model.nextTokenReadiness * Constants.BOARD_RECT_X + (boardRect.Width / Constants.COLUMNS) * Constants.TOKEN_START_COLUMN);
                nextRect.Y = boardRect.Y - (boardRect.Height / Constants.ROWS);
                nextRect.Width = boardRect.Width;
                nextRect.Height = boardRect.Height;
                model.tokenGenerator.Draw(nextRect, model.spriteBatch);
            }


            model.spriteBatch.End();
        }

        protected PlayingGameModel model;
    }
}
