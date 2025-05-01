using System.ComponentModel.DataAnnotations;

namespace sms_ir_api.Models;

public class OtpModel
{
    [Required(ErrorMessage = "شماره موبایل الزامی است.")]
    [RegularExpression(@"^(?:\+98|0)?9\d{9}$", ErrorMessage = "شماره موبایل باید ایرانی باشد.")]
    public string Mobile { get; set; }

    [Required(ErrorMessage = "کد تأیید الزامی است.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "کد تأیید باید 6 رقمی باشد.")]
    public string Code { get; set; }
}