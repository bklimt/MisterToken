using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MisterToken {
    class Constants {
        public const int MILLIS_PER_TOKEN = 250;     // time for the next token to become available.
        public const int MILLIS_PER_ADVANCE = 250;   // time for the active token to fall one row.
        public const int MILLIS_PER_FALL = 100;      // time for free squares to fall one row.
        public const int MILLIS_PER_CLEAR = 150;     // time for matched pieces to clear.

        public const int ROWS = 15;                  // rows on the board.
        public const int COLUMNS = 10;               // columns on the board.

        public const int BOARD_ONE_RECT_X = 280;     // x offset for drawing the board.
        public const int BOARD_TWO_RECT_X = 720;     // x offset for drawing the board.
        public const int BOARD_RECT_Y = 140;          // y offset for drawing the board.
        public const int BOARD_RECT_WIDTH = 300;     // width of the board in pixels.
        public const int BOARD_RECT_HEIGHT = 450;    // height of the board in pixels.

        public const int TOKEN_START_COLUMN = 4;     // column that new tokens should start in.
        public const int TOP_FILLED_ROW = 10;        // how high the fixed pieces should start.
        public const float PROBABILTIY_FILLED = 0.2f;

        public const float PROBABILITY_TWO_PIECE = 0.90f;
        public const float PROBABILITY_THREE_PIECE_ELBOW = 0.10f;
    }
}
