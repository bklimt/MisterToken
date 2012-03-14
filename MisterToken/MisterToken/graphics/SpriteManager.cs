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
            animations = new List<Animation>();

            colorTextures = new Dictionary<CellColor, Drawable>();
            colorTextures[CellColor.RED] = new Image(content.Load<Texture2D>("tokens/red_sprites"));
            colorTextures[CellColor.GREEN] = new Image(content.Load<Texture2D>("tokens/green_sprites"));
            colorTextures[CellColor.WHITE] = new Image(content.Load<Texture2D>("tokens/white_sprites"));
            colorTextures[CellColor.YELLOW] = new Image(content.Load<Texture2D>("tokens/yellow_sprites"));
            colorTextures[CellColor.BLUE] = new Image(content.Load<Texture2D>("tokens/blue_sprites"));
            colorTextures[CellColor.CYAN] = new Image(content.Load<Texture2D>("tokens/cyan_sprites"));
            colorTextures[CellColor.PURPLE] = new Image(content.Load<Texture2D>("tokens/purple_sprites"));
            colorTextures[CellColor.ORANGE] = new Image(content.Load<Texture2D>("tokens/orange_sprites"));

            Animation flash = new Animation(40);
            flash.AddFrame(content.Load<Texture2D>("tokens/red_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/green_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/white_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/yellow_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/blue_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/orange_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/cyan_sprites"));
            flash.AddFrame(content.Load<Texture2D>("tokens/purple_sprites"));
            colorTextures[CellColor.WILD] = flash;
            animations.Add(flash);

            textures = new Dictionary<SpriteHook, Drawable>();
            textures[SpriteHook.TITLE_LAYER] = new Image(content.Load<Texture2D>("layers/title"));
            textures[SpriteHook.BACKGROUND_LAYER] = new Image(content.Load<Texture2D>("layers/background"));
            textures[SpriteHook.HELP_LAYER] = new Image(content.Load<Texture2D>("layers/help_screen"));
            textures[SpriteHook.SCREEN_80_LAYER] = new Image(content.Load<Texture2D>("layers/screen80"));
            textures[SpriteHook.SCREEN_50_LAYER] = new Image(content.Load<Texture2D>("layers/screen50"));
            textures[SpriteHook.SPLATTER_LAYER] = new Image(content.Load<Texture2D>("layers/splatter"));

            textures[SpriteHook.WINNER] = new Image(content.Load<Texture2D>("text/winner"));
            textures[SpriteHook.LOSER] = new Image(content.Load<Texture2D>("text/loser"));
            textures[SpriteHook.BOMB] = new Image(content.Load<Texture2D>("tokens/nuclear"));
            textures[SpriteHook.SKULL] = new Image(content.Load<Texture2D>("tokens/skull"));

            roboto = content.Load<SpriteFont>("text/roboto");
        }

        public void Update(GameTime gameTime) {
            foreach (Animation animation in animations) {
                animation.Update(gameTime);
            }
        }

        public void DrawCell(Cell cell, Rectangle targetRect, SpriteBatch spriteBatch) {
            if (cell.color == CellColor.BLACK) {
                return;
            }

            if (cell.color == CellColor.BOMB) {
                DrawCell(cell, CellColor.RED, targetRect, spriteBatch);
                spriteBatch.Draw(textures[SpriteHook.BOMB].GetTexture(), targetRect, Color.White);
                return;
            }

            if (cell.color == CellColor.SKULL) {
                DrawCell(cell, CellColor.PURPLE, targetRect, spriteBatch);
                spriteBatch.Draw(textures[SpriteHook.SKULL].GetTexture(), targetRect, Color.White);
                return;
            }

            DrawCell(cell, cell.color, targetRect, spriteBatch);
       }

        public void DrawCell(Cell cell, CellColor color, Rectangle targetRect, SpriteBatch spriteBatch) {
            int x = 0;
            int y = 0;
            switch (cell.direction) {
                case Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 0; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 0; y = 2; break; }
                case Cell.Direction.UP | Cell.Direction.RIGHT: { x = 0; y = 4; break; }
                case Cell.Direction.DOWN | Cell.Direction.LEFT | Cell.Direction.RIGHT: { x = 2; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.DOWN | Cell.Direction.RIGHT: { x = 2; y = 2; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.RIGHT: { x = 2; y = 4; break; }
                case Cell.Direction.DOWN | Cell.Direction.LEFT: { x = 4; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT | Cell.Direction.DOWN: { x = 4; y = 2; break; }
                case Cell.Direction.UP | Cell.Direction.LEFT: { x = 4; y = 4; break; }

                case Cell.Direction.RIGHT: { x = 6; y = 0; break; }
                case Cell.Direction.RIGHT | Cell.Direction.LEFT: { x = 7; y = 0; break; }
                case Cell.Direction.LEFT: { x = 8; y = 0; break; }

                case Cell.Direction.DOWN: { x = 10; y = 0; break; }
                case Cell.Direction.UP | Cell.Direction.DOWN: { x = 10; y = 1; break; }
                case Cell.Direction.UP: { x = 10; y = 2; break; }

                case Cell.Direction.NONE: { x = 6; y = 2; break; }
            }
            if (cell.locked) {
                x = 8;
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

            spriteBatch.Draw(colorTextures[color].GetTexture(), targetRect, sourceRect, highlight);
        }

        public void DrawLayer(SpriteHook sprite, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), new Vector2(), Color.White);
        }

        public void DrawLayer(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), targetRect, targetRect, Color.White);
        }

        public void DrawCentered(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            Texture2D texture = textures[sprite].GetTexture();
            Vector2 position;
            position.X = targetRect.Center.X - (texture.Bounds.Width / 2);
            position.Y = targetRect.Center.Y - (texture.Bounds.Height / 2);
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void Draw(SpriteHook sprite, Vector2 position, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), position, Color.White);
        }

        public void DrawString(String text, Color color, Vector2 position, SpriteBatch spriteBatch) {
            spriteBatch.DrawString(roboto, text, position, color);
        }

        private Dictionary<SpriteHook, Drawable> textures;
        private Dictionary<CellColor, Drawable> colorTextures;
        private List<Animation> animations;
        private SpriteFont roboto;
    }
}
