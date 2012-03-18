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
using System.Drawing;
using ArtOfTest.WebAii.Silverlight;
using ArtOfTest.WebAii.Silverlight.UI;
using BITestAdamLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TEST_29072011
{
    /// <summary>
    /// Summary description for TelerikVSUnitTest1
    /// </summary>
    [TestClass]
    public class Pivot_Tests : BaseTestClass
    {
        [TestMethod]
        [Ignore]
        public void LogIN_without_username()
        {
            HtmlSpan span=Find.ById<HtmlSpan>("Login1_UserNameRequired");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.InnerText.Equals("*"));
            span = Find.ById<HtmlSpan>("Login1_PasswordRequired");
            Assert.IsNotNull(span);
            Assert.IsTrue(span.InnerText.Equals("*"));
        }

        [TestMethod]
        public void Open_pivot_table()
        {
            OpenObject(pivotName);
            Browser pivot_frame = GetPivotFrame();
            Assert.IsNotNull(pivot_frame);
            pivot_frame.Actions.WaitForElements(5000, new FindParam[] { new FindParam("id=olapgrid_IG") });
            Assert.IsNotNull(pivot_frame.Find.ById("olapgrid_IG"));
            Assert.IsTrue(pivot_frame.ContainsText("Bikes"));
        }

        [TestMethod]
        [Description("Test doesnt work on IE")]
        public void Pivot_drill() 
        {
            OpenObject(pivotName);
            Browser pivot_frame = GetPivotFrame();
            pivot_frame.WaitForElement(5000,"class=aspnet_s7");
            HtmlImage driller = pivot_frame.Find.ByXPath<HtmlImage>("//table[@id='olapgrid_FTABLE']/tbody/tr[5]/td[1][@class='aspnet_s7']/table/tbody/tr/td[1]/img");
           
            Assert.IsNotNull(driller);
            Assert.IsFalse(pivot_frame.ContainsText("Shorts"));
            driller.Click();
            
            ActiveBrowser.Frames.RefreshAllDomTrees();
            driller.Click();
            Thread.Sleep(5000);
            Assert.IsTrue(pivot_frame.ContainsText("Shorts"));
        }

        [TestMethod]//nie dziala pod IE
        public void pivot_drill_by_context_menu()
        {
            OpenObject(pivotName);
            Browser pivot_frame = GetPivotFrame();
            HtmlTableCell cell = pivot_frame.Find.ByXPath<HtmlTableCell>("//table[@id='olapgrid_FTABLE']/tbody/tr[4]/td[1][@class='aspnet_s8']");
            Assert.IsNotNull(cell);
            
            cell.MouseHover();
            cell.MouseClick(MouseClickType.RightClick);
            Thread.Sleep(3000);
            pivot_frame.WaitForElement(5000,"id=olapgrid_rsPopup");
            HtmlDiv div = pivot_frame.Find.ById<HtmlDiv>("olapgrid_rsPopup");
            Assert.IsNotNull(div);
            div.Wait.ForExists();
            div.Wait.ForVisible();
            div.MouseHover();
            Desktop.Mouse.Click(MouseClickType.LeftClick, new System.Drawing.Point(div.GetRectangle().X+div.GetRectangle().Width/2,
                    div.GetRectangle().Y+div.GetRectangle().Height/20
               ));

            Thread.Sleep(3000);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(pivot_frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void PivotToChart_IMGCompare()
        {
            OpenObject(pivotName);
            Browser pivot_frame = GetPivotFrame();
            Assert.IsNotNull(pivot_frame);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.FindItemByText("Wykres");
            Assert.IsNotNull(rtb_item01);
            rtb_item01.Click();

            string filename = ConvertNameDependingOnBrowser("pivot_to_chart.png");
            IMGCompare(filename, "Mainc_BOCc_visualisation");
        }

        [TestMethod]
        public void PivotToMap_IMGCompare()
        {
            OpenObject(pivotName);
          
            Browser pivot_frame = GetPivotFrame(); 
            Assert.IsNotNull(pivot_frame);
            RadToolBar rtb = GetToolBar();
            RadToolBarItem rtb_item01 = rtb.FindItemByText("Mapa");
            Assert.IsNotNull(rtb_item01);
            rtb_item01.Click();
            string filename = ConvertNameDependingOnBrowser("pivot_to_map.png");
            IMGCompare(filename, "Mainc_BOCc_visualisation");
            
        }

        [TestMethod]
        public void PivotToXLS2()
        {
            OpenObject(pivotName);
            Browser pivot_frame = GetPivotFrame();
            DownloadFileFromWeb("pivot.xls", DownloadFileTypes.XLS, GetToolBar());
        }

        [TestMethod]
        public void PivotFilter_JavaScript()
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            frame.Window.SetActive();
            frame.Window.SetFocus();
            frame.Actions.InvokeScript("javascript:{olapgrid_manager.showDialog('showfiltersettings|[Product].[Product Categories].[Category]|ftOnCaption|fcContains')}");
            frame.Actions.WaitForElement(new FindParam("id=ODLG_tbFirst"), 5000);
            frame.Find.ById<HtmlInputText>("ODLG_tbFirst").Text = "Bikes";
            Thread.Sleep(3000);
            frame.Actions.InvokeScript("olapgrid_manager.applyContextFilter('cfilter|[Product].[Product Categories].[Category]|ftOnCaption|fcContains')");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."));
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."));  
        }

        [TestMethod]
        public void PivotSort_JavaScript()
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            frame.Window.SetActive();
            frame.Window.SetFocus();
            frame.Actions.InvokeScript("javascript:{olapgrid_manager.executeMenuAction('valuesort|3|0')}");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void Pivot_TimeInteligence()
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            frame.Window.SetActive();
            frame.Window.SetFocus();
            frame.Actions.InvokeScript("javascript:{olapgrid_manager.executeMenuAction('addintelligence|4|[Date].[Calendar]|[Measures].[Internet Total Product Cost]')}");
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        
        }

        [TestMethod]
        [Description("Firefox only")]
        public void Pivot_ChangeNumberFormat()
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            frame.Window.SetActive();
            frame.Window.SetFocus();
            frame.Actions.InvokeScript("javascript:{__doPostBack('PivotTable$Grid','popupselected|FormatNumbersFormatNumbers_Thousands')}");

            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        [TestMethod]
        public void Pivot_AddMeasure()//ff chrome
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            HtmlInputCheckBox checkbox = frame.Find.ByXPath<HtmlInputCheckBox>("//span[@id='PivotTable_Grid_ctl00t2' and @class='PivotTable_Grid_ctl00_0 PivotTable_Grid_ctl00_1']/input");
            HtmlSpan span=frame.Find.ByXPath<HtmlSpan>("//span[@id='PivotTable_Grid_ctl00t2' and @class='PivotTable_Grid_ctl00_0 PivotTable_Grid_ctl00_1']/span");
            Assert.IsNotNull(span);
           
            Assert.IsNotNull(checkbox);
            checkbox.InvokeEvent(ScriptEventType.OnClick);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."));
            frame.Refresh();
            frame.WaitUntilReady();
            frame.WaitForElement(5000, "XPath=//table[@id='olapgrid_FTABLE']/tbody/tr[2]/td[3][@class='aspnet_s7']/table/tbody/tr/td[2]");
            HtmlTableCell cell = frame.Find.ByXPath<HtmlTableCell>("//table[@id='olapgrid_FTABLE']/tbody/tr[2]/td[3][@class='aspnet_s7']/table/tbody/tr/td[2]");
            
            Assert.IsNotNull(cell);
            Assert.IsTrue(cell.InnerText == span.InnerText);
            Thread.Sleep(3000);
        }//ff and chrome only

        [TestMethod]//ff chrome IE nie ma liscie
        public void Pivot_FilterCancel()
        {
            OpenObject(pivotName);
            Browser frame = GetPivotFrame();
            HtmlImage img = frame.Find.ByXPath<HtmlImage>("//td[@id='pivot_columnarea']/table/tbody/tr[1]/td/table[@class='aspnet_s14']/tbody/tr/td[2]/img");
            Assert.IsNotNull(img);
            img.InvokeEvent(ScriptEventType.OnClick);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

            frame.WaitForElement(3000,"id=heditor_TREE");

            HtmlInputButton cancel = frame.Find.ById<HtmlInputButton>("btnCancelFilter");

            cancel.Click();
            Thread.Sleep(3000);
            Assert.IsFalse(ActiveBrowser.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");
            Assert.IsFalse(frame.ContainsText("Application encountered an unexpected error. We are very sorry for any inconvenience."), "BinocleWeb Crashed !");

        }

        private Browser GetPivotFrame()
        {
            FrameInfo f = new FrameInfo();
            f.Id = "Mainc_BOCc_ptf";
            return ActiveBrowser.WaitForFrame(f, 15000);
        }


    }
}
