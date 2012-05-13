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
	public class TextGradientLabel : GradientLabel
	{
		#region Fields
		/// <summary>
		/// Vertical alignment for the text
		/// </summary>
		public StringAlignment VerticalAlignment { get; set; }

		/// <summary>
		/// Horizontal alignment for the text
		/// </summary>
		public StringAlignment HorizontalAlignment { get; set; }
		#endregion Fields

		#region Constructors
		/// <summary>
		/// Creates a new <see cref="TextGradientLabel"/>
		/// </summary>
		public TextGradientLabel() : base()
		{
			this.VerticalAlignment = StringAlignment.Center;
			this.HorizontalAlignment = StringAlignment.Center;
		}
		#endregion Constructors

		#region Event Handlers
		/// <summary>
		/// Draws a radial gradient in the label
		/// </summary>
		/// <param name="e">paint arguments</param>
		protected override void OnPaint(PaintEventArgs e)
		{
			// Draw the gradient
			base.OnPaint(e);

			// Draw the text
			StringFormat formatter = new StringFormat();
			formatter.Alignment = this.VerticalAlignment;
			formatter.LineAlignment = this.HorizontalAlignment;
			RectangleF rect = new RectangleF(0, 0, this.Width, this.Height);
			e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(this.ForeColor), rect, formatter);
		}
		#endregion Event Handlers
	}
}
