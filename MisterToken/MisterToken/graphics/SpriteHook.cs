using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public enum SpriteHook {
        // Layers are all 1280x720.
        TITLE_LAYER,
        BACKGROUND_LAYER,
        HELP_LAYER,
        SCREEN_80_LAYER,
        SCREEN_50_LAYER,
        SPLATTER_LAYER,
        CLOUD_LAYER,

        // Strings are arbitrary size.
        WINNER,
        LOSER,

        // Other.
        BOMB,
        WILD,
        SKULL,
    }
}
