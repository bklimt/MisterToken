using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Level {
        private int level;

        public int topFilledRow;
        public int numberFilled;
        private float probabilityTwoPiece;
        private float probabilityThreePieceElbow;
        private List<Cell.Color> colors;

        private Random random;

        public Level(int i) {
            setFromNumber(i);
        }

        public void setFromNumber(int number) {
            level = number;
            random = new Random();
            colors = new List<Cell.Color>();
            switch (number % 4) {
                case 0:
                    topFilledRow = 7;
                    numberFilled = 4;
                    probabilityTwoPiece = 0.95f;
                    probabilityThreePieceElbow = 0.05f;
                    colors.Add(Cell.Color.WHITE);
                    colors.Add(Cell.Color.GREEN);
                    colors.Add(Cell.Color.RED);
                    break;
                case 1:
                    topFilledRow = 5;
                    numberFilled = 12;
                    probabilityTwoPiece = 0.90f;
                    probabilityThreePieceElbow = 0.10f;
                    colors.Add(Cell.Color.RED);
                    colors.Add(Cell.Color.YELLOW);
                    colors.Add(Cell.Color.BLUE);
                    break;
                case 2:
                    topFilledRow = 5;
                    numberFilled = 20;
                    probabilityTwoPiece = 0.85f;
                    probabilityThreePieceElbow = 0.15f;
                    colors.Add(Cell.Color.BLUE);
                    colors.Add(Cell.Color.ORANGE);
                    colors.Add(Cell.Color.WHITE);
                    break;
                case 3:
                    topFilledRow = 3;
                    numberFilled = 30;
                    probabilityTwoPiece = 0.80f;
                    probabilityThreePieceElbow = 0.20f;
                    colors.Add(Cell.Color.PURPLE);
                    colors.Add(Cell.Color.YELLOW);
                    colors.Add(Cell.Color.GREEN);
                    colors.Add(Cell.Color.WHITE);
                    break;
            }
        }

        public int GetColorCount() {
            return colors.Count;
        }

        public Cell.Color GetColor(int i) {
            return colors[i];
        }

        public Cell.Color GetRandomColor() {
            return colors[random.Next(colors.Count)];
        }

        public Token GetRandomToken(Board board) {
            Cell.Color color1 = GetRandomColor();
            Cell.Color color2 = GetRandomColor();
            Cell.Color color3 = GetRandomColor();
            float fraction = (float)random.NextDouble() * (probabilityTwoPiece + probabilityThreePieceElbow);
            if (fraction < probabilityTwoPiece) {
                return new TwoPieceToken(board, 0, 0, color1, color2);
            } else {
                return new ThreePieceElbowToken(board, 0, 0, color1, color2, color3);
            }
        }

        public void Next() {
            setFromNumber(level + 1);
        }
    }
}
