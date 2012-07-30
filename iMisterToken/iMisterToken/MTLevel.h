//
//  MTLevel.h
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
@class MTColorDistribution;
@class MTTokenDistribution;

@interface MTLevel : NSObject {
    NSArray *require;
    MTTokenDistribution *tokens;
    MTColorDistribution *colors;
    NSString *pattern;
}

@property (nonatomic, retain, readonly) NSString *name;
@property (nonatomic, retain, readonly) NSString *help;
@property BOOL wrap;

- (void)setupBoard:(MTBoard *)board random:(NSObject<MTRandom> *)random;
- (MTCellColor)randomColor:(NSObject<MTRandom>*)random;
- (NSObject<MTToken> *)randomTokenForBoard:(MTBoard *)board random:(NSObject<MTRandom>*)random;

@end
