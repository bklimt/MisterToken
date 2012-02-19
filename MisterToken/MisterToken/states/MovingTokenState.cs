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
    public class MovingTokenState : PlayingGameState {
        public MovingTokenState(PlayingGameModel model) : base(model) {
            slamHandled = false;
            timeUntilNextAdvance = Constants.MILLIS_PER_ADVANCE;
        }

        public void setClearingState(ClearingState clearingState) {
            this.clearingState = clearingState;
        }

        public override void Start() {
            slamHandled = true;
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            Token currentToken = model.tokenGenerator.GetCurrentToken();
            if (currentToken == null) {
                throw new InvalidOperationException("Should never be in MovingTokenState with null current token.");
            }

            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_TOKEN_RIGHT)) {
                if (currentToken.CanMove(0, 1)) {
                    // currentToken.Move(0, 1);
                    model.board.ShiftRight();
                }
            }
            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_TOKEN_LEFT)) {
                if (currentToken.CanMove(0, -1)) {
                    // currentToken.Move(0, -1);
                    model.board.ShiftLeft();
                }
            }
            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_ROTATE_LEFT)) {
                if (currentToken.CanRotateLeft())
                    currentToken.RotateLeft();
            }
            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT)) {
                if (currentToken.CanRotateRight())
                    currentToken.RotateRight();
            }
            if (Input.IsDown(BooleanInputHook.PLAYER_ONE_TOKEN_DOWN)) {
                if (!slamHandled) {
                    timeUntilNextAdvance = 0;
                }
            } else {
                slamHandled = false;
            }

            timeUntilNextAdvance -= gameTime.ElapsedGameTime.Milliseconds;
            if (timeUntilNextAdvance <= 0) {
                timeUntilNextAdvance = Constants.MILLIS_PER_ADVANCE;
                // If there's a current token, move it down.
                if (!currentToken.CanMove(1, 0)) {
                    currentToken.Commit();
                    model.tokenGenerator.ClearCurrentToken();
                    return clearingState;
                } else {
                    currentToken.Move(1, 0);
                }
            }
            return this;
        }

        public override Color GetBackgroundColor() {
            return Color.CornflowerBlue;
        }

        int timeUntilNextAdvance;

        bool slamHandled;

        private ClearingState clearingState;
    }
}
