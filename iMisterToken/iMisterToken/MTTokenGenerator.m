//
//  MTTokenGenerator.m
//  iMisterToken
//
//  Created by Bryan Klimt on 8/10/12.
//
//

#import "MTTokenGenerator.h"

#import "MTLevel.h"
#import "MTToken.h"

@implementation MTTokenGenerator

@synthesize board;
@synthesize level;
@synthesize random;
@synthesize currentToken;
@synthesize nextToken;

- (id)initWithBoard:(MTBoard *)aBoard
              level:(MTLevel *)aLevel
             random:(NSObject<MTRandom> *)aRandom {
    if (self = [super init]) {
        self.board = aBoard;
        self.level = aLevel;
        self.random = aRandom;
        self.nextToken = nil;
        self.currentToken = nil;
        [self loadNextToken];
    }
    return self;
}

- (void)clearCurrentToken {
    self.currentToken = nil;
}

- (void)loadNextToken {
    self.currentToken = self.nextToken;
    self.nextToken = [level randomTokenForBoard:board random:random];
}

- (void)draw:(CGRect)boardRect {
    [nextToken draw:boardRect];
}

@end
