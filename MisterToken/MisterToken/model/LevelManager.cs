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
            worlds = content.Load<MisterToken.XmlWorld[]>("levels");
        }

        public int GetWorldCount() {
            return worlds.Length;
        }

        public string GetWorldName(int i) {
            return worlds[i].name;
        }

        public int GetLevelCount(int world) {
            return worlds[world].level.Length;
        }

        public Level GetLevel(int world, int level) {
            return new Level(worlds[world].level[level]);
        }

        public bool IsCompleted(string level) {
            for (int i = 0; i < Storage.GetSaveData().completed.Length; ++i) {
                if (Storage.GetSaveData().completed[i] == level) {
                    return true;
                }
            }
            return false;
        }

        private MisterToken.XmlWorld[] worlds;
    }
}
