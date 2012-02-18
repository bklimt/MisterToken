using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

//  |             ____________________
//  |            /                    \
//  |           |                      |
// \|/         \|/                     |
// WaitingToPlayState                  |
//    |                                |
//    | OnPressStart()                 |
//    |                                |
//   \|/                               |
// SetUpBoardState                     |
//    |             ________________   |
//    |            /                \  |
//    |           |                 |  |
//   \|/         \|/                |  |
// WaitingForTokenState             |  |
//      |                           |  |
//      | OnTokenReady()            |  |
//     / \___________               |  |
//    /              \              |  | OnPressStart()
//   |                |             |  |
//  \|/              \|/            |  |
// MovingTokenState GameFailedState |  |
//      |                    \_____ | _/\
//      | OnTokenCommit()           |    |
//     \|/                          |    |
//   ClearingState                  |    |
//  /|\  |        \                 |    |
//   |   |        |                 |    |
//   |  \|/      \|/                |    |
// FallingState  GameWonState       |    |
//       |          \______________ | ___/
//       \__________________________|
//         
namespace MisterToken {
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MisterTokenGame : Microsoft.Xna.Framework.Game {
        public MisterTokenGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1500;
            graphics.ApplyChanges();

            spriteManager = new SpriteManager();
            PlayingGameModel model = new PlayingGameModel(spriteManager);

            waitingToPlayState = new WaitingToPlayState();
            setUpBoardState = new SetUpBoardState(model);
            waitingForTokenState = new WaitingForTokenState(model);
            movingTokenState = new MovingTokenState(model);
            clearingState = new ClearingState(model);
            fallingState = new FallingState(model);
            failedGameState = new FailedGameState(model);

            waitingToPlayState.SetSetUpBoardState(setUpBoardState);
            setUpBoardState.SetWaitingForTokenState(waitingForTokenState);
            waitingForTokenState.SetMovingTokenState(movingTokenState);
            waitingForTokenState.setFailedGameState(failedGameState);
            movingTokenState.setClearingState(clearingState);
            clearingState.setFallingState(fallingState);
            fallingState.SetClearingState(clearingState);
            fallingState.SetWaitingForTokenState(waitingForTokenState);
            failedGameState.SetWaitingToPlayState(waitingToPlayState);

            state = waitingToPlayState;
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteManager.LoadContent(Content, GraphicsDevice);
            waitingToPlayState.LoadContent(Content, GraphicsDevice);
            setUpBoardState.LoadContent(Content, GraphicsDevice);
            waitingForTokenState.LoadContent(Content, GraphicsDevice);
            movingTokenState.LoadContent(Content, GraphicsDevice);
            clearingState.LoadContent(Content, GraphicsDevice);
            fallingState.LoadContent(Content, GraphicsDevice);
            failedGameState.LoadContent(Content, GraphicsDevice);
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Escape))
                this.Exit();
            GameState nextState = state.Update(gameTime);
            if (nextState != state) {
                nextState.Start();
                state = nextState;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            state.Draw(GraphicsDevice, gameTime);
            base.Draw(gameTime);
        }

        private GraphicsDeviceManager graphics;
        private SpriteManager spriteManager;

        private GameState state;
        private WaitingToPlayState waitingToPlayState;
        private SetUpBoardState setUpBoardState;
        private WaitingForTokenState waitingForTokenState;
        private MovingTokenState movingTokenState;
        private ClearingState clearingState;
        private FallingState fallingState;
        private FailedGameState failedGameState;
    }
}
