//
//  MTToken2.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "MTCellColor.h"
#import "MTToken.h"
#import "MTTokenBase.h"

@class MTBoard;

@interface MTToken2 : MTTokenBase <MTToken> {
    // The rotation of the piece:
    // 0 - AB
    // 1 - A
    //     B
    // 2 - BA
    // 3 - B
    //     A
    int orientation;
}

+ (MTToken2 *)tokenWithBoard:(MTBoard *)board
                         row:(int)row
                      column:(int)column
                      color1:(MTCellColor)color1
                      color2:(MTCellColor)color2;

@end
