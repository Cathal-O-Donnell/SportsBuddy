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
using Android.Locations;
using S00144297MobileDev.Models;
using Newtonsoft.Json;
//using Android.Gms.Maps;
//using Android.Gms.Maps.Model;

namespace S00144297MobileDev
{
    [Activity(Label = "PostDetailsActivity", Theme = "@style/CustomActionBarTheme")]
    public class PostDetailsActivity : Activity/*, IOnMapReadyCallback*/
    {
        public int UserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            int currentUserId = Intent.GetIntExtra("currentUserId", 0);
            UserId = currentUserId;

            string page = "";

            base.OnCreate(savedInstanceState);

            //Get serializes Post object and deserialize it
            var MyJsonString = Intent.GetStringExtra("post");
            var MyObject = JsonConvert.DeserializeObject<Post>(MyJsonString);

            var pDate = Intent.GetStringExtra("pDate");
            var pTime = Intent.GetStringExtra("pTime");

            // Set our view from the post details layout resource
            SetContentView(Resource.Layout.PostDetails);

            LinearLayout mainLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutPostDetails);

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            mainLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));


            //Display Details in the view

            //Title
            TextView txtTitle = (TextView)FindViewById(Resource.Id.txPostDetailsTitle);
            txtTitle.Text = MyObject.ActivityTitle;

            //Details
            TextView txtDetails = (TextView)FindViewById(Resource.Id.txPostDetailsDetails);
            txtDetails.Text = MyObject.ActivityDetails;

            //Time
            TextView txtTime = (TextView)FindViewById(Resource.Id.txPostDetailsTime);
            txtTime.Text = pTime;

            //Date
            TextView txtDate = (TextView)FindViewById(Resource.Id.txPostDetailsDate);
            txtDate.Text = pDate;

            //Phone Number
            TextView txtPhone = (TextView)FindViewById(Resource.Id.txPostDetailsPhone);
            txtPhone.Text = MyObject.ActivityPhone;

            //Email
            TextView txtEmail = (TextView)FindViewById(Resource.Id.txPostDetailsEmail);
            txtEmail.Text = MyObject.ActivityEmail;

            //Address
            TextView txtAddress = (TextView)FindViewById(Resource.Id.txPostDetailsAddress);
            txtAddress.Text = MyObject.ActivityAddress;

            //Google map
            //MapFragment mapFrag = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.txPostDetailsGoogleMap);
            //mapFrag.GetMapAsync(this);

            //Navigation
            #region

            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            //Nav
            //Home
            LinearLayout navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
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
        }

        //Navigation
        public void NavCicked(string page)
        {
            switch (page)
            {
                case "home":
                    var homeIntent = new Intent(this, typeof(HomeActivity));
                    homeIntent.PutExtra("currentUserId", UserId);
                    OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);

                    StartActivity(homeIntent);
                    break;

                case "search":
                    var searchIntent = new Intent(this, typeof(SearchActivity));
                    searchIntent.PutExtra("currentUserId", UserId);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);

                    StartActivity(searchIntent);
                    break;

                case "new":
                    var newpostIntent = new Intent(this, typeof(NewPostActivity));
                    newpostIntent.PutExtra("currentUserId", UserId);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);

                    StartActivity(newpostIntent);
                    break;

                case "user":
                    var userIntent = new Intent(this, typeof(MyProfileActivity));
                    userIntent.PutExtra("currentUserId", UserId);
                    OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);

                    StartActivity(userIntent);
                    break;

                default:
                    break;
            }
        }

        //public void OnMapReady(GoogleMap googleMap)
        //{
        //    MarkerOptions makerOptions = new MarkerOptions();
        //    makerOptions.SetPosition(new LatLng(16.03, 108));
        //    makerOptions.SetTitle("My Position");
        //    googleMap.AddMarker(makerOptions);

        //    //Google Map UI settings
        //    googleMap.UiSettings.ZoomControlsEnabled = true;
        //    googleMap.UiSettings.CompassEnabled = true;
        //    googleMap.MoveCamera(CameraUpdateFactory.ZoomIn());
        //}
    }
}