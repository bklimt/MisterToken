using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Board {
        public Board() {
            rowOffset = 0;
            columnOffset = 0;

            entries = new Cell[Constants.ROWS, Constants.COLUMNS];
            for (int i = 0; i < Constants.ROWS; i++) {
                for (int j = 0; j < Constants.COLUMNS; j++) {
                    entries[i, j] = new Cell();
                }
            }
        }

        public void Setup(Level level) {
            level.SetupBoard(this);
            MarkMatches();
            ClearMatches();
        }

        public Cell GetCell(int row, int column) {
            while (row < 0)
                row += Constants.ROWS;
            while (column < 0)
                column += Constants.COLUMNS;
            return entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS];
        }

        public CellColor GetColor(int row, int column) {
            return GetCell(row, column).color;
        }

        public void SetCell(int row, int column, Cell cell) {
            while (row < 0)
                row += Constants.ROWS;
            while (column < 0)
                column += Constants.COLUMNS;
            entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].color = cell.color;
            entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].direction = cell.direction;
            entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].bomb = cell.bomb;
        }

        public void AddNewRow(Level level) {
            Random random = new Random();
            rowOffset = (rowOffset + 1) % Constants.ROWS;
            int bottomRow = ((Constants.ROWS - 1) + rowOffset) % Constants.ROWS;
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                entries[bottomRow, i].color = level.GetRandomColor();
            }
        }

        public void ShiftRight() {
            columnOffset = (columnOffset + 1) % Constants.COLUMNS;
        }

        public void ShiftLeft() {
            columnOffset--;
            if (columnOffset < 0)
                columnOffset = Constants.COLUMNS - 1;
        }

        public static Rectangle GetCellPosition(Rectangle boardRect, int row, int column) {
            Rectangle tileRect;
            tileRect.X = boardRect.Left + column * Constants.CELL_SIZE;
            tileRect.Y = boardRect.Top + row * Constants.CELL_SIZE;
            tileRect.Width = Constants.CELL_SIZE;
            tileRect.Height = Constants.CELL_SIZE;
            return tileRect;
        }

        public void DrawRect(Rectangle boardRect, SpriteBatch spriteBatch) {
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    GetCell(row, column).DrawRect(GetCellPosition(boardRect, row, column), spriteBatch);
                }
            }
        }

        public List<CellColor> MarkMatches() {
            VerifyBoard();
            List<CellColor> byBomb = MarkMatchesByBomb();
            List<CellColor> byRow = MarkMatchesByRow();
            List<CellColor> byColor = MarkMatchesByColor();
            byBomb.AddRange(byRow);
            byBomb.AddRange(byColor);
            return byBomb;
        }

        private List<CellColor> MarkMatchesByBomb() {
            List<CellColor> colors = new List<CellColor>();
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetCell(row, column).bomb) {
                        CellColor match = CellColor.BLACK;
                        if (row > 0) {
                            if (GetCell(row - 1, column - 1).color != CellColor.BLACK) {
                                GetCell(row - 1, column - 1).matched = true;
                                match = GetCell(row - 1, column - 1).color;
                            }
                            if (GetCell(row - 1, column).color != CellColor.BLACK) {
                                GetCell(row - 1, column).matched = true;
                                match = GetCell(row - 1, column).color;
                            }
                            if (GetCell(row - 1, column + 1).color != CellColor.BLACK) {
                                GetCell(row - 1, column + 1).matched = true;
                                match = GetCell(row - 1, column + 1).color;
                            }
                        }
                        if (GetCell(row, column - 1).color != CellColor.BLACK) {
                            GetCell(row, column - 1).matched = true;
                            match = GetCell(row, column - 1).color;
                        }
                        GetCell(row, column).Clear();
                        if (GetCell(row, column + 1).color != CellColor.BLACK) {
                            GetCell(row, column + 1).matched = true;
                            match = GetCell(row, column + 1).color;
                        }
                        if (row < Constants.ROWS - 1) {
                            if (GetCell(row + 1, column - 1).color != CellColor.BLACK) {
                                GetCell(row + 1, column - 1).matched = true;
                                match = GetCell(row + 1, column - 1).color;
                            }
                            if (GetCell(row + 1, column).color != CellColor.BLACK) {
                                GetCell(row + 1, column).matched = true;
                                match = GetCell(row + 1, column).color;
                            }
                            if (GetCell(row + 1, column + 1).color != CellColor.BLACK) {
                                GetCell(row + 1, column + 1).matched = true;
                                match = GetCell(row + 1, column + 1).color;
                            }
                        }
                        if (match != CellColor.BLACK) {
                            colors.Add(match);
                        }
                    }
                }
            }
            return colors;
        }

        private List<CellColor> MarkMatchesByRow() {
            List<CellColor> colors = new List<CellColor>();
            for (int row = 0; row < Constants.ROWS; ++row) {
                bool complete = true;
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetCell(row, column).color == CellColor.BLACK) {
                        complete = false;
                    }
                }
                if (complete) {
                    colors.Add(GetCell(row, 0).color);
                    for (int column = 0; column < Constants.COLUMNS; ++column) {
                        GetCell(row, column).matched = true;
                    }
                }
            }
            return colors;
        }

        private List<CellColor> MarkMatchesByColor() {
            List<CellColor> colors = new List<CellColor>();
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    CellColor color = GetColor(row, column);
                    if (color == CellColor.BLACK) {
                        continue;
                    }
                    bool hadUnlocked = !GetCell(row, column).locked;
                    if (row + 3 < Constants.ROWS) {
                        int otherRow = row + 1;
                        while (otherRow < Constants.ROWS && GetColor(otherRow, column) == color) {
                            if (!GetCell(otherRow, column).locked) {
                                hadUnlocked = true;
                            }
                            ++otherRow;
                        }
                        if (((otherRow - row) >= 4) && hadUnlocked) {
                            bool redundant = true;
                            while (--otherRow >= row) {
                                if (!GetCell(otherRow, column).matched) {
                                    GetCell(otherRow, column).matched = true;
                                    redundant = false;
                                }
                            }
                            if (!redundant) {
                                colors.Add(color);
                            }
                        }
                    }
                    {
                        int otherColumn = column + 1;
                        while (otherColumn < Constants.COLUMNS && GetColor(row, otherColumn) == color) {
                            if (!GetCell(row, otherColumn).locked) {
                                hadUnlocked = true;
                            }
                            ++otherColumn;
                        }
                        if (((otherColumn - column) >= 4) && hadUnlocked) {
                            bool redundant = true;
                            while (--otherColumn >= column) {
                                if (!GetCell(row, otherColumn).matched) {
                                    GetCell(row, otherColumn).matched = true;
                                    redundant = false;
                                }
                            }
                            if (!redundant) {
                                colors.Add(color);
                            }
                        }
                    }
                }
            }
            return colors;
        }

        public void ClearMatches() {
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetCell(row, column).matched) {
                        Cell.Direction direction = GetCell(row, column).direction;
                        if (direction.HasFlag(Cell.Direction.UP)) {
                            GetCell(row - 1, column).direction &= ~Cell.Direction.DOWN;
                        }
                        if (direction.HasFlag(Cell.Direction.DOWN)) {
                            GetCell(row + 1, column).direction &= ~Cell.Direction.UP;
                        }
                        if (direction.HasFlag(Cell.Direction.RIGHT)) {
                            GetCell(row, column + 1).direction &= ~Cell.Direction.LEFT;
                        }
                        if (direction.HasFlag(Cell.Direction.LEFT)) {
                            GetCell(row, column - 1).direction &= ~Cell.Direction.RIGHT;
                        }
                        GetCell(row, column).Clear();
                    }
                }
            }
            VerifyBoard();
        }

        /**
         * Returns true if the piece can be moved.
         */
        public bool CheckCellLoose(int row, int column, bool root) {
            if (row >= Constants.ROWS) {
                return false;
            }
            Cell cell = GetCell(row, column);
            if (cell.locked) {
                return false;
            }
            if (cell.color == CellColor.BLACK) {
                return true;
            }
            if (cell.loose) {
                return true;
            }
            if (cell.visited) {
                return true;
            }
            cell.visited = true;
            try {
                if (!CheckCellLoose(row + 1, column, false)) {
                    return false;
                }
                if (cell.direction.HasFlag(Cell.Direction.UP) && !CheckCellLoose(row - 1, column, false)) {
                    return false;
                }
                if (cell.direction.HasFlag(Cell.Direction.RIGHT) && !CheckCellLoose(row, column + 1, false)) {
                    return false;
                }
                if (cell.direction.HasFlag(Cell.Direction.LEFT) && !CheckCellLoose(row, column - 1, false)) {
                    return false;
                }
            } finally {
                cell.visited = false;
            }
            if (root) {
                cell.loose = true;
            }
            return true;
        }

        // Makes sure nothing is marked as matched, visited, or loose.
        public void VerifyBoard() {
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (entries[row, column].matched) {
                        throw new Exception("Invalid matched state.");
                    }
                    if (entries[row, column].visited) {
                        throw new Exception("Invalid visited state.");
                    }
                    if (entries[row, column].loose) {
                        throw new Exception("Invalid loose state.");
                    }
                }
            }
        }

        /**
         * Returns true if anything is free to fall.
         */
        public bool MarkLoose() {
            VerifyBoard();
            bool any = false;
            for (int row = Constants.ROWS - 2; row >= 0; --row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    CheckCellLoose(row, column, true);
                    if (GetCell(row, column).loose) {
                        any = true;
                    }
                }
            }
            return any;
        }

        public bool MoveLoose() {
            bool anythingFell = false;
            for (int row = Constants.ROWS - 2; row >= 0; --row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    Cell cell = GetCell(row, column);
                    if (cell.loose) {
                        anythingFell = true;
                        Cell below = GetCell(row + 1, column);
                        if (below.color != CellColor.BLACK) {
                            throw new Exception("Bad gravity logic.");
                        }
                        SetCell(row + 1, column, cell);
                        cell.Clear();
                    }
                }
            }
            VerifyBoard();
            return anythingFell;
        }

        public int GetLockedCount() {
            int count = 0;
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetCell(row, column).locked && !GetCell(row, column).matched) {
                        ++count;
                    }
                }
            }
            return count;
        }

        private Cell[,] entries;
        private int columnOffset;
        private int rowOffset;
    }
}
