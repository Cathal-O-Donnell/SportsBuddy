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
using S00144297MobileDev.DataHelper;
using S00144297MobileDev.Models;
using System.IO;
using SQLite;

namespace S00144297MobileDev
{
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity
    {
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Register);

            //Buttons
            Button LoginButton = FindViewById<Button>(Resource.Id.btnRSignIn);
            Button RegisterButton = FindViewById<Button>(Resource.Id.btnRRegister);

            //Login button click event
            LoginButton.Click += delegate
            {
                //Redirect to Login page
                var mainAct = new Intent(this, typeof(MainActivity));
                StartActivity(mainAct);
                OverridePendingTransition(Resource.Animation.slideRight, Resource.Animation.fadeOut);
            };

            //Register button click event
            RegisterButton.Click += delegate
            {
                EditText email = FindViewById<EditText>(Resource.Id.tbxRegisterEmail);
                string inputemail = email.Text.ToString();

                TextView emailValidation = FindViewById<TextView>(Resource.Id.txtRegisterEmailValidation);
                var emailvalidate = isValidEmail(inputemail);

                EditText password = FindViewById<EditText>(Resource.Id.tbxRegisterPassword);
                string inputPassword = password.Text.ToString();

                EditText passwordConfirm = FindViewById<EditText>(Resource.Id.tbxRegisterConfirmPassword);
                string inputPasswordConfirm = passwordConfirm.Text.ToString();

                TextView passwordValidation = FindViewById<TextView>(Resource.Id.txtRegisterPasswordValidation);

                var passwordLengthValidate = isValidPasswordLength(inputPassword);
                var passwordCompareValidate = isValidPasswordCompare(inputPassword, inputPasswordConfirm);

                //Empty Email field
                if (inputemail == "")
                {
                    emailValidation.Text = "You must enter an email";
                }
                else
                {
                    //Invalid Email
                    if (emailvalidate == false)
                    {
                        emailValidation.Text = "This is not a valid email";
                    }
                }

                //Valid Email remove validation messages
                if (inputemail != "" && emailvalidate == true)
                {
                    emailValidation.Text = "";
                }

                //Empty Password field
                if (inputPassword == "")
                {
                    passwordValidation.Text = "You must enter an password";
                }
                else
                {
                    //Password Invalid
                    if (passwordLengthValidate == false)
                    {
                        passwordValidation.Text = "This is not a valid password";
                    }

                    //Passwords do not match
                    if (passwordCompareValidate == false)
                    {
                        passwordValidation.Text = "These passwords do not match";
                    }
                }

                //Valid Email remove validation messages
                if (passwordLengthValidate == true && passwordCompareValidate == true)
                {
                    passwordValidation.Text = "";
                }

                //Password and Email entered
                if (inputemail != "" && inputPassword != "" && emailvalidate == true && passwordCompareValidate == true && passwordLengthValidate == true)
                {
                    try
                    {
                        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
                        var db = new SQLiteConnection(dbPath);
                        var data = db.Table<User>();

                        User newUser = new User();

                        newUser.UserEmail = inputemail;
                        newUser.UserPassword = inputPassword;

                        //Add new user to the database
                        db.Insert(newUser);
                    }
                    catch (Exception)
                    {

                        throw;
                    }

                    //Redirect to Home page
                    var homeAct = new Intent(this, typeof(SearchActivity));
                    StartActivity(homeAct);
                    OverridePendingTransition(Resource.Animation.fadeIn, Resource.Animation.fadeOut);
                }
              
            };
        }

        //Email Validation - Taken from tutorial: http://www.c-sharpcorner.com/article/how-to-validate-an-email-address-in-xamarin-android-app-using-visual-studio-2015/
        public bool isValidEmail(string email)
        {
            return Android.Util.Patterns.EmailAddress.Matcher(email).Matches();
        }

        //Password Length Validation
        public bool isValidPasswordCompare(string password, string confirmPassword)
        {
            //Check if passwords match
            if (password == confirmPassword)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        //Password Length Validation
        public bool isValidPasswordLength(string password)
        {
            //Check if password has more than 6 characters
            if (password.Length < 6)
            {
                return false;
            }

            else
            {
                return true;
            }          
        }
    }
}