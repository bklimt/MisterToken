//
//  MTTokenDistribution.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "MTCellColor.h"
#import "MTRandom.h"
#import "MTToken.h"

@class MTBoard;

@interface MTTokenDistribution : NSObject {
    NSMutableArray *nodes;
}

// Takes an array of PFObject with token name and weight.
- (id)init:(NSArray *)tokenDistribution;

- (NSObject<MTToken> *)randomTokenForBoard:(MTBoard *)board
                                    color1:(MTCellColor)color1
                                    color2:(MTCellColor)color2
                                    random:(NSObject<MTRandom> *)random;

@end
