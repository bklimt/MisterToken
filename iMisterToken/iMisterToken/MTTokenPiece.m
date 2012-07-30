//
//  MTTokenPiece.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTTokenPiece.h"

#import "MTBoard.h"
#import "MTCell.h"

@implementation MTTokenPiece

@synthesize row;
@synthesize column;
@synthesize cell;

+ (MTTokenPiece *)pieceWithBoard:(MTBoard *)aBoard
                             row:(int)aRow
                          column:(int)aColumn
                            cell:(MTCell *)aCell {
    return [[[MTTokenPiece alloc] initWithBoard:aBoard
                                            row:aRow
                                         column:aColumn
                                           cell:aCell] autorelease];
}

- (id)initWithBoard:(MTBoard *)aBoard row:(int)aRow column:(int)aColumn cell:(MTCell *)aCell {
    if (self = [super init]) {
        board = aBoard;
        row = aRow;
        column = aColumn;
        cell = [aCell retain];
    }
    return self;
}

- (BOOL)isValid {
    return [board colorAtRow:(int)row column:(int)column] == 0;
}

- (void)commit {
    [board setCell:cell row:(int)row column:(int)column];
    if (cell.color == kMTCellColorSkull) {
        [board cellAtRow:(int)row column:(int)column].matched = YES;
    }
}

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap {
    return [self move:actuallyDo rows:deltaRow columns:deltaRow allowWrap:allowWrap force:NO];
}

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap
       force:(BOOL)force {
    if (!force) {
        if (row + deltaRow < 0) {
            return NO;
        }
        if (row + deltaRow >= kMTRows) {
            return NO;
        }
        if (!allowWrap && (((column + deltaColumn) < 0) ||
                           ((column + deltaColumn >= kMTColumns)))) {
            return NO;
        }
        MTCellColor color = [board colorAtRow:(row + deltaRow) column:(column + deltaColumn)];
        if (color != kMTCellColorBlack) {
            if (cell.color != kMTCellColorSkull) {
                return NO;
            }
        }
    }
    if (actuallyDo) {
        row += deltaRow;
        column += deltaColumn;
        if (row < 0) {
            row += kMTRows;
        }
        if (column < 0) {
            column += kMTColumns;
        }
        row %= kMTRows;
        column %= kMTColumns;
        if (cell.color == kMTCellColorSkull) {
            if ([board cellAtRow:row column:column].color != kMTCellColorBlack) {
                [board cellAtRow:row column:column].color = kMTCellColorSkull;
            }
        }
    }
    return YES;
}

- (void)rotateRight {
    [cell rotateRight];
}

- (void)rotateLeft {
    [cell rotateLeft];
}

@end
