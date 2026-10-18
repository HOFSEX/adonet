using DBConnect.Data;
using DBConnect.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

string connectionString =
    "Data Source=(localdb)\\MSSQLLocalDB;" +
    "Initial Catalog=College_DB;" +
    "Integrated Security=True;" +
    "TrustServerCertificate=True;";

var studentRepo = new StudentRepository(connectionString);
var groupRepo = new GroupRepository(connectionString);

// Урок 13
app.MapGet("/groups", () => groupRepo.GetAll());

// Урок 15
app.MapGet("/students/all", () => studentRepo.GetAll());

// Урок 16
app.MapGet("/student/{id:int}", (int id) =>
{
    var student = studentRepo.GetById(id);
    return student is null ? Results.NotFound() : Results.Ok(student);
});

// Урок 14 — фронт
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
