using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class FourPieceTToken : Token {
        public FourPieceTToken(Board board, int row, int column, CellColor color1, CellColor color2, CellColor color3, CellColor color4) {
            Random random = new Random();
            piece = new TokenPiece[4];
            Cell cell1 = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            Cell cell4 = new Cell();
            cell1.color = color1;
            cell2.color = color2;
            cell3.color = color3;
            cell4.color = color4;
            cell1.direction = Cell.Direction.DOWN;
            cell2.direction = Cell.Direction.RIGHT;
            cell3.direction = Cell.Direction.LEFT | Cell.Direction.RIGHT | Cell.Direction.UP;
            cell4.direction = Cell.Direction.LEFT;
            piece[0] = new TokenPiece(board, row, column + 1, cell1);
            piece[1] = new TokenPiece(board, row + 1, column, cell2);
            piece[2] = new TokenPiece(board, row + 1, column + 1, cell3);
            piece[3] = new TokenPiece(board, row + 1, column + 2, cell4);
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
                    if (!piece[0].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, 1, -2, allowWrap)) { return false; }
                    break;
                case 1:
                    if (!piece[1].Move(dryRun, 0, 2, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, -1, 1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, -2, 0, allowWrap)) { return false; }
                    break;
                case 2:
                    if (!piece[0].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 2, -1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    break;
                case 3:
                    if (!piece[0].Move(dryRun, -1, 1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, -1, -1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, 1, 1, allowWrap)) { return false; }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateRight();
                piece[1].RotateRight();
                piece[2].RotateRight();
                piece[3].RotateRight();
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
                    if (!piece[0].Move(dryRun, 1, -1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 1, 1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, -1, -1, allowWrap)) { return false; }
                    break;
                case 1:
                    if (!piece[0].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, -1, 2, allowWrap)) { return false; }
                    break;
                case 2:
                    if (!piece[1].Move(dryRun, 0, -2, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 1, -1, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, 2, 0, allowWrap)) { return false; }
                    break;
                case 3:
                    if (!piece[0].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, -2, 1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[3].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateLeft();
                piece[1].RotateLeft();
                piece[2].RotateLeft();
                piece[3].RotateLeft();
                if (--orientation < 0) {
                    orientation = 3;
                }
            }
            return true;
        }

        // The rotation of the piece:
        // 0 -  A
        //     BCD
        //
        // 1 - B
        //     CA
        //     D
        //
        // 2 - DCB
        //      A
        //
        // 3 -  D
        //     AC
        //      B
        private int orientation;
    }
}
