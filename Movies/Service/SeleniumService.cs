using MongoDB.Driver.Linq;
using Movies.Configuration;
using Movies.Utilities;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace Movies.Service;

public class SeleniumService
{
    private readonly SeleniumConfig _seleniumConfig;

    public SeleniumService(SeleniumConfig seleniumConfig)
    {
        _seleniumConfig = seleniumConfig;
    }

    public bool Login(IEnumerable<Movies.Business.anothers.Cookie> cookies)
    {
        _seleniumConfig.driver.Navigate().GoToUrl("https://www.netflix.com/vn-en/");
        Thread.Sleep(1000);

        foreach (var cookieData in cookies)
        {
            Cookie cookie = new Cookie(cookieData.Name, cookieData.Value, cookieData.Domain, cookieData.Path, Utiles.ConvertToDateTime(cookieData.ExpirationDate));

            _seleniumConfig.driver.Manage().Cookies.AddCookie(cookie);
            Console.WriteLine("Processing");
        }

        _seleniumConfig.driver.Navigate().Refresh();

        if (_seleniumConfig.driver.FindElements(By.Id("signIn")).Count != 0)
        {
            Console.WriteLine("Failure!");
            return false;
        } else if (_seleniumConfig.driver.FindElements(By.ClassName("profile")).Count != 0)
        {
            //enter to profile
            _seleniumConfig.driver.FindElement(By.XPath("//div[@id='appMountPoint']//li[2]")).Click();

            Console.WriteLine("Successfully!");
            //_seleniumConfig.driver.Quit();
        }

        return true;

    }

    public void GetData()
    {
        //scroll to element

        //use action to hover on element and dislay
        var elementToHover = _seleniumConfig.driver.FindElement(By.Id("row-3"));
        Actions action = new Actions(_seleniumConfig.driver);
        action.ScrollToElement(elementToHover).Perform();
        Thread.Sleep(1500);
        action.MoveToElement(elementToHover).Perform();
        Thread.Sleep(2000);
        //get button slide
        _seleniumConfig.driver.FindElement(By.XPath("//div[@id='row-3']//b[@class='indicator-icon icon-rightCaret']")).Click();
        Thread.Sleep(2000);
        //get movie card
        var card = _seleniumConfig.driver.FindElement(By.Id("title-card-3-4"));

        card.Click();
        var element = card.FindElement(By.ClassName("boxart-image-in-padded-container"));
        var image = element.GetAttribute("src");
        Console.WriteLine(image);
    }

}
