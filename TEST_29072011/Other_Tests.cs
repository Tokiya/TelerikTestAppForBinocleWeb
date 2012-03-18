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

using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for Other_Tests
    /// </summary>
    [TestClass]
    public class Other_Tests : BaseTestClass
    {

      //ff - dupa

        [TestMethod]
        public void LogoClick()
        {
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_PANE_CONTENT_Top"),5000);
            ActiveBrowser.WaitForElement(5000, "id=RAD_SPLITTER_PANE_CONTENT_Top");
            HtmlDiv logo = Find.ByXPath<HtmlDiv>("//*[@id=\"RAD_SPLITTER_PANE_CONTENT_Top\"]/div[1]");
            logo.Refresh();
            Assert.IsNotNull(logo);
            logo.MouseClick();
            System.Threading.Thread.Sleep(3000);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            //Assert.IsNull(logo);
        }

        [TestMethod]
        public void SearchTest()
        {
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=search"),5000);
            ActiveBrowser.WaitForElement(5000,"id=search");
            HtmlInputText search = Find.ById<HtmlInputText>("search");
            Assert.IsNotNull(search);
            
             HtmlInputImage img = Find.ById<HtmlInputImage>("searchButton");
            Assert.IsNotNull(img);
            img.Click();
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=ctl05_RadListView1_ctrl0_bolink"),5000);
            ActiveBrowser.WaitForElement(50000, "id=ctl04_RadListView1_ctrl0_bolink");
            HtmlAnchor a = Find.ById<HtmlAnchor>("ctl04_RadListView1_ctrl0_bolink");


            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsTrue(a.InnerText.Contains("real estates"));

        }

        [TestMethod]
        [Ignore]
        public void GoToSettingsWeb()
        {
         
            Manager.SetNewBrowserTracking(true);

           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=PreferencesImage"),5000);
            ActiveBrowser.WaitForElement(5000,"id=PreferencesImage");
            HtmlImage img = Find.ById<HtmlImage>("PreferencesImage");
            img.Click();
       
            Manager.WaitForNewBrowserConnect("http://localhost/BinocleWeb/Preferences.aspx",true, 15000);

  
            Manager.SetNewBrowserTracking(false);

            Assert.IsTrue(Manager.Browsers.Count == 2);
            Assert.IsTrue(ActiveBrowser.Url.Contains("Preferences"));
            try
            {
                Assert.IsTrue(ActiveBrowser.ContainsText("Zmiana hasła"));
            }
            catch {
                Assert.IsTrue(ActiveBrowser.ContainsText("Change password"));
            }
            ActiveBrowser.Close();
            //Thread.Sleep(1000);
            //Assert.IsTrue(Manager.Browsers.Count == 1);
        }

        [TestMethod]
        [Ignore]
        public void GoToContactPage()
        {
            Manager.SetNewBrowserTracking(true);
            OpenObject(dboardName);

            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_1dsitv0_GoToContactFormImage"),15000);
            ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_1dsitv0_GoToContactFormImage");
            HtmlImage img = Find.ById<HtmlImage>("Mainc_BOCc_1dsitv0_GoToContactFormImage");
            img.Click();

            Manager.WaitForNewBrowserConnect("http://localhost/BinocleWeb/ContactForm.aspx", true, 15000);


            Manager.SetNewBrowserTracking(false);

            Assert.IsTrue(Manager.Browsers.Count == 2);
            Assert.IsTrue(ActiveBrowser.Url.Contains("Contact"));
            try
            {
                Assert.IsTrue(ActiveBrowser.ContainsText("Wysyłanie komentarza"));
            }
            catch
            {
                Assert.IsTrue(ActiveBrowser.ContainsText("Send Comment"));
            }
            ActiveBrowser.Window.Close();
        }

        [TestMethod]//nie ma na liscie testow
        public void ReopenObjectAfterSearchAction_issue2718()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(25000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
           
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=search"), 5000);
            ActiveBrowser.WaitForElement(5000,"id=search");
            HtmlInputText search = Find.ById<HtmlInputText>("search");
            Assert.IsNotNull(search);
            search.Text = "chart";
            HtmlInputImage img = Find.ById<HtmlInputImage>("searchButton");
            Assert.IsNotNull(img);
            img.Click();
            System.Threading.Thread.Sleep(4000);
            HtmlSpan pivot = Find.ById<HtmlSpan>(dboardName);
            pivot.Click();
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
           
            System.Threading.Thread.Sleep(3000);
            ActiveBrowser.Window.WaitForVisibility(true, 5000);
            ActiveBrowser.WaitUntilReady();
            pivot.Click();

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");
        }


    }
}
