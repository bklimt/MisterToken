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
        public AnalogInput(PlayerIndex player, AnalogStick stick, float velocityPerSecond) {
            this.player = player;
            this.stick = stick;
            this.velocityPerSecond = velocityPerSecond;
        }

        public static float GetCurrentValue(PlayerIndex player, AnalogStick stick) {
            switch (stick) {
                case AnalogStick.LEFT_X:
                    return GamePad.GetState(player).ThumbSticks.Left.X;
                case AnalogStick.LEFT_Y:
                    return GamePad.GetState(player).ThumbSticks.Left.Y;
                case AnalogStick.RIGHT_X:
                    return GamePad.GetState(player).ThumbSticks.Right.X;
                case AnalogStick.RIGHT_Y:
                    return GamePad.GetState(player).ThumbSticks.Right.Y;
                case AnalogStick.LEFT_TRIGGER:
                    return GamePad.GetState(player).Triggers.Left;
                case AnalogStick.RIGHT_TRIGGER:
                    return GamePad.GetState(player).Triggers.Right;
                default:
                    return 0.0f;
            }
        }

        public void Update(GameTime gameTime) {
            delta = GetCurrentValue(player, stick) * velocityPerSecond * (gameTime.ElapsedGameTime.TotalMilliseconds / 1000.0);
        }

        public double GetDelta() {
            return delta;
        }

        private double delta;
        private double velocityPerSecond;
        private PlayerIndex player;
        private AnalogStick stick;
    }
}
