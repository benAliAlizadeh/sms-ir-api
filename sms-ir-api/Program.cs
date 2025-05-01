using sms_ir_api.Services;
using sms_ir_api.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// بقیه سرویس‌ها مثل ISmsService
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient("smsClient", client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetSection("SmsApi")["BaseUrl"]);
    client.DefaultRequestHeaders.Add("X-API-KEY", builder.Configuration.GetSection("SmsApi")["ApiKey"]);
    client.DefaultRequestHeaders.Add("X-SANDBOX", "1");
});
builder.Services.AddScoped<ISmsService, SmsService>();

var app = builder.Build();

app.UseSession();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapRazorPages().WithStaticAssets();

app.Run();