//
//  MTBoard.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTBoard.h"

#import "MTCell.h"
#import "MTLevel.h"
#import "MTRandom.h"

@implementation MTBoard

- (id)init {
    if (self = [super init]) {
        rowOffset = 0;
        columnOffset = 0;
        
        for (int i = 0; i < kMTRows; ++i) {
            for (int j = 0; j < kMTColumns; ++j) {
                entries[i][j] = [[MTCell alloc] init];
            }
        }
    }
    return self;
}

- (void)dealloc {
    for (int i = 0; i < kMTRows; ++i) {
        for (int j = 0; j < kMTColumns; ++j) {
            [entries[i][j] release];
        }
    }
    [super dealloc];
}

- (MTCell *)cellAtRow:(int)row column:(int)column {
    while (row < 0) {
        row += kMTRows;
    }
    while (column < 0) {
        column += kMTColumns;
    }
    return entries[(row + rowOffset) % kMTRows][(column + columnOffset) % kMTColumns];
}

- (MTCellColor)colorAtRow:(int)row column:(int)column {
    return [self cellAtRow:row column:column].color;
}

- (void)setCell:(MTCell *)cell row:(int)row column:(int)column {
    while (row < 0) {
        row += kMTRows;
    }
    while (column < 0) {
        column += kMTColumns;
    }
    MTCell *target = entries[(row + rowOffset) % kMTRows][(column + columnOffset) % kMTColumns];
    target.color = cell.color;
    target.direction = cell.direction;
}

- (void)addRowForLevel:(MTLevel *)level random:(NSObject<MTRandom> *)random {
    rowOffset = (rowOffset + 1) % kMTRows;
    int bottomRow = ((kMTRows - 1) + rowOffset) % kMTRows;
    for (int i = 0; i < kMTColumns; ++i) {
        entries[bottomRow][i].color = [level randomColor:random];
    }
}

- (void)shiftRight {
    columnOffset = (columnOffset + 1) % kMTColumns;
}

- (void)shiftLeft {
    columnOffset--;
    if (columnOffset < 0) {
        columnOffset = kMTColumns - 1;
    }
}

+ (CGRect)cellPositionOnBoard:(CGRect)boardRect row:(int)row column:(int)column {
    CGRect tileRect;
    tileRect.origin.x = boardRect.origin.x + column * kMTCellSize;
    tileRect.origin.y = boardRect.origin.y + row * kMTCellSize;
    tileRect.size.width = kMTCellSize;
    tileRect.size.height = kMTCellSize;
    return tileRect;
}

- (void)drawRect:(CGRect)boardRect {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            CGRect position = [[self class] cellPositionOnBoard:boardRect row:row column:column];
            [[self cellAtRow:row column:column] drawRect:position];
        }
    }
}

// Makes sure nothing is marked as matched, visited, or loose.
- (void)verifyBoard {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            if (entries[row][column].matched) {
                [NSException raise:NSInternalInconsistencyException
                            format:@"Invalid matched state."];
            }
            if (entries[row][column].visited) {
                [NSException raise:NSInternalInconsistencyException
                            format:@"Invalid visited state."];
            }
            if (entries[row][column].loose) {
                [NSException raise:NSInternalInconsistencyException
                            format:@"Invalid loose state."];
            }
        }
    }
}

- (void)markMatchesBySkull:(NSMutableArray *)colors {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.color == kMTCellColorSkull) {
                cell.matched = YES;
                if (colors.count == 0) {
                    [colors addObject:[NSNumber numberWithInt:kMTCellColorSkull]];
                }
            }
        }
    }
}

