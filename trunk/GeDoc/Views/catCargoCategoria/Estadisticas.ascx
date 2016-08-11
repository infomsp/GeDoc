<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% Html.Telerik().Window()
        .Name("wEstadisticas")
        .Title("Estadísticas de Cargos...")
        .Visible(false)
        .Content(() =>
        {
            %>
            <div id="Estadística" style="border: 1px solid #DADBE9; width: auto; height: 100%">
                <div style="vertical-align: middle; height: 40px; margin-top: 5px;">
                    <%= Html.RadioButton("rbOpciones", "0", true)%>Sin Filtros
                    <%= Html.RadioButton("rbOpciones", "1", false)%>Agrupamiento:
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxAgrupamiento")
                        .DropDownHtmlAttributes(new { style = "width:100px;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })
                        .HighlightFirstMatch(true)
                        .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                        .SelectedIndex(0)
                        .HtmlAttributes(new { style = "width: 60px;" })
                        .BindTo((SelectList)ViewData["agrId_Data"])%>
                    <%= Html.RadioButton("rbOpciones", "2", false)%>Cargo:
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxCargo")
                        .DropDownHtmlAttributes(new { style = "width:Auto;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })
                        .HighlightFirstMatch(true)
                        .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                        .SelectedIndex(0)
                        .HtmlAttributes(new { style = "width: 150px;" })
                        .BindTo((SelectList)ViewData["carId_Data"])%>
                    <%= Html.RadioButton("rbOpciones", "3", false)%>Categoría:
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxCargoCategoria")
                        .DropDownHtmlAttributes(new { style = "width:Auto;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })                    
                        .HighlightFirstMatch(true)
                        .HtmlAttributes(new { style = "width: 290px;" })
                        .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                        .SelectedIndex(0)
                        .BindTo((SelectList)ViewData["categId_Data"])%>
                    <%= Html.RadioButton("rbOpciones", "4", false)%>Zona Sanitaria:
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxZona")
                        .DropDownHtmlAttributes(new { style = "width:Auto;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })
                        .HighlightFirstMatch(true)
                        .HtmlAttributes(new { style = "width: 200px;" })
                        .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                        .SelectedIndex(0)
                        .BindTo((SelectList)ViewData["repId_Data"])%>
                    <%= Html.RadioButton("rbOpciones", "5", false)%>Sector:
                    <%= Html.Telerik().ComboBox()
                        .Name("cbxSector")
                        .DropDownHtmlAttributes(new { style = "width:Auto;" })
                        .OpenOnFocus(true)
                        .AutoFill(true)
                        .Filterable(filtering =>
                        {
                            filtering.FilterMode(AutoCompleteFilterMode.StartsWith);
                        })
                        .HighlightFirstMatch(true)
                        .HtmlAttributes(new { style = "width: 200px;" })
                        .ClientEvents(events => events.OnChange("onComboBoxChange").OnLoad("onComboBoxLoad"))
                        .SelectedIndex(0)
                        .BindTo((SelectList)ViewData["secId_Data"])%>

                    <button class="t-button" name="cmdVer" title="Pulse aquí para ver las estadísticas" onclick="InvocarReporte();">Ver Informe</button>
                </div>
            </div>
        <%})
       .Buttons(b => b.Maximize().Close())
       .Draggable(true)
       .Scrollable(false)
       .Resizable()
       .Width(570)
       .Height(300)
       .Render();
%>
