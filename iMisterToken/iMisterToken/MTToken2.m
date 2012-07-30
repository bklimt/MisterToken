//
//  MTToken2.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTToken2.h"

#import "MTCell.h"
#import "MTTokenPiece.h"

@implementation MTToken2

- (id)initWithBoard:(MTBoard *)board
                row:(int)row
             column:(int)column
             color1:(MTCellColor)color1
             color2:(MTCellColor)color2 {
    if (self = [super init]) {
        MTCell *cell1 = [MTCell cellWithColor:color1 direction:kMTCellDirectionRight];
        MTCell *cell2 = [MTCell cellWithColor:color2 direction:kMTCellDirectionLeft];
        [pieces addObject:[MTTokenPiece pieceWithBoard:board row:row column:column cell:cell1]];
        [pieces addObject:[MTTokenPiece pieceWithBoard:board row:row column:column+1 cell:cell2]];
        orientation = 0;
    }
    return self;
}

+ (MTToken2 *)tokenWithBoard:(MTBoard *)board
                         row:(int)row
                      column:(int)column
                      color1:(MTCellColor)color1
                      color2:(MTCellColor)color2 {
    return [[[MTToken2 alloc] initWithBoard:board
                                       row:row
                                    column:column
                                    color1:color1
                                     color2:color2] autorelease];
}

- (BOOL)rotateRight:(BOOL)actuallyDo
          allowWrap:(BOOL)allowWrap {
    if (actuallyDo) {
        if (![self rotateRight:NO allowWrap:allowWrap]) {
            return NO;
        }
    }
    switch (orientation) {
        case 0: {
            if (![[self piece:1] move:actuallyDo rows:1 columns:-1 allowWrap:allowWrap]) {
                return NO;
            }
            break;
        }
        case 1: {
            if ([[self piece:1] move:NO rows:-1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:0 columns:1 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:-1 columns:0 allowWrap:allowWrap];
            } else if ([[self piece:0] move:NO rows:1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:1 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:1] move:NO rows:-1 columns:-1 allowWrap:allowWrap]) {
                [[self piece:1] move:actuallyDo rows:-1 columns:-1 allowWrap:allowWrap];
            } else {
                return NO;
            }
            break;
        }
        case 2: {
            if (![[self piece:0] move:actuallyDo rows:1 columns:-1 allowWrap:allowWrap]) {
                return NO;
            }
            break;
        }
        case 3: {
            if ([[self piece:0] move:NO rows:-1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:-1 columns:0 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:0 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:1] move:NO rows:1 columns:1 allowWrap:allowWrap]) {
                [[self piece:1] move:actuallyDo rows:1 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:0] move:NO rows:-1 columns:-1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:-1 columns:-1 allowWrap:allowWrap];
            } else {
                return NO;
            }
            break;
        }
    }
    if (actuallyDo) {
        [[self piece:0] rotateRight];
        [[self piece:1] rotateRight];
        orientation = (orientation + 1) % 4;
    }
    return YES;
}

- (BOOL)rotateLeft:(BOOL)actuallyDo
         allowWrap:(BOOL)allowWrap {
    if (actuallyDo) {
        if (![self rotateLeft:NO allowWrap:allowWrap]) {
            return NO;
        }
    }
    switch (orientation) {
        case 0: {
            if (![[self piece:0] move:NO rows:1 columns:0 allowWrap:allowWrap] ||
                ![[self piece:1] move:NO rows:0 columns:-1 allowWrap:allowWrap]) {
                return NO;
            }
            [[self piece:0] move:actuallyDo rows:1 columns:0 allowWrap:allowWrap];
            [[self piece:1] move:actuallyDo rows:0 columns:-1 allowWrap:allowWrap];
            break;
        }
        case 1: {
            if ([[self piece:1] move:NO rows:-1 columns:1 allowWrap:allowWrap]) {
                [[self piece:1] move:actuallyDo rows:-1 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:0] move:NO rows:1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:1 columns:0 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:0 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:1] move:NO rows:-1 columns:-1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:0 columns:-1 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:-1 columns:0 allowWrap:allowWrap];
            } else {
                return NO;
            }
            break;
        }
        case 2: {
            if (![[self piece:0] move:NO rows:0 columns:-1 allowWrap:allowWrap] ||
                ![[self piece:1] move:NO rows:1 columns:0 allowWrap:allowWrap]) {
                return NO;
            }
            [[self piece:0] move:actuallyDo rows:0 columns:-1 allowWrap:allowWrap];
            [[self piece:1] move:actuallyDo rows:1 columns:0 allowWrap:allowWrap];
            break;
        }
        case 3: {
            // here's where i stopped
            if ([[self piece:0] move:NO rows:-1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:-1 columns:1 allowWrap:allowWrap];
            } else if ([[self piece:1] move:NO rows:1 columns:1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:0 columns:1 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:1 columns:0 allowWrap:allowWrap];
            } else if ([[self piece:0] move:NO rows:-1 columns:-1 allowWrap:allowWrap]) {
                [[self piece:0] move:actuallyDo rows:-1 columns:0 allowWrap:allowWrap];
                [[self piece:1] move:actuallyDo rows:0 columns:-1 allowWrap:allowWrap];
            } else {
                return NO;
            }
            break;
        }
    }
    if (actuallyDo) {
        [[self piece:0] rotateLeft];
        [[self piece:1] rotateLeft];
        if (--orientation < 0) {
            orientation = 3;
        }
    }
    return YES;
}

@end
