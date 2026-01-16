using System.Reflection;
using URIS2025_ExamRegistration_Grupa4.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


// Prosirivanje naseg servisa sa Swagger dokumentacijom
// Ovaj deo koda je neophodan kako bi se omogucilo korišćenje Swagger-a. Svaki put kada se pokrene aplikacija,
// Swagger ce generisati dokumentaciju u kojoj ce biti prikazane sve rute i metode koje se koriste u aplikaciji, ali i informacije o kreatoru specifikacije.
// U okviru Swagger-a definise se OpenApi specifikacija. OpenApi specifikacija je standard koji definise kako se dokumentuju REST API-ji.
builder.Services.AddSwaggerGen(setupAction =>
{
    setupAction.SwaggerDoc("ExamRegistrationOpenApiSpecification",
        new Microsoft.OpenApi.Models.OpenApiInfo()
        {
            Title = "Student Exam Registration API",
            Version = "1",
            // Cesto treba da dodamo neke dodatne informacije
            Description = "Pomoću ovog API-ja može da se vrši prijava ispita, modifikacija prijava ispita kao i pregled kreiranih prijava ispita.",
            Contact = new Microsoft.OpenApi.Models.OpenApiContact
            {
                Name = "Marko Marković",
                Email = "marko@mail.com",
                Url = new Uri("http://www.ftn.uns.ac.rs/")
            },
            License = new Microsoft.OpenApi.Models.OpenApiLicense
            {
                Name = "FTN licence",
                Url = new Uri("http://www.ftn.uns.ac.rs/")
            },
            TermsOfService = new Uri("http://www.ftn.uns.ac.rs/examRegistrationTermsOfService")
        });

    //Pomocu refleksije dobijamo ime XML fajla sa komentarima (ovako smo ga nazvali u Project -> Properties)
    var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    //Pravimo putanju do XML fajla sa komentarima
    var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

    //Govorimo swagger-u gde se nalazi dati xml fajl sa komentarima
    setupAction.IncludeXmlComments(xmlCommentsPath);
});

// Navodimo da se svaki put prilikom koriscnja IExamRegistrationRepository interfejsa koristi jedna instanca ExamRegistrationRepository klase
// Ovo je neophodno kako bi se izbeglo pravljenje novih instanci klase prilikom svakog poziva
// Kada se budemo povezali sa bazom podataka, AddSingleton metodu zamenjujemo sa AddScoped
builder.Services.AddSingleton<IExamRegistrationRepository, ExamRegistrationRepository>();

// Automatsko mapiranje izmedju entiteta i DTO objekata
// Navodimo da se na nivou aplikacije prilikom svake promene u entitetima automatski mapiraju i DTO objekti
builder.Services.AddAutoMapper(config => config.AddMaps(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(setupAction =>
    {
        // Podesavamo endpoint gde Swagger UI moze da pronadje OpenAPI specifikaciju
        setupAction.SwaggerEndpoint("/swagger/ExamRegistrationOpenApiSpecification/swagger.json", "Student Exam Registration API");
        setupAction.RoutePrefix = ""; //Dokumentacija ce sada biti dostupna na root-u (ne mora da se pise /swagger)
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
