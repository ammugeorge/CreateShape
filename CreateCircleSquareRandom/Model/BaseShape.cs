using System;
using System.Drawing;

namespace CreateCircleSquareRandom
{
	public class BaseShape
	{
		public BaseShape ()
		{
		}

		public int ShapeId{ get; set; }
		public int Radius{ get; set; }
		public int X_Val{ get; set;}
		public int Y_Val{ get; set; }
		public Color FillColor { get;set;}
		/// <summary>
		/// Generates the random color.
		/// </summary>
		public void GenerateRandomColor()
		{
			Random rand = new Random ();
			this.FillColor = Color.FromArgb (255, 
				rand.Next (0, 255), 
				rand.Next (0, 255), 
				rand.Next (0, 255));
		}

	}
}

