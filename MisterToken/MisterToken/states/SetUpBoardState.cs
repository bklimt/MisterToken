using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class SetUpBoardState : PlayingGameState {
        public SetUpBoardState(PlayingGameModel model) : base(model) {
            waitingForTokenState = null;
        }

        public void SetWaitingForTokenState(WaitingForTokenState waitingForTokenState) {
            this.waitingForTokenState = waitingForTokenState;
        }

        public override void Start() {
            model.board.Randomize(Constants.TOP_FILLED_ROW);
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            return waitingForTokenState;
        }

        public override Color GetBackgroundColor() {
            return Color.CornflowerBlue;
        }

        private WaitingForTokenState waitingForTokenState;
    }
}
