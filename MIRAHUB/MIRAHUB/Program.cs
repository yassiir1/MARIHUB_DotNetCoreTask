using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MIRAHUB.Models;
using MIRAHUB.Services;
using System;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<MIRAHUBDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MIRAHUBDb") ?? throw new InvalidOperationException("Connection string 'MIRAHUBDb' not found.")));
var _JWT = builder.Configuration.GetSection("Jwt");
builder.Services.Configure<Jwt>(_JWT);
var AuthKey = builder.Configuration.GetValue<string>("Jwt:Secret");
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(item =>
{
    item.RequireHttpsMetadata = true; item.SaveToken = true; item.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthKey)),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});


builder.Services.AddScoped<IAccountsServices, AccountsServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();

builder.Services.AddEndpointsApiExplorer();
// Add services to the container.
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<MIRAHUBDb>()
    .AddDefaultTokenProviders();
builder.Services.AddDataProtection()
    .DisableAutomaticKeyGeneration();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddAuthorization();



var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
