Imports System.Web
Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Data.SqlClient
Imports System.Web.Services

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Namespace BIFConvenios

    Partial Class ResultadoProcesoCronogramaFuturo
        Inherits System.Web.UI.Page
        ''Protected WithEvents ltrlNombre As System.Web.UI.WebControls.Literal
        ''Protected WithEvents ltrlNombre1 As System.Web.UI.WebControls.Literal
        ''Protected WithEvents ltrlAnhio As System.Web.UI.WebControls.Literal
        ''Protected WithEvents ltrlMes As System.Web.UI.WebControls.Literal
        ''Protected WithEvents ltrlFechaIBS As System.Web.UI.WebControls.Literal

        Protected oproc As New Proceso()
        Protected objProcesoBL As New clsProcesoBL()
        Protected idGenFile As String = ""
        Protected formatoArchivo As String = ""
        Protected strTipoCliente As String = ""
        Protected situacionTrabajador As String = ""
        Protected idP As String = ""
        Protected modalidad As String = ""
        Protected ImporteHistorico As Decimal

        Protected objArchivosConvenioBL As New clsArchivosConveniosBL()
        Protected objArchivosConvenio As New clsArchivosConvenios()

        Protected Documento As String
        Protected Trabajador As String
        Protected Pagare As String
        Protected EstadoTrabajador As String
        Protected ZonaUse As String

        Protected strCodProceso As String
        Protected strCodIBS As String
        Protected strConsulta As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()



        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            strCodProceso = Request.Params("id").ToString()
            Session("strCodProceso") = strCodProceso

            strCodIBS = Request.Params("codIBS").ToString()
            Session("strCodIBS") = strCodIBS

            strConsulta = Request.Params("consultar").ToString()
            Session("consultar") = strConsulta

            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                Dim dr As SqlDataReader
                Dim ShowGrid As Boolean
                dr = oproc.InformeProceso(CType(Request.Params("id"), String))

                Utils.AddSwap(lnkBack, "Image1", Request.ApplicationPath + "/images/regresar_on.jpg")

                If dr.Read Then
                    ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                    ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                    ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                    'ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                    ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                    ltrlNombre.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlAnhio.Text = CType(dr("Anio_periodo"), String)
                    ltrlMes.Text = MonthName(CType(dr("Mes_Periodo"), Integer))
                    ltrlFechaIBS.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                    ltrlNombre1.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlAnhio1.Text = CType(dr("Anio_periodo"), String)
                    ltrlMes1.Text = MonthName(CType(dr("Mes_Periodo"), Integer))
                    ltrlFechaIBS1.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                    'lblMontoTotalSoles.Text = Format(dr("MontoProcesadoSoles"), "#,###.##")
                    'lblMontoTotalDolares.Text = Format(dr("MontoProcesadoDolares"), "#,###.##")
                    'lblMontoTotalSoles1.Text = IIf(Trim(dr("MontoProcesadoSoles")).Equals(""), "0", Trim(dr("MontoProcesadoSoles")))
                    'lblMontoTotalDolares1.Text = IIf(Trim(dr("MontoProcesadoDolares")).Equals(""), "0", Trim(dr("MontoProcesadoDolares")))
                    ShowGrid = CType(dr("ShowGrid"), Boolean)
                End If


                'Muestra los Montos totales del listado de la grilla
                MostrarTotales()

                ' Obtiene el estado del Trabajador
                ddlEstadoTrabajador.DataSource = oproc.GetEstadosTrabajador(Request.Params("id"))
                ddlEstadoTrabajador.DataBind()
                ddlEstadoTrabajador.Items.Insert(0, New ListItem("-- Todos --", ""))

                ' Obtiene la Zona Use
                Dim drZone As SqlDataReader
                Dim strListaZonas As String = String.Empty
                drZone = oproc.GetDatosZonaUse(Request.Params("id"))

                If drZone.HasRows Then
                    With drZone.Read()
                        strListaZonas = drZone.GetString(0).ToString()
                    End With

                    Dim listZonaUse As New List(Of String)
                    Dim intLength As Integer = strListaZonas.Length

                    ddlZonaUse.Items.Insert(0, New ListItem("-- Todos --", ""))

                    For i As Integer = 0 To (intLength / 2) - 1
                        ddlZonaUse.Items.Insert(i + 1, New ListItem(strListaZonas.Substring(2 * i, 2), strListaZonas.Substring(2 * i, 2)))
                    Next

                    ddlZonaUse.Enabled = True
                Else
                    ddlZonaUse.Enabled = False
                End If

                'If drZone.HasRows = True Then
                '    ddlZonaUse.DataSource = oproc.GetDatosZonaUse(Request.Params("id"))
                '    ddlZonaUse.DataBind()
                '    ddlZonaUse.Items.Insert(0, New ListItem("Todos", "0"))
                '    ddlZonaUse.Enabled = True
                'Else
                '    ddlZonaUse.Enabled = False
                'End If

            End If

            If strConsulta = "1" Then
                LnkNuevo.Enabled = False
                dgProcesoResult.Columns(20).Visible = False
            Else
                LnkNuevo.Enabled = True
                dgProcesoResult.Columns(20).Visible = True
            End If
        End Sub

        Private Sub lnkBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBack.Click
            Session("Criterio") = Nothing
            Session("Valor") = Nothing

            If (strConsulta = 0) Then
                Response.Redirect(Request.ApplicationPath + "/cargageneracioncf.aspx")
            Else
                Response.Redirect(Request.ApplicationPath + "/ReporteCronogramaFuturo.aspx")
            End If
        End Sub

        Sub MostrarTotales()

            Dim ShowGrid As Boolean
            Dim strCodigoProceso As String = String.Empty
            Dim strTrabajador As String = String.Empty
            Dim strDocumento As String = String.Empty
            Dim decPagare As Decimal = 0
            Dim strEstadoTrabajador As String = String.Empty
            Dim strZonaUse As String = String.Empty
            Dim Monto As Decimal = 0
            Dim strMoneda As String = String.Empty

            pnlQueryResult.Visible = True
            pnlMensaje.Visible = False

            If (Not Request.Params("id") Is Nothing) Then
                strCodigoProceso = Request.Params("id").ToString()
            End If

            If Len(txtFiltroDocumento.Text) > 0 Then
                strDocumento = txtFiltroDocumento.Text
            Else
                strDocumento = ""
            End If

            If Len(txtFiltroTrabajador.Text) > 0 Then
                strTrabajador = txtFiltroTrabajador.Text
            Else
                strTrabajador = ""
            End If

            If Len(txtFiltroPagare.Text) > 0 Then
                decPagare = Convert.ToDecimal(txtFiltroPagare.Text)
            Else
                decPagare = 0
            End If

            strEstadoTrabajador = ddlEstadoTrabajador.SelectedValue.ToString()
            strZonaUse = ddlZonaUse.SelectedValue.ToString()

            'Documento = IIf(txtFiltroDocumento.Text Is "", "*", txtFiltroDocumento.Text)
            'Trabajador = IIf(txtFiltroTrabajador.Text Is "", "*", txtFiltroTrabajador.Text)

            'If Len(txtFiltroPagare.Text) = 0 Then
            '    Pagare = 0
            'Else
            '    Pagare = Convert.ToDecimal(txtFiltroPagare.Text)
            'End If

            'EstadoTrabajador = IIf(ddlEstadoTrabajador.SelectedValue Is "", "-", ddlEstadoTrabajador.SelectedValue)
            'ZonaUse = IIf(ddlZonaUse.SelectedValue Is "", "0", ddlZonaUse.SelectedValue)            

            If Not ShowGrid Then
                Session("documento") = strDocumento
                Session("trabajador") = strTrabajador
                Session("pagare") = decPagare
                Session("EstadoTrabajador") = strEstadoTrabajador
                Session("ZonaUse") = strZonaUse
            End If

            'Dim ds As DataSet = oproc.GetRegistrosResultadoProceso(Request.Params("id"), Documento, Trabajador, Pagare, EstadoTrabajador, ZonaUse)
            Dim _dtRegistroProcesos As New DataTable()

            Try
                _dtRegistroProcesos = objProcesoBL.ObtieneRegistroResultadoProcesoPorFiltros(strCodigoProceso, strDocumento, strTrabajador, decPagare, strEstadoTrabajador, strZonaUse)

                For Each dr As DataRow In _dtRegistroProcesos.Rows
                    Monto += dr("DLIC")
                    strMoneda = dr("DLMO")
                Next

                If strMoneda = "SOL" Then
                    lblMontoTotalSoles.Text = Format(Monto, "#,###.##")
                    lblMontoTotalSoles1.Text = IIf(Trim(Monto).Equals(""), "0", Trim(Monto))
                    lblMontoTotalDolares.Text = Format(0, "#,###.##")
                    lblMontoTotalDolares1.Text = IIf(Trim(0).Equals(""), "0", Trim(0))
                Else
                    lblMontoTotalSoles.Text = Format(0, "#,###.##")
                    lblMontoTotalSoles1.Text = IIf(Trim(0).Equals(""), "0", Trim(0))
                    lblMontoTotalDolares.Text = Format(Monto, "#,###.##")
                    lblMontoTotalDolares1.Text = IIf(Trim(Monto).Equals(""), "0", Trim(Monto))
                End If

                dgProcesoResult.DataSource = _dtRegistroProcesos
                dgProcesoResult.DataBind()

                dgProcesoResult.Visible = True
                lblTotalReg.Text = _dtRegistroProcesos.Rows.Count.ToString 'dgProcesoResult.Items.Count.ToString.Trim()
                lblTotalReg1.Text = _dtRegistroProcesos.Rows.Count.ToString
            Catch ex As HandledException
                If ex.ErrorTypeId = -400 Then
                    pnlQueryResult.Visible = False
                    pnlMensaje.Visible = True

                    lblTotalReg.Text = "0"
                    lblTotalReg1.Text = "0"
                    lblMontoTotalSoles.Text = "0"
                    lblMontoTotalSoles1.Text = "0"
                    lblMontoTotalDolares.Text = "0"
                    lblMontoTotalDolares1.Text = "0"
                    lblMensajeSistema.Text = "No hay datos"
                Else
                    pnlQueryResult.Visible = False
                    pnlMensaje.Visible = True

                    lblTotalReg.Text = "0"
                    lblTotalReg1.Text = "0"
                    lblMontoTotalSoles.Text = "0"
                    lblMontoTotalSoles1.Text = "0"
                    lblMontoTotalDolares.Text = "0"
                    lblMontoTotalDolares1.Text = "0"
                    lblMensajeSistema.Text = ex.ErrorMessageFull.ToString()
                End If
            End Try
        End Sub

        Protected Sub dgProcesoResult_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.CancelCommand

            dgProcesoResult.EditItemIndex = -1
            Me.MostrarTotales()

        End Sub


        Protected Sub dgProcesoResult_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.EditCommand

            dgProcesoResult.EditItemIndex = e.Item.ItemIndex

            Dim lblImporte As Label
            lblImporte = CType(e.Item.Cells(1).FindControl("lblImporte"), Label)
            Session("Importe") = lblImporte.Text

            Dim lblCuotaPactada As Label
            lblCuotaPactada = CType(e.Item.Cells(1).FindControl("lblCuotaPactada"), Label)
            Session("CuotaPactada") = lblCuotaPactada.Text

            Dim lblCuotaPagada As Label
            lblCuotaPagada = CType(e.Item.Cells(1).FindControl("lblCuotaPagada"), Label)
            Session("CuotaPagada") = lblCuotaPagada.Text

            Dim lblCuotaPendiente As Label
            lblCuotaPendiente = CType(e.Item.Cells(1).FindControl("lblCuotaPendiente"), Label)
            Session("lblCuotaPendiente") = lblCuotaPendiente.Text

            Me.MostrarTotales()

        End Sub

        Protected Sub dgProcesoResult_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.UpdateCommand

            Dim Codigo_proceso As String = Request.Params("id")

            Dim Importe As Decimal
            Dim txtImporte As TextBox
            txtImporte = CType(e.Item.Cells(1).FindControl("txtImporte"), TextBox)
            Importe = txtImporte.Text

            Dim CodigoClienteIBS As String
            Dim lblCodigoClienteIBS As Label
            lblCodigoClienteIBS = CType(e.Item.Cells(1).FindControl("lblDLCC"), Label)
            CodigoClienteIBS = lblCodigoClienteIBS.Text

            Dim Pagare As String
            Dim lblPagare As Label
            lblPagare = CType(e.Item.Cells(1).FindControl("lblDLNP"), Label)
            Pagare = lblPagare.Text

            Dim Trabajador As String
            Dim lblTrabajador As Label
            lblTrabajador = CType(e.Item.Cells(1).FindControl("lblDLNE"), Label)
            Trabajador = lblTrabajador.Text

            Dim CuotaPactada As Integer
            Dim txtCuotaPactada As TextBox
            txtCuotaPactada = CType(e.Item.Cells(1).FindControl("txtCuotaPactada"), TextBox)
            CuotaPactada = txtCuotaPactada.Text

            Dim CuotaPagada As Integer
            Dim txtCuotaPagada As TextBox
            txtCuotaPagada = CType(e.Item.Cells(1).FindControl("txtCuotaPagada"), TextBox)
            CuotaPagada = txtCuotaPagada.Text

            Dim CuotaPendiente As Integer
            CuotaPendiente = CuotaPactada - CuotaPagada

            Dim li_fila As Integer
            li_fila = dgProcesoResult.DataKeys.Item(e.Item.ItemIndex)

            If li_fila > 0 Then

                'Actualiza en la tabla ClienteCuota
                oproc.ActualizarClienteCuota(Codigo_proceso, CodigoClienteIBS, Pagare, Importe, CuotaPactada, CuotaPagada, CuotaPendiente)

                'Inserta en la tabla HistoricoClienteCuota
                oproc.AddHistorico_ClienteCuota(Codigo_proceso, CodigoClienteIBS, Pagare, Trabajador, CType(Session("Importe"), Decimal), Context.User.Identity.Name, CType(Session("CuotaPactada"), Integer), CType(Session("CuotaPagada"), Integer), CType(Session("lblCuotaPendiente"), Integer))
                dgProcesoResult.EditItemIndex = -1

                Me.MostrarTotales()


            End If

        End Sub

        Protected Sub dgProcesoResult_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.ItemCommand

            Dim Codigo_proceso As String = Request.Params("id")

            If (e.CommandName = "Eliminar") Then

                Dim index As DataGridItem = dgProcesoResult.Items(e.Item.ItemIndex)

                Dim CodigoClienteIBS As String = DirectCast(index.FindControl("lblCodigoCliente"), Label).Text
                Dim Pagare As String = DirectCast(index.FindControl("lblPagare"), Label).Text

                oproc.EliminarTrabajadorClienteCuota(Codigo_proceso, CodigoClienteIBS, Pagare)

                Me.MostrarTotales()

            End If

        End Sub

        Private Sub dgProcesoResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResult.PageIndexChanged
            dgProcesoResult.CurrentPageIndex = IIf(dgProcesoResult.PageCount < e.NewPageIndex, 0, e.NewPageIndex)

            Me.MostrarTotales()

        End Sub

        'Protected Sub lnkEnviarCorreo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEnviarCorreo.Click
        '    If lstFormatFile.Text = "xls" Then
        '        Response.Redirect("selectFileFormat.aspx?id=" + strCodProceso + "&ibs=" + strCodIBS + "&consultar=" + strConsulta.ToString())
        '    Else

        '    End If
        'End Sub

        Private Sub lnkGenerarArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkGenerarArchivo.Click

            If Not hdId.Value Is Nothing And hdId.Value.Trim <> "" Then
                oproc.UpdRestauraEstadoInicial(CType(hdId.Value.Trim.Split("|")(0), String), Context.User.Identity.Name)
                idGenFile = CType(hdId.Value.Trim, String)
                hdId.Value = ""
                idP = idGenFile.Trim.Split("|")(0)
                formatoArchivo = idGenFile.Trim.Split("|")(1)
                'strTipoCliente = idGenFile.Trim.Split("|")(2)
                'situacionTrabajador = idGenFile.Trim.Split("|")(2)
                modalidad = idGenFile.Trim.Split("|")(2) ' ADD JCHAVEZH 21/10/2014

                If formatoArchivo = "defaultXls" Then
                    Response.Redirect(Request.ApplicationPath + "/frmEnvioMail.aspx?id=" + strCodProceso + "&ibs=" + strCodIBS + "&consultar=" + strConsulta.ToString())
                Else
                    pnlGenArchivos.Visible = True
                End If

            End If

        End Sub

        Private Sub lnkObtenerReporte_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkObtenerReporte.Click
            ''/* MOD NCA 08/07/2014 EA2013-273 OPT PROC CONVENIOS*/
            ''Session.Add("Info", ArchivosDescuento.getDataSetListadoCuotasFin(hdId.Value))
            ''/* END */
            'Dim oProceso As New BIFConvenios.Proceso()
            'oProceso.UpdateFechaObtencionArchivo(hdId.Value, False, Context.User.Identity.Name)
            'Proceso.UpdEstadoGeneracionExito(hdId.Value, Context.User.Identity.Name)
            ''/* MOD NCA 08/07/2014 EA2013-273 OPT PROC CONVENIOS*/
            ''Response.Redirect(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx?export=1"))
            ''/* END */

            ''/* ADD NCA 08/07/2014 EA2013-273 OPTIMIZACION PROCESOS CONVENIOS */
            'Dim strNombreArchivo As String = getWebServerDateId() & ".xls"
            'Call (New ArchivosDescuento).GenerarArchivExcelDescuentos(hdId.Value, strNombreArchivo)
            'Call Utils.InvocarArchivo(ConfigurationManager.AppSettings("GenFolder"), strNombreArchivo)
            ''/* END */
            'Session.Add("Info", ArchivosDescuento.getDataSetListadoCuotasFin(hdId.Value))
            'Dim oProceso As New BIFConvenios.Proceso()
            'oProceso.UpdateFechaObtencionArchivo(hdId.Value, False, Context.User.Identity.Name)
            'Proceso.UpdEstadoGeneracionExito(hdId.Value, Context.User.Identity.Name)
            'Response.Redirect(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx?export=1"))
            If Not hdFilter.Value Is Nothing And hdFilter.Value.Trim <> "" Then
                pnlGenReporte.Visible = True
                idP = CType(hdFilter.Value.Trim, String).Trim.Split("|")(0)
                modalidad = CType(hdFilter.Value.Trim, String).Trim.Split("|")(1)
                situacionTrabajador = CType(hdFilter.Value.Trim, String).Trim.Split("|")(2)
            End If
        End Sub

        '/* ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS */
        'Private Sub lnkEnviarMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviarMail.Click

        '    Dim oProceso As New BIFConvenios.Proceso()
        '    oProceso.UpdateFechaObtencionArchivo(hdId.Value, False, Context.User.Identity.Name)
        '    Proceso.UpdEstadoGeneracionExito(hdId.Value, Context.User.Identity.Name)

        '    Dim strNombreArchivo As String = getWebServerDateId() & ".xls"
        '    Call (New ArchivosDescuento).GenerarArchivExcelDescuentos(hdId.Value, strNombreArchivo)

        '    Call OpenWindow("consultas/EnvioReporteMail.aspx?idP=" & hdId.Value & "&archivo=files&#92;generados&#92;" & strNombreArchivo)
        'End Sub

        Public Sub OpenWindow(ByVal url As String)
            Dim s As String = "openPage('" & url + "', 430,500);"
            ClientScript.RegisterStartupScript(Me.GetType(), "script", s, True)
        End Sub
        '/* END*/


        'Protected Sub dgProcesoResult_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.EditCommand
        '    dgProcesoResult.EditItemIndex = e.Item.ItemIndex
        '    importeAnterior.Text = e.Item.Cells(7).Text
        '    BindDS()
        'End Sub

        'Protected Sub dgProcesoResult_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.CancelCommand
        '    dgProcesoResult.EditItemIndex = -1
        '    lblMensajeImp.Text = ""
        '    BindDS()
        'End Sub

        'Protected Sub dgProcesoResult_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgProcesoResult.UpdateCommand
        '    Dim importeNuevo As Double
        '    If CType(e.Item.Cells(7).Controls(0), TextBox).Text = "" Then
        '        lblMensajeImp.Text = "Ingrese el nuevo importe"
        '        Return
        '    ElseIf Not Double.TryParse(CType(e.Item.Cells(7).Controls(0), TextBox).Text, importeNuevo) Then
        '        lblMensajeImp.Text = "Ingrese un importe válido"
        '        Return
        '    ElseIf Page.IsValid Then
        '        lblMensajeImp.Text = ""
        '        'actualiza importe proceso y se graba en el tabla evento de sistema la modificacion realizada
        '        oproc.ActualizaProcesoCliente(Request.Params("id"), e.Item.Cells(1).Text, CType(CType(e.Item.Cells(7).Controls(0), TextBox).Text, Double), _
        '                      "Actualiza Importe Cliente - Parametros: " & " pCodigo_proceso=" & Request.Params("id") & ", pCodigo_pagare=" & e.Item.Cells(1).Text & ", pImporte_nuevo=" & CType(e.Item.Cells(7).Controls(0), TextBox).Text & ", pImporte_Anterior=" & importeAnterior.Text, Context.User.Identity.Name)

        '        dgProcesoResult.EditItemIndex = -1
        '        ReloadPage()
        '    End If
        'End Sub

        'Private Sub BindDS()
        '    Dim ds As DataSet = oproc.GetRegistrosResultadoProceso(Request.Params("id"))
        '    dgProcesoResult.DataSource = ds
        '    dgProcesoResult.DataBind()
        '    dgProcesoResult.Visible = True
        '    lblTotalReg.Text = ds.Tables(0).Rows.Count.ToString 'dgProcesoResult.Items.Count.ToString.Trim()
        '    pnlGenArchivos.Visible = False
        '    pnlGenReporte.Visible = False
        'End Sub
        Private Sub ReloadPage()
            Dim dr As SqlDataReader
            Dim ShowGrid As Boolean
            dr = oproc.InformeProceso(CType(Request.Params("id"), String))

            Utils.AddSwap(lnkBack, "Image1", Request.ApplicationPath + "/images/regresar_on.jpg")

            If dr.Read Then
                ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                'ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                'ltrlNombre.Text = CType(dr("Nombre_Cliente"), String)
                'ltrlAnhio.Text = CType(dr("Anio_periodo"), String)
                'ltrlMes.Text = MonthName(CType(dr("Mes_Periodo"), Integer))
                'ltrlFechaIBS.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                ltrlNombre1.Text = CType(dr("Nombre_Cliente"), String)
                ltrlAnhio1.Text = CType(dr("Anio_periodo"), String)
                ltrlMes1.Text = MonthName(CType(dr("Mes_Periodo"), Integer))
                ltrlFechaIBS1.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))
                ShowGrid = CType(dr("ShowGrid"), Boolean)

            End If

            If Not ShowGrid Then
                Me.MostrarTotales()
                pnlGenArchivos.Visible = False
                pnlGenReporte.Visible = False
            End If

        End Sub

        Protected Sub RedirectReporte(ByVal codigo_proceso_ As String, ByVal modalidad As String, ByVal situacionTrabajador As String)
            Session.Add("Info", ArchivosDescuento.getDataSetListadoCuotasFin(codigo_proceso_, modalidad, situacionTrabajador))
            Dim oProceso As New BIFConvenios.Proceso()
            oProceso.UpdateFechaObtencionArchivo(codigo_proceso_, False, Context.User.Identity.Name)
            oProceso.UpdEstadoGeneracionExito(codigo_proceso_, Context.User.Identity.Name)
            'Response.Redirect(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx?export=1"))
            Response.Redirect(Request.ApplicationPath + "/consultas/ContainerListadoConsultaEnvio.aspx?export=1")
        End Sub

        Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click

            Dim Documento, Trabajador, Pagare, EstadoTrabajador, ZonaUse As String

            Documento = IIf(txtFiltroDocumento.Text Is "", "*", txtFiltroDocumento.Text)
            Trabajador = IIf(txtFiltroTrabajador.Text Is "", "*", txtFiltroTrabajador.Text)
            Pagare = IIf(txtFiltroPagare.Text Is "", "0", txtFiltroPagare.Text)
            EstadoTrabajador = IIf(ddlEstadoTrabajador.SelectedValue Is "", "-", ddlEstadoTrabajador.SelectedValue)
            ZonaUse = IIf(ddlZonaUse.SelectedValue Is "", "0", ddlZonaUse.SelectedValue)

            Me.MostrarTotales()

        End Sub

        Protected Sub LnkNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LnkNuevo.Click
            'LimpiarTextbox()
            'MdlPupTrabajador.Show()
            Response.Redirect(Request.ApplicationPath + "/RegistroCliente.aspx")
        End Sub

    End Class
End Namespace
