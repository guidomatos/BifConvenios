<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EditarCliente"
    CodeFile="EditarCliente.aspx.vb" EnableEventValidation="false" %>
    
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Edición de Datos de la Empresa</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>

    <script type="text/javascript">
    
            function BuscarCliente(){
                var returnValue = OpenFormatPageDialog('frmListadoEmpresasIBS.aspx',400,650);
                if (fctTrim(returnValue) != ''){
                    document.all('hdIdIBS').value = returnValue;                    
                    __doPostBack('lnkCargarClienteIBS','');
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
<body style="margin-left:0; margin-top:0;" onload="MM_preloadImages('/BIFConvenios/images/aceptar_on.jpg','/BIFConvenios/images/cancelar_on.jpg');">
    <form id="Form1" runat="server">
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Registro de Empresas"></uc1:Banner>
                </td>
            </tr>            
        </table>
        <asp:HiddenField ID="hdIdIBS" runat="server" />
        <asp:HiddenField ID="hdIdCliente" runat="server" />
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        <asp:ScriptManager ID="ScripManager1" runat="server"></asp:ScriptManager>
        <div id="container" style="width:600px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search" style="background-color:#E6F5FF;">
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblTitClienteIBS" runat="server" Font-Bold="true" Font-Size="14" Text="Datos de la Empresa en IBS" />
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
                                                <asp:Label ID="lblCodigoIBS" runat="server" Width="100" Text="Codigo IBS:" />
                                            </td>
                                            <td style="width:80px;">
                                                <asp:TextBox ID="txtCodigoIBS" runat="server" Width="80" Enabled="false"/>
                                            </td>
                                            <td style="width:120px;" align="left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Seleccionar..." CssClass="button" OnClientClick="BuscarCliente();" Width="115px" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkCargarClienteIBS" runat="server" Visible="false" />
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
                                                <asp:Label ID="lblDocumento" runat="server" Width="100" Text="Documento:" />
                                            </td>
                                            <td style="width:220px;">
                                                <asp:TextBox ID="txtTipoDocumento" runat="server" Width="220px" Enabled="false" />
                                            </td>
                                            <td style="width:130px;">
                                                <asp:TextBox ID="txtNumeroDocumento" runat="server" Width="130px" Enabled="false" />
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
                                                <asp:Label ID="lblEmpresa" runat="server" Width="100" Text="Empresa:" />
                                            </td>
                                            <td style="width:355px;">
                                                <asp:TextBox ID="txtNombreCliente" runat="server" Width="355px" MaxLength="100" Enabled="false" />
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
                                                <asp:Label ID="lblOficina" runat="server" Width="100" Text="Oficina:" />
                                            </td>
                                            <td style="width:50px;">
                                                <asp:TextBox ID="txtCodOficina" runat="server" Width="50px" MaxLength="4" Enabled="false" />
                                            </td>
                                            <td style="width:300px;">
                                                <asp:TextBox ID="txtNomOficina" runat="server" Width="300px" MaxLength="100" Enabled="false" />
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
                                                <asp:Label ID="lblGestor" runat="server" Width="100" Text="Gestor:" />
                                            </td>
                                            <td style="width:50px;">
                                                <asp:TextBox ID="txtCodGestor" runat="server" Width="50px" MaxLength="100" Enabled="false" />
                                            </td>
                                            <td style="width:300px;">
                                                <asp:TextBox ID="txtNomGestor" runat="server" Width="300px" MaxLength="12" Enabled="false" />
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
                                            <td>
                                                <asp:Label ID="lblTituloClienteBIF" runat="server" Font-Bold="true" Font-Size="14" Text="Datos Complementarios " />
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
                                                <asp:Label ID="lblDiaEnvioPlanilla" runat="server" Width="100" Text="Envío Planilla:" />
                                            </td>
                                            <td style="width:40px;">
                                                <asp:TextBox ID="txtDiaEnvioPlanilla" runat="server" MaxLength="2" Width="40" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblDiaCierrePlanilla" runat="server" Width="100" Text="Cierre Planilla:" />
                                            </td>
                                            <td style="width:40px;">
                                                <asp:TextBox ID="txtDiaCierrePlanilla" runat="server" MaxLength="2" Width="40" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblFuncionarioConvenio" runat="server" Width="100" Text="Funcionario:" />            
                                            </td>
                                            <td style="width:150px;">
                                                <asp:DropDownList ID="ddlFuncionarioConvenios" runat="server" DataTextField="Descripcion" DataValueField="Codigo" Width="150" />
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
                                                <asp:Label ID="lblMAnticipoEnvioListado" runat="server" Width="100" Text="Meses Anticipo:" />
                                            </td>
                                            <td style="width:40px;">
                                                <asp:TextBox ID="txtMesesAnticipoEnvioListado" runat="server" MaxLength="2" Width="40" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblDiaCorte" runat="server" Width="100" Text="Dia de Corte:" />
                                            </td>
                                            <td style="width:40px;">
                                                <asp:TextBox ID="txtDiaCorte" runat="server" MaxLength="2" Width="40" />
                                            </td>
                                            <td style="width:100px;" align="right">
                                                <asp:Label ID="lblTipoArchivo" runat="Server" Width="100" Text="Tipo Archivo:" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:DropDownList ID="ddlTipoArchivo" runat="server" DataTextField="vDescripcion" DataValueField="vDescripcion" Width="150" />
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
                                                <asp:Label ID="lblCodigoInstitucion" runat="server" Width="100" Text="Cod. Institución:" />
                                            </td>
                                            <td style="width:40px;">
                                                <asp:TextBox ID="txtCodigoInstitucion" runat="server" width="40px" />
                                            </td>
                                            <td style="width:145px;" align="right">
                                                <asp:Label ID="lblEnvioAutomaticoListado" runat="server" Width="145" Text="Envio aut. listado descuento:" />
                                            </td>
                                            <td style="width:100px;">
                                                <asp:DropDownList ID="ddlEnvioAutListadoDescuentos" runat="server" DataTextField="Descripcion" DataValueField="Codigo" Width="100" />
                                            </td>
                                            <td style="width:150px;" align="right">
                                                <asp:CheckBox ID="chkBloquear" runat="server" Checked="false" Text="Bloquear Cuenta:" TextAlign="left" />
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
                                                <asp:Label ID="lblTelefonos" runat="server" Width="100" Text="Teléfonos:" />
                                            </td>
                                            <td style="width:145px;">
                                                <asp:TextBox ID="txtTelefono1" runat="server" MaxLength="11" Width="145" />
                                            </td>
                                            <td style="width:145px;">
                                                <asp:TextBox ID="txtTelefono2" runat="server" MaxLength="11" Width="145" />
                                            </td>
                                            <td style="width:150px;">
                                                <asp:TextBox ID="txtTelefono3" runat="server" MaxLength="11" Width="150" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>                            
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="red" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <asp:CustomValidator ID="cvValida" runat="server" ErrorMessage="CustomValidator" Display="none" ClientValidationFunction="valida" />                                                                       
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table width="100%">
                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btnAceptar" runat="server" CssClass="button" Text="Guardar" />
                                            </td>
                                            <td align="left">
                                                <asp:Button ID="btnCancelar" runat="server" CssClass="button" Text="Cancelar" />
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