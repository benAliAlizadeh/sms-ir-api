# sms-ir-api

A lightweight C# client for the [SMS.ir](https://sms.ir) API, built with ASP.NET Core 9. This project allows you to send single or bulk SMS, as well as OTP messages, with a fully Persian RTL interface using Razor Pages.

## Features
- Send single and bulk SMS with minimal code
- Send OTP messages for phone number verification
- Fully Persian interface with RTL support
- Service-oriented architecture with Dependency Injection
- Built with ASP.NET Core 9 and Razor Pages
- Uses the elegant Vazirmatn font for Persian text

## Technologies
- **C#** and **ASP.NET Core 9**
- **Razor Pages** for the user interface
- **HttpClient** for API communication
- **Bootstrap RTL** for responsive design
- **Session** for temporary OTP code storage

## Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- An account on [SMS.ir](https://sms.ir) with an API Key
- Visual Studio 2022 or any preferred IDE

## Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/benAliAlizadeh/sms-ir-api.git
   ```
2. Create an `appsettings.json` file using `appsettings.example.json` and add your SMS.ir API credentials:
   ```json
   {
     "SmsApi": {
       "BaseUrl": "https://api.sms.ir",
       "ApiKey": "your-api-key",
       "LineNumber": "your-line-number",
       "TemplateId": "your-template-id"
     }
   }
   ```
3. Run the project:
   ```bash
   dotnet restore
   dotnet run
   ```
4. Open `https://localhost:5001` in your browser to use the interface.

## Usage Example
### Sending an OTP
The `/SendOtp` page provides a simple form to input a phone number, validate it as an Iranian number, and send an OTP:
```csharp
var response = await _smsService.SendOtpMessage(code, mobile);
HttpContext.Session.SetString("OtpCode", code);
```

### Verifying an OTP
The `/VerifyOtp` page takes the OTP code from the user and compares it with the stored code in the session:
```csharp
string storedCode = HttpContext.Session.GetString("OtpCode");
if (storedCode == Otp.Code)
{
    TempData["Success"] = "کد OTP صحیح است!";
}
```

## Project Structure
```
sms-ir-api/
├── Pages/
│   ├── SendOtp.cshtml
│   ├── SendOtp.cshtml.cs
│   ├── VerifyOtp.cshtml
│   └── VerifyOtp.cshtml.cs
├── Models/
│   └── OtpModel.cs
├── Sevices/
│   ├── Interface/
│   │   └── ISmsService.cs
│   └── SmsService.cs
├── wwwroot/
│   ├── css/
│   │   └── site.css
│   ├── js/
│   │   └── site.js
│   └── lib/
│       └── jquery/
└── Program.cs
```

## Contributing
Contributions are welcome! Feel free to submit Issues or Pull Requests to enhance the project.

## Persian Description (توضیحات فارسی)
این پروژه یه کلاینت ساده برای API پیامکی SMS.ir هست که با ASP.NET Core 9 ساخته شده. می‌تونید باهاش پیامک تکی یا گروهی بفرستید و کد تأیید (OTP) برای اعتبارسنجی شماره موبایل ارسال کنید. رابط کاربری کاملاً فارسی و راست‌به‌چپ (RTL) داره و با فونت زیبای Vazirmatn طراحی شده. برای اطلاعات بیشتر، بخش‌های بالا رو بخونید یا با من تماس بگیرید.

---
Developed by [Ali Alizadeh](https://alizadeh82.ir/)
