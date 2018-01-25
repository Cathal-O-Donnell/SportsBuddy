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
using Newtonsoft.Json;
using S00144297MobileDev.Models;
using static Android.App.DatePickerDialog;

namespace S00144297MobileDev
{
    [Activity(Label = "EditPostActivity", Theme = "@style/CustomActionBarTheme")]
    public class EditPostActivity : Activity, IOnDateSetListener
    {
        //Date
        private const int Date_Dialog = 1;
        private int year = DateTime.Now.Year, month = DateTime.Now.Month, day = DateTime.Now.Day;

        //Time
        //Time
        private int hour;
        private int minute;
        const int TIME_DIALOG_ID = 0;

        public int UserId;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            string page = "";

            //Retrieve the current users ID
            UserId = GlobalVariables.currentUserId;

            //Link this activity to the New Post layout page
            SetContentView(Resource.Layout.EditPost);

            //Get serializes Post object and deserialize it
            var MyJsonString = Intent.GetStringExtra("post");
            var MyObject = JsonConvert.DeserializeObject<Post>(MyJsonString);

            LinearLayout newPostLayout = FindViewById<LinearLayout>(Resource.Id.linearlayoutEditPost);

            //DatePicker
            Button datePickerButton = FindViewById<Button>(Resource.Id.dpEditPostDate);
            datePickerButton.Click += delegate
            {
                ShowDialog(Date_Dialog);
            };

            Button btnTimePicker = FindViewById<Button>(Resource.Id.btnEditPostTime);
            btnTimePicker.Click += (o, e) => ShowDialog(TIME_DIALOG_ID);
            hour = DateTime.Now.Hour;
            minute = DateTime.Now.Minute;

            //Set Background color for view
            string color = Resources.GetString(Resource.String.BackgroundColor);
            newPostLayout.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));

            //Prevent the keyboard opening automatically when the view is loaded
            Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);

            //Spinners
            var sportsSpinner = FindViewById<Spinner>(Resource.Id.lstEditPostSport);
            var townsSpinner = FindViewById<Spinner>(Resource.Id.lstEditPostTown);

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

            //Set selected items in spinners equal to the edit posts selections

            //Title
            EditText txtTitle = FindViewById<EditText>(Resource.Id.tbxEditPostTitle);
            txtTitle.Text = MyObject.ActivityTitle;

            //Details
            EditText txtDetails = FindViewById<EditText>(Resource.Id.tbxEditPostDetails);
            txtDetails.Text = MyObject.ActivityDetails;

            //Date
            datePickerButton.Text = MyObject.ActivityDate.ToShortDateString();

            //Time
            btnTimePicker.Text = MyObject.ActivityTime.ToShortTimeString();

            //Address
            EditText txtAddress = FindViewById<EditText>(Resource.Id.tbxEditPostAddress);
            txtAddress.Text = MyObject.ActivityAddress;

            //Contact Number
            EditText txtContactNumber = FindViewById<EditText>(Resource.Id.tbxEditPostContactNumber);
            txtContactNumber.Text = MyObject.ActivityPhone;

            //Contact Email
            EditText txtContactEmail = FindViewById<EditText>(Resource.Id.tbxEditPostContactEmail);
            txtContactEmail.Text = MyObject.ActivityEmail;

            //Navigation
            #region            
            //Nav
            ActionBar.SetCustomView(Resource.Layout.action_bar);
            ActionBar.SetDisplayShowCustomEnabled(true);

            //Set the Pressed state of the current nav button to true
            LinearLayout navUser = FindViewById<LinearLayout>(Resource.Id.navUser);
            navUser.Pressed = true;

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
            navUser = FindViewById<LinearLayout>(Resource.Id.navUser);
            ImageView navUserimg = FindViewById<ImageView>(Resource.Id.imgNavUser);

            //Navigation Home button click event
            navHome.Click += delegate
            {
                page = "home";
                NavCicked(page);
            };
            navHomeimg.Click += delegate
            {
                page = "home";
                NavCicked(page);
            };

            //Navigation Search button click event
            navSearch.Click += delegate
            {
                page = "search";
                NavCicked(page);
            };
            navSearchimg.Click += delegate
            {
                page = "search";
                NavCicked(page);
            };

            //Navigation New Post button click event
            navNewPost.Click += delegate
            {
                page = "new";
                NavCicked(page);
            };
            navPostimg.Click += delegate
            {
                page = "new";
                NavCicked(page);
            };

            //Navigation Profile button click event
            navUser.Click += delegate
            {
                page = "user";
                NavCicked(page);
            };
            navUserimg.Click += delegate
            {
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

            Button datePickerButton = FindViewById<Button>(Resource.Id.dpEditPostDate);
            datePickerButton.Text = selectedDate;
        }

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
            Button btnTimePicker = FindViewById<Button>(Resource.Id.btnEditPostTime);
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
