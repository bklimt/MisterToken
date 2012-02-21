using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class TokenGenerator {
        public TokenGenerator(Board board) {
            this.board = board;
            this.random = new Random();
            this.nextToken = null;
            LoadNextToken();
        }

        public Token GetCurrentToken() {
            return currentToken;
        }

        public void ClearCurrentToken() {
            currentToken = null;
        }

        public void LoadNextToken() {
            currentToken = nextToken;
            Cell.Color color1 = Cell.GetRandomColor(random);
            Cell.Color color2 = Cell.GetRandomColor(random);
            Cell.Color color3 = Cell.GetRandomColor(random);
            float fraction = (float)random.NextDouble() * (Constants.PROBABILITY_TWO_PIECE + Constants.PROBABILITY_THREE_PIECE_ELBOW);
            if (fraction < Constants.PROBABILITY_TWO_PIECE) {
                nextToken = new TwoPieceToken(board, 0, 0, color1, color2);
            } else {
                nextToken = new ThreePieceElbowToken(board, 0, 0, color1, color2, color3);
            }
        }

        public void Draw(Rectangle boardRect, SpriteBatch spriteBatch) {
            nextToken.DrawRect(boardRect, spriteBatch);
        }

        private Board board;
        private Random random;
        private Token currentToken;
        private Token nextToken;
    }
}
