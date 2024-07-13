using HaasChat.Model;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HaasChat
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        static string userEmailDomain;
        static string userEmail;
        static DBChat DBChat = new DBChat();
        int confirm = 0;
        int count = 0;

        public LoginPage()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                DisplayAlert("m", ex.ToString(), "ok");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = usernameEntry.Text;
            if (string.IsNullOrEmpty(username))
            {
                return;
            }

            User user = await DBChat.getUser(username);
            if (user == null)
            {
                return;
            }

            userEmail = user.Email;
            if (!string.IsNullOrEmpty(userEmail))
            {
                userEmailDomain = SansurleMailAdresi(userEmail);
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
                    Credentials = new NetworkCredential("***", "**")
                };

                using (var message = new MailMessage("***", userEmail)
                {
                    Subject = "Confirm e mail",
                    Body = $"{confirm}"
                })
                {
                    smtp.Send(message);
                }

                count = 60;
                usernameEntry.IsReadOnly = true;
                girisYap.IsEnabled = false;
                timLay.IsVisible = true;
                onayLay.IsVisible = true;

                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    count--;
                    timer.Text = $"{userEmailDomain} adresine bir doğrulama kodu gönderildi.\n00:{count:00}";
                    if (count == 0)
                    {
                        confirm = 0;
                        usernameEntry.IsReadOnly = false;
                        girisYap.IsEnabled = true;
                        timLay.IsVisible = false;
                        onayLay.IsVisible = false;
                        DisplayAlert("Zaman aşımı", "Kodun geçerlilik süresi sona erdi.", "Tamam");
                        return false;
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
                Preferences.Set("isLogged", "true");
                Preferences.Set("username", usernameEntry.Text);
                App.Current.MainPage = new NavigationPage(new ChatsPage());
            }
            else
            {
                _onay.Text = "";
                await DisplayAlert("Yanlış kod...", "Yanlış doğrulama kodu girdiniz. Tekrar deneyiniz.", "Tamam");
            }
        }

        private string SansurleMailAdresi(string email)
        {
            int atSymbolIndex = email.IndexOf('@');
            if (atSymbolIndex >= 3)
            {
                string sansurluKisim = new string('*', atSymbolIndex - 3);
                return sansurluKisim + email.Substring(atSymbolIndex - 2);
            }
            return email;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new SignUp());
        }
    }
}
