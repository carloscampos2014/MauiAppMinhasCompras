using SQLite;

namespace MauiAppMinhasCompras.Models;

[Table("Produto")]
public class ProdutoModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [MaxLength(100)]
    public string Descricao { get; set; } = string.Empty;

    public double Preco { get; set; }

    public double Quantidade { get; set; }

    [Ignore]
    public virtual double Total => Preco * Quantidade;
}
