//
//  ColorClockView.h
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import <ScreenSaver/ScreenSaver.h>
#import <math.h>


@interface ColorClockView : ScreenSaverView {
	IBOutlet id configSheet;
	IBOutlet id timeFormat24;
	IBOutlet id timeFormat12;
	IBOutlet id showHexValue;
	IBOutlet id fontName;
	@private
}

-(NSColor*) getColorForTime:(NSDate*)date;
-(NSString*) getHexColorForTime:(NSDate*)date;

@end
