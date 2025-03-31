using Core.Infrastructure.Configurations;
using Core.ServiceLocator;
using Core.Web.WebDriver;
using NUnit.Framework;

namespace Core.Web
{
    public class BaseWebTestFixture
    {
        [ThreadStatic]
        public static PlaywrightDriver Driver;

        public LaunchConfiguration LaunchConfiguration { get; set; }
        public PlaywrightConfigurations PlaywrightConfigurations { get; set; }

        public BaseWebTestFixture()
        {
            LaunchConfiguration = AppContainer.Resolve<LaunchConfiguration>();
            PlaywrightConfigurations = AppContainer.Resolve<PlaywrightConfigurations>();
        }

        [SetUp]
        public async Task SetUp()
        {
            Driver = new PlaywrightDriver(LaunchConfiguration, PlaywrightConfigurations);
            await Driver.DriverInitializeAsync();
        }

        [TearDown]
        public async Task TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;

            if (outcome == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                var screenshotFilePath = Path.Combine(Path.GetTempPath(), $"{TestContext.CurrentContext.Test.Name}_screenshot.png");

                await Driver.Page.ScreenshotAsync(new Microsoft.Playwright.PageScreenshotOptions { Path = screenshotFilePath });
            }

            await Driver.Context.CloseAsync();

            if (Driver?.Browser != null)
                await Driver.Browser.CloseAsync();
        }
    }
}
