using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class A_ChatPage : ContentPage
    {
        DBChat chat=new DBChat();
        ChatRoom room = new ChatRoom();
        internal A_ChatPage(ChatRoom room)
        {
            InitializeComponent();
            bilgi.Text= $"Ayrıntılı bilgi ({room.Name})";
            if (room.Admins.Contains(Preferences.Get("username", "username")))
            {
                add.IsEnabled = true;
            }
            else
            {
                add.IsEnabled = false;
            }
            this.Resources.Add("UsernameToColorConverter", new UsernameToColorConverter());
            this.room = room;
            ChatListView.BindingContext = this;
            ChatListView.ItemsSource= chat.chats(room.Key);
        }
        
        
        private async void OnSendMessageButtonClicked(object sender, EventArgs e)
        {
            string newMessageText = MessageEntry.Text;

            if (!string.IsNullOrWhiteSpace(newMessageText))
            {
                await chat.SendMessage((new Chat
                {
                    UserName = Preferences.Get("username", "username"), Message = newMessageText, Date = DateTime.Now }),this.room.Key);
                MessageEntry.Text = string.Empty;
            }
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new HaasPopup(this.room.Key));
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatDetailPage(this.room.Key));
        }
    }
    public class UsernameToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string username = value as string;
            if (username == Preferences.Get("username", "username"))
            {
                return Color.DodgerBlue;
            }
            else
            {
                return Color.Red;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}