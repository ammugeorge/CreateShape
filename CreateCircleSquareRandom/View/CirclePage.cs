using System;
using Android.Content;
using Android.Text;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Util;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;

namespace CreateCircleSquareRandom
{
	public class CirclePage:View
	{
		ShapeDrawable shape;
		GestureDetector doubleTapDetector;
		GestureListener gestureListener;
		ShapeViewModel shapeView;

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.CirclePage"/> class.
		/// </summary>
		/// <param name="context">Context.</param>
		/// <param name="shapeViewModel">Shape view model.</param>
		public CirclePage (Context context, ShapeViewModel shapeViewModel) :
		base (context)
		{
			
			shapeView = shapeViewModel;
			shapeViewModel.PropertyChanged += ShapeViewModel_PropertyChanged;
			Initialize ();

		}

//		/// <summary>
//		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.CirclePage"/> class.
//		/// </summary>
//		/// <param name="context">Context.</param>
//		/// <param name="attrs">Attrs.</param>
//		public CirclePage (Context context, IAttributeSet attrs) :
//		base (context, attrs)
//		{
//			Initialize ();
//		}
//
//		/// <summary>
//		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.CirclePage"/> class.
//		/// </summary>
//		/// <param name="context">Context.</param>
//		/// <param name="attrs">Attrs.</param>
//		/// <param name="defStyle">Def style.</param>
//		public CirclePage (Context context, IAttributeSet attrs, int defStyle) :
//		base (context, attrs, defStyle)
//		{
//			Initialize ();
//		}

		/// <summary>
		/// Shapes the view model property changed.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void ShapeViewModel_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Shape") {
				InitDrawShape ();
			}
		}

		/// <summary>
		/// Initialize this instance.
		/// </summary>
		void Initialize ()
		{			
			InitDrawShape ();

			gestureListener = new GestureListener (shapeView.Shape);
			doubleTapDetector = new GestureDetector (this.Context, gestureListener);

			gestureListener.DoubleTap = () => {
		    shapeView.DoubleTapCommand.Execute ();
			};

			gestureListener.RequestLayout = (l, t, r, b) => {
				Layout (l, t, r, b);
				Invalidate ();
			};
		}

		public override bool OnTouchEvent (MotionEvent ev)
		{
			doubleTapDetector.OnTouchEvent (ev);
			gestureListener.HandleMotionEvent (ev);
			return true;
		}

		/// <summary>
		/// Inits the draw shape.
		/// </summary>
		void InitDrawShape ()
		{
			shape = null;
			var paint = new Paint ();
			paint.SetARGB (
				shapeView.Shape.FillColor.A, 
				shapeView.Shape.FillColor.R, 
				shapeView.Shape.FillColor.G,
				shapeView.Shape.FillColor.B);
			paint.SetStyle (Paint.Style.FillAndStroke);
			paint.StrokeWidth = 2;

			shape = new ShapeDrawable (new OvalShape ());
			shape.Paint.Set (paint);

			shape.SetBounds (
				0,
				0,
				shapeView.Shape.Radius * 2, 
				shapeView.Shape.Radius * 2);
		}

		protected override void OnDraw (Canvas canvas)
		{
			shape.Draw (canvas);
		}
	}
}

