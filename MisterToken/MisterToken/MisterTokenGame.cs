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

namespace MisterToken {
    public class MisterTokenGame : Microsoft.Xna.Framework.Game, SinglePlayerListener {
        public MisterTokenGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            spriteManager = new SpriteManager();
            model = new SinglePlayer(spriteManager, this);
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteManager.LoadContent(Content, GraphicsDevice);
            chordSound = Content.Load<SoundEffect>("chord");
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            Input.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            switch (state) {
                case State.WAITING_TO_PLAY:
                    if (Input.IsDown(BooleanInputHook.PLAYER_ONE_START)) {
                        model.Start();
                        state = State.PLAYING;
                    }
                    break;
                case State.PLAYING:
                    model.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            switch (state) {
                case State.WAITING_TO_PLAY:
                    GraphicsDevice.Clear(Color.Red);
                    break;
                case State.PLAYING:
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    model.Draw(GraphicsDevice, spriteBatch);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }

        public void OnClear() {
            chordSound.Play();
        }

        public void OnWon() {
        }

        public void OnFailed() {
            chordSound.Play();
        }

        public void OnFinished() {
            state = State.WAITING_TO_PLAY;
        }

        // Game state.
        private enum State {
            WAITING_TO_PLAY,
            PLAYING,
        }
        private State state;

        // Data model.
        private SinglePlayer model;

        // UI stuff.
        private GraphicsDeviceManager graphics;
        private SpriteManager spriteManager;
        private SpriteBatch spriteBatch;
        private SoundEffect chordSound;
    }
}
