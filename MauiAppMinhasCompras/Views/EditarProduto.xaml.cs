using MauiAppMinhasCompras.Models;
using MauiAppMinhasCompras.Validations;

namespace MauiAppMinhasCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
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
                var produto = BindingContext as ProdutoModel;

                var validation = new ProdutoValidation();
                var result = await validation.ValidateAsync(produto);
                if (!result.IsValid)
                {
                    string errors = string.Join(Environment.NewLine, result.Errors.Select(e => e.ErrorMessage));
                    await DisplayAlertAsync("Erros de Validação", errors, "OK");
                    return;
                }

                int qtd = await App.Db.UpdateProduto(produto);
                if (qtd > 0)
                {
                    await DisplayAlertAsync("Atenção", "Produto Atualizado com Sucesso!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    throw new InvalidOperationException("Não foi Possível Atualizar o Produto!");
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }
}