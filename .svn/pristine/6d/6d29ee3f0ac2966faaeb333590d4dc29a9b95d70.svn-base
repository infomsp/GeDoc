using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text;

namespace GeDoc.Models
{
    public class YsaSSModel
    {
        /// <summary>
        /// div id
        /// </summary>
        private string _wrapperID = YsaSSHelper.GetNextId();
        public string WrapperID
        {
            get
            {
                return "ysaSlideShowDiv" + _wrapperID;
            }
        }

        /// <summary>
        /// slideshow container
        /// </summary>
        public string SSContainer { get; set; }

        /// <summary>
        /// slideshow script
        /// </summary>
        public string SSScript { get; set; }

        /// <summary>
        /// xml path
        /// </summary>
        public string XPath { get; set; }

        /// <summary>
        /// xml source
        /// </summary>
        public string XMLSource { get; set; }

        /// <summary>
        /// div width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// div height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// autoplay true|false
        /// </summary>
        public bool AutoPlay { get; set; }

        /// <summary>
        /// ShowNavigation true|false
        /// </summary>
        public bool ShowNavigation { get; set; }

        /// <summary>
        /// delay between slide in miliseconds
        /// </summary>
        public int Delay_btw_slide { get; set; }

        /// <summary>
        /// transition duration (milliseconds)
        /// </summary>
        public int FadeDuration { get; set; }

        /// <summary>
        /// cycles befote stopping
        /// </summary>
        public int Cycles_before_stopping { get; set; }

        /// <summary>
        /// previous button
        /// </summary>
        public string BtnPreviousImagePath { get; set; }

        /// <summary>
        /// next button
        /// </summary>
        public string BtnNextImagePath { get; set; }

        /// <summary>
        /// play button
        /// </summary>
        public string BtnPlayImagePath { get; set; }

        /// <summary>
        /// pause button
        /// </summary>
        public string BtnPauseImagePath { get; set; }

        //constructor
        public YsaSSModel(string xpath = "site", string xmlSource = "/YSASS/sites.xml", 
            int width = 728, int height = 95,
            bool autoPlay = true, bool showNavigation = true, 
            int delay_btw_slide = 10000, int fadeDuration = 2000,
            int cycles_before_stopping = 99,
            string btnPreviousImagePath = "/Content/ysaSS-images/previous.gif",
            string btnNextImagePath = "/Content/ysaSS-images/next.gif",
            string btnPlayImagePath = "/Content/ysaSS-images/play.gif",
            string btnPauseImagePath = "/Content/ysaSS-images/pause.gif")
        {
            var Utiles = new UtilesController();
            btnPreviousImagePath = Utiles.GetPath("~/Content/ysaSS-images/previous.gif");
            btnNextImagePath = Utiles.GetPath("~/Content/ysaSS-images/next.gif");
            btnPlayImagePath = Utiles.GetPath("~/Content/ysaSS-images/play.gif");
            btnPauseImagePath = Utiles.GetPath("~/Content/ysaSS-images/pause.gif");

            XPath = xpath;
            XMLSource = xmlSource;
            Width = width;
            Height = height;
            AutoPlay = autoPlay;
            ShowNavigation = showNavigation;
            Delay_btw_slide = delay_btw_slide;
            FadeDuration = fadeDuration;
            Cycles_before_stopping = cycles_before_stopping;
            BtnPreviousImagePath = btnPreviousImagePath;
            BtnNextImagePath = btnNextImagePath;
            BtnPlayImagePath = btnPlayImagePath;
            BtnPauseImagePath = btnPauseImagePath;
            SSContainer = YsaSSHelper.CreateDiv(WrapperID);
            SSScript = YsaSSHelper.CreateScript(WrapperID, xmlSource, xpath, width, height, btnPreviousImagePath,
                btnPlayImagePath, btnNextImagePath, btnPauseImagePath, showNavigation, autoPlay,
                delay_btw_slide, cycles_before_stopping, fadeDuration);
        }
    }

    //helper class
    public static class YsaSSHelper
    {
        public static int ID = 0;


