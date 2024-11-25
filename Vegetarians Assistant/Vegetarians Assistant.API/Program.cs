using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Vegetarians_Assistant.API.Mapper;
using Vegetarians_Assistant.Repo.Entity;
using Vegetarians_Assistant.Repo.Repositories.Implement;
using Vegetarians_Assistant.Repo.Repositories.Interface;
using Vegetarians_Assistant.Repo.Repositories.Repo;
using Vegetarians_Assistant.Services.Services.Implement.Admin;
using Vegetarians_Assistant.Services.Services.Implement.Customer;
using Vegetarians_Assistant.Services.Services.Interface.Admin;
using Vegetarians_Assistant.Services.Services.Interface.ArticleImp;
using Vegetarians_Assistant.Services.Services.Interface.Customer;
using Vegetarians_Assistant.Services.Services.Interface.IArticle;
using Vegetarians_Assistant.Services.Services.Interface.Dish;
using Vegetarians_Assistant.Services.Services.Interface.Feedback;
using Vegetarians_Assistant.Services.Services.Implement.FeedbackImp;
using Vegetarians_Assistant.Services.Services.Interface.Membership;
using Vegetarians_Assistant.Services.Services.Interface.CartImp;
using Vegetarians_Assistant.Services.Services.Interface.ICart;
using Vegetarians_Assistant.Services.Services.Interface.IOrder;
using Vegetarians_Assistant.Services.Services.Implement.OrderImp;
using Vegetarians_Assistant.Services.Services.Interface.IFollowImp;
using Vegetarians_Assistant.Services.Services.Implement.FollowImp;
using Vegetarians_Assistant.API.Config;
using System.Text;
using Vegetarians_Assistant.Services.Services.Implement;
using Vegetarians_Assistant.Services.Services.Interface.Favorite;
using Vegetarians_Assistant.Services.Services.Implement.Favorite;
using Vegetarians_Assistant.Services.Services.Implement.DishImp;
using Vegetarians_Assistant.Services.Services.Interface.INutritionCriterion;
using Vegetarians_Assistant.Services.Services.Implement.NutritionCriterionManagementService;
using Vegetarians_Assistant.Services.Services.Interface.IArticleImage;
using Vegetarians_Assistant.Services.Services.Implement.ArticleImageImp;
using Vegetarians_Assistant.API.Helpers;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Vegetarians_Assistant.API.Helpers.AesEncryption;
using Vegetarians_Assistant.API.Helpers.PayOs;
using Vegetarians_Assistant.API.Helpers.Encryption;
using Vegetarians_Assistant.Services.Services.Interface.IArticleLike;
using Vegetarians_Assistant.Services.Services.Implement.ArticleLikeImp;
using Vegetarians_Assistant.Services.Services.Interface.IIngredient;
using Vegetarians_Assistant.Services.Services.Implement.IngredientImp;
using Vegetarians_Assistant.API.Helpers.GoogleMap;
using Vegetarians_Assistant.API.Helpers.Firebase;
using Vegetarians_Assistant.Services.Services.Interface.DiscountHistories;
using Vegetarians_Assistant.Services.Services.Implement.DiscountHistories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        //options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

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

builder.Services.AddScoped<IFavoriteManagementService, FavoriteManagementService>();

builder.Services.AddScoped<INutritionCriterionManagementService, NutritionCriterionManagementService>();

builder.Services.AddScoped<IArticleImageManagementService, ArticleImageManagementService>();

builder.Services.AddScoped<ILoginCService, LoginCService>();

builder.Services.AddScoped<IArticleLikeManagementService, ArticleLikeManagementService>();

builder.Services.AddScoped<IIngredientManagementService, IngredientManagementService>();
builder.Services.AddScoped<IInvalidWordService, InvalidWordService>();
builder.Services.AddScoped<IDiscountHistoryService, DiscountHistoryService>();

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<ICommentHelper, CommentHelper>();
builder.Services.AddScoped<IEncryptionHelper, EncryptionHelper>();
builder.Services.AddScoped<IPayOSHelper, PayOSHelper>();
builder.Services.AddScoped<IGoogleMapHelper, GoogleMapHelper>();
builder.Services.AddScoped<IFirebaseHelper, FirebaseHelper>();


// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program), typeof(Mapping));

//Memory cache
builder.Services.AddMemoryCache();

////Addcors
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Vegetarians_Assistant", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

// Cấu hình xác thực JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"] ?? throw new ArgumentNullException("JWT:Key is null"))),
            ValidIssuer = builder.Configuration["JWT:Issure"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();

// Thêm HttpContextAccessor
builder.Services.AddHttpContextAccessor();
string connection_string = Environment.GetEnvironmentVariable("CONNECTION_STRING");
//Add connection string
builder.Services.AddDbContext<VegetariansAssistantV3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
var app = builder.Build();

//app.UseAuthentication();
//app.UseAuthorization();

// Configure the HTTP request pipeline.
app.UseSwagger();

app.UseSwaggerUI();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
