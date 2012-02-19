using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public interface SinglePlayerListener {
        void OnClear();
        void OnWon();
        void OnFailed();
        void OnFinished();
    }
}
