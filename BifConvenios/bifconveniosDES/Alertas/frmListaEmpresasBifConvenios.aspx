<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmListaEmpresasBifConvenios.aspx.vb" Inherits="Alertas_frmListaEmpresasBifConvenios" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Listado de Empresas BifConvenios</title>
    <meta http-equiv="Pragma" content="no-cache" />
    <meta http-equiv="Expires" content="-1" />
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    
    <base target="_self" />
    
    <script type="text/javascript">
        function Seleccionar(id) {
            top.returnValue = id;
            this.close();
        }
    </script>
    
     <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    
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
</head>
<body style="margin-left:20px; margin-top:10px;">
    <form id="form1" method="post" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:820px;">
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
                                                    <asp:Literal ID="ltrTitulo" Text="Listado de Empresas BIFConvenios" runat="server" />
                                                </h2>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; height:30px;" align="right">
                                                <asp:Label ID="Label1" Text="Buscar por:" runat="server" Width="100px" />
                                            </td>
                                            <td style="width:150px; height:30px;">
                                                <asp:DropDownList ID="ddlCriterio" runat="server" Width="150px" Height="25px">
                                                    <asp:ListItem Value="0" Selected="true">-- Todos --</asp:ListItem>
                                                    <asp:ListItem Value="1">Num. Documento</asp:ListItem>
                                                    <asp:ListItem Value="2">Nombre Cliente</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="width:60px; height:30px;" align="right">
                                                <asp:Label ID="Label2" Text="Valor:" runat="server" CssClass="label" Width="60px" />
                                            </td>
                                            <td style="width:120px; height:30px;">
                                                <asp:TextBox ID="txtValor" runat="server" CssClass="textbox" Width="120" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:Button ID="btnSearch" runat="server" Text="Filtrar" CssClass="button" />
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
                    <asp:UpdatePanel ID="upQuery" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvQuery" runat="server" CellPadding="3" ForeColor="black"
                                GridLines="Vertical" EnableViewState="false" BackColor="white"
                                BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="5"
                                AllowSorting="true"
                                OnPageIndexChanging="gvQuery_PageIndexChanging">
                                <Columns>
                                    <asp:BoundField ItemStyle-Width="100px" DataField="Codigo_Cliente" HeaderText="Cod. Cliente" ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField ItemStyle-Width="80px" DataField="Codigo_IBS" HeaderText="Cod. IBS" ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField ItemStyle-Width="120px" DataField="NumeroDocumento" HeaderText="Documento" ItemStyle-HorizontalAlign="right" />
                                    <asp:BoundField ItemStyle-width="500px" DataField="Nombre_Cliente" HeaderText="Nombre Cliente" ItemStyle-HorizontalAlign="left" />
                                    <asp:TemplateField ItemStyle-Width="100" HeaderText="Acciones">
                                        <ItemTemplate>
                                            <%#GetMensajeSeleccionar(DataBinder.Eval(Container.DataItem, "Codigo_Cliente"))%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
            <div class="row">
                <div class="cell container">
                    <div class="searchborder">
                        <div class="search" style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">
                                    <table style="border:0; width:100%;" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <h3>
                                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Size="14" />
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
    </form>
</body>
</html>

