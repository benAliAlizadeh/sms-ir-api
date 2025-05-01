using Newtonsoft.Json;
using sms_ir_api.Models.SmsApiDto;
using sms_ir_api.Services.Interface;
using System.Text;

namespace sms_ir_api.Services;

public class SmsService : ISmsService
{
    private readonly string _lineNumber;
    private readonly int _TemplateId;
    private readonly HttpClient _client;

    public SmsService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _lineNumber = configuration.GetSection("SmsApi")["LineNumber"];
        _TemplateId = int.Parse(configuration.GetSection("SmsApi")["TemplateId"]);
        _client = httpClientFactory.CreateClient("smsClient");
    }

    public async Task<SmsSendResponse> SendMessage(string text, List<string> numbers)
    {
        try
        {
            SmsRoot root = new SmsRoot()
            {
                lineNumber = long.Parse(_lineNumber),
                messageText = text,
                mobiles = numbers
            };

            var jsonBody = JsonConvert.SerializeObject(root);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");


            var result = await _client.PostAsync("/v1/send/bulk", content);
            string response = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var obj = JsonConvert.DeserializeObject<SmsSendResponse>(response);
                return await Task.FromResult(obj);

            }

            throw new Exception($"status not okey. status={result.StatusCode}\n{JsonConvert.DeserializeObject(response)}");
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public async Task<SmsSendResponse> SendMessage(string text, string number)
    {
        return await SendMessage(text, new List<string> { number });
    }

    public async Task<VerifySmsSendResponse> SendOtpMessage(string otpCode, string number)
    {
        try
        {
            List<VerifyParameter> parameters = new()
            {
                new()
                {
                    name = "CODE",
                    value = otpCode
                }
            };

            VerifySmsRoot root = new VerifySmsRoot()
            {
                mobile = number,
                templateId = _TemplateId,
                parameters = parameters
            };

            var jsonBody = JsonConvert.SerializeObject(root);
            var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");


            var result = await _client.PostAsync("/v1/send/verify", content);
            string response = await result.Content.ReadAsStringAsync();

            if (result.IsSuccessStatusCode)
            {
                var obj = JsonConvert.DeserializeObject<VerifySmsSendResponse>(response);
                return await Task.FromResult(obj);

            }

            throw new Exception($"status not okey. status={result.StatusCode}\n{JsonConvert.DeserializeObject(response)}");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
