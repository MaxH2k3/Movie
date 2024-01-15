using NuGet.Protocol;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Movies.Configuration
{
    public class SeleniumConfig
    {

        public IWebDriver driver;

        public SeleniumConfig()
        {
            // Tạo tùy chọn trình duyệt Chrome để kích hoạt chế độ headless
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--headless");

            // Khởi tạo trình duyệt Chrome với tùy chọn headless
            driver = new ChromeDriver();

            // Thiết lập thời gian chờ là 10 giây
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
        }

    }
}
