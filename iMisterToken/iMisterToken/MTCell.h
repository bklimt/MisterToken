//
//  MTCell.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "MTCellColor.h"

typedef enum {
    kMTCellDirectionNone = 0x00,
    kMTCellDirectionUp = 0x01,
    kMTCellDirectionRight = 0x02,
    kMTCellDirectionDown = 0x04,
    kMTCellDirectionLeft = 0x08,
} MTCellDirection;

@interface MTCell : NSObject <NSCopying>

@property MTCellColor color;
@property MTCellDirection direction;
@property BOOL visited;
@property BOOL loose;
@property BOOL matched;
@property BOOL locked;

+ (MTCell *)cellWithColor:(MTCellColor)aColor direction:(MTCellDirection)aDirection;
- (id)initWithColor:(MTCellColor)aColor direction:(MTCellDirection)aDirection;
- (void)clear;
- (void)rotateRight;
- (void)rotateLeft;
- (void)drawRect:(CGRect)rect;

@end
