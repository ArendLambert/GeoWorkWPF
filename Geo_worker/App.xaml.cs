using Core.DataCreate;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;


namespace Geo_worker;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private IHost? _host;

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        _host = Host.CreateDefaultBuilder(e.Args)
            .ConfigureServices((context, services) =>
            {
                // Регистрируем DbContext
                _ = services.AddDbContext<GravitySurveyOnDeleteNoAction>(options =>
                    options.UseSqlServer("Server=.\\SQLEXPRESS;Database=GravitySurveyOnDeleteNoAction;Trusted_Connection=True;TrustServerCertificate=True;"));

                // Регистрируем UnitOfWork
                _ = services.AddScoped<UnitOfWork>();
            })
            .Build();


        using (IServiceScope scope = _host.Services.CreateScope())
        {
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                // Получаем контекст и применяем миграции
                GravitySurveyOnDeleteNoAction context = services.GetRequiredService<GravitySurveyOnDeleteNoAction>();
                await context.Database.MigrateAsync();

                // Получаем UnitOfWork
                UnitOfWork unitOfWork = services.GetRequiredService<UnitOfWork>();

                // Вызываем метод создания синтетических данных
                await SyntheticData.CreateAll();

                // Запускаем главное окно
                MainWindow mainWindow = new();
                mainWindow.Show();
            }
            catch (Exception ex)
            {
                ILogger<App> logger = services.GetRequiredService<ILogger<App>>();
                logger.LogError(ex, "Ошибка при выполнении миграций или заполнении базы данных");
                _ = MessageBox.Show("Ошибка при инициализации базы данных. Проверьте логирование.");
                Shutdown();
            }
        }

    }

    protected override async void OnExit(ExitEventArgs e)
    {
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
        base.OnExit(e);
    }

}

