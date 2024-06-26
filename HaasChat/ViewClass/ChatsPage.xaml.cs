﻿using HaasChat.Model;
using ImageCircle.Forms.Plugin.Abstractions;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatsPage : ContentPage
    {
        User user;
        string username = string.Empty;
        DBChat DC = new DBChat();

        public ChatsPage()
        {
            InitializeComponent();
            username = Preferences.Get("username", "nullname");
            user = new User();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            user = await DC.getUser(username);

            if (user.chats != null)
            {
                var list = await DC.GetAllChat(user.chats);
                _lstx.ItemsSource= list;
            }
            else
            {
                var list = new List<string>();
                _lstx.BindingContext = list;
            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddChat());
        }

        private async void ListView_Refreshing(object sender, EventArgs e)
        {
            user = await DC.getUser(username);

            if (user.chats != null)
            {
                var list = await DC.GetAllChat(user.chats);
                _lstx.BindingContext = list;
            }

            _lstx.IsRefreshing = false;
        }

        public async void _lstx_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_lstx.SelectedItem != null)
            {
                var selectChatRoom = _lstx.SelectedItem as ChatRoom;
                await Navigation.PushAsync(new A_ChatPage(selectChatRoom));
            }
        }
    }
}