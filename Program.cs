using System.Text;
using dbapp.Helpers;
using dbapp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddSingleton(configuration);
builder.Services.AddScoped<SqlHelper>();

builder.Services.AddScoped<JwtService>();


builder.Services.AddControllersWithViews();

builder.Services.AddRouting(options => {
    options.LowercaseUrls = true;
    options.AppendTrailingSlash = false;
});

builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new TokenValidationParameters {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
    };

    options.Events = new JwtBearerEvents {
        OnMessageReceived = context => {
            context.Token = context.Request.Cookies["JWT"];
            return Task.CompletedTask;
        }
    };
});

var app = builder.Build();


if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Error/HandleError", "?statusCode={0}");

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=ChoosePersonType}");

app.MapControllerRoute(
    name: "Login",
    pattern: "{controller=Home}/{action=Login}");

app.MapControllerRoute(
    name: "CustomerRegister",
    pattern: "{controller=Home}/{action=CustomerRegister}");

app.MapControllerRoute(
    name: "EmployeeRegister",
    pattern: "{controller=Home}/{action=EmployeeRegister}");

app.MapControllerRoute(
    name: "CustomerDashboard",
    pattern: "{controller=Customer}/{action=CustomerDashboard}");

app.Run();