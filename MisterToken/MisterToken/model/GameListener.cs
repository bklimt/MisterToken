using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MisterToken {
    public interface GameListener {
        void OnClear(PlayerIndex player);
        void OnDump(PlayerIndex player, List<Cell.Color> colors);
        void OnWon(PlayerIndex player);
        void OnFailed(PlayerIndex player);
        void OnFinished(PlayerIndex player);
    }
}
