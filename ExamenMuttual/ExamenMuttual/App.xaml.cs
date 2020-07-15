using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenMuttual.Services;
using ExamenMuttual.Views;
using ExamenMuttual.Shared.Helpers;
using System.IO;

namespace ExamenMuttual
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        static SQLiteHelper db;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = ServiceProvider.GetService<MainPage>();
        }
       
        protected override void OnStart()
        {
        }
        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
