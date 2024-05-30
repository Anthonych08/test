using _2_InspectionBackEnd_Application.Exception;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;
using Microsoft.Extensions.Options;
using _2_InspectionBackEnd_Application.Interfaces;
using HtmlAgilityPack;
using static System.Net.Mime.MediaTypeNames;

namespace _2_InspectionBackEnd_Application.Extensions
{
    public class WebScrapeExtension
    {

        public static List<string> Tokopedia(string searchItem)
        {
            //var tokenizeSearchItem = searchItem.ToLower().Split(" ");
            //var url = string.Join("%20", tokenizeSearchItem);
            //var driverPath = Path.Combine(AppContext.BaseDirectory, "Driver");
            //List<string> result = new List<string>();
            //var test = "test";
            //IWebDriver driver = new ChromeDriver(driverPath);
            var tokenizeSearchItem = searchItem.ToLower().Split(" ");
            var url = string.Join("-", tokenizeSearchItem);
            var driverPath = Path.Combine(AppContext.BaseDirectory, "Driver");
            List<string> result = new List<string>();
            IWebDriver driver = new IChromeDriver().InitializeChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.tokopedia.com/search?sc=37&q=" + url);
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                for (int i = 0; i < 50; i++)
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Validation_Exception ex)
                    {
                        throw new Validation_Exception("Unable to get data from Tokopedia");
                    }
                    //again check page state
                    if (jse.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                    {
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        IWebElement elements = driver.FindElement(By.TagName("body"));
                        var text = elements.GetAttribute("innerHTML");
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(text);
                        var listingPriceElement = htmlDocument.DocumentNode.SelectNodes(".//div[@data-testid='linkProductPrice' or @data-testid='spnSRPProdPrice']");
                        foreach (var item in listingPriceElement)
                        {
                            result.Add(item.InnerHtml);
                        }
                        break;
                    }
                }
                driver.Quit();
            }
            catch
            {
                driver.Quit();
                throw;
            }
            return result;
        }
        public static List<string> Olx(string searchItem)
        {
            var tokenizeSearchItem = searchItem.ToLower().Split(" ");
            var url = string.Join("-", tokenizeSearchItem);
            var driverPath = Path.Combine(AppContext.BaseDirectory, "Driver");
            List<string> result = new List<string>();
            //new DriverManager().SetUpDriver(new ChromeConfig());
            //var chromeOptions = new ChromeOptions();
            //ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            //chromeOptions.AddArguments("start-maximized",
            //    "--user-agent=Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36",
            //    "--headless", "--disable-gpu", "--no-sandbox", "--disable-dev-shm-usage");
            IWebDriver driver = new IChromeDriver().InitializeChromeDriver();
            try
            {
                driver.Navigate().GoToUrl("https://www.olx.co.id/motor-bekas_c200/q-" + url);
                //((ITakesScreenshot)driver).GetScreenshot().SaveAsFile("testing.png");
                IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
                for (int i = 0; i < 50; i++)
                {
                    try
                    {
                        Thread.Sleep(1000);
                    }
                    catch (Validation_Exception ex)
                    {
                        throw new Validation_Exception("Unable to get data from OLX");
                    }
                    //again check page state
                    if (jse.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                    {
                        jse.ExecuteScript("window.scrollTo(0, document.body.scrollHeight);");
                        
                        IWebElement elements = driver.FindElement(By.TagName("body"));
                        var text = elements.GetAttribute("innerHTML");
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(text);
                        var listingPriceElement = htmlDocument.DocumentNode.SelectNodes(".//span[@data-aut-id='itemPrice']");
                        if (listingPriceElement != null)
                        {
                            foreach (var item in listingPriceElement)
                            {
                                result.Add(item.InnerHtml);
                            }
                        }
                        else
                        {
                            result.Add("0");
                            Console.WriteLine("Failed to Get Data for " + searchItem);
                            return result;
                        };

                        break;
                    }
                }
                driver.Quit();
            }
            catch
            {
                driver.Quit();
                throw new Validation_Exception("Failed to run chrome");
            }
            return result;
        }
    }
}