        //id
        public static string GetNextId()
        {
            ID++;
            return ID.ToString();
        }

        //create html div
        public static string CreateDiv(string wrapperID)
        {
            StringBuilder sbDiv = new StringBuilder(string.Empty);
            sbDiv.Append("<div id='" + wrapperID + "' style='");
            sbDiv.Append(" background: none repeat scroll 0% 0% white;");
            sbDiv.Append(" overflow: hidden;");
            sbDiv.Append(" position: relative;");
            sbDiv.Append(" visibility: visible;");
            sbDiv.Append(" -moz-background-clip: border;");
            sbDiv.Append(" -moz-background-origin: padding;");
            sbDiv.Append(" -moz-background-inline-policy: continuous;");
            sbDiv.Append("'></div>");

            return sbDiv.ToString();
        }

        //create script
        public static string CreateScript(string wrapperID, string xmlSource, string xpath, int width,
            int height, string btnPreviousImagePath, string btnPlayImagePath, string btnNextImagePath,
            string btnPauseImagePath, bool showNavigation, bool autoPlay, int delay_btw_slide,
            int cycles_before_stopping, int fadeDuration)
        {
            StringBuilder ssScript = new StringBuilder(string.Empty);
            string arrName = "ysaMyArray" + wrapperID;

            ssScript.Append("<script type='text/javascript'>");
            ssScript.Append("var " + arrName + "= [];");
            ssScript.Append("$(document).ready(function() {");
            ssScript.Append(" $.ajax({");
            ssScript.Append("type: \"GET\",");
            ssScript.Append("url: '" + xmlSource + "',");
            ssScript.Append("cache: true,");
            ssScript.Append("dataType: \"xml\",");
            ssScript.Append("success: function(xml) {");
            ssScript.Append("var count = 0;");
            ssScript.Append("$(xml).find('" + xpath + "').each(function() {");

            ssScript.Append(" var url = $(this).find('url').text();");
            ssScript.Append("var target = $(this).find('target').text();");
            ssScript.Append("var imageURL = $(this).find('imageURL').text();");
            ssScript.Append("var alt = $(this).find('alt').text();");

            ssScript.Append(arrName + "[parseInt(count)] = new Array(imageURL, url, target, alt); ");
            ssScript.Append("count++;");
            ssScript.Append("});");

            ssScript.Append(" var mygallery" + wrapperID + " = new simpleGallery({");
            ssScript.Append(" wrapperid: '" + wrapperID + "',");
            ssScript.Append("dimensions: [" + width.ToString() + "," + height.ToString() + "],"); //width/height of gallery in pixels. Should reflect dimensions of the images exactly
            ssScript.Append("imagearray: " + arrName + ","); //array of images
            ssScript.Append("navimages: ['" + btnPreviousImagePath + "', '" +
                btnPlayImagePath + "', '" +
                btnNextImagePath + "', '" +
                btnPauseImagePath + "'],");
            ssScript.Append("showpanel: '" + showNavigation.ToString().ToLower() + "',");
            ssScript.Append(" autoplay: [" + autoPlay.ToString().ToLower() + "," + delay_btw_slide.ToString() + "," 
                + cycles_before_stopping.ToString() + "],"); //[auto_play_boolean, delay_btw_slide_millisec, cycles_before_stopping_int]
            ssScript.Append(" persist: true,");
            ssScript.Append(" fadeduration:" + fadeDuration.ToString() + ","); //transition duration (milliseconds)
            ssScript.Append(" oninit: function() {"); //event that fires when gallery has initialized/ ready to run
            ssScript.Append("  },");
            ssScript.Append("  onslide: function(curslide, i) {"); //event that fires after each slide is shown
            //curslide: returns DOM reference to current slide's DIV (ie: try alert(curslide.innerHTML)
            //i: integer reflecting current image within collection being shown (0=1st image, 1=2nd etc)
            ssScript.Append("   }");
            ssScript.Append("  })");
            ssScript.Append("  }");
            ssScript.Append("   });");

            ssScript.Append(" });");
            ssScript.Append("</script>");

            return ssScript.ToString();
        }

    }

}