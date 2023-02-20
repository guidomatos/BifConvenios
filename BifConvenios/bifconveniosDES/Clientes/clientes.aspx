<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.Clientes" CodeFile="Clientes.aspx.vb" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>    
    <title>BIFConvenios - Registro de Empresas</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    
    <%--<link href="../css/style.css" rel="stylesheet" type="text/css" />--%>
    <LINK href="<%= ResolveUrl("~/css/style.css") %>" type="text/css" rel="stylesheet" />
         <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    <%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
    <script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
    
		function EliminaCliente ( id, name ) {
			if ( confirm ( '¿Desea eliminar el registro de la Empresa : ' + name +  ' ?' )) {
					document.all("hdData").value = id;
				__doPostBack('lnkDelete','');		
			
			}
		}
		
    </script>
    
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
        padding:4px 4px 4px 22px;
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
<body style="margin-left:0; margin-top:0; margin-right:0;" onload="MM_preloadImages('../images/buscar_on.jpg')">
    <form id="Form1" method="post" runat="server">
        <asp:HiddenField ID="hdData" runat="server" />        
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Registro de Empresas"></uc1:Banner>
                </td>
            </tr>            
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>        
        <div id="container" style="width:750px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:60px; height:30px;" align="right">
                                                <asp:Label ID="Label1" Text="Empresa:" runat="server" Width="60px" />
                                            </td>
                                            <td style="width:250px; height:30px;">
                                                <asp:TextBox ID="txtNombreCliente" runat="server" Width="250px" Height="20px" />
                                            </td>
                                            <td style="width:60px;">
                                                <asp:Button ID="btnClientSearch" runat="server" Text="Buscar" CssClass="button" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:Button ID="btnNuevoCliente" runat="server" Text="Nueva Empresa" CssClass="button" />                                                
                                            </td>
                                            <td style="width:120px;">
                                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="button" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:LinkButton ID="lnkDelete" runat="server" Visible="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlQueryClientes" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvClientes" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="vertical" EnableViewState="false" BackColor="White"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvClientes_PageIndexChanging"
                                    AllowSorting="true">
                                    <Columns>
                                        <asp:BoundField DataField="Codigo_Cliente" HeaderText="C&#243;digo" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="Codigo_IBS" HeaderText="Cod. IBS" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="Nombre_Cliente" HeaderText="Nombre Empresa" ItemStyle-Width="240px" />
                                        <asp:BoundField DataField="TipoDocumento" HeaderText="Tipo Documento" ItemStyle-Width="200px" HeaderStyle-Width="120px" />                            
                                        <asp:BoundField DataField="NumeroDocumento" HeaderText="Num. Documento"  ItemStyle-Width="150px" />
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <a href='editarcliente.aspx?id=<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>'>Editar Empresa</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <a href='editarcoordinadorcliente.aspx?id=<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>'>Editar Coordinadores</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Panel runat="server" ID="pnlDel" Visible='<%#Databinder.Eval (Container.DataItem, "CanDelete")%>'>
                                                    <a href='JavaScript:EliminaCliente("<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>", "<%#Databinder.Eval (Container.DataItem, "Nombre_Cliente").Replace("'", "")%>");'>Eliminar</a>
                                                </asp:Panel>
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
            </asp:Panel>
            <br />
            <asp:Panel ID="pnlMensaje" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <div class="searchborder">
                            <div class="search">
                                <div class="row">
                                    <div class="cell">
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red" Font-Size="14" />
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