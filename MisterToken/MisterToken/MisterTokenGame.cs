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
    public class MisterTokenGame : Microsoft.Xna.Framework.Game, GameListener {
        public MisterTokenGame() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = 720;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.ApplyChanges();

            titleMenu = new Menu(delegate() { Exit(); });
            levelMenu = new Menu(delegate() { state = State.TITLE_MENU;  });
        }

        protected override void Initialize() {
            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Sprites.LoadContent(Content, GraphicsDevice);
            Sound.LoadContent(Content);
            Levels.LoadContent(Content);

            titleMenu.Add("1 Player", delegate() {
                singlePlayer = true;
                state = State.LEVEL_MENU;
            });
            titleMenu.Add("2 Player", delegate() {
                singlePlayer = false;
                state = State.LEVEL_MENU;
            });

            for (int i = 0; i < Levels.GetLevelCount(); ++i) {
                // Capture a copy of i for the delegate closure below.
                int j = i;
                levelMenu.Add(Levels.GetLevel(j).GetName(), delegate() {
                    if (singlePlayer) {
                        model = new SinglePlayer(PlayerIndex.One, j, this);
                    } else {
                        model = new MultiPlayer(j, j, this);
                    }
                    state = State.PLAYING;
                });
            }
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            Input.Update(gameTime);
            switch (state) {
                case State.TITLE_MENU:
                    titleMenu.Update();
                    break;
                case State.LEVEL_MENU:
                    levelMenu.Update();
                    break;
                case State.PLAYING:
                    model.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            switch (state) {
                case State.TITLE_MENU:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 380, 320, 130), true, spriteBatch);
                    levelMenu.Draw(new Rectangle(655, 180, 320, 510), false, spriteBatch);
                    spriteBatch.End();
                    break;
                case State.LEVEL_MENU:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 380, 320, 130), false, spriteBatch);
                    levelMenu.Draw(new Rectangle(655, 180, 320, 510), true, spriteBatch);
                    spriteBatch.End();
                    break;
                case State.PLAYING:
                    GraphicsDevice.Clear(Color.Black);
                    spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
                    Sprites.DrawLayer(SpriteHook.BACKGROUND_LAYER, spriteBatch);
                    model.Draw(GraphicsDevice, spriteBatch);
                    spriteBatch.End();
                    break;
            }
            base.Draw(gameTime);
        }

        public void OnPaused(PlayerIndex player, bool paused) {
        }

        public void OnClear(PlayerIndex player) {
        }

        public void OnDump(PlayerIndex player, List<CellColor> colors) {
        }

        public void OnWon(PlayerIndex player) {
        }

        public void OnFailed(PlayerIndex player) {
        }

        public void OnFinished(PlayerIndex player) {
            state = State.TITLE_MENU;
        }

        // Game state.
        private enum State {
            TITLE_MENU,
            LEVEL_MENU,
            PLAYING,
        }
        private State state;

        // Data model.
        private Menu titleMenu;
        private Menu levelMenu;
        private Game model;
        private bool singlePlayer;

        // UI stuff.
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
    }
}
