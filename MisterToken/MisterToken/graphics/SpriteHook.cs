using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    public enum SpriteHook {
        // Layers are all 1280x720.
        TITLE_LAYER,
        BACKGROUND_LAYER,
        SCREEN_80_LAYER,
        SCREEN_50_LAYER,
        SPLATTER_LAYER,
        CLOUD_LAYER,

        // Strings are arbitrary size.
        PLAYER,
        WINNER,
        LOSER,
        PRESS_START_TO_CONTINUE,

        // Number strings.
        NUMBER_1,
        NUMBER_2,
    }
}