- (void)markMatchesByBomb:(NSMutableArray *)colors {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.color == kMTCellColorBomb) {
                MTCellColor match = kMTCellColorBlack;
                if (row > 0) {
                    if ([self cellAtRow:(row-1) column:(column-1)].color != kMTCellColorBlack) {
                        [self cellAtRow:(row-1) column:(column-1)].matched = YES;
                        match = [self cellAtRow:(row-1) column:(column-1)].color;
                    }
                    if ([self cellAtRow:(row-1) column:column].color != kMTCellColorBlack) {
                        [self cellAtRow:(row-1) column:column].matched = YES;
                        match = [self cellAtRow:(row-1) column:column].color;
                    }
                    if ([self cellAtRow:(row-1) column:(column+1)].color != kMTCellColorBlack) {
                        [self cellAtRow:(row-1) column:(column+1)].matched = YES;
                        match = [self cellAtRow:(row-1) column:(column+1)].color;
                    }
                }
                if ([self cellAtRow:row column:(column-1)].color != kMTCellColorBlack) {
                    [self cellAtRow:row column:(column-1)].matched = YES;
                    match = [self cellAtRow:row column:(column-1)].color;
                }
                [[self cellAtRow:row column:column] clear];
                if ([self cellAtRow:row column:(column+1)].color != kMTCellColorBlack) {
                    [self cellAtRow:row column:(column+1)].matched = YES;
                    match = [self cellAtRow:row column:(column+1)].color;
                }
                if (row < kMTRows - 1) {
                    if ([self cellAtRow:(row+1) column:(column-1)].color != kMTCellColorBlack) {
                        [self cellAtRow:(row+1) column:(column-1)].matched = YES;
                        match = [self cellAtRow:(row+1) column:(column-1)].color;
                    }
                    if ([self cellAtRow:(row+1) column:column].color != kMTCellColorBlack) {
                        [self cellAtRow:(row+1) column:column].matched = YES;
                        match = [self cellAtRow:(row+1) column:column].color;
                    }
                    if ([self cellAtRow:(row+1) column:(column+1)].color != kMTCellColorBlack) {
                        [self cellAtRow:(row+1) column:(column+1)].matched = YES;
                        match = [self cellAtRow:(row+1) column:(column+1)].color;
                    }
                }
                if (match != kMTCellColorBlack) {
                    [colors addObject:[NSNumber numberWithInt:kMTCellColorBomb]];
                }
            }
        }
    }
}

- (void)markMatchesByRow:(NSMutableArray *)colors {
    for (int row = 0; row < kMTRows; ++row) {
        BOOL complete = YES;
        BOOL locked = YES;
        for (int column = 0; column < kMTColumns; ++column) {
            if ([self cellAtRow:row column:column].color == kMTCellColorBlack) {
                complete = NO;
            }
            if (![self cellAtRow:row column:column].locked) {
                locked = NO;
            }
        }
        if (complete && !locked) {
            [colors addObject:[NSNumber numberWithInt:[self cellAtRow:row column:0].color]];
            for (int column = 0; column < kMTColumns; ++column) {
                [self cellAtRow:row column:column].matched = YES;
            }
        }
    }
}

- (void)markMatchesByColor:(NSMutableArray *)colors {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            MTCellColor color = cell.color;
            if (color == kMTCellColorBlack) {
                continue;
            }
            // Vertical
            BOOL hadUnlocked = ![self cellAtRow:row column:column];
            if (row + 3 < kMTRows) {
                int otherRow = row + 1;
                while (otherRow < kMTRows &&
                       ([self colorAtRow:otherRow column:column] == color ||
                        [self colorAtRow:otherRow column:column] == kMTCellColorWild ||
                        color == kMTCellColorWild)) {
                    if (color == kMTCellColorWild) {
                        color = [self colorAtRow:otherRow column:column];
                    }
                    if (![self cellAtRow:otherRow column:column].locked) {
                        hadUnlocked = YES;
                    }
                    ++otherRow;
                }
                if ((color != kMTCellColorBlack) && ((otherRow - row) >= 4) && hadUnlocked) {
                    BOOL redundant = YES;
                    while (--otherRow >= row) {
                        if (![self cellAtRow:otherRow column:column].matched) {
                            [self cellAtRow:otherRow column:column].matched = YES;
                            redundant = NO;
                        }
                    }
                    if (!redundant) {
                        [colors addObject:[NSNumber numberWithInt:color]];
                    }
                }
            }
            // Horizontal
            {
                color = [self colorAtRow:row column:column];
                hadUnlocked = ![self cellAtRow:row column:column].locked;
                int otherColumn = column + 1;
                while (otherColumn < kMTColumns &&
                       ([self colorAtRow:row column:otherColumn] == color ||
                        [self colorAtRow:row column:otherColumn] == kMTCellColorWild ||
                        color == kMTCellColorWild)) {
                    if (color == kMTCellColorWild) {
                        color = [self colorAtRow:row column:otherColumn];
                    }
                    if (![self cellAtRow:row column:otherColumn].locked) {
                        hadUnlocked = YES;
                    }
                    ++otherColumn;
                }
                if ((color != kMTCellColorBlack) && ((otherColumn - column) >= 4) && hadUnlocked) {
                    BOOL redundant = YES;
                    while (--otherColumn >= column) {
                        if (![self cellAtRow:row column:otherColumn].matched) {
                            [self cellAtRow:row column:otherColumn].matched = YES;
                            redundant = NO;
                        }
                    }
                    if (!redundant) {
                        [colors addObject:[NSNumber numberWithInt:color]];
                    }
                }
            }
        }
    }
}

