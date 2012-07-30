//
//  MTColorDistribution.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

#import "MTCellColor.h"
#import "MTRandom.h"

@interface MTColorDistribution : NSObject {
    NSMutableArray *nodes;
}

// Takes an array of PFObject with color name and weight.
- (id)init:(NSArray *)colorDistribution;

- (MTCellColor)randomColor:(NSObject<MTRandom> *)random;

@end
