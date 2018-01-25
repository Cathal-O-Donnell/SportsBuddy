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
    [Activity(Label = "SearchActivity", Theme = "@style/CustomActionBarTheme")]
    public class SearchActivity : Activity
    {
        public int UserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            //Retrieve the current users ID
            UserId = GlobalVariables.currentUserId;

            string page = "";

            base.OnCreate(savedInstanceState);

            //Link this activity to the Search layout page
            SetContentView(Resource.Layout.Search);

            LinearLayout searchLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutSearch);

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            searchLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));

            //Prevent the keyboard opening automatically when the view is loaded
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

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

            //Spinners
            var sportsSpinner = FindViewById<Spinner>(Resource.Id.lstSearchSports);
            var townsSpinner = FindViewById<Spinner>(Resource.Id.lstSearchTowns);

            //Spinner Prompt
            sportsSpinner.Prompt = "Filter by sport";
            townsSpinner.Prompt = "Filter by town";

            //Spinner Item Selected
            sportsSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(sportsSpinner_ItemSelected);
            //townsSpinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(townsSpinner_ItemSelected);

            //Sports Adapter
            var sportsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.SportList, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sportsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sportsSpinner.Adapter = sportsAdapter;

            //Towns Adapter
            var townsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.TownList, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            townsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            townsSpinner.Adapter = townsAdapter;

            //Button
            Button searchButton = FindViewById<Button>(Resource.Id.btnSearch);

            //Search button click event
            searchButton.Click += delegate
            {

                var registerAct = new Intent(this, typeof(SearchResultActivity));
                StartActivity(registerAct);
                OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
            };


        }

        private void sportsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var x = sender as Spinner;
            var selectedText = x.GetItemAtPosition(e.Position).ToString();

            if (selectedText != "Select Sport")
            {
                Toast.MakeText(this, "You selected " + x.GetItemAtPosition(e.Position), ToastLength.Short).Show();
            }
        }

        public void NavCicked(string page)
        {
            switch (page)
            {
                case "home":
                    var homeAct = new Intent(this, typeof(HomeActivity));
                    StartActivity(homeAct);
                    OverridePendingTransition(Resource.Animation.slideLeft, Resource.Animation.fadeOut);
                    break;

                case "search":
                    var searchAct = new Intent(this, typeof(SearchActivity));
                    StartActivity(searchAct);
                    OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
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

//Spinner Tutorial: https://www.youtube.com/watch?v=sXEtve_95LI