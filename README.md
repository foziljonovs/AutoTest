# AutoTest Loyihasi

**AutoTest** - bu avtomatlashtirilgan testlarni yaratish va boshqarish uchun ishlab chiqilgan loyiha. Bu loyiha yordamida siz testlar yaratishingiz, ularni boshqarishingiz va PDF shaklida yuklab olish imkoniga ega bo‘lasiz.

## 📌 Loyihaning Asosiy Maqsadi

- ✅ Testlarni avtomatlashtirish  
- ✅ Foydalanuvchi tomonidan yaratilgan testlarni boshqarish  
- ✅ Test hisobotlarini PDF shaklida eksport qilish  
- ✅ Savollarni AI orqali generatsiya qilish  

## 📝 Birinchi Test Misoli

AutoTest orqali yaratilgan birinchi testni quyidagi havola orqali yuklab olishingiz mumkin:  
[Test (PDF)](https://github.com/foziljonovs/AutoTest/blob/master/Test_Birinchi%20test.pdf)

---

## 🔄 Loyiha Qanday Ishlaydi?

1. Foydalanuvchi tomonidan test yaratiladi.
2. Testdagi savollar va variantlar kiritiladi yoki AI orqali generatsiya qilinadi.
3. Test hisobot shaklida eksport qilinadi (PDF formatda).

---

## ⚙️ Loyihada Ishlatilgan Texnologiyalar

- **.NET 9.0** - Loyiha .NET 9.0 frameworkida yozilgan, shu sababli loyihani clone qilib olgach aynan shu versiya kerak bo‘ladi.
- **PostgreSQL 16, 17** - AutoTest ma'lumotlarni PostgreSQL orqali saqlaydi.
- **Kerakli paketlar** - Visual Studio 2022 yoki VSCode orqali quyidagi buyruqlarni bajarib qayta ishga tushurishingiz mumkin:
  
  ```powershell
  dotnet restore
  ```

---

## 🚀 Loyihadan Foydalanish

### 1️⃣ Loyiha Fayllarini Sozlash

Loyihani clone qilib olgach, **WebApi** ichidagi `appsettings.Development.json` faylini quyidagi kabi to‘ldiring:

```json
{
  "ConnectionStrings": {
    "localhost": "Host=localhost;Port=5432;Database=AutoTestDB;Username=your-username;Password=your-password"
  },
  "Jwt": {
    "Issuer": "https://AutoAITest.uz",
    "Audience": "AutoAITest.uz",
    "SecretKey": "1c9e6964-2125-57de-711b-e09fc1f77ai1",
    "Lifetime": 120
  },
  "OpenAI": {
    "ApiKey": "your-openai-api-key"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

ℹ️ **Izoh**:
- `ConnectionStrings.localhost` bo‘limida PostgreSQL ma'lumotlarini to‘ldiring.
- `Jwt` bo‘limida **secret key** va **token muddati** kiritilgan.
- `OpenAI.ApiKey` bo‘limida **AI uchun API kalit** kiritiladi.

---

### 2️⃣ Ma'lumotlar Bazasini Ishga Tushirish

Powershell yoki **Package Manager Console** orqali quyidagi buyruqni bajaring:

```powershell
Update-Database
```

---

### 3️⃣ Loyiha Ishga Tushirish

AutoTest loyihasining **WebApi** va **Desktop** qismlarini **birgalikda** ishga tushuring.

---

### 4️⃣ Ro‘yxatdan O‘tish

🔹 **Tizimga kirish uchun quyidagi ma'lumotlarni kiriting:**
   - **To‘liq ism**
   - **Telefon raqam**
   - **Parol**

📌 **Misol:**  
![Ro‘yxatdan o‘tish](Examples/Ro'yxatdan%20o'tish.png)

---

### 5️⃣ Test Yaratish

✅ "Testlaringiz" bo‘limiga o‘tib **"Yaratish"** tugmasini bosing.

📌 **Misol:**  
![Test yaratish](Examples/Testlar%20o'ynasi.png)

---

### 6️⃣ Testga Savollar Qo‘shish

📌 "Savollar" bo‘limiga o‘tib test uchun **savol va javoblarni** kiriting.

📌 **Misol:**  
![Savollar qo‘shish](Examples/Testga%20savol%20qo'shish%20va%20PDF%20olish.png)

---

### 7️⃣ PDF Hisobotni Yuklab Olish

📌 "Test ma'lumotlari" bo‘limiga o‘tib **"PDF olish"** tugmasini bosing.

📌 **Misol:**  
![PDF yaratish](/Examples/Testga%20savol%20qo'shish%20va%20PDF%20olish.png)

---

### 8️⃣ Yaratilgan PDF-ni Tekshirish

📌 **Kompyuteringizning "Downloads" yoki "Desktop" bo‘limidan PDF faylni toping.** Agar fayl mavjud bo‘lmasa, yana bir bor **"PDF olish"** tugmasini bosing.

📌 **Misol:**  
![PDF joylashuvi](Examples/Screenshot%202025-02-05%20141142.png)

---

## 📩 Aloqa Uchun

Agar loyiha haqida savollaringiz bo‘lsa yoki takliflaringiz bo‘lsa, iltimos, [Telegram](https://t.me/foziljonovs) orqali bog‘laning.
