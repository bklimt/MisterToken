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

        public static float getRadius(Rectangle boardRect) {
            int minDimension = Math.Min(boardRect.Width, boardRect.Height);
            float radius = minDimension / 2;
            return radius;
        }

        public static float getCellHeight(Rectangle boardRect) {
            float circumference = (float)(2 * Math.PI * getRadius(boardRect));
            float cellHeight = circumference / Constants.COLUMNS;
            return cellHeight;
        }

        public static float getDepth(Rectangle boardRect) {
            return getCellHeight(boardRect) * Constants.ROWS;
        }

        public static void GetCircleCellPosition(Rectangle boardRect, int row, int column,
                                                 out Vector3 topLeft, out Vector3 topRight, out Vector3 bottomRight, out Vector3 bottomLeft) {
            float radius = getRadius(boardRect);
            float cellHeight = getCellHeight(boardRect);

            double theta1 = (Math.PI * 2 * column) / Constants.COLUMNS;
            double theta2 = (Math.PI * 2 * (column + 1)) / Constants.COLUMNS;

            theta1 -= (Math.PI / 2);
            theta2 -= (Math.PI / 2);

            topLeft.X = boardRect.Center.X + radius * (float)Math.Sin(theta1);
            topLeft.Y = boardRect.Center.Y - radius * (float)Math.Cos(theta1);
            topLeft.Z = cellHeight * row;
            topRight.X = boardRect.Center.X + radius * (float)Math.Sin(theta2);
            topRight.Y = boardRect.Center.Y - radius * (float)Math.Cos(theta2);
            topRight.Z = cellHeight * row;
            bottomRight.X = boardRect.Center.X + radius * (float)Math.Sin(theta2);
            bottomRight.Y = boardRect.Center.Y - radius * (float)Math.Cos(theta2);
            bottomRight.Z = cellHeight * (row + 1);
            bottomLeft.X = boardRect.Center.X + radius * (float)Math.Sin(theta1);
            bottomLeft.Y = boardRect.Center.Y - radius * (float)Math.Cos(theta1);
            bottomLeft.Z = cellHeight * (row + 1);
        }

        public void DrawCircle(Rectangle boardRect, QuadDrawer quadDrawer) {
            Vector3 topLeft, topRight, bottomRight, bottomLeft;
            for (int row = 0; row < Constants.ROWS; ++row) {
                for (int column = 0; column < Constants.COLUMNS; ++column) {
                    GetCircleCellPosition(boardRect, row, column, out topLeft, out topRight, out bottomRight, out bottomLeft);
                    GetCell(row, column).DrawQuad(topLeft, topRight, bottomRight, bottomLeft, quadDrawer);
                }
            }
        }

        public static Rectangle GetRectCellPosition(Rectangle boardRect, int row, int column) {
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
                    GetCell(row, column).DrawRect(GetRectCellPosition(boardRect, row, column), spriteBatch);
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

        private Cell[,] entries;
        private int columnOffset;
        private int rowOffset;
        private SpriteManager spriteManager;
    }
}
