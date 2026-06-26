using Microsoft.Extensions.Logging;
using MyGroceryList.Data;
using MyGroceryList.ViewModels;
using System.IO;

namespace MyGroceryList
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "grocery.db3");
            builder.Services.AddSingleton<AppDatabase>(s => new AppDatabase(dbPath));
            builder.Services.AddSingleton<GroceryListViewModel>();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
