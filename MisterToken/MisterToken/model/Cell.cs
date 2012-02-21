using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Cell {
        public Cell() {
            this.locked = false;
            this.loose = false;
            this.matched = false;
            this.visited = false;
        }

        public void DrawRect(Rectangle rect, SpriteBatch spriteBatch) {
            if (matched) {
                spriteBatch.Draw(Sprites.GetTextureForCell(this), rect, Microsoft.Xna.Framework.Color.Gray);
            } else {
                spriteBatch.Draw(Sprites.GetTextureForCell(this), rect, Microsoft.Xna.Framework.Color.White);
            }
        }

        public void RotateRight() {
            Direction newDirection = Direction.NONE;
            if (direction.HasFlag(Direction.UP)) newDirection |= Direction.RIGHT;
            if (direction.HasFlag(Direction.RIGHT)) newDirection |= Direction.DOWN;
            if (direction.HasFlag(Direction.DOWN)) newDirection |= Direction.LEFT;
            if (direction.HasFlag(Direction.LEFT)) newDirection |= Direction.UP;
            direction = newDirection;
        }

        public void RotateLeft() {
            Direction newDirection = Direction.NONE;
            if (direction.HasFlag(Direction.UP)) newDirection |= Direction.LEFT;
            if (direction.HasFlag(Direction.RIGHT)) newDirection |= Direction.UP;
            if (direction.HasFlag(Direction.DOWN)) newDirection |= Direction.RIGHT;
            if (direction.HasFlag(Direction.LEFT)) newDirection |= Direction.DOWN;
            direction = newDirection;
        }

        public void Clear() {
            color = Color.BLACK;
            direction = Direction.NONE;
            visited = false;
            loose = false;
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

        [Flags]
        public enum Direction {
            NONE = 0x00,
            UP = 0x01,
            RIGHT = 0x02,
            DOWN = 0x04,
            LEFT = 0x08,
        }
        public Direction direction;

        public bool visited;
        public bool loose;
        public bool matched;
        public bool locked;
    }
}
