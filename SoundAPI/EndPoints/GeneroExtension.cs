using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using Sound.Shared.Modelos.Modelos;
using SoundAPI.Request;
using SoundAPI.Response;
namespace SoundAPI.EndPoints;
public static class GeneroExtensions
{
    public static void AddEndPointGeneros(this WebApplication app)
    {
        app.MapPost("/Generos", ([FromServices] DAL<Genero> conexao, [FromBody] GeneroRequest generoReq)
            => conexao.Adicionar(RequestToEntity(generoReq)));

        app.MapGet("/Generos", ([FromServices] DAL<Genero> conexao)
            => EntityListToResponseList(conexao.Listar()));

        app.MapGet("/Generos/{nome}", ([FromServices] DAL<Genero> conexao, string nome) =>
        {
            Genero? genero = conexao.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            if (genero is not null)
            {
                GeneroResponse response = EntityToResponse(genero!);
                return Results.Ok(response);
            }
            return Results.NotFound("Gênero não encontrado.");
        });

        app.MapDelete("/Generos/{id}", ([FromServices] DAL<Genero> conexao, int id) =>
        {
            Genero? genero = conexao.RecuperarPor(a => a.Id == id);
            if (genero is null)
            {
                return Results.NotFound("Gênero para exclusão não encontrado.");
            }
            conexao.Deletar(genero);
            return Results.NoContent();
        });
    }
    private static Genero RequestToEntity(GeneroRequest generoRequest)
        => new() { Nome = generoRequest.Nome, Descricao = generoRequest.Descricao };

    private static ICollection<GeneroResponse> EntityListToResponseList(IEnumerable<Genero> generos)
        => generos.Select(a => EntityToResponse(a)).ToList();

    private static GeneroResponse EntityToResponse(Genero genero)
        => new(genero.Id, genero.Nome!, genero.Descricao!);

}
