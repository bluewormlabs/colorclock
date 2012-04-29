//
//  ColorClockView.h
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import <ScreenSaver/ScreenSaver.h>


@interface ColorClockView : ScreenSaverView {
@private
	NSRect rectangle;
}

-(NSColor*) getColorForTime:(NSDate*)date;
-(NSString*) getHexColorForTime:(NSDate*)date;

@end
