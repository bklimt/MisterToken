using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public abstract class Token {
        public void Commit() {
            foreach (TokenPiece p in piece) {
                p.Commit();
            }
        }

        public bool IsValid() {
            foreach (TokenPiece p in piece)
                if (!p.IsValid())
                    return false;
            return true;
        }

        public bool CanMove(int deltaRow, int deltaColumn) {
            foreach (TokenPiece p in piece)
                if (!p.CanMove(deltaRow, deltaColumn))
                    return false;
            return true;
        }

        public void Move(int deltaRow, int deltaColumn) {
            foreach (TokenPiece p in piece)
                p.Move(deltaRow, deltaColumn);
        }

        public abstract void RotateRight();
        public abstract void RotateLeft();
        public abstract bool CanRotateRight();
        public abstract bool CanRotateLeft();

        public void DrawRect(Rectangle boardRect, SpriteBatch spriteBatch) {
            foreach (TokenPiece p in piece) {
                p.Cell.DrawRect(Board.GetCellPosition(boardRect, p.Row, p.Column), spriteBatch);
            }
        }

        protected TokenPiece[] piece;
    }
}
