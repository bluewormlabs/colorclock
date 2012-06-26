Color Clock
===========

About
-----
We're fans of [The Colour Clock][TCC]. Unfortunately, it's written in Flash 
and run by small screensaver wrappers. This leads to bugs (particularly with 
multiple displays) and performance issues.

Being software developers, we decided to just write our own.

Windows
-------
There are currently no binaries available.

The source code (under the `win` directory) should build and run on any 
Windows system with .NET 2.0 or newer. Note that the project and solution files 
are in Visual Studio 2010 format.

After building the project, copy the resulting `.exe` to your System32 folder
and rename it; the extension must be `.scr`. You should now be able to select
the screensaver.

The Windows screensaver was built following Frank McCown's 
[Creating a Screen Saver with C#][WinTut].

Mac
---
There's a binary available for 10.7 Lion in the [Downloads][Download] section.
Double click on the ColorClock.saver file to install the screensaver.

If you need to run the Color Clock in another version of OS X you can open the
xcode project file located in mac/ColorClock/ColorClock.xcodeproj and compile
your own version of the binary. 

[TCC]: http://www.thecolourclock.co.uk/
[WinTut]: http://www.harding.edu/fmccown/screensaver/screensaver.html
[Download]: https://github.com/bluewormlabs/colorclock/downloads
