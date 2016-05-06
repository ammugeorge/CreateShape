package md5c07841f3d69a432f1f75e7f3f6260ace;


public class GestureListener
	extends android.view.GestureDetector.SimpleOnGestureListener
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onDoubleTap:(Landroid/view/MotionEvent;)Z:GetOnDoubleTap_Landroid_view_MotionEvent_Handler\n" +
			"";
		mono.android.Runtime.register ("CreateCircleSquareRandom.GestureListener, CreateCircleSquareRandom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", GestureListener.class, __md_methods);
	}


	public GestureListener () throws java.lang.Throwable
	{
		super ();
		if (getClass () == GestureListener.class)
			mono.android.TypeManager.Activate ("CreateCircleSquareRandom.GestureListener, CreateCircleSquareRandom, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onDoubleTap (android.view.MotionEvent p0)
	{
		return n_onDoubleTap (p0);
	}

	private native boolean n_onDoubleTap (android.view.MotionEvent p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
