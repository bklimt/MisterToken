//
//  MTPatternParser.m
//  iMisterToken
//
//  Created by Bryan Klimt on 7/29/12.
//
//

#import "MTPatternParser.h"

#import "MTCell.h"
#import "MTCellColor.h"
#import "MTConstants.h"

typedef enum {
    kMTPatternParserValueInt,
    kMTPatternParserValuePattern,
} MTPatternParserValueType;

@interface MTPatternParserValue : NSObject
@property MTPatternParserValueType type;
@property int intValue;
@property (nonatomic, retain) NSArray *patternValue;

+ (MTPatternParserValue *)valueWithInt:(int)value;
+ (MTPatternParserValue *)valueWithPattern:(NSArray *)pattern;
+ (MTPatternParserValue *)valueWithColor:(MTCellColor)color;

@end

@interface MTPatternParser ()
- (MTPatternParserValue *)parseAtom;
@end

@implementation MTPatternParserValue

@synthesize type;
@synthesize intValue;
@synthesize patternValue;

- (id)initWithInt:(int)value {
    if (self = [super init]) {
        self.type = kMTPatternParserValueInt;
        self.intValue = value;
    }
    return self;
}

- (id)initWithPattern:(NSArray *)pattern {
    if (self = [super init]) {
        self.type = kMTPatternParserValuePattern;
        self.patternValue = pattern;
    }
    return self;
}

- (NSString *)description {
    switch (type) {
        case kMTPatternParserValueInt: {
            return [NSString stringWithFormat:@"%d", self.intValue];
        }
        case kMTPatternParserValuePattern: {
            return @"<Pattern>";
        }
    }
    return @"<Unknown>";
}

+ (MTPatternParserValue *)valueWithInt:(int)value {
    return [[[MTPatternParserValue alloc] initWithInt:value] autorelease];
}

+ (MTPatternParserValue *)valueWithPattern:(NSArray *)pattern {
    return [[[MTPatternParserValue alloc] initWithPattern:pattern] autorelease];
}

+ (MTPatternParserValue *)valueWithColor:(MTCellColor)color {
    MTCell *cell = [MTCell cellWithColor:color direction:kMTCellDirectionNone];
    cell.locked = YES;
    return [MTPatternParserValue valueWithPattern:[NSArray arrayWithObject:cell]];
}

- (MTPatternParserValue *)add:(MTPatternParserValue *)other {
    if (self.type == kMTPatternParserValueInt && other.type == kMTPatternParserValueInt) {
        return [MTPatternParserValue valueWithInt:(self.intValue + other.intValue)];
    } else if (self.type == kMTPatternParserValuePattern &&
               other.type == kMTPatternParserValuePattern) {
        NSMutableArray *pattern = [NSMutableArray array];
        for (MTCell *cell in self.patternValue) {
            [pattern addObject:[[cell copy] autorelease]];
        }
        for (MTCell *cell in other.patternValue) {
            [pattern addObject:[[cell copy] autorelease]];
        }
        return [MTPatternParserValue valueWithPattern:pattern];
    } else {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Invalid addition: %@ + %@.", self, other];
        return nil;
    }
}

- (MTPatternParserValue *)subtract:(MTPatternParserValue *)other {
    if (self.type == kMTPatternParserValueInt &&
        other.type == kMTPatternParserValueInt) {
        return [MTPatternParserValue valueWithInt:(self.intValue - other.intValue)];
    } else {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Invalid subtraction: %@ - %@.", self, other];
        return nil;
    }
}

