﻿using System;
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

        public static void Update(GameTime gameTime) {
            manager.Update(gameTime);
        }

        public static void LoadContent(ContentManager content, GraphicsDevice device) {
            manager.LoadContent(content, device);
        }

        public static void DrawCell(Cell cell, Rectangle targetRect, SpriteBatch spriteBatch) {
            manager.DrawCell(cell, targetRect, spriteBatch);
        }

        public static void DrawGauge(float amount, Rectangle destination, SpriteBatch spriteBatch) {
            manager.DrawGauge(amount, destination, spriteBatch);
        }

        public static void DrawRotatedAndScaled(SpriteHook sprite, Rectangle destination, float rotationInRadians, SpriteBatch spriteBatch) {
            manager.DrawRotatedAndScaled(sprite, destination, rotationInRadians, spriteBatch);
        }

        public static void DrawRotatedAndCentered(SpriteHook sprite, Rectangle destination, float rotationInRadians, SpriteBatch spriteBatch) {
            manager.DrawRotatedAndCentered(sprite, destination, rotationInRadians, spriteBatch);
        }

        public static void DrawLayer(SpriteHook sprite, SpriteBatch spriteBatch) {
            manager.DrawLayer(sprite, spriteBatch);
        }

        public static void DrawLayer(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            manager.DrawLayer(sprite, targetRect, spriteBatch);
        }

        public static void DrawCentered(SpriteHook sprite, Rectangle targetRect, SpriteBatch spriteBatch) {
            manager.DrawCentered(sprite, targetRect, spriteBatch);
        }

        public static void DrawNumberCentered(int number, Rectangle targetRect, SpriteBatch spriteBatch) {
            manager.DrawNumberCentered(number, targetRect, spriteBatch);
        }

        public static void Draw(SpriteHook sprite, Vector2 position, SpriteBatch spriteBatch) {
            manager.Draw(sprite, position, spriteBatch);
        }

        public static void DrawText(String text, Color color, Vector2 position, SpriteBatch spriteBatch) {
            manager.DrawString(text, color, position, spriteBatch);
        }

        private static SpriteManager manager = new SpriteManager();
    }
}
