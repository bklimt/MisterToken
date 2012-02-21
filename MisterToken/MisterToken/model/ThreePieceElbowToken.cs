using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class ThreePieceElbowToken : Token {
        public ThreePieceElbowToken(Board board, int row, int column, Cell.Color color1, Cell.Color color2, Cell.Color color3) {
            Random random = new Random();
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

        public override void RotateRight() {
            switch (orientation) {
                case 0:
                    piece[0].Move(0, 1);
                    piece[1].Move(1, 0);
                    piece[2].Move(0, -1);
                    break;
                case 1:
                    piece[0].Move(1, 0);
                    piece[1].Move(0, -1);
                    piece[2].Move(-1, 0);
                    break;
                case 2:
                    piece[0].Move(0, -1);
                    piece[1].Move(-1, 0);
                    piece[2].Move(0, 1);
                    break;
                case 3:
                    piece[0].Move(-1, 0);
                    piece[1].Move(0, 1);
                    piece[2].Move(1, 0);
                    break;
            }
            piece[0].RotateRight();
            piece[1].RotateRight();
            piece[2].RotateRight();
            orientation = (orientation + 1) % 4;
        }

        public override void RotateLeft() {
            switch (orientation) {
                case 0:
                    piece[0].Move(1, 0);
                    piece[1].Move(0, -1);
                    piece[2].Move(-1, 0);
                    break;
                case 1:
                    piece[0].Move(0, -1);
                    piece[1].Move(-1, 0);
                    piece[2].Move(0, 1);
                    break;
                case 2:
                    piece[0].Move(-1, 0);
                    piece[1].Move(0, 1);
                    piece[2].Move(1, 0);
                    break;
                case 3:
                    piece[0].Move(0, 1);
                    piece[1].Move(1, 0);
                    piece[2].Move(0, -1);
                    break;
            }
            piece[0].RotateLeft();
            piece[1].RotateLeft();
            piece[2].RotateLeft();
            if (--orientation < 0) {
                orientation = 3;
            }
        }

        public override bool CanRotateRight() {
            switch (orientation) {
                case 0:
                    return piece[2].CanMove(0, -1);
                case 1:
                    return piece[2].CanMove(-1, 0);
                case 2:
                    return piece[2].CanMove(0, 1);
                case 3:
                    return piece[2].CanMove(1, 0);
            }
            return false;
        }

        public override bool CanRotateLeft() {
            switch (orientation) {
                case 0:
                    return piece[2].CanMove(0, -1);
                case 1:
                    return piece[2].CanMove(-1, 0);
                case 2:
                    return piece[2].CanMove(0, 1);
                case 3:
                    return piece[2].CanMove(1, 0);
            }
            return false;
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
