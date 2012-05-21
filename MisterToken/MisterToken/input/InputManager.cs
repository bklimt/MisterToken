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

            booleanMap.Add(BooleanInputHook.PLAYER_ONE_MENU_UP, new BooleanInputOnce()
                .AddKey(Keys.W)
                .AddButton(PlayerIndex.One, Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_MENU_DOWN, new BooleanInputOnce()
                .AddKey(Keys.S)
                .AddButton(PlayerIndex.One, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_MENU_ENTER, new BooleanInputOnce()
                .AddKey(Keys.Space)
                .AddButton(PlayerIndex.One, Buttons.Start));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_MENU_BACK, new BooleanInputOnce()
                .AddKey(Keys.Escape)
                .AddButton(PlayerIndex.One, Buttons.Back));

            booleanMap.Add(BooleanInputHook.PLAYER_TWO_MENU_UP, new BooleanInputOnce()
                .AddKey(Keys.Up)
                .AddButton(PlayerIndex.Two, Buttons.DPadUp));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_MENU_DOWN, new BooleanInputOnce()
                .AddKey(Keys.Down)
                .AddButton(PlayerIndex.Two, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_MENU_ENTER, new BooleanInputOnce()
                .AddKey(Keys.Enter)
                .AddButton(PlayerIndex.Two, Buttons.Start));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_MENU_BACK, new BooleanInputOnce()
                .AddKey(Keys.Back)
                .AddButton(PlayerIndex.Two, Buttons.Back));

            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_SLAM, new BooleanInputOnce()
                .AddKey(Keys.W)
                .AddKey(Keys.Space)
                .AddButton(PlayerIndex.One, Buttons.RightShoulder));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_DOWN, new BooleanInputRepeater(25)
                .AddKey(Keys.S)
                .AddButton(PlayerIndex.One, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.D)
                .AddButton(PlayerIndex.One, Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_TOKEN_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.A)
                .AddButton(PlayerIndex.One, Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.X)
                .AddKey(Keys.E)
                .AddButton(PlayerIndex.One, Buttons.A));
            booleanMap.Add(BooleanInputHook.PLAYER_ONE_ROTATE_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.Z)
                .AddKey(Keys.Q)
                .AddButton(PlayerIndex.One, Buttons.X)
                .AddButton(PlayerIndex.One, Buttons.B));

            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_SLAM, new BooleanInputOnce()
                .AddKey(Keys.Up)
                .AddButton(PlayerIndex.Two, Buttons.RightShoulder));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_DOWN, new BooleanInputRepeater(25)
                .AddKey(Keys.Down)
                .AddButton(PlayerIndex.Two, Buttons.DPadDown));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.Right)
                .AddButton(PlayerIndex.Two, Buttons.DPadRight));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_TOKEN_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.Left)
                .AddButton(PlayerIndex.Two, Buttons.DPadLeft));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_ROTATE_RIGHT, new BooleanInputRepeater(200)
                .AddKey(Keys.RightShift)
                .AddButton(PlayerIndex.Two, Buttons.A));
            booleanMap.Add(BooleanInputHook.PLAYER_TWO_ROTATE_LEFT, new BooleanInputRepeater(200)
                .AddKey(Keys.OemQuestion)
                .AddButton(PlayerIndex.Two, Buttons.X)
                .AddButton(PlayerIndex.Two, Buttons.B));

            analogMap = new Dictionary<AnalogInputHook, AnalogInputRepeater>();

            float threshold = 0.2f;
            int minMillisPerRepeat = 160;
            int maxMillisPerRepeat = 500;

            analogMap.Add(AnalogInputHook.PLAYER_ONE_TOKEN_LEFT,
                          new AnalogInputRepeater(PlayerIndex.One, AnalogStick.LEFT_X, -threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_ONE_TOKEN_RIGHT,
                          new AnalogInputRepeater(PlayerIndex.One, AnalogStick.LEFT_X, threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_TWO_TOKEN_LEFT,
                          new AnalogInputRepeater(PlayerIndex.Two, AnalogStick.LEFT_X, -threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_TWO_TOKEN_RIGHT,
                          new AnalogInputRepeater(PlayerIndex.Two, AnalogStick.LEFT_X, threshold, minMillisPerRepeat, maxMillisPerRepeat));

            analogMap.Add(AnalogInputHook.PLAYER_ONE_TOKEN_DOWN,
                          new AnalogInputRepeater(PlayerIndex.One, AnalogStick.LEFT_Y, -0.2f, 10, 150));
            analogMap.Add(AnalogInputHook.PLAYER_TWO_TOKEN_DOWN,
                          new AnalogInputRepeater(PlayerIndex.Two, AnalogStick.LEFT_Y, -0.2f, 10, 150));


            analogMap.Add(AnalogInputHook.PLAYER_ONE_MENU_DOWN,
                          new AnalogInputRepeater(PlayerIndex.One, AnalogStick.LEFT_Y, -threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_ONE_MENU_UP,
                          new AnalogInputRepeater(PlayerIndex.One, AnalogStick.LEFT_Y, threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_TWO_MENU_DOWN,
                          new AnalogInputRepeater(PlayerIndex.Two, AnalogStick.LEFT_Y, -threshold, minMillisPerRepeat, maxMillisPerRepeat));
            analogMap.Add(AnalogInputHook.PLAYER_TWO_MENU_UP,
                          new AnalogInputRepeater(PlayerIndex.Two, AnalogStick.LEFT_Y, threshold, minMillisPerRepeat, maxMillisPerRepeat));
        }

        public void Update(GameTime gameTime) {
            foreach (KeyValuePair<BooleanInputHook, BooleanInput> pair in booleanMap) {
                pair.Value.Update(gameTime);
            }
            foreach (KeyValuePair<AnalogInputHook, AnalogInputRepeater> pair in analogMap) {
                pair.Value.Update(gameTime);
            }
        }

        public bool IsDown(BooleanInputHook input) {
            return booleanMap[input].IsDown();
        }

        public bool IsDown(AnalogInputHook input) {
            return analogMap[input].IsDown();
        }

        private Dictionary<BooleanInputHook, BooleanInput> booleanMap;
        private Dictionary<AnalogInputHook, AnalogInputRepeater> analogMap;
    }
}
