using Microsoft.EntityFrameworkCore;
using Task_Manager_Application.Data;
using Task_Manager_Application.Interfaces;
using Task_Manager_Application.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddDbContext<TaskDbContext>(db =>
    db.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddTransient<ITaskService, TaskServices>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
