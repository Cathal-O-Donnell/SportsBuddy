package md5b913e8bb8b4585d713d797bb779ff0c4;


public class HomeListViewAdapter
	extends android.widget.BaseAdapter
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getCount:()I:GetGetCountHandler\n" +
			"n_getItemId:(I)J:GetGetItemId_IHandler\n" +
			"n_getItem:(I)Ljava/lang/Object;:GetGetItem_IHandler\n" +
			"n_getView:(ILandroid/view/View;Landroid/view/ViewGroup;)Landroid/view/View;:GetGetView_ILandroid_view_View_Landroid_view_ViewGroup_Handler\n" +
			"";
		mono.android.Runtime.register ("S00144297MobileDev.HomeListViewAdapter, S00144297MobileDev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", HomeListViewAdapter.class, __md_methods);
	}


	public HomeListViewAdapter () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HomeListViewAdapter.class)
			mono.android.TypeManager.Activate ("S00144297MobileDev.HomeListViewAdapter, S00144297MobileDev, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public int getCount ()
	{
		return n_getCount ();
	}

	private native int n_getCount ();


	public long getItemId (int p0)
	{
		return n_getItemId (p0);
	}

	private native long n_getItemId (int p0);


	public java.lang.Object getItem (int p0)
	{
		return n_getItem (p0);
	}

	private native java.lang.Object n_getItem (int p0);


	public android.view.View getView (int p0, android.view.View p1, android.view.ViewGroup p2)
	{
		return n_getView (p0, p1, p2);
	}

	private native android.view.View n_getView (int p0, android.view.View p1, android.view.ViewGroup p2);

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