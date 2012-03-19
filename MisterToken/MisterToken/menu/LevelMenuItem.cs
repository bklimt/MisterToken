using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class LevelMenuItem : MenuItem {
        public LevelMenuItem(Level level, MenuAction action) {
            this.level = level;
            this.action = action;
        }

        public void Draw(Rectangle rect, SpriteBatch spriteBatch) {
            Vector2 topLeft;
            topLeft.X = rect.X;
            topLeft.Y = rect.Y;
            string label = level.GetName();
            Sprites.DrawText(label, level.IsCompleted() ? Color.YellowGreen : Color.Cyan, topLeft, spriteBatch);
            topLeft.X -= 25;
            if (level.IsCompleted()) {
                Sprites.DrawText("\u2713", Color.YellowGreen, topLeft, spriteBatch);
            }
        }

        public void OnEnter() {
            action(level);
        }

        private Level level;
        private MenuAction action;
        public delegate void MenuAction(Level level);
    }
}
