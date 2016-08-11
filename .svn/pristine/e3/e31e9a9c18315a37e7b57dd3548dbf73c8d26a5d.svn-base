<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<%  IEnumerable<Telerik.Web.Mvc.UI.TreeViewItemModel> _Item;
    var _Menu = new GeDoc.sisUsuarioController();
    int _usrId = 1;
    if (Session["UsuarioId"] != null)
    {
        _usrId = (int)Session["UsuarioId"];
    }
    _Item = _Menu.getPermisos(_usrId);
%>
<%: Html.Telerik().TreeView()
                    .Name("tvPermisos")
                    .ShowCheckBox(true)
                    .ClientEvents(clientEvents => clientEvents.OnDataBinding("onDataBindingPermisos"))
                    .BindTo(_Item)
%>
