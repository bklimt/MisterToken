using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    class Constants {
        //public const int MILLIS_PER_REPEAT = 200;    // time between key presses when held down.
        public const int MILLIS_PER_TOKEN = 250;     // time for the next token to become available.
        public const int MILLIS_PER_ADVANCE = 250;   // time for the active token to fall one row.
        public const int MILLIS_PER_FALL = 100;      // time for free squares to fall one row.
        public const int MILLIS_PER_CLEAR = 150;     // time for matched pieces to clear.

        public const int ROWS = 20;                  // rows on the board.
        public const int COLUMNS = 20;               // columns on the board.

        public const int BOARD_RECT_X = 280;          // x offset for drawing the board.
        public const int BOARD_RECT_Y = 40;          // y offset for drawing the board.
        public const int BOARD_RECT_WIDTH = 700;     // width of the board in pixels.
        public const int BOARD_RECT_HEIGHT = 700;    // height of the board in pixels.

        public const int BOARD_CIRCLE_X = 280;       // x offset for drawing the board.
        public const int BOARD_CIRCLE_Y = 40;        // y offset for drawing the board.
        public const int BOARD_CIRCLE_WIDTH = 700;   // width of the board in pixels.
        public const int BOARD_CIRCLE_HEIGHT = 700;  // height of the board in pixels.

        public const int TOKEN_START_COLUMN = 4;     // column that new tokens should start in.
        public const int TOP_FILLED_ROW = 10;        // how high the fixed pieces should start.
    }
}
