using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;
using Android.Hardware;
using Android.Content;
using Android.Runtime;


namespace CreateCircleSquareRandom
{
	/// <summary>
	/// Main activity.
	/// </summary>
	[Activity (Label = "CreateCircleSquareRandom", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity,ISensorEventListener
	{
		
		MainViewModel mainModel;
		float last_x = 0.0f;
		float last_y = 0.0f;
		float last_z = 0.0f;
		bool bUpdated = false;
		DateTime dtLastUpdate;
		const int ShakeDetectionTimeLapse = 100;
		const int ShakeThreshold = 800;

		/// <summary>
		/// Raises the create event.
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			RelativeLayout mainLayout = FindViewById<RelativeLayout> (Resource.Id.mainPageRelLayout);
			mainModel = new MainViewModel (mainLayout, this);
			mainLayout.Touch += async (sender, e) => {
				if ((e.Event.Action & MotionEventActions.Mask) == MotionEventActions.Down) {
					await mainModel.AddNewShape ((int)e.Event.GetX(),
						(int)e.Event.GetY(),
						(int)Resources.DisplayMetrics.HeightPixels,
						(int)Resources.DisplayMetrics.WidthPixels);
				}
			};

			var sensorManager = GetSystemService (SensorService) as SensorManager;
			var sensor = sensorManager.GetDefaultSensor (SensorType.Accelerometer);
			sensorManager.RegisterListener(this, sensor, SensorDelay.Ui);
		
		}
		/// <Docs>To be added.</Docs>
		/// <summary>
		/// Called when the accuracy of a sensor has changed.
		/// </summary>
		/// <para tool="javadoc-to-mdoc">Called when the accuracy of a sensor has changed.</para>
		/// <param name="sensor">Sensor.</param>
		/// <param name="accuracy">Accuracy.</param>
		public void OnAccuracyChanged (Sensor sensor,SensorStatus accuracy)
		{
		}

		/// <summary>
		/// Raises the sensor changed event.
		/// </summary>
		/// <param name="e">E.</param>
			public void OnSensorChanged (SensorEvent e)
			{
				if (e.Sensor.Type == SensorType.Accelerometer)
				{
					float x = e.Values[0];
					float y = e.Values[1];
					float z = e.Values[2];

					DateTime curDateTime = DateTime.Now;
					if (bUpdated == false)
					{
						bUpdated = true;
						dtLastUpdate= curDateTime;
						last_x = x;
						last_y = y;
						last_z = z;
						
					}
					else
					{
					if ((curDateTime - dtLastUpdate).TotalMilliseconds > ShakeDetectionTimeLapse) 
						{
						float diffTime = (float)(curDateTime - dtLastUpdate).TotalMilliseconds;
						dtLastUpdate = curDateTime;
							float total = x + y + z - last_x - last_y - last_z;
							float speed = Math.Abs(total) / diffTime * 10000;

							if (speed > ShakeThreshold) {

							RelativeLayout mainLayout = FindViewById<RelativeLayout> (Resource.Id.mainPageRelLayout);
								mainLayout.RemoveAllViews ();
							}

							last_x = x;
							last_y = y;
							last_z = z;
						}
					}
				}
			}



//		//
//		public void onSensorChanged(int sensor, float[] values) {
//			if (sensor == SensorManager.SENSOR_ACCELEROMETER) {
//				long curTime = System.currentTimeMillis();
//				// only allow one update every 100ms.
//				if ((curTime - lastUpdate) > 100) {
//					long diffTime = (curTime - lastUpdate);
//					lastUpdate = curTime;
//
//					x = values[SensorManager.DATA_X];
//					y = values[SensorManager.DATA_Y];
//					z = values[SensorManager.DATA_Z];
//
//					float speed = Math.abs(x+y+z - last_x - last_y - last_z) / diffTime * 10000;
//
//					if (speed > SHAKE_THRESHOLD) {
//						Log.d("sensor", "shake detected w/ speed: " + speed);
//						Toast.makeText(this, "shake detected w/ speed: " + speed, Toast.LENGTH_SHORT).show();
//					}
//					last_x = x;
//					last_y = y;
//					last_z = z;
//				}
//			}
//		}
//		//
	}
}


