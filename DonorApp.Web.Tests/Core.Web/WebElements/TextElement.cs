using Microsoft.Playwright;

namespace Core.Web.WebElement
{
    public class TextElement : UiElement
    {
        private ILocator Locator { get; set; }

        public TextElement(ILocator locator) : base(locator)
        {
            Locator = locator;
        }

        public async Task FillTextAsync(string text)
        {
            await Locator.ClearAsync();

            await Locator.FillAsync(text);
        }

        public async Task JsFillTextAsync(string text)
        {
            await Locator.EvaluateAsync("(element, value) => element.value = value", text);
        }
    }
}
