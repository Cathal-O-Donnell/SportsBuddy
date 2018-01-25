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
using S00144297MobileDev.DataHelper;

namespace S00144297MobileDev
{
    class UserPostsListViewAdapter : BaseAdapter<Post>
    {
        private List<Post> mItems;
        private Context mContext;

        //Count number of rows required for list view
        public override int Count
        {
            get { return mItems.Count; }
        }

        public UserPostsListViewAdapter(Context context, List<Post> items)
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
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.listViewRow, null, false);
            }

            //Get Title TextView
            TextView txtTitle = row.FindViewById<TextView>(Resource.Id.listViewTitle);

            //Set row title
            txtTitle.Text = mItems[position].ActivityTitle;

            //Icons
            var remove = row.FindViewById<ImageView>(Resource.Id.imgRemove);
            var edit = row.FindViewById<ImageView>(Resource.Id.imgEdit);
            //Buttons

            //Remove Icon click event
            remove.Click += delegate
            {
                RemoveDialog(mItems[position].ActivityTitle, mItems[position].ActivityID);
            };

            //Edit Icon click event
            edit.Click += delegate
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

                var intent = new Intent(mContext, typeof(EditPostActivity));

                //Serialize the post object and pass it to the post details activity
                var serializedPost = JsonConvert.SerializeObject(post);
                intent.PutExtra("post", serializedPost);
                intent.PutExtra("pDate", pDate);
                intent.PutExtra("pTime", pTime);

                mContext.StartActivity(intent);
            };

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

        public void RemoveDialog(string title, int id)
        {
            AlertDialog.Builder alertDialog = new AlertDialog.Builder(mContext);
            alertDialog.SetTitle("Remove " + title);
            alertDialog.SetMessage("This cannot be undone");

            alertDialog.SetNegativeButton("Cancel", delegate
            {
                alertDialog.Dispose();
            });

            alertDialog.SetPositiveButton("Confirm", delegate
            {
        //Call remove activity method
        alertDialog.Dispose();

                RemovePost(id);
            });

            alertDialog.Show();
        }

        public void RemovePost(int id) //int id
        {
            //remove Post from database and relaod MyProfile view
            Database dbHelper = new Database();
            dbHelper.deletePost(id);

            //var userAct = new Intent(mContext, typeof(MyProfileActivity));
            //mContext.StartActivity(userAct);

        }
    }
}