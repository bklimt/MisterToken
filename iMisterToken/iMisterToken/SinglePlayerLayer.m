//
//  SinglePlayerLayer.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "SinglePlayerLayer.h"

@implementation SinglePlayerLayer

- (id)init {
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
