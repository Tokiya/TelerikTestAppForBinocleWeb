using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using BITestAdamLib;
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

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for Issues_Tests
    /// </summary>
    [TestClass]
    public class Issues_Tests : BaseTestClass
    {

        [TestMethod]
        [Description("issue 2571")]
        public void MapNavigationZoomPanels()
        {
            OpenObject(mapName);
            // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc$BOCc$visualisationc$MapZoomPanel"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc$BOCc$visualisationc$MapZoomPanel");
            //HtmlImage zoom = Find.ById<HtmlImage>("Mainc$BOCc$visualisationc$MapZoomPanel");
            // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc$BOCc$visualisationc$MapNavigationPanel"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc$BOCc$visualisationc$MapNavigationPanel");
            HtmlDiv zoom = Find.ById<HtmlDiv>("Mainc$BOCc$visualisationc$MapZoomPanelDiv");
            // HtmlImage zoom = Find.ByXPath<HtmlImage>("//*[@id=\"Mainc$BOCc$visualisationc$MapNavigationPanel\"]");
            Assert.IsNotNull(zoom);


            for (int i = 0; i < 2; i++)
            {
                Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(zoom.GetRectangle().X + zoom.GetRectangle().Width / 2,
                          zoom.GetRectangle().Y + zoom.GetRectangle().Height / 24
                     ));

                Thread.Sleep(3000);
            }
            // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc$BOCc$visualisationc$MapNavigationPanel"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc$BOCc$visualisationc$MapNavigationPanel");

            // HtmlImage navig = Find.ById<HtmlImage>("Mainc$BOCc$visualisationc$MapNavigationPanel");
            HtmlDiv navig = Find.ById<HtmlDiv>("Mainc$BOCc$visualisationc$MapNavigationPanelDiv");
            Assert.IsNotNull(navig);
            for (int i = 0; i < 2; i++)
            {
                Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(navig.GetRectangle().X + navig.GetRectangle().Width / 3,
                          navig.GetRectangle().Y + navig.GetRectangle().Height / 2
                     ));

                Thread.Sleep(3000);
            }
            string filename = "map_nz";
            Bitmap bmp = ActiveBrowser.Window.GetBitmap(Find.ById<HtmlDiv>("Mainc$BOCc$visualisationc$Map").GetRectangle());
            //bmp.Save(dir+filename);

            Bitmap bmp3 = new Bitmap(dir + filename);
            //Assert.IsTrue(ImagesHandler.CompareImage(bmp, bmp2, 0.01, 0.1) < 0.1);
            Assert.IsTrue(ImagesHandler.CompareImage(bmp, bmp3, 0.01, 0.1) < 0.1);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");


        }

        [TestMethod]
        [Ignore]
        public void LongTitlesInTooltip()
        {
            OpenObject(dboardName);

        }

        [TestMethod]
        [Description("issue 2533 ")]
        public void PresentationWithFavourite()
        {
            OpenObject("TreeObject.BusinessObject.147");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            ActiveBrowser.WaitForElement(15000, "id=Mainc$BOCc$BOCc$1dsitv1$DIc$BOCc$visualisationc$MapContentFiller");
            OpenObject("TreeObject.BusinessObject.147");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            ActiveBrowser.WaitForElement(15000, "id=Mainc$BOCc$BOCc$1dsitv1$DIc$BOCc$visualisationc$MapContentFiller");

        }

        [TestMethod]
        [Description("FireFox only")]
        public void Iss2527PivotColorAfterFormatChange()
        {
            OpenObject(pivotName);
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Mainc_BOCc_ptf"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Mainc_BOCc_ptf");
            FrameInfo f = new FrameInfo();
            f.Id = "Mainc_BOCc_ptf";
            ActiveBrowser.WaitForFrame(f, 15000);
            ActiveBrowser.Frames.RefreshAllDomTrees();
            ActiveBrowser.Frames.WaitAllUntilReady();
            Browser frame = ActiveBrowser.Frames[0];
            Assert.IsNotNull(frame);
            frame.WaitForElement(5000, "id=PivotTable_Grid");
            HtmlDiv div = frame.Find.ById<HtmlDiv>("PivotTable_Grid");
            Assert.IsNotNull(div);
            /*
            HtmlAnchor a = frame.Find.ByXPath<HtmlAnchor>("//*[@id=\"PivotTable_Grid_olapgrid_menun38\"]/td/table/tbody/tr/td/a");
            Assert.IsNull(a);

            HtmlTableCell cell = frame.Find.ByXPath<HtmlTableCell>("//*[@id=\"olapgrid_FTABLE\"]/tbody/tr[4]/td");
            Assert.IsNotNull(cell);

            cell.MouseClick(MouseClickType.RightClick);
            Thread.Sleep(3000);
            Assert.IsNull(a);
            */
            HtmlStyle style = div.GetStyle("backgroundColor");

            System.Drawing.Color color = style.ToColor();
            int r = color.R, b = color.B, g = color.G;
            Assert.IsTrue(style.ToColor().R == r && b == style.ToColor().B && g == style.ToColor().G);
            frame.Window.SetActive();
            frame.Window.SetFocus();

            frame.Actions.InvokeScript("javascript:__doPostBack('PivotTable$Grid','popupselected|FormatNumbersFormatNumbers_Percentage')");
            Thread.Sleep(5000);
            Assert.IsTrue(style.ToColor().R == r && b == style.ToColor().B && g == style.ToColor().G);



        }

        [TestMethod]
        public void SetStartingreport()
        {

            #region otworz panel boczny
            ActiveBrowser.Window.SetFocus();
            //Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1"), 10000);
            ActiveBrowser.WaitForElement(10000, "id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1");
            //Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();


            Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().X,
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().Y
           ));


            //Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();
            try
            {
                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            catch
            {
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();

                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            #endregion



            HtmlSpan chart = Find.ById<HtmlSpan>(chartName);

            RadMenu menu = Find.ById<RadMenu>("NavPaneAjaxc_tvBusinessObjects_NavPanel_CtxMenu_detached");
            Assert.IsNotNull(menu);
            chart.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            RadMenuItem item = menu.FindItemByText("Mój raport startowy");
            item.Focus();
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            Thread.Sleep(3000);
            Actions.Click(base.Elements["logout"]);
            // ActiveBrowser.Actions.WaitForElement(new FindParam("id=Login1_UserName"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Login1_UserName");
            Actions.Click(base.Elements["login_lang"]);
            Find.ByContent<HtmlListItem>("Polski").Click();
            Actions.SetText(base.Elements["login"], "test");
            Actions.SetText(base.Elements["password"], "1qaz2WSX");
            Actions.Click(base.Elements["login_btn"]);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            ActiveBrowser.Actions.WaitForElement(new FindParam("id=BOTitle"), 15000);
            Assert.IsTrue(Find.ById<HtmlSpan>("BOTitle").InnerText == chart.InnerText);

            #region otworz panel boczny
            ActiveBrowser.Window.SetFocus();
            //Actions.WaitForElement(new FindParam("id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1"), 10000);
            ActiveBrowser.WaitForElement(10000, "id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1");
            //Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();


            Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().X,
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().Y
           ));


            //Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();
            try
            {
                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            catch
            {
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();

                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            #endregion

            chart = Find.ById<HtmlSpan>(chartName);

            chart.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            item = menu.FindItemByText("Brak raportu startowego");
            item.Focus();
            item.Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            Thread.Sleep(3000);
            Actions.Click(base.Elements["logout"]);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=Login1_UserName"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=Login1_UserName");
            Actions.Click(base.Elements["login_lang"]);
            Find.ByContent<HtmlListItem>("Polski").Click();
            Actions.SetText(base.Elements["login"], "test");
            Actions.SetText(base.Elements["password"], "1qaz2WSX");
            Actions.Click(base.Elements["login_btn"]);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            //ActiveBrowser.Actions.WaitForElement(new FindParam("id=BOTitle"), 5000);
            ActiveBrowser.WaitForElement(5000, "id=BOTitle");
            Assert.IsTrue(Find.ById<HtmlSpan>("BOTitle").InnerText == "");

        }

        [TestMethod]
        public void AddNewFolderToFavourities_ConfirmByClick()
        {
            #region otworz panel boczny
            ActiveBrowser.Window.SetFocus();
            ActiveBrowser.WaitForElement(10000, "id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1");
            Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().X,
               Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().Y
          ));

            try
            {
                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            catch
            {
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();

                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            #endregion
            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            RadTreeView tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsNotNull(tree);
            int count = tree.AllNodes.Count;

            HtmlSpan chart = Find.ById<HtmlSpan>(chartName);

            RadMenu menu = Find.ById<RadMenu>("NavPaneAjaxc_tvBusinessObjects_NavPanel_CtxMenu_detached");
            Assert.IsNotNull(menu);
            chart.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");

            RadMenuItem item = menu.FindItemByText("Dodaj do nowej grupy");
            item.Focus();
            item.Click();
            string s = "grupa"+DateTime.Now.ToShortTimeString();
            HtmlInputText text = Find.ById<HtmlInputText>("NavPaneAjaxc_tvCreateNewFavouritesAndAddTo_C_tvCreateNewFavouritesAndAddToNewDialogText");
            Desktop.Mouse.Click(MouseClickType.LeftClick, text.GetRectangle());
            Desktop.KeyBoard.TypeText(s, 1);
            //Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);
            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvCreateNewFavouritesAndAddTo_C");
            Find.ByXPath<HtmlInputButton>("//div[@id='NavPaneAjaxc_tvCreateNewFavouritesAndAddTo_C']/div[2]/input[1]").Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");
            ActiveBrowser.WaitForElement(5000, "id=BOTitle");
            HtmlSpan titleSpan = Find.ById<HtmlSpan>("BOTitle");
            Assert.IsNotNull(titleSpan);
            Assert.IsTrue(titleSpan.InnerText == "", "Search action is performed instead of creating new favourite folder");
            
            ActiveBrowser.WaitForElement(25000, "id=NavPaneAjaxc_tvBusinessObjects");
            tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsNotNull(tree);
            tree.Refresh();
            ActiveBrowser.WaitUntilReady();
           
            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");

            tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsFalse(count == tree.AllNodes.Count, "New favourite folder could not be added.");
           // tree.Wait.ForVisible(5000);
            Assert.IsNotNull(tree);
            tree.FindNodeByText(s).Wait.ForVisible(5000);
            Assert.IsNotNull(tree.FindNodeByText(s));
            RadTreeNode node1 = tree.FindNodeByText(s);
            Assert.IsNotNull(node1);
            Assert.IsTrue(node1.Nodes.Count == 1);
            RadTreeNode node2 = node1.Nodes[0];
            menu = Find.ById<RadMenu>("NavPaneAjaxc_tvBusinessObjects_NavPanel_CtxMenu_detached");
            node2.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            item = menu.FindItemByText("Usuń");
            Assert.IsNotNull(item);
            item.Focus();
            item.Click();
            Thread.Sleep(3000);
            //Assert.IsTrue(node1.Nodes.Count==0);

            node1.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            item = menu.FindItemByText("Usuń");
            Assert.IsNotNull(item);
            item.Focus();
            item.Click();
            RadTreeNode node3 = null;

            try
            {
                node3 = tree.FindNodeByText(s);
            }
            catch { }

            Assert.IsNull(node3);
            Actions.Click(this.Elements["logout"]);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");

        }

        [TestMethod]
        public void AddNewFolderToFavourities_ConfirmByENTER_issue2741()
        {
            #region otworz panel boczny
            ActiveBrowser.Window.SetFocus();
            ActiveBrowser.WaitForElement(10000, "id=RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1");
            Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().X,
               Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").GetRectangle().Y
          ));

            try
            {
                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            catch
            {
                Find.ById<HtmlInputButton>("RAD_SPLITTER_BAR_COLLAPSE_Forward_RadSplitBar1").MouseClick();

                ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            }
            #endregion
            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            RadTreeView tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsNotNull(tree);
            int count = tree.AllNodes.Count;

            HtmlSpan chart = Find.ById<HtmlSpan>(chartName);

            RadMenu menu = Find.ById<RadMenu>("NavPaneAjaxc_tvBusinessObjects_NavPanel_CtxMenu_detached");
            Assert.IsNotNull(menu);
            chart.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");

            RadMenuItem item = menu.FindItemByText("Dodaj do nowej grupy");
            item.Focus();
            item.Click();
            string s = "grupa"+DateTime.Now;
            HtmlInputText text = Find.ById<HtmlInputText>("NavPaneAjaxc_tvCreateNewFavouritesAndAddTo_C_tvCreateNewFavouritesAndAddToNewDialogText");
            Desktop.Mouse.Click(MouseClickType.LeftClick, text.GetRectangle());
            Desktop.KeyBoard.TypeText(s, 1);
            Desktop.KeyBoard.KeyPress(System.Windows.Forms.Keys.Enter);
            ActiveBrowser.WaitForElement(5000, "id=BOTitle");
            HtmlSpan titleSpan = Find.ById<HtmlSpan>("BOTitle");
            Assert.IsNotNull(titleSpan);
            Assert.IsTrue(titleSpan.InnerText=="","Search action is performed instead of creating new favourite folder");
           // Find.ByXPath<HtmlInputButton>("//div[@id='NavPaneAjaxc_tvCreateNewFavouritesAndAddTo_C']/div[2]/input[1]").Click();
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");
            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");
            tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsNotNull(tree);
            Thread.Sleep(3000);
            int count2 = tree.AllNodes.Count;
            Assert.IsFalse(count == count2,"New favourite folder could not be added");

            ActiveBrowser.WaitForElement(5000, "id=NavPaneAjaxc_tvBusinessObjects");

            tree = Find.ById<RadTreeView>("NavPaneAjaxc_tvBusinessObjects");
            Assert.IsNotNull(tree);
            tree.FindNodeByText(s).Wait.ForExists();
            Assert.IsNotNull(tree.FindNodeByText(s));
            RadTreeNode node1 = tree.FindNodeByText(s);
            Assert.IsNotNull(node1);
            Assert.IsTrue(node1.Nodes.Count == 1);
            RadTreeNode node2 = node1.Nodes[0];
            menu = Find.ById<RadMenu>("NavPaneAjaxc_tvBusinessObjects_NavPanel_CtxMenu_detached");
            node2.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            item = menu.FindItemByText("Usuń");
            Assert.IsNotNull(item);
            item.Focus();
            item.Click();
            Thread.Sleep(3000);
            //Assert.IsTrue(node1.Nodes.Count==0);

            node1.MouseClick(MouseClickType.RightClick);
            menu.Wait.ForExists();
            menu.Wait.ForVisible();
            item = menu.FindItemByText("Usuń");
            Assert.IsNotNull(item);
            item.Focus();
            item.Click();
            RadTreeNode node3 = null;

            try
            {
                node3 = tree.FindNodeByText(s);
            }
            catch { }

            Assert.IsNull(node3);
            Actions.Click(this.Elements["logout"]);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !!");

        }

    }
}
