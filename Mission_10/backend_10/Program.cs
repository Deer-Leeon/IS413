using backend_10.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext with SQLite
builder.Services.AddDbContext<BowlingContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("BowlingDb")));

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // Allow requests from this origin
                .AllowAnyHeader() // Allow any header
                .AllowAnyMethod(); // Allow any HTTP method (GET, POST, etc.)
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseRouting();

// Use the CORS policy
app.UseCors("AllowLocalhost");

app.UseAuthorization();

app.MapControllers();

app.Run();