using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class Sprites {
        private Sprites() {
        }

        public static void LoadContent(ContentManager content, GraphicsDevice device) {
            manager.LoadContent(content, device);
        }

        public static Texture2D GetTextureForCell(Cell cell) {
            return manager.GetTextureForCell(cell);
        }

        private static SpriteManager manager = new SpriteManager();
    }
}
