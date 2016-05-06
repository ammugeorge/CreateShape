using System;
using System.Threading.Tasks;
using Android.Graphics;

namespace CreateCircleSquareRandom
{
	public class ShapeViewModel:BaseViewModel
	{
		BaseShape shapeBase;

		ImgColorWebService colorService;
		ProcessImgService imgService;
		CreateShapeFactory shapeFactory;


		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.ShapeViewModel"/> class.
		/// </summary>
		/// <param name="shape">Shape.</param>
		public ShapeViewModel (BaseShape shape)
		{
				shapeBase = shape;
				colorService = ImgColorWebService.Instance;
				imgService = ProcessImgService.Instance;
				shapeFactory = CreateShapeFactory.Instance;
			     DoubleTapCommand = new Command (async ()=> HandleDoubleTap());
		}

		/// <summary>
		/// Loads the image  for square based on need.
		/// </summary>
		/// <returns>The image if need.</returns>
		public async Task LoadImageIfNeed()
		{
			if (shapeBase is Square) {
				Image = await imgService.GetImageFromCache (shapeBase as Square);
			}
		}

		/// <summary>
		/// Handles the double tap.
		/// </summary>
		/// <returns>The double tap.</returns>
		async Task HandleDoubleTap()
		{
			shapeBase.GenerateRandomColor ();
			if (shapeBase is Square) 
			{				
				var ShapeVal = await shapeFactory.GetSquareFromQueue ();
				ShapeVal.X_Val = shapeBase.X_Val;
				ShapeVal.Y_Val = shapeBase.Y_Val;
				ShapeVal.Radius = shapeBase.Radius;
				Shape = ShapeVal;
			} 
			else 
			{
				Shape = await colorService.GetShape (shapeBase);
			}
		}

		/// <summary>
		/// Gets or sets the double tap command.
		/// </summary>
		/// <value>The double tap command.</value>
		public Command DoubleTapCommand { get; set ;}
		/// <summary>
		/// Gets or sets the shape.
		/// </summary>
		/// <value>The shape.</value>
		public BaseShape Shape 
			{ 
				get {
				return shapeBase;
				}
				set {
				shapeBase = value;
				RaisePropertyChanged ();
				}
			}


		/// <summary>
		/// The image.
		/// </summary>
			Bitmap imageVal;

		public Bitmap Image { 
			get {
				return imageVal;
			}
			set {
				imageVal = value;
				RaisePropertyChanged ();
			}
		}
	


	}
}

