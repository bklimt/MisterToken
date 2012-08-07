//
//  MTTokenBase.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@class MTTokenPiece;

@interface MTTokenBase : NSObject {
    NSMutableArray *pieces;
}

- (void)commit;

- (BOOL)isValid;

- (BOOL)move:(BOOL)dryRun
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap;

- (BOOL)move:(BOOL)dryRun
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap
       force:(BOOL)force;

- (void)drawRect:(id)boardRect;

- (MTTokenPiece *)piece:(int)index;

@end
