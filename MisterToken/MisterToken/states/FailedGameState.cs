using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class FailedGameState : PlayingGameState {
        public FailedGameState(PlayingGameModel model) : base(model) {
            waitingToPlayState = null;
            keyDown = false;
        }

        public void SetWaitingToPlayState(WaitingToPlayState waitingToPlayState) {
            this.waitingToPlayState = waitingToPlayState;
        }

        public override void Start() {
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            if (keyDown && !Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter)) {
                keyDown = false;
                return waitingToPlayState;
            } else {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter)) {
                    keyDown = true;
                }
            }
            return this;
        }

        public override Color GetBackgroundColor() {
            return Color.DarkGray;
        }

        private bool keyDown = false;
        private WaitingToPlayState waitingToPlayState;
    }
}
