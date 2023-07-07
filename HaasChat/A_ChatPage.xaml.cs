﻿using System;
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
            this.room = room;
            _lChat.BindingContext = chat.chats(room.Key);
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //chat.SendMessage()
            var chatobj=new Chat()
            {
                Message=_entMessage.Text,
                UserName=Preferences.Get("username","username"),
                Date=DateTime.Now.ToShortTimeString()
            };
            await chat.SendMessage(chatobj,this.room.Key);
            _entMessage.Text = "";
        }
    }
}