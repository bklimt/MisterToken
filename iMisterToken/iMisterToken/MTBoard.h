//
//  MTBoard.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "MTConstants.h"
#import "MTCellColor.h"

@class MTCell;

@interface MTBoard : NSObject {
    MTCell *entries[kMTRows][kMTColumns];
    int columnOffset;
    int rowOffset;
}

- (MTCell *)cellAtRow:(int)row column:(int)column;

// TODO: Get rid of this.
- (MTCellColor)colorAtRow:(int)row column:(int)column;

// TODO: Clarify what this does.
- (void)setCell:(MTCell *)cell row:(int)row column:(int)column;

@end
