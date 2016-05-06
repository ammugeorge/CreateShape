using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CreateCircleSquareRandom
{
	public class CreateShapeFactory
	{
		private static CreateShapeFactory instance;
		ProcessImgService imgService;
		/// <summary>
		/// The height,weight and shape id 
		/// </summary>
		int iMaxHeight=200;
		int iMaxWidth=200;
		Random rand= new Random();
		int iShapeId=1;
		Queue<Square> qSquare;


		/// <summary>
		/// Creating instance of CreateShapeFactory if not exists
		/// </summary>
		public static CreateShapeFactory Instance
		{
			get{

				if (instance==null)

					instance= new CreateShapeFactory();

				return instance;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.CreateShapeFactory"/> class.
		/// </summary>
		public   CreateShapeFactory ()
		{
			imgService = ProcessImgService.Instance;
			Task.Run (() => LoadSquare ());

		}

		/// <summary>
		/// Loads the square.
		/// </summary>
		/// <returns>The square.</returns>
		private async Task LoadSquare()
		{
			int imgCount = imgService.iImgQueueCount;
			if (qSquare == null)
				qSquare = new Queue<Square> (imgCount);
			while (qSquare.Count < imgCount) {
				iShapeId++;
				var shape=new Square(){
					ShapeId= iShapeId
				};
				shape.Y_Val = shape.Y_Val == 0 ? rand.Next (10, iMaxHeight) : shape.Y_Val;
				shape.X_Val = shape.X_Val == 0 ? rand.Next (10, iMaxWidth) : shape.X_Val;
				shape.Radius= shape.Radius==0 ? rand.Next(10,(iMaxWidth+iMaxHeight)/8 ): shape.Radius;
			   //SetShape(ref shape,false,iMaxHeight,iMaxWidth);
				var shapeValue= await imgService.SetImageToCache(shape);
				qSquare.Enqueue(shapeValue);
				

			}
			
		}

		/// <summary>
		/// Gets the square from queue.
		/// </summary>
		/// <returns>The square from queue.</returns>
		public async Task<Square> GetSquareFromQueue()
		{
			if (qSquare.Count == 0)
				await LoadSquare ();
			var shape = qSquare.Dequeue ();
			await Task.Run (() =>   LoadSquare ());
			return shape;

		}
		/// <summary>
		/// Creates the random shape.
		/// </summary>
		/// <returns>The random shape.</returns>
		/// <param name="">.</param>
		public async Task<BaseShape>CreateRandomShape(int iHeight,int iWidth)
		{
			iMaxWidth = iWidth;
			iMaxHeight = iHeight;
			int iShape = rand.Next (10);
			BaseShape shape = new BaseShape ();
			if (iShape % 2 == 0) {
				shape = new Circle ();
				iShapeId++;
				shape.ShapeId = iShapeId;
			} else {
				shape = await GetSquareFromQueue ();
			}
			SetShape(ref shape,true,iHeight,iWidth);
			return shape;
		}
		/// <summary>
		/// Sets the shape.
		/// </summary>
		/// <param name="shape">Shape.</param>
		/// <param name="bFlag">If set to <c>true</c> b flag.</param>
		/// <param name="iMaxHeight">I max height.</param>
		/// <param name="iMaxWidth">I max width.</param>
		private void SetShape(ref BaseShape  shape,bool bFlag, int iMaxHeight,int iMaxWidth)
		{
			shape.Y_Val = shape.Y_Val == 0 ? rand.Next (10, iMaxHeight) : shape.Y_Val;
			shape.X_Val = shape.X_Val == 0 ? rand.Next (10, iMaxWidth) : shape.X_Val;
			shape.Radius= shape.Radius==0 ? rand.Next(10,(iMaxWidth+iMaxHeight)/8 ): shape.Radius;
			if (bFlag)
				shape.GenerateRandomColor ();

		}

	}
}

