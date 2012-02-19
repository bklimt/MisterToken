using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MisterToken {
    public class BooleanInputRepeater : BooleanInput {
        public BooleanInputRepeater(int millisPerRepeat) {
            this.millisPerRepeat = millisPerRepeat;
            this.isDown = false;
            this.wasDown = false;
            this.millisRemaining = 0;
        }

        public override void Update(GameTime gameTime) {
            if (millisRemaining > 0) {
                millisRemaining -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (millisRemaining < 0) {
                millisRemaining = 0;
            }
            if (base.GetCurrentState()) {
                if (!wasDown || millisRemaining <= 0) {
                    millisRemaining = millisPerRepeat;
                    wasDown = true;
                    isDown = true;
                } else {
                    wasDown = true;
                    isDown = false;
                }
            } else {
                wasDown = false;
                isDown = false;
            }
        }

        public override bool IsDown() {
            return isDown;
        }

        private bool isDown;
        private bool wasDown;
        private int millisRemaining;
        private int millisPerRepeat;
    }
}
