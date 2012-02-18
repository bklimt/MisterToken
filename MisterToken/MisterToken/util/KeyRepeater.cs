using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class KeyRepeater {
        public KeyRepeater(Keys key) {
            this.key = key;
            this.wasDown = false;
            this.millisRemaining = 0;
        }

        public void Update(GameTime gameTime) {
            if (millisRemaining > 0) {
                millisRemaining -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (millisRemaining < 0) {
                millisRemaining = 0;
            }
        }

        public bool isDown() {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(key)) {
                if (!wasDown || millisRemaining <= 0) {
                    millisRemaining = Constants.MILLIS_PER_REPEAT;
                    wasDown = true;
                    return true;
                } else {
                    wasDown = true;
                    return false;
                }
            } else {
                wasDown = false;
                return false;
            }
        }

        private Keys key;
        private bool wasDown;
        private int millisRemaining;
    }
}
