using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Telerik.WebAii.Controls.Html;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BITestAdamLib;
namespace TEST_29072011
{
    /// <summary>
    /// Summary description for Presentation_Tests
    /// </summary>
    [TestClass]
    public class Presentation_Tests : BaseTestClass
    {


        [TestMethod]
        public void OpenPresentation_test()
        {
            OpenObject(presentationName);
        }

        [TestMethod]
        public void PresentationNavigatePanel()
        {
            OpenObject(presentationName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_slideCount"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_slideCount");
                HtmlSpan nr_strony = Find.ById<HtmlSpan>("Mainc_BOCc_slideCount");
                Assert.IsNotNull(nr_strony);
                Assert.IsTrue(nr_strony.InnerText.Equals("1"));
                HtmlInputImage arrow = Find.ById<HtmlInputImage>("Mainc_BOCc_arrowRight");
                Assert.IsNotNull(arrow);
                arrow.Click();
                Thread.Sleep(5000);

                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_slideCount"), 10000);
                ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_slideCount");
           
            if(ActiveBrowser.BrowserType==BrowserType.Chrome)
                     ActiveBrowser.Refresh();
                
                Assert.IsTrue(Find.ById<HtmlSpan>("Mainc_BOCc_slideCount").InnerText.Equals("2"));
                arrow = Find.ById<HtmlInputImage>("Mainc_BOCc_arrowLeft");
                Assert.IsNotNull(arrow);
                arrow.Click();
                Thread.Sleep(5000);

                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_slideCount");
           
            if(ActiveBrowser.BrowserType==BrowserType.Chrome)
                     ActiveBrowser.Refresh();

                Assert.IsTrue(Find.ById<HtmlSpan>("Mainc_BOCc_slideCount").InnerText.Equals("1"));
 
        }

        [TestMethod]
        public void PresentationRefresh()
        {

            OpenObject(presentationName);
            ActiveBrowser.WaitForElement(15000,"id=Mainc_Toolbar");
                RadToolBar rtb = Find.ById<RadToolBar>("Mainc_Toolbar");
                Assert.IsNotNull(rtb);
                rtb.RootItems[1].Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        [Ignore]
        public void PresentationToPPTX2()
        {
            OpenObject(presentationName);
            ActiveBrowser.WaitForElement(15000,"id=toolbarClick");
            DownloadFileFromWeb("Presentation.pptx)",DownloadFileTypes.PPTX,GetToolBar());
        }

        [TestMethod]
        public void PresentationNavigation_byToolBar()
        {
            OpenObject(presentationName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_Toolbar"),15000);
            ActiveBrowser.WaitForElement(15000,"id=Mainc_Toolbar");
                Assert.IsFalse(Find.ById<RadToolBar>("Mainc_Toolbar").RootItems[4].Enabled);
                Find.ById<RadToolBar>("Mainc_Toolbar").RootItems[5].Click();
               // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_Toolbar"), 15000);
                ActiveBrowser.WaitForElement(15000, "id=Mainc_Toolbar");
           
                Assert.IsFalse(Find.ById<RadToolBar>("Mainc_Toolbar").RootItems[5].Enabled);

                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Thread.Sleep(5000);

               // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_slideCount"), 10000);
                ActiveBrowser.WaitForElement(10000, "id=Mainc_BOCc_slideCount");
                ActiveBrowser.RefreshDomTree();

                Assert.IsTrue(Find.ById<HtmlSpan>("Mainc_BOCc_slideCount").InnerText.Equals("2"));
                Find.ById<RadToolBar>("Mainc_Toolbar").RootItems[4].Click();
                Assert.IsFalse(Find.ById<RadToolBar>("Mainc_Toolbar").RootItems[4].Enabled);

                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Thread.Sleep(5000);

               // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_slideCount"), 10000);
                ActiveBrowser.WaitForElement(10000, "id=Mainc_BOCc_slideCount");
                   
            ActiveBrowser.RefreshDomTree();

                Assert.IsTrue(Find.ById<HtmlSpan>("Mainc_BOCc_slideCount").InnerText.Equals("1"));    
        }


    }
}
