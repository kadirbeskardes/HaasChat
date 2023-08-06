using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Timers;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading;
using HaasChat.Model;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignUp : ContentPage
    {
        static System.Timers.Timer tim1 = new System.Timers.Timer();
        static DBChat chat = new DBChat();
        public SignUp()
        {
            InitializeComponent();
            Content.BindingContext = this;
        }
        int confirm = 0;
        int count = 0;
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty( _username.Text)||string.IsNullOrEmpty(mailKont.Text))
            {
                await DisplayAlert("Eksik bilgi", "Yanlış veya eskik giriş yaptınız. Tekrar deneyiniz.", "TAMAM");
                return;
            }
            if (await chat.isThereUser(_username.Text)!=null)
            {
                await DisplayAlert("Eksik bilgi", "Bu kullanıcı adı kullanılıyor. Tekrar deneyiniz.", "TAMAM");
                return;
            }
            try
            {
                confirm = new Random().Next(100000, 999999);
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential("haaschat50@gmail.com", "jhljakycmckvahmj")
                };
                using (var message = new MailMessage("haaschat50@gmail.com", mailKont.Text)
                {
                    Subject = "Confirm e mail",
                    Body = $"{confirm}"
                })
                {
                    smtp.Send(message);
                }

                count = 60;
                mailKont.IsReadOnly = true;
                _username.IsReadOnly = true;
                kayıtBut.IsEnabled = false;
                timLay.IsVisible = true;
                onayLay.IsVisible = true;


                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    count--;
                    if (count>10)
                    {
                        timer.Text = $"00:{count.ToString()}";
                    }
                    else
                    {
                        timer.Text = $"00:0{count.ToString()}";
                        if (count == 0)
                        {
                            confirm = 0;
                            mailKont.IsReadOnly = false;
                            _username.IsReadOnly = false;
                            kayıtBut.IsEnabled = true;
                            onayLay.IsVisible = false;
                            timLay.IsVisible = false;
                            DisplayAlert("Zaman aşımı", "Kodun geçerlilik süresi sona erdi.", "Tamam");
                            return false;
                        }
                    }
                    
                    return true;
                });
            }
            catch (Exception ex)
            {
                await DisplayAlert("Bir hata oluştu", $"{ex.Message} hatası oluştu.", "Tamam");

            }
        }
        

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (_onay.Text == confirm.ToString())
            {
                User user = new User();
                user = new User();
                user.UserName = _username.Text;
                user.Email = mailKont.Text;
                user.chats = new List<string>();
                await chat.newUser(user);
                Preferences.Set("isLogged", "true");
                Preferences.Set("username", _username.Text);
                App.Current.MainPage = new NavigationPage(new ChatsPage());
            }
            else
            {
                _onay.Text = "";
                await DisplayAlert("Yanlış kod...", "Yanlış doğrulama kodu girdiniz. Tekrar deneyiniz.", "Tamam");
            }
        }
    }
}