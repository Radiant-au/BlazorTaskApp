using MyBlazorApp.Components;
using MyBlazorApp.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // shortcut: EnsureCreated does not apply later migrations; switch to Migrate when adding migrations.
    await db.Database.EnsureCreatedAsync();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapGet("/api/tasks", async (AppDbContext db) =>
    await db.TodoItems.AsNoTracking().OrderBy(task => task.Id).ToListAsync());

app.MapGet("/api/tasks/{id:int}", async Task<IResult> (int id, AppDbContext db) =>
{
    var task = await db.TodoItems.AsNoTracking().FirstOrDefaultAsync(task => task.Id == id);
    if (task is null) return Results.NotFound(new { error = "Task not found." });
    return Results.Ok(task);
});

app.MapPost("/api/tasks", async Task<IResult> (TaskRequest request, AppDbContext db) =>
{
    var title = request.Title?.Trim();
    if (string.IsNullOrWhiteSpace(title) || title.Length > 120)
        return Results.BadRequest(new { error = "Title must contain 1 to 120 non-whitespace characters." });

    var task = new TodoItem { Title = title, IsComplete = request.IsComplete ?? false };
    db.TodoItems.Add(task);
    await db.SaveChangesAsync();
    return Results.Created($"/api/tasks/{task.Id}", task);
});

app.MapPut("/api/tasks/{id:int}", async Task<IResult> (int id, TaskRequest request, AppDbContext db) =>
{
    var title = request.Title?.Trim();
    if (string.IsNullOrWhiteSpace(title) || title.Length > 120 || request.IsComplete is null)
        return Results.BadRequest(new { error = "A title of 1 to 120 non-whitespace characters and an isComplete value are required." });

    var task = await db.TodoItems.FindAsync(id);
    if (task is null) return Results.NotFound(new { error = "Task not found." });

    task.Title = title;
    task.IsComplete = request.IsComplete.Value;
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapDelete("/api/tasks/{id:int}", async Task<IResult> (int id, AppDbContext db) =>
{
    var task = await db.TodoItems.FindAsync(id);
    if (task is null) return Results.NotFound(new { error = "Task not found." });

    db.TodoItems.Remove(task);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

sealed record TaskRequest(string? Title, bool? IsComplete);
