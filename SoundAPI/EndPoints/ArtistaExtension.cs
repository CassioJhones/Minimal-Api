using Microsoft.AspNetCore.Mvc;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using SoundAPI.Request;

namespace SoundAPI.EndPoints;

public static class ArtistaExtension
{
    public static void AddEndpointArtistas(this WebApplication app)
    {
        #region ROTAS ARTISTAS

        app.MapGet("/Artistas", ([FromServices] DAL<Artista> conexao) => Results.Ok(conexao.Listar()));

        app.MapGet("/Artistas/{nome}", ([FromServices] DAL<Artista> conexao, string nome) =>
        {
            Artista? artista = conexao.RecuperarPor(a => a.Nome.ToUpper().Equals(nome.ToUpper()));
            return artista is null ? Results.NotFound($"{nome.ToUpper()} NAO ENCONTRADO") : Results.Ok(artista);
        });

        app.MapPost("/Artistas", ([FromServices] DAL<Artista> conexao, [FromBody] ArtistaRequest ArtistaRequest) =>
        {
            Artista artista = new(ArtistaRequest.nome, ArtistaRequest.bio);
            conexao.Adicionar(artista);
            return Results.Ok($"{artista.Nome} ADICIONADO AO BANCO");
        });

        app.MapDelete("/Artistas/{id}", ([FromServices] DAL<Artista> conexao, int id) =>
        {
            Artista? artista = conexao.RecuperarPor(x => x.Id == id);
            if (artista is null)
                return Results.NotFound($"Id:{id} Nao existe no Banco");

            conexao.Deletar(artista);
            return Results.Ok($"Id:{id} Apagado com Sucesso");
        });

        app.MapPut("/Artistas", ([FromServices] DAL<Artista> conexao, [FromBody] ArtistaRequestEdit ArtistaRequestEdit) =>
        {
            Artista? artistaAtualizar = conexao.RecuperarPor(x => x.Id == ArtistaRequestEdit.Id);
            if (artistaAtualizar is null)
                return Results.NotFound($"Id:{ArtistaRequestEdit.Id} Nao existe no Banco");

            artistaAtualizar.Nome = ArtistaRequestEdit.nome;
            artistaAtualizar.Bio = ArtistaRequestEdit.bio;

            conexao.Atualizar(artistaAtualizar);
            return Results.Ok($"{ArtistaRequestEdit.nome} ATUALIZADO");
        });

        #endregion ROTAS ARTISTAS
    }

}
