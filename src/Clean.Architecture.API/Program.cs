using Clean.Architecture.API.Filter;
using Clean.Architecture.Core;
using Clean.Architecture.Core.Accounts.Commands.Create;
using Clean.Architecture.Core.Accounts.Commands.Delete;
using Clean.Architecture.Core.Accounts.Commands.Update;
using Clean.Architecture.Core.Accounts.Queries.Get;
using Clean.Architecture.Core.Accounts.Queries.GetAll;
using Clean.Architecture.Core.Common.Response;
using Clean.Architecture.Core.Services.Implementation;
using Clean.Architecture.Core.Services.Interfaces;
using Clean.Architecture.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCore();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "AccountsAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.OperationFilter<AuthorizeCheckOperationFilter>();  // Add the filter
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(builder.Configuration.GetSection("JwtSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//builder.Services.AddSwaggerGen(c =>
//{
//    c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
//    {
//        Description = "api key.",
//        Name = "Authorization",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "basic"
//    });

//    c.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {
//            new OpenApiSecurityScheme
//            {
//                Reference = new OpenApiReference
//                {
//                    Type = ReferenceType.SecurityScheme,
//                    Id = "basic"
//                },
//                In = ParameterLocation.Header
//            },
//            new List<string>()
//        }
//    });
//});

builder.Services.AddTransient<ISampleApiService, SampleApiService>();
builder.Services.AddHttpClient("DummyJSON", (httpClient) =>
{
    httpClient.BaseAddress = new Uri("https://dummyjson.com");
});

//builder.Services.AddScoped<ICommandHandler<CreateAccountCommand>, CreateAccountCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<UpdateAccountCommand>, UpdateAccountCommandHandler>();
//builder.Services.AddScoped<ICommandHandler<DeleteAccountCommand>, DeleteAccountCommandHandler>();
//builder.Services.AddScoped<IQueryHandler<GetAllAccountQuery, IEnumerable<AccountResponse>>, GetAllAccountQueryHandler>();
//builder.Services.AddScoped<IQueryHandler<GetAccountByNumberQuery, AccountResponse>, GetAccountByNumberQueryHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
