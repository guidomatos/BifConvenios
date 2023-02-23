<%@ Register TagPrefix="cc1" Namespace="SolpartWebControls" Assembly="SolpartWebControls" %>
<%@ Control Language="vb" AutoEventWireup="false" Inherits="Banner" CodeFile="Banner.ascx.vb" %>

<table width="100%" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td height="55" colspan="2">
            <table width="100%" height="51" border="0" cellpadding="0" cellspacing="0" background="<%= ResolveUrl("~/images/lineavertical.gif") %>">
                <tr>
                    <td width="224" align="left" valign="top">
                        <a href="<%= ResolveUrl("~/") %>">
                            <img src="<%= ResolveUrl("~/images/cabeceraint.gif") %>" border="0" width="224">
                        </a>
                    </td>
                    <td background="<%= ResolveUrl("~/images/derech.jpg") %>">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td height="32">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td valign="bottom" align="left">
                                    <table border="0" cellspacing="0" cellpadding="0" width="555">
                                        <tr>
                                            <td>
                                                <cc1:SolpartMenu ID="ctlMenu" runat="server" SystemImagesPath="../images/"
                                                    BackColor="#8899DD" ForeColor="White" Font-Names="Arial,Helvetica" Font-Bold="True"
                                                    Font-Size="12px" MenuEffects-Style="filter:progid:DXImageTransform.Microsoft.Shadow(color='DimGray', Direction=135, Strength=4) progid:DXImageTransform.Microsoft.Alpha( Opacity=100, FinishOpacity=75, Style=1, StartX=0,  FinishX=100, StartY=0, FinishY=100);"
                                                    MenuEffects-MouseOverDisplay="None" SelectedColor="#EE6600" MenuEffects-MouseOverExpand="True"
                                                    ShadowColor="#404040" IconBackgroundColor="#8899DD" HighlightColor="#FF8080"
                                                    SelectedForeColor="White" IconWidth="8" SelectedBorderColor="#333333" Moveable="True"
                                                    MenuBarHeight="8" MenuItemHeight="8" MenuBorderWidth="0" ForceDownlevel="False"
                                                    MouseOutHideDelay="1" MenuEffects-MouseOutHideDelay="500" BackgroundMenuImage="../images/bevel_inter.gif"
                                                    MenuEffects-MenuTransition="AlphaFadeBottomRight" Display="Horizontal" MenuEffects-ShadowStrength="4"
                                                    RootArrow="True">
                                                </cc1:SolpartMenu>
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
    <tr>
        <td height="30" valign="bottom">
            <table cellspacing="0" cellpadding="0" border="0" width="100%" height="100%">
                <tr>
                    <td width="30">
                        &nbsp;</td>
                    <td class="PageTitle">
                        <div id="hd">
                            <h1>
                                <asp:Literal ID="ltrlTitle" runat="server"></asp:Literal>
                            </h1>
                        </div>
                    </td>                                 
                </tr>
            </table>
        </td>
    </tr>
</table>
<!---
										<root>
											<menuitem id="1" title="Empresas">
												<menuitem id="2" title="Registro de Empresas" url="/BIFConvenios/Clientes/Clientes.aspx" />
												<menuitem id="3" title="Fechas de Cargo por Empresa" url="/BIFConvenios/Default.aspx" />
												<menuitem id="4" title="Actualizar Nuevos Convenios" url="/BIFConvenios/Clientes/ActualizarTablas.aspx" />
											</menuitem>
											<menuitem id="5" title="Cuotas por Cobrar">
												<menuitem id="6" title="Cargar Cuotas para Envío a Empresa" url="/BIFConvenios/CargaGeneracioncf.aspx"></menuitem>
												<menuitem id="7" title="Generar Archivo de Cuotas" url="/BIFConvenios/ReporteCronogramaFuturo.aspx" />
											</menuitem>
											<menuitem id="8" title="Cuotas Descontadas">
												<menuitem id="9" title="Cargar Cuotas Descontadas (Empresa)" url="/BIFConvenios/descuentos/ProcesarArchivoDescuento.aspx" />
												<menuitem id="10" title="Enviar Cuotas a IBS" url="/BIFConvenios/descuentos/ReporteProcesoDescuento.aspx" />
											</menuitem>
											<menuitem id="11" title="Consultas">
												<menuitem id="12" title="Consultar Envíos por Empresa/Periodo" url="/BIFConvenios/consultas/consultaEnvio.aspx" />
												<menuitem id="13" title="Consultar Pagos del Empleado" url="/BIFConvenios/consultas/ConsultaEnvioPorTrabajador.aspx" />
											</menuitem>
										</root>
--->
