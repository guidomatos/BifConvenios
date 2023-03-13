<%@ Control Language="VB" AutoEventWireup="false" CodeFile="BuscarEmpresa.ascx.vb" Inherits="controls_BuscarEmpresa" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript">
    function Seleccionar(id, empresa)
    {
        document.getElementById('<%=hfCodigoIBS.ClientID %>').value = id;
        document.getElementById('<%=hfNombreEmpresa.ClientID %>').value = empresa;
        document.getElementById('<%=BtnSeleccionarRegistro.ClientID%>').click();
    }
</script>
    
<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
    
<style type="text/css">
    .ui-dialog-title
    {
	    font-size:14px;
    }
    .ui-button-text
    {
	    font-size:14px;
    }
    .ui-dialog-content
    {
	    font-size:12px;
    }
    #container
    {
        display:table;
        border:dashed 1px gray;
        margin: 20px 10px;
    }
    .row
    {
        display:table-row;
    }
    .cell
    {
        display:table-cell;
    }
    .containercell
    {
        padding:1px;
    }
    #search
    {
        display:table;
        width:100%;
        height:30px;
    }
    .searchborder
    {
        border: 1px solid black;
        padding: 1px;
    }
    .button
    {
        font-weight:bold;
        color:#FFFFFF;
        background-color:#555555;
        border-style:solid;
        border-color:#000000;
        border-width:1px;
        height:25px;
    }
    .textbox 
    {
        padding:4px 4px 4px 4px;
        border:1px solid #CCCCCC;  
        height:15px;      
    }
    .label
    {        
        padding:4px 4px 4px 4px;         
        height:15px;        
    }
</style>

<asp:ImageButton ID="btnVerPanel" ImageUrl="../images/texto.gif" AlternateText="Buscar" runat="server" />
<asp:Panel ID="pnlBuscarEmpresa" runat="server">
    <div id="container" style="width:800px;">
        <div class="row">
            <div class="cell containercell">
                <div class="searchborder">
                    <div class="search" style="background-color:#E6F5FF;">
                        <div class="row">
                            <div class="cell">
                                <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td>
                                            <h2>
                                                <asp:Literal ID="ltrTitulo" Text="Listado de Empresas" runat="server" />
                                            </h2>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <br />
                        <div class="row">
                            <div class="cell">
                                <table border="0" cellpadding="0" width="100%">
                                    <tr>
                                        <td>
                                            Empresa:&nbsp;<asp:TextBox ID="txtfEmpresa" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button id="btnSearch" runat="server" class="button" Text="Filtrar" />
                                        </td>
                                    </tr>
			                        <tr>
					                    <td colspan="2">
						                    <span class="SubHead">Número de Registros&nbsp;:&nbsp;</span>
						                    <asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label>
					                    </td>
				                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>                    
            </div>
        </div>
        <div class="row">
            <div class="cell container">
                <asp:UpdatePanel ID="upQuery" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="dgDatos" runat="server" CellPadding="3" ForeColor="Black"
                            GridLines="Vertical" EnableViewState="False" BackColor="White"
                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                            AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="15"
                            AllowSorting="True">
                            <Columns>
                                <asp:BoundField DataField="Codigo_IBS" HeaderText="C&#243;digo IBS"   >
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre Cliente" >
                                    <ItemStyle HorizontalAlign="Left" Width="65%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Acciones">
                                    <ItemTemplate>
                                        <%#GetMensajeSeleccionar(DataBinder.Eval(Container.DataItem, "Codigo_IBS"), DataBinder.Eval(Container.DataItem, "Nombre_Cliente"))%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="20%" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                            <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
                            <AlternatingRowStyle BackColor="#CCCCCC" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="dgDatos" EventName="PageIndexChanging" />
                        <asp:AsyncPostBackTrigger ControlID="dgDatos" EventName="Sorting" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="cell container">
                <div class="searchborder">
                    <div class="search" style="background-color:#E6F5FF;">
                        <div class="row">
                            <div class="cell">
                                <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="height: 40px" align="center">
                                            <h3>
                                                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Size="12pt" />
                                            </h3>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Panel>
<asp:Button ID="BtnSeleccionarRegistro" runat="server" style="display: none;" />
<asp:HiddenField ID="hfCodigoIBS" runat="server" />
<asp:HiddenField ID="hfNombreEmpresa" runat="server" />
<asp:ImageButton ID="btnCerrarPanel" runat="server" style="display: none"  />

<cc1:ModalPopupExtender ID="mpeBuscarEmpresa" runat="server"
    TargetControlID="btnVerPanel"
    PopupControlID="pnlBuscarEmpresa"
    CancelControlID="btnCerrarPanel"
    X="460" Y="200" />