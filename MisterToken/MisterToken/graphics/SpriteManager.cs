using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class SpriteManager {
        public void LoadContent(ContentManager content, GraphicsDevice device) {
            spriteTexture = content.Load<Texture2D>("sprites");
        }

        public void DrawCell(Cell cell, Rectangle targetRect, SpriteBatch spriteBatch) {
            int x = 0;
            int y = 0;
            switch (cell.direction) {
                case Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 0; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 0; y = 1; break; }
                case Cell.Direction.UP | Cell.Direction.RIGHT: { x = 0; y = 2; break; }
                case Cell.Direction.DOWN | Cell.Direction.LEFT | Cell.Direction.RIGHT: { x = 1; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 1; y = 1; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.RIGHT: { x = 1; y = 2; break; }
                case Cell.Direction.DOWN | Cell.Direction.LEFT: { x = 2; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.DOWN: { x = 2; y = 1; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT: { x = 2; y = 2; break; }
                case Cell.Direction.RIGHT: { x = 3; y = 0; break; }
                case Cell.Direction.NONE: { x = 3; y = 1; break; }
                // blank
                case Cell.Direction.RIGHT | Cell.Direction.LEFT: { x = 4; y = 0; break; }
                // locked
                // blank
                case Cell.Direction.LEFT: { x = 5; y = 0; break; }
                // blank
                // blank
                case Cell.Direction.DOWN: { x = 6; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.DOWN: { x = 6; y = 1; break; }
                case Cell.Direction.UP: { x = 6; y = 2; break; }
            }
            if (cell.locked) {
                x = 4;
                y = 1;
            }
            y += (3 * ((int)(cell.color) - 1));
            if (y > 11) {
                y -= 12;
                x += 7;
            }
            if (cell.color == Cell.Color.BLACK) {
                x = 3;
                y = 2;
            }
            x *= 65;
            y *= 65;

            Color highlight = (cell.matched ? Color.Gray : Color.White);

            Rectangle sourceRect;
            sourceRect.X = x;
            sourceRect.Y = y;
            sourceRect.Width = 64;
            sourceRect.Height = 64;

            // Cheat
            if (cell.color == Cell.Color.BLACK) {
                sourceRect.X = 200;
                sourceRect.Y = 135;
                sourceRect.Width = 10;
                sourceRect.Height = 10;
            }

            spriteBatch.Draw(spriteTexture, targetRect, sourceRect, highlight);
        }

        Texture2D spriteTexture;
    }
}
