using System.ComponentModel.DataAnnotations;

namespace SoundAPI.Request;

public record MusicaRequest([Required] string nome, [Required] int ArtistaId, int anoLancamento, ICollection<GeneroRequest> Generos = null);
