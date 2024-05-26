using Firebase.Storage;
using HaasChat.Model;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Net.Http;
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
        static ObservableCollection<Chat> chatlist = new ObservableCollection<Chat>();

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
            this.Resources.Add("IsImageConverter", new IsImageConverter());
            this.Resources.Add("IsVideoConverter", new IsVideoConverter());
            this.Resources.Add("IsFileConverter", new IsFileConverter());
            
            this.room = room;
            
            ChatListView.BindingContext = this;
            
            chatlist = chat.chats(room.Key);
            
            ChatListView.ItemsSource = chatlist;
            
            ScrollToLastItem();
        }

        public A_ChatPage()
        {
            InitializeComponent();
        }



        private async Task<string> DownloadFileAsync(string fileUrl)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(fileUrl);
            
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync(); // dosya içeriğini alır
                var localPath = Path.Combine(FileSystem.CacheDirectory, "downloadedFile.pdf"); // dosya adı ve yolunu belirler
                
                using (var fileStream = new FileStream(localPath, FileMode.Create, FileAccess.Write))
                {
                    await contentStream.CopyToAsync(fileStream); // dosyayı yerel cihaza kaydeder
                }
                
                return localPath;
            }

            return null;
        }

        private async Task OpenDownloadedFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                await Launcher.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });
            }
        }

        private async void OnFileTapped(object sender, EventArgs e)
        {
            var fileUrl = (string)((TappedEventArgs)e).Parameter;

            if (!string.IsNullOrEmpty(fileUrl))
            {
                var localPath = await DownloadFileAsync(fileUrl);

                if (!string.IsNullOrEmpty(localPath))
                {
                    await OpenDownloadedFile(localPath);
                }
                else
                {
                    await DisplayAlert("Hata", "Dosya indirilemedi.", "Tamam");
                }
            }
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
        /*private void AttcButton(object sender, EventArgs e)
        {
            miniWindow.IsVisible = !miniWindow.IsVisible;
        }*/
        private async void OnPickImageButtonClicked(object sender, EventArgs e)
        {
            await PickAndUploadImage();
        }

        private async Task PickAndUploadImage()
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Destek yok", "Seçilen medya bu cihazda desteklenmiyor.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                    return;

                string imageUrl = await UploadImageToFirebaseStorage(file.GetStream());

                await SendMessageWithImage(imageUrl);

                file.Dispose();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Bir hata oluştu: " + ex.Message, "OK");
            }
        }

        private async Task SendMessageWithImage(string imageUrl)
        {
            await chat.SendMessage(new Chat
            {
                UserName = Preferences.Get("username", "username"),
                Date = DateTime.Now,
                ImageURL = imageUrl
            }, this.room.Key);

            await ScrollToLastItem();
        }

        private async Task<string> UploadImageToFirebaseStorage(Stream imageStream)
        {
            var storage = new FirebaseStorage("grid-grid-beta1.appspot.com");

            string fileName = $"{Guid.NewGuid()}.jpg";

            var imageReference = storage.Child("chat_resim").Child(fileName);

            var downloadUrl = await imageReference.PutAsync(imageStream);

            return downloadUrl;
        }

        private async void OnPickVideoButtonClicked(object sender, EventArgs e)
        {
            await PickAndUploadVideo();
        }

        private async Task PickAndUploadVideo()
        {
            if (!CrossMedia.Current.IsTakeVideoSupported || !CrossMedia.Current.IsPickVideoSupported)
            {
                await DisplayAlert("Destek yok", "Video seçimi bu cihazda desteklenmiyor.", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return;

            string videoUrl = await UploadMediaToFirebaseStorage(file.GetStream());

            await SendMessageWithVideo(videoUrl);

            file.Dispose();
        }
        private async Task SendMessageWithVideo(string imageUrl)
        {
            await chat.SendMessage(new Chat
            {
                UserName = Preferences.Get("username", "username"),
                Date = DateTime.Now,
                VideoURL = imageUrl
            }, this.room.Key);

            await ScrollToLastItem();
        }

        private async Task<string> UploadMediaToFirebaseStorage(Stream mediaStream)
        {
            var storage = new FirebaseStorage("grid-grid-beta1.appspot.com");
            string fileName = $"{Guid.NewGuid()}.mp4";

            var mediaReference = storage.Child(fileName);
            var downloadUrl = await mediaReference.PutAsync(mediaStream);

            return downloadUrl;
        }

        private async void OnPickFileButtonClicked(object sender, EventArgs e)
        {
            await PickAndUploadFile();
        }

        private async Task PickAndUploadFile()
        {
            try
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
        {
            { DevicePlatform.Android, new[] { "*/*" } },
        });

                var options =
                    new PickOptions
                    {
                        PickerTitle = "Lütfen bir dosya seçin",
                        FileTypes = customFileType,
                    };

                var result = await FilePicker.PickAsync(options);

                if (result != null)
                {
                    var stream = await result.OpenReadAsync();
                    string fileUrl = await UploadFileToFirebaseStorage(stream, result.FileName);
                    await SendMessageWithFile(fileUrl, result.FileName);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Hata", "Bir hata oluştu: " + ex.Message, "OK");
            }
        }

        private async Task<string> UploadFileToFirebaseStorage(Stream fileStream, string fileName)
        {
            var storage = new FirebaseStorage("grid-grid-beta1.appspot.com");

            var fileReference = storage.Child("chat_dosyalar").Child(fileName);

            var downloadUrl = await fileReference.PutAsync(fileStream);

            return downloadUrl;
        }

        private async Task SendMessageWithFile(string fileUrl, string fileName)
        {
            await chat.SendMessage(new Chat
            {
                UserName = Preferences.Get("username", "username"),
                Date = DateTime.Now,
                FileURL = fileUrl,
                FileName = fileName
            }, this.room.Key);

            await ScrollToLastItem();
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
            }
            else return LayoutOptions.StartAndExpand;
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
            }
            return TextAlignment.Start;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string ImageURL = value as string;
            return !string.IsNullOrEmpty(ImageURL);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsFileConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string FileURL = value as string;
            return !string.IsNullOrEmpty(FileURL);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class IsVideoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string VideoURL = value as string;
            return !string.IsNullOrEmpty(VideoURL);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}