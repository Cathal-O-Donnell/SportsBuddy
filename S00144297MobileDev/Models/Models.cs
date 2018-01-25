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

namespace S00144297MobileDev.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        [Unique]
        public string UserEmail { get; set; }

        [MaxLength(15)]
        public string UserPassword { get; set; }
    }

    public class Post
    {
        [PrimaryKey, AutoIncrement]
        public int ActivityID { get; set; }

        [Indexed]
        public int OwnerID { get; set; }

        public string ActivityTitle { get; set; }

        public string ActivitySport { get; set; }

        public DateTime ActivityDate { get; set; }

        public DateTime ActivityTime { get; set; }

        public string ActivityTown { get; set; }

        public string ActivityAddress { get; set; }

        public string ActivityPhone { get; set; }

        public string ActivityEmail { get; set; }

        public string ActivityDetails { get; set; }
    }

    public static class GlobalVariables
    {
        public static int currentUserId { get; set; }
    }
}