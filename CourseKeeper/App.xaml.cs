using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CourseKeeper.Services;
using CourseKeeper.Views;
using System.IO;
using CourseKeeper.Models;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using Plugin.LocalNotifications;

namespace CourseKeeper
{
    public partial class App : Application
    {
		static CourseKeeperDatabase database;

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
    
        }


        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

		public static CourseKeeperDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new CourseKeeperDatabase(
						DependencyService.Get<IFileHelper>().GetLocalFilePath("CourseKeeperSQLite.db3"));
				}
				return database;
			}
		}

        public void SetNotify(bool enabled, string title, string body, string type, int id, DateTime notifyTime)
        {
            // This absurd thing should create a pretty close to unique id number for a given course, assessment, etc
            // by combining the type + id into a string and hashing it, then converting the has to a numeric.
            int NotifID = BitConverter.ToInt32(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(type + id.ToString())), 0);
            if (enabled)
            {

                CrossLocalNotifications.Current.Show(title, body, NotifID, notifyTime);
            }
            else
            {
                CrossLocalNotifications.Current.Cancel(NotifID);
            }
        }

    }
}
