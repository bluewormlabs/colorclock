using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GradientLabel
{
	/// <summary>
	/// A label that contains only a gradient
	/// </summary>
	public partial class GradientLabel : Label
	{
		#region Fields
		/// <summary>
		/// The start/center color
		/// </summary>
		public Color StartColor { get; set; }

		/// <summary>
		/// The end/outer color
		/// </summary>
		public Color EndColor { get; set; }
		#endregion Fields

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="GradientLabel"/>
		/// </summary>
		public GradientLabel()
		{
			this.StartColor = Color.White;
			this.EndColor = Color.Black;
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Draws a radial gradient in the label
		/// </summary>
		/// <param name="e">paint arguments</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			GraphicsPath path = new GraphicsPath();
			path.AddEllipse(this.ClientRectangle);

			PathGradientBrush brush = new PathGradientBrush(path);

			brush.CenterPoint = new PointF(this.ClientRectangle.Width / 2, this.ClientRectangle.Height / 2);
			brush.CenterColor = this.StartColor;
			brush.SurroundColors = new Color[] { this.EndColor };

			e.Graphics.FillPath(brush, path);

			brush.Dispose();
			path.Dispose();
		}
		#endregion Event Handlers
	}
}