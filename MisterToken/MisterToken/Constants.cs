using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    class Constants {
        public const int MILLIS_PER_TOKEN = 350;     // time for the next token to become available.
        public const int MILLIS_PER_ADVANCE = 400;   // time for the active token to fall one row.
        public const int MILLIS_PER_FALL = 100;      // time for free squares to fall one row.
        public const int MILLIS_PER_CLEAR = 150;     // time for matched pieces to clear.

        public const int ROWS = 15;                  // rows on the board.
        public const int COLUMNS = 12;               // columns on the board.
        public const int CELL_SIZE = 32;             // size of each cell in pixels.

        public const int BOARD_ONE_RECT_X = 150;     // x offset for drawing the board.
        public const int BOARD_TWO_RECT_X = 770;     // x offset for drawing the board.
        public const int BOARD_RECT_Y = 170;          // y offset for drawing the board.

        public const int TOKEN_START_COLUMN = 5;     // column that new tokens should start in.

    }
}
