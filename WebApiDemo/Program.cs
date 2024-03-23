using WebApiDemo;

var builder = WebApplication.CreateBuilder(args);

List<User> users = new()
{
    new(){ UserId = Guid.NewGuid(), FirstName = "Ottorino", LastName = "Bruni" },
    new(){ UserId = Guid.NewGuid(), FirstName = "Luke", LastName = "Skywalker" },
    new(){ UserId = Guid.NewGuid(), FirstName = "Anakin", LastName = "Skywalker" }
};

// Add services to the container.
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

app.MapGet("Users",  () => users);
app.MapPost("Users",  (User user) => users.Add(user));
app.MapPut("Users/{id}", (Guid id, User inputUser) => 
{
    var user = users.Where( u => u.UserId == id).FirstOrDefault();

    if (user is null)
    {
        return Results.NotFound();
    }
    else 
    {
        user.FirstName = inputUser.FirstName;
        user.LastName = inputUser.LastName;
        return Results.NoContent();
    }
});
app.MapDelete("Users/{id}", (Guid id) => users.Remove(users.Find((u) => u.UserId == id)!));

app.Run();