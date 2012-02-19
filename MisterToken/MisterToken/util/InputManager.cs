using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class InputManager {
        public InputManager() {
            booleanMap = new Dictionary<BooleanInputHook, BooleanInput>();
            booleanMap.Add(BooleanInputHook.TOKEN_SLAM, new BooleanInputOnce().AddKey(Keys.W).AddKey(Keys.Up).AddKey(Keys.Space).AddButton(Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.TOKEN_DOWN, new BooleanInputRepeater(25).AddKey(Keys.S).AddKey(Keys.Down).AddButton(Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.TOKEN_RIGHT, new BooleanInputRepeater(200).AddKey(Keys.D).AddKey(Keys.Right).AddButton(Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.TOKEN_LEFT, new BooleanInputRepeater(200).AddKey(Keys.A).AddKey(Keys.Left).AddButton(Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.ROTATE_RIGHT, new BooleanInputRepeater(200).AddKey(Keys.X).AddButton(Buttons.A));
            booleanMap.Add(BooleanInputHook.ROTATE_LEFT, new BooleanInputRepeater(200).AddKey(Keys.Z).AddButton(Buttons.X));

            analogMap = new Dictionary<AnalogInputHook, AnalogInput>();
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

        public double GetDelta(AnalogInputHook input) {
            return analogMap[input].GetDelta();
        }

        private Dictionary<BooleanInputHook, BooleanInput> booleanMap;
        private Dictionary<AnalogInputHook, AnalogInput> analogMap;
    }
}
