using Core.Infrastructure.Configurations;
using Core.Infrastructure.Enums;
using Microsoft.Playwright;

namespace Core.Web.WebDriver
{
    public class PlaywrightConfigurations
    {
        private string Channel { get; set; }

        private bool Headless { get; set; } = false;

        private float? SlowMo { get; set; } = 100;

        private TypeBrowser BrowserType { get; set; }

        private readonly LaunchConfiguration _launchConfiguration;

        public PlaywrightConfigurations(LaunchConfiguration launchConfiguration)
        {
            _launchConfiguration = launchConfiguration;

            Channel = launchConfiguration.Channel ?? "chrome";
            BrowserType = TypeBrowser.Chrome;
            Headless = launchConfiguration.Headless;
        }

        public async Task<IBrowser> GetBrowserAsync()
        {
            var playwright = await Playwright.CreateAsync();

            return BrowserType switch
            {
                TypeBrowser.Chrome => await playwright.Chromium.LaunchAsync(GetBrowserTypeLaunchOptions(_launchConfiguration)),
                TypeBrowser.Chromium => await playwright.Chromium.LaunchAsync(GetBrowserTypeLaunchOptions(_launchConfiguration)),
                TypeBrowser.Firefox => await playwright.Firefox.LaunchAsync(GetBrowserTypeLaunchOptions(_launchConfiguration)),
                TypeBrowser.Edge => await playwright.Chromium.LaunchAsync(GetBrowserTypeLaunchOptions(_launchConfiguration)),
                _ => throw new NotImplementedException()
            };
        }

        private BrowserTypeLaunchOptions GetBrowserTypeLaunchOptions(LaunchConfiguration launchConfiguration)
        {
            return new BrowserTypeLaunchOptions
            {
                Channel = launchConfiguration.Channel ?? "chrome",
                Headless = Headless,
                SlowMo = SlowMo,
            };
        }
    }
}
