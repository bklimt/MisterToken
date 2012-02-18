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
    public class ClearingState : PlayingGameState {
        public ClearingState(PlayingGameModel model) : base(model) {
        }

        public void setFallingState(FallingState fallingState) {
            this.fallingState = fallingState;
        }

        public override void LoadExtraContent(ContentManager content, GraphicsDevice device) {
            chordSound = content.Load<SoundEffect>("chord");
        }

        public override void Start() {
            timeToClear = 0;
        }

        public override GameState PlayingUpdate(GameTime gameTime) {
            if (timeToClear > 0) {
                timeToClear -= gameTime.ElapsedGameTime.Milliseconds;
            }
            if (timeToClear <= 0) {
                model.board.ClearMatches();
                if (model.board.MarkMatches()) {
                    timeToClear = Constants.MILLIS_PER_CLEAR;
                    chordSound.Play();
                    return this;
                } else {
                    return fallingState;
                }
            } else {
                return this;
            }
        }

        public override Color GetBackgroundColor() {
            return Color.CornflowerBlue;
        }

        SoundEffect chordSound;

        private FallingState fallingState;

        private int timeToClear;
    }
}