- (MTPatternParserValue *)multiply:(MTPatternParserValue *)other {
    if (self.type == kMTPatternParserValueInt && other.type == kMTPatternParserValueInt) {
        return [MTPatternParserValue valueWithInt:(self.intValue * other.intValue)];
    } else if (self.type == kMTPatternParserValueInt &&
               other.type == kMTPatternParserValuePattern) {
        NSMutableArray *pattern = [NSMutableArray array];
        for (int i = 0; i < self.intValue; ++i) {
            for (MTCell *cell in other.patternValue) {
                [pattern addObject:[[cell copy] autorelease]];
            }
        }
        return [MTPatternParserValue valueWithPattern:pattern];
    } else if (self.type == kMTPatternParserValuePattern &&
               other.type == kMTPatternParserValueInt) {
        NSMutableArray *pattern = [NSMutableArray array];
        for (int i = 0; i < other.intValue; ++i) {
            for (MTCell *cell in self.patternValue) {
                [pattern addObject:[[cell copy] autorelease]];
            }
        }
        return [MTPatternParserValue valueWithPattern:pattern];
    } else {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Invalid multiplication: %@ * %@.", self, other];
        return nil;
    }
}

- (MTPatternParserValue *)divide:(MTPatternParserValue *)other {
    if (self.type == kMTPatternParserValueInt && other.type == kMTPatternParserValueInt) {
        return [MTPatternParserValue valueWithInt:(self.intValue / other.intValue)];
    } else if (self.type == kMTPatternParserValuePattern &&
               other.type == kMTPatternParserValueInt) {
        NSMutableArray *pattern = [NSMutableArray array];
        int newLength = [self.patternValue count] / other.intValue;
        for (int i = 0; i < newLength; ++i) {
            MTCell *cell = [self.patternValue objectAtIndex:i];
            [pattern addObject:[[cell copy] autorelease]];
        }
        return [MTPatternParserValue valueWithPattern:pattern];
    } else {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Invalid division: %@ / %@.", self, other];
        return nil;
    }
}

- (MTPatternParserValue *)shuffle:(NSObject<MTRandom> *)random {
    if (self.type != kMTPatternParserValuePattern) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Tried to shuffle an integer."];
        return nil;
    }
    NSMutableArray *pattern = [NSMutableArray array];
    for (MTCell *cell in self.patternValue) {
        [pattern addObject:[[cell copy] autorelease]];
    }
    for (int i = 0; i < [pattern count]; ++i) {
        int other = [random nextBetweenMin:i andMax:[pattern count]];
        MTCell *temp = [[[pattern objectAtIndex:i] retain] autorelease];
        [pattern replaceObjectAtIndex:i withObject:[pattern objectAtIndex:other]];
        [pattern replaceObjectAtIndex:other withObject:temp];
    }
    return [MTPatternParserValue valueWithPattern:pattern];
}

- (MTPatternParserValue *)setLocked:(BOOL)locked {
    if (self.type == kMTPatternParserValuePattern) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Tried to unlock an int."];
        return nil;
    }
    NSMutableArray *pattern = [NSMutableArray array];
    for (MTCell *cell in self.patternValue) {
        MTCell *newCell = [[cell copy] autorelease];
        newCell.locked = locked;
        [pattern addObject:newCell];
    }
    return [MTPatternParserValue valueWithPattern:pattern];
}

+ (MTPatternParserValue *)fillRows:(MTPatternParserValue *)rows
                       withPattern:(MTPatternParserValue *)pattern
                          andColor:(MTCellColor)color {
    if (rows.type != kMTPatternParserValueInt) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"fillRows:withPattern:andColor expects a first argument of type int."];
        return nil;
    }
    if (pattern.type != kMTPatternParserValuePattern) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"fillRows:withPattern:andColor expects a second argument of type pattern."];
        return nil;
    }
    int total = rows.intValue * kMTColumns;
    int additional = total - pattern.patternValue.count;
    if (additional < 0) {
        NSMutableArray *newPattern = [NSMutableArray array];
        for (MTCell *cell in pattern.patternValue) {
            [newPattern addObject:[[cell copy] autorelease]];
        }
        return [MTPatternParserValue valueWithPattern:newPattern];
    }
    MTPatternParserValue *result = [MTPatternParserValue valueWithColor:color];
    result = [result multiply:[MTPatternParserValue valueWithInt:additional]];
    result = [result add:pattern];
    return result;
}

