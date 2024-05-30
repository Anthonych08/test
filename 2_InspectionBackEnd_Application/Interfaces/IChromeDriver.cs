using _1_InspectionBackEnd_Domain.Master;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace _2_InspectionBackEnd_Application.Interfaces
{
    public class IChromeDriver
    {
        public IWebDriver driver { get; private set; }

        public IWebDriver InitializeChromeDriver()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var chromeOptions = new ChromeOptions();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.Port = 11000;
            chromeOptions.AddArguments("start-maximized", "--disable-geolocation", "--disable-features=SameSiteByDefaultCookies,CookiesWithoutSameSiteMustBeSecure", "--whitelisted-ips=",
                "--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/125.0.0.0 Safari/537.36",
                "--headless", "--ignore-certificate-errors", "--disable-gpu", "--no-sandbox", "--disable-dev-shm-usage", "disable-blink-features", "disable-blink-features=AutomationControlled",
                "--disable-3d-apis", "--allow-running-insecure-content");
            this.driver = new ChromeDriver(service, chromeOptions);
            return this.driver;
        }
    }
}
