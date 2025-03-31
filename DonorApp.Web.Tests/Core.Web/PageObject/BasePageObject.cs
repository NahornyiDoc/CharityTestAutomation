using Core.Web.WebElement;
using Microsoft.Playwright;

namespace Core.Web.PageObject
{
    public class BasePageObject
    {
        protected IPage Page { get; }

        public BasePageObject(IPage page)
        {
            Page = page;
        }

        public TextElement FindTextElementByXpath(string xpath)
        {
            return new TextElement(Page.Locator(xpath));
        }

        public UiElement FindUiElementByXpath(string xpath)
        {
            return new UiElement(Page.Locator(xpath).First);
        }

        public TextElement FindTextElementById(string id)
        {
            return new TextElement(Page.Locator($"#{id}"));
        }

        public List<UiElement> FindUiElmentsByXPath(string xpath)
        {
            var locators = Page.Locator(xpath).AllAsync().GetAwaiter().GetResult();

            var locatorsList = locators.ToList().Select(e => new UiElement(e)).ToList();

            return locatorsList;
        }
    }
}
