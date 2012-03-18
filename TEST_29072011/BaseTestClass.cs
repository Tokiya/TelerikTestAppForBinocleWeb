using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;

using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using Telerik.WebAii.Controls.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using BITestAdamLib;
namespace TEST_29072011
{
    /// <summary>
    /// Summary description for BaseTestClass
    /// </summary>
    [TestClass]
    [FindParam("login", FindType.XPath, "//*[@id=\"Login1_UserName\"]")]
    [FindParam("password", FindType.XPath, "//*[@id=\"Login1_Password\"]")]
    [FindParam("logout", FindType.XPath, "//*[@id=\"LoginStatus1\"]")]
    [FindParam("login_btn", FindType.XPath, "//*[@id=\"Login1_LoginButton\"]")]
    [FindParam("login_lang",FindType.XPath,"//*[@id=\"Login1_Language_Arrow\"]")]
    public class BaseTestClass : BaseTest
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
            try
            {
                ProcessHandler.KillAllBrowsers();
            }
            catch { }
            System.Threading.Thread.Sleep(5000);
           Settings settings = GetSettings();
           settings.Web.KillBrowserProcessOnClose = true;
           settings.Web.RecycleBrowser = false;
           settings.Web.EnableScriptLogging = true;
            settings.LogLocation = TestContext.TestLogsDir;
            settings.AnnotateExecution = true;
            settings.AnnotationMode = AnnotationMode.All;
             Initialize(settings, new TestContextWriteLine(this.TestContext.WriteLine));

             try
             {
                 string[] browser = File.ReadAllLines(@"C:\testy\projekty Testowe\TEST_29072011\browser.txt");
                 switch (browser[0])
                 {
                     case "CH":
                         {
                             settings.Web.DefaultBrowser = BrowserType.Chrome;
                             break;
                         }
                     case "IE":
                         {
                             settings.Web.DefaultBrowser = BrowserType.InternetExplorer;
                             break;
                         }
                     case "FF":
                         {
                             settings.Web.DefaultBrowser = BrowserType.FireFox;
                             break;
                         }
                     default: goto case "IE";
                 }
             }
             catch (FileNotFoundException)
             {
                 settings.Web.DefaultBrowser = BrowserType.InternetExplorer;
             }
            SetTestMethod(this, (string)TestContext.Properties["TestName"]);

            
            Manager.Settings.UnexpectedDialogAction = UnexpectedDialogAction.DoNotHandle;

           
            Manager.LaunchNewBrowser();

            //ActiveBrowser.ClearCache(BrowserCacheType.Cookies);
           // ActiveBrowser.ClearCache(BrowserCacheType.TempFilesCache);
          //  ActiveBrowser.ClearCache(BrowserCacheType.History);
            if (!ActiveBrowser.Window.IsMaximized)
                ActiveBrowser.Window.Maximize();

            ActiveBrowser.NavigateTo("http://localhost/BinocleWeb/");
            ActiveBrowser.WaitUntilReady();
            ActiveBrowser.WaitForElement(15000, "id=Login1_LoginButton");
                Actions.Click(this.Elements["login_lang"]);
                Find.ByContent<HtmlListItem>("Polski").Click();
                Actions.SetText(this.Elements["login"], "test");
                Actions.SetText(this.Elements["password"], "1qaz2WSX");
                Actions.Click(this.Elements["login_btn"]);

                Assert.IsFalse(ActiveBrowser.ContainsText("Your login attempt was not successful."), "Login Failed");
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb crashed after login");


        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
           try   
           {
               Actions.Click(this.Elements["logout"]);
               ActiveBrowser.ClearCache(BrowserCacheType.Cookies);
               ActiveBrowser.ClearCache(BrowserCacheType.TempFilesCache);
               ActiveBrowser.ClearCache(BrowserCacheType.History);
                     }
            catch { }

