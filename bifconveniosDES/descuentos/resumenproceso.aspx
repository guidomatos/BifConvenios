<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ResumenProceso"
    CodeFile="ResumenProceso.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>BIFConvenios - Enviar Información a IBS</title>
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />

    <script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"
        type="text/javascript"></script>

    <script language="javascript">
		<!--
		function Procesar ( id ) {
			if ( confirm ( '¿Desea enviar el archivo de cuotas recibido de la Empresa a IBS?') ) {
				document.all('hdIdEnvio').value = id;
				__doPostBack('lnkEnviar', '');			
			}
		}
		
		function MsgAlerta(){
			alert("No cuenta con fondos suficientes para realizar la operación.");
		}
		-->
		
		
		
    </script>

</head>
<body leftmargin="0" topmargin="0">
    <form id="Form1" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" Title="Enviar Información a IBS" runat="server"></uc1:Banner>
                </td>
            </tr>
            <tr>
                <td>
                    <table height="550" width="650">
                        <tr>
                            <td width="40">
                                &nbsp;</td>
                            <td valign="top">
                                <asp:Panel ID="pnlControl" runat="server">
                                    <table cellspacing="5" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Empresa</td>
                                            <td class="Normal" width="75%">
                                                <asp:Literal ID="ltrlCliente" runat="server"></asp:Literal></FONT></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Documento</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlDocumento" runat="server"></asp:Literal></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Estado</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlEstado" runat="server"></asp:Literal></FONT></td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead">
                                                Periodo</td>
                                            <td class="Normal">
                                                <asp:Literal ID="ltrlPeriodo" runat="server"></asp:Literal></FONT></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="SubHead" align="middle" colspan="2">
                                                Estado de los Registros<br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="middle" colspan="2">
                                                <asp:DataGrid ID="dgInformacionResumen" runat="server" ShowHeader="False" AutoGenerateColumns="False"
                                                    BorderColor="black" BorderWidth="1">
                                                    <Columns>
                                                        <asp:BoundColumn ItemStyle-CssClass="Normal" DataField="CodigoNombre" ItemStyle-Width="200"
                                                            HeaderText="Estado"></asp:BoundColumn>
                                                        <asp:BoundColumn ItemStyle-CssClass="Normal" ItemStyle-HorizontalAlign="Right" DataField="Conteo"
                                                            ItemStyle-Width="50" HeaderText="Cantidad"></asp:BoundColumn>
                                                    </Columns>
                                                </asp:DataGrid><br>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="Normal" align="middle" colspan="2">
                                                Total de Registros&nbsp;:&nbsp;
                                                <asp:Label ID="lblTotal" CssClass="SubHead" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                &nbsp;
                                                <asp:Label ID="lblMensajeError" runat="server" ForeColor="red"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <a href="JavaScript:Procesar('<%=Pid%>');" class="Normal">Enviar archivo de cuotas recibido
                                                    de la Empresa a IBS</a><input id="hdIdEnvio" type="hidden" name="hdIdEnvio" runat="server">
                                                <asp:LinkButton ID="lnkEnviar" runat="server"></asp:LinkButton></td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <asp:Panel ID="pnlMensaje" runat="server" Visible="False">
                                    <br>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ID="lblMensaje" CssClass="SubHead" runat="server"></asp:Label>

                                    <script language="javascript">
							<!--
							//procedimiento para mostrar la ventana de espera del proceso
								openPage('EsperaFinalEnvioAS400.aspx?id=<%=Pid%>', 300, 390);							
							-->
                                    </script>

                                </asp:Panel>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
