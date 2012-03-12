using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Cell {
        public Cell() {
            this.color = CellColor.BLACK;
            this.locked = false;
            this.loose = false;
            this.matched = false;
            this.visited = false;
            this.bomb = false;
        }

        public Cell(Cell other) {
            this.color = other.color;
            this.locked = other.locked;
            this.loose = other.loose;
            this.matched = other.matched;
            this.visited = other.visited;
            this.bomb = other.bomb;
        }

        public void DrawRect(Rectangle rect, SpriteBatch spriteBatch) {
            Sprites.DrawCell(this, rect, spriteBatch);
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
            color = CellColor.BLACK;
            direction = Direction.NONE;
            visited = false;
            loose = false;
            matched = false;
            locked = false;
            bomb = false;
        }

        public CellColor color;

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
        public bool bomb;
    }
}
