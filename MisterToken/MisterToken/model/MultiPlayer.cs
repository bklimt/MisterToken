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
        public MultiPlayer(int level1, int level2, StatsTracker stats, GameListener listener) {
            this.stats = stats;
            this.listener = listener;
            one = new SinglePlayer(PlayerIndex.One, level1, false, this);
            two = new SinglePlayer(PlayerIndex.Two, level2, false, this);
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch) {
            one.Draw(device, spriteBatch);
            two.Draw(device, spriteBatch);

            // Draw the multiplayer score.
            int board1Right = Constants.BOARD_ONE_RECT_X + (Constants.CELL_SIZE * (Constants.COLUMNS + 2));
            int board2Left = Constants.BOARD_TWO_RECT_X - (Constants.CELL_SIZE * 2);
            int gapWidth = board2Left - board1Right;
            int gapHeight = (Constants.CELL_SIZE * (Constants.ROWS + 1));
            Rectangle mpRect = new Rectangle();
            mpRect.X = board1Right;
            mpRect.Y = Constants.BOARD_RECT_Y;
            mpRect.Width = gapWidth;
            mpRect.Height = gapHeight;
            Sprites.DrawGauge(stats.GetGaugeMetric(one.GetLockedCount(), two.GetLockedCount()), mpRect, spriteBatch);
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
            stats.Win(player);
            listener.OnWon(player);
            if (player == PlayerIndex.One) {
                two.Fail();
            } else {
                one.Fail();
            }
        }

        public void OnFailed(PlayerIndex player) {
            stats.Lose(player);
            listener.OnFailed(player);
            if (player == PlayerIndex.One) {
                two.Win();
            } else {
                one.Win();
            }
        }

        public void OnFinished(PlayerIndex player, bool shouldContinue, int level) {
            if (level >= Levels.GetLevelCount()) {
                listener.OnFinished(player, shouldContinue, Levels.GetLevelCount() - 1);
            }
            listener.OnFinished(player, shouldContinue, level);
        }

        private SinglePlayer one;
        private SinglePlayer two;
        private StatsTracker stats;
        private GameListener listener;
    }
}
