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
using SQLite;
using Android.Util;
using S00144297MobileDev.Models;
using System.IO;

namespace S00144297MobileDev.DataHelper
{
    public class Database
    {
        /*
        -----------------------------
                 Tutorials
        -----------------------------
        1. https://components.xamarin.com/gettingstarted/sqlite-net
        2. https://www.youtube.com/watch?v=3AVPhip842M
        3. https://www.codeproject.com/Articles/792883/Using-Sqlite-in-a-Xamarin-Android-Application-Deve
     */


        //Create Database
        public void CreateDatabase()
        {
            try
            {
                string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "S00144297.db");
                var db = new SQLiteConnection(dbPath);

                //Create tables if they do not exist in the database
                db.CreateTable<User>();
                db.CreateTable<Post>();
            }
            catch (Exception ex)
            {
                Log.Info("SQLiteException ", ex.Message);
            }
        }



        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

        //Insert User
        public bool insertUserIntoDatabase(User user)
        {
            //Check if user is already registered
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    con.Insert(user);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException insertUserIntoDatabase ", ex.Message);
                return false;
            }
        }

        //Insert Post
        public bool insertPostIntoDatabase(Post post)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    con.Insert(post);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException insertPostIntoDatabase ", ex.Message);
                return false;
            }
        }

        //List of Posts
        public List<Post> getAllPosts()
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    return con.Table<Post>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException getAllPosts ", ex.Message);
                return null;
            }
        }

        //Update Post
        public bool updatePost(Post post)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    con.Query<Post>("UPDATE Post  set ActivityTitle=?, ActivitySport=?, ActivityDate=?, ActivityTime=?, ActivityTown=?, ActivityAddress=?, ActivityPhone=?, ActivityEmail=?, ActivityDetails=? WHERE ActivityID=?", post.ActivityTitle, post.ActivitySport, post.ActivityDate, post.ActivityTime, post.ActivityTown, post.ActivityAddress, post.ActivityPhone, post.ActivityEmail, post.ActivityDetails, post.ActivityID);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException insertUserIntoDatabase ", ex.Message);
                return false;
            }
        }

        //Delete Post
        public bool deletePost(int id)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    Post post = con.Query<Post>("SELECT * FROM Post WHERE ActivityID=? ", id).FirstOrDefault();

                    con.Delete(post);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException deletePost ", ex.Message);
                return false;
            }
        }

        //Select single Post
        public bool selectPost(int? id)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    con.Query<Post>("SELECT * FROM Post WHERE ActivityID=? ", id);

                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException insertUserIntoDatabase ", ex.Message);
                return false;
            }
        }

        //Select User Posts
        public List<Post> userPosts(int? id)
        {
            try
            {
                using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                {
                    return con.Query<Post>("SELECT * FROM Post WHERE OwnerID=? ", id);
                }
            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException getAllPosts ", ex.Message);
                return null;
            }
        }

        //User Custom Search Posts
        public List<Post> userSearch(string search, string sport)
        {
            try
            {
                //Search term and sport
                if (search != "" && sport != "")
                {
                    using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                    {
                        return con.Query<Post>("SELECT * FROM Post WHERE ActivityTitle=? AND ActivitySport=? ", search, sport);
                    }
                }

                //Search term no sport
                else if (search != null && sport == null)
                {
                    using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                    {
                        return con.Query<Post>("SELECT * FROM Post WHERE ActivityTitle=?", search);
                    }
                }

                //No search term just sport
                else if (search == null && sport != null)
                {
                    using (var con = new SQLiteConnection(System.IO.Path.Combine(folder, "S00144297.db")))
                    {
                        return con.Query<Post>("SELECT * FROM Post WHERE ActivitySport=?", sport);
                    }
                }

                else
                    return null;


            }
            catch (SQLiteException ex)
            {
                Log.Info("SQLiteException getAllPosts ", ex.Message);
                return null;
            }
        }
    }
}