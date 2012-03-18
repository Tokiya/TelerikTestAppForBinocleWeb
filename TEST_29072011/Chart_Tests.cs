using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.IO;
using BITestAdamLib;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Telerik.WebAii.Controls.Html;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest1
    /// </summary>
    [TestClass]
    public class Chart_Tests : BaseTestClass
    {
        
        #region Tests

        [TestMethod]
        public void OpenChartTest()
        {
            OpenObject(chartName);

        }

        [TestMethod]
        public void OpenChart_IMGCompare()
        {
            OpenObject(chartName);
            string divName = "Mainc_BOCc_visualisation";
            ActiveBrowser.WaitForElement(10000, "id="+divName);
            Assert.IsNotNull(Find.ById(divName));
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            string filename = ConvertNameDependingOnBrowser("chart.png");
            IMGCompare(filename, divName);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");             
        }

        [TestMethod]
        public void ChartContextMenu()
        {
            OpenObject(chartName);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_visualisationc_chart");
            HtmlImage chart=Find.ById<HtmlImage>("Mainc_BOCc_visualisationc_chart");
            chart.MouseHover();
            chart.MouseClick(MouseClickType.RightDown);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_visualisationc_contextMenu_detached");
            RadMenu menu01 = Find.ById<RadMenu>("Mainc_BOCc_visualisationc_contextMenu_detached");
           
            menu01.Wait.ForExists();
            menu01.Wait.ForVisible();
            Thread.Sleep(3000);
            Assert.IsNotNull(menu01);
            ActiveBrowser.WaitForElement(5000,"class=rmItem rmFirst");
            RadMenuItem menu01_item = null;
            try
            {
                menu01_item = menu01.FindItemByText("1 ");
                menu01_item.Wait.ForVisible();
            }
            catch {
                 menu01_item = menu01.AllItems[0];
            }

            menu01_item.Wait.ForVisible();
            menu01_item.MouseHover();
            RadMenuItem menu02_item = menu01_item.FindItemByText("Internet Total Product Cost");
            Assert.IsNotNull(menu02_item);
            menu02_item.Wait.ForVisible();
            menu02_item.MouseHover();
            RadMenuItem menu03_item = menu02_item.FindItemByText("Na prawej osi");
            menu03_item.Wait.ForVisible();
            menu03_item.MouseHover();
            menu03_item.Click();
            Desktop.Mouse.Click(MouseClickType.RightClick, new Point(0,0));
           
        }

        [TestMethod]
        public void ChartToPivot()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.FindItemByText("Tabela przestawna");
            Assert.IsNotNull(rtb_item01);
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void ChartToMap()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.FindItemByText("Mapa");
            Assert.IsNotNull(rtb_item01);
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void ChartRefresh()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.RootItems[1];
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void ChartToInvisiblePNG2()
        {
            OpenObject(chartName);
            DownloadFileFromWeb("chart_inv.png", DownloadFileTypes.INVPNG,GetToolBar());
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void ChartToPNG2()
        {
            OpenObject(chartName);
            DownloadFileFromWeb("chart01.png", DownloadFileTypes.PNG,GetToolBar());
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
          
        }
       
       
        [TestMethod]
        public void ChartToXLS2()
        {
            OpenObject(chartName);
            DownloadFileFromWeb("chart01.xls", DownloadFileTypes.XLS,GetToolBar());
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void ChartToCSV2()
        {
            OpenObject(chartName);
            DownloadFileFromWeb("chart01.csv", DownloadFileTypes.CSV,GetToolBar());
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void Chart2D_ConvertTo3D_IMGCompare()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.RootItems[4];
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            string filename = ConvertNameDependingOnBrowser("chart_3D.PNG");
            IMGCompare(filename, "Mainc_BOCc_visualisation");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
          
        }

        [TestMethod]
        public void ChangeDesignToChart_IMGCompare()
        {
            OpenObject(chartName);
            string[] opcje = { "Jagoda", "Jasna pastelowa", "Czekolada", "Barwy ziemi", "Excel", "Ogień", "Jasna pastelowa", "Morska zieleń", "Półprzeźroczysta" };
            for (int i = 0; i < opcje.Length; i++)
            {
                RadToolBar rtb = GetToolBar();
                RadToolBarItem rtb_item01 = rtb.FindItemByText(opcje[i]);
                Assert.IsNotNull(rtb_item01,opcje[i] +" design could not be found");
                rtb_item01.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                string filename = ConvertNameDependingOnBrowser(i+"_chart_design.PNG");
                IMGCompare(filename, "Mainc_BOCc_visualisation");
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            }
        }

        [TestMethod]
        public void ChangeChartType_IMGCompare()
        {
            OpenObject(chartName);
            string[] opcje = { "Warstwowy", "Kolumnowy","Liniowy","Punktowy","Interpolowany",
                           "Interpolowany warstwowy",
                             "Skumulowany warstwowy","Skumulowany warstwowy do 100%","Schodkowy",
                             "Skumulowany kolumnowy","Skumulowany kolumnowy do 100%",
                             "Słupkowy","Skumulowany słupkowy","Skumulowany słupkowy do 100%",
                             "Polarowy","Radarowy"};
            for (int i = 0; i < opcje.Length; i++)
            {
                RadToolBar rtb = GetToolBar();
                RadToolBarItem rtb_item01 = rtb.FindItemByText(opcje[i]);
                Assert.IsNotNull(rtb_item01,opcje[i]+"type could not be found");

                rtb_item01.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                string filename = ConvertNameDependingOnBrowser(opcje[i]+"_chart.PNG");
                IMGCompare(filename, "Mainc_BOCc_visualisation");
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            }
        }

        [TestMethod]
        public void ChartLegend_IMGCompare()
        {
            OpenObject(chartName);
            ActiveBrowser.WaitForElement(5000, "id=toolbarClick");
            string[] opcje = { "Góra", "Lewo", "Prawo", "Dół", "Ukryj wewnętrzne legendy", "Ukryj tło", "Ukryj legendę" };
            for (int i = 0; i < opcje.Length; i++)
            {
                RadToolBar rtb = GetToolBar();
                RadToolBarItem rtb_item01 = rtb.FindItemByText(opcje[i]);
                Assert.IsNotNull(rtb_item01);

                rtb_item01.Click();
                string filename = ConvertNameDependingOnBrowser(opcje[i]+"_chart_legend.png");
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                IMGCompare(filename, "Mainc_BOCc_visualisation");
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            }
        }

        [TestMethod]
        public void ChartSeriesInRows_IMGCompare()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.RootItems[9];
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            string filename = ConvertNameDependingOnBrowser("chart_seriesInRow.png");
            IMGCompare(filename, "Mainc_BOCc_visualisation");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        
        }

        [TestMethod]
        public void SplitChart_IMGCompare()
        {
            OpenObject(chartName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.RootItems[9];
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            rtb_item01 = GetToolBar().RootItems[8];
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            string filename = ConvertNameDependingOnBrowser("chart_split.png");
            IMGCompare(filename, "Mainc_BOCc_visualisation");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        #endregion
    }
}
