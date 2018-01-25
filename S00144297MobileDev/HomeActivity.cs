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
using Android.Util;
using System.IO;
using SQLite;
using S00144297MobileDev.DataHelper;

namespace S00144297MobileDev
{
    [Activity(Label = "HomeActivity", Theme = "@style/CustomActionBarTheme")]
    public class HomeActivity : Activity
    {
        private List<Post> mItems;
        private ListView mListView;

        public int UserId;

        protected override void OnCreate(Bundle bundle)
        {
            //Retrieve the current users ID
            UserId = GlobalVariables.currentUserId;

            string page = "";

            base.OnCreate(bundle);

            //Link this activity to the Home layout page
            SetContentView(Resource.Layout.Home);

            LinearLayout homeLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutHome);

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            homeLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));

            //Navigation
            #region
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            //Set the Pressed state of the current nav button to true
            LinearLayout navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            navHome.Pressed = true;

            //Nav
            //Home
            //LinearLayout navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            ImageView navHomeimg = FindViewById<ImageView>(Resource.Id.imgNavHome);

            //Search
            LinearLayout navSearch = FindViewById<LinearLayout>(Resource.Id.navSearch);
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
                //var homeAct = new Intent(this, typeof(HomeActivity));
                //StartActivity(homeAct);
                page = "home";
                NavCicked(page);
            };
            navHomeimg.Click += delegate
            {
                //var homeAct = new Intent(this, typeof(HomeActivity));
                //StartActivity(homeAct);
                page = "home";
                NavCicked(page);
            };

            //Navigation Search button click event
            navSearch.Click += delegate
            {
                //var searchAct = new Intent(this, typeof(SearchActivity));
                //StartActivity(searchAct);
                page = "search";
                NavCicked(page);
            };
            navSearchimg.Click += delegate
            {
                //var searchAct = new Intent(this, typeof(SearchActivity));
                //StartActivity(searchAct);
                page = "search";
                NavCicked(page);
            };

            //Navigation New Post button click event
            navNewPost.Click += delegate
            {
                //var newpostAct = new Intent(this, typeof(NewPostActivity));
                //StartActivity(newpostAct);
                page = "new";
                NavCicked(page);
            };
            navPostimg.Click += delegate
            {
                //var newpostAct = new Intent(this, typeof(NewPostActivity));
                //StartActivity(newpostAct);
                page = "new";
                NavCicked(page);
            };

            //Navigation Profile button click event
            navUser.Click += delegate
            {
                //var profileAct = new Intent(this, typeof(MyProfileActivity));
                //StartActivity(profileAct);
                page = "user";
                NavCicked(page);
            };
            navUserimg.Click += delegate
            {
                //var profileAct = new Intent(this, typeof(MyProfileActivity));
                //StartActivity(profileAct);
                page = "user";
                NavCicked(page);
            };
            #endregion


            mListView = FindViewById<ListView>(Resource.Id.lstHomeItems);

            mItems = new List<Post>();

            //Pull the posts from the database
            Database dbHelper = new Database();
            mItems = dbHelper.getAllPosts();

            HomeListViewAdapter adapter = new HomeListViewAdapter(this, mItems);

            mListView.Adapter = adapter;
        }

        public void NavCicked(string page)
        {
            switch (page)
            {
                case "home":
                    var homeAct = new Intent(this, typeof(HomeActivity));
                    StartActivity(homeAct);
                    OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
                    break;

                case "search":
                    var searchAct = new Intent(this, typeof(SearchActivity));
                    StartActivity(searchAct);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);
                    break;               

                case "new":
                    var newpostAct = new Intent(this, typeof(NewPostActivity));
                    StartActivity(newpostAct);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);
                    break;

                case "user":
                    var userAct = new Intent(this, typeof(MyProfileActivity));
                    StartActivity(userAct);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);
                    break;

                default:
                    break;
            }
        }
    }
}

//ListView tutorial used: https://www.youtube.com/watch?v=zZHLHrkvUt0
//Navigation Bar Tutorial used: https://www.youtube.com/watch?v=JM-NkUwng1Y