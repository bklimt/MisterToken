//
//  MTTokenPiece.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@class MTBoard;
@class MTCell;

@interface MTTokenPiece : NSObject {
    MTBoard *board;
}

@property (readonly) int row;
@property (readonly) int column;
@property (retain, readonly) MTCell *cell;

+ (MTTokenPiece *)pieceWithBoard:(MTBoard *)aBoard
                             row:(int)aRow
                          column:(int)aColumn
                            cell:(MTCell *)aCell;

- (id)initWithBoard:(MTBoard *)aBoard row:(int)aRow column:(int)aColumn cell:(MTCell *)aCell;

- (BOOL)isValid;

- (void)commit;

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap;

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap
       force:(BOOL)force;

- (void)rotateRight;

- (void)rotateLeft;

@end