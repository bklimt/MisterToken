using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MisterToken {
    class AnalogInputRepeater : AnalogInput {
        public AnalogInputRepeater(PlayerIndex player, AnalogStick stick, float threshold, int minMillisPerRepeat, int maxMillisPerRepeat)
            : base(player, stick) {
            this.threshold = threshold;
            this.millisRemaining = 0;
            this.minMillisPerRepeat = minMillisPerRepeat;
            this.maxMillisPerRepeat = maxMillisPerRepeat;
            this.isDown = false;
        }

        public void Update(GameTime gameTime) {
            isDown = false;
            if (millisRemaining > 0) {
                millisRemaining -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (millisRemaining <= 0) {
                float value = base.GetValue();
                if (((threshold > 0) && (value > threshold)) || ((threshold < 0) && (value < threshold))) {
                    isDown = true;
                    float fraction = Math.Abs(value) / (1.0f - Math.Abs(threshold));
                    millisRemaining = (int)(minMillisPerRepeat + ((1.0f - fraction) * (maxMillisPerRepeat - minMillisPerRepeat)));
                } else {
                    millisRemaining = 0;
                }
            }
        }

        public bool IsDown() {
            return isDown;
        }

        private bool isDown;
        private float threshold;
        private int millisRemaining;
        private int minMillisPerRepeat;
        private int maxMillisPerRepeat;
    }
}
