<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.AccesoArchivoGenerado"
    CodeFile="AccesoArchivoGenerado.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Archivo de cronograma futuro</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />

    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>

    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script type="text/javascript">
		<!--
			function EnviaCorreo(id, formatoArchivo, tipoCliente){
				openPage( 'enviomail.aspx?idp='+ id +  '&formatoArchivo=' + formatoArchivo +'&tipoCliente='+tipoCliente, 430,500 );
			}
			
			function GenerarArchivo (id, nombre, anio, mes, fechaProcesoAS400){
				var returnValue = OpenFormatPageDialog('../selectFileFormat.aspx?id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 300,580 );
				if ( fctTrim(returnValue) != '' ){ 
					document.all('hdId').value = returnValue;
					__doPostBack('lnkGenerarArchivo','');
				}
			}
					
		function OpenFormatPageDialog(url, height , width ) {
					var returnValue = window.showModalDialog(url,'', 'dialogTop: 200px; dialogLeft: 200px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
					if (typeof (returnValue) == "undefined"){
						returnValue = '';
					}
					return returnValue;
			}							
		-->
    </script>

</head>
<body style="margin-left:0; margin-top:0;">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" Title="Archivo de cronograma futuro" runat="server"></uc1:Banner>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="35" width="650" border="0">
            <tr>
                <td valign="top" width="30%" background="/BIFConvenios/images/hoja1.jpg" height="550">
                    <table cellspacing="0" cellpadding="35" width="100%" border="0">
                        <tr>
                            <td colspan="2">
                                <table cellspacing="0" cellpadding="5" border="0">
                                    <tr>
                                        <td class="SubHead">
                                            Empresa</td>
                                        <td class="Normal" width="70%">
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
                                            Fecha de Proceso IBS</td>
                                        <td class="Normal">
                                            <asp:Literal ID="ltrlFechaProcesoAS400" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td class="SubHead">
                                            Fecha de Carga</td>
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
                                            Formato del archivo</td>
                                        <td class="Normal">
                                            <asp:Literal ID="ltrlFormato" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td class="SubHead" align="center" colspan="2">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>                                                    
                                                    <td valign="top">
                                                        <asp:LinkButton ID="lnkDescargar" runat="server" CssClass="SubHead">Descargar Archivo</asp:LinkButton></td>
                                                    <td colspan="2">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td valign="top">
                                                        <a class="SubHead" href='javascript:GenerarArchivo("<%=PID%>","<asp:literal id="ltrlNombre" runat="server"/>","<asp:literal id="ltrlAnhio" runat="server"/>","<asp:literal id="ltrlMes" runat="server"/>","<asp:literal id="ltrlFechaIBS" runat="server"/>");'>
                                                            Generar otro archivo</a>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton runat="server" ID="lnkCancelar">Cancelar</asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!--generacion del archivo-->
        <asp:LinkButton ID="lnkGenerarArchivo" runat="server"></asp:LinkButton>
        <input id="hdId" type="hidden" name="hdId" runat="server" />
        <asp:Panel runat="server" ID="pnlGenArchivos" Visible="False">
            <table cellspacing="0" cellpadding="10" border="0">
                <tr>
                    <td>
                        <asp:Label ID="lblMensaje" runat="server" CssClass="SubHead"></asp:Label></td>
                </tr>
            </table>

            <script type="text/javascript">
							<!--
								openPage('<%Response.Write(Request.ApplicationPath)%>/generacionCf/EsperaFinalGeneracionCf.aspx?id=<%=idGenFile%>', 300, 390);
							-->
            </script>

        </asp:Panel>
    </form>
</body>
</html>
