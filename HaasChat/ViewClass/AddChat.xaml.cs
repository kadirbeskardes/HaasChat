using System;
using System.Collections.Generic;
using Firebase.Storage;
using System.Numerics;
using HaasChat.Model;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddChat : ContentPage
    {
        public AddChat()
        {
            InitializeComponent();
            foto.Source = "meeting.jpg";
        }
        static bool photob = false;
        static Xamarin.Essentials.FileResult photo;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            var db = new DBChat();
            User user = new User();
            user = await db.getUser(Preferences.Get("username", "null"));
            if (user.chats == null)
            {
                user.chats = new List<string>();
            }
            string downloadLink;
            if (photob)
            {
                var imageS = new FirebaseStorage("haaschat-9a85d.appspot.com", new FirebaseStorageOptions
                { ThrowOnCancel = true }).Child("chatRoom")
                .Child($"{Guid.NewGuid()}.jpg").PutAsync(await photo.OpenReadAsync());
                downloadLink = await imageS;
            }
            else
            {
                downloadLink = "https://firebasestorage.googleapis.com/v0/b/haaschat-9a85d.appspot.com/o/chatRoom%2Fmeeting.jpeg?alt=media";
            }
            
            user.chats.Add(await db.NewChat(_chatName.Text,user.UserName,downloadLink));
            await db.newUser(user);
            

            await Navigation.PopAsync();
        }
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            try
            {
                photo = await Xamarin.Essentials.MediaPicker.PickPhotoAsync();
                if (photo == null) { photob = false; }
                else
                { foto.Source = photo.FullPath; foto.WidthRequest = 100; photob = true; }
            }
            catch (Exception ex)
            { await DisplayAlert("Bir hata meydana geldi", "Belirlenemeyen bir sorun oluştu..", "Ok"); }
        }
    }
}