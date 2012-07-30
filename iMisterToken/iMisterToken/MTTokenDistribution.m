//
//  MTTokenDistribution.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTTokenDistribution.h"

#import "Parse/Parse.h"

#import "MTCellColor.h"
#import "MTRandom.h"
#import "MTToken.h"
#import "MTToken2.h"

@class MTBoard;

typedef NSObject<MTToken> *(^MTTokenFactory)(MTBoard *board,
                                             MTCellColor color1,
                                             MTCellColor color2);

@interface MTTokenDistributionNode : NSObject
@property double sum;
@property (nonatomic, copy) MTTokenFactory factory;
@end

@implementation MTTokenDistributionNode
@synthesize sum;
@synthesize factory;
@end

MTTokenFactory tokenFactoryFromString(NSString *name) {
    if ([name isEqualToString:@"2"]) {
        return ^(MTBoard *board, MTCellColor color1, MTCellColor color2) {
            return [MTToken2 tokenWithBoard:board row:0 column:0 color1:color1 color2:color2];
        };
    }
    [NSException raise:NSInternalInconsistencyException
                format:@"Unable to parse token named \"%@\".", name];
    return nil;
}

@implementation MTTokenDistribution

- (MTTokenDistributionNode *)lastNode {
    return (MTTokenDistributionNode *)[nodes lastObject];
}

- (void)add:(MTTokenFactory)factory withWeight:(double)weight {
    MTTokenDistributionNode *node = [[[MTTokenDistributionNode alloc] init] autorelease];
    if ([nodes count] == 0) {
        node.sum = weight;
    } else {
        node.sum = [self lastNode].sum + weight;
    }
    node.factory = factory;
    [nodes addObject:node];
}

// Takes an array of PFObject with token name and weight.
- (id)init:(NSArray *)tokenDistribution {
    if (self = [super init]) {
        nodes = [[NSMutableArray alloc] init];
        for (PFObject *tokenNameAndWeight in tokenDistribution) {
            NSString *name = [tokenNameAndWeight objectForKey:@"token"];
            double weight = [[tokenNameAndWeight objectForKey:@"weight"] doubleValue];
            MTTokenFactory factory = tokenFactoryFromString(name);
            [self add:factory withWeight:weight];
        }
    }
    return self;
}

- (void)dealloc {
    [nodes release];
    [super dealloc];
}

- (NSObject<MTToken> *)randomTokenForBoard:(MTBoard *)board
                                    color1:(MTCellColor)color1
                                    color2:(MTCellColor)color2
                                    random:(NSObject<MTRandom> *)random {
    double number = [random nextDouble];
    for (MTTokenDistributionNode *node in nodes) {
        if (number < node.sum) {
            return node.factory(board, color1, color2);
        }
    }
    return [self lastNode].factory(board, color1, color2);
}

@end
