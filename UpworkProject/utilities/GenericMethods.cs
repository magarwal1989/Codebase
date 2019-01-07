using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Configuration;

namespace UpworkProject.utilities
{
    /*
     *extending the driver class so that driver instance can be used to use
     * selenium method.
     */
    class GenericMethods : DriverClass
    {
        public IWebDriver driver;
        public GenericMethods(IWebDriver driver)
        {
            this.driver = driver;
        }
        public IWebDriver getWebDriver()
        {
            return driver;
        }

        //Handle locator type
        public By ByLocator(String locator)
        {
            By result = null;

            if (locator.StartsWith("//"))
            { result = By.XPath(locator); }
            else if (locator.StartsWith("css="))
            { result = By.CssSelector(locator.Replace("css=", "")); }
            else if (locator.StartsWith("name="))
            {
                result = By.Name(locator.Replace("name=", ""));
            }
            else if (locator.StartsWith("link="))
            { result = By.LinkText(locator.Replace("link=", "")); }
            else
            { result = By.Id(locator); }
            return result;
        }

        //Assert element present
        public Boolean isElementPresent(IWebElement ele)
        {
            Boolean result = false;
            try
            {
                if (ele != null)
                result = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            return result;
        }

        //this method will wait for the element visibility
        public void WaitForElementVisible(IWebElement ele, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(ele))
                {
                    if (ele.Displayed)
                    {
                        break;
                    }
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }

            }
        }

        //this method will wait for element's invisibility
        public void WaitForElementNotVisible(IWebElement ele, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(ele))
                {
                    if (!ele.Displayed)
                    {
                        break;
                    }
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    Console.Write(ex);
                }

            }
        }

        //this method will be used to drag and drop
        public void dragAndDrop(IWebElement source, IWebElement target)
        {
            Actions act = new Actions(getWebDriver());
            act.ClickAndHold(source).MoveToElement(target).Release(target).Build().Perform();
        }

        //this method will be used to wait for element present
        public void WaitForElementPresent(IWebElement ele, int timeout)
        {

            for (int i = 0; i < timeout; i++)
            {
                if (isElementPresent(ele))
                {
                    break;
                }
                try
                {
                    Thread.Sleep(1000);
                }
                catch(Exception ex)
                {
                    Console.Write(ex);
                }
            }
        }

        //this method will do mouser hover on given webelement
        public void mouseOver(IWebElement ele)
        {
            this.WaitForElementPresent(ele, 20);
            
            //Build and Perform the mouseOver with Advanced User Interactions API
            Actions Builder = new Actions(getWebDriver());
            Builder.MoveToElement(ele).Build().Perform();
            Thread.Sleep(2000);
        }

        //this method will be used to click on an element
        public void clickOn(IWebElement ele)
        {
            //this.WaitForElementPresent(ele, 20);
            //Assert.True(isElementPresent(ele));
            ele.Click();
            Thread.Sleep(2000);
        }

        //this method will be used to double click on an element
        public void doubleClick(IWebElement ele)
        {
            this.WaitForElementPresent(ele, 20);
            Assert.True(isElementPresent(ele));
            Actions action = new Actions(driver);
            action.DoubleClick(ele).Perform();
        }

        //this method will be used to perform right click
        public void rightClick(IWebElement ele)
        {
            Actions action = new Actions(driver);
            action.ContextClick(ele).Build().Perform();
        }

        //this method will be used to enter text in input field
        public void sendKeys(IWebElement ele, String value)
        {
            this.WaitForElementPresent(ele, 20);
            Assert.True(isElementPresent(ele));
            ele.Clear();
            ele.SendKeys(value);
        }

        //this method will be used to select a frame in dom
        public void selectFrame(IWebElement ele)
        {
            this.WaitForElementPresent(ele, 20);
            Assert.True(isElementPresent(ele));
            getWebDriver().SwitchTo().Frame(ele);
        }

        //this method will be used to verify element is present or not
        public Boolean isTextPresent(String locator, String str)
        {
            String message = getWebDriver().FindElement(ByLocator(locator)).Text;
            if (message.Contains(str)) { return true; }
            else { return false; }
        }

        //this method will be used to scroll down to the given element
        public void scrollIntoView(String locator)
        {

            IWebElement elem = getWebDriver().FindElement(ByLocator(locator));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", new Object[] { elem });
        }

        
    }
}
