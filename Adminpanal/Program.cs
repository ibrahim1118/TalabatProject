using Adminpanal.Hellper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core;
using Talabat.Core.Entitys.Identity;
using Talabat.Repositiry;
using Talabat.Repositiry.Data;
using Talabat.Repositiry.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>
           (option => option.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

builder.Services.AddDbContext<AppIdentityDbContext>(option =>

option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConStr")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>(); 

builder.Services.AddAutoMapper(p=>p.AddProfile(new MappingProFile()));  



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=Login}/{id?}");

app.Run();
