//
//  ColorClockView.m
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import "ColorClockView.h"
#include <math.h>

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
	
	[color set];
	
	// Build a rectangle to draw in
	NSSize screenSize = [self bounds].size;
	rectangle.size = NSMakeSize(screenSize.width, screenSize.height);
	rectangle.origin = NSMakePoint(0, 0);
	[NSBezierPath fillRect:rectangle];
	//[NSBezierPath fillRect:self.frame];
	
	NSMutableParagraphStyle* style = [NSMutableParagraphStyle new];
	[style setAlignment:NSCenterTextAlignment];
	NSDictionary *attr = [NSDictionary dictionaryWithObject:style forKey:NSParagraphStyleAttributeName];
	
	NSString *triplet = [NSString stringWithFormat:@"(%2.5f, %2.5f, %2.5f)", [color redComponent], [color greenComponent], [color blueComponent]];
	[triplet drawInRect:[self bounds] withAttributes:attr];
	
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
