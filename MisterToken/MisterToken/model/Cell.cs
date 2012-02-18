using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Cell {
        public Cell(SpriteManager spriteManager) {
            this.spriteManager = spriteManager;
            this.locked = false;
            this.matched = false;
        }

        public void DrawRect(Rectangle rect, SpriteBatch spriteBatch) {
            if (matched) {
                spriteBatch.Draw(spriteManager.GetTextureForCell(this), rect, Microsoft.Xna.Framework.Color.Gray);
            } else {
                spriteBatch.Draw(spriteManager.GetTextureForCell(this), rect, Microsoft.Xna.Framework.Color.White);
            }
        }

        public void DrawQuad(Vector3 topLeft, Vector3 topRight, Vector3 bottomRight, Vector3 bottomLeft, QuadDrawer quadDrawer) {
            quadDrawer.Draw(spriteManager.GetTextureForCell(this), topLeft, topRight, bottomRight, bottomLeft);
        }

        public void DrawQuad(Rectangle rect, QuadDrawer quadDrawer) {
            DrawQuad(new Vector3(rect.Left, rect.Top, 0),
                     new Vector3(rect.Right, rect.Top, 0),
                     new Vector3(rect.Right, rect.Bottom, 0),
                     new Vector3(rect.Left, rect.Bottom, 0),
                     quadDrawer);
        }

        public void RotateRight() {
            switch (direction) {
                case Direction.UP: direction = Direction.RIGHT; break;
                case Direction.RIGHT: direction = Direction.DOWN; break;
                case Direction.DOWN: direction = Direction.LEFT; break;
                case Direction.LEFT: direction = Direction.UP; break;
            }
        }

        public void RotateLeft() {
            switch (direction) {
                case Direction.UP: direction = Direction.LEFT; break;
                case Direction.RIGHT: direction = Direction.UP; break;
                case Direction.DOWN: direction = Direction.RIGHT; break;
                case Direction.LEFT: direction = Direction.DOWN; break;
            }
        }

        public void Clear() {
            color = Color.BLACK;
            matched = false;
            locked = false;
        }

        public static Color GetRandomColor(Random random) {
            switch (random.Next(COLORS)) {
                case 0: return Color.RED;
                case 1: return Color.YELLOW;
                case 2: return Color.BLUE;
                default: throw new InvalidOperationException("Invalid color index?");
            }
        }

        public const int COLORS = 3;
        public enum Color {
            BLACK,
            RED,
            YELLOW,
            BLUE,
        }
        public Color color;

        public enum Direction {
            NONE,
            UP,
            RIGHT,
            DOWN,
            LEFT,
        }
        public Direction direction;

        public bool matched;
        public bool locked;

        private SpriteManager spriteManager;
    }
}
