//
//  MTRandom.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol MTRandom <NSObject>

- (int)nextBetweenMin:(int)minValue andMax:(int)maxValue;
- (double)nextDouble;

@end