- (void)markMatches:(NSMutableArray *)colors {
    [self verifyBoard];
    [self markMatchesBySkull:colors];
    [self markMatchesByBomb:colors];
    [self markMatchesByRow:colors];
    [self markMatchesByColor:colors];
}

- (void)clearMatches {
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.matched) {
                if (cell.direction & kMTCellDirectionUp) {
                    [self cellAtRow:(row - 1) column:column].direction &= ~kMTCellDirectionDown;
                }
                if (cell.direction & kMTCellDirectionDown) {
                    [self cellAtRow:(row + 1) column:column].direction &= ~kMTCellDirectionUp;
                }
                if (cell.direction & kMTCellDirectionRight) {
                    [self cellAtRow:row column:(column + 1)].direction &= ~kMTCellDirectionLeft;
                }
                if (cell.direction & kMTCellDirectionLeft) {
                    [self cellAtRow:row column:(column - 1)].direction &= ~kMTCellDirectionRight;
                }
                [cell clear];
            }
        }
    }
    [self verifyBoard];
}

/**
 * Returns YES if the piece can be moved.
 */
- (BOOL)checkLooseAtRow:(int)row column:(int)column root:(BOOL)root {
    if (row >= kMTRows) {
        return NO;
    }
    MTCell *cell = [self cellAtRow:row column:column];
    if (cell.locked) {
        return NO;
    }
    if (cell.color == kMTCellColorBlack) {
        return YES;
    }
    if (cell.loose) {
        return YES;
    }
    if (cell.visited) {
        return YES;
    }
    cell.visited = YES;
    @try {
        if (![self checkLooseAtRow:(row + 1) column:column root:NO]) {
            return NO;
        }
        if ((cell.direction & kMTCellDirectionUp) &&
            ![self checkLooseAtRow:(row - 1) column:column root:NO]) {
            return NO;
        }
        if ((cell.direction & kMTCellDirectionRight) &&
            ![self checkLooseAtRow:row column:(column + 1) root:NO]) {
            return NO;
        }
        if ((cell.direction & kMTCellDirectionLeft) &&
            ![self checkLooseAtRow:row column:(column - 1) root:NO]) {
            return NO;
        }
    } @finally {
        cell.visited = NO;
    }
    if (root) {
        cell.loose = YES;
    }
    return YES;
}

/**
 * Returns YES if anything is free to fall.
 */
- (BOOL)markLoose {
    [self verifyBoard];
    BOOL any = NO;
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            [self checkLooseAtRow:row column:column root:YES];
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.loose) {
                any = YES;
            }
        }
    }
    return any;
}

- (BOOL)moveLoose {
    BOOL anythingFell = NO;
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.loose) {
                MTCell *below = [self cellAtRow:(row + 1) column:column];
                NSAssert(below.color != kMTCellColorBlack, @"Bad gravity logic.");
                [self setCell:cell row:(row + 1) column:column];
                [cell clear];
            }
        }
    }
    [self verifyBoard];
    return anythingFell;
}

- (int)lockedCount {
    int count = 0;
    for (int row = 0; row < kMTRows; ++row) {
        for (int column = 0; column < kMTColumns; ++column) {
            MTCell *cell = [self cellAtRow:row column:column];
            if (cell.locked && !cell.matched) {
                ++count;
            }
        }
    }
    return count;
}

- (void)setup:(MTLevel *)level random:(NSObject<MTRandom> *)random {
    [level setupBoard:self random:random];
    NSMutableArray *colors = [NSMutableArray array];
    [self markMatches:colors];
    [self clearMatches];
}

@end
