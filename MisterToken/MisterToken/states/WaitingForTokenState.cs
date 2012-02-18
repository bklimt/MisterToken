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
    public class WaitingForTokenState : PlayingGameState {
        public WaitingForTokenState(PlayingGameModel model) : base(model) {
        }

        public void SetMovingTokenState(MovingTokenState movingTokenState) {
            this.movingTokenState = movingTokenState;
        }

        public void setFailedGameState(FailedGameState failedGameState) {
            this.failedGameState = failedGameState;
        }

        public override void LoadExtraContent(ContentManager content, GraphicsDevice device) {
            chordSound = content.Load<SoundEffect>("chord");
        }

        public override void Start() {
            timeToNextToken = Constants.MILLIS_PER_TOKEN;
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            timeToNextToken -= gameTime.ElapsedGameTime.Milliseconds;
            model.nextTokenReadiness = 1.0f - ((float)timeToNextToken / Constants.MILLIS_PER_TOKEN);
            if (timeToNextToken <= 0) {
                model.tokenGenerator.LoadNextToken();
                model.nextTokenReadiness = 0.0f;
                Token token = model.tokenGenerator.GetCurrentToken();
                token.Move(0, Constants.TOKEN_START_COLUMN);
                if (!token.IsValid()) {
                    // Game over!
                    token.Commit();
                    chordSound.Play();
                    return failedGameState;
                } else {
                    return movingTokenState;
                }
            } else {
                return this;
            }
        }

        public override Color GetBackgroundColor() {
            return Color.CornflowerBlue;
        }

        SoundEffect chordSound;

        private MovingTokenState movingTokenState;
        private FailedGameState failedGameState;

        private int timeToNextToken;
    }
}
