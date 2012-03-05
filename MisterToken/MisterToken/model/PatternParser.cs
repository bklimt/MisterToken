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
            public List<Cell> patternValue = null;
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
                patternValue = new List<Cell>();
                Cell cell = new Cell();
                cell.color = color;
                cell.locked = true;
                patternValue.Add(cell);
                type = Type.PATTERN;
            }

            public Value(List<Cell> pattern) {
                type = Type.PATTERN;
                patternValue = new List<Cell>();
                for (int i = 0; i < pattern.Count; ++i) {
                    patternValue.Add(new Cell(pattern[i]));
                }
            }

            public Value Add(Value other) {
                if (type == Type.INT && other.type == Type.INT) {
                    return new Value(intValue + other.intValue);
                } else if (type == Type.PATTERN && other.type == Type.PATTERN) {
                    List<Cell> pattern = new List<Cell>(patternValue.Count + other.patternValue.Count);
                    for (int i = 0; i < patternValue.Count; ++i) {
                        Cell cell = new Cell();
                        cell.color = patternValue[i].color;
                        cell.locked = patternValue[i].locked;
                        pattern.Add(cell);
                    }
                    for (int i = 0; i < other.patternValue.Count; ++i) {
                        Cell cell = new Cell();
                        cell.color = other.patternValue[i].color;
                        cell.locked = other.patternValue[i].locked;
                        pattern.Add(cell);
                    }
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
                    List<Cell> pattern = new List<Cell>(intValue * other.patternValue.Count);
                    for (int i = 0; i < intValue; ++i) {
                        for (int j = 0; j < other.patternValue.Count; ++j) {
                            pattern.Add(new Cell(other.patternValue[j]));
                        }
                    }
                    return new Value(pattern);
                } else if (type == Type.PATTERN && other.type == Type.INT) {
                    List<Cell> pattern = new List<Cell>(other.intValue * patternValue.Count);
                    for (int i = 0; i < other.intValue; ++i) {
                        for (int j = 0; j < patternValue.Count; ++j) {
                            pattern.Add(new Cell(patternValue[j]));
                        }
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
                    List<Cell> pattern = new List<Cell>(patternValue.Count / other.intValue);
                    for (int i = 0; i < (patternValue.Count / other.intValue); ++i) {
                        pattern.Add(new Cell(patternValue[i]));
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
                List<Cell> pattern = new List<Cell>(patternValue.Count);
                for (int j = 0; j < patternValue.Count; ++j) {
                    pattern.Add(patternValue[j]);
                }
                for (int i = 0; i < pattern.Count - 1; ++i) {
                    int other = random.Next(i, pattern.Count);
                    Cell temp = pattern[i];
                    pattern[i] = new Cell(pattern[other]);
                    pattern[other] = new Cell(temp);
                }
                return new Value(pattern);
            }

            public Value SetLocked(bool locked) {
                if (type != Type.PATTERN) {
                    throw new Exception("Tried to unlock an int.");
                }
                Value other = new Value(patternValue);
                for (int i = 0; i < other.patternValue.Count; ++i) {
                    other.patternValue[i].locked = locked;
                }
                return other;
            }

            public static Value FillRows(Value rows, Value pattern, CellColor color) {
                if (rows.type != Type.INT) {
                    throw new Exception("fill_rows expects a first argument of type int.");
                }
                if (pattern.type != Type.PATTERN) {
                    throw new Exception("fill_rows expects a second argument of type pattern.");
                }
                int total = rows.intValue * Constants.COLUMNS;
                int additional = total - pattern.patternValue.Count;
                if (additional < 0) {
                    return pattern;
                }
                return ((new Value(color)).Multiply(new Value(additional))).Add(pattern);
            }

            public static Value BlankRows(Value rows) {
                if (rows.type != Type.INT) {
                    throw new Exception("blank_rows expects an argument of type int.");
                }
                return (new Value(CellColor.BLACK)).Multiply(new Value(rows.intValue * Constants.COLUMNS));
            }
        }

        private PatternParser(string text, Random random) {
            position = 0;
            this.random = random;
            Tokenize(text);

            constants = new Dictionary<string, Value>();
            constants.Add("rows", new Value(Constants.ROWS));
            constants.Add("columns", new Value(Constants.COLUMNS));
            constants.Add("_", new Value(CellColor.BLACK));
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
                    while (position < text.Length && (Char.IsLetter(text[position]) || text[position] == '_')) {
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
            if (Peek() == "!") {
                Next();
            }
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
            if (MatchToken("$") != null) {
                return ParseAtom().SetLocked(false);
            } else if (MatchToken("shuffle") != null) {
                return ParseAtom().Shuffle(random);
            } else if (MatchToken("blank_rows") != null) {
                return Value.BlankRows(ParseAtom());
            } else if (MatchToken("fill_rows") != null) {
                if (MatchToken("(") == null) {
                    throw new Exception("Expected (.");
                }
                Value rows = ParseSum();
                if (MatchToken(",") == null) {
                    throw new Exception("Expected ,.");
                }
                Value pattern = ParseSum();
                CellColor color = CellColor.BLACK;
                if (MatchToken(",") != null) {
                    Value colorValue = ParseValue();
                    if (colorValue.type != Value.Type.PATTERN || colorValue.patternValue.Count != 1) {
                        throw new Exception("Expected a color.");
                    }
                    color = colorValue.patternValue[0].color;
                }
                if (MatchToken(")") == null) {
                    throw new Exception("Expected ).");
                }
                return Value.FillRows(rows, pattern, color);
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

        public static List<CellColor> GetColors(string text) {
            List<CellColor> result = new List<CellColor>();
            PatternParser parser = new PatternParser(text, null);
            for (int i = 0; i < parser.tokens.Count; ++i) {
                if (i > 0 && parser.tokens[i-1] == "!") {
                    continue;
                }
                if (parser.constants.ContainsKey(parser.tokens[i])) {
                    Value value = parser.constants[parser.tokens[i]];
                    if (value.type == Value.Type.PATTERN) {
                        for (int j = 0; j < value.patternValue.Count; ++j) {
                            if (!result.Contains(value.patternValue[j].color) && value.patternValue[j].color != CellColor.BLACK) {
                                result.Add(value.patternValue[j].color);
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static List<Cell> ParseExpression(string text, Random random) {
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
