using sms_ir_api.Models.SmsApiDto;

namespace sms_ir_api.Services.Interface;

public interface ISmsService
{
    Task<SmsSendResponse> SendMessage(string text, List<string> numbers);
    Task<SmsSendResponse> SendMessage(string text, string number);
    Task<VerifySmsSendResponse> SendOtpMessage(string otpCode, string number);
}
