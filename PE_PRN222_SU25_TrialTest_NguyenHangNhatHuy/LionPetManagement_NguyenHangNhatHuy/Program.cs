using LionPetManagement_NguyenHangNhatHuy;
using LionPetManagement_NguyenHangNhatHuy.BLL;
using LionPetManagement_NguyenHangNhatHuy.DAL;
using LionPetManagement_NguyenHangNhatHuy.DAL.UnitOfWorks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<Su25lionDbContext>(options =>
		  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionStringDB")));

builder.Services.AddScoped<UnitOfWork>();
builder.Services.AddScoped(typeof(GenericRepository<>));
builder.Services.AddScoped<LionAccountService>();
builder.Services.AddScoped<LionProfileService>();
builder.Services.AddScoped<LionTypeService>();
builder.Services.AddAuthorization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			  .AddCookie(options =>
			  {
				  options.LoginPath = "/User/Login";
				  options.LogoutPath = "/User/Logout";
			  });

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" && !context.User.Identity.IsAuthenticated)
    {
        context.Response.Redirect("/User/Login");
        return;
    }
    await next();
});

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<LionProfileHub>("/lionProfileHub");

app.Run();
