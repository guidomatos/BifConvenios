<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteefectividadmes.aspx.vb" Inherits="BIFConvenios.reporteefectividadmes" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head >
        <title>Reporte de Efectividad por mes</title>
        <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">    
		 <%--<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>--%>
    <LINK href="<%= ResolveUrl("~/css/global.css") %>" type="text/css" rel="stylesheet" />
    <%--<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>--%>
    <script language="javascript" src="<%= ResolveUrl("~/js/global.js") %>" type="text/javascript"></script>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body leftMargin="0" topMargin="0">
    <form id="form1" runat="server">
		<table cellSpacing="0" cellPadding="0" width="100%" border="0">
			<tr>
				<td><uc1:banner id="Banner1" title="Reporte de Efectividad" runat="server"></uc1:banner></td>
			</tr>
		    <tr>  
                <td >
						<table id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td width="30">&nbsp;</td>
								<td colSpan="2">&nbsp;</td>
								<td width="30">&nbsp;</td>								
							</tr>
							<tr>
								<td width="30" >&nbsp;</td>
								<td colSpan="2">&nbsp;
								    <table class="InputField" cellSpacing="0" cellPadding="0" width="63%" border="0">
								         <tr>
								          <TD width="75">&nbsp;</TD>
                                          <td colspan=3>&nbsp;
                                          </td>		
                                          </tr>
                                          <tr>
                                          <TD width="75">&nbsp;</TD>
                                          <td class="SubHead" width="75">						
								            <asp:Label ID="Label1" runat="server" Text="Periodo" Font-Bold="True"></asp:Label>
								          </td> 
								           <TD width="75">&nbsp;</TD>
								           <td>
                                            <asp:DropDownList ID="ddlPeriodo" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                           </td>                                         
                                          </tr> 
								         <tr>
								         <TD width="75">&nbsp;</TD>
                                          <td colspan="3">&nbsp;
                                          </td>		
                                          </tr>                                          
                                   </table> 
                                </td>
                                <td width="30" >&nbsp;</td>
							</tr>							
							<tr>
							<td width="30">&nbsp;</td>
							<td colspan = 2>                
                        &nbsp;&nbsp;
                        <div class="tabla">
                            <asp:GridView ID="gridEfectividad" runat="server" AutoGenerateColumns="False" AllowPaging="True"  >            
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>                      
                                                <%# esVisible(Databinder.Eval(Container, "DataItemIndex")) %>
                                                        
                                                        <%#DataBinder.Eval(Container.DataItem, "codigo_clienteIBS")%>
                                                        </td>                                                         
                                                        <td>
                                                        <%#DataBinder.Eval(Container.DataItem, "Nombre_empresa")%>
                                                        </td> 
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_clientes_enviado")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_monto_enviado_soles")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_monto_enviado_dolares")%>
                                                        </td>  
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_clientes_retornado")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_monto_retornado_soles")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "nro_monto_retornado_dolares")%>
                                                        </td>  
                                                        <td><%#DataBinder.Eval(Container.DataItem, "efectividad_nro_clientes")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "efectividad_monto_soles")%>
                                                        </td>
                                                        <td><%#DataBinder.Eval(Container.DataItem, "efectividad_monto_dolares")%>
                                                        
                                                 <%#esVisible2(Databinder.Eval(Container, "DataItemIndex")) %>
                                            
                                        </ItemTemplate>                        
                                    </asp:TemplateField>
                                </Columns>
                                <AlternatingRowStyle CssClass="odd" />
                            </asp:GridView>
                            </div> 
                       </td>
                            <td width="30" >&nbsp;</td>
                    </tr>  
							<tr>
								<td width="30">&nbsp;</td>
								<td colSpan="2">
								<br />
                                    <div runat="server" id="pnlexportar" visible ="false" >
										        <table border="0" cellpadding="2" cellspacing="0" class="box" >
										            <thead>
											            <tr >												
												            <th >
													            <img alt ="Exportar Excel" src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17">
													        </th>
												            <th >
													          <asp:LinkButton ID="LinkButton1" runat="server">Exportar a Excel</asp:LinkButton>
												            </th>
												            <th >
													            <img alt ="Exportar Excel" src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17">
													        </th>
											            </tr>
										            </thead>
									            </table>   
									  </div>
                                </td>
                                <td width="30" >&nbsp;</td>
							</tr>                       
                   </table>                               
                  </td>
            </tr>   
            
           </table>     
    </form>
</body>
</html>