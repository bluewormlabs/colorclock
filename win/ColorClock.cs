using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ColorClock
{
	/// <summary>
	/// A color clock screensaver window
	/// </summary>
	public partial class ColorClock : Form
	{
		#region API Methods
		[DllImport("user32.dll")]
		static extern IntPtr SetParent(IntPtr hChildWindow, IntPtr hNewParentWindow);

		[DllImport("user32.dll")]
		static extern int SetWindowLong(IntPtr hWindow, int index, IntPtr newLong);
		
		[DllImport("user32.dll", SetLastError = true)]
		static extern int GetWindowLong(IntPtr hWindow, int index);

		[DllImport("user32.dll")]
		static extern bool GetClientRect(IntPtr hWindow, out Rectangle rect);
		#endregion API Methods

		#region Data Members
		/// <summary>
		/// The last known location of the mouse; used to determine if
		/// the user has moved the mouse and the screensaver should quit
		/// </summary>
		private Point lastMouseLocation;

		/// <summary>
		/// The font to use on the labels
		/// </summary>
		private Font font = new Font("Verdana", 100f);

		/// <summary>
		/// Whether or not we're a preview window
		/// </summary>
		private bool previewMode = false;

		#region Colors
		/// <summary>
		/// The alpha level for text fields
		/// </summary>
		private static readonly int Alpha = 60;

		/// <summary>
		/// Black
		/// </summary>
		private static readonly Color Black = Color.Black;

		/// <summary>
		/// White at 60% opacity
		/// </summary>
		private static readonly Color White = Color.FromArgb(ColorClock.Alpha, Color.White);

		/// <summary>
		/// White Smoke at 60% opacity
		/// </summary>
		private static readonly Color WhiteSmoke = Color.FromArgb(ColorClock.Alpha, Color.WhiteSmoke);
		#endregion Colors
		#endregion Data Members

		#region Constructors
		/// <summary>
		/// Creates a new ColorClock window
		/// </summary>
		/// <param name="bounds">the screen bounds to display on</param>
		public ColorClock(Rectangle bounds)
		{
			InitializeComponent();
			this.SetBlack();
			this.Bounds = bounds;

			// Set the fonts on the two labels
			this.timeLabel.Font = new Font(this.font.FontFamily, (float)this.DetermineFontSize());
			this.hexLabel.Font = new Font(this.font.FontFamily, (float)(this.DetermineFontSize() / 10.0));

			// Set the correct colors and display initial values
			this.SetColors();
			this.UpdateTime();
		}

		/// <summary>
		/// Creates a new ColorClock window
		/// </summary>
		/// <param name="hWindow">a handle to the parent window (typically the Screensaver settings mini-monitor)</param>
		public ColorClock(IntPtr hWindow)
		{
			InitializeComponent();
			this.SetBlack();

			// Make the preview window our parent
			SetParent(this.Handle, hWindow);

			// Make it a child window; GetWindowLong(this.Handle, GWL_STYLE) | WS_CHILD
			SetWindowLong(this.Handle, -16, new IntPtr(GetWindowLong(this.Handle, -16) | 0x40000000));

			// Move the window inside the parent window
			Rectangle parent;
			GetClientRect(hWindow, out parent);
			this.Size = parent.Size;
			this.Location = new Point(0, 0);

			// Set the fonts on the two labels
			this.timeLabel.Font = new Font(this.font.FontFamily, 6);
			this.hexLabel.Font = new Font(this.font.FontFamily, 1);

			// Mark that we're a preview
			this.previewMode = true;

			// Set the correct colors and display initial values
			this.SetColors();
			this.UpdateTime();
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Handles the window loading
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">arguments for the event</param>
		private void ColorClock_Load(object sender, EventArgs e)
		{
			Cursor.Hide();
			this.TopMost = true;
			this.timer.Start();
		}
		
		/// <summary>
		/// Closes the screensaver when the mouse is moved
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">arguments for the event</param>
		private void ColorClock_MouseMove(object sender, MouseEventArgs e)
		{
			int threshold = 1;
			if (!this.lastMouseLocation.IsEmpty)
			{
				// Quit if the user moved
				if (Math.Abs(this.lastMouseLocation.X - e.X) > threshold || Math.Abs(this.lastMouseLocation.Y - e.Y) > threshold)
				{
					Application.Exit();
				}
			}

			// Save the new location
			this.lastMouseLocation = e.Location;
		}

		/// <summary>
		/// Closes the screensaver when the mouse is clicked
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">arguments for the event</param>
		private void ColorClock_MouseClick(object sender, MouseEventArgs e)
		{
			Application.Exit();
		}
		
		/// <summary>
		/// Closes the screensaver when a key is pressed
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">arguments for the event</param>
		private void ColorClock_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!this.previewMode)
			{
				Application.Exit();
			}
		}

		/// <summary>
		/// Updates the time/color every second
		/// </summary>
		/// <param name="sender">the object sending the event</param>
		/// <param name="e">arguments for the event</param>
		private void timer_Tick(object sender, EventArgs e)
		{
			this.UpdateTime();
		}
		#endregion Event Handlers

		#region Private Methods
		/// <summary>
		/// Sets everything to black (for the moment before the timer fires)
		/// </summary>
		private void SetBlack()
		{
			this.BackColor = ColorClock.Black;
			this.timeLabel.ForeColor = ColorClock.Black;
			this.hexLabel.ForeColor = ColorClock.Black;
		}

		/// <summary>
		/// Sets the label text colors
		/// </summary>
		private void SetColors()
		{
			this.timeLabel.ForeColor = ColorClock.White;
			this.hexLabel.ForeColor = ColorClock.WhiteSmoke;
		}

		/// <summary>
		/// Updates the time
		/// 
		/// 1. Gets the current time
		/// 2. Gets a hex value for the current time
		/// 3. Updates the time label
		/// 4. Updates the hex label
		/// 5. Updates the background color
		/// </summary>
		private void UpdateTime()
		{
			// Get the current time
			DateTime now = DateTime.Now;

			// Determine the new color
			string hex = "#" + this.GetColourClockHex(now);
			Color color = ColorTranslator.FromHtml(hex);

			// Update everything
			this.timeLabel.Text = now.ToString("T");
			this.hexLabel.Text = hex;
			this.BackColor = color;
		}

		/// <summary>
		/// Determines the font size to use
		/// </summary>
		/// <returns>the font size (em)</returns>
		private double DetermineFontSize()
		{
			return this.DetermineFontSize(20.0);
		}

		/// <summary>
		/// Determines the font size to use
		/// </summary>
		/// <param name="baseSize">the base font size use</param>
		/// <returns>the font size (em)</returns>
		private double DetermineFontSize(double baseSize)
		{
			int width = Screen.GetBounds(this).Width;
			return baseSize * ((double)width / 200.0);
		}

		/// <summary>
		/// Computes the color to display from the given time. Uses the
		/// same algorithm that thecolourclock.co.uk uses
		/// </summary>
		/// <param name="time">the time to convert</param>
		/// <returns>a hex value (without '#') for the specified time</returns>
		private string GetColourClockHex(DateTime time)
		{
			StringBuilder ret = new StringBuilder();

			ret.Append(this.ConvertDigitToHex((int)this.Math1(time.Hour, time.Minute, 60, 24)));
			ret.Append(this.ConvertDigitToHex((int)this.Math2(time.Hour, time.Minute, 60, 8)));
			ret.Append(this.ConvertDigitToHex((int)this.Math1(time.Minute, time.Second, 60, 60)));
			ret.Append(this.ConvertDigitToHex((int)this.Math2(time.Minute, time.Second, 60, 10)));
			ret.Append(this.ConvertDigitToHex((int)this.Math1(time.Second, time.Millisecond, 1000, 60)));
			ret.Append(this.ConvertDigitToHex((int)this.Math2(time.Second, time.Millisecond, 1000, 10)));

			return ret.ToString();
		}

		/// <summary>
		/// Computes the following equation:
		/// 
		///        B
		///   A + ---
		///        C
		/// ---------
		///     D
		/// </summary>
		/// <param name="A">A from the equation</param>
		/// <param name="B">B from the equation</param>
		/// <param name="C">C from the equation</param>
		/// <param name="D">D from the equation</param>
		/// <returns>the value of the equation</returns>
		private double Math0(int A, int B, int C, int D)
		{
			double t1 = (double)B / (double)C;
			double t2 = (double)A + t1;
			double t3 = t2 / (double)D;
			return t3;
		}

		/// <summary>
		/// Computes another part of the equation
		/// 
		///                B
		///           A + ---
		///                C
		/// round( ----------- * 15 )
		///             D
		/// </summary>
		/// <param name="A">A from the equation</param>
		/// <param name="B">B from the equation</param>
		/// <param name="C">C from the equation</param>
		/// <param name="D">D from the equation</param>
		/// <returns>the value of the equation</returns>
		private double Math1(int A, int B, int C, int D)
		{
			double t1 = this.Math0(A, B, C, D);
			double t2 = t1 * (double)15;
			double t3 = Math.Round(t2);
			return t3;
		}

		/// <summary>
		/// Computes another part of the equation
		/// 
		///                  B                     B
		///             A + ---               A + ---
		///                  C                     C
		/// round( ((-----------) - floor(-----------)) * 15 )
		///               D                    D
		/// </summary>
		/// <param name="A">A from the equation</param>
		/// <param name="B">B from the equation</param>
		/// <param name="C">C from the equation</param>
		/// <param name="D">D from the equation</param>
		/// <returns>the value of the equation</returns>
		private double Math2(int A, int B, int C, int D)
		{
			double t1 = this.Math0(A, B, C, D);
			double t2 = t1 - Math.Floor(t1);
			double t3 = t2 * (double)15;
			double t4 = Math.Round(t3);
			return t4;
		}

		/// <summary>
		/// Converts a digit to its hex equivalent
		/// </summary>
		/// <param name="digit">the digit to convert</param>
		/// <returns>a string containing the digit's hex value</returns>
		private string ConvertDigitToHex(int digit)
		{
			switch (digit)
			{
				case 10:
					return "A";
				case 11:
					return "B";
				case 12:
					return "C";
				case 13:
					return "D";
				case 14:
					return "E";
				case 15:
					return "F";
				default:
					return digit.ToString();
			}
		}
		#endregion Private Methods
	}
}
