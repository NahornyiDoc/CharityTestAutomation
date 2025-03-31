using Core.Infrastructure.Configurations;
using Core.ServiceLocator;
using Core.Web;
using Interactions.Web.ReportPortalPages;
using Interactions.Web.SiteNavigations;

namespace Web.Test.ReportPortal.Tests.Smoke
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LoginTestFixture : BaseWebTestFixture
    {
        private readonly UserDataConfiguration _userDataConfiguration;

        public LoginTestFixture()
        {
            _userDataConfiguration = AppContainer.Resolve<UserDataConfiguration>();
        }

        [Test]
        public async Task LoginTest()
        {
            var page = Driver.Page;

            var navigation = new Navigation(page);

            await navigation.OpenDonorPortal();

            var loginPage = new LoginPage(page);

            await loginPage.LoginToDonorPortal(_userDataConfiguration);
        }
    }
}