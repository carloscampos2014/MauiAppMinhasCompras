using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	private readonly ObservableCollection<ProdutoModel> lista;

	public ListaProduto()
	{
		InitializeComponent();
		lista = new ObservableCollection<ProdutoModel>();
		lstProdutos.ItemsSource = lista;
    }

	protected override async void OnAppearing()
	{
        try
        {
            base.OnAppearing();
            await CarregarProdutos(string.Empty);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }

    private async void btnAdicionar_Clicked(object sender, EventArgs e)
    {
		try
		{
			await Navigation.PushAsync(new NovoProduto());
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
		}
    }

    private async void txtPesquisar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string filtro = e.NewTextValue;
            await CarregarProdutos(filtro);
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }

    private async void mnuRemover_Clicked(object sender, EventArgs e)
    {
        try
        {
            var menuItem = sender as MenuItem;
            var produto = menuItem?.BindingContext as ProdutoModel;
            if (produto != null)
            {
                bool confirmacao = await DisplayAlertAsync("Confirmação", $"Deseja realmente remover o produto {produto.Descricao}?", "Sim", "Não");
                if (confirmacao)
                {
                    await App.Db.Delete(produto.Id);
                    lista.Remove(produto);
                    SomarProdutos();
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }

    private void SomarProdutos()
	{
		double total = lista.Sum(p => p.Total);
		lblTotalGeral.Text = $"Total Geral: {total:C}";
    }

    private async Task CarregarProdutos(string filtro)
	{
		var produtos = string.IsNullOrEmpty(filtro) ? 
			await App.Db.GetAllProdutos() : 
			await App.Db.SearchProduto(filtro);
		lista.Clear();
		produtos.ForEach(p => lista.Add(p));
        SomarProdutos();
    }

    private async void lstProdutos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            var produto = e.SelectedItem as ProdutoModel;
            if (produto != null)
            {
                await Navigation.PushAsync(new EditarProduto() { BindingContext = produto });
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops...", $"Ocorreu um erro -> {ex.Message}", "OK");
        }
    }
}