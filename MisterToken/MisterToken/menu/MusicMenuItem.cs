using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class MusicMenuItem : MenuItem {
        public MusicMenuItem(string description, SoundHook song) {
            this.description = description;
            this.song = song;
        }

        public void Draw(Rectangle rect, SpriteBatch spriteBatch) {
            Vector2 topLeft;
            topLeft.X = rect.X;
            topLeft.Y = rect.Y;
            Global.Sprites.DrawText(description, Color.Cyan, topLeft, spriteBatch);
            topLeft.X -= 25;
            if (song == Global.Sound.GetMusic()) {
                Global.Sprites.DrawText("\u2713", Color.YellowGreen, topLeft, spriteBatch);
            }
        }

        public void Draw2(int x, int y, SpriteBatch spriteBatch) {
            Global.Sprites.Draw(SpriteHook.MENU_PANEL, new Vector2(x, y), spriteBatch);
            if (song == Global.Sound.GetMusic()) {
                Global.Sprites.Draw(SpriteHook.MENU_CHECK_OVERLAY, new Vector2(x, y), spriteBatch);
            }
            Global.Sprites.DrawText(
                description,
                IsEnabled() ? Color.Black : Color.Gray,
                new Vector2(x + 60, y + 14),
                true,
                spriteBatch);
        }

        public void OnEnter() {
            if (IsEnabled()) {
                Global.Sound.SetMusic(song);
            }
        }

        public bool IsEnabled() {
            return true;
        }

        private string description;
        private SoundHook song;
    }
}
