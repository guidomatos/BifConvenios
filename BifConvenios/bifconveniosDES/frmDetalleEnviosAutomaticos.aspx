<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmDetalleEnviosAutomaticos.aspx.vb" Inherits="frmDetalleEnviosAutomaticos" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>    

<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>BIFConvenios - Detalle de Procesos Automáticos</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <script src="js/global.js" type="text/javascript"></script>
    
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
        margin: 30px 30px;
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
        padding:4px 4px 4px 22px;
        border:1px solid #CCCCCC;  
        height:15px;      
    }
    .label
    {        
        padding:4px 4px 4px 22px;         
        height:15px;        
    }   
    
    </style>
</head>
<body style="margin-left:0; margin-top:0; margin-right:0;">
    <form id="form1" method="post" runat="server">
        <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:Banner id="Banner1" runat="server" Title="Detalles de Procesos Automáticos" />
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:1200px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search" style="width:1200px; background-color:#e6f5ff;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; height: 29px;" align="right">
                                                <asp:Label  ID="Label1" Text="Buscar por:" runat="server" Width="100px" />
                                            </td>
                                            <td style="width:120px; height: 29px;">
                                                <asp:DropDownList ID="ddlCriterio" runat="server" Width="120px" Height="25px">
                                                    <asp:ListItem Value="0" Selected="True">-- Todos --</asp:ListItem>
                                                    <asp:ListItem Value="1">Cod. Cliente</asp:ListItem>
							                        <asp:ListItem value="2">Cod. IBS</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:60px; height: 29px;" align="right">
                                                <asp:Label ID="Label2" Text="Valor:" runat="server" CssClass="label" Width="60px" />
                                            </td>
                                            <td style="width:120px; height: 29px;">
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Width="120px" />
                                            </td>
                                            <td style="width:150px; height: 29px;" align="right">
                                                <asp:Label ID="Label5" Text="Tipo de Envio:" runat="server" CssClass="label" Width="150px" />
                                            </td>
                                            <td style="width:260px; height: 29px;">
                                                <asp:DropDownList ID="ddlTipoEnvio" runat="server" Width="260px" Height="25px" DataValueField="vValor" DataTextField="vDescripcion" />
                                            </td>
                                            <td style="width:60px; height: 29px;" align="right">
                                                <asp:Label ID="Label6" Text="Estado:" runat="server" CssClass="label" Width="60px" />
                                            </td>
                                            <td style="width:150px; height: 29px;">
                                                <asp:DropDownList ID="ddlEstado" runat="server" Width="150px" Height="25px" DataValueField="vValor" DataTextField="vDescripcion" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Filtrar" CssClass="button" />
                                            </td>
                                            <td style="width:150px;" align="right">
                                                <asp:linkbutton id="lnkBack" Runat="server" Height="15px">
                                                    <img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Regresar' />
                                                </asp:linkbutton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </div>
            </div>            
            <asp:Panel ID="pnlQueryResult" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvQuery" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="Vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    AllowSorting="true"
                                    OnPageIndexChanging="gvQuery_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="80" DataField="iEnvioCorreoId" HeaderText="Codigo" ItemStyle-HorizontalAlign="left" Visible="false" />                                        
                                        <asp:BoundField ItemStyle-Width="300" DataField="vTipoEnvioCorreoId" HeaderText="Tipo de Envio" ItemStyle-HorizontalAlign="left" />                                        
                                        <asp:BoundField ItemStyle-width="100" DataField="iCodigoCliente" HeaderText="Cod. Cliente" ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField ItemStyle-width="80" DataField="iCodigoIBS" HeaderText="Cod. IBS" ItemStyle-HorizontalAlign="right" />
                                        <asp:BoundField ItemStyle-Width="300" DataField="vNombreCliente" HeaderText="Cliente" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField ItemStyle-width="150" DataField="vEstado" HeaderText="Estado" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField ItemStyle-width="220" DataField="dFecha" HeaderText="Fecha Envio" ItemStyle-HorizontalAlign="left" />
                                        <asp:BoundField ItemStyle-width="500" DataField="vMensajeProceso" HeaderText="Mensaje" ItemStyle-HorizontalAlign="left" />
                                    </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="center" />
                                    <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="true" ForeColor="white" />
                                    <HeaderStyle BackColor="black" Font-Bold="true" ForeColor="white" />
                                    <AlternatingRowStyle BackColor="#cccccc" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <div class="searchborder">
                            <div class="search">
                                <div class="row">
                                    <div class="cell">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Size=14 />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>               
        </div>
    </form>
</body>
</html>
