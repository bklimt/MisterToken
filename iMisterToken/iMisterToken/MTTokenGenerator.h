//
//  MTTokenGenerator.h
//  iMisterToken
//
//  Created by Bryan Klimt on 8/10/12.
//
//

#import <Foundation/Foundation.h>

#import "MTRandom.h"
#import "MTToken.h"

@class MTBoard;
@class MTLevel;

@interface MTTokenGenerator : NSObject

@property (nonatomic, retain) MTBoard *board;
@property (nonatomic, retain) MTLevel *level;
@property (nonatomic, retain) NSObject<MTRandom> *random;
@property (nonatomic, retain) NSObject<MTToken> *currentToken;
@property (nonatomic, retain) NSObject<MTToken> *nextToken;

@end
