using RelevantCodes.ExtentReports;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpworkProject.utilities
{
    class Report
    {
        private static ExtentReports extent;
        private static ExtentTest test;
        static String path = ConfigurationManager.AppSettings["PROJECTPATH"];

        //report path where report will be generated
        static String reportPath = path+ "test-output\\report.html";

        public Report()
        {
            extent = new ExtentReports(reportPath, true);
            extent.AddSystemInfo("Host Name", ConfigurationManager.AppSettings["HOSTNAME"])

               .AddSystemInfo("Environment", ConfigurationManager.AppSettings["ENVIRONMENT"])

               .AddSystemInfo("Username", ConfigurationManager.AppSettings["USERNAME"]);

            extent.LoadConfig(path + "Extent-config.xml");
            
        }

        //this method will mark test case as pass
        public void pass(String testcasename, String testcasedesc)
        {
            test = extent.StartTest(testcasename);
            test.Log(LogStatus.Pass, testcasedesc);
            extent.EndTest(test);
        }

        //this method will mark test case as fail
        public void fail(String testcasedesc, String msg, String screenshotPath)
        {
            
            test = extent.StartTest(testcasedesc);
            test.Log(LogStatus.Fail, msg+test.AddScreenCapture(screenshotPath));
            extent.EndTest(test);
        }

        //this method will create the report at the end and close reporter
        public void tearDown()
        {
            extent.Flush();
            extent.Close();
        }
    }
}
