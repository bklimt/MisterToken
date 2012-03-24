using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class TokenDistribution {
        public TokenDistribution(string description) {
            sums = new List<Node>();

            List<String> tokens = Tokenize(description);
            for (int i = 0; i < tokens.Count; ++i) {
                double weight = Double.Parse(tokens[i++]);
                if (i >= tokens.Count) {
                    throw new Exception("Invalid token distribution: \"" + description + "\"");
                }
                string piece = tokens[i];
                Add(piece, weight);
            }
        }

        public Token GetRandomToken(Board board, CellColor color1, CellColor color2, Random random) {
            if (sums.Count == 0) {
                throw new Exception("Tried to get a random token from the empty set.");
            }
            double number = random.NextDouble() * sums[sums.Count - 1].sum;
            for (int i = 0; i < sums.Count - 1; ++i) {
                if (number < sums[i].sum) {
                    return sums[i].factory(board, color1, color2);
                }
            }
            return sums[sums.Count - 1].factory(board, color1, color2);
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

        private void Add(TokenFactory factory, double weight) {
            Node node = new Node();
            if (sums.Count == 0) {
                node.sum = weight;
            } else {
                node.sum = sums[sums.Count - 1].sum + weight;
            }
            node.factory = factory;
            sums.Add(node);
        }

        private void Add(string tokenName, double weight) {
            if (tokenName.Equals("bomb")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new BombToken(board, 0, 0);
                }, weight);
                return;
            }
            if (tokenName.Equals("skull")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new SkullToken(board, 0, 0);
                }, weight);
                return;
            }
            if (tokenName.Equals("2")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new TwoPieceToken(board, 0, 0, color1, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("3elbow")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new ThreePieceElbowToken(board, 0, 0, color1, color1, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4square")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceSquareToken(board, 0, 0, color1, color1, color2, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4t")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceTToken(board, 0, 0, color1, color2, color2, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4s")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceSToken(board, 0, 0, color1, color1, color2, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4z")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceZToken(board, 0, 0, color1, color1, color2, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4l")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceLToken(board, 0, 0, color1, color2, color2, color1);
                }, weight);
                return;
            }
            if (tokenName.Equals("4j")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceJToken(board, 0, 0, color1, color1, color2, color2);
                }, weight);
                return;
            }
            if (tokenName.Equals("4i")) {
                Add(delegate(Board board, CellColor color1, CellColor color2) {
                    return new FourPieceIToken(board, 0, 0, color1, color1, color2, color2);
                }, weight);
                return;
            }
            throw new Exception("Unknown token type: \"" + tokenName + "\"");
        }

        private delegate Token TokenFactory(Board board, CellColor color1, CellColor color2);

        private struct Node {
            public double sum;
            public TokenFactory factory;
        }

        private List<Node> sums;
    }
}
