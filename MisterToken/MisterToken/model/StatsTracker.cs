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

        public float GetGaugeMetric(int playerOneLocked, int playerTwoLocked) {
            if (playerOneLocked == 0 || playerTwoLocked == 0) {
                return GetGaugeMetricWithWins(0, 0);
            }
            float playerOneBias = (float)playerTwoLocked / (playerOneLocked + playerTwoLocked);
            return GetGaugeMetricWithWins(1, 0) * playerOneBias + GetGaugeMetricWithWins(0, 1) * (1.0f - playerOneBias);
        }

        private float GetGaugeMetricWithWins(int playerOneDelta, int playerTwoDelta) {
            if (playerOneWins + playerOneDelta + playerTwoWins + playerTwoDelta < 3) {
                return 0.5f + ((playerTwoWins + playerTwoDelta) / 6.0f) - ((playerOneWins + playerOneDelta) / 6.0f);
            } else {
                return (float)(playerTwoWins + playerTwoDelta) / (playerOneWins + playerOneDelta + playerTwoWins + playerTwoDelta);
            }
        }

        private int playerOneWins;
        private int playerTwoWins;
    }
}
