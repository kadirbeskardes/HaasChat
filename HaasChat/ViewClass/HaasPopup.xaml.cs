﻿using HaasChat.Model;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.XPath;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HaasPopup : PopupPage
    {
        static string _key;
        public HaasPopup(string key)
        {
            _key = key;
            InitializeComponent();

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            DBChat chat = new DBChat();
            User user = await chat.getUser(_username.Text);
            
            if (user != null)
            {
                if (user.chats == null)
                {
                    user.chats = new List<string>();
                }
                user.chats.Add(_key);
                await chat.addParToChat(_username.Text,_key);
                await chat.newUser(user);
                await Navigation.PopPopupAsync();
            }

        }
    }
}