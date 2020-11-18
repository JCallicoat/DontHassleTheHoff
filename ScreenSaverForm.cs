using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Media;
using System.Resources;
using System.Windows.Forms;
using System.ComponentModel;

namespace ScreenSaver
{
	public class ScreenSaverForm : Form
	{
		private IContainer components;
//		private Point MouseXY;
		private Timer timer;
		private PictureBox pictureBox;
		
		private ArrayList images;
		private int index;
		private int typed;
		private string pass;
		
		private ResourceManager resources;

		public ScreenSaverForm ()
		{
//			SetStyle (ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint |
			SetStyle (ControlStyles.DoubleBuffer, true);

			this.resources = new ResourceManager (typeof (ScreenSaverForm));

			this.images = new ArrayList ();
			for (int i = 0 ; i < 6 ; ++i)
			{
				images.Add (resources.GetObject ("hoff" + i + ".jpg"));
			}

			index   = 0;
			typed   = 0;
			pass    = "";

			InitializeComponent ();

		}

		protected override void Dispose (bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		private void DontHasselTheHoff ()
		{
			try
			{
				MemoryStream ms = new MemoryStream (File.ReadAllBytes ("hoff1.wav"));
				SoundPlayer sp = new SoundPlayer (ms);
				sp.Play ();
				MessageBox.Show ("Don't Hassel the Hoff!");
				sp.Stop ();
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
			}
		}

		private void OnFormLoad (object sender, EventArgs e)
		{
			Bounds = Screen.PrimaryScreen.WorkingArea;
			this.pictureBox.Bounds = new Rectangle (0, 0, Bounds.Width, Bounds.Height);
			this.pictureBox.Image = this.images[index] as Image;

			Cursor.Hide ();
			this.Focus ();

			if ((int) Environment.OSVersion.Platform != 6) // OS X
			{
				TopMost = true;
			}
			
			try
			{
				MemoryStream ms = new MemoryStream (File.ReadAllBytes ("hoff.wav"));
				SoundPlayer sp = new SoundPlayer (ms);
				sp.PlayLooping ();
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
			}
		}

		private void OnTimerTick (object sender, EventArgs e)
		{
			if (index == 5)
			{
				index = 0;
			}
			else
			{
				++index;
			}

			this.pictureBox.Image = this.images[index] as Image;
		}
		
		private void OnMouseEvent (object sender, MouseEventArgs e)
		{
//			if (!MouseXY.IsEmpty) {
//				if (MouseXY != new Point (e.X, e.Y) || e.Clicks > 0)
//					Close ();
//			}
//			MouseXY = new Point (e.X, e.Y);
			this.DontHasselTheHoff ();
		}

		private void OnKeyDown (object sender, KeyEventArgs e)
		{
			string key = e.KeyData.ToString();
			if (key == "H" || key == "O" || key == "F" || key == "Space")
			{
				
				pass += key;
				typed += 1;
				if (typed == 4)
				{
					if (pass == "HOFF" || pass == "SpaceSpaceSpaceSpace")
					{
						Close ();
					}
					else {
						MessageBox.Show ("Hasselhoff does not approve!");
						typed = 0;
						pass = "";
					}
				}

			}
			else
			{
				typed = 0;
				pass  = "";

				this.DontHasselTheHoff ();
			}
			e.Handled = true;
			e.SuppressKeyPress = true;
		}

		private void OnLostFocus(object sender, EventArgs e)
		{
			this.Focus ();
		}

		private void InitializeComponent ()
		{
			this.components = new Container ();
			this.timer = new Timer (this.components);
			this.pictureBox = new PictureBox ();
			this.SuspendLayout ();
			// 
			// pictureBox
			// 
			//this.pictureBox.Image = (Bitmap) this.images[index];
			this.pictureBox.Location = new Point (0, 0);
			this.pictureBox.Size = new Size (0, 0);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.pictureBox.MouseClick += new MouseEventHandler (this.OnMouseEvent);
			this.pictureBox.KeyDown += new KeyEventHandler (this.OnKeyDown);
			// 
			// timer
			// 
			this.timer.Enabled = true;
			this.timer.Interval = 2000;
			this.timer.Tick += new EventHandler (this.OnTimerTick);
			// 
			// ScreenSaverForm
			// 
			try
			{
				this.Icon = new Icon ("App.ico");
			}
			catch (Exception ex)
			{
				Console.WriteLine (ex);
			}
			this.AutoScaleBaseSize = new Size (0, 0);
			this.BackColor = Color.Black;
			this.ClientSize = new Size (0, 0);
			this.Controls.AddRange (new Control[] { this.pictureBox });
			this.FormBorderStyle = FormBorderStyle.None;
			this.Name = "ScreenSaverForm";
			this.Text = "ScreenSaver";
			this.KeyDown += new KeyEventHandler (this.OnKeyDown);
//			this.MouseMove += new MouseEventHandler (this.OnMouseEvent);
			this.Load += new EventHandler (this.OnFormLoad);
			this.LostFocus += new EventHandler (this.OnLostFocus);
			this.ResumeLayout (false);
			
		}
	}
}
