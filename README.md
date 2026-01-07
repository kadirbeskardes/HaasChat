# 💬 HaasChat - Gerçek Zamanlı Mesajlaşma Uygulaması

<p align="center">
  <img src="https://img.shields.io/badge/Xamarin.Forms-5.0.0-3498DB?style=for-the-badge&logo=xamarin&logoColor=white" alt="Xamarin.Forms"/>
  <img src="https://img.shields.io/badge/C%23-10.0-239120?style=for-the-badge&logo=csharp&logoColor=white" alt="C#"/>
  <img src="https://img.shields.io/badge/Firebase_Realtime_DB-FFCA28?style=for-the-badge&logo=firebase&logoColor=black" alt="Firebase"/>
  <img src="https://img.shields.io/badge/Firebase_Storage-FFCA28?style=for-the-badge&logo=firebase&logoColor=black" alt="Firebase Storage"/>
  <img src="https://img.shields.io/badge/Android_13-3DDC84?style=for-the-badge&logo=android&logoColor=white" alt="Android"/>
  <img src="https://img.shields.io/badge/.NET_Standard-2.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET Standard"/>
</p>

<p align="center">
  <strong>HaasChat</strong>, Xamarin.Forms ile geliştirilmiş, Firebase altyapısını kullanan, 
  grup tabanlı gerçek zamanlı mesajlaşma uygulamasıdır. Kullanıcılar sohbet odaları oluşturabilir, 
  metin mesajları gönderebilir, fotoğraf/video/dosya paylaşabilir ve diğer kullanıcıları gruplara davet edebilir.
</p>

---

## 📋 İçindekiler

