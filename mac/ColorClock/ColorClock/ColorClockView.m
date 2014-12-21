//
//  ColorClockView.m
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import "ColorClockView.h"

@implementation ColorClockView

static NSString * const ColorClock = @"com.bluewormlabs.ColorClock";

- (id)initWithFrame:(NSRect)frame isPreview:(BOOL)isPreview {
	self = [super initWithFrame:frame isPreview:isPreview];
	if (self) {
		ScreenSaverDefaults * defaults;
		defaults = [ScreenSaverDefaults defaultsForModuleWithName:ColorClock];

		//register default values
		[defaults registerDefaults:[NSDictionary dictionaryWithObjectsAndKeys:
									@"NO", @"timeFormat12",
									@"YES", @"timeFormat24",
									@"Verdana", @"fontName",
									@"NO", @"showHexValue",
									nil]];
		
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
	NSString *timeFormat;
	NSString *hex;
	CGFloat fontSize;
	CGFloat hexFontSize;
	const int BASE_FONT_SIZE = 20;
	NSDateFormatter *dateFormatter = [NSDateFormatter new];
	NSFont *font;
	NSFont *hexFont;
	NSColor *fontColor = [NSColor colorWithCalibratedWhite:1 alpha:0.6];
	ScreenSaverDefaults * defaults;
	defaults = [ScreenSaverDefaults defaultsForModuleWithName:ColorClock];
	hex = [self getHexColorForTime:date];
	
	if([defaults boolForKey:@"timeFormat12"]) {
		timeFormat = @"h:mm:ss";
	} else {
		timeFormat = @"H:mm:ss";
	}
	
	[dateFormatter setDateFormat:timeFormat];
	time = [dateFormatter stringFromDate:date];
	
	[color set];
    
	NSSize screenSize = rect.size;
    
	//set the font size based on screen size
	fontSize = BASE_FONT_SIZE * (screenSize.width / 200);
    
	[NSBezierPath fillRect:rect];	

	font = [NSFont fontWithName:@"Verdana" size:fontSize];
	NSMutableParagraphStyle* style = [NSMutableParagraphStyle new];
	[style setAlignment:NSCenterTextAlignment];

	NSArray *values = [NSArray arrayWithObjects:font, style, fontColor, nil];
	NSArray *keys = [NSArray arrayWithObjects:NSFontAttributeName, NSParagraphStyleAttributeName, NSForegroundColorAttributeName, nil];
    
	NSDictionary *attr = [NSDictionary dictionaryWithObjects:values forKeys:keys];
	NSSize stringSize = [time sizeWithAttributes:attr];
	//not sure why ((screenSize.height / 2) - (stringSize.height / 2)) doesn't center properly
	// but when I divide the string height by 3 it's closer to center
	CGFloat y = ((screenSize.height / 2) - (stringSize.height / 3));
	CGFloat x = ((screenSize.width / 2) - (stringSize.width / 2));

	[time drawAtPoint:NSMakePoint(x, y) withAttributes:attr];
	
	//make the hex font size smaller
	hexFontSize = BASE_FONT_SIZE * (screenSize.width / 2000);
	hexFont = [NSFont fontWithName:@"Verdana" size:hexFontSize];
	NSArray *hexFontValues = [NSArray arrayWithObjects:hexFont, style, fontColor, nil];
	NSDictionary *hexAttr = [NSDictionary dictionaryWithObjects:hexFontValues forKeys:keys];
	NSSize hexStringSize = [hex sizeWithAttributes:hexAttr];
	CGFloat hexX = ((screenSize.width / 2) - (hexStringSize.width / 2));
	[hex drawAtPoint:NSMakePoint(hexX, y) withAttributes:hexAttr];

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
	return YES;
}

- (NSWindow*)configureSheet {
	if (!configSheet)
	{
		if (![NSBundle loadNibNamed:@"ConfigurationPanel" owner:self])
		{
			NSLog( @"Failed to load configure sheet." );
			NSBeep();
		}
	}
	
	return configSheet;
}

- (IBAction)cancelClick:(id)sender {
	[[NSApplication sharedApplication] endSheet:configSheet];
}

- (IBAction) okClick: (id)sender
{
	ScreenSaverDefaults *defaults;
	
	defaults = [ScreenSaverDefaults defaultsForModuleWithName:ColorClock];
	
	// Update our defaults
	[defaults setBool:[timeFormat12 state]
			   forKey:@"timeFormat12"];
	[defaults setBool:[timeFormat24 state]
			   forKey:@"timeFormat24"];
	[defaults setBool:[showHexValue state]
			   forKey:@"showHexValue"];
	
	// Save the settings to disk
	[defaults synchronize];
	
	// Close the sheet
	[[NSApplication sharedApplication] endSheet:configSheet];
}

@end
