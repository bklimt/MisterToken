using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public class WaitingToPlayState : GameState {
        public WaitingToPlayState() {
            setUpBoardState = null;
            keyDown = false;
        }

        public void Start() {
        }

        public void SetSetUpBoardState(SetUpBoardState setUpBoardState) {
            this.setUpBoardState = setUpBoardState;
        }

        public void LoadContent(ContentManager content, GraphicsDevice device) {
        }

        public GameState Update(GameTime gameTime) {
            if (keyDown && !Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter)) {
                keyDown = false;
                return setUpBoardState;
            } else {
                if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Enter)) {
                    keyDown = true;
                }
            }
            return this;
        }

        public void Draw(GraphicsDevice device, GameTime gameTime) {
            device.Clear(Color.Red);
        }

        private bool keyDown;
        private SetUpBoardState setUpBoardState;
    }
}
