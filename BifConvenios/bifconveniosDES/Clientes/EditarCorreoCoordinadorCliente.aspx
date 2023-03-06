<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EditarCorreoCoordinadorCliente"
    CodeFile="EditarCorreoCoordinadorCliente.aspx.vb" EnableEventValidation="false" %>
    
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Edición de Coordinadores de la Empresa</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   <%-- <link href="<%=Request.ApplicationPath%>/css/grid.css" rel="stylesheet" type="text/css" />--%>
    <LINK href="<%= ResolveUrl("~/css/grid.css") %>" type="text/css" rel="stylesheet">
    <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">--%>
		<LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet">
		<%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
		<script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
		
			function Valida (obj, args ){
				args.IsValid = true;					
				
				if ( args.IsValid ) {
					args.IsValid = confirm ("¿Desea actualizar la información de los coordinadores de la empresa?");
				}
			}
			
			function EditarCoordinador(idCliente, idCoordinador) {			    
			    document.all("hdCodCliente").value = idCliente;
			    document.all("hdCodCoordinador").value = idCoordinador;
			    
			    __doPostBack('lnkEditar','');
			}
			
			function EliminarCoordinador(idCliente, idCoordinador, nomCoordinador, emailCoordinador) {
			    if (confirm('Desea eliminar el Coordinador :' + nomCoordinador + ', con codigo: ' + idCoordinador + '?')) {
			        document.all("hdCodCliente").value = idCliente;
			        document.all("hdCodCoordinador").value = idCoordinador;
			        document.all("hdEmailCoordinador").value = emailCoordinador;
			        __doPostBack('lnkEliminar','');
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
<body style="margin-left:0; margin-top:0;">
    <form id="Form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Coordinadores de la Empresa"></uc1:Banner>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdCodCliente" runat="server" />
        <asp:HiddenField ID="hdCodCoordinador" runat="server" />
        <asp:HiddenField ID="hdEmailCoordinador" runat="server" />
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        <asp:LinkButton ID="lnkEditar" runat="server" Visible="false"/>
        <asp:LinkButton ID="lnkEliminar" runat="server" Visible="false" />        
       <%-- <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>--%>
        <div id="container" style="width:720px;">
            <div class="row">
                <div class="cell containercell">
                    <div id="searchborder">
                        <div id="search" style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTitCoordinador" runat="server" Font-Bold="true" Font-Size="14" Text="Datos del Coordinador" />
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
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblCliente" runat="server" Width="100" Text="Empresa:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:TextBox ID="txtEmpresa" runat="server" Width="250" Enabled="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblNombreCoordinador" runat="server" Width="100" Text="Nombre:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:TextBox ID="txtNombreCoordinador" runat="server" Width="250" Enabled="false"/>
                                            </td>
                                            <td style="width:90px;" align="right">
                                                <asp:Label ID="lblCorreoElectronico" runat="server" Width="90" Text="Correo:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:TextBox ID="txtCorreoElectronico" runat="server" Width="250" Enabled="false"/>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblCargo" runat="server" Width="100" Text="Cargo:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:TextBox ID="txtCargo" runat="server" Width="250" Enabled="false"/>
                                            </td>
                                            <td style="width:90px;" align="right">
                                                <asp:Label ID="lblTipoPlanilla" runat="server" Width="90" Text="T. Planilla:" />
                                            </td>
                                            <td style="width:250px;">
                                                <asp:TextBox ID="txtTipoPlanilla" runat="server" Width="250" Enabled="false"/>
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
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblTelefono" runat="server" Width="100" Text="Telefono:" />
                                            </td>
                                            <td style="width:130px;">
                                                <asp:TextBox ID="txtTelefono" runat="server" Width="130" Enabled="false"/>
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblAnexo" runat="server" Width="100" Text="Anexo:" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:TextBox ID="txtAnexo" runat="server" Width="100" Enabled="false"/>
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblCelular" runat="server" Width="100" Text="Celular:" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:TextBox ID="txtCelular" runat="server" Width="150" Enabled="false"/>
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
                            <td>
                                <asp:Button ID="btnNuevo" runat="server" CssClass="button" Text="Nuevo" />
                            </td>
                            <td>
                                <asp:Button ID="btnRegresar" runat="server" CssClass="button" Text="Regresar" />
                            </td>
                            <td>
                                <asp:Button ID="btnGuardar" runat="server" CssClass="button" Text="Guardar" Visible="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" Visible="false" />
                            </td>                            
                        </tr>
                    </table>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="cell container">
                    <asp:UpdatePanel ID="upQuery" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="gvCoordinadores" runat="server" CellPadding="3" ForeColor="Black"
                                GridLines="None" EnableViewState="False" BackColor="White"
                                BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px"
                                AutoGenerateColumns="False" Width="100%" AllowPaging="True"
                                CssClass = "mGrid"
                                PagerStyle-CssClass="pgr"
                                AlternatingRowStyle-CssClass="alt"
                                AllowSorting="True">
                                <Columns>
                                    <asp:BoundField DataField="iClienteId" HeaderText="Cliente" Visible="False" >
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="iCoordinadorId" HeaderText="Codigo" >
                                        <HeaderStyle Width="80px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vNombreCoordinador" HeaderText="Coordinador" >
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="vEmailCoordinador" HeaderText="Correo Electronico" >
                                        <HeaderStyle Width="150px" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href='JavaScript:EditarCoordinador("<%#Databinder.eval (Container.DataItem, "iClienteId") %>","<%#Databinder.Eval (Container.DataItem, "iCoordinadorId") %>");'>Editar</a>    
                                        </ItemTemplate>                                        
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a href='JavaScript:EliminarCoordinador("<%#Databinder.eval (Container.DataItem, "iClienteId") %>","<%#Databinder.Eval (Container.DataItem, "iCoordinadorId") %>","<%#Databinder.eval (Container.DataItem, "vNombreCoordinador") %>","<%#Databinder.eval (Container.DataItem, "vEmailCoordinador") %>");'>Eliminar</a>
                                        </ItemTemplate>
                                        <ItemStyle Width="80px" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
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
