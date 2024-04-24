using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using Sound.Shared.Modelos.Modelos;
using SoundAPI.Request;

namespace SoundAPI.EndPoints;

public static class MusicaExtension
{
    public static void AddEndpointMusicas(this WebApplication app)
    {
        #region ROTAS MUSICAS

        app.MapGet("/Musicas", ([FromServices] DAL<Musica> conexao) => Results.Ok(conexao.Listar()));

        app.MapGet("/Musicas/{nome}", ([FromServices] DAL<Musica> conexao, string nome) =>
        {
            Musica? musica = conexao.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            return musica is null ? Results.NotFound() : Results.Ok(musica);
        });

        app.MapPost("/Musicas", (
            [FromServices] DAL<Musica> conexao,
            [FromServices] DAL<Genero> conexaoGenero,
            [FromBody] MusicaRequest musicaRequest) =>
        {
            Musica musica = new(musicaRequest.nome)
            {
                ArtistaId = musicaRequest.ArtistaId,
                AnoLancamento = musicaRequest.anoLancamento,
                Generos = musicaRequest.Generos is not null
                ? GeneroConverter(musicaRequest.Generos, conexaoGenero)
                : new List<Genero>()

            };
            conexao.Adicionar(musica);
            return Results.Ok();
        });

        app.MapDelete("/Musicas/{id}", ([FromServices] DAL<Musica> conexao, int id) =>
        {
            Musica? musica = conexao.RecuperarPor(a => a.Id == id);
            if (musica is null)
                return Results.NotFound();

            conexao.Deletar(musica);
            return Results.NoContent();
        });

        app.MapPut("/Musicas", ([FromServices] DAL<Musica> conexao, [FromBody] MusicaRequestEdit MusicaRequestEdit) =>
        {
            Musica? AlteraMusica = conexao.RecuperarPor(a => a.Id == MusicaRequestEdit.Id);
            if (AlteraMusica is null)
                return Results.NotFound();

            AlteraMusica.Nome = MusicaRequestEdit.nome;
            AlteraMusica.AnoLancamento = MusicaRequestEdit.anoLancamento;

            conexao.Atualizar(AlteraMusica);
            return Results.Ok();
        });

        #endregion ROTAS MUSICAS
    }

    private static ICollection<Genero> GeneroConverter(
        ICollection<GeneroRequest> generos,
        DAL<Genero> conexaoGenero)
    {
        List<Genero> listaGeneros = new();
        foreach (GeneroRequest item in generos)
        {
            Genero entity = RequestToEntity(item);
            Genero? genero = conexaoGenero.RecuperarPor(g => g.Nome.ToUpper().Equals(item.Nome.ToUpper()));

            if (genero is not null)
            {
                listaGeneros.Add(genero);
            }
            else
            {
                listaGeneros.Add(entity);
            }
        }

        return listaGeneros;
    }

    private static Genero RequestToEntity(GeneroRequest genero)
        => new() { Nome = genero.Nome, Descricao = genero.Descricao };


}
