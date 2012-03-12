using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class SkullToken : Token {
        public SkullToken(Board board, int row, int column) {
            piece = new TokenPiece[1];
            Cell cell = new Cell();
            cell.color = CellColor.SKULL;
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
