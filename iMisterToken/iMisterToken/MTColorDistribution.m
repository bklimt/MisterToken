//
//  MTColorDistribution.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTColorDistribution.h"

#import <Parse/Parse.h>

@interface MTColorDistributionNode : NSObject
@property double sum;
@property MTCellColor color;
@end

@implementation MTColorDistributionNode
@synthesize sum;
@synthesize color;
@end

@implementation MTColorDistribution

- (MTColorDistributionNode *)lastNode {
    return (MTColorDistributionNode *)[nodes lastObject];
}

- (void)addColor:(MTCellColor)color withWeight:(double)weight {
    MTColorDistributionNode *node = [[[MTColorDistributionNode alloc] init] autorelease];
    if ([nodes count] == 0) {
        node.sum = weight;
    } else {
        node.sum = [self lastNode].sum + weight;
    }
    node.color = color;
    [nodes addObject:node];
}

- (id)init:(NSArray *)colorDistribution {
    if (self = [super init]) {
        nodes = [[NSMutableArray alloc] init];

        for (PFObject *colorAndWeight in colorDistribution) {
            NSString *color = [colorAndWeight objectForKey:@"color"];
            double weight = [[colorAndWeight objectForKey:@"weight"] doubleValue];
            [self addColor:colorFromString(color) withWeight:weight];
        }
    }
    return self;
}

- (void)dealloc {
    [nodes release];
    [super dealloc];
}

- (MTCellColor)randomColor:(NSObject<MTRandom> *)random {
    double number = [random nextDouble] * [self lastNode].sum;
    for (MTColorDistributionNode *node in nodes) {
        if (number < node.sum) {
            return node.color;
        }
    }
    return [self lastNode].color;
}

@end
