using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum BooleanInputHook {
        TOKEN_SLAM,
        TOKEN_RIGHT,
        TOKEN_DOWN,
        TOKEN_LEFT,
        ROTATE_RIGHT,
        ROTATE_LEFT,
        SWITCH_MODE,
    }

    public enum AnalogInputHook {
        CAMERA_POSITION_X,
        CAMERA_POSITION_Y,
        LOOK_AT_X,
        LOOK_AT_Y,
        INCREASE_FOV,
        DECREASE_FOV,
    }

    public class InputManager {
        public InputManager() {
            booleanMap = new Dictionary<BooleanInputHook, BooleanInput>();
            booleanMap.Add(BooleanInputHook.TOKEN_SLAM, new BooleanInputOnce().AddKey(Keys.W).AddKey(Keys.Up).AddKey(Keys.Space).AddButton(Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.TOKEN_DOWN, new BooleanInputRepeater(25).AddKey(Keys.S).AddKey(Keys.Down).AddButton(Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.TOKEN_RIGHT, new BooleanInputRepeater(200).AddKey(Keys.D).AddKey(Keys.Right).AddButton(Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.TOKEN_LEFT, new BooleanInputRepeater(200).AddKey(Keys.A).AddKey(Keys.Left).AddButton(Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.ROTATE_RIGHT, new BooleanInputRepeater(200).AddKey(Keys.X).AddButton(Buttons.A));
            booleanMap.Add(BooleanInputHook.ROTATE_LEFT, new BooleanInputRepeater(200).AddKey(Keys.Z).AddButton(Buttons.X));
            booleanMap.Add(BooleanInputHook.SWITCH_MODE, new BooleanInputOnce().AddKey(Keys.C).AddButton(Buttons.Y));

            analogMap = new Dictionary<AnalogInputHook, AnalogInput>();
            analogMap.Add(AnalogInputHook.CAMERA_POSITION_X, new AnalogInput(AnalogStick.LEFT_X, 1));
            analogMap.Add(AnalogInputHook.CAMERA_POSITION_Y, new AnalogInput(AnalogStick.LEFT_Y, 1));
            analogMap.Add(AnalogInputHook.LOOK_AT_X, new AnalogInput(AnalogStick.RIGHT_X, 1));
            analogMap.Add(AnalogInputHook.LOOK_AT_Y, new AnalogInput(AnalogStick.RIGHT_Y, 1));
            analogMap.Add(AnalogInputHook.INCREASE_FOV, new AnalogInput(AnalogStick.RIGHT_TRIGGER, 1));
            analogMap.Add(AnalogInputHook.DECREASE_FOV, new AnalogInput(AnalogStick.LEFT_TRIGGER, 1));
        }

        public void Update(GameTime gameTime) {
            foreach (KeyValuePair<BooleanInputHook, BooleanInput> pair in booleanMap) {
                pair.Value.Update(gameTime);
            }
            foreach (KeyValuePair<AnalogInputHook, AnalogInput> pair in analogMap) {
                pair.Value.Update(gameTime);
            }
        }

        public bool IsDown(BooleanInputHook input) {
            return booleanMap[input].IsDown();
        }

        public float GetDelta(AnalogInputHook input) {
            return analogMap[input].GetDelta();
        }

        private Dictionary<BooleanInputHook, BooleanInput> booleanMap;
        private Dictionary<AnalogInputHook, AnalogInput> analogMap;
    }
}
