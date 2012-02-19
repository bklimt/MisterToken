using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public abstract class BooleanInput {
        public BooleanInput() {
            keys = new List<Keys>();
            buttons = new List<Tuple<PlayerIndex, Buttons>>();
        }

        public BooleanInput AddKey(Keys key) {
            keys.Add(key);
            return this;
        }

        public BooleanInput AddButton(PlayerIndex player, Buttons button) {
            buttons.Add(new Tuple<PlayerIndex, Buttons>(player, button));
            return this;
        }

        protected bool GetCurrentState() {
            foreach (Keys key in keys) {
                if (Keyboard.GetState().IsKeyDown(key)) {
                    return true;
                }
            }
            foreach (Tuple<PlayerIndex, Buttons> tuple in buttons) {
                if (GamePad.GetState(tuple.Item1).IsButtonDown(tuple.Item2)) {
                    return true;
                }
            }
            return false;
        }

        public abstract void Update(GameTime gameTime);
        public abstract bool IsDown();

        private List<Keys> keys;
        private List<Tuple<PlayerIndex, Buttons>> buttons;
    }
}
