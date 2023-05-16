using api_back;
using api_back.Data;
using api_back.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

if(appSettings.UseJson)
{
    builder.Services.AddTransient<IUserService, UserServices >();
    builder.Services.AddTransient<IRoleService, RoleServices>();
}
else
{
    builder.Services.AddTransient<IRoleService, RolesServicesDb>();
    builder.Services.AddTransient<IUserService, UserServicesDb>();

}

builder.Services.AddDbContext<UserApiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("UserApiDbContext")));
//builder.Services.AddDbContext<UserApiDbContext>(options => options.UseInMemoryDatabase("UserDb"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<UserApiDbContext>();
    dbContext.Database.Migrate();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();


app.Run();
