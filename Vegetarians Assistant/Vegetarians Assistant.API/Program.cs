using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Vegetarians_Assistant.API.Mapper;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Implement;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.Services.Implement.Admin;
using Vegetarians_Assistant.Services.Services.Implement.Customer;
using Vegetarians_Assistant.Services.Services.Implement.Dish;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Interface.ArticleImp;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.IArticle;
using Vegetarians_Assistant.Services.Services.Interface.Dish;
using Vegetarians_Assistant.Services.Services.Interface.Feedback;
using Vegetarians_Assistant.Services.Services.Implement.Feedback;
using Vegetarians_Assistant.Services.Services.Interface.Membership;
using Vegetarians_Assistant.Services.Services.Interface.CartImp;
using Vegetarians_Assistant.Services.Services.Interface.ICart;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;
using Vegetarians_Assistant.Services.Services.Implement.OrderImp;
using Vegetarians_Assistant.Services.Services.Interface.IFollowImp;
using Vegetarians_Assistant.Services.Services.Implement.FollowImp;
using Vegetarians_Assistant.API.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Vegetarians_Assistant.Services.Services.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add connection string
builder.Services.AddDbContext<VegetariansAssistantV3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

//add Repo
builder.Services.AddScoped<ArticleRepository>();
builder.Services.AddScoped<CartRepository>();

//Add service to the container
builder.Services.AddScoped<ILoginAService, LoginAService>();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddScoped<IDishManagementService, DishManagementService>();

builder.Services.AddScoped<ICustomerManagementService, CustomerManagementService>();

builder.Services.AddScoped<IArticleService, ArticleService>();

builder.Services.AddScoped<IFeedbackManagementService, FeedbackManagementService>();

builder.Services.AddScoped<IMembershipTierService, MembershipTierService>();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();

builder.Services.AddScoped<ICartService, CartService>();

builder.Services.AddScoped<IUsermembershipService, UserMembershipService>();

builder.Services.AddScoped<IOrderManagementService, OrderManagementService>();

builder.Services.AddScoped<IFollowManagementService, FollowManagementService>();

builder.Services.AddScoped<AuthService>();



// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program), typeof(Mapping));

//Memory cache
builder.Services.AddMemoryCache();

////Addcors
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//// Configure JWT Authentication
//var jwtConfig = builder.Configuration.GetSection("JwtConfig").Get<JwtConfig>();
//builder.Services.AddSingleton(jwtConfig);

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Secret)),
//            ValidateIssuer = false,
//            ValidateAudience = false
//        };
//    });

var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapControllers();

app.Run();
