using MauiAppMinhasCompras.Helpers;

namespace MauiAppMinhasCompras;

public partial class App : Application
{
    private static SQLiteDatabaseHelper _db;

    public static SQLiteDatabaseHelper Db
    {
        get
        {
            if (_db == null)
            {
                string path = Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData),
                    "banco_sqlite_compras.db3");
                _db = new SQLiteDatabaseHelper(path);
            }

            return _db;
        }
    }
    
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var displayInfo = DeviceDisplay.MainDisplayInfo;

        var width = 800;
        var height = 600;
        var screenWidth = displayInfo.Width / displayInfo.Density;
        var screenHeight = displayInfo.Height / displayInfo.Density;
        var x = (screenWidth - width) / 2;
        var y = (screenHeight - height) / 2;
        return new Window(new NavigationPage(new Views.ListaProduto()))
        {
            Width = width,
            Height = height,
            Title = "Minhas Compras",
            X = x,
            Y = y
        };

    }
}