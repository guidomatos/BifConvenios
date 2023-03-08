<%@ Page Language="vb" AutoEventWireup="false" Inherits="BIFConvenios.ReporteSeguimiento" CodeFile="ReporteSeguimiento.aspx.vb"  EnableEventValidation="false"  %>
<%@ Register TagPrefix="uc1" TagName="Banner" Src="../controls/Banner.ascx" %>
<%--<%@ Register TagPrefix="uc2" TagName="pnlBusParamEmpresa" Src="~/controls/BuscarParametrosEmpresa.ascx" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>BIFConvenios - Seguimiento</title>
		<link href="<%=ResolveUrl("~/css/global.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/global.js") %>' language ="javascript" type="text/javascript"></script>
		<link media="all" href="<%=ResolveUrl("~/css/calendar.css") %>" rel="Stylesheet" type="text/css" />
		<script src='<%=ResolveUrl("~/js/calendar.js") %>' language ="javascript" type="text/javascript"></script>
		<script src='<%=ResolveUrl("~/js/calendar-es.js") %>' language ="javascript" type="text/javascript"></script>
		
		<script language="javascript" type="text/javascript">
				<!--
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


            var popupGenerarReporte;
            function fnOpenPopupGenerarReporte(url, height, width) {

                // Obtener el tamaño de la ventana principal
                var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
                var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

                // Calcular la posición de la ventana secundaria
                var left = (parentWidth - width) / 2;
                var top = (parentHeight - height) / 2;

                popupGenerarReporte = window.open(url, '', 'left=' + left + ',top=' + top + ',height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
                popupGenerarReporte.opener = window;

                var timer = setInterval(function () {
                    if (popupGenerarReporte.closed) {
                        clearInterval(timer);
                        var returnValue = popupGenerarReporte.ReturnValueSeleccionado();

                        if (typeof returnValue !== "undefined") {
                            if (fctTrim(returnValue) != '') {
                                document.all('hdFilter').value = returnValue;
                                __doPostBack('lnkGenerarReporte', '');
                            }
                        }
                    }
                }, 100);

            }
            function fnGenerarReporte(id, nombre, anio, mes, fechaProcesoAS400) {
                fnOpenPopupGenerarReporte("selectFilterReport.aspx?id=" + id + "&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" + fechaProcesoAS400, 250, 500);
            }






            var popupBuscarEmpresa;
			function fnOpenPopupBusquedaEmpresa(url, height, width) {

				// Obtener el tamaño de la ventana principal
				var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
				var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

				// Calcular la posición de la ventana secundaria
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
							document.all('hdParam6').value = getvalue(result, 9, '|'); // Fecha de Corte de Seguimiento 

							document.all('txtPeriodo').value = getvalue(result, 6, '|');
							document.all('hdCodigoEmpresa').value = getvalue(result, 4, '|');
							document.all('txtNombreEmpresa').value = getvalue(result, 5, '|');

							if (document.all('dvData') != null) {
								document.all('dvData').className = 'hide';
								document.all('dvMostrar').className = 'hide';
								document.all('dvddl').className = 'hide';
							}
						}
					}
				}, 100);
			}
					
			function fnBuscarEmpresa() {
                fnOpenPopupBusquedaEmpresa("<%= ResolveUrl("~/busqueda/BuscarParametrosEmpresaSeguimiento.aspx") %>", 380, 500);
			}
				
			function Valida(obj, args) {
				if (fctTrim(document.all('hdParam1').value) == '') {
					alert('Debe seleccionar la empresa y el periodo');
					args.IsValid = false;
				}
				else {
                    document.all('dvProcess').innerHTML = '<br><br>' + waitMessage("<%= ResolveUrl("~/images/sqsWait.gif")%>");//'<center class="Subhead">Procesando...</center>';
					if (document.all('dvData') != null) {
                        document.all('dvData').className = 'hide';
                        document.all('dvMostrar').className = 'hide';
                        document.all('dvddl').className = 'hide';
                    }
					args.IsValid = True;
				}
			}

			function fnValidarSeleccionPrestamo() {
                if (document.forms[0].chkData != null) {
                    var data = getSelectedCheckboxValue(document.forms[0].chkData);
                    if (data.length <= 0) return false;
					return true;
				}
				return false;
            }

            function fnGenerarDocumentoCobranza() {

				if (!fnValidarSeleccionPrestamo()) {
                    alert('seleccione al menos un prestamo');
                    return;
                }

                fnEjecutarGeneracionDocumentoCobranza();
			}

			function fnBloquearDesbloquear() {
                if (!fnValidarSeleccionPrestamo()) {
                    alert('seleccione al menos un prestamo');
                    return;
                }

                fnEjecutarBloqueoDesbloqueo();
			}

			function fnProcesarProrroga() {
                if (!fnValidarSeleccionPrestamo()) {
                    alert('seleccione al menos un prestamo');
                    return;
                }

                fnEjecutarProcesoProrroga();
            }

			function fnExportarDocumentoCobranza() {
                if (!fnValidarSeleccionPrestamo()) {
                    alert('seleccione al menos un prestamo');
                    return;
                }

                fnProcesarExportacionDocumentoCobranza();
            }

			function checkall() {
				if (document.forms[0].chkData != null) {
					setCheckBox(document.forms[0].chkData, document.all('chkall').checked);
				}
			}

			function checkall_delete() {
				if (document.forms[0].chkData != null) {
					setCheckBox(document.forms[0].chkData_delete, document.all('chkall_delete').checked);
				}
			}
	
			/*********procesar la generacion del documento********/

            var popupSeleccionarTipoDocumento;
            function fnOpenPopupSeleccionarTipoDocumento(url, height, width) {

                // Obtener el tama�o de la ventana principal
                var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
                var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

                // Calcular la posici�n de la ventana secundaria
                var left = (parentWidth - width) / 2;
                var top = (parentHeight - height) / 2;

                popupSeleccionarTipoDocumento = window.open(url, '', 'left=' + left + ',top=' + top + ',height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
                popupSeleccionarTipoDocumento.opener = window;

                var timer = setInterval(function () {
                    if (popupSeleccionarTipoDocumento.closed) {
                        clearInterval(timer);
                        var returnValue = popupSeleccionarTipoDocumento.ReturnValueSeleccionado();

						if (typeof returnValue !== "undefined") {
                            var amounts = '';
                            var Data = getSelectedCheckboxValue(document.forms[0].chkData);
                            var pagares = '';
                            // Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
                            for (var i = 0; i < data.length; i++) {
                                pagares = pagares + "!" + Data[i] + "!,";
                                amounts = amounts + "!" + Data[i] + '=' + document.all('dvAmount' + data[i]).value + "!,";
                            }
                            pagares = pagares.substring(0, pagares.length - 1) + 'end';
                            amounts = amounts.substring(0, amounts.length - 1) + 'end';
                            document.forms["frmData"].dt.value = result;
                            document.forms["frmData"].p.value = pagares;
                            document.forms["frmData"].a.value = amounts;

                            document.forms["frmData"].anio.value = document.all('hdParam2').value; // Año
                            document.forms["frmData"].mes.value = document.all('hdParam3').value; // Mes
                            document.forms["frmData"].proceso.value = document.all('hdParam1').value;//proceso
                            if (fctTrim(document.all('hdParam6').value) == '') {
                                if (!confirm('Generando el documento de cobranza se establecerá el dia de hoy como fecha de corte del Seguimiento,\n¿Desea Continuar?')) {
                                    return;
                                }
                            }
                            document.all('hdParam6').value = 'ok';
                            document.forms["frmData"].submit();
                            
                        }
                    }
                }, 100);
            }

			function fnEjecutarGeneracionDocumentoCobranza() {
                fnOpenPopupSeleccionarTipoDocumento("DetalleSeguimientoDocumentos.html", 180, 450);
			}

			/*********procesar la generacion del documento********/


            var popupExportacionDocumentoCobranza;
            function fnOpenPopupExportacionDocumentoCobranza(url, height, width) {

                // Obtener el tama�o de la ventana principal
                var parentWidth = window.innerWidth || document.documentElement.clientWidth || document.body.clientWidth;
                var parentHeight = window.innerHeight || document.documentElement.clientHeight || document.body.clientHeight;

                // Calcular la posici�n de la ventana secundaria
                var left = (parentWidth - width) / 2;
                var top = (parentHeight - height) / 2;

                popupExportacionDocumentoCobranza = window.open(url, '', 'left=' + left + ',top=' + top + ',height=' + height + 'px,width=' + width + 'px,toolbar=no,directories=no,status=no,continued from previous linemenubar = no, scrollbars = no, resizable = no, modal = yes');
                popupExportacionDocumentoCobranza.opener = window;

                var timer = setInterval(function () {
                    if (popupExportacionDocumentoCobranza.closed) {
                        clearInterval(timer);
                        var returnValue = popupExportacionDocumentoCobranza.ReturnValueSeleccionado();

                        if (typeof returnValue !== "undefined") {
                            var amounts = '';
                            var Data = getSelectedCheckboxValue(document.forms[0].chkData);
                            var pagares = '';
                            // Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
                            for (var i = 0; i < data.length; i++) {
                                pagares = pagares + "!" + Data[i] + "!,";
                                amounts = amounts + "!" + Data[i] + '=' + document.all('dvAmount' + data[i]).value + "!,";
                            }
                            pagares = pagares.substring(0, pagares.length - 1) + 'end';
                            amounts = amounts.substring(0, amounts.length - 1) + 'end';
                            document.forms["frmData1"].dt.value = result;
                            document.forms["frmData1"].p.value = pagares;
                            document.forms["frmData1"].a.value = amounts;

                            document.forms["frmData1"].anio.value = document.all('hdParam2').value; // Año
                            document.forms["frmData1"].mes.value = document.all('hdParam3').value; // Mes
                            document.forms["frmData1"].proceso.value = document.all('hdParam1').value;//proceso
                            if (fctTrim(document.all('hdParam6').value) == '') {
                                if (!confirm('Generando el documento de cobranza se establecerá el dia de hoy como fecha de corte del Seguimiento,\n¿Desea Continuar?')) {
                                    return;
                                }
                            }
                            document.all('hdParam6').value = 'ok';
                            document.forms["frmData1"].submit();

                        }
                    }
                }, 100);
            }

			function fnProcesarExportacionDocumentoCobranza() {
                fnOpenPopupExportacionDocumentoCobranza("DetalleSeguimientoDocumentos.html", 180, 450);
			}

			function fnEjecutarBloqueoDesbloqueo() {
				var Data = getSelectedCheckboxValue(document.forms[0].chkData);
				var pagares = '';
				// Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
				for (var i = 0; i < data.length; i++) {
					pagares = pagares + "!" + Data[i] + "!,";
				}
				pagares = pagares.substring(0, pagares.length - 1);
				//Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
				//--document.write  ( "ProcesarBloqueos.aspx?pagares=" + pagares + "&proceso=" + document.all ( 'hdParam1').value );
				//document.all ( "fraProccess").src = "ProcesarBloqueos.aspx?pagares=" + pagares + "&proceso=" + document.all ( 'hdParam1').value;
				document.forms["frmSnd"].pagares.value = pagares;//proceso
				document.forms["frmSnd"].proceso.value = document.all('hdParam1').value;//proceso
				document.forms["frmSnd"].submit();

				document.all("divFrame").className = "show1";
				document.all('dvData').className = 'hide';
			}
			
			// Proceso de Prorrogas
			/*
			function fnEjecutarProcesoProrroga() {
				var Data = getSelectedCheckboxValue(document.forms[0].chkData);
				var pagares = '';
				// Obtenemos la informacion de pagares para abrirlos en la ventana de impresion
				for (var i = 0; i < data.length; i++) {
					pagares = pagares + "!" + Data[i] + "!,";
				}
				pagares = pagares.substring(0, pagares.length - 1);

				document.forms["frmSndP"].pagares.value = pagares;//proceso
				document.forms["frmSndP"].proceso.value = document.all('hdParam1').value;//proceso
				document.forms["frmSndP"].submit();

				document.all("divFrame").className = "show1";
				document.all('dvData').className = 'hide';
			}
			*/

            function ShowDetalle(codEmpresa, numPagare, fechaDesde, fechaHasta, nombreTrabajador, montoTotal, moneda, cuotaMes, importeDescontado, deudaPeriodo) {
				//openDialog('<%=Request.ApplicationPath%>/consultas/ConsultaDetallePagosIBSFechas.aspx?p1=' + codEmpresa + '&p2=' + numPagare + '&p3=' + fechaDesde + '&p4=' + fechaHasta + '&nt=' + nombreTrabajador + '&amount=' + montoTotal + '&mon=' + moneda + '&cuotaMes=' + cuotaMes + '&importeDescontado=' + importeDescontado + '&deudaPeriodo=' + deudaPeriodo, 380, 785);
                openDialog('<%=ResolveUrl("~/consultas/ConsultaDetallePagosIBSFechas.aspx") %>?p1=' + codEmpresa + '&p2=' + numPagare + '&p3=' + fechaDesde + '&p4=' + fechaHasta + '&nt=' + nombreTrabajador + '&amount=' + montoTotal + '&mon=' + moneda + '&cuotaMes=' + cuotaMes + '&importeDescontado=' + importeDescontado + '&deudaPeriodo=' + deudaPeriodo, 380, 785);
			}
				
			window.onload = function () {
				document.Form1.chkInclude.onclick = function () {
					document.all('ddlBuscarpor').disabled = !document.all('chkInclude').checked;
					document.all('txtCampo').disabled = !document.all('chkInclude').checked;
				}
			}
		//-->
        </script>
		<style type="text/css">
		<!--
		DIV.hide1 {
			DISPLAY: none;
			z-index:-1;

		}
		DIV.show1 {
			DISPLAY: block;
			z-index:10;
			position:absolute;
			top:100px;
			left:5px;
		}
		-->
		</style>		
	</HEAD>
	<body  leftMargin="0" topMargin="0" rightMargin="0" >
		<form id="Form1" method="post" runat="server">
			<input id="hdParam1" type="hidden" name="hdParam1" runat="server"> <input id="hdParam2" type="hidden" name="hdParam2" runat="server">
			<input id="hdParam3" type="hidden" name="hdParam3" runat="server"> <input id="hdParam4" type="hidden" name="hdParam4" runat="server">
			<input id="hdParam5" type="hidden" name="hdParam5" runat="server">
			<input id="hdParam6" type="hidden" name="hdParam6" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td><uc1:banner id="Banner1" title="Seguimiento de Cobranza &nbsp;" runat="server"></uc1:banner></td>
				</tr>
				<tr>
					<td><br></td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0"  width="750" border="0">
							<tr>
							<td width=25 >&nbsp;</td>
							
								<td vAlign="top">
									<table class="InputField" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<tr><td colspan="5" >&nbsp;</td></tr>
										<tr><td rowspan="4" width="30">&nbsp;</td></tr>
										<tr>
											<td>Empresa
											</td>
											<td colSpan="3">
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td width="430"><asp:textbox id="txtNombreEmpresa" Runat="server" MaxLength="100" Columns="60" ReadOnly="true"></asp:textbox><input id="hdCodigoEmpresa" type="hidden" name="hdCodigoEmpresa" runat="server">
														</td>
														<td>
															<%--<uc2:pnlBusParamEmpresa id="ucBuscarParanetroEmpresa" runat="server" />--%>
															<a href="javascript:fnBuscarEmpresa()"><img alt="buscar" src="<%=ResolveUrl("~/images/texto.gif") %>" border="0"></a>
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
											<td>
											<asp:CheckBox Runat=server ID="chkInclude"  Text="Filtrar también" ></asp:CheckBox>
											</td>
											<td colspan="2">
												<asp:DropDownList ID="ddlBuscarpor" runat="server" Enabled="False"  >
													<asp:ListItem Value="Nombre" >Nombre de Cliente</asp:ListItem>
													<asp:ListItem Value="Codigo" >Código de Pagaré</asp:ListItem>
												</asp:DropDownList>

												<asp:TextBox ID=txtCampo Runat=server Enabled=False ></asp:TextBox>
											</td>
										</tr>
										
										<tr>
											<td align="right" colSpan="5">
												<asp:LinkButton id="lnkBuscar" runat="server">
													<img src="<%=ResolveUrl("~/images/buscar.jpg") %>" alt="Procesar archivo" />
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
				<td width="2">&nbsp;</td>
				<td id="tdMostrar" runat="server" visible="false">
					<div id="dvMostrar">
					&nbsp;Mostrar:&nbsp;
					</div>
				</td>
				<td id="tdddl" runat="server" visible="false" align="right" colspan="3">
					<div id="dvddl">
						<table border="0" cellpadding="0" width="100%">
							<tr>
								<td>
									<asp:DropDownList ID="ddlKind" runat="server" AutoPostBack="true">
										<asp:ListItem Value ="">Todos con/sin descuento</asp:ListItem> 	
										<asp:ListItem Value ="DLID >0">Con descuento</asp:ListItem> 	
										<asp:ListItem Value ="SaldoDeudorAcreedor<0">Con descuento mayor</asp:ListItem> 	
										<asp:ListItem Value ="SaldoDeudorAcreedor=0">Con descuento exacto</asp:ListItem> 	
										<asp:ListItem Value ="SaldoDeudorAcreedor>0">Con descuento menor</asp:ListItem> 	
										<asp:ListItem Value ="SaldoDeudorAcreedor>0 AND DLID >0">Soló descuentos parciales</asp:ListItem> 							
										<asp:ListItem Value ="SaldoDeudorAcreedor>0 AND DLID =0">Sin descuento</asp:ListItem> 							
										<asp:ListItem Value ="SaldoDeudorAcreedor>0 and NUMCUOTAS>1">Deudores con más de una cuota pendiente</asp:ListItem>
										<asp:ListItem Value ="SaldoDeudorAcreedor>0 and NUMCUOTAS=1">Deudores con UNA SOLA cuota pendiente</asp:ListItem>
										<asp:ListItem Value ="ImporteBIFActualizada=0">Sin deuda a la fecha en IBS</asp:ListItem>
										<asp:ListItem Value ="ImporteBIFActualizada<>DLIC">Diferencia entre monto INFORMADO y actual en IBS</asp:ListItem>
										<asp:ListItem Value ="ImporteBIFActualizada<>DLID">Diferencia entre monto RECIBIDO y actual en IBS</asp:ListItem>
										<asp:ListItem Value ="(ImporteBIFActualizada<>DLIC) OR (ImporteBIFActualizada<>DLID)">Diferencia entre monto Informado, Recibido y actual en IBS</asp:ListItem>
										<asp:ListItem Value ="ImporteBIFActualizada<DLID">Devoluciones PROBABLES</asp:ListItem>
										<asp:ListItem Value ="(DLID =0  AND TotalPagosCliente = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTASACTUAL = 1)">Información para Prorroga</asp:ListItem>
										<asp:ListItem Value ="Bloquear=1">Pagares Sin Bloqueo</asp:ListItem>
									</asp:DropDownList>
								<!--						<asp:ListItem Value ="DLID = 0  AND TotalPagosCliente = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTAS = 1">Información para Prorroga</asp:ListItem>
			-->
			<!--
									<asp:ListItem Value ="(DLID <ImporteBIFActualizada  AND TotalPagosCliente = '0.00' AND ImporteBIFActualizada <>0 AND NUMCUOTASACTUAL = 1) OR (DLID <ImporteBIFActualizada AND ImporteBIFActualizada>0 AND NUMCUOTASACTUAL = 1)">Información para Prorroga</asp:ListItem>

			-->
								</td>
								<td>
									<asp:DropDownList ID="ddlUGE" Runat="server" Visible="False" AutoPostBack="True" DataTextField="DESCRIPCIONCORTA" DataValueField="CODE"   >
									</asp:DropDownList>
								</td>
								<td>
									<asp:DropDownList ID="ddlSituacion" runat="server" Visible="False" AutoPostBack="True">
										<asp:ListItem Value="">Todas las situaciones</asp:ListItem>
										<asp:ListItem Value="DLST = 'A'">Activos</asp:ListItem> 
										<asp:ListItem Value="DLST in ('C','S')">Cesantes y Sobrevivientes</asp:ListItem> 
										<asp:ListItem Value="DLST = ''">Sin situacion</asp:ListItem> 						
									</asp:DropDownList>
								</td>
							</tr>
						</table>
				
					</div>
				</td>
					<!--td>
					</td>
					<td>
					</td>-->
			
			</tr>
			<tr>
			<td width="5">&nbsp;</td>
			<td vAlign="top" colspan=3 >
			<br>
			
				<table border="0" cellpadding="2" cellspacing="0" class="box">
					<thead>
						<tr>
							<th>
								<img src="<%= ResolveUrl("~/images/bar_begin.gif") %>" height="17" alt="">
							</th>
							<th>
								<a href="javascript:fnGenerarDocumentoCobranza();">Documento de Cobranza</a></th>
							<%if ddlKind.SelectedIndex = 15 Then 'Pagares sin Bloqueo%> 
							<th>
								<img src="<%= ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
							</th>
							<th>
								<a href="javascript:fnBloquearDesbloquear();">Bloquear/Desbloquear</a></th>
							<%End If%>
								
							<!--<th>
								<img src="<%=Request.ApplicationPath%>/images/bar_div.gif" width="17" height="18"></th>								
							<th>
								<a href="javascript:fnProcesarProrroga();">Prorrogar</a></th>-->
							<th>
								<img src="<%= ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
							</th>
							<th>
								<asp:LinkButton runat="server" ID="lnkGenerarReporte" CausesValidation="false">Obtener informacion en archivo</asp:LinkButton>
							</th>
							<%if ddlKind.SelectedIndex <> 15 then 'Pagares sin Bloqueo%> 
							<th>
								<img src="<%= ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
							</th>
							<th>
								<a href="javascript:fnExportarDocumentoCobranza();">Documento de Cobranza (export)</a>
							</th>
							<%End If%>
							<th>
								<img src="<%= ResolveUrl("~/images/bar_div.gif") %>" width="17" height="18" alt="">
							</th>
							<th>
								<asp:LinkButton runat="server" ID="LinkButton_delete" CausesValidation="false">Eliminar</asp:LinkButton>
							</th>
							<th>
								<img src="<%= ResolveUrl("~/images/bar_end.gif") %>" width="17" height="18" alt="">
							</th>
						</tr>
					</thead>
				</table>
			</td>
			</tr>
			</table>

			<div id="dvProcess" name="dvProcess"/>
			<div id="dvData" name="dvData" runat="server" visible="false" class='show'>
			<div class="tabla">
				<asp:datagrid id="dgProcesoResult" runat="server" CellPadding="3" CellSpacing="3"  BorderWidth="1px" Width="780" AutoGenerateColumns="False" Visible="False"  ShowFooter="True"  AllowPaging="True" PageSize="100">
					<ItemStyle CssClass="TablaNormalBIF" VerticalAlign="Top"></ItemStyle>
					<HeaderStyle CssClass="TablaNormalBoldBIF" VerticalAlign="Top" Height="35"></HeaderStyle>
					<FooterStyle  HorizontalAlign ="Right" BorderWidth="0px"   BorderColor=#ffffff  >
					</FooterStyle>
					<AlternatingItemStyle CssClass="odd"></AlternatingItemStyle>	
					<Columns>
						<asp:TemplateColumn ItemStyle-Width="20" HeaderText="<input type=checkbox id=chkall name=chkall onclick='javascript:checkall();'>">
							<ItemTemplate>
							<input type="hidden" id='dvAmount<%#DataBinder.Eval(Container.DataItem, "DLNP")%>'  value="<%#DataBinder.Eval(Container.DataItem, "ImporteBIFActualizadaReporte")%>"/>
							<input type="checkbox" id="chkData" name="chkData" value='<%#DataBinder.Eval(Container.DataItem, "DLNP")%>' <%#iif(((DataBinder.Eval(Container.DataItem, "SaldoDeudorAcreedor")>0) And (DataBinder.Eval(Container.DataItem, "ImporteBIFActualizada")>0)) or (ddlKind.SelectedIndex = 15) , "", "disabled")%> <%#iif(((InStr(DataBinder.Eval(Container.DataItem, "prorrogado"),"no")<>0))  or (ddlKind.SelectedIndex = 15), "", "disabled")%> <%#iif(((DataBinder.Eval(Container.DataItem, "NotaGenerada").Trim()="")) or (ddlKind.SelectedIndex = 15), "", "disabled")%> <%#iif(DataBinder.Eval(Container.DataItem, "DLID")>DataBinder.Eval(Container.DataItem, "ImporteBIFActualizada") , " disabled ", "" )%> >
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn ItemStyle-Width="20" HeaderText="<input type=checkbox id=chkall_delete name=chkall_delete onclick='javascript:checkall_delete();'>Eliminar">
							<ItemTemplate>
								<input type="checkbox" id="chkData_delete" name="chkData_delete" value='<%#DataBinder.Eval(Container.DataItem, "DLNP")%>' >
							</ItemTemplate>
						</asp:TemplateColumn>
						
						<asp:BoundColumn DataField="DLNP" HeaderText="Pagaré" ItemStyle-Width="80">
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DLNE" HeaderText="Trabajador" ItemStyle-Width="200"></asp:BoundColumn>
						<asp:BoundColumn DataField="DLMO" HeaderText="Mon" ItemStyle-Width="10px"></asp:BoundColumn>
						<asp:BoundColumn DataField="DLIC" HeaderText="Importe BIF Infor-mado" DataFormatString="{0:#.00}"  HeaderStyle-Width="55" ItemStyle-Width="55">
							<ItemStyle HorizontalAlign="Right"  BackColor="#ffffcc" ></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn DataField="DLID" HeaderText="Importe de Institu-ción" DataFormatString="{0:#.00}"  HeaderStyle-Width="55" ItemStyle-Width="55">
							<ItemStyle HorizontalAlign="Right" BackColor="#ffffcc" ></ItemStyle>
						</asp:BoundColumn>
						<asp:BoundColumn Visible="false" DataField="DeudaPeriodo"  HeaderText="Deuda Periodo" DataFormatString="{0:#.00}" ItemStyle-Width="55">
							<ItemStyle  HorizontalAlign="Right"></ItemStyle>
						</asp:BoundColumn>
						<asp:TemplateColumn Visible="false"  headertext="Total Ventani-lla" ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"   HeaderStyle-Width="55" ItemStyle-Width="55"  >
								<ItemTemplate>
								<%#DataBinder.Eval(Container.DataItem, "PAGOVENTANILLA")%>
								</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn  Visible="false" headertext="Total Cta. Cte."  ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="55" ItemStyle-Width="55"   >
								<ItemTemplate>
								<%#DataBinder.Eval(Container.DataItem, "PAGOINTERNET")%>
								</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="false"  headertext="Total Otros (IBS)"   ItemStyle-BackColor="#e0ffff"  ItemStyle-HorizontalAlign="Right"  HeaderStyle-Width="85" ItemStyle-Width="85"  >
								<ItemTemplate>
								<%#DataBinder.Eval(Container.DataItem, "PAGOIBS")%>
								</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Total Pagos Cliente(IBS)"  ItemStyle-BackColor="#00ccff"   HeaderStyle-Width="55" ItemStyle-Width="55" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate><%#getAmount(DataBinder.Eval ( Container.DataItem, "DLCC"), DataBinder.Eval ( Container.DataItem, "DLNP"), DataBinder.Eval ( Container.DataItem, "DLNE"), DataBinder.Eval ( Container.DataItem, "DLMO"),  DataBinder.Eval (Container.DataItem, "DLIC"), DataBinder.Eval (Container.DataItem, "DLID"), DataBinder.Eval (Container.DataItem, "DeudaPeriodo"), DataBinder.Eval (Container.DataItem, "DLFP") , DataBinder.Eval (Container.DataItem, "TotalPagosCliente") )%></ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="SaldoDeudorAcreedor" ItemStyle-BackColor="LemonChiffon"  ItemStyle-HorizontalAlign="Right"   HeaderStyle-Width="80" ItemStyle-Width="80"  HeaderText="Saldo<br>Deudor(+)/<br>Acreedor(-)" DataFormatString="{0:#0.00}"></asp:BoundColumn> 
						<asp:TemplateColumn  HeaderText="Deuda Actual Proyec-tada(IBS)"  HeaderStyle-Width="55" ItemStyle-Width="55" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right">
							<ItemTemplate ><%#Format(DataBinder.Eval(Container.DataItem, "ImporteBIFActualizada"), "#0.00")%></ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Bloq."  HeaderStyle-Width="55" ItemStyle-Width="55" >
						<ItemTemplate>
						<div id="dvData<%#DataBinder.Eval(Container.DataItem, "DLNP")%>" name="dvData<%#DataBinder.Eval(Container.DataItem, "DLNP")%>"><%#DataBinder.Eval(Container.DataItem, "bloqueado")%></div> 
						</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="Pror."  HeaderStyle-Width="55" ItemStyle-Width="55" >
						<ItemTemplate>
						<div id="dvDataP<%#DataBinder.Eval(Container.DataItem, "DLNP")%>" name="dvDataP<%#DataBinder.Eval(Container.DataItem, "DLNP")%>"><%#DataBinder.Eval(Container.DataItem, "prorrogado")%></div> 
						</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="NotaGenerada" HeaderText="Nota" ItemStyle-Width="80">
						</asp:BoundColumn>						
					</Columns>
					<PagerStyle VerticalAlign="Middle" HorizontalAlign="Left" Position="TopAndBottom" CssClass="CommandButton" Mode="NumericPages"></PagerStyle>
				</asp:datagrid>
				</div>
						<!--
						getAmountUpdatedDiscount(DataBinder.Eval ( Container.DataItem, "DLCC"), DataBinder.Eval ( Container.DataItem, "DLNP"), DataBinder.Eval ( Container.DataItem, "DLAP"), DataBinder.Eval ( Container.DataItem, "DLMP"))
						asp:TemplateColumn  HeaderText="Saldo Deudor/<br>Acreedor" >
							<ItemStyle HorizontalAlign="Right"></ItemStyle>
							<ItemTemplate ><--%--#(getAmountUpdatedDiscount(DataBinder.Eval ( Container.DataItem, "DLCC"), DataBinder.Eval ( Container.DataItem, "DLNP"), DataBinder.Eval ( Container.DataItem, "DLAP"), DataBinder.Eval ( Container.DataItem, "DLMP"))- DataBinder.Eval(Container.DataItem, "DLID"))%></ItemTemplate>
						</asp:TemplateColumn
					-->				
				<br>
				<table cellSpacing="0" cellPadding="0" border="0">
					<tr>
						<td class="SubHead">Total de Registros&nbsp;&nbsp;
							<asp:label CssClass="Text" id="lblTotalReg" Runat="server"></asp:label></td>
					</tr>
				</table>
			</div>
			<asp:CustomValidator id="cvValida" runat="server" ClientValidationFunction="Valida" Display="None" ErrorMessage="CustomValidator"></asp:CustomValidator>
		</form>
		<form id="frmData" action="ContainerDocumentoCobranza.aspx" target="_blank" method="post">
			<input type="hidden" name="dt" id="dt"> 
			<input type="hidden" name="p" id="p">
			<input type="hidden" name="a" id="a">
			<input type="hidden" name="anio" id="anio">
			<input type="hidden" name="mes" id="mes">
			<input type="hidden" name="proceso" id="proceso">
		</form>
		<form id="frmData1" action="ContainerDocumentoCobranza.aspx" target="_blank" method="post">
			<input type="hidden" name="dt" id="dt"> 
			<input type="hidden" name="p" id="p">
			<input type="hidden" name="a" id="a">
			<input type="hidden" name="anio" id="anio">
			<input type="hidden" name="mes" id="mes">
			<input type="hidden" name="proceso" id="proceso">
			<input type="hidden" name="export" id="export" value ="1">
		</form>
		
		<div id="divFrame" name="divFrame" class="hide1">
		<iframe id="fraProccess"  name="fraProccess"   height="400" width="800" frameborder=0>
		</iframe>
		</div>
		<form id="frmSnd" name="frmSnd" target="fraProccess" action="ProcesarBloqueosTestWait.aspx" method="post">
			<input type="hidden" name="pagares" id="pagares">
			<input type="hidden" name="proceso" id="proceso">
		</form>				
		<form id="frmSndP" name="frmSndP" target="fraProccess" action="ProcesarProrrogasTestWait.aspx" method="post">
			<input type="hidden" name="pagares" id="pagares">
			<input type="hidden" name="proceso" id="proceso">
		</form>				
	</body>
</HTML>
