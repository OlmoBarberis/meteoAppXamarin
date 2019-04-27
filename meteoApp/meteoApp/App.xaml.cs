using meteoApp.Database;
using meteoApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace meteoApp
{
    public partial class App : Application
    {
        static MeteoDatabase database;

        public static MeteoDatabase Database
        {
            get
            {
                if (database == null)
                    database = new MeteoDatabase();
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            var nav = new NavigationPage(new MeteoListPage())
            {
                BarBackgroundColor = Color.LightGreen,
                BarTextColor = Color.Wheat
            };
            MainPage = nav;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
