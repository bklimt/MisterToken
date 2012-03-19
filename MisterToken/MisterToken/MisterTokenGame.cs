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

            stats = new StatsTracker();

            titleMenu = new Menu(PlayerIndex.One, delegate() { SaveAndQuit(); });
            levelMenu = new Menu(PlayerIndex.One, delegate() { state = State.TITLE_MENU; });
            videoMenu = new Menu(PlayerIndex.One, delegate() { state = State.TITLE_MENU; });
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
                subMenu = levelMenu;
                state = State.SUB_MENU;
            });
            titleMenu.Add("2 Player", delegate() {
                singlePlayer = false;
                subMenu = levelMenu;
                state = State.SUB_MENU;
            });
            titleMenu.Add("Video", delegate() {
                subMenu = videoMenu;
                state = State.SUB_MENU;
            });
            titleMenu.Add("Help", delegate() {
                state = State.HELP_SCREEN;
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
                levelMenu.AddLevel(Levels.GetLevel(j), delegate(Level level) {
                    if (singlePlayer) {
                        model = new SinglePlayer(PlayerIndex.One, j, true, this);
                    } else {
                        model = new MultiPlayer(j, j, stats, this);
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
                case State.SUB_MENU:
                    subMenu.Update();
                    break;
                case State.HELP_SCREEN:
                    if (Input.IsDown(BooleanInputHook.PLAYER_ONE_MENU_BACK) ||
                        Input.IsDown(BooleanInputHook.PLAYER_ONE_MENU_ENTER) ||
                        Input.IsDown(BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT) ||
                        Input.IsDown(BooleanInputHook.PLAYER_ONE_ROTATE_LEFT)) {
                        state = State.TITLE_MENU;
                    }
                    break;
                case State.PLAYING:
                    model.Update(gameTime);
                    break;
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            Sprites.Update(gameTime);
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
                    titleMenu.Draw(new Rectangle(255, 280, 320, 320), true, spriteBatch);
                    break;
                case State.SUB_MENU:
                    Sprites.DrawLayer(SpriteHook.TITLE_LAYER, spriteBatch);
                    titleMenu.Draw(new Rectangle(255, 280, 320, 320), false, spriteBatch);
                    subMenu.Draw(new Rectangle(620, 180, 400, 510), true, spriteBatch);
                    break;
                case State.HELP_SCREEN:
                    Sprites.DrawLayer(SpriteHook.HELP_LAYER, spriteBatch);
                    break;
                case State.PLAYING:
                    Sprites.DrawLayer(SpriteHook.BACKGROUND_LAYER, spriteBatch);
                    Sprites.DrawLayer(SpriteHook.SCREEN_80_LAYER, spriteBatch);
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

        public void OnFinished(PlayerIndex player, bool shouldContinue, int level) {
            if (shouldContinue) {
                if (singlePlayer) {
                    model = new SinglePlayer(PlayerIndex.One, level, true, this);
                } else {
                    model = new MultiPlayer(level, level, stats, this);
                }
                state = State.PLAYING;
            } else {
                subMenu = levelMenu;
                state = State.SUB_MENU;
            }
        }

        // Game state.
        private enum State {
            TITLE_MENU,
            SUB_MENU,
            HELP_SCREEN,
            PLAYING,
        }
        private State state;

        // Menus
        private Menu titleMenu;
        private Menu videoMenu;
        private Menu levelMenu;
        private Menu subMenu;

        // Data model.
        private Game model;
        private bool singlePlayer;
        private StatsTracker stats;

        // UI stuff.
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
    }
}
