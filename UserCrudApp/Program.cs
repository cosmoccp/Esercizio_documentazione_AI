using Microsoft.EntityFrameworkCore;
using UserCrudApp.Data;
using UserCrudApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserDbContext>(options => options.UseInMemoryDatabase("UsersDb"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/users", async (UserDbContext context) =>
    await context.Users.ToListAsync());

app.MapGet("/users/{id}", async (int id, UserDbContext context) =>
{
    var user = await context.Users.FindAsync(id);
    return user is null ? Results.NotFound() : Results.Ok(user);
});

app.MapPost("/users", async (CreateUserRequest request, UserDbContext context) =>
{
    var user = new User
    {
        Name = request.Name,
        Email = request.Email
    };

    context.Users.Add(user);
    await context.SaveChangesAsync();

    return Results.Created($"/users/{user.Id}", user);
});

app.MapPut("/users/{id}", async (int id, UpdateUserRequest request, UserDbContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user is null)
    {
        return Results.NotFound();
    }

    user.Name = request.Name;
    user.Email = request.Email;
    await context.SaveChangesAsync();

    return Results.Ok(user);
});

app.MapDelete("/users/{id}", async (int id, UserDbContext context) =>
{
    var user = await context.Users.FindAsync(id);
    if (user is null)
    {
        return Results.NotFound();
    }

    context.Users.Remove(user);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.Run();
