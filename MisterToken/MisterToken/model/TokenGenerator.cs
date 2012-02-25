using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class TokenGenerator {
        public TokenGenerator(Board board, Level level) {
            this.board = board;
            this.level = level;
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
            nextToken = level.GetRandomToken(board);
        }

        public void Draw(Rectangle boardRect, SpriteBatch spriteBatch) {
            nextToken.DrawRect(boardRect, spriteBatch);
        }

        private Board board;
        private Level level;
        private Random random;
        private Token currentToken;
        private Token nextToken;
    }
}
