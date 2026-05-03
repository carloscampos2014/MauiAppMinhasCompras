using MauiAppMinhasCompras.Models;
using SQLite;

namespace MauiAppMinhasCompras.Helpers;

public class SQLiteDatabaseHelper
{
    private readonly SQLiteAsyncConnection _connection;

    public SQLiteDatabaseHelper(string path)
    {
        _connection = new SQLiteAsyncConnection(path);
        _connection.CreateTableAsync<ProdutoModel>().Wait();
    }

    public async Task<List<ProdutoModel>> GetAllProdutos()
    {
        return await _connection.Table<ProdutoModel>()
            .ToListAsync();
    }

    public async Task<int> Delete(int id)
    {
        return await _connection.Table<ProdutoModel>().DeleteAsync(p => p.Id == id);
    }

    public async Task<int> InsertProduto(ProdutoModel item)
    {
        return await _connection.InsertAsync(item);
    }

    public async Task<List<ProdutoModel>> SearchProduto(string searchTerm)
    {
        return await _connection.Table<ProdutoModel>()
            .Where(p => p.Descricao.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase))
            .ToListAsync();
    }

    public async Task<int> UpdateProduto(ProdutoModel item)
    {
        string sql = "UPDATE Produto SET Descricao = ?, Preco = ?, Quantidade = ? WHERE Id = ?";
        return await _connection.ExecuteAsync(sql, item.Descricao, item.Preco, item.Quantidade, item.Id);
    }

    private async Task CreateTableAsync<T>() where T : new()
    {
        await _connection.CreateTableAsync<T>();
    }
}
