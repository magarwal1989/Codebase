using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpworkProject.pages;
using UpworkProject.utilities;

namespace UpworkProject.tests
{
    //extending DriverClass so that test can be run from driver run
    class OJT_ApprenticeshipTest : DriverClass
    {
        // initializing OJTApprenticeshipPage page
        OJT_ApprenticeshipPage ojt;

        // initializing report here
        Report r;

        //this will be exeucted only once before the actual test
        [OneTimeSetUp]
        public void Setup()
        {
            //calling setup method to invoke browser and launching application
            setup();
            ojt = new OJT_ApprenticeshipPage(getWebDriver());
            r = new Report();
        }

        //actual test for the application
        [Test]
        public void ValidateFirstName()
        {
            try
            {
                ojt.gotoModuleTwo();
                ojt.clickOnOJTModule();
                ojt.clickOnOnlineApp();
                ojt.clickOnCreateAnAccount();
                ojt.selectCheckbox();
                ojt.verifyFirstName();
                r.pass("Verify that all fields are present on Create An account page.", "Fiels are available.");
            }
            catch (Exception e)
            {
                LogInfo("Exception occured: " + e);

                //capturing screenshot if test is failed
                String path = captureScreenshot("homepage");

                //adding test case information and marking test case as fail
                r.fail("Verify that all fields are present on Create An account page.", "Fiels are available.", path);
            }
        }

        [Test]
        public void ValidateSubmitButton()
        {
            try
            {
                setup();
                ojt.gotoModuleTwo();
                ojt.clickOnOJTModule();
                ojt.clickOnOnlineApp();
                ojt.clickOnCreateAnAccount();
                ojt.selectCheckbox();
                ojt.verifySubmitBtn();
                r.pass("Verify that submit button is present on Create An account page.", "Button is available.");
            }
            catch (Exception e)
            {
                LogInfo("Exception occured: " + e);

                //capturing screenshot if test is failed
                String path = captureScreenshot("homepage");

                //adding test case information and marking test case as fail
                r.fail("Verify that submit button is present on Create An account page.", "Button is available.", path);
            }
        }

        //this will be executed at the end of all test but only once
        [OneTimeTearDown]
        public void tearDown()
        {
            //generating report here
            r.tearDown();
        }
    }
}
