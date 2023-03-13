<%@ Page Language="vb" AutoEventWireup="false" Inherits="ReportePostConciliacion" CodeFile="ReportePostConciliacion.aspx.vb" EnableEventValidation="false"  %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
	
		<title>BIFConvenios - Post Conciliación</title>

		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src ='<%=ResolveUrl("~/js/global.js") %>' language="javascript" type="text/javascript"></script>

		<link media="all" href="<%=ResolveUrl("~/css/calendar.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/calendar.js") %>' language="javascript" type="text/javascript"></script>
		<script src='<%=ResolveUrl("~/js/calendar-es.js") %>' language="javascript" type="text/javascript"></script>
		
		<script language="javascript" type="text/javascript">

			//Código para mostrar el calendario
			var oldLink = null;

			function selected(cal, date) {
				cal.sel.value = date;
				cal.callCloseHandler();
			}

			function closeHandler(cal) {
				cal.hide();
			}

			function showCalendar(id, format) {
				var el = document.getElementById(id);
				if (calendar != null) {
					calendar.hide();
				} else {
					var cal = new Calendar(false, null, selected, closeHandler);

					calendar = cal;
					cal.setRange(1900, 2070);
					cal.create();
				}

				calendar.setDateFormat(format);
				calendar.parseDate(el.value);
				calendar.sel = el;
				calendar.showAtElement(el);

				return false;
			}

			function ShowDetalle(codEmpresa, numPagare, fechaDesde, fechaHasta, nombreTrabajador, montoTotal, moneda, cuotaMes, importeDescontado, deudaPeriodo) {
				// openDialog('<%=Request.ApplicationPath%>/consultas/ConsultaDetallePagosIBS.aspx?p1=' + codEmpresa + '&p2=' + numPagare + '&p3=' + fechaDesde + '&p4=' + fechaHasta + '&nt=' + nombreTrabajador + '&amount=' + montoTotal + '&mon=' + moneda + '&cuotaMes=' + cuotaMes + '&importeDescontado=' + importeDescontado + '&deudaPeriodo=' + deudaPeriodo, 380, 785);
                openDialog('<%= ResolveUrl("~/consultas/ConsultaDetallePagosIBS.aspx") %>?p1=' + codEmpresa + '&p2=' + numPagare + '&p3=' + fechaDesde + '&p4=' + fechaHasta + '&nt=' + nombreTrabajador + '&amount=' + montoTotal + '&mon=' + moneda + '&cuotaMes=' + cuotaMes + '&importeDescontado=' + importeDescontado + '&deudaPeriodo=' + deudaPeriodo, 380, 785);
			}

			function Valida(obj, args) {
				if (fctTrim(document.all('hdParam1').value) == '') {
					alert('Debe seleccionar la empresa, el periodo y establecer las fechas \ncontra las que quiere comparar en IBS');
					args.IsValid = false;
				}
				else {
					document.all('dvProcess').innerHTML = '<br><br>' + waitMessage('<%=ResolveUrl("~/images/sqsWait.gif")%>');//'<center class="Subhead">Procesando...</center>';
					if (document.all('dvData') != null) {
						document.all('dvData').className = 'hide';
					}

					args.IsValid = true;
				}
			}

            var popupBuscarEmpresa;
            function fnOpenPopupBuscarEmpresa(url, height, width) {

                // Obtener el tama�o de la ventana principal
                var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
                var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

                // Calcular la posici�n de la ventana secundaria
                var left = (parentWidth - width) / 2;
                var top = (parentHeight - height) / 2;

                popupBuscarEmpresa = window.open(url, '', 'left=' + left + ',top=' + top + ',height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
                popupBuscarEmpresa.opener = window;

                var timer = setInterval(function () {
                    if (popupBuscarEmpresa.closed) {
                        clearInterval(timer);
                        var result = popupBuscarEmpresa.ReturnValueSeleccionado();

                        if (typeof result !== "undefined") {
                            document.all('hdParam1').value = getvalue(result, 1, '|'); // Proceso 
                            document.all('hdParam2').value = getvalue(result, 2, '|'); // Año
                            document.all('hdParam3').value = getvalue(result, 3, '|'); // Mes
                            document.all('hdParam4').value = getvalue(result, 7, '|'); // Tipo Documento
                            document.all('hdParam5').value = getvalue(result, 8, '|'); // Numero Documento
                            document.all('txtPeriodo').value = getvalue(result, 6, '|');
                            document.all('hdCodigoEmpresa').value = getvalue(result, 4, '|');
                            document.all('txtNombreEmpresa').value = getvalue(result, 5, '|');
                            document.all('txtFechaDesde').value = getvalue(result, 9, '|');
                            document.all('txtFechaHasta').value = getvalue(result, 10, '|');

                            if (document.all('dvData') != null) {
                                document.all('dvData').className = 'hide';
                            }
                        }
                    }
                }, 100);

            }

            function openBusqueda() {
                fnOpenPopupBuscarEmpresa("<%= ResolveUrl("~/busqueda/BuscarParametrosEmpresa.aspx") %>", 380, 500);
            }
				
				/***********************mostrar informacion acerca del documento a ser generado**************/
			function checkall() {
				if (document.forms[0].chkData != null) {
					setCheckBox(document.forms[0].chkData, document.all('chkall').checked);
				}

			}
		
			function GenerarDocumentoCobranza() {
				if (document.forms[0].chkData != null) {
					var data = getSelectedCheckboxValue(document.forms[0].chkData);
					if (data.length <= 0) {
						alert('seleccione al menos un prestamo');
						return;
					}

                    processGdChecks();
				}
			}
            function Prorrogar() {
                if (document.forms[0].chkData != null) {
                    var data = getSelectedCheckboxValue(document.forms[0].chkData);
                    if (data.length <= 0) {
                        alert('seleccione al menos un prestamo');
                        return;
                    }

                    procesarProrroga();
                }
			}


            var popupGenerarDocumentoCobranza;
			function fnOpenPopupGenerarDocumentoCobranza(url, height, width) {

				// Obtener el tama�o de la ventana principal
				var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
				var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

				// Calcular la posici�n de la ventana secundaria
				var left = (parentWidth - width) / 2;
				var top = (parentHeight - height) / 2;

				popupGenerarDocumentoCobranza = window.open(url, '', 'left=' + left + ',top=' + top + ',height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
				popupGenerarDocumentoCobranza.opener = window;

				var timer = setInterval(function () {
					if (popupGenerarDocumentoCobranza.closed) {
						clearInterval(timer);
						var result = popupGenerarDocumentoCobranza.ReturnValueSeleccionado();

						if (typeof result !== "undefined") {
							var data = getSelectedCheckboxValue(document.forms[0].chkData);
							var pagares = '';
							var amounts = '';
							//Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
							for (var i = 0; i < data.length; i++) {
								pagares = pagares + "!" + data[i] + "!,";
								amounts = amounts + "!" + data[i] + '=' + document.all('dvAmount' + data[i]).value + "!,";

							}
							pagares = pagares.substring(0, pagares.length - 1) + 'end';
							amounts = amounts.substring(0, amounts.length - 1) + 'end';
							//openPage ( 'ContainerDocumentoCobranza.aspx?dt='+result + '&p=' +pagares , 600, 800);
							document.forms["frmData"].dt.value = result;
							document.forms["frmData"].a.value = amounts;
							//alert ( amounts);					
							document.forms["frmData"].p.value = pagares;
							document.forms["frmData"].proceso.value = document.all('hdParam1').value;
							document.forms["frmData"].anio.value = document.all('hdParam2').value; // Año					
							document.forms["frmData"].mes.value = document.all('hdParam3').value;
							document.forms["frmData"].submit();
						}
					}
				}, 100);

			}

            function processGdChecks() {
                fnOpenPopupGenerarDocumentoCobranza("DetallePostConciliacion.html", 180, 450);
			}
			
			window.onload = function () {
				MM_preloadImages('<%=ResolveUrl("~/images/procesar_on.jpg")%>');
				document.Form1.chkInclude.onclick = function () {
					document.all('ddlBuscarpor').disabled = !document.all('chkInclude').checked;
					document.all('txtCampo').disabled = !document.all('chkInclude').checked;
				}
			}
			
			// Proceso de Prorrogas
			function procesarProrroga() {
				var data = getSelectedCheckboxValue(document.forms[0].chkData);
				var pagares = '';
				//Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
				for (var i = 0; i < data.length; i++) {
					pagares = pagares + "!" + data[i] + "!,";
				}
				pagares = pagares.substring(0, pagares.length - 1);

				document.forms["frmSndP"].pagares.value = pagares;//proceso
				document.forms["frmSndP"].proceso.value = document.all('hdParam1').value;//proceso
				document.forms["frmSndP"].submit();

				document.all("divFrame").className = "show1";
				document.all('dvData').className = 'hide';
			}
			
        </script>
	</HEAD>
	
	<body leftMargin="0" topMargin="0"  rightMargin="0">
	
		<form id="Form1" method="post" runat="server">
		
			<input id="hdParam1" type="hidden" name="hdParam1" runat="server"> 
			<input id="hdParam2" type="hidden" name="hdParam2" runat="server">
			<input id="hdParam3" type="hidden" name="hdParam3" runat="server"> 
			<input id="hdParam4" type="hidden" name="hdParam4" runat="server">
			<input id="hdParam5" type="hidden" name="hdParam5" runat="server">
			
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Post Conciliación" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td>
						<br>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" width="650" border="0">
							<tr>
								<td width="30">&nbsp;</td>
								<td vAlign="top">
									<table class="InputField" cellSpacing="0" cellPadding="5" width="100%" border="0">
										<tr>
											<td colspan="4">&nbsp;</td>
										</tr>
										<tr>
											<td>Empresa
											</td>
											<td colSpan="3">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td>
														    <asp:TextBox id="txtNombreEmpresa" Runat="server" MaxLength="100" Columns="60" ReadOnly="true"></asp:TextBox>
														    <input id="hdCodigoEmpresa" type="hidden" name="hdCodigoEmpresa" runat="server">
														</td>
														<td>
														    <a href="javascript:openBusqueda()"><img alt="buscar" src="../images/texto.gif" border="0"></a>
														</td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td>Periodo
											</td>
											<td colSpan="3"><asp:TextBox id="txtPeriodo" Runat="server" MaxLength="30" Columns="20" ReadOnly="true"></asp:TextBox></td>
										</tr>
										<tr>
											<td colSpan="4">Comparar con pagos en IBS
											</td>
										</tr>
										<tr>
											<td>Desde
											</td>
											<td><asp:TextBox id="txtFechaDesde" Runat="server" MaxLength="30" Columns="10" ReadOnly="true"></asp:TextBox>&nbsp;<%=PintarBotonCalendario(ClientID, "txtFechaDesde")%>
											</td>
											<td>Hasta
											</td>
											<td><asp:TextBox id="txtFechaHasta" Runat="server" MaxLength="30" Columns="10" ReadOnly="true"></asp:TextBox>&nbsp;<%=PintarBotonCalendario(ClientID, "txtFechaHasta")%>
											</td>
										</tr>
										<tr>
											<td>
												<asp:CheckBox runat="server" ID="chkInclude" Text="Filtrar también"></asp:CheckBox>
											</td>
											<td colspan="2">
												<asp:DropDownList ID="ddlBuscarpor" runat="server" Enabled="false">
													<asp:ListItem Value="Nombre">Nombre de Cliente</asp:ListItem>
													<asp:ListItem Value="Codigo">Código de Pagaré</asp:ListItem>
												</asp:DropDownList>

												<asp:TextBox ID="txtCampo" runat="server" Enabled="false"></asp:TextBox>
											</td>
										</tr>
										
										<tr>
											<td align="right" colSpan="4">
												<asp:LinkButton id="lnkBuscar" runat="server">
													<img src="<%=ResolveUrl("~/images/procesar.jpg") %>" alt="Procesar archivo" />
                                                </asp:LinkButton>
                                            </td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<table cellSpacing="0" cellPadding="0" width="650" border="0" >
				<tr>
					<td id="tdMostrar" runat="server" visible="false">
						<div id="dvMostrar">
						&nbsp;Mostrar:
						</div>
					</td>
					<td id="tdddl" runat="server" visible="false">
						<div id="dvddl"><!--<asp:ListItem  Value ="(SaldoDeudorAcreedor > 0) AND (DLID =0) And ( TotalPagosCliente ='0.00') And ( PAGOIBSPROCESOCOBRANZA='0.00') ">Págares para prorroga</asp:ListItem> -->
							<table border="0" cellpadding="0" cellpadding="0" width="100%" >
								<tr>
									<td>
										<asp:DropDownList ID="ddlKind" Runat="server"  AutoPostBack="True">
											<asp:ListItem Value ="">Todos</asp:ListItem>
											<asp:ListItem Value ="SaldoDeudorAcreedor<0">Con diferencias a favor</asp:ListItem>    
											<asp:ListItem Value ="SaldoDeudorAcreedor=0">Sin diferencias</asp:ListItem>
											<asp:ListItem Value ="SaldoDeudorAcreedor>0">Con diferencias en contra</asp:ListItem> 
											<asp:ListItem Value ="ImporteBIFActualizada>0">Con DEUDA en IBS</asp:ListItem>
											<asp:ListItem Value ="(((PAGOIBSPROCESOCOBRANZA<>'0.00' AND  DLID <>0) OR (DLID=0) )  AND PAGOPARCIAL = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTASACTUAL = 1)">Información para Prorroga</asp:ListItem>
											<asp:ListItem Value ="prorrogado='si'">Documentos prorrogados</asp:ListItem>
											<asp:ListItem Value ="(NUMCUOTASACTUAL=1)">Con una cuota vencida</asp:ListItem>                     
										</asp:DropDownList>
										<!--
										<asp:ListItem Value ="(DLID =0  AND TotalPagosCliente = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTASACTUAL = 1)">Información para Prorroga</asp:ListItem>
										<asp:ListItem Value ="DLID=0  AND TotalPagosCliente = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTAS = 1">Información para Prorroga</asp:ListItem>
										-->
									</td>
									<td>
										<asp:DropDownList ID="ddlUGE" Runat="server" Visible="False"  AutoPostBack="True" DataTextField="DESCRIPCIONCORTA" DataValueField="CODE">
										</asp:DropDownList>
									</td>
									<td>
										<asp:DropDownList ID="ddlSituacion" Runat="server" Visible="False"  AutoPostBack="True">
											<asp:ListItem Value="">Todas las situaciones</asp:ListItem>
											<asp:ListItem Value="SITUACIONLABORALACTUAL = 'A'">Activos</asp:ListItem> 
											<asp:ListItem Value="SITUACIONLABORALACTUAL in ('C','S')">Cesantes y Sobrevivientes</asp:ListItem>
											<asp:ListItem Value="SITUACIONLABORALACTUAL = ''">Sin situacion</asp:ListItem>                        
										</asp:DropDownList>
									</td>
								</tr>
							</table> 
						</div>
					</td>
				</tr>
			</table>
			<br>
			<table cellSpacing="0" cellPadding="0" width="650" border="0">
				<tr>
					<td width="5">&nbsp;</td>
					<td vAlign="top">
                        <table border="0" cellpadding="2" cellspacing="0" class="box">
                            <thead>
                                <tr>
                                    <th>
                                        <img src="<%=ResolveUrl("~/images/bar_begin.gif") %>" height="17" alt=""></th>
									<th>
										<a href="javascript:GenerarDocumentoCobranza()">Generar Documento de Cobranza</a>
									</th>
							        <th>
								        <img src="<%=ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
								    </th>								
							        <th>
								        <a href="javascript:Prorrogar()">Prorrogar</a>
								    </th>
							        <th>
								        <img src="<%=ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
								    </th>
							        <th>
							            <asp:LinkButton Runat=server ID=lnkGenerarReporte CausesValidation=False>Obtener informacion en archivo</asp:LinkButton>
							        </th>
									<th>
										<img src="<%=ResolveUrl("~/images/bar_end.gif") %>" width="17" height="18" alt="">
									</th>
								</tr>
							</thead>
						</table>
					</td>
				</tr>
			</table>
			<br>
			<div id="dvProcess" name="dvProcess" />
			
			<div id="dvData" name="dvData" runat="server" visible="false">
                <!--%#iif(DataBinder.Eval(Container.DataItem, "DeudaPeriodo")>0, "", "disabled")%-->
                    <div class="tabla">
				        <asp:DataGrid id="dgProcesoResult" runat="server" CellPadding="3" CellSpacing="3"  BorderWidth="1px" Width="780" AutoGenerateColumns="False" Visible="False" ShowFooter="True"  AllowPaging="True" PageSize="80">
							<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
							<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
							<FooterStyle  HorizontalAlign ="Right" BorderWidth="0px"   BorderColor="#ffffff">
							</FooterStyle>
							<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>	

							<Columns>
								<asp:TemplateColumn ItemStyle-Width="20" HeaderText="<input type=checkbox id=chkall name=chkall onclick='javascript:checkall();'>">
									<ItemTemplate>
										<input type="hidden" id='dvAmount<%#DataBinder.Eval(Container.DataItem, "DLNP")%>'  value="<%#DataBinder.Eval(Container.DataItem, "ImporteBIFActualizadaReporte")%>"/>							
										<input type="checkbox" id="chkData" name="chkData" value='<%#DataBinder.Eval(Container.DataItem, "DLNP")%>'  >
									</ItemTemplate>
								</asp:TemplateColumn>
						
								<asp:BoundColumn DataField="DLNP" HeaderText="Pagaré" ItemStyle-Width="80"></asp:BoundColumn>
								<asp:BoundColumn DataField="DLNE" HeaderText="Trabajador" ItemStyle-Width="250"></asp:BoundColumn>
								<asp:BoundColumn DataField="DLMO" HeaderText="Mon" ItemStyle-Width="10px"></asp:BoundColumn>
								<asp:BoundColumn DataField="DLIC" HeaderText="Importe BIF Informado" DataFormatString="{0:#.00}" Visible="True" ItemStyle-Width="80" ItemStyle-BackColor="#ffffcc">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="DLID" HeaderText="Importe de Institución" DataFormatString="{0:#.00}" ItemStyle-Width="80" ItemStyle-BackColor="#ffffcc">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>

						
								<asp:TemplateColumn headertext="Total Ventanilla" Visible=False  ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"   HeaderStyle-Width="55" ItemStyle-Width="55"  >
									<ItemTemplate><%#DataBinder.Eval(Container.DataItem, "PAGOVENTANILLA")%></ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn headertext="Total Cta. Cte."  Visible=False   ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="55" ItemStyle-Width="55"   >
									<ItemTemplate>
									<%#DataBinder.Eval(Container.DataItem, "PAGOINTERNET")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn headertext="Total Otros – IBS"  Visible=False   ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="55" ItemStyle-Width="55"  >
									<ItemTemplate>
									<%#DataBinder.Eval(Container.DataItem, "PAGOIBS")%>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="Otros Pagos IBS"  ItemStyle-BackColor="#00ccff"   HeaderStyle-Width="55" ItemStyle-Width="55" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate><%#GetAmount(DataBinder.Eval(Container.DataItem, "DLCC"), DataBinder.Eval(Container.DataItem, "DLNP"), DataBinder.Eval(Container.DataItem, "DLNE"), DataBinder.Eval(Container.DataItem, "DLMO"), DataBinder.Eval(Container.DataItem, "DLIC"), DataBinder.Eval(Container.DataItem, "DLID"), DataBinder.Eval(Container.DataItem, "DeudaPeriodo"), DataBinder.Eval(Container.DataItem, "DLFP"), DataBinder.Eval(Container.DataItem, "TotalPagosCliente"))%></ItemTemplate>
								</asp:TemplateColumn>
						
								<asp:BoundColumn Visible="False" DataField="DeudaPeriodo" HeaderText="Saldo Deudor/<br>Acreedor" DataFormatString="{0:#.00}" ItemStyle-Width="80">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
								<asp:BoundColumn DataField="SaldoDeudorAcreedor" ItemStyle-BackColor="LemonChiffon"  ItemStyle-HorizontalAlign="Right"   HeaderStyle-Width="75" ItemStyle-Width="75"  HeaderText="Saldo Deudor(+)/<br>Acreedor(-)"></asp:BoundColumn> 

								<asp:BoundColumn DataField="PAGOIBSPROCESOCOBRANZA" HeaderText="Importe de Institución PROCESADO (IBS)" DataFormatString="{0:#.00}" ItemStyle-Width="80" ItemStyle-BackColor="#ffffcc">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
								</asp:BoundColumn>
						
								<asp:TemplateColumn HeaderText="Deuda Actual Proyec-tada(IBS)"  HeaderStyle-Width="55" ItemStyle-Width="55" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
									<ItemTemplate><%#Format(DataBinder.Eval(Container.DataItem, "ImporteBIFActualizada"), "#0.00")%></ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Pror."  HeaderStyle-Width="55" ItemStyle-Width="55" >
								<ItemTemplate>
								<div id="dvDataP<%#DataBinder.Eval(Container.DataItem, "DLNP")%>" name="dvDataP<%#DataBinder.Eval(Container.DataItem, "DLNP")%>"><%#DataBinder.Eval(Container.DataItem, "prorrogado")%></div> 
								</ItemTemplate>

								</asp:TemplateColumn>
								<asp:BoundColumn DataField="CartaGenerada" HeaderText="Carta" ItemStyle-Width="80"></asp:BoundColumn>
							</Columns>
							<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
						</asp:DataGrid>
			        </div>
				    <br>
						<%--<asp:TemplateColumn HeaderText="Pagos del Cliente" HeaderStyle-Width="80px" ItemStyle-Width="80px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate><%#GetAmount(DataBinder.Eval ( Container.DataItem, "DLCC"), DataBinder.Eval ( Container.DataItem, "DLNP"), DataBinder.Eval ( Container.DataItem, "DLNE"), DataBinder.Eval ( Container.DataItem, "DLMO"),  DataBinder.Eval (Container.DataItem, "DLIC"), DataBinder.Eval (Container.DataItem, "DLID"), DataBinder.Eval (Container.DataItem, "DeudaPeriodo") )%></ItemTemplate>
						</asp:TemplateColumn>--%>
						<!-- <asp:BoundColumn DataField="Estado" HeaderText="Estado&#160;Registro" ItemStyle-Width="220"></asp:BoundColumn> -->
				        <table cellSpacing="0" cellPadding="0" border="0">
						<tr>
							<td class="SubHead">Total de Registros&nbsp;&nbsp;
								<asp:Label CssClass="Text" id="lblTotalReg" Runat="server"></asp:Label>
							</td>
						</tr>
					</table>
			</div>
			<asp:CustomValidator id="cvValida" runat="server" ClientValidationFunction="Valida" Display="None" ErrorMessage="CustomValidator"></asp:CustomValidator>
		</form>
		
		<div id="divFrame" name="divFrame" class="hide1">
			<iframe id="fraProccess"  name="fraProccess"   height="400" width="800" frameborder=0>
			</iframe>
		</div>
				
		<form id="frmData" action="ContainerDocumentoCobranza.aspx" target="_blank" method="post">
			<input type="hidden" name="dt" id="dt"> 
			<input type="hidden" name="p" id="p"> 
			<input type="hidden" name="proceso" id="proceso">
			<input type="hidden" name="a" id="a">
			<input type="hidden" name="anio" id="anio">			
			<input type="hidden" name="mes" id ="mes"> 
		</form>
		
		<form id="frmSndP" name="frmSndP" target="fraProccess" action="ProcesarProrrogasTestWait.aspx" method="post">
			<input type="hidden" name="pagares" id="pagares">
			<input type="hidden" name="proceso" id="proceso">
		</form>

	</body>
</HTML>
