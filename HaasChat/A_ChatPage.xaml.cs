using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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
            /*MessagingCenter.Subscribe<ChatsPage, ChatRoom>(this, "ChatRoomProp", (page, data) =>
            {
                room = data;

            });*/
            
            bilgi.Text= $"Ayrıntılı bilgi ({room.Name})";
            if (room.Admins.Contains(Preferences.Get("username", "username")))
            {
                add.IsEnabled = true;
            }
            else
            {
                add.IsEnabled = false;
            }
            this.room = room;
            _lChat.BindingContext = chat.chats(room.Key);
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //chat.SendMessageş()
            var chatobj=new Chat()
            {
                Message=_entMessage.Text,
                UserName=Preferences.Get("username","username"),
                Date=DateTime.Now.ToShortTimeString()
            };
            await chat.SendMessage(chatobj,this.room.Key);
            _entMessage.Text = "";
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
}