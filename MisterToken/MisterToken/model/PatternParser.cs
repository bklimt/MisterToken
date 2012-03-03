using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class PatternParser {
        private int position;
        private List<string> tokens;
        private Dictionary<string, Value> constants;
        private Random random;

        private class Value {
            public int intValue = 0;
            public List<CellColor> patternValue = null;
            public enum Type {
                INT,
                PATTERN
            }
            public Type type;

            public Value(int value) {
                intValue = value;
                type = Type.INT;
            }

            public Value(CellColor color) {
                patternValue = new List<CellColor>();
                patternValue.Add(color);
                type = Type.PATTERN;
            }

            public Value(List<CellColor> pattern) {
                patternValue = pattern;
                type = Type.PATTERN;
            }

            public Value Add(Value other) {
                if (type == Type.INT && other.type == Type.INT) {
                    return new Value(intValue + other.intValue);
                } else if (type == Type.PATTERN && other.type == Type.PATTERN) {
                    List<CellColor> pattern = new List<CellColor>(patternValue.Count + other.patternValue.Count);
                    pattern.AddRange(patternValue);
                    pattern.AddRange(other.patternValue);
                    return new Value(pattern);
                } else {
                    throw new Exception("Invalid addition: " + type + " + " + other.type);
                }
            }

            public Value Subtract(Value other) {
                if (type == Type.INT && other.type == Type.INT) {
                    return new Value(intValue - other.intValue);
                } else {
                    throw new Exception("Invalid subtraction: " + type + " - " + other.type);
                }
            }

            public Value Multiply(Value other) {
                if (type == Type.INT && other.type == Type.INT) {
                    return new Value(intValue * other.intValue);
                } else if (type == Type.INT && other.type == Type.PATTERN) {
                    List<CellColor> pattern = new List<CellColor>(intValue * other.patternValue.Count);
                    for (int i = 0; i < intValue; ++i) {
                        pattern.AddRange(other.patternValue);
                    }
                    return new Value(pattern);
                } else if (type == Type.PATTERN && other.type == Type.INT) {
                    List<CellColor> pattern = new List<CellColor>(other.intValue * patternValue.Count);
                    for (int i = 0; i < other.intValue; ++i) {
                        pattern.AddRange(patternValue);
                    }
                    return new Value(pattern);
                } else {
                    throw new Exception("Invalid multiplication: " + type + " * " + other.type);
                }
            }

            public Value Divide(Value other) {
                if (type == Type.INT && other.type == Type.INT) {
                    return new Value(intValue / other.intValue);
                } else if (type == Type.PATTERN && other.type == Type.INT) {
                    List<CellColor> pattern = new List<CellColor>(patternValue.Count / other.intValue);
                    for (int i = 0; i < (patternValue.Count / other.intValue); ++i) {
                        pattern.Add(patternValue[i]);
                    }
                    return new Value(pattern);
                } else {
                    throw new Exception("Invalid division: " + type + " / " + other.type);
                }
            }

            public Value Shuffle(Random random) {
                if (type != Type.PATTERN) {
                    throw new Exception("Tried to shuffle an integer.");
                }
                List<CellColor> pattern = new List<CellColor>(patternValue.Count);
                pattern.AddRange(patternValue);
                for (int i = 0; i < pattern.Count - 1; ++i) {
                    int other = random.Next(i, pattern.Count);
                    CellColor temp = pattern[i];
                    pattern[i] = pattern[other];
                    pattern[other] = temp;
                }
                return new Value(pattern);
            }
        }

        private PatternParser(string text, Random random) {
            position = 0;
            this.random = random;
            Tokenize(text);

            constants = new Dictionary<string, Value>();
            constants.Add("rows", new Value(Constants.ROWS));
            constants.Add("columns", new Value(Constants.COLUMNS));
            foreach (CellColor color in Enum.GetValues(typeof(CellColor))) {
                constants.Add(Enum.GetName(typeof(CellColor), color).ToLower(), new Value(color));
            }
        }

        private void Tokenize(string text) {
            tokens = new List<string>();
            text = text.ToLower();
            int position = 0;
            while (position < text.Length) {
                if (text[position] >= '0' && text[position] <= '9') {
                    string s = "";
                    while (position < text.Length && Char.IsDigit(text[position])) {
                        s += text[position++];
                    }
                    tokens.Add(s);
                } else if (Char.IsLetter(text[position])) {
                    string s = "";
                    while (position < text.Length && Char.IsLetter(text[position])) {
                        s += text[position++];
                    }
                    tokens.Add(s);
                } else if (Char.IsWhiteSpace(text[position])) {
                    position++;
                } else {
                    tokens.Add("" + text[position++]);
                }
            }
        }

        private string Peek() {
            return tokens[position];
        }

        private string Next() {
            return tokens[position++];
        }

        private bool End() {
            return position >= tokens.Count;
        }

        private string MatchToken(params string[] tokenList) {
            foreach (string token in tokenList) {
                if (token.Equals(Peek())) {
                    return Next();
                }
            }
            return null;
        }

        private int? MatchInt() {
            foreach (char c in Peek()) {
                if (!Char.IsDigit(c)) {
                    return null;
                }
            }
            return int.Parse(Next());
        }

        private Value ParseValue() {
            if (constants.ContainsKey(Peek())) {
                return constants[Next()];
            }
            bool negative = (MatchToken("-") != null);
            int? value = MatchInt();
            if (value == null) {
                throw new Exception("Unable to parse integer from " + Peek());
            }
            if (negative) {
                value *= -1;
            }
            return new Value(value.Value);
        }

        private Value ParseAtom() {
            if (MatchToken("shuffle") != null) {
                return ParseAtom().Shuffle(random);
            } else if (MatchToken("(") != null) {
                Value value = ParseSum();
                if (MatchToken(")") == null) {
                    throw new Exception("Missing ).");
                }
                return value;
            } else {
                return ParseValue();
            }
        }

        private Value ParseProduct() {
            Value value = ParseAtom();
            while (!End()) {
                string op = MatchToken("*", "/");
                if (op == "*") {
                    value = value.Multiply(ParseAtom());
                } else if (op == "/") {
                    value = value.Divide(ParseAtom());
                } else {
                    return value;
                }
            }
            return value;
        }

        private Value ParseSum() {
            Value value = ParseProduct();
            while (!End()) {
                string op = MatchToken("+", "-");
                if (op == "+") {
                    value = value.Add(ParseProduct());
                } else if (op == "-") {
                    value = value.Subtract(ParseProduct());
                } else {
                    return value;
                }
            }
            return value;
        }

        public static List<CellColor> ParseExpression(string text, Random random) {
            PatternParser parser = new PatternParser(text, random);
            Value value = parser.ParseSum();
            if (!parser.End()) {
                throw new Exception("Stray characters.");
            }
            if (value.type != Value.Type.PATTERN) {
                throw new Exception("Expected a pattern, but found an integer.");
            }
            return value.patternValue;
        }
    }
}
