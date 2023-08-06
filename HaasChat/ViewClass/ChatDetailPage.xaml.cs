using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HaasChat.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatDetailPage : ContentPage
    {
        static ChatRoom _room = new ChatRoom();
        static DBChat DBChat = new DBChat();
        static string key;
        public ChatDetailPage(string _key)
        {
            InitializeComponent();
            key= _key;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            _room = await DBChat.GetChat(key);
            ObservableCollection<string> AdmList = _room.Admins;
            AdminListView.ItemsSource = AdmList;
            ObservableCollection<string> ParList = _room.Partpicatinas;
            PartpicatinasListView.ItemsSource = ParList;
        }
    }
}