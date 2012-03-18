using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using ArtOfTest.WebAii.Controls.HtmlControls;
using ArtOfTest.WebAii.Controls.HtmlControls.HtmlAsserts;
using ArtOfTest.WebAii.Core;
using ArtOfTest.WebAii.ObjectModel;
using ArtOfTest.WebAii.TestAttributes;
using ArtOfTest.WebAii.TestTemplates;
using ArtOfTest.WebAii.Win32.Dialogs;
using Telerik.WebAii.Controls.Html;
using ArtOfTest.WebAii.Silverlight;
//using ArtOfTest.WebAii.Silverlight.UI;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using BITestAdamLib;

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for Map_Tests
    /// </summary>
    [TestClass]
    public class Map_Tests : BaseTestClass
    {
        #region Tests
        [TestMethod]
        public void MapOpenMapTest_IMGCompare()
        {
            OpenObject(mapName);
            string divName = "Mainc$BOCc$visualisationc$Map";
            ActiveBrowser.WaitForElement(15000, "id="+divName);
            Assert.IsNotNull(Find.ById(divName));

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            string filename = ConvertNameDependingOnBrowser("map1.png");
            Thread.Sleep(5000); 
            IMGCompare(filename, divName);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void MapToChart()
        {
            OpenObject(mapName);
                RadToolBar rtb = GetToolBar();
                RadToolBarItem rtb_item01 = rtb.FindItemByText("Wykres");
                Assert.IsNotNull(rtb_item01);
                rtb_item01.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void MapToPivot()
        {
            OpenObject(mapName);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.FindItemByText("Tabela przestawna");
            Assert.IsNotNull(rtb_item01,"Could not find option: Tabela przestawna");
            rtb_item01.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void MapRefresh()
        {
            OpenObject(mapName);
            RadToolBar rtb = GetToolBar();
                RadToolBarItem rtb_item01 = rtb.RootItems[1];
                Assert.IsNotNull(rtb_item01);
                rtb_item01.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void MapToXLSX2()//trzeba popracowac na innymi przegladarkami
        {
            OpenObject(mapName);
            DownloadFileFromWeb("map.xlsx", DownloadFileTypes.XLSX, GetToolBar());
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
        }

        [TestMethod]
        public void MapToXLS2()//trzeba popracowac na innymi przegladarkami
        {
            OpenObject(mapName);
            DownloadFileFromWeb("map.xls", DownloadFileTypes.XLS, GetToolBar());
        }

        [TestMethod]
        public void MapToInvPNG2()//trzeba popracowac na innymi przegladarkami
        {

            OpenObject(mapName);
            DownloadFileFromWeb("map.png", DownloadFileTypes.INVPNG, GetToolBar());
        }

        [TestMethod]
        public void MapToPNG2()//trzeba popracowac na innymi przegladarkami
        {
            OpenObject(mapName);
            DownloadFileFromWeb("map.png", DownloadFileTypes.PNG, GetToolBar());  
        }
        
        [TestMethod]
        public void MapToCSV2()//trzeba popracowac na innymi przegladarkami
        {
            OpenObject(mapName);
            DownloadFileFromWeb("map.csv", DownloadFileTypes.CSV, GetToolBar());
        }

        [TestMethod]
        public void MapChangeMap()
        {
            OpenObject(mapName);
          //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_visualisationc_contextMenu_detached"), 5000);
           ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_visualisationc_contextMenu_detached");    
            RadMenu context_menu = Find.ById<RadMenu>("Mainc_BOCc_visualisationc_contextMenu_detached");
                Assert.IsNotNull(context_menu);

                RadMenuItem menu_item = context_menu.FindItemByText("Zmień mapę");

                // menu_item.Wait.ForVisible();
                HtmlImage img = Find.ById<HtmlImage>("ImageX2Y2");
                Assert.IsNotNull(img);

                //img.MouseClick(MouseClickType.RightDown);

                Assert.IsNotNull(menu_item);
                // Wait.For<RadMenuItem>(testItem => testItem.Visible == true, menu_item, 5000);
                menu_item.Wait.ForExists();
                //menu_item.Wait.ForVisible();
                menu_item.Open();
              //  menu_item.Focus();
                //menu_item.MouseHover();
                //RadMenuItem mapa1 = menu_item.FindItemByText("World Cities");
                //RadMenuItem mapa1 = menu_item.Find.ByContent<RadMenuItem>("North America"); ;
                RadMenuItem mapa1 = menu_item.FindItemByText("Germany");
               
                Assert.IsNotNull(mapa1);
               // mapa1.Focus();
                mapa1.Click();
               /* RadMenuItem mapa2 = mapa1.FindItemByText("North America");
                Assert.IsNotNull(mapa2);
                mapa2.Click();*/
                Thread.Sleep(4000);
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Desktop.Mouse.Click(MouseClickType.RightClick, new Point(0, 0));
           
               
        }

        [TestMethod]
        public void MapDrillMap()
        {
            OpenObject(mapName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_visualisationc_contextMenu_detached"),5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_visualisationc_contextMenu_detached");
            RadMenu context_menu = Find.ById<RadMenu>("Mainc_BOCc_visualisationc_contextMenu_detached");
                Assert.IsNotNull(context_menu);

                RadMenuItem menu_item = context_menu.FindItemByText("Drąż wszystkie do następnego poziomu");

                HtmlImage img = Find.ById<HtmlImage>("ImageX2Y2");
                Assert.IsNotNull(img);
                img.MouseHover();
                img.MouseClick();

                menu_item.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                menu_item = context_menu.FindItemByText("Drąż wszystkie w górę");
                Assert.IsNotNull(menu_item);
                menu_item.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

                Desktop.Mouse.Click(MouseClickType.RightClick, new Point(0, 0));
           
        }

        [TestMethod]
        public void MapGoToMapRegion()
        {
            OpenObject(mapName);

            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_visualisationc_contextMenu_detached"),5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_visualisationc_contextMenu_detached");   
            RadMenu context_menu = Find.ById<RadMenu>("Mainc_BOCc_visualisationc_contextMenu_detached");
                Assert.IsNotNull(context_menu);

                RadMenuItem menu_item = context_menu.FindItemByText("Przejdź do tego regionu");

                HtmlImage img = Find.ById<HtmlImage>("ImageX3Y1");
                Assert.IsNotNull(img);
                img.MouseHover();
                //img.MouseClick(MouseClickType.LeftDoubleClick, img.GetRectangle().X + img.GetRectangle().Width / 3, img.GetRectangle().Y + img.GetRectangle().Height / 3);

                menu_item.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                //menu_item = context_menu.FindItemByText("Wróæ do poprzedniej mapy");
                //Assert.IsNotNull(menu_item);
                //menu_item.Click();
                Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
                Desktop.Mouse.Click(MouseClickType.RightClick, new Point(0, 0));
           
            
        }

        #endregion
    }
}
