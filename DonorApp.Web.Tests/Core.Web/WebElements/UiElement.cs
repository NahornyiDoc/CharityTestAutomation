using Microsoft.Playwright;

namespace Core.Web.WebElement
{
    public class UiElement
    {
        protected ILocator Locator { get; }

        public UiElement(ILocator locator)
        {
            Locator = locator;
        }

        public async Task JsClickAsync()
        {
            await Locator.EvaluateAsync("element => element.click()");
        }

        public async Task WaitForVisibility()
        {
            await Locator.WaitForAsync(new LocatorWaitForOptions { State = WaitForSelectorState.Visible });
        }

        public async Task<string> GetElementText()
        {
            return await Locator.InnerTextAsync();
        }

        public async Task<bool> IsVisibleAsync()
        {
            return await Locator.IsVisibleAsync();
        }

        public async Task ClickAsync()
        {
            await Locator.ClickAsync();
        }
    }
}
