namespace ColorClock
{
	partial class ColorClock
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.timeLabel = new System.Windows.Forms.Label();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.hexLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// timeLabel
			// 
			this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.timeLabel.Font = new System.Drawing.Font("Arial Narrow", 72F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.timeLabel.ForeColor = System.Drawing.Color.White;
			this.timeLabel.Location = new System.Drawing.Point(12, 9);
			this.timeLabel.Name = "timeLabel";
			this.timeLabel.Size = new System.Drawing.Size(523, 343);
			this.timeLabel.TabIndex = 0;
			this.timeLabel.Text = "00:00:00 PM";
			this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.timeLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseClick);
			this.timeLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseMove);
			// 
			// timer
			// 
			this.timer.Interval = 1000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// hexLabel
			// 
			this.hexLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.hexLabel.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.hexLabel.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.hexLabel.Location = new System.Drawing.Point(12, 315);
			this.hexLabel.Name = "hexLabel";
			this.hexLabel.Size = new System.Drawing.Size(514, 20);
			this.hexLabel.TabIndex = 1;
			this.hexLabel.Text = "#000000";
			this.hexLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.hexLabel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseClick);
			this.hexLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseMove);
			// 
			// ColorClock
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(547, 361);
			this.Controls.Add(this.hexLabel);
			this.Controls.Add(this.timeLabel);
			this.ForeColor = System.Drawing.Color.LightGray;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ColorClock";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Color Clock";
			this.Load += new System.EventHandler(this.ColorClock_Load);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ColorClock_KeyPress);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseClick);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ColorClock_MouseMove);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label timeLabel;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Label hexLabel;
	}
}

