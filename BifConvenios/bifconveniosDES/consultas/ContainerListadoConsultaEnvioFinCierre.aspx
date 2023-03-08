<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContainerListadoConsultaEnvioFinCierre.aspx.vb" Inherits="BIFConvenios.ContainerListadoConsultaEnvioFinCierre" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
        <title>BIFConvenios - Listado de Importes por Vencer</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		
		<script type="text/javascript">
		/* ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS */
			function EnviarReporte(idx){
			    document.all('hdId').value = idx;
				__doPostBack('lnkExportarArchivoDescuentos','');
			}
		/* END */	
		</script>
</head>
<body leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tbody>
					<tr>
						<td><uc1:banner id="Banner1" title="" runat="server"></uc1:banner></td>
					</tr>
				</tbody>
			</table>
			<!-- ADD NCA 08/07/2014 EA2013-273 OPT. CONVENIOS  -->
			<table border="0" cellpadding="2" cellspacing="0" class="box">
			<thead>
			    <tr>
			        <th>
						<img alt="" src="<%= ResolveUrl("~/images/bar_begin.gif") %>" height="17"/>
					</th>
			        <th>
			            <a href="javascript:EnviarReporte('<%=Request.Params("idx")%>');">Exportar Archivo Descuentos</a>
					    <asp:LinkButton Runat="server" ID="lnkExportarArchivoDescuentos"></asp:LinkButton>
			        
			        </th>
			        <th>
						<img alt="" src="<%= ResolveUrl("~/images/bar_end.gif") %>" height="17"/>
				    </th>
			        <th>
			        </th>
			    </tr>
			</thead>
			</table>
			<rsweb:ReportViewer ID="ReportViewer1" runat="server">
            </rsweb:ReportViewer>
			
			<!-- END -->
			    
			<!-- ADD NCA 08/07/2014 EA2013-273 OPT. CONVENIOS  -->
			<input id="hdId" type="hidden" name="hdId" runat="server"/>
			<!-- END -->
		</form>
	</body>
</html>
