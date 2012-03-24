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
        public MultiPlayer(Level level, StatsTracker stats, GameListener listener) {
            this.stats = stats;
            this.listener = listener;
            one = new SinglePlayer(PlayerIndex.One, level, false, this);
            two = new SinglePlayer(PlayerIndex.Two, new Level(level), false, this);
        }

        public void Draw(GraphicsDevice device, SpriteBatch spriteBatch) {
            one.Draw(device, spriteBatch);
            two.Draw(device, spriteBatch);

            // Draw the multiplayer score.
            Rectangle mpRect1 = new Rectangle();
            mpRect1.X = Constants.BOARD_ONE_RECT_X + (Constants.CELL_SIZE * (Constants.COLUMNS + 2));
            mpRect1.Width = (Constants.BOARD_TWO_RECT_X - (Constants.CELL_SIZE * 2)) - mpRect1.X;
            mpRect1.Y = Constants.BOARD_RECT_Y + (Constants.CELL_SIZE * 4);
            mpRect1.Height = (Constants.CELL_SIZE * (Constants.ROWS - 3)) / 2;

            Rectangle mpRect2 = new Rectangle();
            mpRect2.X = mpRect1.X;
            mpRect2.Width = mpRect1.Width;
            mpRect2.Height = mpRect1.Height;
            mpRect2.Y = mpRect1.Y + mpRect1.Height;

            float gameMetric = 0.5f;
            float lockedOne = one.GetLockedCount();
            float lockedTwo = two.GetLockedCount();
            if (lockedOne + lockedTwo != 0) {
                gameMetric = lockedOne / (lockedOne + lockedTwo);
            }
            Sprites.DrawGauge(gameMetric, false, mpRect1, spriteBatch);
            Sprites.DrawGauge(stats.GetGaugeMetric(), true, mpRect2, spriteBatch);
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

        public void OnFinished(PlayerIndex player, bool shouldContinue, Level level) {
            Level next = level.GetNext();
            if (next == null) {
                listener.OnFinished(player, shouldContinue, level);
            } else {
                listener.OnFinished(player, shouldContinue, next);
            }
        }

        private SinglePlayer one;
        private SinglePlayer two;
        private StatsTracker stats;
        private GameListener listener;
    }
}
