namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{
	public ListaProduto()
	{
		InitializeComponent();
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
}