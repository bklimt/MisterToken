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
            textures = new Dictionary<SpriteHook, Texture2D>();
            textures[SpriteHook.TITLE_LAYER] = content.Load<Texture2D>("title");
            textures[SpriteHook.BACKGROUND_LAYER] = content.Load<Texture2D>("background");
            textures[SpriteHook.SCREEN_80_LAYER] = content.Load<Texture2D>("screen80");
            textures[SpriteHook.SCREEN_50_LAYER] = content.Load<Texture2D>("screen50");
            textures[SpriteHook.SPLATTER_LAYER] = content.Load<Texture2D>("splatter");
            textures[SpriteHook.CLOUD_LAYER] = content.Load<Texture2D>("cloud");
            textures[SpriteHook.PLAYER] = content.Load<Texture2D>("player");
            textures[SpriteHook.WINNER] = content.Load<Texture2D>("winner");
            textures[SpriteHook.LOSER] = content.Load<Texture2D>("loser");
            textures[SpriteHook.NUMBER_1] = content.Load<Texture2D>("1");
            textures[SpriteHook.NUMBER_2] = content.Load<Texture2D>("2");
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

            spriteBatch.Draw(spriteTexture, targetRect, sourceRect, highlight);
        }

        public void DrawLayer(SpriteHook sprite, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite], new Vector2(), Color.White);
        }

        public void DrawLayer(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite], targetRect, targetRect, Color.White);
        }

        public void DrawCentered(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            Texture2D texture = textures[sprite];
            Vector2 position;
            position.X = targetRect.Center.X - (texture.Bounds.Width / 2);
            position.Y = targetRect.Center.Y - (texture.Bounds.Height / 2);
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Draw(SpriteHook sprite, Vector2 position, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite], position, Color.White);
        }

        private Texture2D spriteTexture;
        private Dictionary<SpriteHook, Texture2D> textures;
    }
}
