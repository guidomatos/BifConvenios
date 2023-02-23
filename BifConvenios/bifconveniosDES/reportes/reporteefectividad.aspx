<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteefectividad.aspx.vb" Inherits="BIFConvenios.reporteefectividad" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head >
        <title>Reporte de Efectividad</title>
        <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">    
		<script language=javascript src="<%Response.Write(Request.ApplicationPath)%>/js/global.js" type=text/javascript></script>
		<LINK href="<%=Request.ApplicationPath%>/css/global.css" type=text/css rel=stylesheet>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

		
	    <script language="javascript" type="text/javascript">
    
             function openBusqueda(){                            
                                
                 var returnValue = OpenFormatPageDialog('../reportes/buscarEmpresaIBS.aspx',400,875);                                

                if (fctTrim(returnValue) != ''){

                    document.all('txtCodigoIBS').value = getvalue(returnValue, 1 , '|');       
                    document.all('hdCodigoEmpresa').value = getvalue(returnValue, 1 , '|');       
                    document.all('txtNombreEmpresa').value = getvalue(returnValue, 2 , '|');                                               	                                 
                    
                        
                }
             }
            
           
            function OpenFormatPageDialog(url, height , width ) {
					var returnValue = window.showModalDialog(url,'', 'dialogTop: 150px; dialogLeft: 150px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
					if (typeof (returnValue) == "undefined"){
						returnValue = '';
					}
					return returnValue;
			}
			
		function validar()
        {                                                          
			
			if ( fctTrim (document.all("txtCodigoIBS").value ) == '' ) {
				alert ('Ingrese el Código IBS');
				document.all("txtCodigoIBS").focus();
				args.IsValid = false;
				return;
			}							
		 
        }    
        
				
    </script>

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
								<td width="30">&nbsp;</td>
								<td colSpan="2">&nbsp;
								    <table class="InputField" cellSpacing="0" cellPadding="0" width="73%" border="0">
								         <tr>
                                              <td colspan="6">&nbsp;
                                              </td>
                                          </tr> 
								        <tr>
								            <td width="75">&nbsp;</td>								        
								            <td class="SubHead" width="75">
								                <asp:Label ID="Label1" runat="server" Text="Codigo IBS" Font-Bold="True"></asp:Label>
								            </td>
								            <td width="75">&nbsp;</td>
								            <td width="75">
                                                
                                                <asp:TextBox id="txtCodigoIBS" runat="server" CausesValidation="True"></asp:TextBox>
                                                <input id="hdCodigoEmpresa" type="hidden" name="hdCodigoEmpresa" runat="server"/>
                                                
                                            </td> 
								            <td width="75">&nbsp;</td>                                            
                                            <td></td> 
                                          </tr> 
                                          <tr>
                                            <td>&nbsp;</td>								        
                                            <td class="SubHead" width="40">
								                <asp:Label ID="lblEmpresa" runat="server" Text="Empresa" Font-Bold="True"></asp:Label>
								            </td>
								             <td ></td>
								             <td width="85">
								                <asp:textbox id="txtNombreEmpresa" Runat="server" MaxLength="100"  Width="300px"></asp:textbox>
       											 
								             </td>                                            
								             <td>
								             &nbsp;&nbsp;
								             <a href="javascript:openBusqueda()"><img alt="buscar" src="../images/texto.gif" border="0"></a>
								             &nbsp;&nbsp;
								             <asp:Button ID="Button1" runat="server" Text="Buscar" CssClass="box"/>
								                
								             </td>
                                          </tr>
                                          <tr>
                                          <td width="75">&nbsp;</td>
                                          <td colspan="5">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCodigoIBS" ErrorMessage="Ingrese el codigo IBS del cliente"></asp:RequiredFieldValidator>                                                
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtCodigoIBS" ErrorMessage="Ingrese un numero valido como codigo IBS" ValidationExpression="\d*"></asp:RegularExpressionValidator>                                        
                                                                                        
                                            </td> 
                                          </tr> 
                                        </table> 
                                    <br />
                                 </td>
                                 <td width="30" style="height: 63px" >&nbsp;</td>
							</tr>							
							<tr>
							<td width="90" >&nbsp;
					            
							</td>
							<td colspan="2">                
                                &nbsp;&nbsp;
                        <div class="tabla">
                            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Size="12pt" />
                            <asp:GridView ID="gridEfectividad" runat="server" AutoGenerateColumns="False" AllowPaging="True"  >            
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>                      
                                                <%# esVisible(Databinder.Eval(Container, "DataItemIndex")) %>
                                                        
                                                        <%#DataBinder.Eval(Container.DataItem, "AnioMes")%>
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
                       <td width="30">&nbsp;</td>
                    </tr>  
							<tr>
								<td width="30" style="height: 19px" >&nbsp;</td>
								<td colSpan="2" align="left" >
								    <br />
                                    <div runat="server" id="pnlexportar" visible ="false" >
										        <table border="0" cellpadding="2" cellspacing="0" class="box" >
										            <thead>
											            <tr >												
												            <th >
													            <img alt ="Exportar Excel" src="<%=Request.ApplicationPath%>/images/bar_begin.gif" height="17">
													        </th>
												            <th>
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
                                <td width="30">&nbsp;</td>
							</tr>                       
                   </table>                               
                  </td>
            </tr>   
            
           </table>     
    </form>
</body>
</html>
