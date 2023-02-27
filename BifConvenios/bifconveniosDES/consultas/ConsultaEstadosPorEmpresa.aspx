<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConsultaEstadosPorEmpresa.aspx.vb" Inherits="BIFConvenios.consultas_ConsultaEstadosPorEmpresa" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title>BIFConvenios - Consulta de Envio por Empleado</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">

		    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />

		<LINK href="../css/global.css" type="text/css" rel="Stylesheet">
		<script language=javascript  type=text/javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"></script>
	</head>
	<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<TBODY>
					<tr>
						<td><uc1:banner id="Banner1" title="Consulta Estados por Empresa" runat="server"></uc1:banner></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
					</tr>
					<tr>
						<td>
							<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
								<TBODY>
									<TR>
										<TD width="30">&nbsp;</TD>
										<TD colSpan="2">
											<TABLE class="InputField" height="100" cellSpacing="0" cellPadding="0" border="0">
												<TR>
													<TD width="70">&nbsp;</TD>
													<TD width="80">&nbsp;</TD>
													<TD>&nbsp;</TD>
												</TR>
												<TR>
													<TD width="30">&nbsp;</TD>
													<td colspan="2">
														<table border="0" cellpadding="2" cellspacing="0" width="90%">
															<tr>
																<TD class="SubHead" width="50">Empresa</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="650" colspan="5">
                                                                    <asp:TextBox ID="txtEmpresa" runat="server"></asp:TextBox></TD>
															</tr>
															<TR>
																<TD class="SubHead" width="50">
                                                                    Funcionario</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="170">
																	<asp:DropDownList id="ddlFuncionario" runat="server" DataTextField="Descripcion" DataValueField="Codigo" Width="200px" AutoPostBack="True"></asp:DropDownList></TD>
																<TD width="30">&nbsp;</TD>
																<TD>
																	</TD>
															</TR>
															<tr>
																<TD class="SubHead" width="50">
                                                                    Periodo</TD>
																<TD width="75">&nbsp;</TD>
																<TD align="left" width="650" >
                                                                    <asp:DropDownList id="ddlMes" runat="server" Width="200px" AutoPostBack="True" DataValueField="MonthOrder" DataTextField="MonthName"></asp:DropDownList></TD>
															    <TD width="75">&nbsp;</TD>
																<TD align="left" width="650" >
                                                                    <asp:TextBox ID="txtAnio" runat="server" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtMes" runat="server" Visible="False"></asp:TextBox>
                                                                    <asp:TextBox ID="txtIdFuncionario" runat="server" Visible="False"></asp:TextBox> 
                                                                    <asp:TextBox ID="txtNombreEmpresa" runat="server" Visible="False"></asp:TextBox>                                                                                                                                         
                                                                </TD>
															</tr>
															<tr>
															    <td colspan="4" align="left"><asp:Label id="lblMensaje" runat="server" ForeColor="Red"></asp:Label></td>
																<td colspan="3" align="right" height="30" valign="middle" class="SubHead">
                                                                    &nbsp;<asp:LinkButton Runat="server" ID="lnkBuscar">Buscar</asp:LinkButton>
																</td>
															</tr>
														</table>
                                                    </td>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
										<TD width="30" style="height: 19px" >&nbsp;</TD>
										<TD colSpan="2" align="left" style="height: 19px">
										    <div runat="server" id="botonera" visible ="false" >
										        <table border="0" cellpadding="2" cellspacing="0" class="box" >
										            <thead>
											            <tr >												
												            <th>
													            <img alt ="Exportar Excel" src="../images/bar_begin.gif" height="17">
													        </th>
												            <th>
													            <asp:LinkButton Runat="server" ID="exportar">Exportar a Excel</asp:LinkButton>
												            </th>
												            <th>
													            <img alt ="Exportar Excel" src="../images/bar_begin.gif" height="17">
													        </th>
											            </tr>
										            </thead>
									            </table>
									        </div>
										</TD>
									</TR> 
									<TR>
										<TD width="30">&nbsp;</TD>
									</TR> 
									<TR>
										<TD width="30">&nbsp;</TD>
										<TD colSpan="2" align="right">
											<TABLE cellSpacing="0" width="90%" cellPadding="0" border="0">
											    <TR>
													<TD width="30">&nbsp;</TD>
													<TD colSpan="2">
											            <div id="dvData" name="dvData" runat="server" visible="false" >
                                                        <div class="tabla">
                                                            <asp:datagrid id="dgConsultaEstado" runat="server" CellPadding="3" CellSpacing="3"  BorderWidth="1px" Width="720px" AutoGenerateColumns="False" Visible="False" PagerStyle-Position="TopAndBottom"   AllowPaging="True">
					                                            <ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
					                                            <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35px"></HeaderStyle>
					                                            <FooterStyle  HorizontalAlign ="Right" BorderWidth="0px"   BorderColor="White"  ></FooterStyle>
					                                            <AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>	

					                                            <Columns>
						                                            <asp:BoundColumn DataField="codigo_IBS" HeaderText="C&#243;digo IBS">
                                                                        <ItemStyle Width="50px" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Nombre_cliente" HeaderText="Empresa">
                                                                        <ItemStyle Width="120px" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_Envio_Planilla" HeaderText="Fecha de Env&#237;o">
                                                                        <ItemStyle Width="40px" HorizontalAlign="center"/>
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_Pago" HeaderText="Fecha de Pago">
                                                                        <ItemStyle Width="40px" HorizontalAlign="center" />
                                                                    </asp:BoundColumn>
                                                                    <asp:BoundColumn DataField="Meses_Anticipacion" HeaderText="Meses de Anticipaci&#243;n">
                                                                        <ItemStyle Width="40px" HorizontalAlign="center" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_Enviado" HeaderText="Enviado">
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_Recibido" HeaderText="Recibido">
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_Procesado" HeaderText="Procesado">
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:BoundColumn>
						                                            <asp:BoundColumn DataField="Fecha_PostConciliacion" HeaderText="Conciliado">
                                                                        <ItemStyle Width="30px" />
                                                                    </asp:BoundColumn> 
					                                            </Columns>
					                                            <PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
				                                            </asp:datagrid>
                                                        </div> 
                                                        </div> 
										            </TD>
												</TR>
											</TABLE>
										</TD>
									</TR>
									<TR>
									    <TD width="30">&nbsp;</TD>
									</TR> 
									<TR>
								        <TD>&nbsp;</TD>
								        <TD colSpan="2">
								            <div id="numRegistros" visible ="false" runat="server">
									        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										        <TR>
											        <TD class="SubHead" width="150">Número de Registros
											        </TD>
											        <TD class="Text">
												        <asp:Label id="lblNumReg" Runat="server"></asp:Label></TD>
										        </TR>
									        </TABLE>
									        </div>
								        </TD>
							        </TR>
									<TR>
										<TD width="30">&nbsp;</TD>
									</TR> 
								</TBODY>
							</TABLE>
                            </TD>
					</TR>
				</TBODY>
			</TABLE>
		</form>
		
	</body>
</HTML>
