using System;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Xml.Linq;
using System.Linq;
//For ARG
using System.Drawing;
//For Exception log on debug
using System.Diagnostics;

namespace CreateCircleSquareRandom
{
	public class ImgColorWebService
	{
		public ImgColorWebService ()
		{
		}
		private const string URI = "http://www.colourlovers.com/api/";
		private static ImgColorWebService instance;
		public static ImgColorWebService Instance {
			get {
				if (instance == null) {
					instance = new ImgColorWebService ();
				}
				return instance;
			}
		}

		/// <summary>
		/// Gets the shape Circle/Square based on the input.
		/// </summary>
		/// <returns>The shape infor async.</returns>
		/// <param name="input">Input.</param>
		public async Task<BaseShape> GetShape(BaseShape input)
		{
			if (input is Square)
				return await GetSquare (input as Square);
			if (input is Circle)
				return await GetCircle (input as Circle);
			return input;
		}

		/// <summary>
		/// Gets the square shapes from the specified URI.
		/// </summary>
		/// <returns>The square.</returns>
		/// <param name="input">Input.</param>
		public async Task<Square> GetSquare (Square input)
		{
			try {HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (URI + "patterns/random"));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ()) {
				using (StreamReader strReader = new StreamReader (response.GetResponseStream ())) 
				{
					
						var data = strReader.ReadToEnd ();
						if (string.IsNullOrWhiteSpace (data)) {
							return input;
						} else {

							var doc = XDocument.Parse (data);
							return doc.Root.Descendants ("pattern")
								.Select (x => new Square () {
									ShapeId = input.ShapeId,
									ImagePath = x.Element ("imageUrl").Value,
									X_Val = input.X_Val,
									Y_Val= input.Y_Val, 
									Radius = input.Radius
								}).FirstOrDefault () ?? input;
						} 
					} 
				}
			}
			catch (WebException ex) {
				Debug.Write (ex.ToString ());
				Square SquareOffline = new Square ();
				SquareOffline.X_Val = input.X_Val;
				SquareOffline.Y_Val = input.Y_Val;
				SquareOffline.ShapeId = input.ShapeId;
				SquareOffline.Radius = input.Radius;
				string strPAth=Environment.GetFolderPath(Environment.SpecialFolder.Resources);
				strPAth = Path.Combine (strPAth, "Icon.png");
				SquareOffline.ImagePath = strPAth;//"Icon.png";//Environment.GetFolderPath(Environment.SpecialFolder.Resources);
				SquareOffline.GenerateRandomColor ();
				return SquareOffline;
				
			}

			catch(Exception ex) {
				Debug.Write (ex.ToString ());
				Square SquareOffline = new Square ();
				SquareOffline.X_Val = input.X_Val;
				SquareOffline.Y_Val = input.Y_Val;
				SquareOffline.ShapeId = input.ShapeId;
				SquareOffline.Radius = input.Radius;
				string strPAth=Environment.GetFolderPath(Environment.SpecialFolder.Resources);
				strPAth = Path.Combine (strPAth, "Icon.png");
				SquareOffline.ImagePath = strPAth;//"Icon.png";//Environment.GetFolderPath(Environment.SpecialFolder.Resources);
				SquareOffline.GenerateRandomColor ();
				return SquareOffline;

			}
		}

		/// <summary>
		/// Gets the circle.
		/// </summary>
		/// <returns>The circle.</returns>
		/// <param name="input">Input.</param>
		public async Task<Circle> GetCircle (Circle input)
		{
			try {
				HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create (new Uri (URI + "colors/random"));
			request.ContentType = "application/json";
			request.Method = "GET";

			using (WebResponse response = await request.GetResponseAsync ()) {
				using (StreamReader strReader = new StreamReader (response.GetResponseStream ())) {
					
						var data = strReader.ReadToEnd ();
						if (string.IsNullOrWhiteSpace (data)) {
							return input;
						} else {

							var doc = XDocument.Parse (data);
							return doc.Root.Descendants ("rgb")
								.Select (x => {
									int R = int.Parse (x.Element ("red").Value);
									int G = int.Parse (x.Element ("green").Value);
									int B = int.Parse (x.Element ("blue").Value);
									return new Circle () {
										ShapeId = input.ShapeId,
										FillColor = Color.FromArgb (255, R, G, B),
										X_Val = input.X_Val,
										Y_Val = input.Y_Val, 
										Radius = input.Radius
									};
								}).FirstOrDefault () ?? input;
						} 
					} 
			}
		}
			catch (WebException ex) {
				Debug.Write (ex.ToString ());
				Circle CircleOffline = new Circle ();
				CircleOffline.X_Val = input.X_Val;
				CircleOffline.Y_Val = input.Y_Val;
				CircleOffline.ShapeId = input.ShapeId;
				CircleOffline.Radius = input.Radius;
				CircleOffline.GenerateRandomColor ();
				return CircleOffline;

			}
			catch(Exception ex) {
				Debug.WriteLine ("Exception at GetCircle"+ex.ToString());
				return input;

			}	
		}
	}
}