+ (MTPatternParserValue *)blankRows:(MTPatternParserValue *)rows {
    if (rows.type != kMTPatternParserValueInt) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"blankRows: expects an argument of type int."];
        return nil;
    }
    MTPatternParserValue *result = [MTPatternParserValue valueWithColor:kMTCellColorBlack];
    result = [result multiply:[MTPatternParserValue valueWithInt:(rows.intValue * kMTColumns)]];
    return result;
}

@end

@interface MTPatternParser ()
@property int position;
@property (nonatomic, retain) NSMutableArray *tokens;
@property (nonatomic, retain) NSMutableDictionary *constants;
@property (nonatomic, retain) NSObject<MTRandom> *random;
@end

@implementation MTPatternParser

@synthesize position;
@synthesize tokens;
@synthesize constants;
@synthesize random;

- (void)tokenize:(NSString *)text {
    text = [text lowercaseString];
    int pos = 0;
    while (pos < text.length) {
        if (isdigit([text characterAtIndex:pos])) {
            NSMutableString *word = [NSMutableString string];
            while (pos < text.length && isdigit([text characterAtIndex:pos])) {
                [word appendFormat:@"%c", [text characterAtIndex:pos++]];
            }
            [self.tokens addObject:word];
        } else if (islower([text characterAtIndex:pos])) {
            NSMutableString *word = [NSMutableString string];
            while (pos < text.length && (islower([text characterAtIndex:pos]) ||
                                         [text characterAtIndex:pos] == '_')) {
                [word appendFormat:@"%c", [text characterAtIndex:pos++]];
            }
            [self.tokens addObject:word];
        } else if (isspace([text characterAtIndex:pos])) {
            pos++;
        } else {
            [self.tokens addObject:[NSString stringWithFormat:@"%c",
                                    [text characterAtIndex:pos++]]];
        }
    }
}

- (NSString *)peek {
    return (NSString *)[self.tokens objectAtIndex:self.position];
}

- (NSString *)next {
    return (NSString *)[self.tokens objectAtIndex:self.position++];
}

- (BOOL)end {
    return self.position >= self.tokens.count;
}

- (NSString *)matchTokens:(NSArray *)tokenList {
    for (NSString *token in tokenList) {
        if ([token isEqualToString:[self peek]]) {
            return [self next];
        }
    }
    return nil;
}

- (NSString *)matchToken:(NSString *)token {
    return [self matchTokens:[NSArray arrayWithObject:token]];
}

- (NSNumber *)matchInt {
    NSString *token = [self peek];
    for (int pos = 0; pos < [token length]; ++pos) {
        if (!isdigit([token characterAtIndex:pos])) {
            return nil;
        }
    }
    return [NSNumber numberWithInt:[[self next] intValue]];
}

- (MTPatternParserValue *)parseValue {
    if ([[self peek] isEqualToString:@"!"]) {
        [self next];
    }
    MTPatternParserValue *constant = [self.constants objectForKey:[self peek]];
    if (constant) {
        [self next];
        return constant;
    }
    BOOL negative = ([self matchToken:@"-"] != nil);
    NSNumber *value = [self matchInt];
    if (value == nil) {
        [NSException raise:NSInternalInconsistencyException
                     format:@"Unable to parse integer from \"%@\".", [self peek]];
        return nil;
    }
    if (negative) {
        value = [NSNumber numberWithInt:([value intValue] * -1)];
    }
    return [MTPatternParserValue valueWithInt:[value intValue]];
}

- (MTPatternParserValue *)parseProduct {
    MTPatternParserValue *value = [self parseAtom];
    while (![self end]) {
        NSString *op = [self matchTokens:[NSArray arrayWithObjects:@"*", @"/", nil]];
        if ([op isEqualToString:@"*"]) {
            value = [value multiply:[self parseAtom]];
        } else if ([op isEqualToString:@"/"]) {
            value = [value divide:[self parseAtom]];
        } else {
            return value;
        }
    }
    return value;
    
}

