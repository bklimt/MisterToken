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
    public class FallingState : PlayingGameState {
        public FallingState(PlayingGameModel model) : base(model) {
        }

        public void SetWaitingForTokenState(WaitingForTokenState waitingForTokenState) {
            this.waitingForTokenState = waitingForTokenState;
        }

        public void SetClearingState(ClearingState clearingState) {
            this.clearingState = clearingState;
        }

        public override void LoadExtraContent(ContentManager content, GraphicsDevice device) {
        }

        public override void Start() {
            timeToNextFall = 0;
            anythingFell = false;
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            if (timeToNextFall > 0) {
                timeToNextFall -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToNextFall <= 0) {
                if (model.board.ApplyGravity()) {
                    anythingFell = true;
                    timeToNextFall = Constants.MILLIS_PER_FALL;
                    return this;
                } else {
                    if (anythingFell) {
                        return clearingState;
                    } else {
                        return waitingForTokenState;
                    }
                }
            } else {
                return this;
            }
        }

        public override Color GetBackgroundColor() {
            return Color.CornflowerBlue;
        }

        private WaitingForTokenState waitingForTokenState;
        private ClearingState clearingState;

        private bool anythingFell;
        private int timeToNextFall;
    }
}
