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
    /// Summary description for Dboard
    /// </summary>
    [TestClass]
    public class Dboard_Tests : BaseTestClass
    {
        
        [TestMethod]
        public void OpenDboard_test()
        {
            OpenObject(dboardName);
            ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
        }

        [TestMethod]
        public void DboardRefresh()
        {

            OpenObject(dboardName);

            ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"),15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
                RadToolBar rtb = Find.ById<RadToolBar>("Mainc_Toolbar");
                rtb.RootItems[1].Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void DboardToXLSX2()
        {
            OpenObject(dboardName);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            DownloadFileFromWeb("Dboard.xlsx", DownloadFileTypes.XLSX,GetToolBar());

        }

        [TestMethod]
        public void DboardToXLS2()
        {
            OpenObject(dboardName);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            DownloadFileFromWeb("Dboard.xls", DownloadFileTypes.XLS,GetToolBar());
        }

        [TestMethod]
        public void DboardHideFilterBYToolBar()//głupi chrome 
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
                RadToolBar rtb = Find.ById<RadToolBar>("Mainc_Toolbar");

                HtmlDiv filtr = Find.ById<HtmlDiv>("RAD_SPLITTER_PANE_CONTENT_Mainc_BOCc_filter_panelc_FilterItemsPane");
                Assert.IsNotNull(filtr);
                ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_PANE_CONTENT_Mainc_BOCc_filter_panelc_FilterItemsPane"), 5000);
                Assert.IsTrue(filtr.IsVisible());
                RadToolBarItem rtb_item04 = rtb.RootItems[4];
                Assert.IsNotNull(rtb_item04);

                try
                {
                    rtb_item04.Click();
                }
                catch { }
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            }

        [TestMethod]
        public void DboardFiltering()// Naprawde g³upi chrome
        {

            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
                RadTreeView filtr = Find.ById<RadTreeView>("Mainc_BOCc_filter_panelc_fi0_C_Cc_Ccc_tvHierarchy");
                Assert.IsNotNull(filtr);

                RadTreeNode filtr_tnode = filtr.FindNodeByText("France");
                Assert.IsNotNull(filtr_tnode);
                filtr_tnode.Uncheck();
                HtmlInputSubmit filtruj = Find.ById<HtmlInputSubmit>("Mainc_BOCc_filter_panelc_FilterItemsApply");
                Assert.IsNotNull(filtruj);
                HtmlSpan tytul = Find.ById<HtmlSpan>("Mainc_BOCc_1dsitv0_lblTitle");
                Assert.IsNotNull(tytul);
                int k = tytul.InnerText.Length;
                Assert.IsFalse(ActiveBrowser.ContainsText("Australia, Canada, Germany, United Kingdom, United States"));
                filtruj.Click();
                Thread.Sleep(2000);
                ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            if(ActiveBrowser.BrowserType==BrowserType.Chrome)   
            ActiveBrowser.Refresh();
                Assert.IsTrue(ActiveBrowser.ContainsText("Australia, Canada, Germany, United Kingdom, United States"));
        }

        [TestMethod]
        [Description("operacje na wewnetrznych obiektach")]
        public void Dboard_ChartsToPivots_issue2665()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(25000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
            RadToolBar rtb_chart = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv0_DIc_Toolbar");
            Assert.IsNotNull(rtb_chart);
            RadToolBarItem item = rtb_chart.FindItemByText("Tabela przestawna");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !!");
            item = rtb_chart.FindItemByText("Wykres");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed");
        }

        [TestMethod]
        [Description("operacje na wewnetrznych obiektach")]
        public void Dboard_ChartsToMap_issue2665()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
            RadToolBar rtb_chart = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv0_DIc_Toolbar");
            Assert.IsNotNull(rtb_chart);
            RadToolBarItem item = rtb_chart.FindItemByText("Mapa");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !!");
            item = rtb_chart.FindItemByText("Wykres");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !!");
        }

        [TestMethod]
        public void Dboard_PivotToChart()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
            RadToolBar rtb_pivot = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv2_DIc_Toolbar");

            Assert.IsNotNull(rtb_pivot);
            RadToolBarItem item = rtb_pivot.FindItemByText("Wykres");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
         
            item = rtb_pivot.FindItemByText("Tabela przestawna");

            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
     
        }

        [TestMethod]
        public void Dboard_PivotToMap()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0"); 
           
            RadToolBar rtb_pivot = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");

            Assert.IsNotNull(rtb_pivot);
            RadToolBarItem item = rtb_pivot.FindItemByText("Mapa");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb_pivot.FindItemByText("Tabela przestawna");

            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void Dboard_MapToChart_issue2665()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_1dsitv2_DIc_Toolbar"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_1dsitv2_DIc_Toolbar");
            RadToolBar rtb_map = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv2_DIc_Toolbar");
            Assert.IsNotNull(rtb_map);
            RadToolBarItem item = rtb_map.FindItemByText("Wykres");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");
            item = rtb_map.FindItemByText("Mapa");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");
     
        }

        [TestMethod]
        public void Dboard_MapToPivot_issue2665()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            RadToolBar rtb_map = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv1_DIc_Toolbar");
            Assert.IsNotNull(rtb_map);
            RadToolBarItem item = rtb_map.FindItemByText("Tabela przestawna");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !!");
            item = rtb_map.FindItemByText("Mapa");
            Assert.IsNotNull(item);
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"BinocleWeb Crashed !!");

        }

        [TestMethod]
        public void Doboard_Chart3D()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            
            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.RootItems[2];
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        public void Dboard_ChartChangeDesign()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            
            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.FindItemByText("Jagoda");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Ogień");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Morska zieleń");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        public void Dboard_ChartChangeType()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.FindItemByText("Warstwowy");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Interpolowany");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Słupkowy");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Polarowy");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Lejkowy");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");





        }

        [TestMethod]
        public void Dboard_ChartLegend()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(25000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.FindItemByText("Góra");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Lewo");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Ukryj tło");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Ukryj wewnętrzne legendy");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Ukryj legendę");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        public void Dboard_ChartSplitting()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(25000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.RootItems[6];
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        public void Dboard_ChartSeriesInRow()
        {
            OpenObject(dboardName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.RootItems[7];
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        public void Dboard_ChartShowValues()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);

            RadToolBarItem item = rtb.FindItemByText("Pokazuj wartości");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Domyślny");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            item = rtb.FindItemByText("Miliony");
            Assert.IsNotNull(item);

            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void Dboard_ChartToPNG2()
        {
            OpenObject(dboardName);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv1_DIc_Toolbar");
            Assert.IsNotNull(rtb);
            DownloadFileFromWeb("dboardChart.png", DownloadFileTypes.PNG,rtb);

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void Dboard_PivotToXLS2()
        {
            OpenObject(dboardName);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv3_DIc_Toolbar");
            Assert.IsNotNull(rtb);
            DownloadFileFromWeb("dboardPivot.xls", DownloadFileTypes.XLS, rtb);

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void Dboard_MapToXLS2()
        {
            OpenObject(dboardName);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");

            RadToolBar rtb = Find.ById<RadToolBar>("Mainc_BOCc_1dsitv2_DIc_Toolbar");
            Assert.IsNotNull(rtb);
            DownloadFileFromWeb("dboardmap.xls", DownloadFileTypes.XLS, rtb);
            
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void Dboard_FilterContextMenu_selectAll()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            
            RadTreeView filter = Find.ById<RadTreeView>("Mainc_BOCc_filter_panelc_fi0_C_Cc_Ccc_tvHierarchy");

            HtmlAnchor narzedzia = Find.ByAttributes<HtmlAnchor>("title=Narzędzia");
            Assert.IsNotNull(narzedzia);
            narzedzia.MouseClick();
            ActiveBrowser.RefreshDomTree();
           // ActiveBrowser.Actions.WaitForElement(new FindParam("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context"), 5000);
            ActiveBrowser.WaitForElement(5000, "class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            RadMenu menu = Find.ByExpression<RadMenu>("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            Assert.IsNotNull(menu);
            menu.Wait.ForVisible();

            RadMenuItem item = menu.FindItemByText("Odznacz wszystko");
            Assert.IsNotNull(item);
            item.Wait.ForExists();
            item.Wait.ForVisible();
            item.Focus();
            item.Click();
           // ActiveBrowser.Actions.WaitForElement(new FindParam("class=RadTreeView RadTreeView_Windows7"), 5000);
            ActiveBrowser.WaitForElement(5000, "class=RadTreeView RadTreeView_Windows7");
            RadTreeView filtr = Find.ByExpression<RadTreeView>("id=Mainc_BOCc_filter_panelc_fi0_C_Cc_Ccc_tvHierarchy", "class=RadTreeView RadTreeView_Windows7");
            Assert.IsNotNull(filtr);
            foreach (RadTreeNode node in filtr.RootNodes)
            {
                Assert.IsFalse(node.Checked);
                node.MouseHover();
            }
            narzedzia = Find.ByAttributes<HtmlAnchor>("title=Narzędzia");
            Assert.IsNotNull(narzedzia);
            narzedzia.MouseClick();

            item = menu.FindItemByText("Zaznacz wszystko");
            Assert.IsNotNull(item);
            item.Wait.ForExists();
            item.Wait.ForVisible();
            item.Focus();
            item.Click();

            

            foreach (RadTreeNode node in filtr.RootNodes)
            {   
                Assert.IsTrue(node.Checked);
                node.MouseHover();
            }
            
        }

        [TestMethod]
        public void DboardGoToLinkedRaport()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_1dsitv0_imgLinkAction"), 15000);
            ActiveBrowser.WaitForElement(15000, "id=Mainc_BOCc_1dsitv0_imgLinkAction");
            HtmlImage img = Find.ById<HtmlImage>("Mainc_BOCc_1dsitv0_imgLinkAction");
            Assert.IsNotNull(img);

            img.Click();

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."),"Binocleweb Crashed");
            
        }

        [TestMethod]//nie w liscie
        public void DBoard_filterSearch()
        {
            OpenObject(dboardName);
           // ActiveBrowser.Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_Mainc_BOCc_1S0"), 15000);
            ActiveBrowser.WaitForElement(20000, "id=RAD_SPLITTER_Mainc_BOCc_1S0");
            //ActiveBrowser.Actions.WaitForElement(new FindParam("class=RadTreeView RadTreeView_Windows7"), 5000);
            ActiveBrowser.WaitForElement(5000, "class=RadTreeView RadTreeView_Windows7");
            RadTreeView filtr = Find.ByExpression<RadTreeView>("id=Mainc_BOCc_filter_panelc_fi0_C_Cc_Ccc_tvHierarchy", "class=RadTreeView RadTreeView_Windows7");
            Assert.IsNotNull(filtr);
            int count = filtr.AllNodes.Count;
            Assert.IsNotNull(filtr.FindNodeByText(filtr.RootNodes[1].InnerText));

            HtmlAnchor narzedzia = Find.ByAttributes<HtmlAnchor>("title=Narzędzia");
            Assert.IsNotNull(narzedzia);
            narzedzia.MouseClick();
            ActiveBrowser.RefreshDomTree();
            //ActiveBrowser.Actions.WaitForElement(new FindParam("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context"), 5000);
            ActiveBrowser.WaitForElement(5000, "class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            RadMenu menu = Find.ByExpression<RadMenu>("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            Assert.IsNotNull(menu);
            menu.Wait.ForVisible();

            RadMenuItem item = menu.FindItemByText("Szukaj");
            Assert.IsNotNull(item);
            item.Wait.ForExists();
            item.Wait.ForVisible();
            item.Focus();
            item.Click();
            
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_filter_panelc_tvHierarchySearchDialog_C_tvHierarchySearchDialogText"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_filter_panelc_tvHierarchySearchDialog_C_tvHierarchySearchDialogText");
            HtmlInputText searchtext = Find.ById<HtmlInputText>("Mainc_BOCc_filter_panelc_tvHierarchySearchDialog_C_tvHierarchySearchDialogText");
            ActiveBrowser.Actions.ScrollToVisible(Find.ById("Mainc_BOCc_filter_panelc_tvHierarchySearchDialog_C_tvHierarchySearchDialogText"));
            Desktop.Mouse.Click(MouseClickType.LeftClick, searchtext.GetRectangle());
            Desktop.KeyBoard.TypeText(filtr.RootNodes[0].InnerText, 5);
            
            //Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);   
            HtmlInputButton searchbtn = Find.ByExpression<HtmlInputButton>("type=button","value=Szukaj");
            Assert.IsNotNull(searchbtn);
            searchbtn.Click();
           
            narzedzia = Find.ByAttributes<HtmlAnchor>("title=Narzędzia");
            Assert.IsNotNull(narzedzia);
            narzedzia.MouseClick();
            ActiveBrowser.RefreshDomTree();
            
           // ActiveBrowser.Actions.WaitForElement(new FindParam("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context"), 5000);
            ActiveBrowser.WaitForElement(5000, "class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            menu = Find.ByExpression<RadMenu>("class=RadMenu RadMenu_Vista RadMenu_Context RadMenu_Vista_Context");
            Assert.IsNotNull(menu);
            menu.Wait.ForVisible();

            item = menu.FindItemByText("Cofnij szukanie");
            Assert.IsNotNull(item);
            item.Wait.ForExists();
            item.Wait.ForVisible();
            item.Focus();
            item.Click();
            Assert.IsTrue(count==filtr.RootNodes.Count);

        }

    }

}
