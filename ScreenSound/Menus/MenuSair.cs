using ScreenSound.Modelos;

namespace ScreenSound.Menus;

public class MenuSair : Menu
{
    public override void Executar(Dictionary<string, Artista> artistasRegistrados)
    {
        Console.WriteLine("Tchau tchau :)");
    }
}
