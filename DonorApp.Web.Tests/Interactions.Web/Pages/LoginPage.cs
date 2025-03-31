using Core.Infrastructure.Configurations;
using Core.Web.PageObject;
using Core.Web.WebElement;
using Microsoft.Playwright;

namespace Interactions.Web.ReportPortalPages
{
    public class LoginPage : BasePageObject
    {
        private IPage _page { get; set; }

        private TextElement LoginInput => FindTextElementById("email");

        private TextElement PasswordInput => FindTextElementById("password");

        private TextElement RememberMeCheckbox => FindTextElementByXpath("//input[contains(@class,'jss5')]");

        private UiElement LoginButton => FindUiElementByXpath("//button[contains(@class,'MuiButton-containedPrimary')]"); 

        public LoginPage(IPage page) : base(page)
        {
            _page = page;
        }

        public async Task EnterLoginAsync(string login)
        {          
            await LoginInput.FillTextAsync(login);
        }

        public async Task CheckRemeberMe()
        {
            await RememberMeCheckbox.ClickAsync();
        }

        public async Task EnterPasswordAsync(string password)
        {            
            await PasswordInput.FillTextAsync(password);
        }

        public async Task LoginClickAsync()
        {
            await LoginButton.JsClickAsync();
        }

        public async Task LoginToDonorPortal(UserDataConfiguration userDataConfiguration)
        {
            await EnterLoginAsync(userDataConfiguration.LoginName);

            await EnterPasswordAsync(userDataConfiguration.Password);

            await CheckRemeberMe();

            await LoginClickAsync();
        }
    }
}
