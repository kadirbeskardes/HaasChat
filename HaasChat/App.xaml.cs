using System.Collections.Generic;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HaasChat
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (Preferences.Get("isLogged","false") == "false")
            {
                App.Current.MainPage = new NavigationPage(new SignUp());
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new ChatsPage());
            }
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
