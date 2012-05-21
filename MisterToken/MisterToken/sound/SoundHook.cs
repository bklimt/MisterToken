﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum SoundHook {
        CLEAR_1,
        CLEAR_2,
        CLEAR_3,
        DUMP,
        LOST,
        ROTATE_LEFT,
        ROTATE_RIGHT,
        SLAM,
        WON,

        SONG_NONE,
        SONG_2,
        SONG_3,
        SONG_RANDOM,
    }

    public enum MusicHook {
        SONG_1,
        SONG_2,
    }
}
