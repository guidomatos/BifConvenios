<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ProcesarArchivoDescuento" CodeFile="ProcesarArchivoDescuento.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Cargar Cuotas Descontadas (Empresa)</title>
    <link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
    <script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
        
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
        padding: 5px;
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
        padding:2px 2px 2px 3px;
        border:1px solid #CCCCCC;  
        height:25px;      
    }
    .label
    {
        padding:4px 4px 4px 22px;         
        height:25px;  
    }   
    
    </style> 
    
</head>
<body style="margin-left:0; margin-top:0; margin-right:0;">
    <form id="Form1" method="post" runat="server">
        <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <uc1:Banner id="Banner1" runat="server" Title="Cargar Cuotas Descontadas (Empresa)" />
                </td>
            </tr>
        </table>
        <div id="container" style="width:1200px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search">
                            <div class="row">
                                <div class="cell">
                                    <asp:Label  ID="lblAnio" Text="Año:" runat="server" CssClass="label" Width="60px" />
                                    <asp:DropDownList DataValueField="Anio_periodo" DataTextField="Anio_periodo" ID="ddlAnio"
                                        runat="server" Width="80px">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblMes" Text="Mes:" runat="server" CssClass="label" Width="60px" />
                                    <asp:DropDownList ID="ddlMes" DataTextField="MonthName" DataValueField="MonthOrder"
                                        runat="server" Width="150px">                                        
                                    </asp:DropDownList>
                                    <asp:Label ID="lblCliente" Text="Empresa:" runat="server" CssClass="label" Width="60px" />
                                    <asp:TextBox ID="txtCliente" runat="server" CssClass="textbox" Width="350" />
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="button" Width="80" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlInputProceso" runat="server">
                <div class="cell container">
                    <asp:UpdatePanel ID="upDatos" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvDatosCarga" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="Vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvDatosCarga_PageIndexChanging">
                                    <Columns>
                                        <asp:BoundField ItemStyle-Width="60" ItemStyle-Height="35" HeaderStyle-Width="60" DataField="Codigo_Cliente" HeaderText="C&#243;d Empresa" />
                                        <asp:BoundField ItemStyle-Width="450" DataField="Nombre_Cliente" HeaderText="Nombre Empresa" />
                                        <asp:TemplateField HeaderText="Periodo" ItemStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMes" runat="server" Text='<%# BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval(Container, "DataItem.Mes_Periodo"))  +" "+ DataBinder.Eval (Container.DataItem, "Anio_periodo")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Fecha de Carga" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="140">
                                            <ItemTemplate>
                                                <asp:Label runat="server" Text='<%# BIFConvenios.Utils.GetFechaCanonica ( DataBinder.Eval(Container, "DataItem.Fecha_ProcesoAS400"))  %>'
                                                        ID="lblFechaCarga">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="CodigoNombre" HeaderText="Estado" />
                                        <asp:BoundField ItemStyle-Width="130" ItemStyle-Height="35" HeaderStyle-Width="130" DataField="Fecha_CargaAS400" HeaderText="Fecha Proceso" />
                                        <asp:TemplateField ItemStyle-Width="100">
                                            <ItemTemplate>
                                                <a href='processfile.aspx?id=<%#DataBinder.Eval(Container.DataItem, "Codigo_proceso")%>'>
                                                        <%#GetMensajeProceso(DataBinder.Eval(Container.DataItem, "Estado"))%>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle BackColor="#999999" ForeColor="black" HorizontalAlign="center" />
                                    <SelectedRowStyle BackColor="#000099" Font-Bold="true" ForeColor="white" />
                                    <HeaderStyle BackColor="black" Font-Bold="true" ForeColor="white" />
                                    <AlternatingRowStyle BackColor="#cccccc" />
                                </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>                    
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <div class="searchborder">
                            <div class="search">
                                <div class="row">
                                    <div class="cell">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red"/>                                        
                                        <asp:Literal ID="ltrlScript" runat="server" Visible="false" />
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
