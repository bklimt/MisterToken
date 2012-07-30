//
//  MTPatternParser.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/29/12.
//
//

#import <Foundation/Foundation.h>

#import "MTRandom.h"

@interface MTPatternParser : NSObject

+ (NSArray *)parseExpression:(NSString *)text random:(NSObject<MTRandom> *)random;

@end
