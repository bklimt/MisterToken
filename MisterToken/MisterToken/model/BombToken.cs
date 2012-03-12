using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class BombToken : Token {
        public BombToken(Board board, int row, int column) {
            piece = new TokenPiece[1];
            Cell cell = new Cell();
            cell.color = CellColor.BLACK;
            cell.bomb = true;
            piece[0] = new TokenPiece(board, row, column, cell);
        }

        public override bool RotateRight(bool dryRun, bool allowWrap) {
            return true;
        }

        public override bool RotateLeft(bool dryRun, bool allowWrap) {
            return true;
        }
    }
}
