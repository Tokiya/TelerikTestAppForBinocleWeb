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

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for Benchmark_Tests
    /// </summary>
    [TestClass]
    public class Benchmark_Tests : BaseTestClass
    {

        private string object_name = "TreeObject.BusinessObject.17";

        [TestMethod]
        public void OpenBenchmark_test()
        {

            OpenObject(BenchmarkName);
        }

        [TestMethod]
        public void BenchmarkFilterToSelection()//do dopracowania jeszcze, ale praktycznie dziala tylko nie pod chromem !! >:-[
        {

            OpenObject(BenchmarkName);

            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=toolbarClick"), 5000);
            ActiveBrowser.WaitForElement(5000,"id=toolbarClick");
                HtmlTableRow trow = Find.ById<HtmlTableRow>("Mainc_BOCc_gvKPIs_ctl00__1");
                Assert.IsNotNull(trow);

                trow.Click();

                string trow_id = new string(trow.ID.ToCharArray());
                RadMenu menu = Find.ById<RadMenu>("Mainc_BOCc_contextMenu_detached");

                Assert.IsNotNull(menu);
                RadMenuItem menu_item01 = menu.FindItemByText("Filtruj do zaznaczonych elementów");
                Assert.IsNotNull(menu_item01);

                Assert.IsTrue(Find.ById<HtmlTableRow>("Mainc_BOCc_gvKPIs_ctl00__0").IsVisible());
                Assert.IsTrue(Find.ById<HtmlTableRow>("Mainc_BOCc_gvKPIs_ctl00__2").IsVisible());
                menu_item01.Click();

                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !");
                Thread.Sleep(1000);
                Assert.IsTrue(Find.ById<HtmlTableRow>("Mainc_BOCc_gvKPIs_ctl00__0").IsVisible());
            if(ActiveBrowser.BrowserType==BrowserType.Chrome)    
            ActiveBrowser.Refresh();
                Assert.IsNull(Find.ById<HtmlTableRow>("Mainc_BOCc_gvKPIs_ctl00__2"));
              
            }
        }
        
        
        

    }

