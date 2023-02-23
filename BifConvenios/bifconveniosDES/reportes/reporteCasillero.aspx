<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reporteCasillero.aspx.vb" Inherits="reportes_reporteCasillero"  EnableEventValidation="false"%>

<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>

<head runat="server">

    <title>Reporte Casilleros</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
	<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>	
       
    <link href="../css/global.css" rel="stylesheet" type="text/css" />    
    <script src ='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
    
    <script language="javascript" type="text/javascript">
    
            
             function openBusqueda(){                            
                               
                 
                 var returnValue = OpenFormatPageDialog('../reportes/buscarEmpresaIBS.aspx',400,875);                                                            

                if (fctTrim(returnValue) != ''){
                    
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
			
			if ( fctTrim (document.all("txtNombreEmpresa").value ) == '' ) {
				alert ('Ingrese el nombre de la empresa');
				document.all("txtNombreEmpresa").focus();
				args.IsValid = false;
				return;
			}							
		 
        }            
        		
    </script>	
    
    <style type="text/css">
    
    .reporte
    {
       display:table;
       margin: 20px 31px;
        
    }        

    </style>
	
</head>

<body leftMargin="0" topMargin="0"  rightMargin="0">
    
    <form id="Form1" method="post" runat="server">
    
        <div id="filtro">            
        
        	<table cellSpacing="0" cellPadding="0" width="100%" border="0">        	
        	    
        	    <tr>
					<td><uc1:banner id="Banner1" title="Reporte Casilleros" runat="server"></uc1:banner></td>
				</tr>
        	    <tr>
					<td></td>
				</tr>
				<tr>
				    <td>
				         <table cellspacing="0" width="650" border="0">
				            <tr>
				                <td width="30">&nbsp;</td>
				                 <td vAlign="top">				                 
    		                        <table class="InputField" cellSpacing="0" cellPadding="5" width="100%" border="0">
		                                 <tr>
									        <td colspan="3">&nbsp;</td>
								         </tr>          
		                                 <tr>
		                                     <td>Periodo</td>
     									     <td colspan="2">
     									        <asp:DropDownList id="ddlAnio" runat="server" AutoPostBack="True" Width="200px" DataValueField="Anio_Periodo" DataTextField="Anio_Periodo"></asp:DropDownList>
     									     </td>
		                                 </tr>  		     
		                                 <tr>
		                                    <td>
		                                        Mes
		                                    </td>
		                                    <td>
	                                        	<asp:DropDownList id="ddlMes" runat="server" Width="200px">
	                                        	    <asp:ListItem Text="Enero" Value="1"></asp:ListItem>
	                                        	    <asp:ListItem Text="Febrero" Value="2"></asp:ListItem>
	                                        	    <asp:ListItem Text="Marzo" Value="3"></asp:ListItem>
	                                        	    <asp:ListItem Text="Abril" Value="4"></asp:ListItem>
	                                        	    <asp:ListItem Text="Mayo" Value="5"></asp:ListItem>
	                                        	    <asp:ListItem Text="Junio" Value="6"></asp:ListItem>
	                                        	    <asp:ListItem Text="Julio" Value="7"></asp:ListItem>
	                                        	    <asp:ListItem Text="Agosto" Value="8"></asp:ListItem>
	                                        	    <asp:ListItem Text="Septiembre" Value="9"></asp:ListItem>
	                                        	    <asp:ListItem Text="Octubre" Value="10"></asp:ListItem>
	                                        	    <asp:ListItem Text="Noviembre" Value="11"></asp:ListItem>
	                                        	    <asp:ListItem Text="Diciembre" Value="12"></asp:ListItem>
	                                        	    
	                                        	</asp:DropDownList>
		                                    </td>
		                                 </tr>                           	                        
                                        <tr>
                                            <td>Empresa</td>
                                            <td>
											     <asp:textbox id="txtNombreEmpresa" Runat="server" MaxLength="100" Columns="60" ReadOnly="false"></asp:textbox>											
											     <input id="hdCodigoEmpresa" type="hidden" name="hdCodigoEmpresa" runat="server"/>																		
											     <a href="javascript:openBusqueda()"><img alt="buscar" src="../images/texto.gif" border="0"></a>											      
											</td>											
											<td align="right">
											    <asp:linkbutton id="lnkBuscar" runat="server" Text="<img src='/BIFConvenios/images/procesar.jpg' name='Image1' border=0 alt='Buscar'/>"  OnClientClick="validar();"></asp:linkbutton>&nbsp;
											</td>											
                                        </tr>		                                 
                                        <tr>
                                            <td colspan="3" align="right">
                                                    <asp:linkbutton id="lnkBack" Runat="server"><img src='/BIFConvenios/images/regresar.jpg' name='Image1' border="0" alt='Regresar' /></asp:linkbutton>                    
                                            </td>
                                        </tr>
	    	                        </table>				                    				                 
				                 </td>				            
				            </tr>				         
				         </table>				    
				    </td>				    
				</tr>       	    				
				<tr>
				    <td align="left">
				        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblMensaje" runat="server" ForeColor="Red" Font-Size="12pt" />				    	                    
	                </td>		
				</tr>
				<tr>
				    <td>
                        <asp:Panel ID="PnlReporte" runat="server" CssClass="reporte">
                        
                            <div id="resumen">
                            <table class="InputField">
                             <tr>                    
                                <td style="width:350px;">
                                    Empresa&nbsp;:
					                <asp:label CssClass="Text" id="lblEmpresa" Runat="server"></asp:label>
				                </td>
				                <td style="width:300px;">
				                    Total a Pagar&nbsp;:
				                    <asp:label CssClass="Text" id="lblTotalPagar" Runat="server"></asp:label>
				                </td>
				                <td style="width:116px;">
				                    &nbsp;&nbsp;&nbsp;&nbsp;
				                    <asp:LinkButton Runat="server" ID="lnkExportar" CausesValidation="False">Exportar Excel</asp:LinkButton>
				                </td>
				                
                                </tr>                    
                          </table>                    
                        </div>						    
                        
                            <div class="tabla">
                                
                                <asp:GridView ID="dgDetalleCasillero" runat="server" CellPadding="3" CellSpacing="3"  BorderWidth="1px" Width="780" AutoGenerateColumns="False" 
                                               Visible="true" ShowFooter="True"  AllowPaging="True" PageSize="15">
                                                                               
                                        <Columns>
                                        
                                            <asp:BoundField DataField="DEAACC" HeaderText="Pagaré" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="center" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="CUSNA1" HeaderText="Cliente" ItemStyle-Width="250px" HeaderStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="left" />
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="DLSAMT" HeaderText="Tarifa a Pagar" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:F}">
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:BoundField>
                                            
                                            <asp:BoundField DataField="DLSPAD" HeaderText="Tarifa Pagada" ItemStyle-Width="100px"  ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:F}">
                                                <ItemStyle HorizontalAlign="Center"/>
                                            </asp:BoundField>                                            
                                                                                        
                                        </Columns>
                                        
                                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="Primero" LastPageText="Ultimo" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />                                                                                                                                                         
                                        <RowStyle CssClass="TablaNormalBIF" VerticalAlign="Top"/>
			                            <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35" ForeColor="black"></HeaderStyle>
			                            <FooterStyle  HorizontalAlign ="Right" BorderWidth="0px"   BorderColor="#ffffff"></FooterStyle>                                        
                                        <AlternatingRowStyle CssClass="odd"/>
                                                                                
                                    </asp:GridView>				     
                            
                            </div>
                        
                        </asp:Panel>
				        
				    </td>		            
				    
				</tr>
				
        	</table>        
        </div>
                
        
    </form>
    
</body>
</html>
