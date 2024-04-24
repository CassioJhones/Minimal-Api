using ScreenSound.Banco;
using ScreenSound.Modelos;
using SoundAPI.EndPoints;
using System.Text.Json.Serialization;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<DAL<Artista>>();
builder.Services.AddTransient<DAL<Musica>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

WebApplication app = builder.Build();
app.AddEndpointArtistas();
app.AddEndpointMusicas();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();
///Artistas/pitty