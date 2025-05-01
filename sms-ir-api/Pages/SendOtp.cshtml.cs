using sms_ir_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using sms_ir_api.Services.Interface;

namespace sms_ir_api.Pages;

public class SendOtpModel : PageModel
{
    private readonly ISmsService _smsService;

    [BindProperty]
    public OtpModel Otp { get; set; }

    public SendOtpModel(ISmsService smsService)
    {
        _smsService = smsService;
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrEmpty(Otp.Mobile))
        {
            return Page();
        }

        // چک کردن شماره موبایل ایرانی
        if (!Regex.IsMatch(Otp.Mobile, @"^(?:\+98|0)?9\d{9}$"))
        {
            ModelState.AddModelError("Otp.Mobile", "شماره موبایل باید ایرانی باشد.");
            return Page();
        }

        // تولید کد OTP تصادفی (6 رقمی)
        string code = new Random().Next(100000, 999999).ToString();

        // ارسال OTP با متد SmsService
        var response = await _smsService.SendOtpMessage(code, Otp.Mobile);
        if (response.status == 1) // فرض می‌کنیم Status=1 یعنی موفقیت
        {
            // ذخیره کد و شماره توی Session
            HttpContext.Session.SetString("OtpCode", code);
            HttpContext.Session.SetString("Mobile", Otp.Mobile);
            TempData["Success"] = "کد OTP ارسال شد.";
            return RedirectToPage("VerifyOtp");
        }
        else
        {
            ModelState.AddModelError("", "خطا در ارسال OTP.");
            return Page();
        }
    }
}