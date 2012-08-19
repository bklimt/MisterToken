//
//  SinglePlayerLayer.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "SinglePlayerLayer.h"

#import "MTBoard.h"
#import "MTTokenGenerator.h"

@implementation SinglePlayerLayer

- (id)initWithPlayer:(int)aPlayer
               level:(MTLevel *)aLevel
              random:(NSObject<MTRandom> *)aRandom
        singlePlayer:(BOOL)isSinglePlayer {
    
    player = aPlayer;
    level = aLevel;
    random = aRandom;
    singlePlayer = isSinglePlayer;
    
    paused = NO;
    otherPaused = NO;
    nextTokenReadiness = 0.0f;
    
    self.board = [MTBoard board...];
    
    if (self = [super init]) {
        CCSprite *background = [CCSprite spriteWithFile:@"background.png"];
        [self addChild:background];
        background.position = ccp(512, 384);
        
        CCLabelTTF *titleLeft = [CCLabelTTF labelWithString:@"Game"
                                                   fontName:@"Marker Felt"
                                                   fontSize:48];
        titleLeft.position = ccp(80, 345);
        [self addChild:titleLeft];
    }
    return self;
}

@end
