using System;
using Android.Views;

namespace CreateCircleSquareRandom
{
	public class GestureListener:GestureDetector.SimpleOnGestureListener
	{
		public Action DoubleTap;

		public Action<int, int, int, int> RequestLayout;

		public BaseShape Shape { get; set;}

		float XTouch;
		float YTouch;
		float XVal= 0;
		float YVal = 0;
		int IdPoint;
		public GestureListener (BaseShape shape)
		{
			Shape = shape;
		}

		/// <summary>
		/// Raises the double tap event.
		/// </summary>
		/// <param name="e">E.</param>
		public override bool OnDoubleTap (MotionEvent e)
		{
			if (DoubleTap != null)
				DoubleTap ();
//			RequestLayout (
//				Shape.X_Val - Shape.Radius,
//				Shape.Y_Val - Shape.Radius,
//				Shape.X_Val + Shape.Radius,
//				Shape.Y_Val + Shape.Radius
//			);
			RequestLayout (
				Shape.X_Val ,
				Shape.Y_Val ,
				Shape.X_Val ,
				Shape.Y_Val 
			);
			return true;
		}

		/// <summary>
		/// Handles the drag event.
		/// </summary>
		/// <param name="ev">Ev.</param>
		public void HandleMotionEvent (MotionEvent ev)
		{
			MotionEventActions action = ev.Action & MotionEventActions.Mask;
			int pointerIndex;
			switch (action) {
			case MotionEventActions.Down:
				XTouch = ev.RawX;
				YTouch = ev.RawY;
				IdPoint = ev.GetPointerId (0);
//					XVal = XVal == 0 ? XVal-XTouch - Shape.Radius : XVal;
//					YVal = YVal == 0 ? YVal-YTouch - Shape.Radius : YVal;
				
				XVal = XVal == 0 ? XTouch - Shape.Radius : XVal;
				YVal = YVal == 0 ? YTouch - Shape.Radius : YVal;
			break;

			case MotionEventActions.Move:
				pointerIndex = ev.FindPointerIndex (IdPoint);
				float x = ev.RawX;
				float y = ev.RawY;

				float deltaX = x - XTouch;
				float deltaY = y - YTouch;
				XVal+= deltaX;
				YVal += deltaY;
				if (RequestLayout != null)
					RequestLayout ( (int)XVal, (int)YVal,(int)XVal+ Shape.Radius * 2, (int)YVal + Shape.Radius * 2);

				XTouch = x;
				YTouch= y;
				break;

			case MotionEventActions.PointerUp:
				// check to make sure that the pointer that went up is for the gesture we're tracking.
				pointerIndex = (int)(ev.Action & MotionEventActions.PointerIndexMask) >> (int)MotionEventActions.PointerIndexShift;
				int pointerId = ev.GetPointerId (pointerIndex);
				if (pointerId == IdPoint) {

					XTouch = ev.RawX;
					YTouch = ev.RawY;
					Shape.X_Val = (int)XVal+ Shape.Radius;
					Shape.Y_Val = (int)YVal + Shape.Radius;

				}
				break;

			case MotionEventActions.Up:
				XTouch = ev.RawX;
				YTouch = ev.RawY;
				Shape.X_Val = (int)XVal+ Shape.Radius;
				Shape.Y_Val = (int)YVal + Shape.Radius;
				XVal= 0;
				YVal = 0;
				break;

			case MotionEventActions.Cancel:
				IdPoint = -1;
				break;



			}
		}
	}}
	

