<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAlertas.aspx.vb" Inherits="Alertas_frmAlertas" 
    EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>BIFConvenios - Consulta de Alertas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    <%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
    <script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
        
    <script type="text/javascript">
        function CambiarEstadoAlerta(id,estado,name) {
            if (confirm('¿Desea cambiar de estado de la Alerta: ' + name + ' ?')) {
                document.all("hdIdAlerta").value = id;
                document.all("hdEstado").value = estado;
                __doPostBack('lnkCambiarEstado','');
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
        height:20px;
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
<body style="margin-left:0; margin-top:0; margin-right:0">
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Mantenimiento de Alertas"></uc1:Banner>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdIdAlerta" runat="server" />
        <asp:HiddenField ID="hdEstado" runat="server" />
        <asp:LinkButton ID="lnkEditar" runat="server" Visible="false"/>
        <asp:LinkButton ID="lnkCambiarEstado" runat="server" Visible="false" />
       <%-- <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>--%>
        <div id="container" style="width:850px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search"  style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:60px;" align="right">
                                                <asp:Label ID="lblTipo" Text="Tipo:" runat="server" Width="60px" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:DropDownList ID="ddlTipoAlerta" runat="server" Width="100px" DataTextField="vDescripcion" DataValueField="vValor" />
                                            </td>
                                            <td style="width:60px;" align="right">
                                                <asp:Label ID="lblAlerta" Text="Alerta:" runat="server" Width="60px" />
                                            </td>
                                            <td style="width:200px;">
                                                <asp:TextBox ID="txtNombreAlerta" runat="server" Width="200px" />
                                            </td>
                                            <td style="width:60px;">
                                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="button" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="button" />
                                            </td>   
                                            <td style="width:120px;">
                                                <asp:Button ID="btnRegresar" runat="server" Text="Regresar" CssClass="button" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:Panel ID="pnlQueryAlertas" runat="server" Visible="false">
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvAlertas" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="vertical" EnableViewState="false" BackColor="White"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvAlertas_PageIndexChanging"
                                    AllowSorting="true">
                                    <Columns>
                                        <asp:BoundField DataField="iAlertaId" HeaderText="C&#243;digo" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="iTipoAlerta" Visible="false" />
                                        <asp:BoundField DataField="vTipoAlerta" HeaderText="Tipo Alerta" HeaderStyle-Width="120px" />
                                        <asp:BoundField DataField="vNombreAlerta" HeaderText="Nombre" HeaderStyle-Width="200px" />
                                        <asp:BoundField DataField="vAsuntoMensaje" HeaderText="Asunto" HeaderStyle-Width="300px" />                                        
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <a href='frmEditarAlertas.aspx?id=<%#Databinder.Eval (Container.DataItem, "iAlertaId")%>'>Editar</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="vEstadoAlerta" HeaderText="Estado" HeaderStyle-Width="100px" />
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <a href='JavaScript:CambiarEstadoAlerta("<%#Databinder.Eval (Container.DataItem, "iAlertaId")%>","<%#Databinder.Eval (Container.DataItem, "iEstadoAlerta")%>" ,"<%#Databinder.Eval (Container.DataItem, "vNombreAlerta").Replace("'", "")%>");'>Cambiar Estado</a>
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
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="red" />
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
