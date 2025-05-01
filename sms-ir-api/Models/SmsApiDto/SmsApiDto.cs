namespace sms_ir_api.Models.SmsApiDto;

public class SmsSendResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public SmsData data { get; set; }
}
public class SmsData
{
    public string packId { get; set; }
    public List<int> messageIds { get; set; }
    public double cost { get; set; }
}

public class VerifySmsSendResponse
{
    public int status { get; set; }
    public string message { get; set; }
    public VerifySmsData data { get; set; }
}
public class VerifySmsData
{
    public int messageId { get; set; }
    public decimal cost { get; set; }
}

//Request class
public class SmsRoot
{
    public long lineNumber { get; set; }
    public string messageText { get; set; }
    public List<string> mobiles { get; set; }
}

public class VerifySmsRoot
{
    public string mobile { get; set; }
    public int templateId { get; set; }
    public List<VerifyParameter> parameters { get; set; }

}

public class VerifyParameter
{
    public string name { get; set; }
    public string value { get; set; }

}
