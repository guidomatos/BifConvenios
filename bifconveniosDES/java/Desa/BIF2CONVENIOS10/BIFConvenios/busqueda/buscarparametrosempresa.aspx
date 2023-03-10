<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.BuscarParametrosEmpresa" CodeFile="BuscarParametrosEmpresa.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Selecci?n de Empresa y Periodo</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<BASE TARGET="_self">
		<script language="javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" 
type="text/javascript"></script>
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel=stylesheet>
		<script language="javascript">
<!--
	function Seleccionar(){
		if ( getSelectedRadio ( Form1.document.all ("rData") ) !=-1){
			top.returnValue = getSelectedRadioValue( Form1.document.all ("rData") ); 
			window.close();
		}
		else{
			alert ( 'Seleccione una empresa y periodo');
		}
		
	}
	
	function Cancelar(){
		window.close();
	}
-->
		</script>
	</HEAD>
	<body topmargin="10" leftmargin="15">
		<form id="Form1" method="post" runat="server">
			<table border="0" cellpadding="0" width="100%">
				<TR>
					<TD class="SubHead">A?o</TD>
					<TD width="650">
						<asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" DataTextField="Anio_periodo" DataValueField="Anio_periodo" Width="200px"></asp:DropDownList></TD>
				</TR>
				<TR>
					<TD class="SubHead">Mes</TD>
					<TD width="650">
						<asp:DropDownList id="ddlMes" runat="server" AutoPostBack="True" DataTextField="MonthName" DataValueField="MonthOrder" Width="200px"></asp:DropDownList></TD>
				</TR>
				<tr>
					<td colspan="2" class="SubHead">
						Empresas
					</td>
				</tr>
				<tr>
					<td colspan="2" align="center">
						<table border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td class="CommandButton"><a href="javascript:Seleccionar();">Seleccionar</a>&nbsp;</td>
								<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:DataGrid id="dgDatos" runat="server" Width="100%" CellPadding="4" BorderWidth="1" AutoGenerateColumns="False">
							<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
							<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<input type="radio" id="rData" name="rData" value='<%#DataBinder.Eval(Container.DataItem,"Codigo_proceso")%>|<%#DataBinder.Eval(Container.DataItem,"Anio_periodo")%>|<%#DataBinder.Eval(Container.DataItem,"Mes_Periodo")%>|<%#DataBinder.Eval(Container.DataItem,"Codigo_Cliente")%>|<%#DataBinder.Eval(Container.DataItem,"Nombre_Cliente")%>|<%#BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval (Container.DataItem, "Mes_Periodo"))%> <%#DataBinder.Eval (Container.DataItem, "Anio_periodo")%>|<%#DataBinder.Eval (Container.DataItem, "TipoDocumento")%>|<%#DataBinder.Eval (Container.DataItem, "NumeroDocumento")%>|<%#DataBinder.Eval (Container.DataItem, "FInicial")%>|<%#DataBinder.Eval (Container.DataItem, "FFinal")%>'>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="Nombre_Cliente" HeaderText="Nombre Cliente"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Periodo">
									<ItemTemplate>
										<%#BIFConvenios.Periodo.GetMonthByNumber ( DataBinder.Eval (Container.DataItem, "Mes_Periodo"))%>
										<%#DataBinder.Eval (Container.DataItem, "Anio_periodo")%>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:DataGrid>
					</td>
				</tr>
				<TR>
					<TD colspan="2">
						<span class="SubHead">N?mero de Registros&nbsp;:&nbsp;</span>
						<asp:Label id="lblNumReg" Width="10" CssClass="Text" Runat="server"></asp:Label></TD>
				</TR>
				<tr>
					<td colspan="2" align="center">
						<table border="0" cellspacing="0" cellpadding="0">
							<tr>
								<td class="CommandButton"><a href="javascript:Seleccionar();">Seleccionar</a>&nbsp;</td>
								<td class="CommandButton"><a href="javascript:Cancelar();">Cancelar</a></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
