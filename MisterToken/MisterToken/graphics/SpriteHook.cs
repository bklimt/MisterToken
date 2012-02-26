using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public enum SpriteHook {
        // Layers are all 1280x720.
        TITLE_LAYER,
        BACKGROUND_LAYER,
        SCREEN_LAYER,
        SPLATTER_LAYER,
        CLOUD_LAYER,

        // Strings are arbitrary size.
        WINNER,
        LOSER,
    }
}
