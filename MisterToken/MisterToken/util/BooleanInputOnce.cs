using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    class BooleanInputOnce : BooleanInput {
        public BooleanInputOnce() {
            down = false;
            consumed = false;
        }

        public override void Update(GameTime gameTime) {
            if (base.IsDown()) {
                down = true;
            } else {
                down = false;
                consumed = false;
            }
        }

        public override bool IsDown() {
            if (down && !consumed) {
                consumed = true;
                return true;
            }
            return false;
        }

        private bool down;
        private bool consumed;
    }
}
