//Inside SeleniumTest.cs

using NUnit.Framework;

using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;

using OpenQA.Selenium.Interactions;
using System;

using System.Collections.ObjectModel;

using System.IO;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace TwitterAutomation

{

    public class TwitterAutomation

    {
        string nextPostStyle = string.Empty;

        IWebDriver driver;

        [OneTimeSetUp]

        public void Setup()

        {


            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;

            //Creates the ChomeDriver object, Executes tests on Google Chrome

            driver = new ChromeDriver(path + @"\drivers\");


        }

        [Test]

        public void verifyLogo()

        {

            driver.Navigate().GoToUrl("https://twitter.com/i/flow/login");
            driver.Manage().Window.Maximize();

        }

        [Test]

        public void verifyMenuItemcount()
        {
            Thread.Sleep(8000);
            IWebElement username = driver.FindElement(By.CssSelector("input[autocomplete=\"username\"]"));
            username.SendKeys("Boyapati1310");
            string resultPath = "C:\\Krishna\\St.Clair\\Sem 4\\Capstone\\Data";
            username.SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            IWebElement password = driver.FindElement(By.CssSelector("input[name=\"password\"]"));
            password.SendKeys("Cap@1310");
            password.SendKeys(Keys.Enter);
            Thread.Sleep(3000);
            IWebElement searchBox = driver.FindElement(By.CssSelector("input[placeholder=\"Search\"]"));
            Thread.Sleep(5000);
            driver.Navigate().GoToUrl("https://twitter.com/search?q=%23IPL2024&src=typeahead_click&f=live");
            Thread.Sleep(5000);
            TwitterAutomation twitterAutomation = new TwitterAutomation();
            List<TwitterDataInfo> twitterData = new List<TwitterDataInfo>();
            try
            {
                for (int i = 1; i <= 1200; i++)
                {
                    List<TwitterDataInfo> twitterPostData = new List<TwitterDataInfo>();
                    try
                    {
                        twitterPostData = twitterAutomation.LoadTheLatestPostLiveInfoPage(driver, i);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                    Thread.Sleep(2000);
                }
                string[] output = new string[twitterData.Count];
                int k = 0;
                foreach (var twitterDataInfo in twitterData)
                {
                    output[k] = twitterDataInfo.PostNumber + ", " + twitterDataInfo.PostedBy + ", " + twitterDataInfo.postedOn + ", " + twitterDataInfo.PickUpline + ", " + twitterDataInfo.MentionedInfo;
                    k++;
                }
                Thread.Sleep(20000);
                File.WriteAllLines($"{resultPath}\\outputlive.csv", output);
            }
            catch (Exception ex)
            {
                string[] output = new string[twitterData.Count];
                int k = 0;
                foreach (var twitterDataInfo in twitterData)
                {
                    output[k] = twitterDataInfo.PostNumber + ", " + twitterDataInfo.PostedBy + ", " + twitterDataInfo.postedOn + ", " + twitterDataInfo.PickUpline + ", " + twitterDataInfo.MentionedInfo;
                    k++;
                }
                Thread.Sleep(20000);
                File.WriteAllLines($"{resultPath}\\outputlive.csv", output);
            }
        }
        public void LoadIplTezLivePage()
        {
            driver.Navigate().GoToUrl("https://twitter.com/IPLT20_TezLive\r\n");
            //*[@id="id__u1t92hurq6p"]/div[1]/div/a/div/div[1]/span/span
            Thread.Sleep(5000);
            string val = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[1]/div[1]/div/div/div/div/div/div[2]/div/div")).Text;
            int postCount = Convert.ToInt32(val.Split(" ")[0].Trim().Replace(",", ""));
            string[] resultLines = new string[postCount];
            Actions actions = new Actions(driver);
            string[] output = new string[5000];
        }
        public void LoadLiveIpl2024DataInfo()
        {
            driver.Navigate().GoToUrl("https://twitter.com/search?q=%23IPL2024&src=typeahead_click&f=live");
            Thread.Sleep(5000);

        }

        public List<TwitterDataInfo> LoadTheLatestPostLiveInfoPage(IWebDriver driver, int postNumber)
        {

            //*[@id="id__u1t92hurq6p"]/div[1]/div/a/div/div[1]/span/span
            List<TwitterDataInfo> twitterDataInform = new List<TwitterDataInfo>();
            TwitterDataInfo twitterDataInfo = new TwitterDataInfo();
            Actions actions = new Actions(driver);
            for (int k = 1; k <= 13; k++)
            {
                twitterDataInfo.PostNumber = postNumber;
                
                var currentPost = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/section/div/div/div[" + k + "]"));
                string currentPostStyle = currentPost.GetAttribute("style");
                if (nextPostStyle == currentPostStyle || nextPostStyle == string.Empty)
                {
                    twitterDataInfo.CurrentPostStyle = currentPostStyle;
                    twitterDataInfo.PostedBy = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[1]/div/a/div/div[1]/span/span")).Text;
                    try
                    {
                        twitterDataInfo.postedOn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[2]/div/div[3]/a/time")).Text;
                    }
                    catch
                    {
                        twitterDataInfo.postedOn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[2]/div/div[3]/div/a/time")).Text;
                    }
                    try
                    {
                        twitterDataInfo.PickUpline = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[2]/div/span[1]")).Text;
                    }
                    catch (Exception ex)
                    {
                        twitterDataInfo.PickUpline = "N/A";
                    }
                    TwitterAutomation twitterAutomation = new TwitterAutomation();

                    int nextPostIndex = k + 1;
                    string nextPostType = string.Empty;
                    try
                    {
                        nextPostType = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + nextPostIndex + "]/div/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[2]/div/div[1]/span")).Text;
                    }
                    catch
                    {
                        nextPostType = string.Empty;
                    }

                    if (nextPostType == "Ad")
                    {
                        nextPostIndex = nextPostIndex + 1;
                    }
                    twitterDataInform = twitterAutomation.GetHashTagInfoLiveData(k, driver, twitterDataInfo);

                    var nextPost = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + nextPostIndex + "]"));
                    nextPostStyle = nextPost.GetAttribute("style");
                    actions.MoveToElement(nextPost);
                    actions.Perform();
                    Thread.Sleep(1000);
                    break;
                }
            }
            return twitterDataInform;
        }

        public List<TwitterDataInfo> GetHashTagInfoLiveData(int k, IWebDriver driver, TwitterDataInfo twitterDataInfo)
        {
            List<TwitterDataInfo> twitterDatas = new List<TwitterDataInfo>(); 
            string mentions = string.Empty;
            int data = 30;
            for (int i = 3; i <= data; i += 2)
            {
                try
                {
                    if (i >= 10)
                    {
                        data = data + 2;
                    }
                    string tagged = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div/div/div[3]/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[2]/div/span[" + i + "]")).Text;
                    if (string.IsNullOrWhiteSpace(tagged))
                    {
                        break;
                    }
                    else
                    {
                        if (tagged.StartsWith('#'))
                        {
                            TwitterDataInfo twitter = twitterDataInfo;
                            twitter.MentionedInfo = tagged;
                            twitterDatas.Add(twitter);
                        }
                        if (i <= 5)
                        {
                            i = i + 1;
                        }
                    }
                }
                catch
                {
                    break;
                }

            }
            return twitterDatas;
        }

        public TwitterDataInfo LoadTezLivepostDataInfo()
        {

            Actions actions = new Actions(driver);
            TwitterDataInfo twitterDataInfo = new TwitterDataInfo();

            try
            {
                for (int k = 3; k <= 13; k++)
                {
                    var currentPost = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]"));

                    string currentPostStyle = currentPost.GetAttribute("style");
                    if (nextPostStyle == currentPostStyle || nextPostStyle == string.Empty)
                    {
                        twitterDataInfo.CurrentPostStyle = currentPostStyle;
                        twitterDataInfo.PostedBy = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[1]/div/a/div/div[1]/span/span")).Text;
                        try
                        {
                            twitterDataInfo.postedOn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[2]/div/div[3]/a/time")).Text;
                        }
                        catch
                        {
                            twitterDataInfo.postedOn = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[1]/div/div[1]/div/div/div[2]/div/div[3]/div/a/time")).Text;
                        }
                        try
                        {
                            twitterDataInfo.PickUpline = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[2]/div/span[1]")).Text;
                        }
                        catch (Exception ex)
                        {
                            twitterDataInfo.PickUpline = "N/A";
                        }

                        //try
                        //{
                        //    twitterDataInfo.Comment = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + k + "]/div/div/article/div/div/div[2]/div[2]/div[2]/div/span[2]")).Text;
                        //}
                        //catch (Exception ex)
                        //{
                        //    twitterDataInfo.Comment = "N/A";
                        //}
                        int nextPostIndex = k + 1;
                        var nextPost = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[2]/main/div/div/div/div[1]/div/div[3]/div/div/section/div/div/div[" + nextPostIndex + "]"));
                        nextPostStyle = nextPost.GetAttribute("style");
                        actions.MoveToElement(nextPost);
                        actions.Perform();
                        Thread.Sleep(1000);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return twitterDataInfo;

        }


        [OneTimeTearDown]

        public void TearDown()

        {

            driver.Quit();

        }

    }

}