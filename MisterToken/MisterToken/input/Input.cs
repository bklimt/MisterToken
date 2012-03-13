using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class Input {
        private Input() {
        }

        public static void Update(GameTime gameTime) {
            manager.Update(gameTime);
        }

        public static bool IsDown(BooleanInputHook input) {
            return manager.IsDown(input);
        }

        public static bool IsDown(AnalogInputHook input) {
            return manager.IsDown(input);
        }

        private static InputManager manager = new InputManager();
    }
}
