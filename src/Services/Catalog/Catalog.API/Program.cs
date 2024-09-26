var builder = WebApplication.CreateBuilder(args);

// add services to the container


var app = builder.Build();

// configuration of the http request pipeline

app.Run();
