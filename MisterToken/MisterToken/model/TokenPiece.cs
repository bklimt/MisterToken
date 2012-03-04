using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class TokenPiece {
        public TokenPiece(Board board, int row, int column, Cell cell) {
            this.board = board;
            this.row = row;
            this.column = column;
            this.cell = cell;
        }

        public bool CanMove(int deltaRow, int deltaColumn, bool allowWrap) {
            if (row + deltaRow < 0) {
                return false;
            }
            if (row + deltaRow >= Constants.ROWS) {
                return false;
            }
            if (!allowWrap && (((column + deltaColumn) < 0) || ((column + deltaColumn >= Constants.COLUMNS)))) {
                return false;
            }
            return (board.GetColor(row + deltaRow, column + deltaColumn) == CellColor.BLACK);
        }

        public void Move(int deltaRow, int deltaColumn) {
            row += deltaRow;
            column += deltaColumn;
            if (row < 0)
                row += Constants.ROWS;
            if (column < 0)
                column += Constants.COLUMNS;
            row %= Constants.ROWS;
            column %= Constants.COLUMNS;
        }

        public bool IsValid() {
            return (board.GetColor(row, column) == 0);
        }

        public void Commit() {
            board.SetCell(row, column, cell);
        }

        public void RotateRight() {
            cell.RotateRight();
        }

        public void RotateLeft() {
            cell.RotateLeft();
        }

        public int Row { get { return row; } }
        public int Column { get { return column; } }
        public Cell Cell { get { return cell; } }

        private int row;
        private int column;
        private Cell cell;

        private Board board;
    }
}
