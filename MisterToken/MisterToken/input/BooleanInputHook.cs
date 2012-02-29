using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum BooleanInputHook {
        MENU_UP,
        MENU_DOWN,
        MENU_ENTER,

        PLAYER_ONE_START,
        PLAYER_ONE_TOKEN_SLAM,
        PLAYER_ONE_TOKEN_RIGHT,
        PLAYER_ONE_TOKEN_DOWN,
        PLAYER_ONE_TOKEN_LEFT,
        PLAYER_ONE_ROTATE_RIGHT,
        PLAYER_ONE_ROTATE_LEFT,

        PLAYER_TWO_START,
        PLAYER_TWO_TOKEN_SLAM,
        PLAYER_TWO_TOKEN_RIGHT,
        PLAYER_TWO_TOKEN_DOWN,
        PLAYER_TWO_TOKEN_LEFT,
        PLAYER_TWO_ROTATE_RIGHT,
        PLAYER_TWO_ROTATE_LEFT,
    }

    public enum PerPlayerBooleanInputHook {
        START,
        TOKEN_SLAM,
        TOKEN_RIGHT,
        TOKEN_DOWN,
        TOKEN_LEFT,
        ROTATE_RIGHT,
        ROTATE_LEFT,
    }

    public static class PerPlayerBooleanInputHookExtensions {
        public static BooleanInputHook ForPlayer(this PerPlayerBooleanInputHook input, PlayerIndex player) {
            if (player == PlayerIndex.One) {
                switch (input) {
                    case PerPlayerBooleanInputHook.START:
                        return BooleanInputHook.PLAYER_ONE_START;
                    case PerPlayerBooleanInputHook.TOKEN_SLAM:
                        return BooleanInputHook.PLAYER_ONE_TOKEN_SLAM;
                    case PerPlayerBooleanInputHook.TOKEN_RIGHT:
                        return BooleanInputHook.PLAYER_ONE_TOKEN_RIGHT;
                    case PerPlayerBooleanInputHook.TOKEN_DOWN:
                        return BooleanInputHook.PLAYER_ONE_TOKEN_DOWN;
                    case PerPlayerBooleanInputHook.TOKEN_LEFT:
                        return BooleanInputHook.PLAYER_ONE_TOKEN_LEFT;
                    case PerPlayerBooleanInputHook.ROTATE_RIGHT:
                        return BooleanInputHook.PLAYER_ONE_ROTATE_RIGHT;
                    case PerPlayerBooleanInputHook.ROTATE_LEFT:
                        return BooleanInputHook.PLAYER_ONE_ROTATE_LEFT;
                    default:
                        throw new Exception("Unknown PerPlayerBooleanInputHook: " + input);
                }
            } else if (player == PlayerIndex.Two) {
                switch (input) {
                    case PerPlayerBooleanInputHook.START:
                        return BooleanInputHook.PLAYER_TWO_START;
                    case PerPlayerBooleanInputHook.TOKEN_SLAM:
                        return BooleanInputHook.PLAYER_TWO_TOKEN_SLAM;
                    case PerPlayerBooleanInputHook.TOKEN_RIGHT:
                        return BooleanInputHook.PLAYER_TWO_TOKEN_RIGHT;
                    case PerPlayerBooleanInputHook.TOKEN_DOWN:
                        return BooleanInputHook.PLAYER_TWO_TOKEN_DOWN;
                    case PerPlayerBooleanInputHook.TOKEN_LEFT:
                        return BooleanInputHook.PLAYER_TWO_TOKEN_LEFT;
                    case PerPlayerBooleanInputHook.ROTATE_RIGHT:
                        return BooleanInputHook.PLAYER_TWO_ROTATE_RIGHT;
                    case PerPlayerBooleanInputHook.ROTATE_LEFT:
                        return BooleanInputHook.PLAYER_TWO_ROTATE_LEFT;
                    default:
                        throw new Exception("Unknown PerPlayerBooleanInputHook: " + input);
                }
            } else {
                throw new Exception("Player " + player + " not supported.");
            }
        }
    }
}
