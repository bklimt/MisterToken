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
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_START, new BooleanInputOnce()
                .AddKey(Keys.Enter));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_SLAM, new BooleanInputOnce()
                .AddKey(Keys.W)
                .AddKey(Keys.Up)
                .AddKey(Keys.Space)
                .AddButton(PlayerIndex.One, Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_DOWN, new BooleanInputRepeater(25)
                .AddKey(Keys.S)
                .AddKey(Keys.Down)
                .AddButton(PlayerIndex.One, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.D)
                .AddKey(Keys.Right)
                .AddButton(PlayerIndex.One, Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.A)
                .AddKey(Keys.Left)
                .AddButton(PlayerIndex.One, Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.X)
                .AddButton(PlayerIndex.One, Buttons.A));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.Z)
                .AddButton(PlayerIndex.One, Buttons.X));

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
