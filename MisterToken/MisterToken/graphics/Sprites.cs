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

        public static void DrawCell(Cell cell, Rectangle targetRect, SpriteBatch spriteBatch) {
            manager.DrawCell(cell, targetRect, spriteBatch);
        }

        private static SpriteManager manager = new SpriteManager();
    }
}
