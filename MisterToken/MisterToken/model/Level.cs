using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class Level {
        public Level(XmlLevel xml) {
            seed = new Random().Next();
            random = new Random(seed);
            next = null;
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

        public Level(Level other) {
            random = new Random(other.seed);
            next = other.next;
            name = other.name;
            tokens = new TokenDistribution(other.tokens);
            colors = new ColorDistribution(other.colors);
            pattern = other.pattern;
            wrap = other.wrap;
            help = other.help;
        }

        public string GetName() {
            return name;
        }

        public Level GetNext() {
            return next;
        }

        public void SetNext(Level next) {
            this.next = next;
        }

        public bool IsCompleted() {
            return Levels.IsCompleted(GetName());
        }

        public string GetHelp() {
            return help;
        }

        public void SetupBoard(Board board) {
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

        public CellColor GetRandomColor() {
            return colors.GetRandomColor(random);
        }

        public Token GetRandomToken(Board board) {
            CellColor color1 = GetRandomColor();
            CellColor color2 = GetRandomColor();
            return tokens.GetRandomToken(board, color1, color2, random);
        }

        public bool Wrap() {
            return wrap;
        }

        private string name;
        private string help;
        private Level next;
        private TokenDistribution tokens;
        private ColorDistribution colors;
        private string pattern;
        private bool wrap;

        private Random random;
        private int seed;
    }
}
