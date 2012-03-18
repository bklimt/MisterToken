using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;

namespace MisterToken {
    public class LevelManager {
        public void LoadContent(ContentManager content) {
            levels = content.Load<MisterToken.XmlLevel[]>("levels");
        }

        public int GetLevelCount() {
            return levels.Length;
        }

        public Level GetLevel(int i) {
            return new Level(levels[i]);
        }

        public bool IsCompleted(int level) {
            for (int i = 0; i < Storage.GetSaveData().completed.Length; ++i) {
                if (Storage.GetSaveData().completed[i] == level) {
                    return true;
                }
            }
            return false;
        }

        private MisterToken.XmlLevel[] levels;
    }
}
