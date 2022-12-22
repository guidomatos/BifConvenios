<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.Clientes" CodeFile="Clientes.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="~/controls/banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Registro de Empresas</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" 
type=text/css rel=stylesheet>
		<script language=javascript 
src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type=text/javascript></script>
		<script language="javascript">
<!--
		function EliminaCliente ( id, name ) {
			if ( confirm ( '¿Desea eliminar el registro de la Empresa : ' + name +  ' ?' )) {
					document.all("hdData").value = id;
				__doPostBack('lnkDelete','');		
			
			}
		}

-->
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" rightmargin="0" onload="MM_preloadImages('/BIFConvenios/images/buscar_on.jpg')">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" runat="server" Title="Registro de Empresas"></uc1:banner></td>
				</tr>
				<tr>
					<td>
						<table border="0" cellpadding="0" cellspacing="0" width="775">
							<tr>
								<td width="30">&nbsp;</td>
								<td width="700">
									<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
										<TR>
											<TD valign="top">
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="708" border="0">
													<tr>
														<td><br>
														</td>
													</tr>
													<TR>
														<TD align="left" class="InputField" height="60">
															<table border="0" cellpadding="0" cellspacing="0" width="100%">
																<tr>
																	<td width="30">&nbsp;</td>
																	<TD width="125" class="SubHead" valign="top">
																		Nombre de la Empresa
																	</TD>
																	<TD align="left" width="460">
																		<table border="0" cellpadding="0" cellspacing="0">
																			<tr>
																				<td valign="top"><asp:textbox id="txtNombreCliente" Width="420" runat="server" BorderWidth="1pt" BorderColor="Silver"></asp:textbox></td>
																				<td>&nbsp;&nbsp;
																					<asp:LinkButton id="lnkBuscar" runat="server" Text="<img src='/BIFConvenios/images/buscar.jpg' name='Image1' border=0 alt='Procesar archivo'/>"></asp:LinkButton>&nbsp;</td>
																			</tr>
																			<tr>
																				<td colspan="2" align="right">
																					<A href="editarcliente.aspx" class="CommandButton">Agregar Empresa</A>&nbsp;&nbsp;
																					<asp:linkbutton id="lnkDelete" runat="server"></asp:linkbutton><INPUT id="hdData" type="hidden" runat="server" NAME="hdData">
																				</td>
																			</tr>
																		</table>
																	</TD>
																</tr>
															</table>
														</TD>
													</TR>
													<TR>
														<TD colspan="2">&nbsp;</TD>
													<TR>
														<TD colspan="2" align="right">
															<!--<DIV id="dvDia" class="DRCuerpoNormal" style="OVERFLOW: auto; WIDTH: 750px; HEIGHT: 200px" width="100%">-->
												<div class="tabla">															
																<asp:datagrid id="dgClientes"  CellPadding="3" CellSpacing="3"  BorderWidth="1px" runat="server" AutoGenerateColumns="False" Width="730px">
																	<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
																	<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
																	<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
																	<Columns>
																		<asp:BoundColumn DataField="Codigo_Cliente" HeaderText="C&#243;digo Empresa">
																			<HeaderStyle Width="70px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="Nombre_Cliente" HeaderText="Nombre Empresa">
																			<HeaderStyle Width="140px"></HeaderStyle>
																		</asp:BoundColumn>
																		<asp:BoundColumn DataField="TipoDocumento" HeaderText="Tipo Documento" ItemStyle-Width="200" HeaderStyle-Width="80"></asp:BoundColumn>
																		<asp:BoundColumn DataField="NumeroDocumento" HeaderText="N&#250;m Documento"></asp:BoundColumn>
																		<asp:TemplateColumn ItemStyle-Width="40" HeaderStyle-Width="40">
																			<ItemTemplate>
																				<A href='editarcliente.aspx?id=<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>'>
																					Editar Empresa</A>
																			</ItemTemplate>
																		</asp:TemplateColumn>																		
																		<asp:TemplateColumn ItemStyle-Width="40" HeaderStyle-Width="40">
																			<ItemTemplate>
																				<A href='editarcoordinadorcliente.aspx?id=<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>'>
																					Editar Coordinadores</A>
																			</ItemTemplate>
																		</asp:TemplateColumn>																		
																		<asp:TemplateColumn ItemStyle-Width="40" HeaderStyle-Width="40">
																			<ItemTemplate>
																				<asp:Panel runat=server id=pnlDel Visible='<%#Databinder.Eval (Container.DataItem, "CanDelete")%>'>
																					<A href='JavaScript:EliminaCliente("<%#Databinder.Eval (Container.DataItem, "Codigo_Cliente")%>", "<%#Databinder.Eval (Container.DataItem, "Nombre_Cliente").Replace("'", "")%>");'>
																						Eliminar Empresa</A>
																				</asp:Panel>
																			</ItemTemplate>
																		</asp:TemplateColumn>
																	</Columns>
																</asp:datagrid>
															<!--</DIV>-->
															</div>
														</TD>
													</TR>
													<tr>
														<td colspan="2">&nbsp;</td>
													</tr>
													<tr>
														<td colspan="2">
															<table border="0" cellpadding="0" cellspacing="0">
																<tr>
																	<td class="SubHead">
																		Número de Registros&nbsp;:&nbsp;
																	</td>
																	<td class="Text">
																		<asp:Label Runat="server" ID="lblNumReg"></asp:Label>
																	</td>
																</tr>
															</table>
														</td>
													</tr>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
