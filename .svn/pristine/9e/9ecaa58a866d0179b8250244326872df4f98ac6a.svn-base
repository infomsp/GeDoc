<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%  IEnumerable<Telerik.Web.Mvc.UI.TreeViewItemModel> _Item;
    var _Idiomas = new GeDoc.catPersonaController();
    _Item = _Idiomas.getDatosIdiomas();
%>
<%: Html.Telerik().TreeView()
                    .Name("tvIdiomas")
                    .ShowCheckBox(true)
                    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingIdiomas"))
                    .BindTo(_Item)
%>
