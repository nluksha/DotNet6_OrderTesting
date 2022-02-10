using OrderSvc_Provider.Models;
using OrderSvc_Provider.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<DiscountService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    var svc = endpoints.ServiceProvider.GetRequiredService<DiscountService>();

    endpoints.MapPost("/discount", async context =>
    {
        var model = await context.Request.ReadFromJsonAsync<DiscountModel>();
        var amount = svc.GetDiscountAmount(model.CustomerRaiting);

        await context.Response.WriteAsJsonAsync(new DiscountModel
        {
            CustomerRaiting = model.CustomerRaiting,
            AmountToDiscount = amount
        });
    });
});

app.Run();
