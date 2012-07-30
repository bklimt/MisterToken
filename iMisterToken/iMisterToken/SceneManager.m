//
//  SceneManager.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "SceneManager.h"

#import "cocos2d.h"

#import "MenuLayer.h"
#import "SinglePlayerLayer.h"

@implementation SceneManager

+ (void)show:(CCLayer *)layer {
    CCDirector *director = [CCDirector sharedDirector];
    CCScene *newScene = [CCScene node];
    [newScene addChild:layer];
    if ([director runningScene]) {
        [director replaceScene:newScene];
    } else {
        [director runWithScene:newScene];
    }
}

+ (void)showMenu {
    [self show:[MenuLayer node]];
}

+ (void)showGame {
    [self show:[SinglePlayerLayer node]];
}

@end
