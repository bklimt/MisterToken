using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Level {
        private String name;
        private float probabilityTwoPiece;
        private float probabilityThreePiece;
        private float probabilityFourPiece;
        private List<CellColor> colors;
        private string pattern;
        private bool wrap;

        private Random random;

        public Level(XmlLevel xml) {
            random = new Random();
            name = xml.name;
            probabilityTwoPiece = xml.probabilityTwoPiece;
            probabilityThreePiece = xml.probabilityThreePiece;
            probabilityFourPiece = xml.probabilityFourPiece;
            pattern = xml.pattern;
            colors = PatternParser.GetColors(pattern);
            wrap = xml.wrap;
        }

        public String GetName() {
            return name;
        }

        public void SetupBoard(Board board) {
            List<CellColor> cells = PatternParser.ParseExpression(pattern, random);
            int start = Constants.ROWS * Constants.COLUMNS - cells.Count;
            if (start < 0) {
                start = 0;
            }
            int offset = 0;
            for (int row = 0; row < Constants.ROWS; row++) {
                for (int column = 0; column < Constants.COLUMNS; column++) {
                    board.GetCell(row, column).Clear();
                    if (offset >= start) {
                        CellColor color = cells[offset - start];
                        if (color != CellColor.BLACK) {
                            board.GetCell(row, column).color = color;
                            board.GetCell(row, column).locked = true;
                        }
                    }
                    ++offset;
                }
            }
        }

        public int GetColorCount() {
            return colors.Count;
        }

        public CellColor GetColor(int i) {
            return colors[i];
        }

        public CellColor GetRandomColor() {
            return colors[random.Next(colors.Count)];
        }

        public Token GetRandomToken(Board board) {
            CellColor color1 = GetRandomColor();
            CellColor color2 = GetRandomColor();
            CellColor color3 = GetRandomColor();
            CellColor color4 = GetRandomColor();
            float fraction = (float)random.NextDouble() * (probabilityTwoPiece + probabilityThreePiece + probabilityFourPiece);
            if (fraction < probabilityTwoPiece) {
                return new TwoPieceToken(board, 0, 0, color1, color2);
            } else if (fraction < (probabilityTwoPiece + probabilityThreePiece)) {
                return new ThreePieceElbowToken(board, 0, 0, color1, color2, color3);
            } else {
                return new FourPieceToken(board, 0, 0, color1, color2, color3, color4);
            }
        }

        public bool Wrap() {
            return wrap;
        }
    }
}
