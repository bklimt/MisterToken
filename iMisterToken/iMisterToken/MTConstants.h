//
//  MTConstants.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#ifndef iMisterToken_MTConstants_h
#define iMisterToken_MTConstants_h

#define kMTMillisPerToken 350      // time for the next token to become available.
#define kMTMillisPerAdvance 400    // time for the active token to fall one row.
#define kMTMillisPerFall 100;      // time for free squares to fall one row.
#define kMTMILLIS_PER_CLEAR = 150  // time for matched pieces to clear.

#define kMTRows 15                 // rows on the board.
#define kMTColumns 12              // columns on the board.
#define kMTCellSize 32             // size of each cell in pixels.

#define kMTBoardOneRectX 150       // x offset for drawing the board.
#define kMTBoardTwoRectX 770       // x offset for drawing the board.
#define kMTBoardRectY 170          // y offset for drawing the board.

#define kMTTokenStartColumn 5      // column that new tokens should start in.

#endif
