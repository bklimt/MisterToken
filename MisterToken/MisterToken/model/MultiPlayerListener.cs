using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public interface MultiPlayerListener {
        void OnClear(PlayerIndex player);
        void OnWon(PlayerIndex winner);
        void OnFailed(PlayerIndex winner);
        void OnFinished();
    }
}
