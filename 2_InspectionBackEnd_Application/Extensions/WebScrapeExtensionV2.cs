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
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;

namespace _2_InspectionBackEnd_Application.Extensions
{
    public class WebScrapeExtensionV2
    {

        public static List<AveragePricesBasedOnSearch> Tokopedia(List<string?> searchItems)
        {
            //1. Check if search item is empty or not
            if (searchItems == null || searchItems.Count == 0)
            {
                return new List<AveragePricesBasedOnSearch>();
            }
            //2. Initialize web driver
            IWebDriver driver = new IChromeDriver().InitializeChromeDriver();

            //3. Initialize the result
            List<AveragePricesBasedOnSearch> result = new List<AveragePricesBasedOnSearch>();

            //4. For each every search items
            foreach (var searchItem in searchItems)
            {
                var tokenizeSearchItem = searchItem.ToLower().Split(" ");
                var url = "https://www.tokopedia.com/search?sc=37&q=" + string.Join("-", tokenizeSearchItem);
                try
                {
                    //Navigate to the url
                    driver.Navigate().GoToUrl(url);

                    //Create Javascript excuter
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;

                    //if page is loaded continue with the grabbing data process;
                    if (WaitDriverToLoad(jse))
                    {
                        //Get latest height of the page
                        Int64 last_height = (Int64)((jse).ExecuteScript("return document.documentElement.scrollHeight"));

                        //Scroll until reaches the maximum height
                        while (true)
                        {
                            (jse).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");

                            // Wait for the page to load
                            if (WaitDriverToLoad(jse))
                            {
                                // Calculate new scroll height and compare with latest scroll height
                                Int64 new_height = (Int64)(jse).ExecuteScript("return document.documentElement.scrollHeight");
                                if (new_height == last_height)
                                    break;
                                last_height = new_height;
                            }
                            else
                            {
                                break;
                            }
                        }

                        //Get the data if page successfully loaded
                        IWebElement elements = driver.FindElement(By.TagName("body"));
                        var text = elements.GetAttribute("innerHTML");
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(text);
                        var listingPriceElement = htmlDocument.DocumentNode.SelectNodes(".//div[@data-testid='linkProductPrice' or @data-testid='spnSRPProdPrice']");

                        //Calculate Average Price
                        if (listingPriceElement != null)
                        {
                            var averagePrice = 0;
                            foreach (var item in listingPriceElement)
                            {
                                var splittedNumbers = Regex.Split(item.InnerHtml, @"\D+");
                                var price = int.Parse(string.Join(string.Empty, splittedNumbers));
                                averagePrice += price;
                            }
                            averagePrice /= listingPriceElement.Count;

                            //Rounding
                            var roundedAveragePrice = averagePrice % 1000 >= 500 ? averagePrice + 1000 - averagePrice % 1000 : averagePrice - averagePrice % 1000;
                            result.Add(new AveragePricesBasedOnSearch
                            {
                                SearchKeyword = searchItem,
                                Price = roundedAveragePrice,
                            });
                        }
                        else
                        {
                            //if there is no item after search then set the price to 0
                            result.Add(new AveragePricesBasedOnSearch
                            {
                                SearchKeyword = searchItem,
                                Price = 0
                            });
                        };
                    }
                    else
                    {
                        //if Failed to grab data, set average price to 0
                        result.Add(new AveragePricesBasedOnSearch
                        {
                            SearchKeyword = searchItem,
                            Price = 0
                        });
                    }
                }
                catch
                {
                    driver.Quit();
                    driver = new IChromeDriver().InitializeChromeDriver();
                    continue;
                }
            }

            driver.Quit();
            return result;
        }
        public static List<AveragePricesBasedOnSearch> Olx(List<string?> searchItems)
        {
            //1. Check if search item is empty or not
            if (searchItems == null || searchItems.Count == 0)
            {
                return new List<AveragePricesBasedOnSearch>();
            }
            //2. Initialize web driver
            IWebDriver driver = new IChromeDriver().InitializeChromeDriver();

            //3. Initialize the result
            List<AveragePricesBasedOnSearch> result = new List<AveragePricesBasedOnSearch>();

            //4. For each every search items
            foreach(var searchItem in searchItems)
            {
                var tokenizeSearchItem = searchItem.ToLower().Split(" ");
                var url = "https://www.olx.co.id/motor-bekas_c200/q-" + string.Join("-", tokenizeSearchItem);
                try
                {
                    //Navigate to the url
                    driver.Navigate().GoToUrl(url);
                    
                    //Create Javascript excuter
                    IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;                    

                    //if page is loaded continue with the grabbing data process;
                    if (WaitDriverToLoad(jse))
                    {
                        //Get latest height of the page
                        Int64 last_height = (Int64)((jse).ExecuteScript("return document.documentElement.scrollHeight"));
                        
                        //Scroll until reaches the maximum height
                        while (true)
                        {
                            (jse).ExecuteScript("window.scrollTo(0, document.documentElement.scrollHeight);");

                            // Wait for the page to load
                            if (WaitDriverToLoad(jse))
                            {
                                // Calculate new scroll height and compare with latest scroll height
                                Int64 new_height = (Int64)(jse).ExecuteScript("return document.documentElement.scrollHeight");
                                if (new_height == last_height)
                                    break;
                                last_height = new_height;
                            }
                            else
                            {
                                break;
                            }
                        }

                        //Get the data if page successfully loaded
                        IWebElement elements = driver.FindElement(By.TagName("body"));
                        var text = elements.GetAttribute("innerHTML");
                        HtmlAgilityPack.HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
                        htmlDocument.LoadHtml(text);
                        var listingPriceElement = htmlDocument.DocumentNode.SelectNodes(".//span[@data-aut-id='itemPrice']");

                        //Calculate Average Price
                        if (listingPriceElement != null)
                        {
                            var averagePrice = 0;
                            foreach (var item in listingPriceElement)
                            {
                                var splittedNumbers = Regex.Split(item.InnerHtml, @"\D+");
                                var price = int.Parse(string.Join(string.Empty, splittedNumbers));
                                averagePrice += price;
                            }
                            averagePrice /= listingPriceElement.Count;

                            //Rounding
                            var roundedAveragePrice = averagePrice % 1000 >= 500 ? averagePrice + 1000 - averagePrice % 1000 : averagePrice - averagePrice % 1000;
                            result.Add(new AveragePricesBasedOnSearch
                            {
                                SearchKeyword = searchItem,
                                Price = roundedAveragePrice,
                            });
                        }
                        else
                        {
                            //if there is no item after search then set the price to 0
                            result.Add(new AveragePricesBasedOnSearch
                            {
                                SearchKeyword = searchItem,
                                Price = 0
                            });
                        };
                    }
                    else
                    {
                        //if Failed to grab data, set average price to 0
                        result.Add(new AveragePricesBasedOnSearch
                        {
                            SearchKeyword = searchItem,
                            Price = 0
                        });
                    }
                }
                catch
                {
                    driver.Quit();
                    driver = new IChromeDriver().InitializeChromeDriver();
                    continue;
                    //throw new Validation_Exception("Failed to run chrome");
                }
            }

            driver.Quit();
            return result;
        }

        public static bool WaitDriverToLoad(IJavaScriptExecutor jse)
        {
            //Repeat 50 times to pause the thread if page is not loaded
            var isLoaded = false;
            for (int i = 0; i < 150; i++)
            {
                Thread.Sleep(1000);
                try
                {
                    //again check page state
                    if (jse.ExecuteScript("return document.readyState").ToString().Equals("complete"))
                    {
                        return true;
                    }
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public class AveragePricesBasedOnSearch
        {
            public string? SearchKeyword { get; set; }
            public int? Price { get; set; }
        }
    }
}
