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

namespace S00144297MobileDev
{
    [Activity(Label = "SearchResultActivity", Theme = "@style/CustomActionBarTheme")]
    public class SearchResultActivity : Activity
    {
        //Add the new navigation method

        private List<Post> mItems;
        private ListView mListView;

        public int UserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Retrieve the current users ID
            UserId = GlobalVariables.currentUserId;

            base.OnCreate(savedInstanceState);

            //Link this activity to the Search Resuly layout page
            SetContentView(Resource.Layout.SearchResult);

            LinearLayout searchResultLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutSearchResults);

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            searchResultLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));

            //Navigation
            #region
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            //Set the Pressed state of the current nav button to true
            LinearLayout navSearch = FindViewById<LinearLayout>(Resource.Id.navSearch);
            navSearch.Pressed = true;

            //Home
            LinearLayout navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            ImageView navHomeimg = FindViewById<ImageView>(Resource.Id.imgNavHome);

            //Search
            ImageView navSearchimg = FindViewById<ImageView>(Resource.Id.imgNavSearch);

            //New Post
            LinearLayout navNewPost = FindViewById<LinearLayout>(Resource.Id.navNewPost);
            ImageView navPostimg = FindViewById<ImageView>(Resource.Id.imgNewPost);

            //User
            LinearLayout navUser = FindViewById<LinearLayout>(Resource.Id.navUser);
            ImageView navUserimg = FindViewById<ImageView>(Resource.Id.imgNavUser);

            //Navigation Home button click event
            navHome.Click += delegate
            {
                var homeAct = new Intent(this, typeof(HomeActivity));
                StartActivity(homeAct);
            };
            navHomeimg.Click += delegate
            {
                var homeAct = new Intent(this, typeof(HomeActivity));
                StartActivity(homeAct);
            };

            //Navigation Search button click event
            navSearch.Click += delegate
            {
                var searchAct = new Intent(this, typeof(SearchActivity));
                StartActivity(searchAct);
            };
            navSearchimg.Click += delegate
            {
                var searchAct = new Intent(this, typeof(SearchActivity));
                StartActivity(searchAct);
            };

            //Navigation New Post button click event
            navNewPost.Click += delegate
            {
                var newpostAct = new Intent(this, typeof(NewPostActivity));
                StartActivity(newpostAct);
            };
            navPostimg.Click += delegate
            {
                var newpostAct = new Intent(this, typeof(NewPostActivity));
                StartActivity(newpostAct);
            };

            //Navigation Profile button click event
            navUser.Click += delegate
            {
                var profileAct = new Intent(this, typeof(MyProfileActivity));
                StartActivity(profileAct);
            };
            navPostimg.Click += delegate
            {
                var profileAct = new Intent(this, typeof(MyProfileActivity));
                StartActivity(profileAct);
            };
            #endregion

            mListView = FindViewById<ListView>(Resource.Id.lstSearchResults);

            //This data will be pulled from the database, but for now it is hardcoded
            mItems = new List<Post>();

            List<Post> userPosts = new List<Post>();

            //Hardcoding Posts - this will be pulled from the database
            Post post1 = new Post();
            post1.ActivityTitle = "Weekly Tennis";
            post1.ActivitySport = "Tennis";
            post1.ActivityTown = "Sligo";

            Post post2 = new Post();
            post2.ActivityTitle = "Hillwalking Club";
            post2.ActivitySport = "Hillwalking";
            post2.ActivityTown = "Galway";

            mItems.Add(post1);
            mItems.Add(post2);

            HomeListViewAdapter adapter = new HomeListViewAdapter(this, mItems);

            mListView.Adapter = adapter;
        }
    }
}