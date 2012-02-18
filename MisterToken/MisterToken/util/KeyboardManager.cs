using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class KeyboardManager {
        public KeyboardManager() {
            keys = new Dictionary<Keys, KeyRepeater>();
        }

        public void Update(GameTime gameTime) {
            foreach (KeyValuePair<Keys, KeyRepeater> pair in keys) {
                pair.Value.Update(gameTime);
            }
        }

        public bool IsDown(Keys key) {
            if (!keys.ContainsKey(key)) {
                keys.Add(key, new KeyRepeater(key));
            }
            return keys[key].isDown();
        }

        private Dictionary<Keys, KeyRepeater> keys;
    }
}
