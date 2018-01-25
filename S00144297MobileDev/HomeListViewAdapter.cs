using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using S00144297MobileDev.Models;
using Newtonsoft.Json;

namespace S00144297MobileDev
{
    class HomeListViewAdapter : BaseAdapter<Post>
    {
        private List<Post> mItems;
        private Context mContext;

        //Count number of rows required for list view
        public override int Count
        {
            get { return mItems.Count; }
        }

        public HomeListViewAdapter(Context context, List<Post> items)
        {
            mItems = items;
            mContext = context;
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Post this[int position]
        {
            get { return mItems[position]; }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.HomeListViewRow, null, false);
            }

            //Get Title TextView
            TextView txtTitle = row.FindViewById<TextView>(Resource.Id.txtHomeTitle);
            TextView txtSubTitle = row.FindViewById<TextView>(Resource.Id.txtHomeSubTitle);

            //Set row title
            txtTitle.Text = mItems[position].ActivityTitle;
            txtSubTitle.Text = mItems[position].ActivityTown;

            //Row onClick - redirect user to the post details page for the selected row
            row.Click += delegate
            {
                //Create Post object for selected row
                Post post = new Post();
                post.ActivityTitle = mItems[position].ActivityTitle;
                post.ActivitySport = mItems[position].ActivitySport;
                post.ActivityTown = mItems[position].ActivityTown;
                post.ActivityAddress = mItems[position].ActivityAddress;
                post.ActivityDetails = mItems[position].ActivityDetails;
                post.ActivityDate = mItems[position].ActivityDate.Date;
                post.ActivityEmail = mItems[position].ActivityEmail;
                post.ActivityPhone = mItems[position].ActivityPhone;
                post.OwnerID = mItems[position].OwnerID;
                post.ActivityID = mItems[position].ActivityID;

                string pTime = mItems[position].ActivityTime.ToShortTimeString();
                string pDate = mItems[position].ActivityDate.ToShortDateString();

                var intent = new Intent(mContext, typeof(PostDetailsActivity));

                //Serialize the post object and pass it to the post details activity
                var serializedPost = JsonConvert.SerializeObject(post);
                intent.PutExtra("post", serializedPost);
                intent.PutExtra("pDate", pDate);
                intent.PutExtra("pTime", pTime);

                mContext.StartActivity(intent);
            };


            return row;
        }
    }
}