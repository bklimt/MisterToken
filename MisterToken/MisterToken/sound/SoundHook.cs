using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum SoundHook {
        PLAYER_ONE_CLEAR,
        PLAYER_ONE_WON,
        PLAYER_ONE_LOST,
        PLAYER_TWO_CLEAR,
        PLAYER_TWO_WON,
        PLAYER_TWO_LOST,
    }

    public enum PerPlayerSoundHook {
        CLEAR,
        WON,
        LOST,
    }

    public static class PerPlayerSoundHookExtensions {
        public static SoundHook ForPlayer(this PerPlayerSoundHook sound, PlayerIndex player) {
            if (player == PlayerIndex.One) {
                switch (sound) {
                    case PerPlayerSoundHook.CLEAR:
                        return SoundHook.PLAYER_ONE_CLEAR;
                    case PerPlayerSoundHook.WON:
                        return SoundHook.PLAYER_ONE_WON;
                    case PerPlayerSoundHook.LOST:
                        return SoundHook.PLAYER_ONE_LOST;
                    default:
                        throw new Exception("Unknown PerPlayerSoundHook: " + sound);
                }
            } else if (player == PlayerIndex.Two) {
                switch (sound) {
                    case PerPlayerSoundHook.CLEAR:
                        return SoundHook.PLAYER_TWO_CLEAR;
                    case PerPlayerSoundHook.WON:
                        return SoundHook.PLAYER_TWO_WON;
                    case PerPlayerSoundHook.LOST:
                        return SoundHook.PLAYER_TWO_LOST;
                    default:
                        throw new Exception("Unknown PerPlayerSoundHook: " + sound);
                }
            } else {
                throw new Exception("Player " + player + " not supported.");
            }
        }
    }
}
