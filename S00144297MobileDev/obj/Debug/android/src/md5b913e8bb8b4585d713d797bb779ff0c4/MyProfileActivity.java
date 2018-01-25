package md5b913e8bb8b4585d713d797bb779ff0c4;


public class MyProfileActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("S00144297MobileDev.MyProfileActivity, S00144297MobileDev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MyProfileActivity.class, __md_methods);
	}


	public MyProfileActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == MyProfileActivity.class)
			mono.android.TypeManager.Activate ("S00144297MobileDev.MyProfileActivity, S00144297MobileDev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
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
