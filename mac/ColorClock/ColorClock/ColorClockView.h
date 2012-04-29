//
//  ColorClockView.h
//  ColorClock
//
//  Copyright 2012 Blue Worm Labs, LLC. All rights reserved.
//

#import <ScreenSaver/ScreenSaver.h>
#import <math.h>


@interface ColorClockView : ScreenSaverView {
@private
}

-(NSColor*) getColorForTime:(NSDate*)date;
-(NSString*) getHexColorForTime:(NSDate*)date;

@end
