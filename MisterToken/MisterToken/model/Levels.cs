using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MisterToken {
    public class Levels {
        private Levels() {
        }

        public static void LoadContent(ContentManager content) {
            manager.LoadContent(content);
        }

        public static int GetLevelCount() {
            return manager.GetLevelCount();
        }

        public static Level GetLevel(int i) {
            return manager.GetLevel(i);
        }

        public static bool IsCompleted(int level) {
            return manager.IsCompleted(level);
        }

        private static LevelManager manager = new LevelManager();
    }
}
