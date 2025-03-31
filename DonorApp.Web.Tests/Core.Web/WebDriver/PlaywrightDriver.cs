using Core.Infrastructure.Configurations;
using Microsoft.Playwright;

namespace Core.Web.WebDriver
{
    public class PlaywrightDriver
    {
        public LaunchConfiguration LaunchConfiguration;

        public PlaywrightConfigurations PlayWriteConfigurations;

        public IBrowser? Browser;

        public IBrowserContext? Context;

        public IPage? Page;

        public PlaywrightDriver(LaunchConfiguration launchConfiguration, PlaywrightConfigurations playWriteConfigurations)
        {
            PlayWriteConfigurations = playWriteConfigurations ?? throw new ArgumentNullException(nameof(playWriteConfigurations));
            LaunchConfiguration = launchConfiguration ?? throw new ArgumentNullException(nameof(launchConfiguration));
        }

        public async Task DriverInitializeAsync()
        {
            Browser = await InitializeBrowserAsync();
            Context = await CreateBrowserContextAsync();
            Page = await CreatePageAsync();
        }

        public async Task<IBrowser> InitializeBrowserAsync()
        {
            return await PlayWriteConfigurations.GetBrowserAsync()
                                   ?? throw new InvalidOperationException("Failed to initialize browser.");
        }

        public async Task<IBrowserContext> CreateBrowserContextAsync()
        {
            return await (Browser?.NewContextAsync()
                                   ?? throw new InvalidOperationException("Browser is not initialized."));
        }

        public async Task<IPage> CreatePageAsync()
        {
            return await (Context?.NewPageAsync()
                               ?? throw new InvalidOperationException("Browser context is not initialized."));
        }
    }
}
