using WebApiDemo;

var builder = WebApplication.CreateBuilder(args);

List<User> users = new()
{
    new(){ UserId = Guid.Parse("c5eb988c-aa12-40ed-838b-6ff34370b8b4"), FirstName = "Ottorino", LastName = "Bruni" },
    new(){ UserId = Guid.Parse("50f6228e-e7cf-4ef6-a9a7-fd455f491dc4"), FirstName = "Luke", LastName = "Skywalker" },
    new(){ UserId = Guid.Parse("87d713b2-ffcf-4ba0-b5ba-3de1109a642c"), FirstName = "Anakin", LastName = "Skywalker" }
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
    var user = users.FirstOrDefault(u => u.UserId == id);

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