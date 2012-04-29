//
//  ColorClockView.m
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import "ColorClockView.h"


@implementation ColorClockView

- (id)initWithFrame:(NSRect)frame isPreview:(BOOL)isPreview {
    self = [super initWithFrame:frame isPreview:isPreview];
    if (self) {
        [self setAnimationTimeInterval:1/30.0];
    }
    return self;
}

- (void)startAnimation {
    [super startAnimation];
}

- (void)stopAnimation {
    [super stopAnimation];
}

- (void)drawRect:(NSRect)rect {
	// Get a color and hex string for the current time
	NSDate *date = [NSDate date];
	NSColor *color = [self getColorForTime:date];
    NSString *time;
    NSDateFormatter *timeFormat = [NSDateFormatter new];
    NSFont *font;
    NSColor *fontColor = [NSColor colorWithCalibratedRed:1.0 green:1.0 blue:1.0 alpha:0.6];
    [timeFormat setDateFormat:@"H:mm:ss"];
    time = [timeFormat stringFromDate:date];
	
	[color set];
	
    NSSize screenSize = rect.size;
	[NSBezierPath fillRect:rect];	

    font = [NSFont fontWithName:@"Verdana" size:200];
	NSMutableParagraphStyle* style = [NSMutableParagraphStyle new];
	[style setAlignment:NSCenterTextAlignment];

    NSArray *values = [NSArray arrayWithObjects:font, style, fontColor, nil];
    NSArray *keys = [NSArray arrayWithObjects:NSFontAttributeName, NSParagraphStyleAttributeName, NSForegroundColorAttributeName, nil];
    
	NSDictionary *attr = [NSDictionary dictionaryWithObjects:values forKeys:keys];
    NSSize stringSize = [time sizeWithAttributes:attr];
    CGFloat y = ((screenSize.height / 2) -  (stringSize.height / 2));
    CGFloat x = ((screenSize.width / 2) - (stringSize.width / 2));
    
	[time drawAtPoint:NSMakePoint(x, y) withAttributes:attr];
	
	[style release];    
}

- (void)animateOneFrame {    
    [self setNeedsDisplay:YES];
    return;
}

-(NSString*) getHexColorForTime:(NSDate*)date {
	NSDateFormatter* formatter = [NSDateFormatter new];
	CGFloat tmpNumber, firstDigit, secondDigit, thirdDigit, fourthDigit, fifthDigit, sixthDigit;
	
	//get milliseconds
	[formatter setDateFormat:@"SSS"];
	float milliseconds = [[formatter stringFromDate:date] floatValue];
	
	//get seconds
	[formatter setDateFormat:@"ss"];
	float seconds = [[formatter stringFromDate:date] floatValue];
	
	//get minutes
	[formatter setDateFormat:@"mm"];
	float minutes = [[formatter stringFromDate:date] floatValue];
	
	//get hours
	[formatter setDateFormat:@"H"];
	float hours = [[formatter stringFromDate:date] floatValue];
    
    firstDigit = round(((hours + (minutes / 60.0)) / 24.0) * 15);
	secondDigit = (hours + (minutes / 60.0)) / 8.0;
	tmpNumber = floor(secondDigit);
	secondDigit = round((secondDigit - tmpNumber) * 15);
    
	thirdDigit = round((((minutes + (seconds / 60.0)) / 60.0)) * 15);
	fourthDigit = (minutes + (seconds / 60.0)) / 10.0;
	tmpNumber = floor(fourthDigit);
	fourthDigit = round((fourthDigit - tmpNumber) * 15);
	
	fifthDigit = round(((seconds + (milliseconds / 1000.0)) / 60.0) * 15);
	sixthDigit = ((seconds + (milliseconds / 1000.0)) / 10.0);
	tmpNumber = floor(sixthDigit);
	sixthDigit = round((sixthDigit - tmpNumber) * 15);
    
	//get our color
	NSString *hex = [NSString stringWithFormat:@"%X%X%X%X%X%X", (int)firstDigit, (int)secondDigit, (int)thirdDigit, (int)fourthDigit, (int)fifthDigit, (int)sixthDigit];
    
	return hex;
}

- (NSColor*)getColorForTime:(NSDate*)date {
	NSString *hex = [self getHexColorForTime:date];
    
    unsigned int tmpRed, tmpGreen, tmpBlue;
    float red, green, blue;
    
    NSScanner *scannerRed = [NSScanner scannerWithString:[hex substringWithRange:NSMakeRange(0, 2)]];
    [scannerRed scanHexInt:&tmpRed];
    red = (float)tmpRed / 255.0;
    
    NSScanner *scannerGreen = [NSScanner scannerWithString:[hex substringWithRange:NSMakeRange(2, 2)]];
    [scannerGreen scanHexInt:&tmpGreen];
    green = (float)tmpGreen / 255.0;
    
    NSScanner *scannerBlue = [NSScanner scannerWithString:[hex substringWithRange:NSMakeRange(4, 2)]];
    [scannerBlue scanHexInt:&tmpBlue];    
    blue = (float)tmpBlue / 255.0;
    
	NSColor *color = [NSColor colorWithCalibratedRed:red green:green blue:blue alpha:1];
	return color;
}

- (BOOL)hasConfigureSheet {
    return NO;
}

- (NSWindow*)configureSheet {
    return nil;
}

@end
