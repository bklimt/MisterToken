using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class DefaultMenuItem : MenuItem {
        public DefaultMenuItem(string text, MenuAction action) {
            this.text = text;
            this.action = action;
        }

        public void Draw(Rectangle rect, SpriteBatch spriteBatch) {
            Vector2 topLeft;
            topLeft.X = rect.X;
            topLeft.Y = rect.Y;
            Global.Sprites.DrawText(text, Color.YellowGreen, topLeft, spriteBatch);
        }

        public void Draw2(int x, int y, SpriteBatch spriteBatch) {
            Global.Sprites.Draw(SpriteHook.MENU_PANEL, new Vector2(x, y), spriteBatch);
            Global.Sprites.DrawText(
                text,
                Color.Black,
                new Vector2(x + 36, y + 14),
                true,
                spriteBatch);
        }

        public void OnEnter() {
            if (IsEnabled()) {
                action();
            }
        }

        public bool IsEnabled() {
            return true;
        }

        public delegate void MenuAction();
        private string text;
        private MenuAction action;
    }
}
