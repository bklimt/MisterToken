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

        public bool Move(bool dryRun, int deltaRow, int deltaColumn, bool allowWrap) {
            return Move(dryRun, deltaRow, deltaColumn, allowWrap, false);
        }

        public bool Move(bool dryRun, int deltaRow, int deltaColumn, bool allowWrap, bool force) {
            if (!force && !dryRun) {
                if (!Move(true, deltaRow, deltaColumn, allowWrap, false)) {
                    return false;
                }
            }
            foreach (TokenPiece p in piece) {
                if (!p.Move(dryRun, deltaRow, deltaColumn, allowWrap, force)) {
                    return false;
                }
            }
            return true;
        }

        public abstract bool RotateRight(bool dryRun, bool allowWrap);
        public abstract bool RotateLeft(bool dryRun, bool allowWrap);

        public void DrawRect(Rectangle boardRect, SpriteBatch spriteBatch) {
            foreach (TokenPiece p in piece) {
                p.Cell.DrawRect(Board.GetCellPosition(boardRect, p.Row, p.Column), spriteBatch);
            }
        }

        protected TokenPiece[] piece;
    }
}
