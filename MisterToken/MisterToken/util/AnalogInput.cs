using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum AnalogStick {
        LEFT_X,
        LEFT_Y,
        RIGHT_X,
        RIGHT_Y,
        LEFT_TRIGGER,
        RIGHT_TRIGGER,
    }

    public class AnalogInput {
        public AnalogInput(AnalogStick stick, float velocityPerMilli) {
            this.velocityPerMilli = velocityPerMilli;
            this.stick = stick;
        }

        public void Update(GameTime gameTime) {
            float value = 0.0f;
            switch (stick) {
                case AnalogStick.LEFT_X:
                    value = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                    break;
                case AnalogStick.LEFT_Y:
                    value = GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                    break;
                case AnalogStick.RIGHT_X:
                    value = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                    break;
                case AnalogStick.RIGHT_Y:
                    value = GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                    break;
                case AnalogStick.LEFT_TRIGGER:
                    value = GamePad.GetState(PlayerIndex.One).Triggers.Left;
                    break;
                case AnalogStick.RIGHT_TRIGGER:
                    value = GamePad.GetState(PlayerIndex.One).Triggers.Right;
                    break;
            }
            delta = value * velocityPerMilli * gameTime.ElapsedGameTime.Milliseconds;
        }

        public float GetDelta() {
            return delta;
        }

        private float delta;
        private float velocityPerMilli;
        private AnalogStick stick;
    }
}
