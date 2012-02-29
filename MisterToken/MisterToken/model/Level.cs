using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Level {
        private String name;
        private int topFilledRow;
        private int numberFilled;
        private float probabilityTwoPiece;
        private float probabilityThreePiece;
        private float probabilityFourPiece;
        private CellColor[] colors;

        private Random random;

        public Level(XmlLevel xml) {
            random = new Random();
            name = xml.name;
            topFilledRow = xml.topFilledRow;
            numberFilled = xml.numberFilled;
            probabilityTwoPiece = xml.probabilityTwoPiece;
            probabilityThreePiece = xml.probabilityThreePiece;
            probabilityFourPiece = xml.probabilityFourPiece;
            colors = xml.colors;
        }

        public String GetName() {
            return name;
        }

        public int GetTopFilledRow() {
            return topFilledRow;
        }

        public int GetNumberFilled() {
            return numberFilled;
        }

        public int GetColorCount() {
            return colors.Length;
        }

        public CellColor GetColor(int i) {
            return colors[i];
        }

        public CellColor GetRandomColor() {
            return colors[random.Next(colors.Length)];
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
    }
}
