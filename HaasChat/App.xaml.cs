using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            App.Current.MainPage = new NavigationPage(new ChatsPage());
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
