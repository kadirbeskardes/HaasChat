using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class A_ChatPage : ContentPage
    {
        DBChat chat = new DBChat();
        ChatRoom room = new ChatRoom();
        static ObservableCollection<Chat> chatlist=new ObservableCollection<Chat>();
        internal A_ChatPage(ChatRoom room)
        {
            InitializeComponent();
            bilgi.Text = $"Ayrıntılı bilgi ({room.Name})";
            if (room.Admins.Contains(Preferences.Get("username", "username")))
            {
                add.IsEnabled = true;
            }
            else
            {
                add.IsEnabled = false;
            }
            this.Resources.Add("UsernameToColorConverter", new UsernameToColorConverter());
            this.Resources.Add("IsMyMessageToColorConverter", new IsMyMessageToColorConverter());
            this.Resources.Add("IsMyMessageToHorizontalOptionsConverter", new IsMyMessageToHorizontalOptionsConverter());
            this.Resources.Add("IsMyMessageToTextAlignmentConverter", new IsMyMessageToTextAlignmentConverter());
            this.room = room;
            ChatListView.BindingContext = this;
            chatlist = chat.chats(room.Key);
            ChatListView.ItemsSource = chatlist;
            ScrollToLastItem();
        }


        private async void OnSendMessageButtonClicked(object sender, EventArgs e)
        {
            string newMessageText = MessageEntry.Text;

            if (!string.IsNullOrWhiteSpace(newMessageText))
            {
                await chat.SendMessage((new Chat
                {
                    UserName = Preferences.Get("username", "username"),
                    Message = newMessageText,
                    Date = DateTime.Now
                }), this.room.Key);
                MessageEntry.Text = string.Empty;
            }
            await ScrollToLastItem();
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new HaasPopup(this.room.Key));
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChatDetailPage(this.room.Key));
        }
        private async Task ScrollToLastItem()
        {
            if (chatlist.Count > 0)
            {
                var lastItem = chatlist[chatlist.Count - 1];
                await Task.Delay(100); 
                ChatListView.ScrollTo(lastItem, ScrollToPosition.End, false);
            }
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
    public class IsMyMessageToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string username = value as string;
            if (username == Preferences.Get("username", "username"))
            {
                return Color.FloralWhite;
            }
            else return Color.LightGray;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsMyMessageToHorizontalOptionsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string username = value as string;
            if (username == Preferences.Get("username", "username"))
            {
                return LayoutOptions.EndAndExpand;
            }else return LayoutOptions.StartAndExpand;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsMyMessageToTextAlignmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string username = value as string;
            if (username == Preferences.Get("username", "username"))
            {
                return TextAlignment.End;
            }return TextAlignment.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}