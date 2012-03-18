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
    /// Summary description for BSC_Tests
    /// </summary>
    [TestClass]
    public class BSC_Tests : BaseTestClass
    {

         
        #region Tests
        [TestMethod]
        public void OpenBSC_Test()
        {
            OpenObject(BSCName);
        
        }

        [TestMethod]
        public void BSCtoXLSX2()
        {
            OpenObject(BSCName);
            DownloadFileFromWeb("BSC.xlsx", DownloadFileTypes.XLSX, GetToolBar());
        }

        [TestMethod]
        public void BSCRefresh()
        {
            OpenObject(BSCName);
            RadToolBar rtb = GetToolBar();
            rtb.RootItems[1].Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
         }

        [TestMethod]
        public void BSCGridExpand()
        {
            OpenObject(BSCName);
            ActiveBrowser.WaitForElement(5000,"id=toolbarClick");
            HtmlInputSubmit expand = Find.ById<HtmlInputSubmit>("Mainc_BOCc_BOCcc_gvKPIs_mtv_di0_MyExpandCollapseButton");
                Assert.IsNotNull(expand);
                Assert.IsTrue(Find.ById("Mainc_BOCc_BOCcc_gvKPIs_mtv_ctl06_Detail10_di0_lb").InnerText.Contains("Australia"));

                Assert.IsTrue(Find.ById<HtmlAnchor>("Mainc_BOCc_BOCcc_gvKPIs_mtv_ctl06_Detail10_di0_lb").IsVisible());

                expand.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Assert.IsFalse(Find.ById<HtmlAnchor>("Mainc_BOCc_BOCcc_gvKPIs_mtv_ctl06_Detail10_di0_lb").IsVisible());

                expand.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Assert.IsTrue(Find.ById<HtmlAnchor>("Mainc_BOCc_BOCcc_gvKPIs_mtv_ctl06_Detail10_di0_lb").IsVisible());
 
        }

        [TestMethod]
        public void BSCNotificationToolTip()
        {
            OpenObject(BSCName);
            ActiveBrowser.WaitForElement(20000, "id=Mainc_BOCc_BOCcc_gvKPIs_mtv_di0_tImage");
            HtmlImage img_notify = Find.ById<HtmlImage>("Mainc_BOCc_BOCcc_gvKPIs_mtv_di0_tImage");
            img_notify.MouseHover();
            ActiveBrowser.WaitForElement(5000, "id=RadToolTipWrapper_Mainc_BOCc_BOCcc_gvKPIs_mtv_di0_RadToolTip1");
            HtmlDiv rtt = Find.ById<HtmlDiv>("RadToolTipWrapper_Mainc_BOCc_BOCcc_gvKPIs_mtv_di0_RadToolTip1");
            Assert.IsNotNull(rtt);
            Assert.IsTrue(rtt.InnerText.Contains("Total revenue from operations."));
            
        }
        #endregion


    }
}
