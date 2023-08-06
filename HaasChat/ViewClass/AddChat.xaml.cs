using System;
using System.Collections.Generic;
using HaasChat.Model;
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
            User user = new User();
            user = await db.getUser(Preferences.Get("username", "null"));
            if (user.chats == null)
            {
                user.chats = new List<string>();
            }
            user.chats.Add(await db.NewChat(_chatName.Text,user.UserName));
            await db.newUser(user);
            await Navigation.PopAsync();
            /*await db.saveChat(new ChatRoom()
            {
                Name = _chatName.Text
            });

            User user = await db.getUser(Preferences.Get("username", "nullname"));
            if (user.chats == null)
            {
                var list = new ObservableCollection<string>();
                list.Add(_chatName.Text);
                user.chats= list;
            }
            else
            {
                user.chats.Add(_chatName.Text);
            }
            await db.newUser(user);*/
        }
    }
}