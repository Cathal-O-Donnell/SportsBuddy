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
using static Android.App.DatePickerDialog;
using S00144297MobileDev.Models;
using System.IO;
using SQLite;
using Android.Util;

namespace S00144297MobileDev
{
    [Activity(Label = "NewPostActivity", Theme = "@style/CustomActionBarTheme")]
    public class NewPostActivity : Activity, IOnDateSetListener
    {
        //Date
        private const int Date_Dialog = 1;
        private int year = DateTime.Now.Year, month = DateTime.Now.Month - 1, day = DateTime.Now.Day;

        //Time
        private int hour;
        private int minute;
        const int TIME_DIALOG_ID = 0;

        public int UserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {

            //Retrieve the current users ID
            UserId = GlobalVariables.currentUserId;

            string page = "";

            base.OnCreate(savedInstanceState);

            //Link this activity to the New Post layout page
            SetContentView(Resource.Layout.NewPost);

            LinearLayout newPostLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutNewPost);

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            newPostLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));

            //Prevent the keyboard opening automatically when the view is loaded
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            //Spinners
            var sportsSpinner = FindViewById<Spinner>(Resource.Id.lstNewPostSport);
            var townsSpinner = FindViewById<Spinner>(Resource.Id.lstNewPostTown);

            //Spinner Prompt
            sportsSpinner.Prompt = "Filter by sport";
            townsSpinner.Prompt = "Filter by town";

            //Sports Adapter
            var sportsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.SportList, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sportsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            sportsSpinner.Adapter = sportsAdapter;

            //Towns Adapter
            var townsAdapter = ArrayAdapter.CreateFromResource(this, Resource.Array.TownList, Android.Resource.Layout.SimpleSpinnerDropDownItem);
            townsAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            townsSpinner.Adapter = townsAdapter;

            //Inputs
            EditText Title = FindViewById<EditText>(Resource.Id.tbxNewPostTitle);
            EditText Details = FindViewById<EditText>(Resource.Id.tbxNewPostDetails);
            EditText Address = FindViewById<EditText>(Resource.Id.tbxNewPostAddress);
            EditText Phone = FindViewById<EditText>(Resource.Id.tbxNewPostContactNumber);
            EditText Email = FindViewById<EditText>(Resource.Id.tbxNewPostContactEmail);

            //Time
            Button btnTimePicker = FindViewById<Button>(Resource.Id.btnNewPostTime);
            btnTimePicker.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            //DatePicker button
            Button datePickerButton = FindViewById<Button>(Resource.Id.btnNewPostDate);
            datePickerButton.Click += delegate
            {
                ShowDialog(Date_Dialog);
            };

            //Create Button Click
            Button createButton = FindViewById<Button>(Resource.Id.btnNewPostCreate);
            createButton.Click += delegate
            {
                //Make sure that all the required fields have been filled in

                //Create the new post object
                Post post = new Post();

                post.ActivityTitle = Title.Text;
                post.ActivityDetails = Details.Text;
                post.ActivityAddress = Address.Text;
                post.ActivityPhone = Phone.Text;
                post.ActivityEmail = Email.Text;
                post.ActivityDate = DateTime.Parse(datePickerButton.Text).Date;
                post.ActivityTime = DateTime.Parse(btnTimePicker.Text);
                post.ActivitySport = sportsSpinner.SelectedItem.ToString();
                post.ActivityTown = townsSpinner.SelectedItem.ToString();
                post.OwnerID = UserId;

                //Add the new Post to the database
                try
                {
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
                    var db = new SQLiteConnection(dbPath);
                    var data = db.Table<Post>();

                    //Add new post to the database
                    db.Insert(post);
                }
                catch (Exception ex)
                {
                    Log.Info("SQLiteException insert post to database ", ex.Message);
                    throw;
                }

                var homeIntent = new Intent(this, typeof(HomeActivity));
                homeIntent.PutExtra("currentUserId", UserId);
                OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);

                StartActivity(homeIntent);
            };

            //Navigation
            #region            
            //Nav
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            //Set the Pressed state of the current nav button to true
            LinearLayout navNewPost = FindViewById<LinearLayout>(Resource.Id.navNewPost);
            navNewPost.Pressed = true;

            //Nav
            //Home
            LinearLayout navHome = FindViewById<LinearLayout>(Resource.Id.navHome);
            ImageView navHomeimg = FindViewById<ImageView>(Resource.Id.imgNavHome);

            //Search
            LinearLayout navSearch = FindViewById<LinearLayout>(Resource.Id.navSearch);
            ImageView navSearchimg = FindViewById<ImageView>(Resource.Id.imgNavSearch);

            //New Post
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

        public void OnDateSet(DatePicker view, int year, int monthOfYear, int dayOfYear)
        {
            this.year = year;
            this.month = monthOfYear;
            this.day = dayOfYear;

            string selectedDate = string.Format("{0}/{1}/{2}", day, month, year);

            Button datePickerButton = FindViewById<Button>(Resource.Id.btnNewPostDate);
            datePickerButton.Text = selectedDate;
        }

        //Date/Time Dialog
        protected override Dialog OnCreateDialog(int id)
        {
            switch (id)
            {
                case Date_Dialog:
                    {
                        return new DatePickerDialog(this, this, year, month, day);
                    }
                case TIME_DIALOG_ID:
                    {
                        return new TimePickerDialog(this, TimePickerCallback, hour, minute, false);
                    }

                default:
                    break;
            }

            return null;
        }

        private void TimePickerCallback(object sender, TimePickerDialog.TimeSetEventArgs e)
        {
            hour = e.HourOfDay;
            minute = e.Minute;

            string time = string.Format("{0}:{1}", hour, minute.ToString().PadLeft(2, '0'));
            Button btnTimePicker = FindViewById<Button>(Resource.Id.btnNewPostTime);
            btnTimePicker.Text = time;
        }

        //Sport Spinner new item selected
        private void sportsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var x = sender as Spinner;
            var selectedText = x.GetItemAtPosition(e.Position).ToString();

            if (selectedText != "Select Sport")
            {
                Toast.MakeText(this, "You selected " + x.GetItemAtPosition(e.Position), ToastLength.Short).Show();
            }
        }

        //Town Spinner new item selected
        private void townsSpinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            var x = sender as Spinner;
            var selectedText = x.GetItemAtPosition(e.Position).ToString();

            if (selectedText != "Select Town")
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
                    OverridePendingTransition(Resource.Animation.slideLeft, Resource.Animation.fadeOut);
                    break;

                case "new":
                    var newpostAct = new Intent(this, typeof(NewPostActivity));
                    StartActivity(newpostAct);
                    OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
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
