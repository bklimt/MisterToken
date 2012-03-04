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
            Components.Add(new GamerServicesComponent(this));

            titleMenu = new Menu(delegate() { SaveAndQuit(); });
            levelMenu = new Menu(delegate() { state = State.TITLE_MENU; });
            videoMenu = new Menu(delegate() { state = State.TITLE_MENU; });
        }

        protected override void Initialize() {
            base.Initialize();
            Storage.Load(delegate() {
                graphics.PreferredBackBufferHeight = Storage.GetSaveData().height;
                graphics.PreferredBackBufferWidth = Storage.GetSaveData().width;
                graphics.IsFullScreen = Storage.GetSaveData().fullscreen;
                graphics.ApplyChanges();
            });
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
            titleMenu.Add("Video", delegate() {
                state = State.VIDEO_MENU;
            });
            titleMenu.Add("Exit", delegate() {
                SaveAndQuit();
            });

            videoMenu.Add("Fullscreen Native", delegate() {
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();
                graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();

                Storage.GetSaveData().height = graphics.PreferredBackBufferHeight;
                Storage.GetSaveData().width = graphics.PreferredBackBufferWidth;
                Storage.GetSaveData().fullscreen = graphics.IsFullScreen;
            });
            videoMenu.Add("Window 1280x720 (16:9)", delegate() {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();

                Storage.GetSaveData().height = graphics.PreferredBackBufferHeight;
                Storage.GetSaveData().width = graphics.PreferredBackBufferWidth;
                Storage.GetSaveData().fullscreen = graphics.IsFullScreen;
            });
            videoMenu.Add("Window 640x480 (4:3)", delegate() {
                graphics.PreferredBackBufferWidth = 640;
                graphics.PreferredBackBufferHeight = 480;
                graphics.IsFullScreen = false;
                graphics.ApplyChanges();

                Storage.GetSaveData().height = graphics.PreferredBackBufferHeight;
                Storage.GetSaveData().width = graphics.PreferredBackBufferWidth;
                Storage.GetSaveData().fullscreen = graphics.IsFullScreen;
            });
            videoMenu.Add("Fullscreen 1280x720", delegate() {
                graphics.PreferredBackBufferWidth = 1280;
                graphics.PreferredBackBufferHeight = 720;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();

                Storage.GetSaveData().height = graphics.PreferredBackBufferHeight;
                Storage.GetSaveData().width = graphics.PreferredBackBufferWidth;
                Storage.GetSaveData().fullscreen = graphics.IsFullScreen;
            });
            videoMenu.Add("Fullscreen 640x480", delegate() {
                graphics.PreferredBackBufferWidth = 640;
                graphics.PreferredBackBufferHeight = 480;
                graphics.IsFullScreen = true;
                graphics.ApplyChanges();

                Storage.GetSaveData().height = graphics.PreferredBackBufferHeight;
                Storage.GetSaveData().width = graphics.PreferredBackBufferWidth;
                Storage.GetSaveData().fullscreen = graphics.IsFullScreen;

            });
            videoMenu.Add("Back", delegate() {
                state = State.TITLE_MENU;
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
                case State.VIDEO_MENU:
                    videoMenu.Update();
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
            GraphicsDevice.Clear(Color.Black);

            // Figure out the right transformation for the resolution.
            Matrix transform = Matrix.Identity;
            if (GraphicsDevice.Viewport.Height != 720 || GraphicsDevice.Viewport.Width != 1280) {
                // Rescale it.
                float scale = GraphicsDevice.Viewport.Height / 720.0f;
                if ((GraphicsDevice.Viewport.Width / scale) < 1280) {
                    scale = GraphicsDevice.Viewport.Width / 1280.0f;
                }

                // Recenter it.
                int height = (int)Math.Round(GraphicsDevice.Viewport.Height / scale);
                int width = (int)Math.Round(GraphicsDevice.Viewport.Width / scale);
                transform = Matrix.CreateTranslation((width - 1280) / 2, (height - 720) / 2, 0);
                transform *= Matrix.CreateScale(scale);
            }
            spriteBatch.Begin(SpriteSortMode.Deferred,
                              BlendState.AlphaBlend,
                              SamplerState.LinearClamp,
                              DepthStencilState.None,
                              RasterizerState.CullCounterClockwise,
                              null,
                              transform);
            
            switch (state) {
                case State.TITLE_MENU:
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 380, 320, 260), true, spriteBatch);
                    break;
                case State.VIDEO_MENU:
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 380, 320, 260), false, spriteBatch);
                    videoMenu.Draw(new Rectangle(620, 280, 640, 380), true, spriteBatch);
                    break;
                case State.LEVEL_MENU:
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 380, 320, 260), false, spriteBatch);
                    levelMenu.Draw(new Rectangle(655, 180, 320, 510), true, spriteBatch);
                    break;
                case State.PLAYING:
                    Sprites.DrawLayer(SpriteHook.BACKGROUND_LAYER, spriteBatch);
                    model.Draw(GraphicsDevice, spriteBatch);
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public void SaveAndQuit() {
            Storage.Save();
            Exit();
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
            state = State.LEVEL_MENU;
        }

        // Game state.
        private enum State {
            TITLE_MENU,
            VIDEO_MENU,
            LEVEL_MENU,
            PLAYING,
        }
        private State state;

        // Menus
        private Menu titleMenu;
        private Menu videoMenu;
        private Menu levelMenu;

        // Data model.
        private Game model;
        private bool singlePlayer;

        // UI stuff.
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
    }
}
