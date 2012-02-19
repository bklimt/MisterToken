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
        public AnalogInput(AnalogStick stick, float velocityPerSecond) {
            this.velocityPerSecond = velocityPerSecond;
            this.stick = stick;
        }

        public static float GetCurrentValue(AnalogStick stick) {
            switch (stick) {
                case AnalogStick.LEFT_X:
                    return GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.X;
                case AnalogStick.LEFT_Y:
                    return GamePad.GetState(PlayerIndex.One).ThumbSticks.Left.Y;
                case AnalogStick.RIGHT_X:
                    return GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.X;
                case AnalogStick.RIGHT_Y:
                    return GamePad.GetState(PlayerIndex.One).ThumbSticks.Right.Y;
                case AnalogStick.LEFT_TRIGGER:
                    return GamePad.GetState(PlayerIndex.One).Triggers.Left;
                case AnalogStick.RIGHT_TRIGGER:
                    return GamePad.GetState(PlayerIndex.One).Triggers.Right;
                default:
                    return 0.0f;
            }
        }

        public void Update(GameTime gameTime) {
            delta = GetCurrentValue(stick) * velocityPerSecond * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);
        }

        public double GetDelta() {
            return delta;
        }

        private double delta;
        private double velocityPerSecond;
        private AnalogStick stick;
    }
}
