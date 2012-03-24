using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Level {
        public Level(XmlLevel xml) {
            name = xml.name;
            tokens = new TokenDistribution(xml.tokens);
            colors = new ColorDistribution(xml.colors);
            pattern = xml.pattern;
            wrap = xml.wrap;

            // Strip the whitespace from the help text.
            help = "";
            string[] lines = xml.help.Split('\n');
            foreach (string line in lines) {
                if (help.Length > 0 || line.Trim().Length > 0) {
                    help += line.Trim();
                    help += '\n';
                }
            }
        }

        public string GetName() {
            return name;
        }

        public bool IsCompleted() {
            return Global.Levels.IsCompleted(GetName());
        }

        public string GetHelp() {
            return help;
        }

        public void SetupBoard(Board board, Random random) {
            List<Cell> cells = PatternParser.ParseExpression(pattern, random);
            int start = Constants.ROWS * Constants.COLUMNS - cells.Count;
            if (start < 0) {
                start = 0;
            }
            int offset = 0;
            for (int row = 0; row < Constants.ROWS; row++) {
                for (int column = 0; column < Constants.COLUMNS; column++) {
                    board.GetCell(row, column).Clear();
                    if (offset >= start) {
                        CellColor color = cells[offset - start].color;
                        if (color != CellColor.BLACK) {
                            board.GetCell(row, column).color = color;
                            board.GetCell(row, column).locked = cells[offset - start].locked;
                        }
                    }
                    ++offset;
                }
            }
        }

        public CellColor GetRandomColor(Random random) {
            return colors.GetRandomColor(random);
        }

        public Token GetRandomToken(Board board, Random random) {
            CellColor color1 = GetRandomColor(random);
            CellColor color2 = GetRandomColor(random);
            return tokens.GetRandomToken(board, color1, color2, random);
        }

        public bool Wrap() {
            return wrap;
        }

        private string name;
        private string help;
        private TokenDistribution tokens;
        private ColorDistribution colors;
        private string pattern;
        private bool wrap;
    }
}
