using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using StackF5.Data;
using StackF5.Repository.Comment;
using StackF5.Repository.Incidence;
using StackF5.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var conStrBuilder = new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("DefaultConnection"));
    var dbPassword = builder.Configuration["SecretManager:DbPassword"];
    
    if (string.IsNullOrEmpty(dbPassword))
    {
        throw new ArgumentNullException("Password cannot be null or empty", nameof(dbPassword));
    }

    conStrBuilder.Password = dbPassword;
    var connection = conStrBuilder.ConnectionString;
    options.UseSqlServer(connection);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// repository
builder.Services.AddScoped<IIncidenceRepository,IncidenceRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();