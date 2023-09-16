using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core;
using Talabat.Core.Entitys.Identity;
using Talabat.Core.IRepositiry;
using Talabat.Repositiry;
using Talabat.Repositiry.Data;
using Talabat.Repositiry.Identity;
using Talabat.Repositiry.Repositiry;
using Talabt.APIs.Erorr;
using Talabt.APIs.Extentions;
using Talabt.APIs.Helper;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>
            (option => option.UseSqlServer(builder.Configuration.GetConnectionString("constr")));

        builder.Services.AddDbContext<AppIdentityDbContext>(option =>
        
        option.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConStr"))); 
        
        builder.Services.AddSingleton<IConnectionMultiplexer>(option =>
        {
            var connection = builder.Configuration.GetConnectionString("Redis");
            return ConnectionMultiplexer.Connect(connection);
        }
        );

        builder.Services.AddScoped<IBasketRepositiry, BasketRepositiry>();

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();         
        builder.Services.AddIdentityService(builder.Configuration);
        
        builder.Services.AddApplicationService(); 

        var app = builder.Build();

        app.UseMiddleware<ExceptionMiddelWare>();

        var Scop = app.Services.CreateScope(); 

        var services = Scop.ServiceProvider;
        var Dbcontext = services.GetRequiredService<AppDbContext>();
        
        await DataSeed.SeedingData(Dbcontext);

        var userManger = services.GetRequiredService<UserManager<AppUser>>(); 

        await IdentityDbContextSeed.SeedData(userManger);
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors("MyPlicy"); 
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        
        app.UseStaticFiles();
        app.MapControllers();

        app.Run();
    }
}