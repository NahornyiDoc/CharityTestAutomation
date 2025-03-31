using Autofac;
using Autofac.Extensions.DependencyInjection;
using Core.Infrastructure.Configurations;
using Core.ServiceLocator;
using Core.Web.WebDriver;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace Web.Test.ReportPortal
{
    public class Startup
    {
        private static IConfiguration? _configuration { get; set; }

        private static AppConfiguration? _appConfiguration;

        public Startup() { }

        [ModuleInitializer]
        public static void Setup()
        {
            var startupFileLocation = Path.GetDirectoryName(typeof(Startup).Assembly.Location);

            if (string.IsNullOrEmpty(startupFileLocation))
            {
                throw new InvalidOperationException("The startup file location could not be determined.");
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(startupFileLocation)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            ConfigureServices();
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddOptions();

            _appConfiguration = new AppConfiguration();

            _configuration.Bind("LaunchConfiguration", _appConfiguration.LaunchConfiguration);
            _configuration.Bind("EnvironmentConfiguration", _appConfiguration.EnvironmentConfiguration);
            _configuration.Bind("UserDataConfiguration", _appConfiguration.UserDataConfiguration);

            services.AddSingleton(_appConfiguration);
            services.AddSingleton(_appConfiguration.LaunchConfiguration);
            services.AddSingleton(_appConfiguration.EnvironmentConfiguration);
            services.AddSingleton(_appConfiguration.UserDataConfiguration);

            var builder = new ContainerBuilder();

            builder.RegisterInstance(new PlaywrightConfigurations(_appConfiguration.LaunchConfiguration)).AsSelf();

            builder.Populate(services);

            var container = builder.Build();

            AppContainer.SetResolver(container);

            return new AutofacServiceProvider(container);
        }
    }
}
