using Core.Infrastructure.Configurations;
using Core.ServiceLocator;
using Microsoft.Playwright;


namespace Interactions.Web.SiteNavigations
{
    public class Navigation
    {
        private static EnvironmentConfiguration? _environmentConfiguration;

        private readonly IPage _page;

        public Navigation(IPage page)
        {
            _page = page;

            _environmentConfiguration = AppContainer.Resolve<EnvironmentConfiguration>();
        }
        public async Task OpenDonorPortal()
        {
            await _page.GotoAsync(_environmentConfiguration.DonorPortalUrl, new PageGotoOptions
            {
                Timeout = 60000,
                WaitUntil = WaitUntilState.Load
            });
        }
    }
}
