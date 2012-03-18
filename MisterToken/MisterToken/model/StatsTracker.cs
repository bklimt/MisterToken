using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MisterToken {
    public class StatsTracker {
        public void Reset() {
            playerOneWins = 0;
            playerTwoWins = 0;
        }

        public void Win(PlayerIndex player) {
            if (player == PlayerIndex.One) {
                playerOneWins++;
            } else {
                playerTwoWins++;
            }
        }

        public void Lose(PlayerIndex player) {
            if (player == PlayerIndex.One) {
                playerTwoWins++;
            } else {
                playerOneWins++;
            }
        }

        public float GetGaugeMetric() {
            if (playerOneWins + playerTwoWins < 3) {
                return 0.5f + (playerTwoWins / 6.0f) - (playerOneWins / 6.0f);
            } else {
                return (float)playerTwoWins / (playerOneWins + playerTwoWins);
            }
        }

        private int playerOneWins;
        private int playerTwoWins;
    }
}
