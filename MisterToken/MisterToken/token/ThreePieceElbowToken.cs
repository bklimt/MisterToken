using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class ThreePieceElbowToken : Token {
        public ThreePieceElbowToken(Board board, int row, int column, CellColor color1, CellColor color2, CellColor color3) {
            piece = new TokenPiece[3];
            Cell cell1 = new Cell();
            Cell cell2 = new Cell();
            Cell cell3 = new Cell();
            cell1.color = color1;
            cell2.color = color2;
            cell3.color = color3;
            cell1.direction = Cell.Direction.RIGHT;
            cell2.direction = Cell.Direction.LEFT | Cell.Direction.DOWN;
            cell3.direction = Cell.Direction.UP;
            piece[0] = new TokenPiece(board, row, column, cell1);
            piece[1] = new TokenPiece(board, row, column + 1, cell2);
            piece[2] = new TokenPiece(board, row + 1, column + 1, cell3);
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
                    if (!piece[0].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    break;
                case 1:
                    if (!piece[0].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    break;
                case 2:
                    if (!piece[0].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    break;
                case 3:
                    if (!piece[0].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateRight();
                piece[1].RotateRight();
                piece[2].RotateRight();
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
                    if (!piece[0].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    break;
                case 1:
                    if (!piece[0].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    break;
                case 2:
                    if (!piece[0].Move(dryRun, -1, 0, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    break;
                case 3:
                    if (!piece[0].Move(dryRun, 0, 1, allowWrap)) { return false; }
                    if (!piece[1].Move(dryRun, 1, 0, allowWrap)) { return false; }
                    if (!piece[2].Move(dryRun, 0, -1, allowWrap)) { return false; }
                    break;
            }
            if (!dryRun) {
                piece[0].RotateLeft();
                piece[1].RotateLeft();
                piece[2].RotateLeft();
                if (--orientation < 0) {
                    orientation = 3;
                }
            }
            return true;
        }

        // The rotation of the piece:
        // 0 - AB
        //      C
        // 1 -  A
        //     CB
        // 2 - C
        //     BA
        // 3 - BC
        //     A
        private int orientation;
    }
}
