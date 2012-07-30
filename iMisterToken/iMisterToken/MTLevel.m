//
//  MTLevel.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTLevel.h"

#import "Parse/Parse.h"

#import "MTBoard.h"
#import "MTCell.h"
#import "MTColorDistribution.h"
#import "MTPatternParser.h"
#import "MTToken.h"
#import "MTTokenDistribution.h"

@interface MTLevel ()
@property (nonatomic, retain, readwrite) NSString *name;
@property (nonatomic, retain, readwrite) NSString *help;
@end

@implementation MTLevel

@synthesize name;
@synthesize help;
@synthesize wrap;

// Initializes a level from a PFObject with:
//   name
//   help
//   pattern
//   wrap
//
// And one-to-many relationships with
//   token distribution
//   color distribution
//
// And many-to-many relationships with
//   require
//
- (id)init:(PFObject *)level {
    if (self = [super init]) {
        self.name = [level objectForKey:@"name"];
        self.help = [level objectForKey:@"level"];
        pattern = [[level objectForKey:@"pattern"] retain];
        self.wrap = [[level objectForKey:@"wrap"] boolValue];

        // TODO: Make sure this isn't called in the main thread.
        
        require = [[[level relationforKey:@"require"] query] findObjects];

        PFQuery *tokenQuery = [PFQuery queryWithClassName:@"TokenDistribution"];
        [tokenQuery setCachePolicy:kPFCachePolicyNetworkElseCache];
        [tokenQuery whereKey:@"level" equalTo:level];
        tokens = [[MTTokenDistribution alloc] init:[tokenQuery findObjects]];

        PFQuery *colorQuery = [PFQuery queryWithClassName:@"ColorDistribution"];
        [colorQuery setCachePolicy:kPFCachePolicyNetworkElseCache];
        [colorQuery whereKey:@"level" equalTo:level];
        colors = [[MTColorDistribution alloc] init:[colorQuery findObjects]];
    }
    return self;
}

- (void)dealloc {
    [pattern release];
    [require release];
    [tokens release];
    [colors release];
    [super dealloc];
}

- (void)setupBoard:(MTBoard *)board random:(NSObject<MTRandom> *)random {
    NSArray *cells = [MTPatternParser parseExpression:pattern random:random];
    int start = kMTRows + kMTColumns - [cells count];
    if (start < 0) {
        start = 0;
    }
    int offset = 0;
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            [[board cellAtRow:row column:column] clear];
            if (offset >= start) {
                MTCellColor color = ((MTCell *)[cells objectAtIndex:(offset - start)]).color;
                BOOL locked = ((MTCell *)[cells objectAtIndex:(offset - start)]).locked;
                if (color != kMTCellColorBlack) {
                    [board cellAtRow:row column:column].color = color;
                    [board cellAtRow:row column:column].locked = locked;
                }
            }
            ++offset;
        }
    }
}

- (MTCellColor)randomColor:(NSObject<MTRandom>*)random {
    return [colors randomColor:random];
}

- (NSObject<MTToken> *)randomTokenForBoard:(MTBoard *)board random:(NSObject<MTRandom>*)random {
    MTCellColor color1 = [self randomColor:random];
    MTCellColor color2 = [self randomColor:random];
    return [tokens randomTokenForBoard:board color1:color1 color2:color2 random:random];
}

@end
