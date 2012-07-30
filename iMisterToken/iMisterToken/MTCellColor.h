//
//  CellColor.h
//  iMisterToken
//
//  Created by Bryan Klimt on 7/28/12.
//  Copyright (c) 2012 __MyCompanyName__. All rights reserved.
//

#import <Foundation/Foundation.h>

typedef enum {
    kMTCellColorBlack,

    kMTCellColorBomb,
    kMTCellColorWild,
    kMTCellColorSkull,

    kMTCellColorCyan,
    kMTCellColorRed,
    kMTCellColorYellow,
    kMTCellColorGreen,
    kMTCellColorWhite,
    kMTCellColorPurple,
    kMTCellColorBlue,
    kMTCellColorOrange,
} MTCellColor;

MTCellColor colorFromString(NSString *name);