            this.CleanUp();
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
        protected void OpenObject(string object_name)
        {
            try
            {
                //ActiveBrowser.Actions.WaitForElement(new FindParam("id=NavPaneAjaxc_tvBusinessObjects"), 5000);
               ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            
            catch 
            {
                ActiveBrowser.WaitForElement(50000,"id=RadSplitBar1");
                RadSplitBar splitbar= Find.ById<RadSplitBar>("RadSplitBar1");
                Assert.IsNotNull(splitbar);
                splitbar.ForwardElement.MouseClick();
            }

            ActiveBrowser.WaitForElement(50000,"id="+object_name);
            HtmlSpan pivot = Find.ById<HtmlSpan>(object_name);
            Assert.IsNotNull(pivot);
            pivot.Click();
            //pivot.Click();
           // System.Threading.Thread.Sleep(3000);
            ActiveBrowser.WaitForElement(50000, "id=BOTitle");
            HtmlSpan title = Find.ById<HtmlSpan>("BOTitle");
            Assert.IsNotNull(title, "Title could not be loaded properly");
            try
            {
                title.Wait.ForContent(FindContentType.InnerText, pivot.InnerText, 50000);
            }
            catch
            {
                Assert.IsTrue(title.InnerText.Equals(pivot.InnerText), "Wrong Text in title");
            }
            ActiveBrowser.Window.WaitForVisibility(true,5000);
            ActiveBrowser.WaitUntilReady();

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."));
        }

        #region sciaezka do plikow tesowych
        protected string dir = "C:\\testy\\Pliki_Testowe\\";
        #endregion
        #region nazwy obiektow
        protected string chartName = "TreeObject.BusinessObject.105";
        protected string BSCName = "TreeObject.BusinessObject.18";
        protected string BenchmarkName = "TreeObject.BusinessObject.17";
        protected string mapName = "TreeObject.BusinessObject.19";
        protected string dboardName = "TreeObject.BusinessObject.23";
        protected string pivotName = "TreeObject.BusinessObject.13";
        protected string presentationName = "TreeObject.BusinessObject.22";
        #endregion

        protected void IMGCompare(string filename, string div_name)
        {
            System.Threading.Thread.Sleep(5000);
            System.Drawing.Bitmap bmp = ActiveBrowser.Window.GetBitmap(Find.ById<HtmlDiv>(div_name).GetRectangle());
            //bmp.Save(dir + filename);
            System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(dir+filename);

            Assert.IsTrue(ImagesHandler.CompareImage(bmp, bmp2, 0.01, 0.1) < 0.1, "Maybe Some of the image elements could not be loaded");
            Assert.IsTrue(ImagesHandler.CompareImage(bmp, bmp2, 0.01, 0.1) < 0.05, "Maybe Some of the image elements could not be loaded");
        }
        protected string ConvertNameDependingOnBrowser(string name)
        {
            switch (ActiveBrowser.BrowserType)
            {
                case BrowserType.Chrome:
                    { return "CH_" + name; }
                case BrowserType.FireFox:
                    return "FF_"+name;
                case BrowserType.InternetExplorer:
                    return "IE_" + name;
                default:
                    return name;
            }
        }

        protected RadToolBar GetToolBar()
        {
            HtmlAnchor a = Find.ById<HtmlAnchor>("toolbarClick");
            a.MouseClick();
            HtmlDiv div_menu = Find.ById<HtmlDiv>("Mainc_toolbarPaneNewPlaceHolder");
            Assert.IsNotNull(div_menu);
            div_menu.Wait.ForExists();
            ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_Toolbar"), 5000);
            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_Toolbar");
            Assert.IsNotNull(rtb);
            return rtb;
        }

        protected void DownloadFileFromWeb(string filename, DownloadFileTypes fileType, RadToolBar rtb)
        {
            RadToolBarItem rtb_item01 = null;
            switch (fileType)
            {
                case DownloadFileTypes.CSV:
                    {
                        rtb_item01 = rtb.FindItemByText("CSV");
                        break;
                    }
                case DownloadFileTypes.INVPNG:
                    {
                        rtb_item01 = rtb.FindItemByText("Przezroczysty (PNG)");
                        break;
                    }
                case DownloadFileTypes.PNG:
                    {
                        rtb_item01 = rtb.FindItemByText("PNG");
                        break;
                    }
                case DownloadFileTypes.PPTX:
                    {
                        rtb_item01 = rtb.FindItemByText("Power Point (PPTX)");
                        break;
                    }
                case DownloadFileTypes.XLS:
                    {
                        rtb_item01 = rtb.FindItemByText("Excel (XLS)");
                        break;
                    }
                case DownloadFileTypes.XLSX:
                    {
                        rtb_item01 = rtb.FindItemByText("Aktualizowalny Excel (XLSX)");
                        break;
                    }

            }

            Assert.IsNotNull(rtb_item01, "Could not find option when trying to download the file");
            DownloadDialogsHandler handler = new DownloadDialogsHandler(ActiveBrowser, DialogButton.SAVE, dir + filename, Desktop);
            try
            {
                rtb_item01.Click();
            }
            catch { }
            ActiveBrowser.Window.SetFocus();

            handler.WaitUntilHandled(20000);

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            System.Threading.Thread.Sleep(4000);
            DirectoryInfo directory = new DirectoryInfo(dir);
            Assert.IsTrue(directory.GetFiles(filename, SearchOption.AllDirectories).Length != 0);
            FileInfo file = directory.GetFiles(filename, SearchOption.TopDirectoryOnly)[0];
            file.Delete();
            Assert.IsTrue(directory.GetFiles(filename, SearchOption.AllDirectories).Length == 0);
        }

        protected enum DownloadFileTypes
        { XLS,XLSX,PPTX,CSV,PNG,INVPNG}
    }
}
