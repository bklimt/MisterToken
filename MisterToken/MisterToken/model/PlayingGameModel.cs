using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace MisterToken {
    public class PlayingGameModel {
        public PlayingGameModel(SpriteManager spriteManager) {
            nextTokenReadiness = 0.0f;
            board = new Board(spriteManager);

            tokenGenerator = new TokenGenerator(board, spriteManager);
        }

        public void LoadContent(GraphicsDevice device) {
            spriteBatch = new SpriteBatch(device);
        }

        public Board board;
        public TokenGenerator tokenGenerator;
        public float nextTokenReadiness;

        public SpriteBatch spriteBatch;
    }
}
