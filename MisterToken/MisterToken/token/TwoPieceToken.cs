using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class TwoPieceToken : Token {
        public TwoPieceToken(Board board, int row, int column, CellColor color1, CellColor color2) {
            piece = new TokenPiece[2];
            Cell cell1 = new Cell();
            Cell cell2 = new Cell();
            cell1.color = color1;
            cell2.color = color2;
            cell1.direction = Cell.Direction.RIGHT;
            cell2.direction = Cell.Direction.LEFT;
            piece[0] = new TokenPiece(board, row, column, cell1);
            piece[1] = new TokenPiece(board, row, column + 1, cell2);
            orientation = 0;
        }

        public override bool RotateRight(bool dryRun, bool allowWrap) {
            if (!dryRun) {
                if (!RotateRight(true, allowWrap)) {
                    return false;
                }
            }
            switch (orientation) {
                case 0:
                    if (!piece[1].Move(dryRun, 1, -1, allowWrap)) {
                        return false;
                    }
                    break;
                case 1:
                    if (piece[1].Move(true, -1, 1, allowWrap)) {
                        piece[0].Move(dryRun, 0, 1, allowWrap);
                        piece[1].Move(dryRun, -1, 0, allowWrap);
                    } else if (piece[0].Move(true, 1, 1, allowWrap)) {
                        piece[0].Move(dryRun, 1, 1, allowWrap);
                    } else if (piece[1].Move(true, -1, -1, allowWrap)) {
                        piece[1].Move(dryRun, -1, -1, allowWrap);
                    } else {
                        return false;
                    }
                    break;
                case 2:
                    if (!piece[0].Move(dryRun, 1, -1, allowWrap)) {
                        return false;
                    }
                    break;
                case 3:
                    if (piece[0].Move(true, -1, 1, allowWrap)) {
                        piece[0].Move(dryRun, -1, 0, allowWrap);
                        piece[1].Move(dryRun, 0, 1, allowWrap);
                    } else if (piece[1].Move(false, 1, 1, allowWrap)) {
                        piece[1].Move(dryRun, 1, 1, allowWrap);
                    } else if (piece[0].Move(false, -1, -1, allowWrap)) {
                        piece[0].Move(dryRun, -1, -1, false);
                    } else {
                        return false;
                    }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateRight();
                piece[1].RotateRight();
                orientation = (orientation + 1) % 4;
            }
            return true;
        }

        public override bool RotateLeft(bool dryRun, bool allowWrap) {
            if (!dryRun) {
                if (!RotateLeft(true, allowWrap)) {
                    return false;
                }
            }
            switch (orientation) {
                case 0:
                    if (!piece[0].Move(true, 1, 0, allowWrap) || !piece[1].Move(true, 0, -1, allowWrap)) {
                        return false;
                    }
                    piece[0].Move(dryRun, 1, 0, allowWrap);
                    piece[1].Move(dryRun, 0, -1, allowWrap);
                    break;
                case 1:
                    if (piece[1].Move(true, -1, 1, allowWrap)) {
                        piece[1].Move(dryRun, -1, 1, allowWrap);
                    } else if (piece[0].Move(true, 1, 1, allowWrap)) {
                        piece[0].Move(dryRun, 1, 0, allowWrap);
                        piece[1].Move(dryRun, 0, 1, allowWrap);
                    } else if (piece[1].Move(true, -1, -1, allowWrap)) {
                        piece[0].Move(dryRun, 0, -1, allowWrap);
                        piece[1].Move(dryRun, -1, 0, allowWrap);
                    } else {
                        return false;
                    }
                    break;
                case 2:
                    if (!piece[0].Move(true, 0, -1, allowWrap) || !piece[1].Move(true, 1, 0, allowWrap)) {
                        return false;
                    }
                    piece[0].Move(dryRun, 0, -1, allowWrap);
                    piece[1].Move(dryRun, 1, 0, allowWrap);
                    break;
                case 3:
                    if (piece[0].Move(true, -1, 1, allowWrap)) {
                        piece[0].Move(dryRun, -1, 1, allowWrap);
                    } else if (piece[1].Move(true, 1, 1, allowWrap)) {
                        piece[0].Move(dryRun, 0, 1, allowWrap);
                        piece[1].Move(dryRun, 1, 0, allowWrap);
                    } else if (piece[0].Move(true, -1, -1, allowWrap)) {
                        piece[0].Move(dryRun, -1, 0, allowWrap);
                        piece[1].Move(dryRun, 0, -1, allowWrap);
                    } else {
                        return false;
                    }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateLeft();
                piece[1].RotateLeft();
                if (--orientation < 0) {
                    orientation = 3;
                }
            }
            return true;
        }

        // The rotation of the piece:
        // 0 - AB
        // 1 - A
        //     B
        // 2 - BA
        // 3 - B
        //     A
        private int orientation;
    }
}
