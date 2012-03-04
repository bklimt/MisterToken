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
    public class MultiPlayer : Game, GameListener {
        public MultiPlayer(int level1, int level2, GameListener listener) {
            this.listener = listener;
            one = new SinglePlayer(PlayerIndex.One, level1, this);
            two = new SinglePlayer(PlayerIndex.Two, level2, this);
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch) {
            one.Draw(device, spriteBatch);
            two.Draw(device, spriteBatch);
        }

        public void Update(GameTime gameTime) {
            one.Update(gameTime);
            two.Update(gameTime);
        }

        public void OnPaused(PlayerIndex player, bool paused) {
            one.SetPaused(paused);
            two.SetPaused(paused);
        }

        public void OnClear(PlayerIndex player) {
            listener.OnClear(player);
        }

        public void OnDump(PlayerIndex player, List<CellColor> colors) {
            if (player == PlayerIndex.One) {
                two.Dump(colors);
            } else {
                one.Dump(colors);
            }
            listener.OnDump(player, colors);
        }

        public void OnWon(PlayerIndex player) {
            listener.OnWon(player);
            if (player == PlayerIndex.One) {
                two.Fail();
            } else {
                one.Fail();
            }
        }

        public void OnFailed(PlayerIndex player) {
            listener.OnFailed(player);
            if (player == PlayerIndex.One) {
                two.Win();
            } else {
                one.Win();
            }
        }

        public void OnFinished(PlayerIndex player) {
            listener.OnFinished(player);
        }

        private SinglePlayer one;
        private SinglePlayer two;
        private GameListener listener;
    }
}
