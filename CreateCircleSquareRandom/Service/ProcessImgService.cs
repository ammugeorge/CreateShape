using System;
using Android.Gestures;
using Android.Graphics;
using System.Diagnostics;
using System.Net;
using Android.Util;
using System.Threading.Tasks;


/// <summary>
/// Image web service for retrieving the image from the URI.
/// </summary>
namespace CreateCircleSquareRandom
{
	/// <summary>
	/// Image web service.
	/// </summary>
	public class ProcessImgService
	{
		LruCache imgCache;
		/// <summary>
		/// Gets the i image queue count.
		/// </summary>
		/// <value>The i image queue count.</value>
		public int iImgQueueCount{get { return 3; }}
		private static ProcessImgService instance;
		ImgColorWebService colorService;
		/// <summary>
		/// Initializes a new instance of the <see cref="CreateCircleSquareRandom.ProcessImgService"/> class.
		/// </summary>
		public ProcessImgService ()
		{
				imgCache= new LruCache(iImgQueueCount);
			colorService = ImgColorWebService.Instance;
		}

		/// <summary>
		/// Gets the instance.
		/// </summary>
		/// <value>The instance.</value>
		public static ProcessImgService Instance {
				get {
					if (instance == null) {
						instance = new ProcessImgService ();
					}
					return instance;
				}
			}

		/// <summary>
		/// Gets the image from cache.
		/// </summary>
		/// <returns>The image from cache.</returns>
		/// <param name="shape">Shape.</param>
		public async Task<Bitmap> GetImageFromCache (Square shape)
			{
				try
				{
					var image = imgCache.Get (shape.ShapeId.ToString()) as Bitmap;
				if (image == null&&shape.ImagePath!="Icon.png") {
					image = await GetImageFromUrl(shape.ImagePath);
					}
				else 
					image= (Bitmap)Resource.Mipmap.Icon;
				return image;
				}
				catch (Exception ex) 
				{
					Debug.WriteLine (ex.ToString ());
				}
				return null;
			}


		/// <summary>
		/// Puts the image to cache.
		/// </summary>
		/// <returns>The image to cache.</returns>
		/// <param name="squareShape">Square shape.</param>
			public async Task<Square> SetImageToCache (Square squareShape)
			{
				Square shape = null;
				if (string.IsNullOrEmpty (squareShape.ImagePath)) {
				shape = await colorService.GetSquare (squareShape);
				}
			if (shape != null) {
				
				if (shape.ImagePath != "Icon.png") 
				{
					 var image = await GetImageFromUrl (shape.ImagePath);
					if (image != null)
						imgCache.Put (shape.ShapeId.ToString (), image);
					return shape;
				} 

				}

				return squareShape;
			}
				
			
		/// <summary>
		/// Gets the image bitmap from URL.
		/// </summary>
		/// <returns>The image bitmap from URL.</returns>
		/// <param name="url">URL.</param>
			private async Task<Bitmap> GetImageFromUrl (string strUrl)
			{
				Bitmap image = null;
				using (var webClient = new WebClient ()) {
					try {
					var imageByte = await webClient.DownloadDataTaskAsync (strUrl);
						if (imageByte != null && imageByte.Length > 0) {
							image = BitmapFactory.DecodeByteArray (imageByte, 0, imageByte.Length);
						}

					else 
					{
						image = (Bitmap)Resource.Mipmap.Icon;
					}

				} catch (Exception ex)
					{
					Debug.WriteLine("Exception in GetImageFromUrl"+ ex.ToString());
					}
				}

				return image;
			}
	}
}

