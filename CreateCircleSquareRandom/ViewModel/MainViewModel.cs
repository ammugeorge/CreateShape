using System;
using Android.Widget;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;

namespace CreateCircleSquareRandom
{
	public class MainViewModel
	{
			ImgColorWebService colorService;
			CreateShapeFactory shapeFactory;
			RelativeLayout mainRelLayout;
			Context contextVal;
			/// <summary>
			/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.MainViewModel"/> class.
			/// </summary>
			/// <param name="mainLayout">Main layout.</param>
			/// <param name="context">Context.</param>
			public MainViewModel (RelativeLayout mainLayout, Context context)
			{
				colorService = ImgColorWebService.Instance;
				shapeFactory = CreateShapeFactory.Instance;
				mainRelLayout = mainLayout;
				contextVal = context;
			}

			/// <summary>
			/// Adds the new shape-Circle/Square.
			/// </summary>
			/// <returns>The new shape.</returns>
			/// <param name="maxHeight">Max height.</param>
			/// <param name="maxWidth">Max width.</param>
			 public async Task AddNewShape (int XVal,int YVal,int maxHeight, int maxWidth)
			{
				var shape = await shapeFactory.CreateRandomShape (maxHeight, maxWidth);
				BaseShape shapeInfo;
				if (shape is Circle)
					shapeInfo = await colorService.GetShape (shape);
				else
					shapeInfo = shape;
				RelativeLayout.LayoutParams layoutParams = new RelativeLayout.LayoutParams (shapeInfo.Radius * 2, shapeInfo.Radius * 2);
			layoutParams.LeftMargin = XVal-shapeInfo.Radius;
			layoutParams.TopMargin = YVal- shapeInfo.Radius;
				ShapeViewModel viewModel = new ShapeViewModel (shapeInfo);
				//Adding circle based on the shape information retrieved.
				if (shapeInfo is Circle) {
					var circleView = new CirclePage (contextVal, viewModel);
					mainRelLayout.AddView (circleView, layoutParams);
				}
				//Adding Square based on the shape information retrieved.
				if (shapeInfo is Square) {
					var squareView = new SquarePage (contextVal, viewModel);
					mainRelLayout.AddView (squareView, layoutParams);
				}
			}
	}
}

