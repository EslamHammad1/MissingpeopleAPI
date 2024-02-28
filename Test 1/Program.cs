using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Test_1.DataEF;
 

var builder = WebApplication.CreateBuilder(args);
//builder.Configuration.AddUserSecrets<ApplicationUser>();// new
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
/*
builder.Services.AddCors(crosOptions =>
{
    crosOptions.AddPolicy("MyPolicy", CorsPolicyBuilder =>
    {
        CorsPolicyBuilder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
*/
builder.Services.AddDbContext<MissingPersonEntity>(options =>
{
    options.UseSqlServer("Data Source =(localdb)\\ProjectModels; Initial Catalog = MissingpersonLast ; Integrated Security =True ; Trusted_Connection=True ; Encrypt = False");
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().
    AddEntityFrameworkStores<MissingPersonEntity>().AddDefaultTokenProviders(); // add AddDefaultTokenProviders for resat pass or forget
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudiance"],
        IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});
// todo: add login with google and facebook
// todo: add multi language
//new
//builder.Services.AddAuthentication().AddGoogle(options =>
//{
//    IConfiguration googleauth = builder.Configuration.GetSection("Authentication:Google");
//    options.ClientId = googleauth["ClientId"];
//    options.ClientSecret = googleauth["ClientSecret"];

//});

/// swagger buttom 
builder.Services.AddSwaggerGen(Options =>
{
    Options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Missing person",
        Version = "v1",
        Description = "{\r\n  \"userName\": \"Eslam_Hammad\",\r\n  \"password\": \"Eslam1234@\"\r\n}",

        Contact = new OpenApiContact
        {
            Name = "Eslam Hammad",
            Email = "eslamhammadxz13@gmail.com\r\n",
            Url = new Uri("https://www.linkedin.com/in/eslamhamma74/")

        }
    });
    Options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
    });
    Options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                    {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                    },
                    Name ="Bearer",
                    In = ParameterLocation.Header
                    },
                    new List<string>()

                    }
    });

});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseStaticFiles();
//app.UseCors("MyPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
