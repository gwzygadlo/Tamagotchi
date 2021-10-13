using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Tamagotchi
{
    public partial class App : Application
    {
        public static string _dbFilePath { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public App(string filePath)
        {
            InitializeComponent();

            MainPage = new MainPage();

            _dbFilePath = filePath;
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
