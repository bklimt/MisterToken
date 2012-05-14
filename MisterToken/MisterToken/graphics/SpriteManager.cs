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
            textures[SpriteHook.SPLATTER_LAYER] = new Image(content.Load<Texture2D>("layers/splatter"));
            textures[SpriteHook.CLOUD_LAYER] = new Image(content.Load<Texture2D>("layers/cloud"));
            textures[SpriteHook.MENU_OVERLAY_LAYER] = new Image(content.Load<Texture2D>("layers/menu_overlay"));
            textures[SpriteHook.TITLE_OVERLAY_LAYER] = new Image(content.Load<Texture2D>("layers/title_overlay"));

            textures[SpriteHook.GAUGE_BACKGROUND] = new Image(content.Load<Texture2D>("gauge/background"));
            textures[SpriteHook.GAUGE_ARROW] = new Image(content.Load<Texture2D>("gauge/arrow"));
            textures[SpriteHook.GAUGE_GLASS] = new Image(content.Load<Texture2D>("gauge/glass"));
            textures[SpriteHook.GAUGE_MATCH] = new Image(content.Load<Texture2D>("gauge/match"));
            textures[SpriteHook.GAUGE_GAME] = new Image(content.Load<Texture2D>("gauge/game"));

            textures[SpriteHook.BOMB] = new Image(content.Load<Texture2D>("tokens/nuclear"));
            textures[SpriteHook.SKULL] = new Image(content.Load<Texture2D>("tokens/skull"));
            textures[SpriteHook.WINNER] = new Image(content.Load<Texture2D>("text/winner"));
            textures[SpriteHook.LOSER] = new Image(content.Load<Texture2D>("text/loser"));
            textures[SpriteHook.MENU_PANEL] = new Image(content.Load<Texture2D>("menu_panel_large"));
            textures[SpriteHook.MENU_CHECK_OVERLAY] = new Image(content.Load<Texture2D>("check_overlay"));

            digitTextures = new Dictionary<int, Drawable>();
            digitTextures[0] = new Image(content.Load<Texture2D>("text/0"));
            digitTextures[1] = new Image(content.Load<Texture2D>("text/1"));
            digitTextures[2] = new Image(content.Load<Texture2D>("text/2"));
            digitTextures[3] = new Image(content.Load<Texture2D>("text/3"));
            digitTextures[4] = new Image(content.Load<Texture2D>("text/4"));
            digitTextures[5] = new Image(content.Load<Texture2D>("text/5"));
            digitTextures[6] = new Image(content.Load<Texture2D>("text/6"));
            digitTextures[7] = new Image(content.Load<Texture2D>("text/7"));
            digitTextures[8] = new Image(content.Load<Texture2D>("text/8"));
            digitTextures[9] = new Image(content.Load<Texture2D>("text/9"));

            roboto18 = content.Load<SpriteFont>("text/roboto18");
            roboto36 = content.Load<SpriteFont>("text/roboto36");
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

        public void DrawGauge(float amount, bool forMatch, Rectangle destination, SpriteBatch spriteBatch) {
            // Center the gauge.
            int centerX = destination.Center.X;
            int centerY = destination.Center.Y;
            int size = Math.Min(destination.Width, destination.Height);
            destination.X = centerX - size / 2;
            destination.Y = centerY - size / 2;
            destination.Width = size;
            destination.Height = size;

            // Draw the thing.
            DrawScaled(SpriteHook.GAUGE_BACKGROUND, destination, spriteBatch);
            DrawRotatedAndScaled(SpriteHook.GAUGE_ARROW, destination, (float)(Math.PI * amount), spriteBatch);
            DrawScaled(SpriteHook.GAUGE_GLASS, destination, spriteBatch);
            if (forMatch) {
                DrawScaled(SpriteHook.GAUGE_MATCH, destination, spriteBatch);
            } else {
                DrawScaled(SpriteHook.GAUGE_GAME, destination, spriteBatch);
            }
        }

        public void DrawRotatedAndCentered(SpriteHook sprite, Rectangle destination, float rotationInRadians, SpriteBatch spriteBatch) {
            int centerX = destination.Center.X;
            int centerY = destination.Center.Y;
            int width = textures[sprite].GetTexture().Width;
            int height = textures[sprite].GetTexture().Height;
            destination.X = centerX - width / 2;
            destination.Y = centerY - height / 2;
            destination.Width = width;
            destination.Height = height;
            DrawRotatedAndScaled(sprite, destination, rotationInRadians, spriteBatch);
        }

        public void DrawRotatedAndScaled(SpriteHook sprite, Rectangle destination, float rotationInRadians, SpriteBatch spriteBatch) {
            // Alter the destination (x, y) so that the sprite rotates about its center instead of the origin.
            int originalX = destination.X;
            int originalY = destination.Y;
            int centerX = originalX + destination.Width / 2;
            int centerY = originalY + destination.Height / 2;
            double radius = Math.Sqrt(destination.Width * destination.Width + destination.Height * destination.Height) / 2.0;
            double originalAngle = Math.Asin((centerY - originalY) / radius) - Math.PI;
            destination.X = (int)Math.Round(centerX + radius * Math.Cos(rotationInRadians + originalAngle));
            destination.Y = (int)Math.Round(centerY + radius * Math.Sin(rotationInRadians + originalAngle));
            spriteBatch.Draw(textures[sprite].GetTexture(), destination, null, Color.White, rotationInRadians, new Vector2(), SpriteEffects.None, 0);
        }

        public void DrawLayer(SpriteHook sprite, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), new Vector2(), Color.White);
        }

        public void DrawLayer(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), targetRect, targetRect, Color.White);
        }

        private void DrawCentered(Texture2D texture, Rectangle targetRect, SpriteBatch spriteBatch) {
            Vector2 position;
            position.X = targetRect.Center.X - (texture.Bounds.Width / 2);
            position.Y = targetRect.Center.Y - (texture.Bounds.Height / 2);
            spriteBatch.Draw(texture, position, Color.White);
        }

        public void DrawCentered(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            DrawCentered(textures[sprite].GetTexture(), targetRect, spriteBatch);
        }

        public void DrawNumberCentered(int number, Rectangle targetRect, SpriteBatch spriteBatch) {
            if (number < 0) {
                throw new Exception("Attempt to draw negative number.");
            }
            if (number == 0) {
                DrawCentered(digitTextures[0].GetTexture(), targetRect, spriteBatch);
                return;
            }

            int kerning = -15;

            int width = 0;
            int height = 0;
            List<Texture2D> textures = new List<Texture2D>();
            while (number > 0) {
                Texture2D texture = digitTextures[number % 10].GetTexture();
                textures.Add(texture);
                width += texture.Bounds.Width;
                height = Math.Max(texture.Bounds.Height, height);
                number /= 10;
                width += kerning;
            }
            textures.Reverse();

            Vector2 position;
            position.X = targetRect.Center.X - (width / 2);
            position.Y = targetRect.Center.Y - (height / 2);
            foreach (Texture2D texture in textures) {
                spriteBatch.Draw(texture, position, Color.White);
                position.X += texture.Bounds.Width;
                position.X += kerning;
            }
        }

        public void DrawScaled(SpriteHook sprite, Rectangle destination, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), destination, Color.White);
        }

        public void Draw(SpriteHook sprite, Vector2 position, SpriteBatch spriteBatch) {
            spriteBatch.Draw(textures[sprite].GetTexture(), position, Color.White);
        }

        public void DrawText(String text, Color color, Vector2 position, SpriteBatch spriteBatch) {
            spriteBatch.DrawString(roboto18, text, position, color);
        }

        public void DrawText(String text, Color color, Vector2 position, bool large, SpriteBatch spriteBatch) {
            spriteBatch.DrawString(large ? roboto36 : roboto18, text, position, color);
        }

        private Dictionary<SpriteHook, Drawable> textures;
        private Dictionary<CellColor, Drawable> colorTextures;
        private Dictionary<Int32, Drawable> digitTextures;
        private List<Animation> animations;
        private SpriteFont roboto18;
        private SpriteFont roboto36;
    }
}
