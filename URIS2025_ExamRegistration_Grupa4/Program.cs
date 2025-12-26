using URIS2025_ExamRegistration_Grupa4.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Navodimo da se svaki put prilikom koriscnja IExamRegistrationRepository interfejsa koristi jedna instanca ExamRegistrationRepository klase
// Ovo je neophodno kako bi se izbeglo pravljenje novih instanci klase prilikom svakog poziva
// Kada se budemo povezali sa bazom podataka, AddSingleton metodu zamenjujemo sa AddScoped
builder.Services.AddSingleton<IExamRegistrationRepository, ExamRegistrationRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