- [Genel Bakış](#-genel-bakış)
- [Özellikler](#-özellikler)
- [Mimari Yapı](#-mimari-yapı)
- [Proje Yapısı](#-proje-yapısı)
- [Veri Modelleri](#-veri-modelleri)
- [Sayfa ve Ekranlar](#-sayfa-ve-ekranlar)
- [Firebase Entegrasyonu](#-firebase-entegrasyonu)
- [Kullanılan Paketler](#-kullanılan-paketler)
- [Kurulum](#-kurulum)
- [Kullanım Kılavuzu](#-kullanım-kılavuzu)
- [Teknik Detaylar](#-teknik-detaylar)
- [Katkıda Bulunma](#-katkıda-bulunma)

---

## 🌟 Genel Bakış

HaasChat, kullanıcıların grup sohbet odaları oluşturup yönetmelerine olanak tanıyan bir mobil mesajlaşma uygulamasıdır. Uygulama, aşağıdaki temel işlevleri destekler:

- **E-posta doğrulaması** ile güvenli kullanıcı kaydı ve girişi
- **Grup sohbet odaları** oluşturma ve yönetme
- **Gerçek zamanlı mesajlaşma** (Firebase Realtime Database)
- **Multimedya paylaşımı** (Fotoğraf, Video, Dosya)
- **Admin ve katılımcı yönetimi**

---

## ✨ Özellikler

### 🔐 Kimlik Doğrulama Sistemi
| Özellik | Açıklama |
|---------|----------|
| **Kullanıcı Kaydı** | Kullanıcı adı ve e-posta ile kayıt |
| **E-posta Doğrulama** | SMTP ile 6 haneli doğrulama kodu gönderimi |
| **Oturum Yönetimi** | Xamarin.Essentials Preferences ile kalıcı oturum |
| **Zaman Aşımı** | 60 saniyelik doğrulama kodu süresi |
| **E-posta Gizleme** | Giriş sırasında e-posta adresinin kısmi gösterimi |

### 💬 Mesajlaşma Özellikleri
| Özellik | Açıklama |
|---------|----------|
| **Metin Mesajları** | Standart metin mesajları gönderme |
| **Fotoğraf Paylaşımı** | Galeriden fotoğraf seçip gönderme |
| **Video Paylaşımı** | Video dosyası seçip gönderme |
| **Dosya Paylaşımı** | Her türlü dosyayı paylaşabilme |
| **Gerçek Zamanlı Güncelleme** | Firebase Observable ile anlık mesaj akışı |
| **Zaman Damgası** | Her mesajda saat:dakika formatında zaman gösterimi |

### 👥 Grup Yönetimi
| Özellik | Açıklama |
|---------|----------|
| **Oda Oluşturma** | Özel profil fotoğraflı sohbet odaları |
| **Admin Sistemi** | Oda oluşturan otomatik admin olur |
| **Kullanıcı Davet** | Adminler yeni kullanıcı ekleyebilir |
| **Katılımcı Listesi** | Admin ve katılımcı listeleri görüntüleme |

### 🎨 Kullanıcı Arayüzü
| Özellik | Açıklama |
|---------|----------|
| **Mesaj Baloncukları** | Gönderen/alıcı bazlı farklı renkler ve pozisyonlar |
| **Yuvarlak Profil Resimleri** | ImageCircle plugin ile dairesel görseller |
| **Pull-to-Refresh** | Liste yenileme desteği |
| **Popup Pencereler** | Rg.Plugins.Popup ile modal pencereler |
| **Medya Oynatıcı** | Video dosyaları için yerleşik oynatıcı |

---

## 🏗 Mimari Yapı

Proje, **MVVM benzeri bir mimari** kullanmaktadır. View dosyaları (XAML) arayüzü tanımlarken, ViewClass dosyaları (code-behind) iş mantığını içerir.

```
┌─────────────────────────────────────────────────────────────┐
│                         VIEW LAYER                          │
│    ┌─────────────┐  ┌─────────────┐  ┌─────────────┐       │
│    │ LoginPage   │  │ ChatsPage   │  │ A_ChatPage  │       │
│    │   .xaml     │  │   .xaml     │  │   .xaml     │       │
│    └─────────────┘  └─────────────┘  └─────────────┘       │
├─────────────────────────────────────────────────────────────┤
│                      VIEWCLASS LAYER                        │
│    ┌─────────────┐  ┌─────────────┐  ┌─────────────┐       │
│    │ LoginPage   │  │ ChatsPage   │  │ A_ChatPage  │       │
│    │  .xaml.cs   │  │  .xaml.cs   │  │  .xaml.cs   │       │
│    └─────────────┘  └─────────────┘  └─────────────┘       │
├─────────────────────────────────────────────────────────────┤
│                       MODEL LAYER                           │
│    ┌─────────┐  ┌──────────┐  ┌────────┐  ┌─────────┐      │
│    │  User   │  │ ChatRoom │  │  Chat  │  │ DBChat  │      │
│    └─────────┘  └──────────┘  └────────┘  └─────────┘      │
├─────────────────────────────────────────────────────────────┤
│                     FIREBASE BACKEND                        │
│    ┌─────────────────────┐  ┌─────────────────────┐        │
│    │  Realtime Database  │  │   Firebase Storage  │        │
│    │   (Mesaj & Oda)     │  │   (Medya Dosyaları) │        │
│    └─────────────────────┘  └─────────────────────┘        │
└─────────────────────────────────────────────────────────────┘
```

---

## 📁 Proje Yapısı

```
HaasChat/
├── 📄 HaasChat.sln                    # Visual Studio Solution dosyası
│
├── 📁 HaasChat/                       # Paylaşılan .NET Standard 2.0 kütüphanesi
│   ├── 📄 HaasChat.csproj             # Proje dosyası
│   ├── 📄 AssemblyInfo.cs             # Assembly bilgileri
│   │
│   ├── 📁 Model/                      # Veri modelleri
│   │   ├── 📄 User.cs                 # Kullanıcı modeli
│   │   ├── 📄 Chat.cs                 # Mesaj modeli
│   │   ├── 📄 ChatRoom.cs             # Sohbet odası modeli
│   │   └── 📄 DBChat.cs               # Firebase veritabanı işlemleri
│   │
│   ├── 📁 View/                       # XAML arayüz dosyaları
│   │   ├── 📄 App.xaml                # Uygulama ana tanımları
│   │   ├── 📄 LoginPage.xaml          # Giriş sayfası arayüzü
│   │   ├── 📄 SignUp.xaml             # Kayıt sayfası arayüzü
│   │   ├── 📄 ChatsPage.xaml          # Sohbet listesi arayüzü
│   │   ├── 📄 A_ChatPage.xaml         # Mesajlaşma sayfası arayüzü
│   │   ├── 📄 AddChat.xaml            # Yeni oda oluşturma arayüzü
│   │   ├── 📄 ChatDetailPage.xaml     # Oda detayları arayüzü
│   │   └── 📄 HaasPopup.xaml          # Kullanıcı ekleme popup'ı
│   │
│   └── 📁 ViewClass/                  # Code-behind dosyaları
│       ├── 📄 App.xaml.cs             # Uygulama başlangıç mantığı
│       ├── 📄 LoginPage.xaml.cs       # Giriş işlemleri
│       ├── 📄 SignUp.xaml.cs          # Kayıt işlemleri
│       ├── 📄 ChatsPage.xaml.cs       # Sohbet listesi işlemleri
│       ├── 📄 A_ChatPage.xaml.cs      # Mesajlaşma işlemleri + Converters
│       ├── 📄 AddChat.xaml.cs         # Oda oluşturma işlemleri
│       ├── 📄 ChatDetailPage.xaml.cs  # Oda detay işlemleri
│       └── 📄 HaasPopup.xaml.cs       # Kullanıcı ekleme işlemleri
│
└── 📁 HaasChat.Android/               # Android platforma özgü proje
    ├── 📄 HaasChat.Android.csproj     # Android proje dosyası
    ├── 📄 MainActivity.cs             # Android giriş noktası
    │
    ├── 📁 Properties/
    │   ├── 📄 AndroidManifest.xml     # Android yapılandırması
    │   └── 📄 AssemblyInfo.cs         # Assembly bilgileri
    │
    ├── 📁 Resources/
    │   ├── 📁 drawable/               # Çizilebilir kaynaklar
    │   ├── 📁 mipmap-*/               # Uygulama ikonları
    │   ├── 📁 values/
    │   │   ├── 📄 colors.xml          # Renk tanımları
    │   │   └── 📄 styles.xml          # Stil tanımları
    │   └── 📁 xml/
    │       └── 📄 file_paths.xml      # Dosya sağlayıcı yolları
    │
    └── 📁 Assets/
        └── 📄 AboutAssets.txt         # Asset bilgileri
```

---

## 📊 Veri Modelleri

### 👤 User (Kullanıcı)

```csharp
internal class User
{
    public string UserName { get; set; }      // Benzersiz kullanıcı adı
    public string Email { get; set; }          // E-posta adresi
    public List<string> chats { get; set; }    // Katıldığı sohbet odası anahtarları
}
```

**Firebase Yapısı:** `/HaasChatAppUser/{username}/`

### 🏠 ChatRoom (Sohbet Odası)

```csharp
internal class ChatRoom
{
    public string Key { get; set; }                          // Firebase benzersiz anahtar
    public string Name { get; set; }                         // Oda adı
    public string ImageURL { get; set; }                     // Oda profil resmi URL'i
    public ObservableCollection<string> Admins { get; set; }       // Admin kullanıcı adları
    public ObservableCollection<string> Partpicatinas { get; set; } // Katılımcı kullanıcı adları
}
```

**Firebase Yapısı:** `/HaasChatApp/Groups/{key}/`

### 💬 Chat (Mesaj)

```csharp
internal class Chat
{
    public string UserName { get; set; }   // Gönderen kullanıcı adı
    public string Message { get; set; }    // Metin mesajı
    public string ImageURL { get; set; }   // Fotoğraf URL'i (opsiyonel)
    public string VideoURL { get; set; }   // Video URL'i (opsiyonel)
    public string FileURL { get; set; }    // Dosya URL'i (opsiyonel)
    public string FileName { get; set; }   // Dosya adı (opsiyonel)
    public DateTime Date { get; set; }     // Gönderim zamanı
}
```

**Firebase Yapısı:** `/HaasChatApp/Groups/{key}/Message/{messageId}/`

---

## 📱 Sayfa ve Ekranlar

### 1️⃣ LoginPage - Giriş Sayfası

**Dosyalar:** `View/LoginPage.xaml` | `ViewClass/LoginPage.xaml.cs`

**İşlevler:**
- Kullanıcı adı ile giriş
- SMTP üzerinden e-posta doğrulama kodu gönderimi
- 60 saniyelik geri sayım zamanlayıcısı
- E-posta adresinin kısmi gizlenmesi (örn: `***@gmail.com`)
- Kayıt sayfasına yönlendirme

**Akış:**
```
Kullanıcı Adı Girişi → DB'den Kullanıcı Kontrolü → E-posta Doğrulama Kodu Gönderimi → Kod Onayı → Ana Sayfaya Yönlendirme
```

---

### 2️⃣ SignUp - Kayıt Sayfası

**Dosyalar:** `View/SignUp.xaml` | `ViewClass/SignUp.xaml.cs`

**İşlevler:**
- Yeni kullanıcı kaydı
- Kullanıcı adı benzersizlik kontrolü
- E-posta doğrulama sistemi
- Firebase'e kullanıcı kaydetme

**Akış:**
```
Kullanıcı Adı + E-posta Girişi → Benzersizlik Kontrolü → Doğrulama Kodu → Kayıt Tamamlama
```

---

### 3️⃣ ChatsPage - Sohbet Listesi

**Dosyalar:** `View/ChatsPage.xaml` | `ViewClass/ChatsPage.xaml.cs`

**İşlevler:**
- Kullanıcının katıldığı tüm sohbet odalarını listeleme
- Dairesel profil resimleri ile oda gösterimi
- Pull-to-Refresh desteği
- Yeni oda oluşturma butonu (Toolbar)
- Oda seçimi ile mesajlaşma sayfasına geçiş

**UI Bileşenleri:**
- `ListView` - Sohbet odaları listesi
- `CircleImage` - Yuvarlak oda resimleri
- `Frame` - Kart görünümü

---

### 4️⃣ A_ChatPage - Mesajlaşma Sayfası

**Dosyalar:** `View/A_ChatPage.xaml` | `ViewClass/A_ChatPage.xaml.cs`

**İşlevler:**
- Gerçek zamanlı mesaj akışı (Observable Collection)
- Metin mesajı gönderme
- Fotoğraf seçme ve gönderme
- Video seçme ve gönderme
- Dosya seçme ve gönderme
- Dosya indirme ve açma
- Otomatik son mesaja kaydırma
- Kullanıcı ekleme (sadece adminler)

**Value Converters:**
| Converter | İşlev |
|-----------|-------|
| `UsernameToColorConverter` | Kullanıcı adına göre renk (mavi/kırmızı) |
| `IsMyMessageToColorConverter` | Mesaj baloncuğu arka plan rengi |
| `IsMyMessageToHorizontalOptionsConverter` | Mesaj pozisyonu (sağ/sol) |
| `IsMyMessageToTextAlignmentConverter` | Metin hizalama |
| `IsImageConverter` | Resim görünürlüğü kontrolü |
| `IsVideoConverter` | Video görünürlüğü kontrolü |
| `IsFileConverter` | Dosya görünürlüğü kontrolü |

**Toolbar Butonları:**
- Oda detayları (bilgi)
- Kullanıcı ekleme (+) - Sadece adminler için aktif

---

### 5️⃣ AddChat - Yeni Oda Oluşturma

**Dosyalar:** `View/AddChat.xaml` | `ViewClass/AddChat.xaml.cs`

**İşlevler:**
- Oda adı belirleme
- Oda profil resmi seçme (opsiyonel)
- Varsayılan resim kullanma
- Firebase Storage'a resim yükleme
- Oda oluşturan otomatik admin olur

---

### 6️⃣ ChatDetailPage - Oda Detayları

**Dosyalar:** `View/ChatDetailPage.xaml` | `ViewClass/ChatDetailPage.xaml.cs`

**İşlevler:**
- Admin listesini görüntüleme
- Katılımcı listesini görüntüleme

---

### 7️⃣ HaasPopup - Kullanıcı Ekleme

**Dosyalar:** `View/HaasPopup.xaml` | `ViewClass/HaasPopup.xaml.cs`

**İşlevler:**
- Popup modal pencere
- Kullanıcı adı ile arama
- Kullanıcıyı sohbet odasına ekleme
- Kullanıcının `chats` listesine oda ekleme

---

## 🔥 Firebase Entegrasyonu

### DBChat Sınıfı İşlevleri

`Model/DBChat.cs` dosyası tüm Firebase işlemlerini yönetir:

```csharp
// Firebase bağlantı URL'i
FirebaseClient client = new FirebaseClient(
    "https://grid-grid-beta1-default-rtdb.europe-west1.firebasedatabase.app/"
);
```

### Veritabanı Metodları

| Metod | Açıklama |
|-------|----------|
| `GetAllChat(List<string> _chats)` | Belirtilen anahtarlara sahip tüm odaları getirir |
| `GetChat(string key)` | Tek bir sohbet odasını getirir |
| `isThereUser(string username)` | Kullanıcı var mı kontrol eder |
| `getUser(string _username)` | Kullanıcı bilgilerini getirir |
| `newUser(User user)` | Yeni kullanıcı kaydeder |
| `NewChat(...)` | Yeni sohbet odası oluşturur |
| `saveChat(...)` | Sohbet odası günceller |
| `addParToChat(...)` | Odaya katılımcı ekler |
| `SendMessage(Chat ch, string _room)` | Mesaj gönderir |
| `chats(string _room)` | Gerçek zamanlı mesaj akışı döndürür |

### Firebase Storage Kullanımı

Medya dosyaları Firebase Storage'a yüklenir:

```
Firebase Storage: grid-grid-beta1.appspot.com
├── chat_resim/     # Fotoğraflar
├── chat_dosyalar/  # Dosyalar
└── chatRoom/       # Oda profil resimleri
```

---

## 📦 Kullanılan Paketler

### .NET Standard Projesi (HaasChat)

| Paket | Versiyon | Açıklama |
|-------|----------|----------|
| `Xamarin.Forms` | 5.0.0.2599-pre1 | Cross-platform UI framework |
| `Xamarin.Essentials` | 1.8.0-preview1 | Platform API'leri |
| `FirebaseDatabase.net` | 4.2.0 | Firebase Realtime Database |
| `FirebaseStorage.net` | 1.0.3 | Firebase Storage |
| `Rg.Plugins.Popup` | 2.1.0 | Popup pencereler |
| `Xam.Plugin.Media` | 6.0.2 | Medya seçici |
| `Xam.Plugins.Forms.ImageCircle` | 3.0.0.5 | Yuvarlak resimler |
| `Xamarin.CommunityToolkit` | 2.0.6 | Topluluk araçları (MediaElement) |

### Android Projesi Ek Paketleri

- Android 13 (API 33) hedeflenir
- `Xamarin.Android.Net.AndroidClientHandler` HTTP istemcisi

---

## 🚀 Kurulum

### Gereksinimler

- ✅ Visual Studio 2022 (veya üstü)
- ✅ Xamarin.Forms workload yüklü
- ✅ Android SDK (API 33)
- ✅ Firebase projesi

### Adımlar

1. **Repository'yi klonlayın:**
   ```bash
   git clone https://github.com/kadirbeskardes/HaasChat.git
   cd HaasChat
   ```

2. **Visual Studio ile açın:**
   - `HaasChat.sln` dosyasını açın

3. **NuGet paketlerini yükleyin:**
   - Solution üzerine sağ tıklayın
   - "Restore NuGet Packages" seçin

4. **Firebase yapılandırması:**
   - Firebase Console'dan proje oluşturun
   - Realtime Database aktifleştirin
   - Storage aktifleştirin
   - `DBChat.cs` dosyasındaki Firebase URL'ini güncelleyin

5. **Derleme ve çalıştırma:**
   - HaasChat.Android projesini başlangıç projesi olarak ayarlayın
   - Emülatör veya fiziksel cihazda çalıştırın

---

## 📖 Kullanım Kılavuzu

### 1. Kayıt Olma
1. Uygulamayı açın
2. "Kayıt Ol" butonuna tıklayın
3. Kullanıcı adı ve e-posta girin
4. E-postanıza gelen 6 haneli kodu girin
5. Otomatik olarak ana sayfaya yönlendirilirsiniz

### 2. Giriş Yapma
1. Kullanıcı adınızı girin
2. "Giriş Yap" butonuna tıklayın
3. E-postanıza gelen kodu girin
4. Oturum kalıcı olarak saklanır

### 3. Sohbet Odası Oluşturma
1. Ana sayfada "+" butonuna tıklayın
2. Oda adı girin
3. İsterseniz profil resmi seçin
4. "Oluştur" butonuna tıklayın

### 4. Mesaj Gönderme
1. Sohbet odasına girin
2. Alt kısımdaki metin kutusuna yazın
3. Mavi gönder butonuna tıklayın

### 5. Medya Paylaşımı
- 🟢 **Fotoğraf:** Yeşil butona tıklayın
- 🟠 **Video:** Turuncu butona tıklayın
- 🔴 **Dosya:** Kırmızı butona tıklayın

### 6. Kullanıcı Ekleme (Admin)
1. Sohbet odasında "+" butonuna tıklayın
2. Eklemek istediğiniz kullanıcı adını girin
3. "Ekle" butonuna tıklayın

---

## 🔧 Teknik Detaylar

### Oturum Yönetimi
```csharp
// Oturum kontrolü (App.xaml.cs)
if (Preferences.Get("isLogged", "false") == "false")
    MainPage = new NavigationPage(new LoginPage());
else
    MainPage = new NavigationPage(new ChatsPage());
```

### E-posta Gönderimi
- SMTP protokolü kullanılır
- Gmail SMTP sunucusu (smtp.gmail.com:587)
- SSL/TLS şifreleme aktif

### Gerçek Zamanlı Mesajlaşma
```csharp
// Observable mesaj akışı
public ObservableCollection<Chat> chats(string _room)
{
    return client.Child("HaasChatApp/Groups/" + _room + "/Message")
                 .AsObservable<Chat>()
                 .AsObservableCollection();
}
```

### Dosya İndirme ve Açma
```csharp
// HttpClient ile dosya indirme
var response = await client.GetAsync(fileUrl);
var contentStream = await response.Content.ReadAsStreamAsync();

// Xamarin.Essentials Launcher ile dosya açma
await Launcher.OpenAsync(new OpenFileRequest {
    File = new ReadOnlyFile(filePath)
});
```

---

## 🤝 Katkıda Bulunma

1. 🍴 Fork edin
2. 🌿 Feature branch oluşturun (`git checkout -b feature/YeniOzellik`)
3. 💾 Değişiklikleri commit edin (`git commit -m 'Yeni özellik eklendi'`)
4. 📤 Push edin (`git push origin feature/YeniOzellik`)
5. 🔄 Pull Request açın

---

## 📄 Lisans

Bu proje MIT Lisansı altında lisanslanmıştır.

---

<p align="center">
  💬 <strong>HaasChat</strong> - Gerçek zamanlı grup mesajlaşmanın keyfi!
  <br><br>
  <sub>Xamarin.Forms ve Firebase ile geliştirilmiştir.</sub>
</p>
