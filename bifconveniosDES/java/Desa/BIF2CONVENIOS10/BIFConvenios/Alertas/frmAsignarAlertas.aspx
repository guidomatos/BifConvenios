<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmAsignarAlertas.aspx.vb" Inherits="Alertas_frmAsignarAlertas" 
    EnableEventValidation="false" %>
    
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>BIFConvenios - Consulta de Alertas a Empresas</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />
    
    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>
        
    <script type="text/javascript">
        function CambiarEstadoAlertaCliente(id,name) {
            if (confirm('¿Desea Eliminar la Alerta: ' + name + ' de la empresa ?')) {
                document.all("hdAlertaClienteId").value = id;
                __doPostBack('lnkEliminar','');
            }
        }
        
        function BuscarCliente() {
            var returnValue = OpenFormatPageDialog('frmListaEmpresasBifConvenios.aspx',400,875);
            if (fctTrim(returnValue) != ''){
                    document.all('hdCodCliente').value = returnValue;                    
                    __doPostBack('lnkCargarCliente','');
                }
        }
        
        function OpenFormatPageDialog(url, height , width ) {
			var returnValue = window.showModalDialog(url,'', 'dialogTop: 150px; dialogLeft: 150px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
			if (typeof (returnValue) == "undefined"){
				returnValue = '';
			}
			return returnValue;
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
        padding:4px 4px 4px 4px;
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
                    <uc1:Banner ID="Banner1" runat="server" Title="Asignar Alertas a Empresas"></uc1:Banner>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdCodCliente" runat="server" />
        <asp:HiddenField ID="hdAlertaClienteId" runat="server" />
        <asp:LinkButton ID="lnkEliminar" runat="server" Visible="false" />
        <asp:LinkButton ID="lnkCargarCliente" runat="server" Visible="false" />
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:850px;">
            <div class="row">
                <div class="cell container">
                    <div class="searchborder">
                        <div id="search" style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">                                    
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblEmpresa" runat="server" Width="100px" text="Empresa:"/>
                                            </td>
                                            <td style="width:80px;">
                                                <asp:TextBox ID="txtCodEmpresa" runat="server" Width="80px" Enabled="false" />
                                            </td>
                                            <td style="width:320px;">
                                                <asp:TextBox ID="txtNomEmpresa" runat="server" Width="320px" Enabled="false" />
                                            </td>
                                            <td style="width:100px;"> 
                                                <asp:Button ID="btnBuscar" runat="server" Text="Seleccionar" CssClass="button" OnClientClick="BuscarCliente();"/>
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
            <br />
            <div class="row">
                <div class="cell">
                    <table>
                        <tr>
                            <td style="width:80px;" align="left">
                                <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" CssClass="button" Visible="false" Width="80px" />
                            </td>
                            <td style="width:100px;" align="left">
                                <asp:Button ID="btnEnviarAlertas" runat="server" Text="Enviar Alertas" CssClass="button" Visible="false" Width="100px" />
                            </td>
                        </tr>                        
                    </table>
                </div>
            </div>
            <asp:Panel ID="pnlQueryAlertasClientes" runat="server" Visible="false">                
                <div class="row">
                    <div class="cell container">
                        <asp:UpdatePanel ID="upQuery" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvAlertasClientes" runat="server" CellPadding="3" ForeColor="black"
                                    GridLines="Vertical" EnableViewState="false" BackColor="white"
                                    BorderColor="#999999" BorderStyle="solid" BorderWidth="1px"
                                    AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="10"
                                    
                                    AllowSorting="true">
                                    <Columns>
                                        <asp:BoundField DataField="iAlertaClienteId" Visible="false" />
                                        <asp:BoundField DataField="iAlertaId" Visible="false" />
                                        <asp:BoundField DataField="iTipoAlerta" Visible="false" />
                                        <asp:BoundField DataField="iClienteId" Visible="false" />                                        
                                        <asp:BoundField DataField="vNombreAlerta" HeaderText="Nombre Alerta" HeaderStyle-Width="250px" />
                                        <asp:BoundField DataField="vTipoAlerta" HeaderText="Tipo Alerta" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="iDiasAntes" HeaderText="Dias antes" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="iDiasDespues" HeaderText="Dias despues" HeaderStyle-Width="100px" />
                                        <asp:BoundField DataField="vAdjunto" HeaderText="Adjunto" HeaderStyle-Width="80px" />
                                        <asp:BoundField DataField="vEstadoAlerta" HeaderText="Estado Alerta" HeaderStyle-Width="150px" />
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                            <ItemTemplate>
                                                <a href='frmEditarAlertaCliente.aspx?id=<%#Databinder.Eval (Container.DataItem, "iAlertaClienteId") %>&idCliente=<%#Databinder.Eval (Container.DataItem, "iClienteId") %>'>Editar</a>
                                            </ItemTemplate>                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="40px" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <a href='JavaScript:CambiarEstadoAlertaCliente("<%#Databinder.Eval (Container.DataItem, "iAlertaClienteId") %>","<%#Databinder.Eval (Container.DataItem, "vNombreAlerta").Replace("'","") %>");'>Eliminar</a>
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