- (MTPatternParserValue *)parseSum {
    MTPatternParserValue *value = [self parseProduct];
    while (![self end]) {
        NSString *op = [self matchTokens:[NSArray arrayWithObjects:@"+", @"-", nil]];
        if ([op isEqualToString:@"+"]) {
            value = [value add:[self parseProduct]];
        } else if ([op isEqualToString:@"-"]) {
            value = [value subtract:[self parseProduct]];
        } else {
            return value;
        }
    }
    return value;
}

- (MTPatternParserValue *)parseAtom {
    if ([self matchToken:@"$"]) {
        return [[self parseAtom] setLocked:NO];
    } else if ([self matchToken:@"shuffle"]) {
        return [[self parseAtom] shuffle:self.random];
    } else if ([self matchToken:@"blank_rows"]) {
        return [MTPatternParserValue blankRows:[self parseAtom]];
    } else if ([self matchToken:@"fill_rows"]) {
        if ([self matchToken:@"("] == nil) {
            [NSException raise:NSInternalInconsistencyException
                        format:@"Expected (."];
        }
        MTPatternParserValue *rows = [self parseSum];
        if ([self matchToken:@","] == nil) {
            [NSException raise:NSInternalInconsistencyException
                        format:@"Expected ,."];
        }
        MTPatternParserValue *pattern = [self parseSum];
        MTCellColor color = kMTCellColorBlack;
        if ([self matchToken:@","] != nil) {
            MTPatternParserValue *colorValue = [self parseValue];
            if (colorValue.type != kMTPatternParserValuePattern ||
                colorValue.patternValue.count != 1) {
                [NSException raise:NSInternalInconsistencyException
                            format:@"Expected a color."];
            }
            color = [(MTCell *)[colorValue.patternValue objectAtIndex:0] color];
        }
        if ([self matchToken:@")"] == nil) {
            [NSException raise:NSInternalInconsistencyException
                        format:@"Expected )."];
        }
        return [MTPatternParserValue fillRows:rows withPattern:pattern andColor:color];
    } else if ([self matchToken:@"("]) {
        MTPatternParserValue *value = [self parseSum];
        if ([self matchToken:@")"] == nil) {
            [NSException raise:NSInternalInconsistencyException
                        format:@"Missing )."];
        }
        return value;
    } else {
        return [self parseValue];
    }
}

- (id)initWithText:(NSString *)text andRandom:(NSObject<MTRandom> *)aRandom {
    if (self = [super init]) {
        self.position = 0;
        self.tokens = [NSMutableArray array];
        self.constants = [NSMutableDictionary dictionary];
        self.random = aRandom;

        [constants setObject:[MTPatternParserValue valueWithInt:kMTRows] forKey:@"rows"];
        [constants setObject:[MTPatternParserValue valueWithInt:kMTColumns] forKey:@"columns"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorBlack] forKey:@"_"];

        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorBlack] forKey:@"black"];

        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorBomb] forKey:@"bomb"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorWild] forKey:@"wild"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorSkull] forKey:@"skull"];

        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorCyan] forKey:@"cyan"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorRed] forKey:@"red"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorYellow] forKey:@"yellow"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorGreen] forKey:@"green"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorWhite] forKey:@"white"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorPurple] forKey:@"purple"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorBlue] forKey:@"blue"];
        [constants setObject:[MTPatternParserValue valueWithColor:kMTCellColorOrange] forKey:@"orange"];
        
        [self tokenize:text];
    }
    return self;
}

+ (NSArray *)parseExpression:(NSString *)text random:(NSObject<MTRandom> *)random {
    MTPatternParser *parser =
        [[[MTPatternParser alloc] initWithText:text andRandom:random] autorelease];
    MTPatternParserValue *value = [parser parseSum];
    if (![parser end]) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Stray characters."];
    }
    if (value.type != kMTPatternParserValuePattern) {
        [NSException raise:NSInternalInconsistencyException
                    format:@"Expected a pattern, but found an integer."];
    }
    return value.patternValue;
}

@end
