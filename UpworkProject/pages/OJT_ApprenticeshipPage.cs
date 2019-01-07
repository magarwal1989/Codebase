using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpworkProject.utilities;

namespace UpworkProject.pages
{
    //extending the GenericMethods so that generic methods can be used here
    class OJT_ApprenticeshipPage : GenericMethods
    {
        public OJT_ApprenticeshipPage(IWebDriver driver) : base(driver)
        {
            //initiating page factory here
            PageFactory.InitElements(getWebDriver(), this);
        }

        //Locators for the OJT_Apprenticeship page

        [FindsBy(How = How.XPath, Using = "//ul[@id='top_nav_menu']//a[contains(text(), 'Benefits & Resources')]")]
        private IWebElement BenefitsAndResources { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Education')]")]
        private IWebElement Education { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'Minnesota GI Bill')]")]
        private IWebElement MinnesotaGIBill { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'OJT/Apprenticeship')]")]
        private IWebElement OJTModule { get; set; }

        [FindsBy(How = How.XPath, Using = "//a[contains(text(), 'online application')]")]
        private IWebElement OnlineApplication { get; set; }

        [FindsBy(How = How.XPath, Using = "//div/button[2]")]
        private IWebElement CreateAnApplicationBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[@class='mat-checkbox-inner-container']")]
        private IWebElement CheckBox { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[contains(@class, 'mat-input-infix')]")]
        private IWebElement Fields { get; set; }

        [FindsBy(How = How.XPath, Using = "//button[contains(@class, 'mat-raised-button')]")]
        private IWebElement SubmitBtn { get; set; }

        //code to land on Minnesota Gl Billpage
        public void gotoModuleTwo()
        {
            mouseOver(BenefitsAndResources);
            mouseOver(Education);
            clickOn(MinnesotaGIBill);
            LogInfo("Landed on Minnesota GI Bill page.");
        }

        //code to click on OJT module
        public void clickOnOJTModule()
        {
            clickOn(OJTModule);
            LogInfo("Successfully clicked on OJT Module.");
        }

        //code to click on online application link
        public void clickOnOnlineApp()
        {
            //storing window id for the current window
            String win1 = driver.CurrentWindowHandle;
            clickOn(OnlineApplication);
            LogInfo("Clicked on Online Application link.");

            //storing all window id in list
            List<string> lstWindow = driver.WindowHandles.ToList();
            foreach (String s in lstWindow)
            {
                //switching to earch window and then verifying with page title
                driver.SwitchTo().Window(s);
                if (driver.Title.Equals("Home Page - VATSExternal"))
                {
                    //if title mathces with the target page then breaking loop
                    break;
                }
            }
            LogInfo("Switched to window: "+ driver.Title);
            //Thread.Sleep(15000);
        }

        //code to click on Create An Account button
        public void clickOnCreateAnAccount()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//div/button[2]")));
            clickOn(CreateAnApplicationBtn);
            LogInfo("Clicked on create app application button.");
        }

        //code to select checkbox
        public void selectCheckbox()
        {
            clickOn(CheckBox);
            LogInfo("checkbox selected.");
        }

        //verifying first name here
        public void verifyFirstName()
        {
            //asserting first name
            Assert.True(isElementPresent(Fields));
            LogInfo("First name is validated and found true.");
        }

        //verifying first name here
        public void verifySubmitBtn()
        {
            //asserting first name
            Assert.True(isElementPresent(SubmitBtn));
            LogInfo("Submit button is validated and found true.");
        }
    }
}
