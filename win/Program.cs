using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ColorClock
{
	/// <summary>
	/// Entry point for the screensaver
	/// </summary>
	static class Program
	{
		#region Data Members
		public static readonly string AppName = "Color Clock";
		#endregion Data Members

		#region Public Methods
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			if (args.Length > 0)
			{
				#region Get the command line arguments
				string first = args[0].ToLower().Trim();
				string second = null;

				if (first.Length > 2)
				{
					second = first.Substring(3).Trim();
					first = first.Substring(0, 2);
				}
				else if (args.Length > 1)
				{
					second = args[1];
				}
				#endregion Get the command line arguments

				if ("/c" == first) // show configuration
				{
					MessageBox.Show("There are no configuration options.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					return;
				}
				else if ("/p" == first) // preview mode
				{
					if (null == second)
					{
						MessageBox.Show("Preview window handle not provided.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}

					IntPtr hWindow = new IntPtr(long.Parse(second));
					Application.Run(new ColorClock(hWindow));
				}
				else if ("/s" == first) // start the screensaver
				{
					ShowScreenSaver();
					Application.Run();
				}
				else
				{
					MessageBox.Show("Command line argument \"" + first + "\" invalid.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Please install the screensaver and set it in the display properties.", Program.AppName, MessageBoxButtons.OK, MessageBoxIcon.Stop);
				return;
			}
		}

		/// <summary>
		/// Show the screensaver on each screen
		/// </summary>
		static void ShowScreenSaver()
		{
			foreach (Screen screen in Screen.AllScreens)
			{
				ColorClock screensaver = new ColorClock(screen.Bounds);
				screensaver.Show();
			}
		}
		#endregion Public Methods
	}
}
