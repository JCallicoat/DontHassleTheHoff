using System;
using System.Windows.Forms;

namespace ScreenSaver
{
	public class HasselhoffScreenSaver
	{
		[STAThread]
		static void Main (string[] args)
		{
			if (args.Length > 0) {
				if (args[0].ToLower ().Trim ().Substring (0, 2) == "/c") {
					MessageBox.Show ("This Screen Saver has no options you can set.",
					                 "Hasslehoff Screensaver", MessageBoxButtons.OK,
					                 MessageBoxIcon.Exclamation);
				} else if (args[0].ToLower () == "/s") {
					System.Windows.Forms.Application.Run (new ScreenSaverForm ());
				}
			} else {
				System.Windows.Forms.Application.Run (new ScreenSaverForm ());
			}
		}
	}
}
