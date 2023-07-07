using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChat : ContentPage
    {
        public AddChat()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new DBChat();
            await db.saveChat(new ChatRoom()
            {
                Name = _chatName.Text
            });

            User user = await db.getUser(Preferences.Get("username", "nullname"));
            if (user.chats == null)
            {
                var list = new List<string>();
                list.Add(_chatName.Text);
                user.chats= list;
            }
            else
            {
                user.chats.Add(_chatName.Text);
            }
            await db.newUser(user);
            await Navigation.PopAsync();
        }
    }
}