using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class ColorDistribution {
        public ColorDistribution(string description) {
            sums = new List<Node>();

            List<String> tokens = Tokenize(description);
            for (int i = 0; i < tokens.Count; ++i) {
                double weight = Double.Parse(tokens[i++]);
                if (i >= tokens.Count) {
                    throw new Exception("Invalid token distribution: \"" + description + "\"");
                }
                string color = tokens[i];
                Add(color, weight);
            }
        }

        public CellColor GetRandomColor(Random random) {
            if (sums.Count == 0) {
                throw new Exception("Tried to get a random color from the empty set.");
            }
            double number = random.NextDouble() * sums[sums.Count - 1].sum;
            for (int i = 0; i < sums.Count - 1; ++i) {
                if (number < sums[i].sum) {
                    return sums[i].color;
                }
            }
            return sums[sums.Count - 1].color;
        }

        private List<string> Tokenize(string text) {
            List<String> tokens = new List<string>();
            text = text.ToLower();
            int position = 0;
            while (position < text.Length) {
                if (Char.IsLetterOrDigit(text[position]) || text[position] == '.') {
                    string s = "";
                    while (position < text.Length && (Char.IsLetterOrDigit(text[position]) || text[position] == '.')) {
                        s += text[position++];
                    }
                    tokens.Add(s);
                } else if (Char.IsWhiteSpace(text[position])) {
                    position++;
                } else {
                    throw new Exception("Invalid character in token distribution: \"" + text[position] + "\"");
                }
            }
            return tokens;
        }

        private void Add(CellColor color, double weight) {
            Node node = new Node();
            if (sums.Count == 0) {
                node.sum = weight;
            } else {
                node.sum = sums[sums.Count - 1].sum + weight;
            }
            node.color = color;
            sums.Add(node);
        }

        private void Add(string tokenName, double weight) {
            Add((CellColor)Enum.Parse(typeof(CellColor), tokenName, true), weight);
        }

        private struct Node {
            public double sum;
            public CellColor color;
        }

        private List<Node> sums;
    }
}
