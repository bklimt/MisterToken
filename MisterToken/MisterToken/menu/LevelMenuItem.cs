using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class LevelMenuItem : MenuItem {
        public LevelMenuItem(int number, Level level, MenuAction action) {
            this.number = number;
            this.level = level;
            this.action = action;
        }

        public void Draw(Rectangle rect, SpriteBatch spriteBatch) {
            Vector2 topLeft;
            topLeft.X = rect.X;
            topLeft.Y = rect.Y;
            string label = level.GetName();
            Global.Sprites.DrawText(label, level.IsCompleted() ? Color.YellowGreen : Color.Cyan, topLeft, spriteBatch);
            topLeft.X -= 25;
            if (level.IsCompleted()) {
                Global.Sprites.DrawText("\u2713", Color.YellowGreen, topLeft, spriteBatch);
            }
        }

        public void Draw2(int x, int y, SpriteBatch spriteBatch) {
            Global.Sprites.Draw(SpriteHook.MENU_PANEL, new Vector2(x, y), spriteBatch);
            if (level.IsCompleted()) {
                Global.Sprites.Draw(SpriteHook.MENU_CHECK_OVERLAY, new Vector2(x, y), spriteBatch);
            }
            Global.Sprites.DrawText(
                number + " - " + level.GetName(),
                IsEnabled() ? Color.Black : Color.Gray,
                new Vector2(x + 60, y + 14),
                true,
                spriteBatch);
        }

        public void OnEnter() {
            if (IsEnabled()) {
                action(level);
            }
        }

        public bool IsEnabled() {
            return level.IsEnabled();
        }

        private int number;
        private Level level;
        private MenuAction action;
        public delegate void MenuAction(Level level);
    }
}
