using BWD.Web.Areas.Identity.Data;
using BWD.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BWDWebContextConnection") ?? throw new InvalidOperationException("Connection string 'BWDWebContextConnection' not found.");

builder.Services.AddDbContext<BWDWebContext>(options =>
	options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<BWDWebContext>();

builder.Services.AddDefaultIdentity<Users>(options =>
{
	options.SignIn.RequireConfirmedAccount = false;
	options.Password.RequireDigit = false;
	options.Password.RequireLowercase = false;
}).AddRoles<IdentityRole>()
 .AddEntityFrameworkStores<BWDWebContext>()
 .AddDefaultTokenProviders();

// Add services to the container.
builder.Services.AddRazorPages();

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
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapRazorPages();

app.Run();
