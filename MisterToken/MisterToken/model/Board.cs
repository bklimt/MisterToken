using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class Board {
        public Board(SpriteManager spriteManager) {
            this.spriteManager = spriteManager;

            rowOffset = 0;
            columnOffset = 0;

            entries = new Cell[Constants.ROWS, Constants.COLUMNS];
            for (int i = 0; i < Constants.ROWS; i++) {
                for (int j = 0; j < Constants.COLUMNS; j++) {
                    entries[i, j] = new Cell(spriteManager);
                }
            }
        }

        public void Randomize(int topRow) {
            Random random = new Random();
            rowOffset = 0;
            columnOffset = 0;
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (row < topRow) {
                        entries[row, column].Clear();
                    } else {
                        entries[row, column].Clear();
                        if (random.Next(2) == 1) {
                            entries[row, column].color = Cell.GetRandomColor(random);
                            entries[row, column].locked = true;
                        }
                    }
                }
            }
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

        public Cell.Color GetColor(int row, int column) {
            while (row < 0)
                row += Constants.ROWS;
            while (column < 0)
                column += Constants.COLUMNS;
            return entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].color;
        }

        public void SetCell(int row, int column, Cell cell) {
            while (row < 0)
                row += Constants.ROWS;
            while (column < 0)
                column += Constants.COLUMNS;
            entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].color = cell.color;
            entries[(row + rowOffset) % Constants.ROWS, (column + columnOffset) % Constants.COLUMNS].direction = cell.direction;
        }

        public void AddNewRow() {
            Random random = new Random();
            rowOffset = (rowOffset + 1) % Constants.ROWS;
            int bottomRow = ((Constants.ROWS - 1) + rowOffset) % Constants.ROWS;
            for (int i = 0; i < Constants.COLUMNS; ++i) {
                entries[bottomRow, i].color = Cell.GetRandomColor(random);
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
            int cellWidth = boardRect.Width / Constants.COLUMNS;
            int cellHeight = boardRect.Height / Constants.ROWS;
            tileRect.X = boardRect.Left + column * cellWidth;
            tileRect.Y = boardRect.Top + row * cellHeight;
            tileRect.Width = cellWidth;
            tileRect.Height = cellHeight;
            return tileRect;
        }

        public void DrawRect(Rectangle boardRect, SpriteBatch spriteBatch) {
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    GetCell(row, column).DrawRect(GetCellPosition(boardRect, row, column), spriteBatch);
                }
            }
        }

        public bool MarkMatches() {
            bool any = false;
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    entries[row, column].matched = false;
                }
            }
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    Cell.Color color = GetColor(row, column);
                    if (color == Cell.Color.BLACK) {
                        continue;
                    }
                    if (row + 3 < Constants.ROWS) {
                        bool match = true;
                        for (int otherRow = row + 1; otherRow < row + 4; ++otherRow) {
                            if (GetColor(otherRow, column) != color) {
                                match = false;
                            }
                        }
                        if (match) {
                            any = true;
                            for (int otherRow = row; otherRow < row + 4; ++otherRow) {
                                GetCell(otherRow, column).matched = true;
                            }
                        }
                    }
                    {
                        bool match = true;
                        for (int otherColumn = column + 1; otherColumn < column + 4; ++otherColumn) {
                            if (GetColor(row, otherColumn) != color) {
                                match = false;
                            }
                        }
                        if (match) {
                            any = true;
                            for (int otherColumn = column; otherColumn < column + 4; ++otherColumn) {
                                GetCell(row, otherColumn).matched = true;
                            }
                        }
                    }
                }
            }
            return any;
        }

        public void ClearMatches() {
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetCell(row, column).matched) {
                        switch (GetCell(row, column).direction) {
                            case Cell.Direction.UP: GetCell(row - 1, column).direction = Cell.Direction.NONE; break;
                            case Cell.Direction.DOWN: GetCell(row + 1, column).direction = Cell.Direction.NONE; break;
                            case Cell.Direction.RIGHT: GetCell(row, column + 1).direction = Cell.Direction.NONE; break;
                            case Cell.Direction.LEFT: GetCell(row, column - 1).direction = Cell.Direction.NONE; break;
                        }
                        GetCell(row, column).Clear();
                    }
                }
            }
        }

        public bool ApplyGravity() {
            bool any = false;
            for (int row = Constants.ROWS - 2; row >= 0; --row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    if (GetColor(row, column) != Cell.Color.BLACK &&
                        !GetCell(row, column).locked) {
                        // Can this piece fall?
                        if (GetColor(row + 1, column) == Cell.Color.BLACK) {
                            if (GetCell(row, column).direction == Cell.Direction.RIGHT) {
                                if (GetColor(row + 1, column + 1) == Cell.Color.BLACK) {
                                    // We can move both.
                                    SetCell(row + 1, column, GetCell(row, column));
                                    SetCell(row + 1, column + 1, GetCell(row, column + 1));
                                    GetCell(row, column).Clear();
                                    GetCell(row, column + 1).Clear();
                                    any = true;
                                }
                            } else if (GetCell(row, column).direction != Cell.Direction.LEFT) {
                                // We can move this one.
                                SetCell(row + 1, column, GetCell(row, column));
                                GetCell(row, column).Clear();
                                any = true;
                            }
                        }
                    }
                }
            }
            return any;
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
        private SpriteManager spriteManager;
    }
}
