using System;
using Android.Widget;
using Android.Graphics.Drawables;
using Android.Views;
using Android.Content;
using Android.Util;
using System.Threading.Tasks;
using Android.Graphics;
using Android.Graphics.Drawables.Shapes;

namespace CreateCircleSquareRandom
{
	public class SquarePage :LinearLayout
	{
		
		ShapeViewModel shapeViewModel;
		ShapeDrawable shape;
		GestureDetector doubleTapDetector;
		GestureListener gestureListener;

		ImageView _imageView;

		public SquarePage (Context context, ShapeViewModel shapeViewModelVal) :
		base (context)
		{
			shapeViewModel = shapeViewModelVal;
			shapeViewModel.PropertyChanged += ShapeViewModel_PropertyChanged;
			 Initialize ();
		}

		void ShapeViewModel_PropertyChanged (object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Shape") {
				 HandleShapeChanged ();
			}
		}

		async Task HandleShapeChanged()
		{
			await InitDrawShape ();
			await ChangeImageSource ();
			gestureListener.Shape = shapeViewModel.Shape;
		}

		async Task Initialize ()
		{
			await InitDrawShape ();
			InitImage ();

			gestureListener = new GestureListener (shapeViewModel.Shape);
			doubleTapDetector = new GestureDetector (this.Context, gestureListener);

			gestureListener.DoubleTap = () => {
				shapeViewModel.DoubleTapCommand.Execute ();
			};
			gestureListener.RequestLayout = (l, t, r, b) => {
				Layout (l, t, r, b);
				Invalidate ();
			};
		}

		async Task InitDrawShape ()
		{
			if (shapeViewModel.Image == null) {
				var paint = new Paint ();
				paint.SetARGB (
					shapeViewModel.Shape.FillColor.A, 
					shapeViewModel.Shape.FillColor.R, 
					shapeViewModel.Shape.FillColor.G,
					shapeViewModel.Shape.FillColor.B);
				paint.SetStyle (Paint.Style.FillAndStroke);
				paint.StrokeWidth = 4;

				shape = new ShapeDrawable (new RectShape ());
				shape.Paint.Set (paint);

				shape.SetBounds (
					0,
					0, 
					shapeViewModel.Shape.Radius * 2, 
					shapeViewModel.Shape.Radius * 2);
			}
			await shapeViewModel.LoadImageIfNeed ();
		}

		void InitImage ()
		{
			_imageView = new ImageView (this.Context);
			TextView textView = new TextView (this.Context);
			if (shapeViewModel.Image != null) {
				_imageView.SetImageBitmap (shapeViewModel.Image);
				LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams (
					                                         shapeViewModel.Shape.Radius * 2,
					                                         shapeViewModel.Shape.Radius * 2);

				AddView (_imageView, layoutParams);

			} else {

					var paint = new Paint ();
					paint.SetARGB (
						shapeViewModel.Shape.FillColor.A, 
						shapeViewModel.Shape.FillColor.R, 
						shapeViewModel.Shape.FillColor.G,
						shapeViewModel.Shape.FillColor.B);
					paint.SetStyle (Paint.Style.FillAndStroke);
					paint.StrokeWidth = 4;

					shape = new ShapeDrawable (new RectShape ());
					shape.Paint.Set (paint);

					shape.SetBounds (
						0,
						0, 
						shapeViewModel.Shape.Radius * 2, 
						shapeViewModel.Shape.Radius * 2);
				
				textView.Text = string.Empty;
				textView.Background = shape;
				LinearLayout.LayoutParams layoutParams = new LinearLayout.LayoutParams (
					shapeViewModel.Shape.Radius * 2,
					shapeViewModel.Shape.Radius * 2);
				AddView (textView, layoutParams);
				
			}
		}

		async Task ChangeImageSource()
		{
			if (shapeViewModel.Image != null) {
				if (_imageView == null) {
					InitImage ();
					return;
				}
				_imageView.SetImageBitmap (shapeViewModel.Image);
			}
		}

		public override bool OnTouchEvent (MotionEvent motion)
		{
			try{
			doubleTapDetector.OnTouchEvent (motion);
			gestureListener.HandleMotionEvent (motion);
			}
			catch (Exception ex){
				
				return true;
			}
			return true;
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.Draw (canvas);
			if (shape != null && shapeViewModel.Image == null)
				shape.Draw (canvas);
		}

	}

}

