//
//  MTToken.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

@protocol MTToken <NSObject>

- (void)commit;

- (BOOL)isValid;

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap;

- (BOOL)move:(BOOL)actuallyDo
        rows:(int)deltaRow
     columns:(int)deltaColumn
   allowWrap:(BOOL)allowWrap
       force:(BOOL)force;

- (BOOL)rotateRight:(BOOL)actuallyDo
          allowWrap:(BOOL)allowWrap;

- (BOOL)rotateLeft:(BOOL)actuallyDo
         allowWrap:(BOOL)allowWrap;

- (void)drawRect:(CGRect)boardRect;

@end
