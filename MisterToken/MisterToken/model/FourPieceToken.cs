using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class FourPieceToken : Token {
        public FourPieceToken(Board board, int row, int column, CellColor color1, CellColor color2, CellColor color3, CellColor color4) {
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
            cell1.direction = Cell.Direction.RIGHT | Cell.Direction.DOWN;
            cell2.direction = Cell.Direction.LEFT | Cell.Direction.DOWN;
            cell3.direction = Cell.Direction.UP | Cell.Direction.LEFT;
            cell4.direction = Cell.Direction.UP | Cell.Direction.RIGHT;
            piece[0] = new TokenPiece(board, row, column, cell1);
            piece[1] = new TokenPiece(board, row, column + 1, cell2);
            piece[2] = new TokenPiece(board, row + 1, column + 1, cell3);
            piece[3] = new TokenPiece(board, row + 1, column, cell4);
            orientation = 0;
        }

        public override void RotateRight(bool allowWrap) {
            switch (orientation) {
                case 0:
                    piece[0].Move(0, 1);
                    piece[1].Move(1, 0);
                    piece[2].Move(0, -1);
                    piece[3].Move(-1, 0);
                    break;
                case 1:
                    piece[0].Move(1, 0);
                    piece[1].Move(0, -1);
                    piece[2].Move(-1, 0);
                    piece[3].Move(0, 1);
                    break;
                case 2:
                    piece[0].Move(0, -1);
                    piece[1].Move(-1, 0);
                    piece[2].Move(0, 1);
                    piece[3].Move(1, 0);
                    break;
                case 3:
                    piece[0].Move(-1, 0);
                    piece[1].Move(0, 1);
                    piece[2].Move(1, 0);
                    piece[3].Move(0, -1);
                    break;
            }
            piece[0].RotateRight();
            piece[1].RotateRight();
            piece[2].RotateRight();
            piece[3].RotateRight();
            orientation = (orientation + 1) % 4;
        }

        public override void RotateLeft(bool allowWrap) {
            switch (orientation) {
                case 0:
                    piece[0].Move(1, 0);
                    piece[1].Move(0, -1);
                    piece[2].Move(-1, 0);
                    piece[3].Move(0, 1);
                    break;
                case 1:
                    piece[0].Move(0, -1);
                    piece[1].Move(-1, 0);
                    piece[2].Move(0, 1);
                    piece[3].Move(1, 0);
                    break;
                case 2:
                    piece[0].Move(-1, 0);
                    piece[1].Move(0, 1);
                    piece[2].Move(1, 0);
                    piece[3].Move(0, -1);
                    break;
                case 3:
                    piece[0].Move(0, 1);
                    piece[1].Move(1, 0);
                    piece[2].Move(0, -1);
                    piece[3].Move(-1, 0);
                    break;
            }
            piece[0].RotateLeft();
            piece[1].RotateLeft();
            piece[2].RotateLeft();
            piece[3].RotateLeft();
            if (--orientation < 0) {
                orientation = 3;
            }
        }

        public override bool CanRotateRight(bool allowWrap) {
            return true;
        }

        public override bool CanRotateLeft(bool allowWrap) {
            return true;
        }

        // The rotation of the piece:
        // 0 - AB
        //     DC
        // 1 - DA
        //     CB
        // 2 - CD
        //     BA
        // 3 - BC
        //     AD
        private int orientation;
    }
}
