//
//  MenuLayer.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MenuLayer.h"

#import "SceneManager.h"

@implementation MenuLayer

- (id)init {
    if (self = [super init]) {
        CCSprite *background = [CCSprite spriteWithFile:@"background.png"];
        [self addChild:background];
        background.position = ccp(512, 384);
        
        CCLabelTTF *titleLeft = [CCLabelTTF labelWithString:@"Menu"
                                                   fontName:@"Marker Felt"
                                                   fontSize:48];
        titleLeft.position = ccp(80, 345);
        [self addChild:titleLeft];

        CCLabelTTF *titleRight = [CCLabelTTF labelWithString:@" System"
                                                    fontName:@"Marker Felt"
                                                    fontSize:48];
        titleRight.position = ccp(220, 345);
        [self addChild:titleRight];
        
        CCLabelTTF *titleCenterTop = [CCLabelTTF labelWithString:@"How to build a..."                       
                                                        fontName:@"Marker Felt"
                                                        fontSize:26];
        titleCenterTop.position = ccp(160, 380);
        [self addChild:titleCenterTop];

        CCLabelTTF *titleCenterBottom = [CCLabelTTF labelWithString:@"Part 1"
                                                           fontName:@"Marker Felt"
                                                           fontSize:48];
        titleCenterBottom.position = ccp(160, 300);
        [self addChild:titleCenterBottom];
        
        CCMenuItemFont *startNew = [CCMenuItemFont itemFromString:@"New Game" block:^(id sender) {
            [SceneManager showGame];
        }];

        CCMenuItemFont *credits = [CCMenuItemFont itemFromString:@"Credits" block:^(id sender) {
            [SceneManager showMenu];
        }];
        
        CCMenu *menu = [CCMenu menuWithItems:startNew, credits, nil];
        menu.position = ccp(160, 200);
        [menu alignItemsVerticallyWithPadding:40.0f];
        [self addChild:menu z:2];
    }
    return self;
}

@end
