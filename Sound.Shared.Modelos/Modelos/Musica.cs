using Sound.Shared.Modelos.Modelos;

namespace ScreenSound.Modelos;

public class Musica
{
    public Musica()
    {

    }
    public Musica(string nome) => Nome = nome;
    public Musica(string nome, int anoLancamento, int id)
    {
        Nome = nome;
        AnoLancamento = anoLancamento;
        Id = id;
    }

    public string Nome { get; set; }
    public int Id { get; set; }
    public int? AnoLancamento { get; set; }
    public int? ArtistaId { get; set; }
    public virtual Artista? Artista { get; set; }
    public virtual ICollection<Genero> Generos { get; set; }

    public void ExibirFichaTecnica() => Console.WriteLine($"Nome: {Nome}");

    public override string ToString() => @$"Id: {Id}
        Nome: {Nome}";
}