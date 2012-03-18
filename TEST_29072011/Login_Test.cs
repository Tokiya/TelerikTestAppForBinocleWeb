using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;

using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest1
    /// </summary>
    [TestClass]
    public class Login_Test : BaseTest
    {

        #region [Setup / TearDown]

        private TestContext testContextInstance = null;
        /// <summary>
        ///Gets or sets the VS test context which provides
        ///information about and functionality for the
        ///current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }


        //Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }


        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            #region WebAii Initialization

            // Initializes WebAii manager to be used by the test case.
            // If a WebAii configuration section exists, settings will be
            // loaded from it. Otherwise, will create a default settings
            // object with system defaults.
            //
            // Note: We are passing in a delegate to the VisualStudio
            // testContext.WriteLine() method in addition to the Visual Studio
            // TestLogs directory as our log location. This way any logging
            // done from WebAii (i.e. Manager.Log.WriteLine()) is
            // automatically logged to the VisualStudio test log and
            // the WebAii log file is placed in the same location as VS logs.
            //
            // If you do not care about unifying the log, then you can simply
            // initialize the test by calling Initialize() with no parameters;
            // that will cause the log location to be picked up from the config
            // file if it exists or will use the default system settings (C:\WebAiiLog\)
            // You can also use Initialize(LogLocation) to set a specific log
            // location for this test.

            // Pass in 'true' to recycle the browser between test methods
            Initialize(false, this.TestContext.TestLogsDir, new TestContextWriteLine(this.TestContext.WriteLine));

            // If you need to override any other settings coming from the
            // config section you can comment the 'Initialize' line above and instead
            // use the following:

            /*

            // This will get a new Settings object. If a configuration
            // section exists, then settings from that section will be
            // loaded

            Settings settings = GetSettings();

            // Override the settings you want. For example:
            settings.DefaultBrowser = BrowserType.FireFox;

            // Now call Initialize again with your updated settings object
            Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

            */

            // Set the current test method. This is needed for WebAii to discover
            // its custom TestAttributes set on methods and classes.
            // This method should always exist in [TestInitialize()] method.
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            #endregion

            //
            // Place any additional initialization here
            //

        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {

            //
            // Place any additional cleanup here
            //

            #region WebAii CleanUp

            // Shuts down WebAii manager and closes all browsers currently running
            // after each test. This call is ignored if recycleBrowser is set
            this.CleanUp();

            #endregion
        }

        //Use ClassCleanup to run code after all tests in a class have run
        [ClassCleanup()]
        public static void MyClassCleanup()
        {
            // This will shut down all browsers if
            // recycleBrowser is turned on. Else
            // will do nothing.
            ShutDown();
        }

        #endregion

        public void LogIn(int browser, string url, string user_name, string password)
        {
            #region Logowanie
            switch (browser)
            {
                case 1:
                    Manager.LaunchNewBrowser(BrowserType.InternetExplorer);
                    break;
                case 2:
                    Manager.LaunchNewBrowser(BrowserType.Chrome);
                    break;

                case 3:
                    Manager.LaunchNewBrowser(BrowserType.FireFox);
                    break;
                case 4:
                    Manager.LaunchNewBrowser(BrowserType.Safari);
                    break;
                default:
                    Manager.LaunchNewBrowser(BrowserType.InternetExplorer);
                    break;

            }

            ActiveBrowser.NavigateTo(url);
            Find.ByXPath<HtmlInputText>("//*[@id=\"Login1_UserName\"]").Text = user_name;
            Find.ByName<HtmlInputPassword>("Login1$Password").Text = password;
            Find.ByName<HtmlInputSubmit>("Login1$LoginButton").Click();
            #endregion
        }
        public void LogOut()
        {
            HtmlAnchor wyloguj = Find.ById<HtmlAnchor>("LoginStatus1");
            wyloguj.MouseClick();
            ActiveBrowser.Close();
        }
        [TestMethod]
        public void LogIN_without_username()
        {
            LogIn(1, "http://localhost/BinocleWeb/", "", "");

            HtmlSpan span = Find.ById<HtmlSpan>("Login1_UserNameRequired");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.InnerText.Equals("*"));
            span = Find.ById<HtmlSpan>("Login1_PasswordRequired");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.InnerText.Equals("*"));

        }

        [TestMethod]
        public void LogIN_with_wrong_username()
        {
            LogIn(7, "http://localhost/BinocleWeb/", "FSDFSCSASDfd", "asdrsc");
            Assert.IsTrue(ActiveBrowser.ContainsText("Your login attempt was not successful."));
        
        }

        [TestMethod]
        public void LogIN_with_wrong_password()
        {
            LogIn(7, "http://localhost/BinocleWeb/", "test", "asdrsc");
            Assert.IsTrue(ActiveBrowser.ContainsText("Your login attempt was not successful."));

        }

        [TestMethod]
        public void LogIN_succesfull()
        {
            LogIn(7, "http://localhost/BinocleWeb/", "test", "1qaz2WSX");
            Assert.IsFalse(ActiveBrowser.ContainsText("Your login attempt was not successful."));
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_PANE_CONTENT_Top"),5000);
            ActiveBrowser.WaitForElement(5000, "id=RAD_SPLITTER_PANE_CONTENT_Top");
            HtmlDiv top = Find.ById<HtmlDiv>("RAD_SPLITTER_PANE_CONTENT_Top");
            Assert.IsNotNull(top);
            LogOut();

        }
    }
}
