namespace Core.Infrastructure.Configurations
{
    public class AppConfiguration
    {
        public LaunchConfiguration LaunchConfiguration { get; set; } = new();

        public EnvironmentConfiguration EnvironmentConfiguration { get; set; } = new();

        public UserDataConfiguration UserDataConfiguration { get; set; } = new();
    }
}
