<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.EditarCliente"
    CodeFile="EditarCliente.aspx.vb" EnableEventValidation="false" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    

<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/Banner.ascx" %>



<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Edición de Datos de la Empresa</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
   <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet">--%>
		<LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet">
		<%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
		<script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
    <link href="<%= ResolveUrl("~/App_Themes/Default/Default.css") %>" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
 
            function ObtenerNombreEmpresa(nombre){
            document.all('hdNombreEmpresa').value = nombre;
            }
            function ObtenerTipoDocumento(value){
            document.all('hdTipoDocumento').value = value;
            }
            function ObtenerNumeroDocumento(value){
            document.all('hdNumeroDocumento').value = value;
            }
            function ObtenerCodigoIBS(value){
            document.all('hdCodigoIBS').value = value;
            }
            function ObtenerCodOficina(value){
            document.all('hdCodOficina').value = value;
            }
            function ObtenerNomOficina(value){
            document.all('hdNomOficina').value = value;
            }
            function ObtenerCodGestor(value){
            document.all('hdCodGestor').value = value;
            }
            function ObtenerNomGestor(value){
            document.all('hdNomGestor').value = value;
            }
            
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
			
			
		
                
                function ShowEditModal(ExpanseID) {
                    var _CodigoIBS = document.all('hdIdCliente').value;
                    var frame = $get('IframeEdit');
                    frame.src = "editarCodigoIBS.aspx?codigoIBS=" + _CodigoIBS;
                    $find('EditModalPopup').show();
                }
                
                function EditCancelScript() {
                var frame = $get('IframeEdit');
                frame.src = "Loading.aspx";
                
                }
                
                 function EditOkayScript() {
                    $get('btnCancelar').click();
                    EditCancelScript();
                }
			    
			    function onExiste(){
			        alert("El código IBS no existe");
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
        height: 450px;
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
  
        .modalBackground
            {
                background-color: #000000;
                opacity: 0.7;
                filter: alpha(opacity=50);
                -moz-opacity: 0.7;
                position :absolute !important;
                z-index : 100003 !important;
            }
        .modalPopup
        {
            
            background-color: #FFFFFF;
            margin: 0px; 
            padding: 0px;
            width:240px;
        }
        
        
    
    </style>

</head>
<body style="margin-left:0; margin-top:0;" onload="MM_preloadImages('../images/aceptar_on.jpg','../images/cancelar_on.jpg');">
    <form id="Form1" runat="server">
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Registro de Empresas"></uc1:Banner>
                </td>
            </tr>            
        </table>
        <asp:HiddenField ID="hdIdIBS" runat="server" />
        <asp:HiddenField ID="hdExiste" runat="server" />
        <asp:HiddenField ID="hdIdCliente" runat="server" />
        <input type="hidden" runat="server" id="hdNombreEmpresa" />
        <input type="hidden" runat="server" id="hdTipoDocumento" />
        <input type="hidden" runat="server" id="hdNumeroDocumento" />
        <input type="hidden" runat="server" id="hdCodigoIBS" />
        <input type="hidden" runat="server" id="hdCodOficina" />
        <input type="hidden" runat="server" id="hdNomOficina" />
        <input type="hidden" runat="server" id="hdCodGestor" />
        <input type="hidden" runat="server" id="hdNomGestor" />
        
        <asp:HiddenField ID="hdTipoGuardar" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
       
        <div id="container" style="width:600px;">
            <div class="row">
                <div class="cell containercell">
                    <div class="searchborder">
                        <div id="search" style="background-color:#E6F5FF; height:450px;">
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
                                                <%--<asp:TextBox ID="txtCodigoIBS" runat="server" Width="80" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt"/>--%>
                                                <input type="text" id="txtCodigoIBS" runat="server" style="font-family:Arial;font-size:8pt;width:80px;border:1px solid black;" maxlength="100" onblur="ObtenerCodigoIBS(this.value);" />
                                            </td>
                                            <td style="width:120px;" align="left">
                                                <asp:Button ID="btnSearch" runat="server" Text="Seleccionar..." CssClass="button" OnClientClick="BuscarCliente();" Width="115px" />
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkCargarClienteIBS" runat="server" Visible="false" />
                                            </td>
                                            <td>
                                               
                                                <asp:Button ID="btneditar" CausesValidation="false" runat="server" Text="Modificar Código IBS" CssClass="button"
                                                OnClientClick="ShowEditModal();" />
                                                
                                                <asp:Button ID="ButtonEdit" runat="server" Text="Edit Expanse" style="display:none" />
                                                <cc1:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="ModalPopupBG"
                                                    runat="server" CancelControlID="ButtonEditCancel" OkControlID="ButtonEditDone" 
                                                    TargetControlID="btneditar" PopupControlID="DivEditWindow" 
                                                    OnCancelScript="EditCancelScript();" OnOkScript="EditOkayScript();"
                                                    BehaviorID="EditModalPopup">
                                                </cc1:ModalPopupExtender>
                                                <div class="popup_Buttons" style="display: none">
                                                    <input id="ButtonEditDone" value="Done" type="button" />
                                                    <input id="ButtonEditCancel" value="Cancel" type="button" />
                                                </div>
                                                
                                                
                                                <div id="DivEditWindow" style="display: none;" class="popupConfirmation">
                                                    <iframe id="IframeEdit" frameborder="0" height="148" scrolling="no">
                                                    </iframe>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <table>
                                        <tr>
                                            <td style="width:100px; height: 26px;" align="right">
                                                <asp:Label ID="lblDocumento" runat="server" Width="100" Text="Documento:" />
                                            </td>
                                            <td style="width:220px; height: 26px;">
                                                <input type="text" id="txtTipoDocumento" runat="server" style="font-family:Arial;font-size:8pt;width:220px;border:1px solid black;" maxlength="100" />
                                                <%--<asp:TextBox ID="txtTipoDocumento" runat="server" Width="220px" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                            </td>
                                            <td style="width:130px; height: 26px;">
                                                <input type="text" id="txtNumeroDocumento" runat="server" style="font-family:Arial;font-size:8pt;width:130px;border:1px solid black;" maxlength="100" />
                                                <%--<asp:TextBox ID="txtNumeroDocumento" runat="server" Width="130px" BackColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
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
                                            <td style="width:355px; height: 26px;">
                                                <%--<asp:TextBox ID="txtNombreCliente" runat="server" Width="355px" MaxLength="100" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                                <input type="text" id="txtNombreCliente" runat="server" style="font-family:Arial;font-size:8pt;width:355px;border:1px solid black;" maxlength="100" />
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
                                            <td style="width:50px; height: 26px;">
                                                <%--<asp:TextBox ID="txtCodOficina" runat="server" Width="50px" MaxLength="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                                <input type="text" id="txtCodOficina" runat="server" style="font-family:Arial;font-size:8pt;width:50px;border:1px solid black;" maxlength="100" />
                                            </td>
                                            <td style="width:300px;">
                                                <%--<asp:TextBox ID="txtNomOficina" runat="server" Width="300px" MaxLength="100" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                                <input type="text" id="txtNomOficina" runat="server" style="font-family:Arial;font-size:8pt;width:300px;border:1px solid black;" maxlength="100" />
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
                                                <%--<asp:TextBox ID="txtCodGestor" runat="server" Width="50px" MaxLength="100" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                                <input type="text" id="txtCodGestor" runat="server" style="font-family:Arial;font-size:8pt;width:50px;border:1px solid black;" maxlength="100" />
                                            </td>
                                            <td style="width:300px;">
                                                <%--<asp:TextBox ID="txtNomGestor" runat="server" Width="300px" MaxLength="12" BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="8pt" />--%>
                                                <input type="text" id="txtNomGestor" runat="server" style="font-family:Arial;font-size:8pt;width:300px;border:1px solid black;" maxlength="100" />
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
                                <asp:Label ID="lblCodigoCAS" runat="server" Width="100" Text="Cod. CAS:" />
                                </td>
                                <td style="width:40px;">
                                <asp:TextBox ID="txtCodigoCAS" runat="server" MaxLength="4" Width="40" />
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
                                <div class="cell" style="text-align:center;">
                                    <asp:Label ID="lblMensaje" runat="server" ForeColor="red" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="cell">
                                    <asp:CustomValidator ID="cvValida" runat="server" ErrorMessage="CustomValidator" Display="none"  />                                                                       
                                </div>
                            </div>
                            <br />
                            <div class="row">
                                <div class="cell">
                                    <table width="100%">
                                        <tr>
                                            <td align="center">
                                                <asp:Button ID="btnAceptar" runat="server" CssClass="button" Text="Guardar" />
                                            </td>
                                            <td align="center">
                                                <asp:Button ID="btnEliminar" runat="server" CssClass="button" Text="Eliminar" />
                                            </td>
                                            <td align="center">
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