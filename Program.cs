using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<ITransferService, TransferService>();
builder.Services.AddSingleton<IMessageQueue, RabbitMQService>();

var app = builder.Build();

app.MapControllers();

app.Run();
