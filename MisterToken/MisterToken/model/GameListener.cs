using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MisterToken {
    public interface GameListener {
        void OnPaused(PlayerIndex player, bool paused);
        void OnClear(PlayerIndex player);
        void OnDump(PlayerIndex player, List<CellColor> colors);
        void OnWon(PlayerIndex player);
        void OnFailed(PlayerIndex player);
        void OnFinished(PlayerIndex player, bool shouldContinue, Level level);
    }
}
