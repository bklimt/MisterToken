using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public class PlayingGameModel {
        public PlayingGameModel(SpriteManager spriteManager) {
            nextTokenReadiness = 0.0f;
            circleMode = true;
            board = new Board(spriteManager);
            tokenGenerator = new TokenGenerator(board, spriteManager);
        }

        public Board board;
        public TokenGenerator tokenGenerator;
        public float nextTokenReadiness;
        public bool circleMode;
    }
}
