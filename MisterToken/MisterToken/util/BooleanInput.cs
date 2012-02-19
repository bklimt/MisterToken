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
            buttons = new List<Buttons>();
        }

        public BooleanInput AddKey(Keys key) {
            keys.Add(key);
            return this;
        }

        public BooleanInput AddButton(Buttons button) {
            buttons.Add(button);
            return this;
        }

        public virtual void Update(GameTime gameTime) {
        }

        public virtual bool IsDown() {
            foreach (Keys key in keys) {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(key)) {
                    return true;
                }
            }
            foreach (Buttons button in buttons) {
                if (GamePad.GetState(PlayerIndex.One).IsButtonDown(button)) {
                    return true;
                }
            }
            return false;
        }

        private List<Keys> keys;
        private List<Buttons> buttons;
    }
}
