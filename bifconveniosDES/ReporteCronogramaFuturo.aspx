<%@ Register TagPrefix="uc1" TagName="Banner" Src="controls/Banner.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ReporteCronogramaFuturo"
    CodeFile="ReporteCronogramaFuturo.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>BIFConvenios - Generación Archivo de Cobranzas</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="<%=Request.ApplicationPath%>/css/global.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="<%Response.Write(Request.ApplicationPath)%>/js/global.js"></script>

    <script type="text/javascript">
		<!--
			//rutina de eliminacion de la informacion de proceso
			function EliminaProceso(id, nombre, anio, mes, fechaProcesoAS400) {
				if (confirm ( '¿Desea eliminar la información de proceso actual para la empresa: ' + nombre + '\ndel periodo: ' + mes + ' - ' + anio + ', procesado en IBS el '+ fechaProcesoAS400 + '?' )) {
					document.all('hdData').value = id;
					__doPostBack('lnkDeleteProcess','');
				}
			}
			
			function GenerarArchivo (id, nombre, anio, mes, fechaProcesoAS400){
				var returnValue = OpenFormatPageDialog('selectFileFormat.aspx?id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 250,500 );
				//alert(returnValue)				
				if ( fctTrim(returnValue) != '' ){ 
					document.all('hdId').value = returnValue;
					__doPostBack('lnkGenerarArchivo','');
				}
			}
			/*
			function GenerarArchivo2(id, idIBS) {
			
			    document.all('hdCodigoCliente').value = id;
				document.all('hdCodigoIBS').value = idIBS;
				
				__doPostBack('lnkConsultar','');
			}*/
			function GenerarArchivo2(id, idCodigo, tipoDocumento, numeroDocumento) {
			
			    document.all('hdCodigoCliente').value = id;
				document.all('hdCodigo').value = idCodigo;
				document.all('hdTipoDocumento').value = tipoDocumento;
				document.all('hdNumeroDocumento').value = numeroDocumento;
				
				__doPostBack('lnkConsultar','');
			}
			
			function GenerarReporte(id, nombre, anio, mes, fechaProcesoAS400){
				var returnValue = OpenFormatPageDialog('selectFilterReport.aspx?id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 250,500);
				if ( fctTrim(returnValue) != '' ){ 
					document.all('hdFilter').value = returnValue;
					__doPostBack('lnkGenerarReporte','');								
				}
			}			
			
			function OpenFormatPageDialog(url, height , width ) {
					var returnValue = window.showModalDialog(url,'', 'dialogTop: 200px; dialogLeft: 200px;dialogWidth:' + width +  'px;dialogHeight:' + height+ 'px;status: no;help:no;'); 
					if (typeof (returnValue) == "undefined"){
						returnValue = '';
					}
					return returnValue;
			}			
		-->
    </script>

</head>
<body style="margin-left:0; margin-top:0; margin-right:0">
    <form id="Form1" method="post" runat="server">        
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                    <uc1:Banner ID="Banner1" Title="Generación Archivo de Cobranzas" runat="server"></uc1:Banner>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="pnlControls" Visible="True" runat="server">
                        <table cellspacing="0" cellpadding="0" width="750" border="0">
                            <tr>
                                <td style="width:30px;">
                                    &nbsp;</td>
                                <td colspan="2">
                                    <table style="height:100px;" class="InputField" cellspacing="0" cellpadding="0" border="0">
                                        <tr>
                                            <td style="width:70px;">
                                                &nbsp;</td>
                                            <td style="width:80px;">
                                                Periodo</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width:30px;">
                                                &nbsp;</td>
                                            <td style="width:50px;" class="SubHead">
                                                Año</td>
                                            <td style="width:650px;" align="left">
                                                <asp:DropDownList ID="ddlAnio" runat="server" DataTextField="Anio_Periodo" DataValueField="Anio_Periodo"
                                                    Width="200px" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td class="SubHead">
                                                Mes</td>
                                            <td>
                                                <asp:DropDownList ID="ddlMes" runat="server" DataTextField="MonthName" DataValueField="MonthOrder"
                                                    Width="200px" AutoPostBack="True">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width:30px;" align="center">
                                    &nbsp;</td>
                                <td align="left" colspan="2">
                                    <!--<asp:BoundColumn DataField="Nombre_Cliente" HeaderText="Nombre del Cliente" ></asp:BoundColumn> -->
                                    <div class="tabla">
                                        <asp:DataGrid ID="dgProcesos" runat="server" Width="720px" AutoGenerateColumns="False"
                                            BorderWidth="1px" CellSpacing="3" CellPadding="3">
                                            <ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
                                            <HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top"></HeaderStyle>
                                            <AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>
                                            <Columns>
                                                <asp:TemplateColumn HeaderText="Nombre de la Empresa" ItemStyle-Width="230">
                                                    <ItemTemplate>
                                                        <a href="javascript:GenerarArchivo2('<%#Eval("Codigo_Proceso") %>','<%#Eval("Codigo_Cliente") %>','<%#Eval("TipoDocumento") %>','<%#Eval("NumeroDocumento") %>')" visible='<%# ctype(Eval("ShowDelete"), Boolean)%>'><asp:Label runat="server" Visible='<%# ctype(Eval("ShowDelete"), Boolean)%>' Text='<%# Eval("Nombre_Cliente") %>'></asp:Label></a>
                                                        <asp:Literal runat="server" ID="ltrl" Visible='<%# (Not ctype(Eval("ShowDelete"), Boolean))%>'
                                                            Text='<%# Eval("Nombre_Cliente") %>'>
                                                        </asp:Literal>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Periodo">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# BIFConvenios.Periodo.GetMonthByNumber ( Eval("Mes_Periodo") ) %> '>
                                                        </asp:Label>
                                                        <%#Eval("Anio_periodo")%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn HeaderText="Fecha Carga" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="60">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" Text='<%# BIFConvenios.Utils.GetFechaCanonica( Eval("Fecha_ProcesoAS400") ) %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:BoundColumn DataField="NumRegs" HeaderText="Empl. Conv." ItemStyle-Width="25">
                                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NombreEstado" HeaderText="Estado Proceso" ItemStyle-Width="70">
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="GeneracionArchivo" HeaderText="Comentarios"></asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Reporte">
                                                    <ItemTemplate>
                                                        <a href="javascript:GenerarReporte('<%#Eval("Codigo_Proceso") %>','<%#Eval("Nombre_Cliente")%>','<%#Eval("Anio_periodo")%>','<%#Eval("Mes_Periodo")%>','<%#Eval("Fecha_ProcesoAS400")%>')">
                                                            <img src='images/agenda.gif' border='0' alt='Generar Listado de Cuotas por Vencer'></a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn ItemStyle-Width="40">   
                                                    <ItemTemplate>
                                                        <a href="javascript:GenerarArchivo2('<%#Eval("Codigo_Proceso") %>','<%#Eval("Codigo_Cliente") %>','<%#Eval("TipoDocumento") %>','<%#Eval("NumeroDocumento") %>')">Consultar/Editar</a>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                                <asp:TemplateColumn>
                                                    <ItemTemplate>
                                                        <%#MostrarElemento(CType(Eval("ShowDelete"), Boolean), Eval("Codigo_Proceso"), Eval("Nombre_Cliente"), Eval("Anio_periodo"), Eval("Mes_Periodo"), Eval("Fecha_ProcesoAS400"))%>
                                                    </ItemTemplate>
                                                </asp:TemplateColumn>
                                            </Columns>
                                        </asp:DataGrid></div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 43px">
                                    &nbsp;</td>
                                <td colspan="2" style="height: 43px">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="SubHead" width="150">
                                                Número de Registros
                                            </td>
                                            <td class="Text">
                                                <asp:Label ID="lblNumReg" runat="server"></asp:Label></td>
                                        </tr>
                                    </table>
                                    <asp:LinkButton ID="lnkGenerarArchivo" runat="server"></asp:LinkButton>
                                    <input id="hdId" type="hidden" name="hdId" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:LinkButton ID="lnkDeleteProcess" runat="server"></asp:LinkButton>
                                    <input id="hdData" type="hidden" name="hdData" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px">
                                    &nbsp;</td>
                                <td style="height: 24px">
                                    <asp:LinkButton ID="lnkGenerarReporte" runat="server"></asp:LinkButton>
                                    <input id="hdFilter" type="hidden" name="hdFilter" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 24px">
                                    &nbsp;</td>
                                <td style="height: 24px">
                                    <asp:LinkButton ID="lnkConsultar" runat="server"></asp:LinkButton>
                                    <input type="hidden" id="hdCodigoCliente" name="hdCodigoCliente" runat="server" />
                                    <!--<input type="hidden" id="hdCodigoIBS" name="hdCodigoIBS" runat="server" /> -->
                                    <input type="hidden" id="hdCodigo" name="hdCodigo" runat="server" />
                                    <input type="hidden" id="hdTipoDocumento" name="hdTipoDocumento" runat="server" />
                                    <input type="hidden" id="hdNumeroDocumento" name="hdNumeroDocumento" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlGenArchivos" Visible="False">
                        <table cellspacing="0" cellpadding="10" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensaje" runat="server" CssClass="SubHead"></asp:Label></td>
                            </tr>
                        </table>

                        <script type="text/javascript">
							<!--
							    //openPage('/BIFConvenios/generacionCf/enviomail.aspx?idp=<%=idP%>&formatoArchivo=<%=formatoArchivo%>&situacionTrabajador=<%=situacionTrabajador%>&modalidad= <%=modalidad%>', 430,500 );
								openPage('/BIFConvenios/generacionCf/EsperaFinalGeneracionCf.aspx?id=<%=idGenFile%>&cantidad=&montosoles=&montodolares=', 300, 390);
							-->
                        </script>

                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlGenReporte" Visible="False">
                        <table cellspacing="0" cellpadding="10" border="0">
                            <tr>
                                <td>
                                    <asp:Label ID="lblMensaje1" runat="server" CssClass="SubHead"></asp:Label></td>
                            </tr>
                        </table>
                        <%RedirectReporte(idP, modalidad, situacionTrabajador)%>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>