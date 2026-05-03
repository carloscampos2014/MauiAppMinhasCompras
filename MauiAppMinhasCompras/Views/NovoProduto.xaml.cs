using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void mnuCancelar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string conteudo = string.Concat(txtDescricao.Text, txtQuantidade.Text, txtPrecoUnitario.Text);
            bool cancelar = true;
            if (!string.IsNullOrEmpty(conteudo))
            {
                cancelar = await DisplayAlertAsync("Atenção", "Deseja realmente cancelar o cadastro do produto?", "Sim", "Não");
            }

            if (cancelar)
            {
                await Navigation.PopAsync();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }

    private async void mnuSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            bool salvar = await DisplayAlertAsync("Atenção", "Deseja realmente salvar o produto?", "Sim", "Não");
            if (salvar)
            {
                var produto = new ProdutoModel
                {
                    Descricao = txtDescricao.Text,
                    Quantidade = Convert.ToDouble(txtQuantidade.Text),
                    Preco = Convert.ToDouble(txtPrecoUnitario.Text)
                };

                int qtd = await App.Db.InsertProduto(produto);
                if (qtd > 0)
                {
                    await DisplayAlertAsync("Atenção", "Produto Adicionado com Sucesso!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    throw new InvalidOperationException("Não foi Possível Adicionar o Produto!");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }
}