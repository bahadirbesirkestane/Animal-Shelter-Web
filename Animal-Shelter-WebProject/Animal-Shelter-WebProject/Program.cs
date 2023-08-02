using Animal_Shelter_WebProject.Data;
using Animal_Shelter_WebProject.Services.Admins;
using Animal_Shelter_WebProject.Services.Adoptions;
using Animal_Shelter_WebProject.Services.Password;
using Animal_Shelter_WebProject.Services.Pets;
using Animal_Shelter_WebProject.Services.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Account/Login";
                        options.LogoutPath = "/Account/Login";
                    });


builder.Services.AddDbContext<Animal_Shelter_WebProjectDBContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("mssql")));

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdoptionService, AdoptionService>();

//builder.Services.AddScoped<IMedicineService, MedicineService>();
//builder.Services.AddScoped<ITrackingService, TrackingService>();
//builder.Services.AddScoped<IInformationService, InformationService>();



builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromDays(1);
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

app.UseAuthentication();

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Kayitsiz}/{id?}");

app.Run();