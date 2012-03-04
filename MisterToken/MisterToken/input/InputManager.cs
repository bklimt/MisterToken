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

            booleanMap.Add(BooleanInputHook.MENU_UP, new BooleanInputOnce()
                .AddKey(Keys.Up)
                .AddKey(Keys.W)
                .AddButton(PlayerIndex.One, Buttons.DPadUp)
                .AddButton(PlayerIndex.Two, Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.MENU_DOWN, new BooleanInputOnce()
                .AddKey(Keys.Down)
                .AddKey(Keys.S)
                .AddButton(PlayerIndex.One, Buttons.DPadDown)
                .AddButton(PlayerIndex.Two, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.MENU_ENTER, new BooleanInputOnce()
                .AddKey(Keys.Enter)
                .AddButton(PlayerIndex.One, Buttons.Start)
                .AddButton(PlayerIndex.One, Buttons.X)
                .AddButton(PlayerIndex.Two, Buttons.Start)
                .AddButton(PlayerIndex.Two, Buttons.X));
            booleanMap.Add(BooleanInputHook.MENU_BACK, new BooleanInputOnce()
                .AddKey(Keys.Escape)
                .AddButton(PlayerIndex.One, Buttons.Back)
                .AddButton(PlayerIndex.Two, Buttons.Back));

            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_SLAM, new BooleanInputOnce()
                .AddKey(Keys.W)
                .AddKey(Keys.Up)
                .AddKey(Keys.Space)
                .AddButton(PlayerIndex.One, Buttons.B));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_DOWN, new BooleanInputRepeater(25)
                .AddKey(Keys.S)
                .AddKey(Keys.Down)
                .AddButton(PlayerIndex.One, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.D)
                .AddKey(Keys.Right)
                .AddButton(PlayerIndex.One, Buttons.RightTrigger)
                .AddButton(PlayerIndex.One, Buttons.RightShoulder)
                .AddButton(PlayerIndex.One, Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.A)
                .AddKey(Keys.Left)
                .AddButton(PlayerIndex.One, Buttons.LeftTrigger)
                .AddButton(PlayerIndex.One, Buttons.LeftShoulder)
                .AddButton(PlayerIndex.One, Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.X)
                .AddButton(PlayerIndex.One, Buttons.A));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.Z)
                .AddButton(PlayerIndex.One, Buttons.X));

            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_SLAM, new BooleanInputOnce()
                .AddKey(Keys.I)
                .AddButton(PlayerIndex.Two, Buttons.B));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_DOWN, new BooleanInputRepeater(25)
                .AddKey(Keys.K)
                .AddButton(PlayerIndex.Two, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.L)
                .AddButton(PlayerIndex.Two, Buttons.RightTrigger)
                .AddButton(PlayerIndex.Two, Buttons.RightShoulder)
                .AddButton(PlayerIndex.Two, Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.J)
                .AddButton(PlayerIndex.Two, Buttons.LeftTrigger)
                .AddButton(PlayerIndex.Two, Buttons.LeftShoulder)
                .AddButton(PlayerIndex.Two, Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_ROTATE_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.OemComma)
                .AddButton(PlayerIndex.Two, Buttons.A));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_ROTATE_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.M)
                .AddButton(PlayerIndex.Two, Buttons.X));

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
