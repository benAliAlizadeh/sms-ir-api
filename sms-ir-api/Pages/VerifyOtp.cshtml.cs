using sms_ir_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace sms_ir_api.Pages;

public class VerifyOtpModel : PageModel
{
    [BindProperty]
    public OtpModel Otp { get; set; }

    public void OnGet()
    {
        Otp = new OtpModel
        {
            Mobile = HttpContext.Session.GetString("Mobile")
        };
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        string storedCode = HttpContext.Session.GetString("OtpCode");
        if (storedCode == Otp.Code)
        {
            TempData["Success"] = "کد OTP صحیح است!";
        }
        else
        {
            ModelState.AddModelError("Otp.Code", "کد OTP اشتباه است.");
        }

        return Page();
    }
}