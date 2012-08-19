//
//  SinglePlayerLayer.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "cocos2d.h"

#import "MTRandom.h"

@class MTBoard;
@class MTLevel;
@class MTTokenGenerator;

typedef enum {
    kMTGameStateSettingUpBoard,
    kMTGameStateDumping,
    kMTGameStateWaitingForToken,
    kMTGameStateMovingToken,
    kMTGameStateClearing,
    kMTGameStateFalling,
    kMTGameStateFailed,
    kMTGameStateWon,
} MTGameState;

@interface SinglePlayerLayer : CCLayer {
    // Game state.
    MTGameState state;

    // Pause state.
    BOOL paused;
    BOOL otherPaused;

    // Waiting for token.
    int timeToNextToken;

    // Moving token.
    int timeUntilNextAdvance;

    // Clearing.
    int timeToClear;

    // Falling.
    int timeToNextFall;

    // Won.

    // Failed.

    // Internal State.
    BOOL singlePlayer;
    int player;
    MTLevel *level;  // TODO: Who owns this?
    NSObject<MTRandom> *random;  // TODO: Who owns this?
    float nextTokenReadiness;
}

@property (nonatomic, retain) MTBoard *board;
@property (nonatomic, retain) MTTokenGenerator *tokenGenerator;
@property (nonatomic, retain) NSMutableArray *matches;
@property (nonatomic, retain) NSMutableArray *dumps;

@end
