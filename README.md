# 📦 Oto kurtarma takip sistemi

ASP.NET Core MVC ile geliştirilmiş bir web uygulamasıdır. Bu proje bir oto kurtarma takip sistemidir.

## 🚀 Özellikler

- ASP.NET Core MVC mimarisi
- Entity Framework Core ile veri erişimi
- Bootstrap ile duyarlı arayüz
- Oturum yönetimi ve yetkilendirme
- Dinamik veri listeleri

## 🛠️ Kullanılan Teknolojiler

- [.NET 8]  
- ASP.NET Core MVC  
- Entity Framework Core  
- MSSQL
- Bootstrap 5  
- LINQ  
- JavaScript / jQuery  
- LeafletJS

## ⚙️ Kurulum

1. Bu repoyu klonla:
   ```bash
   git clone https://github.com/MetaMsa/Oto-kurtarma-takip-sistemi.git
   cd Oto-kurtarma-takip-sistemi
   cd otokurtarma
   ```

2. Gerekli NuGet paketlerini yükle:
   ```bash
   dotnet restore
   ```

3. Veritabanını oluştur (Önce lokalde otokurtarma adlı veritabanını oluştur sonrasında komutu gir):
   ```bash
   dotnet ef database update
   ```

4. Uygulamayı başlat:
   ```bash
   dotnet run
   ```

5. Tarayıcıdan şu adrese git:
   ```
   http://localhost:5247
   ```

## 🧪 Geliştirici Notları

- `appsettings.json` dosyası içinde veritabanı bağlantı cümlesini ayarlamayı unutma.
- Migration eklemek için:
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```
- Ayrıca direk sql dosyasını import edebilirsiniz.
   ```bash
   sqlcmd -S localhost -d master -i proje.sql
   ```

## 📄 Lisans

Bu proje MIT lisansı ile lisanslanmıştır.
