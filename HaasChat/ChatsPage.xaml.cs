﻿using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
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
            bool waitUntilClosed = false;
            HaasPopup haasPopup = new HaasPopup();
            if (username == "nullname")
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PushPopupAsync(haasPopup);
                });
            }
            if (waitUntilClosed)
                await haasPopup.PopupClosedTask;
            var _username = Preferences.Get("username", "nullname");
            User user = new User();
            user.UserName = _username;
            user.chats = new List<string>();
            username = _username;
            await DC.newUser(user);
            user = await DC.getUser(_username);
            if (user.chats != null)
            {
                var list = await DC.GetAllChat(user);
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
            _lstx.BindingContext = await DC.GetAllChat(user);
            _lstx.IsRefreshing = false;
        }

        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {

        }

        public async void _lstx_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_lstx.SelectedItem != null)
            {
                var selectChatRoom = _lstx.SelectedItem as ChatRoom;
                //MessagingCenter.Send<ChatsPage, ChatRoom>(this,"ChatRoomProp", selectChatRoom);
                await Navigation.PushAsync(new A_ChatPage(selectChatRoom));
                // Navigation.PushAsync(new A_ChatPage());

            }
        }
    }
}