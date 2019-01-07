using OpenQA.Selenium;
using System;
using System.Configuration;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using NLog;
using System.IO;
using OpenQA.Selenium.Remote;

namespace UpworkProject.utilities
{
    class DriverClass
    {
        //initializing webdriver instance here
        public IWebDriver driver;

        

        //initializing logger class here for the current class
        private Logger log = LogManager.GetCurrentClassLogger();

        //getting this url from configuration file
        String url = ConfigurationManager.AppSettings["URL"];

        //getting this browser from configuration file
        String browser = ConfigurationManager.AppSettings["BROWSER"];

        //getting the project path from configuration file
        String path = ConfigurationManager.AppSettings["PROJECTPATH"];

        //this method will be executed at the starting of test
        //and will launch browser then will be hitting url
        public void setup()
        {
            if(driver == null) {
                
                //as per choice of browser in configuration file, code will be executed
                if (browser.Equals("CHROME", StringComparison.InvariantCultureIgnoreCase))
                {
                    
                    this.driver = new ChromeDriver(@path + "driver");
                    LogInfo("Chrome browser invoked successfully.");
                }
                else if (browser.Equals("FIREFOX", StringComparison.InvariantCultureIgnoreCase))
                {
                    this.driver = new FirefoxDriver(@path + "driver");
                    LogInfo("Firefox browser invoked successfully.");
                }
                else if (browser.Equals("IE", StringComparison.InvariantCultureIgnoreCase))
                {
                    this.driver = new InternetExplorerDriver(@path + "driver");
                    LogInfo("IE browser invoked successfully.");
                }
                else
                {
                    this.driver = new ChromeDriver(@path + "driver");
                }

                //maximizing the window
                driver.Manage().Window.Maximize();
                LogInfo("Window maximized successfully successfully.");
            }
            

            //deleting all the cooking of invoked browser
            driver.Manage().Cookies.DeleteAllCookies();

            //hitting the url
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["URL"]);
            LogInfo("Navigated to url: " + url + "successfully.");
        }

        //returning the driver instance
        public IWebDriver getWebDriver()
        {
            return driver;
        }

        //use this method to log information
        public void LogInfo(String str)
        {
            log.Info(str);
        }

        //user this method to log error
        public void LogError(String str)
        {
            log.Error(str);
        }

        //Capturing screenshot on failure 
        public String captureScreenshot(String fileName)
        {
            Screenshot ss = ((ITakesScreenshot)driver).GetScreenshot();
            string Runname = fileName + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss");
            string drive = ConfigurationManager.AppSettings["PROJECTPATH"] + "\\screenshots\\";
            if (System.IO.Directory.Exists(drive))
            {
                ss.SaveAsFile(@ConfigurationManager.AppSettings["PROJECTPATH"] + "\\screenshots\\" + Runname + ".jpeg", ScreenshotImageFormat.Jpeg);
            }
            
            return ConfigurationManager.AppSettings["PROJECTPATH"] + "\\screenshots\\" + Runname+".jpeg";
        }

    }
}
