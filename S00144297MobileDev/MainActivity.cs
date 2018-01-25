using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;
using S00144297MobileDev.DataHelper;
using System.IO;
using SQLite;
using S00144297MobileDev.Models;
using Android.Util;
using Java.Lang;

namespace S00144297MobileDev
{
    [Activity(Label = "S00144297MobileDev", MainLauncher = true,  Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //Create Database
            Database dbHelper = new Database();
            dbHelper.CreateDatabase();

            //Buttons
            Button RegisterButton = FindViewById<Button>(Resource.Id.btnMainRegister);
            Button LoginButton = FindViewById<Button>(Resource.Id.btnMainLogIn);

            //Login button click event
            LoginButton.Click += delegate
            {
                EditText email = FindViewById<EditText>(Resource.Id.tbxLoginEmail);
                string inputemail = email.Text.ToString();

                TextView emailValidation = FindViewById<TextView>(Resource.Id.txtLoginEmailValidation);
                var emailvalidate = isValidEmail(inputemail);

                EditText password = FindViewById<EditText>(Resource.Id.tbxLoginPassword);
                string inputPassword = password.Text.ToString();
                TextView passwordValidation = FindViewById<TextView>(Resource.Id.txtLoginPasswordValidation);

                try
                {
                    string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
                    var db = new SQLiteConnection(dbPath);
                    var data = db.Table<User>();

                    //Check if there is a user in database that matches the details entered
                    var login = data.Where(x => x.UserEmail == inputemail && x.UserPassword == inputPassword).FirstOrDefault();

                    //User has successfully logged in, redirect them to the home page
                    if (login != null)
                    {        
                        //Store the current users id               
                        GlobalVariables.currentUserId = login.UserID;
                                                
                        var homeAct = new Intent(this, typeof(HomeActivity));
                        StartActivity(homeAct);
                        OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
                    }

                    else
                    {
                        Toast.MakeText(this, "You have entered an incorrect email or password", ToastLength.Short).Show();
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Info("SQLiteException ", ex.Message);
                }



                ////Empty Email field
                //if (inputemail == "")
                //{
                //    emailValidation.Text = "You must enter an email";
                //}
                //else
                //{
                //    //Invalid Email
                //    if (emailvalidate == false)
                //    {
                //        emailValidation.Text = "This is not a valid email";
                //    }
                //}

                ////Valid Email remove validation messages
                //if (inputemail != "" && emailvalidate == true)
                //{
                //    emailValidation.Text = "";
                //}

                ////Empty Password field
                //if (inputPassword == "")
                //{
                //    passwordValidation.Text = "You must enter an password";
                //}
                ////Valid Password remove validation message
                //else
                //{
                //    passwordValidation.Text = "";
                //}

                ////Password and Email entered
                //if (emailvalidate == true)
                //{
                //    //Check if user exists in Database
                //    try
                //    {
                //        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
                //        var db = new SQLiteConnection(dbPath);
                //        var data = db.Table<User>();

                //        var login = data.Where(x => x.UserEmail == inputemail && x.UserPassword == inputPassword).FirstOrDefault();

                //        if (login != null)
                //        {
                //            //User has successfully logged in, redirect them to the home page
                //            var homeAct = new Intent(this, typeof(HomeActivity));
                //            StartActivity(homeAct);
                //            OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
                //        }

                //        else
                //        {
                //            Toast.MakeText(this, "You have entered an incorrect email or password", ToastLength.Short).Show();
                //        }
                //    }
                //    catch (System.Exception ex)
                //    {
                //        Log.Info("SQLiteException ", ex.Message);
                //    }

                //    //Redirect to Home page
                //    //var homeAct = new Intent(this, typeof(HomeActivity));
                //    //StartActivity(homeAct);
                //    //OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
                //}
            };                    

        //Register button click event
        RegisterButton.Click += delegate
            {
                var registerAct = new Intent(this, typeof(RegisterActivity));
                StartActivity(registerAct);
                OverridePendingTransition(Resource.Animation.slideLeft, Resource.Animation.fadeOut);
            };
        }

        //Email Validation - Taken from tutorial: http://www.c-sharpcorner.com/article/how-to-validate-an-email-address-in-xamarin-android-app-using-visual-studio-2015/
        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        ////Create Database
        //private void CreateDatabase()
        //{
        //    try
        //    {
        //        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
        //        var db = new SQLiteConnection(dbPath);

        //        //Create tables if they do not exist in the database
        //        db.CreateTable<User>();
        //        db.CreateTable<Post>();
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.Info("SQLiteException ", ex.Message);
        //    }
        //}
    }
}

/*
Notes
1. editText_style: https://www.codeproject.com/Tips/845894/How-to-make-EditText-With-Border-and-Gradient-Back 

*/
