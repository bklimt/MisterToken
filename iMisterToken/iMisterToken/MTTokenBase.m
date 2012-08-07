//
//  MTTokenBase.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTTokenBase.h"
#import "MTTokenPiece.h"

@implementation MTTokenBase

- (id)init {
    if (self = [super init]) {
        pieces = [[NSMutableArray alloc] init];
    }
    return self;
}

- (void)dealloc {
    [pieces release];
    [super dealloc];
}

- (void)commit {
    for (MTTokenPiece *piece in pieces) {
        [piece commit];
    }
}

- (BOOL)isValid {
    for (MTTokenPiece *piece in pieces) {
        if (![piece isValid]) {
            return NO;
        }
    }
    return YES;
}

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap {
    return [self move:actuallyDo rows:deltaRow columns:deltaColumn allowWrap:allowWrap force:NO];
}

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap
       force:(BOOL)force {
    if (actuallyDo && !force) {
        if (![self move:NO rows:deltaRow columns:deltaColumn allowWrap:allowWrap force:NO]) {
            return NO;
        }
    }
    for (MTTokenPiece *piece in pieces) {
        if (![piece move:actuallyDo
                    rows:deltaRow
                  columns:deltaColumn
               allowWrap:allowWrap
                   force:force]) {
            return NO;
        }
    }
    return YES;
}

- (void)drawRect:(id)boardRect {
}

- (MTTokenPiece *)piece:(int)index {
    return (MTTokenPiece *)[pieces objectAtIndex:index];
}

@end
