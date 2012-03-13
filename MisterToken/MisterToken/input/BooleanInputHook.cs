using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum BooleanInputHook {
        PLAYER_ONE_TOKEN_SLAM,
        PLAYER_ONE_TOKEN_RIGHT,
        PLAYER_ONE_TOKEN_DOWN,
        PLAYER_ONE_TOKEN_LEFT,
        PLAYER_ONE_ROTATE_RIGHT,
        PLAYER_ONE_ROTATE_LEFT,
        PLAYER_ONE_MENU_UP,
        PLAYER_ONE_MENU_DOWN,
        PLAYER_ONE_MENU_ENTER,
        PLAYER_ONE_MENU_BACK,

        PLAYER_TWO_TOKEN_SLAM,
        PLAYER_TWO_TOKEN_RIGHT,
        PLAYER_TWO_TOKEN_DOWN,
        PLAYER_TWO_TOKEN_LEFT,
        PLAYER_TWO_ROTATE_RIGHT,
        PLAYER_TWO_ROTATE_LEFT,
        PLAYER_TWO_MENU_UP,
        PLAYER_TWO_MENU_DOWN,
        PLAYER_TWO_MENU_ENTER,
        PLAYER_TWO_MENU_BACK,
    }

    public enum PerPlayerBooleanInputHook {
        TOKEN_SLAM,
        TOKEN_RIGHT,
        TOKEN_DOWN,
        TOKEN_LEFT,
        ROTATE_RIGHT,
        ROTATE_LEFT,
        MENU_UP,
        MENU_DOWN,
        MENU_ENTER,
        MENU_BACK,
    }

    public static class PerPlayerBooleanInputHookExtensions {
        public static BooleanInputHook ForPlayer(this PerPlayerBooleanInputHook input, PlayerIndex player) {
            if (player == PlayerIndex.One) {
                switch (input) {
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
                    case PerPlayerBooleanInputHook.MENU_UP:
                        return BooleanInputHook.PLAYER_ONE_MENU_UP;
                    case PerPlayerBooleanInputHook.MENU_DOWN:
                        return BooleanInputHook.PLAYER_ONE_MENU_DOWN;
                    case PerPlayerBooleanInputHook.MENU_ENTER:
                        return BooleanInputHook.PLAYER_ONE_MENU_ENTER;
                    case PerPlayerBooleanInputHook.MENU_BACK:
                        return BooleanInputHook.PLAYER_ONE_MENU_BACK;
                    default:
                        throw new Exception("Unknown PerPlayerBooleanInputHook: " + input);
                }
            } else if (player == PlayerIndex.Two) {
                switch (input) {
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
                    case PerPlayerBooleanInputHook.MENU_UP:
                        return BooleanInputHook.PLAYER_TWO_MENU_UP;
                    case PerPlayerBooleanInputHook.MENU_DOWN:
                        return BooleanInputHook.PLAYER_TWO_MENU_DOWN;
                    case PerPlayerBooleanInputHook.MENU_ENTER:
                        return BooleanInputHook.PLAYER_TWO_MENU_ENTER;
                    case PerPlayerBooleanInputHook.MENU_BACK:
                        return BooleanInputHook.PLAYER_TWO_MENU_BACK;
                    default:
                        throw new Exception("Unknown PerPlayerBooleanInputHook: " + input);
                }
            } else {
                throw new Exception("Player " + player + " not supported.");
            }
        }
    }
}
