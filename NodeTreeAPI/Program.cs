using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using NodeTree.Application.Interface;
using NodeTree.Infrastructure.SystemExceptions;
using NodeTree.Infrastructure.NodeContext;
using NodeTree.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<AppDBContext2>(options =>
//             options.UseSqlServer(builder.Configuration.GetConnectionString("sqlConnection")));

var connectionString = builder.Configuration.GetConnectionString("sqlConnection");
builder.Services.AddDbContext<AppDBContext>(x => x.UseSqlServer(connectionString));
builder.Services.AddTransient<INodeTree, NodeTreeRepository>();

//builder.Services.AddDbContext<AppDBContext2>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
