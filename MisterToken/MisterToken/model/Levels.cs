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

        public static int GetWorldCount() {
            return manager.GetWorldCount();
        }

        public static string GetWorldName(int i) {
            return manager.GetWorldName(i);
        }

        public static int GetLevelCount(int world) {
            return manager.GetLevelCount(world);
        }

        public static Level GetLevel(int world, int level) {
            return manager.GetLevel(world, level);
        }

        public static bool IsCompleted(string level) {
            return manager.IsCompleted(level);
        }

        private static LevelManager manager = new LevelManager();
    }
}
