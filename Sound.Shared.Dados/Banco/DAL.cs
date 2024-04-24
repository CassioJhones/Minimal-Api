namespace ScreenSound.Banco;
public class DAL<T> where T : class
{
    private readonly ScreenSoundContext bancoSQL;

    public DAL(ScreenSoundContext BancoSql) => this.bancoSQL = BancoSql;

    public IEnumerable<T> Listar() => bancoSQL.Set<T>().ToList();
    public void Adicionar(T objeto)
    {
        bancoSQL.Set<T>().Add(objeto);
        bancoSQL.SaveChanges();
    }
    public void Atualizar(T objeto)
    {
        bancoSQL.Set<T>().Update(objeto);
        bancoSQL.SaveChanges();
    }
    public void Deletar(T objeto)
    {
        bancoSQL.Set<T>().Remove(objeto);
        bancoSQL.SaveChanges();
    }

    public T? RecuperarPor(Func<T, bool> condicao) => bancoSQL.Set<T>().FirstOrDefault(condicao);

    public IEnumerable<T> ListarPor(Func<T, bool> condicao) => bancoSQL.Set<T>().Where(condicao);
}
