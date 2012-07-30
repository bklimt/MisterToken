//
//  Cell.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTCell.h"

@implementation MTCell

@synthesize color;
@synthesize direction;
@synthesize visited;
@synthesize loose;
@synthesize matched;
@synthesize locked;

+ (MTCell *)cellWithColor:(MTCellColor)aColor direction:(MTCellDirection)aDirection {
    return [[[MTCell alloc] initWithColor:aColor direction:aDirection] autorelease];
}

- (id)initWithColor:(MTCellColor)aColor direction:(MTCellDirection)aDirection {
    if (self = [super init]) {
        [self clear];
        color = aColor;
        direction = aDirection;
    }
    return self;
}

- (id)init {
    return [self initWithColor:kMTCellColorBlack direction:kMTCellDirectionNone];
}

- (id)copyWithZone:(NSZone *)zone {
    MTCell *other = [[MTCell allocWithZone:zone] init];
    other.color = self.color;
    other.direction = self.direction;
    other.visited = self.visited;
    other.loose = self.loose;
    other.matched = self.matched;
    other.locked = self.locked;
    return other;
}

- (void)drawRect:(CGRect)rect {
    
}

- (void)rotateRight {
    MTCellDirection newDirection = kMTCellDirectionNone;
    if (self.direction & kMTCellDirectionUp) { newDirection |= kMTCellDirectionRight; }
    if (self.direction & kMTCellDirectionRight) { newDirection |= kMTCellDirectionDown; }
    if (self.direction & kMTCellDirectionDown) { newDirection |= kMTCellDirectionLeft; }
    if (self.direction & kMTCellDirectionLeft) { newDirection |= kMTCellDirectionUp; }
    self.direction = newDirection;
}

- (void)rotateLeft {
    MTCellDirection newDirection = kMTCellDirectionNone;
    if (self.direction & kMTCellDirectionUp) { newDirection |= kMTCellDirectionLeft; }
    if (self.direction & kMTCellDirectionRight) { newDirection |= kMTCellDirectionUp; }
    if (self.direction & kMTCellDirectionDown) { newDirection |= kMTCellDirectionRight; }
    if (self.direction & kMTCellDirectionLeft) { newDirection |= kMTCellDirectionDown; }
    self.direction = newDirection;
}

- (void)clear {
    self.color = kMTCellColorBlack;
    self.direction = kMTCellDirectionNone;
    self.locked = NO;
    self.loose = NO;
    self.matched = NO;
    self.visited = NO;
}

@end
