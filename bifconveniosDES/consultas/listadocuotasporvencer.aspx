<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ListadoCuotasPorVencer" CodeFile="ListadoCuotasPorVencer.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Listado de Importes por vencer</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body topmargin="0" leftmargin="0">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" cellspacing="0" width="100%">
				<tr>
					<td>
						<asp:Repeater Runat="server" ID="drep">
							<ItemTemplate>
								<!--Encabezado-->
								<table border="0" cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td>
											<table border="0" cellpadding="0" cellspacing="0" width="100%">
												<tr>
													<td colspan="2">
														<br>
													</td>
												</tr>
												<tr>
													<td colspan="2" align="center"><b>LISTADO DE IMPORTES POR VENCER DE PRESTAMOS B.I.F. 
															CONVENIOS</b>
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<br>
													</td>
												</tr>
												<tr>
													<td width="200">
														Empresa
													</td>
													<td>
														<asp:Label id="Label1" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "CUSNA1")%>'>
														</asp:Label>
													</td>
												</tr>
												<tr>
													<td>
														Dirección
													</td>
													<td>
														<asp:Label id="Label2" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "CUSNA2")%>'>
														</asp:Label>
													</td>
												</tr>
												<tr>
													<td>&nbsp;</td>
													<td>
														<asp:Label id="Label3" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "CUSCTY")%>'>
														</asp:Label></td>
												</tr>
												<tr>
													<td colspan="2"><br>
													</td>
												</tr>
												<tr>
													<td>
														Atención
													</td>
													<td>
														<asp:Label id="Label4" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "NOMCNT")%>'>
														</asp:Label>
													</td>
												</tr>
												<tr>
													<td colspan="2"><br>
													</td>
												</tr>
												<tr>
													<td colspan="2">
														<table width="100%" cellpadding="0" cellspacing="0">
															<tr>
																<td><b> MONEDA&nbsp;:&nbsp; </b>
																</td>
																<td align="left"><b>
																		<asp:Label id="Label5" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "DLCCY")%>'>
																		</asp:Label>
																	</b>
																</td>
																<td>&nbsp;</td>
																<td><b>Nro. CTA. CTE.:</b></td>
																<td align="left">
																	<b>
																		<asp:Label id="Label6" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "DLCTA")%>'>
																		</asp:Label>
																	</b>
																</td>
																<td>&nbsp;</td>
																<td align="left">
																	<b>
																		<asp:Label id="Label7" runat="server" text='<%#DataBinder.Eval (Container.DataItem, "ST")%>'>
																		</asp:Label>
																	</b>
																</td>
																<td>
																	<b>
																		<asp:Label Runat=server ID=lblLabel18 text='<%#DataBinder.Eval (Container.DataItem, "AGC")%>'>
																		</asp:Label></b>
																</td>
															</tr>
														</table>
													</td>
												</tr>
											</table>
											<!--Fin Encabezado-->
										</td>
									</tr>
									<tr>
										<td>
											<asp:DataGrid id="dgListadoR" runat="server" AutoGenerateColumns="False" DataSource='<%#GetListadoCuotas(CType(DataBinder.Eval(Container.DataItem, "DLSTS"), String), CType(DataBinder.Eval(Container.DataItem, "DLCCY"), String), CType(DataBinder.Eval(Container.DataItem, "DLAGC"), DECIMAL))%>'>
												<Columns>
													<asp:BoundColumn DataField="DLNP" HeaderText="Número<br> Prestamo"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLCUN" HeaderText="Código"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLNE" HeaderText="Nombre<br> Empresa"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLOAM" HeaderText="Monto<br>Original" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#,#.00}"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLPRI" HeaderText="Saldo<br>Actual" DataFormatString="{0:#,#.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLCNC1" HeaderText="Nro. Cuotas<br>Pactadas" ItemStyle-HorizontalAlign="center"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLNCT" HeaderText="Nro. Cuotas<br>Pagadas" ItemStyle-HorizontalAlign="center"></asp:BoundColumn>
													<asp:BoundColumn DataField="CPENDIENTES" HeaderText="Nro.Cuotas<br>Pendientes" ItemStyle-HorizontalAlign="center"></asp:BoundColumn>
													<asp:BoundColumn DataField="FechaCargoCuenta" HeaderText="Fecha Cargo<br>En Cuenta" DataFormatString="{0:d}"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLIC" HeaderText="Importe<br>Cuota" DataFormatString="{0:#,#.00}" ItemStyle-HorizontalAlign="Right"></asp:BoundColumn>
													<asp:BoundColumn DataField="DLCM" HeaderText="Cod. Planilla<br>de la Empresa"></asp:BoundColumn>
												</Columns>
											</asp:DataGrid>
										</td>
									</tr>
									<tr>
										<td>
											<asp:Repeater Runat=server ID=rpResumen DataSource='<%#GetResumenListado(CType(DataBinder.Eval(Container.DataItem, "DLSTS"), String), CType(DataBinder.Eval(Container.DataItem, "DLCCY"), String), CType(DataBinder.Eval(Container.DataItem, "DLAGC"), DECIMAL))%>'  >
												<ItemTemplate>
													<table border="0" cellpadding="0" cellspacing="0">
														<tr>
															<td>
																<b>TOTAL MONEDA</b>&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;
															</td>
															<td>
																<asp:Label Runat=server ID=lblMoneda  text='<%# DataBinder.Eval (Container.DataItem, "COUNTER")%>' >
																</asp:Label>
																&nbsp;</td>
															<td>
																<b>TOTAL MONTO ORIGINAL</b>&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;
															</td>
															<td>
																<asp:Label Runat=server ID="Label8"  text='<%#DataBinder.Eval (Container.DataItem, "SOAM")%>'>
																</asp:Label>
																&nbsp;</td>
															<td>
																<b>TOTAL SALDO ACTUAL</b>&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;
															</td>
															<td>
																<asp:Label Runat=server ID="Label9"  text='<%#DataBinder.Eval (Container.DataItem, "SPRI")%>'>
																</asp:Label>
																&nbsp;
															</td>
															<td>
																<b>TOTAL IMPORTE CUOTA</b>&nbsp;&nbsp;&nbsp;&nbsp;:&nbsp;&nbsp;
															</td>
															<td>
																<asp:Label Runat=server ID="Label10"  text='<%#DataBinder.Eval (Container.DataItem, "SIC")%>'>
																</asp:Label>
															</td>
														</tr>
													</table>
												</ItemTemplate>
											</asp:Repeater>
										</td>
									</tr>
								</table>
							</ItemTemplate>
						</asp:Repeater>
						<!--Fin Encabezado-->
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
