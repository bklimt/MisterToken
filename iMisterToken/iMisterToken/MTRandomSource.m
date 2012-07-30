//
//  MTRandomSource.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTRandomSource.h"

@implementation MTRandomSource

- (int)nextBetweenMin:(int)minValue andMax:(int)maxValue {
    return (arc4random() % (maxValue - minValue)) + minValue;
}

- (double)nextDouble {
    return (double)(arc4random() % RAND_MAX) / RAND_MAX;
}

@end
