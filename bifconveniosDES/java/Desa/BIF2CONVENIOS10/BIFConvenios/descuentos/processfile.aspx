<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.processfile"
    CodeFile="processfile.aspx.vb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
    
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Procesar Archivo de cuotas</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <link href="../css/global.css" type="text/css" rel="stylesheet" />

    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>

    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script type="text/javascript">
		
		function info(obj, args){
				
				if ( fctTrim ( document.all( 'flnArchivoDescuento').value ) == '' ){
					alert ( 'Ingrese el archivo para el proceso' );
					document.all( 'flnArchivoDescuento').focus();
					args.IsValid = false;		
					return;					
				} 
				else{
					args.IsValid = confirm('¿Desea cargar el archivo de Cuotas recibido de la Empresa?');		
					return;					
				}
				args.IsValid = true;
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
<body style="margin-left:0; margin-top:0; margin-right:0;" onload="MM_preloadImages('/BIFConvenios/images/procesar_on.jpg', '/BIFConvenios/images/cancelar_on.jpg')">
    <form id="Form1" method="post" enctype="multipart/form-data" runat="server">
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" runat="server" Title="Procesar Archivo de Cuotas" />
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server" />        
        <table style="border:0; width:100%;" cellspacing="0" cellpadding="0">            
            <tr>
                <td>
                    <table style="height:550px; width:650;">                        
                        <tr>
                            <td>
                                &nbsp;</td>
                            <td style="width:40px;">
                                &nbsp;</td>
                            <td valign="top">
                                <asp:Panel ID="pnlControls" runat="server" Visible="True">
                                    <table cellspacing="18" cellpadding="0" border="0">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Empresa</td>
                                            <td class="Normal" style="width:75%;">
                                                <asp:Literal ID="ltrlCliente" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Documento</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlDocumento" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Fecha Proceso</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlFechaProceso" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Estado</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlEstado" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Periodo</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlPeriodo" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Fecha de Carga</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlProcesoAS400" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Formato de archivo&nbsp;&nbsp;</td>
                                            <td class="Normal">
                                                <!-- MOD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS - GENERACION ARCHIV. DESCUENTOS -->
                                                <%--												    <SELECT id="lstFormatFile" name="lstFormatFile" runat="server">
														<OPTION value="xls">Formato Est&aacute;ndar - MS Excel (.xls)</OPTION>
													</SELECT>--%>
                                                <select id="lstFormatFile" name="lstFormatFile" runat="server">
                                                    <option value="xls" selected="selected">Formato Estándar - MS Excel (.xls)</option>
                                                    <option value="csv">Delimitado por comas (.csv)</option>
                                                    <option value="txt">Texto encolumnado (.txt)</option>                                                    
                                                </select>
                                                <!-- END -->
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Archivo</td>
                                            <td class="Normal">
                                                <input id="flnArchivoDescuento" type="file" name="flnArchivoDescuento" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkProcesar" runat="server" Text="<img src='/BIFConvenios/images/procesar.jpg' name='Image1' border=0 alt='Procesar archivo'/>"></asp:LinkButton>&nbsp;
                                                <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False">
														<img src='/BIFConvenios/images/cancelar.jpg' alt='Cancelar' /></asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <br />
                                <br />
                                <br />
                                <asp:Panel ID="pnlResults" runat="server" Visible="false" Width="550">
                                    <asp:Label ID="lblResults" runat="server" CssClass="SubHead"></asp:Label>
                                </asp:Panel>
                                <asp:Panel ID="pnlEsperaFinal" runat="server" Visible="False">
                                    <script type="text/javascript">
        							    openPage('EsperaFinalAD.aspx?id=<%=Pid%>', 300, 390);
                                    </script>
                                </asp:Panel>
                                <input id="hdIdProceso" type="hidden" name="hdIdProceso" runat="server" />
                                <input id="hdCodigoIBS" type="hidden" name="hdCodigoIBS" runat="server" />
                                <input id="hdAnio" type="hidden" name="hdAnio" runat="server" />
                                <input id="hdMes" type="hidden" name="hdMes" runat="server" />
                                <asp:CustomValidator ID="cvValida" runat="server" ClientValidationFunction="info"
                                    Display="None" ErrorMessage="CustomValidator"></asp:CustomValidator></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
