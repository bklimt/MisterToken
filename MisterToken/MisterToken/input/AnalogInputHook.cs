using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MisterToken {
    public enum AnalogInputHook {
        PLAYER_ONE_TOKEN_LEFT,
        PLAYER_ONE_TOKEN_RIGHT,
        PLAYER_ONE_TOKEN_DOWN,
        PLAYER_ONE_MENU_DOWN,
        PLAYER_ONE_MENU_UP,

        PLAYER_TWO_TOKEN_LEFT,
        PLAYER_TWO_TOKEN_RIGHT,
        PLAYER_TWO_TOKEN_DOWN,
        PLAYER_TWO_MENU_DOWN,
        PLAYER_TWO_MENU_UP,
    }

    public enum PerPlayerAnalogInputHook {
        TOKEN_LEFT,
        TOKEN_RIGHT,
        TOKEN_DOWN,
        MENU_UP,
        MENU_DOWN,
    }

    public static class PerPlayerAnalogInputHookExtensions {
        public static AnalogInputHook ForPlayer(this PerPlayerAnalogInputHook input, PlayerIndex player) {
            if (player == PlayerIndex.One) {
                switch (input) {
                    case PerPlayerAnalogInputHook.TOKEN_LEFT:
                        return AnalogInputHook.PLAYER_ONE_TOKEN_LEFT;
                    case PerPlayerAnalogInputHook.TOKEN_RIGHT:
                        return AnalogInputHook.PLAYER_ONE_TOKEN_RIGHT;
                    case PerPlayerAnalogInputHook.TOKEN_DOWN:
                        return AnalogInputHook.PLAYER_ONE_TOKEN_DOWN;
                    case PerPlayerAnalogInputHook.MENU_DOWN:
                        return AnalogInputHook.PLAYER_ONE_MENU_DOWN;
                    case PerPlayerAnalogInputHook.MENU_UP:
                        return AnalogInputHook.PLAYER_ONE_MENU_UP;
                    default:
                        throw new Exception("Unknown PerPlayerAnalogInputHook: " + input);
                }
            } else if (player == PlayerIndex.Two) {
                switch (input) {
                    case PerPlayerAnalogInputHook.TOKEN_LEFT:
                        return AnalogInputHook.PLAYER_TWO_TOKEN_LEFT;
                    case PerPlayerAnalogInputHook.TOKEN_RIGHT:
                        return AnalogInputHook.PLAYER_TWO_TOKEN_RIGHT;
                    case PerPlayerAnalogInputHook.TOKEN_DOWN:
                        return AnalogInputHook.PLAYER_TWO_TOKEN_DOWN;
                    case PerPlayerAnalogInputHook.MENU_DOWN:
                        return AnalogInputHook.PLAYER_TWO_MENU_DOWN;
                    case PerPlayerAnalogInputHook.MENU_UP:
                        return AnalogInputHook.PLAYER_TWO_MENU_UP;
                    default:
                        throw new Exception("Unknown PerPlayerAnalogInputHook: " + input);
                }
            } else {
                throw new Exception("Player " + player + " not supported.");
            }
        }
    }
}
