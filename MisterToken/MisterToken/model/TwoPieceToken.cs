using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class TwoPieceToken : Token {
        public TwoPieceToken(Board board, int row, int column, CellColor color1, CellColor color2) {
            Random random = new Random();
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

        public override void RotateRight() {
            switch (orientation) {
                case 0:
                    piece[1].Move(1, -1);
                    break;
                case 1:
                    piece[0].Move(0, 1);
                    piece[1].Move(-1, 0);
                    break;
                case 2:
                    piece[0].Move(1, -1);
                    break;
                case 3:
                    piece[0].Move(-1, 0);
                    piece[1].Move(0, 1);
                    break;
            }
            piece[0].RotateRight();
            piece[1].RotateRight();
            orientation = (orientation + 1) % 4;
        }

        public override void RotateLeft() {
            switch (orientation) {
                case 0:
                    piece[0].Move(1, 0);
                    piece[1].Move(0, -1);
                    break;
                case 1:
                    piece[1].Move(-1, 1);
                    break;
                case 2:
                    piece[0].Move(0, -1);
                    piece[1].Move(1, 0);
                    break;
                case 3:
                    piece[0].Move(-1, 1);
                    break;
            }
            piece[0].RotateLeft();
            piece[1].RotateLeft();
            if (--orientation < 0) {
                orientation = 3;
            }
        }

        public override bool CanRotateRight() {
            switch (orientation) {
                case 0:
                    return piece[1].CanMove(1, -1);
                case 1:
                    return piece[0].CanMove(0, 1) && piece[1].CanMove(-1, 0);
                case 2:
                    return piece[0].CanMove(1, -1);
                case 3:
                    return piece[0].CanMove(-1, 0) && piece[1].CanMove(0, 1);
            }
            return false;
        }

        public override bool CanRotateLeft() {
            switch (orientation) {
                case 0:
                    return piece[0].CanMove(1, 0) && piece[1].CanMove(0, -1);
                case 1:
                    return piece[1].CanMove(-1, 1);
                case 2:
                    return piece[0].CanMove(0, -1) && piece[1].CanMove(1, 0);
                case 3:
                    return piece[0].CanMove(-1, 1);
            }
            return false;
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
