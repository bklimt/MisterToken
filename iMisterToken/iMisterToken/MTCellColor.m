//
//  CellColor.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import "MTCellColor.h"

MTCellColor colorFromString(NSString *name) {
    if ([name isEqualToString:@"black"]) { return kMTCellColorBlack; }

    if ([name isEqualToString:@"bomb"]) { return kMTCellColorBomb; }
    if ([name isEqualToString:@"wild"]) { return kMTCellColorWild; }
    if ([name isEqualToString:@"skull"]) { return kMTCellColorSkull; }

    if ([name isEqualToString:@"cyan"]) { return kMTCellColorCyan; }
    if ([name isEqualToString:@"red"]) { return kMTCellColorRed; }
    if ([name isEqualToString:@"yellow"]) { return kMTCellColorYellow; }
    if ([name isEqualToString:@"green"]) { return kMTCellColorGreen; }
    if ([name isEqualToString:@"white"]) { return kMTCellColorWhite; }
    if ([name isEqualToString:@"purple"]) { return kMTCellColorPurple; }
    if ([name isEqualToString:@"blue"]) { return kMTCellColorBlue; }
    if ([name isEqualToString:@"orange"]) { return kMTCellColorOrange; }
    
    [NSException raise:NSInternalInconsistencyException
                format:@"Tried to parse invalid color \"%@\".", name];
    return kMTCellColorBlack;
}

