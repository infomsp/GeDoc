<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<script type="text/javascript">
    var idMenu;
    var _UltimoMenuRef;
    var _UltimoMenuItemURL = "";

    $("#divMenuPrincipal").focusout(function () {
        CerrarMenu();
    })
    $("#divMenuPrincipal").blur(function () {
        CerrarMenu();
    })

    function OnSelected(e) {
        var _item = $("a", e.item)[0].href;
        var _Ref = $("a", e.item)[0];
        var menu = $("#mnuPrincipal").data("tMenu");
        if ((_UltimoMenuRef != null) && (_UltimoMenuItemURL != _item) && (_item.indexOf("/Home/Index") == -1)) {
            jQuery(_UltimoMenuRef).attr("href", _UltimoMenuItemURL);
        }
        if (_item.indexOf("/Home/Index") != -1 || _item == "") {
            //e.preventDefault();
            //e.stopPropagation();
            jQuery(_Ref).removeAttr("href");
            return;
        }
        _UltimoMenuItemURL = _item;
        _UltimoMenuRef = _Ref;
        jQuery(_Ref).removeAttr("href");

        CerrarMenu();
        AbrirWaiting();
        onCargaMenuFinalizada = null;
        $('#divTituloCatalogos').html(e.item.textContent);
        $('#divTituloCatalogos').attr('style', 'width: 98.9%; text-align: center; font-size: x-large; font-weight: bold; display: block;');
        $.post(_item
            , null
            , function (data) {
                //chequear opcionalemnte el tamaño de la respuesta
                if (data.length > 0) {
                    //pegar en div principal
                    $('#divContenidoPrincipal').html(data);
                    $('#divContenidoPrincipal').attr('style', 'display: block; outline: none;');
                    $('#divHome').attr('style', 'display: none;');
                    $('#divHome').empty();
                    CerrarWaiting();
                    if (onCargaMenuFinalizada != null) {
                        onCargaMenuFinalizada();
                    }
                }
            });
    }
    function CerrarMenu() {
        //Cerramos el Menú
        var menu = $("#mnuPrincipal").data("tMenu");
        $("> li", menu.element).each(function (_Element) {
            menu.close(menu.element.childNodes[_Element]);
        });
    }
</script>
<%  IEnumerable<Telerik.Web.Mvc.UI.MenuItem> _Item;
    if ((Session["MenuPrincipal"] as List<Telerik.Web.Mvc.UI.MenuItem>).Count == 0)
    {
        var _Menu = new GeDoc.MenuPrincipalController();
        _Item = _Menu.getMenu((Session["Usuario"] as GeDoc.Models.sisUsuario));
        Session["MenuPrincipal"] = _Item.ToList();
    }
    else
    {
        _Item = (Session["MenuPrincipal"] as List<Telerik.Web.Mvc.UI.MenuItem>).AsEnumerable();
    }
%>
<div id="divMenuPrincipal" tabindex="15" style="outline: none;">
<%: Html.Telerik().Menu()
        .Name("mnuPrincipal")
        .ClientEvents(eventos => eventos.OnSelect("OnSelected"))
        .OpenOnClick(true)
        //.Effects(w => w.Opacity())
        .BindTo(_Item)
%>
</div>