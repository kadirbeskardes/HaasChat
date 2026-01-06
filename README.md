# 💬 HaasChat

<p align="center">
  <img src="https://img.shields.io/badge/Xamarin. Forms-3498DB?style=for-the-badge&logo=xamarin&logoColor=white" alt="Xamarin.Forms"/>
  <img src="https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Firebase-FFCA28?style=for-the-badge&logo=firebase&logoColor=black" alt="Firebase"/>
  <img src="https://img.shields.io/badge/Android-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android"/>
</p>

**HaasChat**, Xamarin.Forms ile geliştirilmiş gerçek zamanlı mesajlaşma uygulamasıdır.  MVVM (Model-View-ViewModel) design pattern kullanılarak modern ve sürdürülebilir bir mimari ile tasarlanmıştır. 

## 📋 İçindekiler

- [Özellikler](#-özellikler)
- [Mimari](#-mimari)
- [Teknolojiler](#-teknolojiler)
- [Kurulum](#-kurulum)
- [Proje Yapısı](#-proje-yapısı)
- [Katkıda Bulunma](#-katkıda-bulunma)

## ✨ Özellikler

- 💬 **Gerçek Zamanlı Mesajlaşma**: Anlık mesaj gönderme ve alma
- 👥 **Kullanıcı Yönetimi**: Kayıt ve giriş sistemi
- 🔔 **Bildirimler**: Push notification desteği
- 📱 **Cross-Platform**: Android desteği
- 🎨 **Modern UI**:  Temiz ve kullanıcı dostu arayüz
- 🔒 **Güvenlik**: Firebase Authentication ile güvenli giriş

## 🏗 Mimari

Proje **MVVM (Model-View-ViewModel)** pattern'i kullanmaktadır:

```
┌─────────────────────────────────────┐
│              View                   │
│     (XAML Pages & Controls)         │
├─────────────────────────────────────┤
│           ViewModel                 │
│   (ViewClass - Business Logic)      │
├─────────────────────────────────────┤
│             Model                   │
│        (Data Entities)              │
└─────────────────────────────────────┘
```

## 🛠 Teknolojiler

- **Xamarin.Forms** - Cross-platform UI framework
- **C#** - Programlama dili
- **Firebase** - Backend-as-a-Service
  - Realtime Database
  - Authentication
  - Cloud Messaging
- **MVVM Pattern** - Mimari pattern

## 🚀 Kurulum

### Gereksinimler
- Visual Studio 2022 (Xamarin workload yüklü)
- Android SDK
- Firebase Projesi

### Adımlar

```bash
# Repository'yi klonlayın
git clone https://github.com/kadirbeskardes/HaasChat.git
cd HaasChat

# Visual Studio ile açın
# HaasChat.sln dosyasını açın
```

### Firebase Yapılandırması

1. [Firebase Console](https://console.firebase.google.com)'dan yeni proje oluşturun
2. Android uygulaması ekleyin
3. `google-services.json` dosyasını indirin
4. `HaasChat.Android` projesine ekleyin

## 📁 Proje Yapısı

```
HaasChat/
├── HaasChat/                      # Shared Xamarin.Forms projesi
│   ├── Model/                     # Veri modelleri
│   │   ├── User.cs               # Kullanıcı modeli
│   │   ├── Message.cs            # Mesaj modeli
│   │   └── Chat.cs               # Sohbet modeli
│   ├── View/                      # XAML sayfaları
│   │   ├── LoginPage.xaml        # Giriş sayfası
│   │   ├── RegisterPage.xaml     # Kayıt sayfası
│   │   ├── ChatListPage.xaml     # Sohbet listesi
│   │   └── ChatPage.xaml         # Mesajlaşma sayfası
│   └── ViewClass/                 # ViewModel sınıfları
│       ├── LoginViewModel.cs
│       ├── RegisterViewModel. cs
│       └── ChatViewModel.cs
├── HaasChat.Android/              # Android platforma özgü kod
│   ├── MainActivity.cs
│   └── google-services.json
└── HaasChat.sln                   # Solution dosyası
```

## 🔧 Yapılandırma

### Firebase Bağlantısı

```csharp
// App.xaml.cs içinde
public static string FirebaseApiKey = "YOUR_API_KEY";
public static string FirebaseDatabaseUrl = "YOUR_DATABASE_URL";
```

## 📱 Kullanım

1. **Kayıt Ol**:  E-posta ve şifre ile hesap oluşturun
2. **Giriş Yap**: Mevcut hesabınızla giriş yapın
3. **Sohbet Başlat**: Kullanıcı listesinden kişi seçin
4. **Mesaj Gönderin**: Gerçek zamanlı mesajlaşmaya başlayın

## 🤝 Katkıda Bulunma

1. Fork edin
2. Feature branch oluşturun (`git checkout -b feature/NewFeature`)
3. Commit edin (`git commit -m 'Add NewFeature'`)
4. Push edin (`git push origin feature/NewFeature`)
5. Pull Request açın

## 📄 Lisans

MIT License

---

<p align="center">
  💬 <strong>HaasChat</strong> - Anlık mesajlaşmanın keyfi!
</p>
