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
        SPLATTER_LAYER,
        CLOUD_LAYER,
        MENU_OVERLAY_LAYER,
        TITLE_OVERLAY_LAYER,

        // Strings are arbitrary size.
        WINNER,
        LOSER,

        // Gauge.
        GAUGE_BACKGROUND,
        GAUGE_ARROW,
        GAUGE_GLASS,
        GAUGE_GAME,
        GAUGE_MATCH,

        // Other.
        BOMB,
        SKULL,
        MENU_PANEL,
        MENU_CHECK_OVERLAY,
    }
}
