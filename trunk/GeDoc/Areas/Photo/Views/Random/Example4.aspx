<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>



<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
 
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h2>Example 4: Slideshow using ASPX View Engine and Masterpage</h2>


<%: Html.Partial("YsaUserControls/_YsaSS", new GeDoc.Models.YsaSSModel(xmlSource: "/xml/500x281.xml", 
                            width: 500, height:281, xpath: "site500x281", 
                            btnPauseImagePath:"/Content/YsaSS-images/pause2.png",
                                      btnNextImagePath: "/Content/YsaSS-images/forward.png",
                                 btnPlayImagePath: "/Content/YsaSS-images/play2.png",
                                      btnPreviousImagePath: "/Content/YsaSS-images/backward.png"
                                 )) %>

<script src="/Scripts/simplegallery.js" type="text/javascript"></script>

</asp:Content>
