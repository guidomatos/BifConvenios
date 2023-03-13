Imports System.IO
Imports System.Data.SqlClient
Imports BIFConvenios

Partial Class ReportePostConciliacion
    Inherits Page
    Protected oproc As New Proceso()

    Protected dsIBS As DataSet
    Protected dsIBSTD As DataSet
    Protected dsData As New DataSet

    Protected dsIBSCuotas As DataSet  'Información actualizada de las cuotas
    Protected dsIBSDatosAdicionales As DataSet  'Informacion de datos adicionales de los clientes
    Protected dsIBSDatosCuentas As DataSet  'Informacion de cuentas de los clientes
    Protected dsIBSDatosCastigo As DataSet  'Informacion de castigos de los clientes
    Protected dsIBSDatosDevolucion As DataSet 'Informacion devolucion
    Protected dblImporteInstitucion As Decimal = 0
    Protected dblImporteBIFActualizado As Decimal = 0
    Protected dblSaldoDeudorAcreedor As Decimal = 0
    Protected dblPAGOIBSPROCESOCOBRANZA As Decimal = 0

    Protected dblImporteInformado As Decimal = 0

    Protected objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here

        If Not Page.IsPostBack Then
            txtFechaDesde.Text = Format(DateSerial(Year(Now), Month(Now), 1), "dd/MM/yyyy")
            txtFechaHasta.Text = Format(DateSerial(Year(Now), Month(Now) + 1, 0), "dd/MM/yyyy")
            Utils.AddSwap(lnkBuscar, "Image1", "~/images/procesar_on.jpg")
        End If

        ddlBuscarpor.Enabled = chkInclude.Checked
        txtCampo.Enabled = chkInclude.Checked

    End Sub

    Private Function GetCondicionFiltro() As String

        Dim condicionFiltro As String = ""
        condicionFiltro = ddlKind.SelectedItem.Value
        If ddlUGE.SelectedItem IsNot Nothing Then
            condicionFiltro += IIf(ddlUGE.SelectedItem.Value.Trim <> "", IIf(ddlKind.SelectedItem.Value.Trim() <> "", " AND ", "") + " UGE='" + ddlUGE.SelectedItem.Value.Trim + "'", "")
        End If
        condicionFiltro += IIf(ddlSituacion.SelectedItem.Value.Trim <> "", IIf(condicionFiltro.Trim <> "", " AND ", "") + ddlSituacion.SelectedItem.Value, "")
        Return condicionFiltro

    End Function

    Private Sub lnkGenerarReporte_Click(sender As Object, e As EventArgs) Handles lnkGenerarReporte.Click

        Dim dv, dvDescuento As New DataView()
        Dim fileName As String = Utils.getWebServerDateId() + ".xls"

        Me.MostrarInformacionSeguimiento()

        Dim _dtDescuento As New DataTable()

        _dtDescuento = Session("dsDescuento")

        'dv = New DataView(dsData.Tables("Descuentos"), Me.getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)
        dvDescuento = New DataView(_dtDescuento, Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)

        'Se actualiza la fecha de post conciliacion del proceso. Christian Rivera 15-01-2015 EA273 Mejoras Convenios
        oproc.ActualizaFechaPostConciliacion(hdParam1.Value.Trim())

        LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dvDescuento, ConfigurationManager.AppSettings("GenFolder"), fileName, ConfigurationManager.AppSettings("archivoPostConciliacionCampos"), ConfigurationManager.AppSettings("archivoPostConciliacionTitulos"))

        Dim ms As IO.MemoryStream
        Dim fileS As FileStream = File.OpenRead(ConfigurationManager.AppSettings("GenFolder") + fileName)

        ms = New MemoryStream(fileS.Length)
        Dim br As New BinaryReader(fileS)
        Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

        ms.Write(bytesRead, 0, fileS.Length)

        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/vnd.ms-excel"
            .AddHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".xls")
            .BinaryWrite(ms.ToArray)
            .End()
        End With

    End Sub

    Private Sub lnkBuscar_Click(sender As Object, e As EventArgs) Handles lnkBuscar.Click

        Dim daTransform As New Data.OleDb.OleDbDataAdapter()
        Dim oDS As New DataSet()

        Dim lds As DataSet
        Dim ldt As New DataTable
        Dim dv As DataView
        'Dim CuotaBL As New CuotaBL()
        Dim Cuota As New Cuota()
        Dim result As New ADODB.Recordset()
        Dim dsJoin As New DataSet()
        Dim dsTest As SqlDataReader
        Dim intCodigoIBS As Integer = 0
        Dim dsDescuento As New DataSetDescuentos()
        Dim DLDNI As String

        dsTest = oproc.GetCodigoClienteIBS(hdParam1.Value.Trim())

        With dsTest.Read
            intCodigoIBS = Convert.ToInt32(dsTest.Item("CodigoCliente").ToString())
        End With

        dsData = oproc.GetRegistrosResultadoProcesoDescuentos(hdParam1.Value.Trim(), Me.NumeroPagare, Me.NombreCliente)

        ActualizaInformacionDataSet(dsData)
        Session.Add("dsDataPostConciliacion", dsData)

        'OBTENER MOTIVOS DE IBS DE UNA EMPRESA
        lds = objWSConvenios.ConsultarMotivoIBS(hdParam1.Value.Trim(), intCodigoIBS.ToString(), hdParam2.Value.Trim(), hdParam3.Value.Trim())

        Dim Motivo As String
        Dim fila() As DataRow

        For Each dr As DataRow In dsData.Tables("Descuentos").Rows
            fila = lds.Tables("table").Select("DLRNP = " & dr("DLNP"))
            If fila.Length > 0 Then
                Motivo = fila(0).Item("DLDMO")
            Else
                Motivo = ""
            End If

            DLDNI = IIf(dr("DLDNI") Is DBNull.Value, "", dr("DLDNI"))

            'dsDescuento.DESCUENTO.AddDESCUENTORow(dr("DLNP"), dr("DLCM"), dr("DLNE"), dr("DLAP"), dr("DLMP"), dr("DLIC"), dr("DLID"), dr("DeudaPeriodo"), dr("ImporteBIFActualizada"), _
            '                                      dr("PAGOIBSPROCESOCOBRANZA"), dr("DEVOLUCION"), dr("UGE"), dr("NUMCUOTAS"), dr("NUMCUOTASACTUAL"), dr("SITUACIONPAGARE"), dr("CUENTACARGO"), _
            '                                      dr("PAGOPARCIAL"), dr("SITUACIONLABORALACTUAL"), dr("DIRECCION"), dr("CUIDAD"), dr("TELCASA"), dr("TELTRABAJO"), dr("TELOTRO"), DLDNI, _
            '                                      dr("ESTADOPAGARE"), dr("CUENTAAHORRO"), dr("CASTIGORCD"), dr("DIFERENCIA"), Motivo, dr("SaldoDeudorAcreedor"))
            dsDescuento.DESCUENTOEXCEL.AddDESCUENTOEXCELRow(dr("DLNP"), dr("DLCM"), DLDNI, dr("DLNE"), dr("DLAP"), dr("DLMP"), dr("DLID"), dr("PAGOIBSPROCESOCOBRANZA"), dr("DEVOLUCION"), dr("DIFERENCIA"),
                                                            dr("ESTADOPAGARE"), dr("CUENTAAHORRO"), dr("TIPODEVOLUCION"), dr("CASTIGORCD"), Motivo, dr("SaldoDeudorAcreedor")) 'EA2017-11386

        Next

        'Session.Add("dsDataPostConciliacion", dsData)
        Session.Add("dsDescuento", dsDescuento.DESCUENTOEXCEL)

        ddlUGE.DataSource = Proceso.getUGES(hdParam1.Value)
        ddlUGE.DataBind()

        If ddlUGE.Items.Count > 0 Then
            ddlUGE.Visible = True
            ddlUGE.Items.Insert(0, New ListItem("Todas las UGE", ""))
        Else
            ddlUGE.Visible = False
        End If

        dv = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)) 'dsData
        'dv = (New DataView(dsDescuento.DESCUENTO, Me.getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)) 'dsData
        GetCalculoResumenCarga(dv)

        dgProcesoResult.CurrentPageIndex = 0
        dgProcesoResult.DataSource = dv
        dgProcesoResult.DataBind()
        dgProcesoResult.Visible = True

        lblTotalReg.Text = dsData.Tables(0).Rows.Count  'dgProcesoResult.Items.Count.ToString.Trim()
        dvData.Visible = True
        tdMostrar.Visible = True
        tdddl.Visible = True
        ddlSituacion.Visible = True

    End Sub

#Region "Actualizacion de informacion del DataSet desde IBS"
    'Actualizamos los datos del Dataset desde IBS
    Protected Sub ActualizaInformacionDataSet(ByRef dsData As DataSet)
        Dim dr As DataRow
        Dim importe As String
        Dim PAGOVENTANILLA As String, PAGOINTERNET As String, PAGOIBS As String, PAGOIBSPROCESOCOBRANZA As String, NUMCUOTASACTUAL As String = "0", SITUACIONPAGARE As String = "", CUENTACARGO As String = "", PAGOPARCIAL As String = "0.00", SITUACIONLABORALACTUAL As String = ""
        Dim DIRECCION As String = "", CUIDAD As String = "", TELCASA As String = "", TELTRABAJO As String = "", TELOTRO As String = "", DLDNI As String = "", ESTADOPAGARE As String = "", CUENTAAHORRO As String = "", CASTIGORCD As String = ""
        Dim DEVOLUCION As String = "0.00"
        Dim MOTDEVOLUCION As String = "" 'EA2017-11386
        ' Try

        For Each dr In dsData.Tables(0).Rows()
            dr("TotalPagosCliente") = GetTotalPagosCliente(dr("DLCC"), GetSQLDate(txtFechaDesde.Text), GetSQLDate(txtFechaHasta.Text), dr("DLNP"), dr("LOTE"), PAGOVENTANILLA, PAGOINTERNET, PAGOIBS, PAGOIBSPROCESOCOBRANZA, SITUACIONPAGARE, CUENTACARGO)
            'RESPINOZA 20061023 - Los pagos son ahora calculados de forma diferente
            '                       Diferencia del Mes = Monto de Envio - (Monto Recibido + Total Pagos)
            dr("SaldoDeudorAcreedor") = Decimal.Parse(IIf(TypeOf (dr("DLIC")) Is DBNull, "0", dr("DLIC"))) - (Decimal.Parse(IIf(TypeOf (dr("DLID")) Is DBNull, "0", dr("DLID"))) + Decimal.Parse(dr("TotalPagosCliente")))   'Decimal.Parse(importe) - Decimal.Parse(IIf(TypeOf (dr("DLID")) Is DBNull, "0", dr("DLID")))

            ', SITUACIONLABORALACTUAL
            importe = GetAmountUpdatedDiscount(dr("DLCC"), dr("DLNP"), dr("DLAP"), dr("DLMP"), NUMCUOTASACTUAL, PAGOPARCIAL)
            dr("ImporteBIFActualizada") = importe

            'RESPINOZA 20070914 - Obtenemos datos adicionales del cliente para el reporte 
            GetDatosAdicionales(dr("DLCC"), dr("DLNP"), SITUACIONLABORALACTUAL, DIRECCION, CUIDAD, TELCASA, TELTRABAJO, TELOTRO, DLDNI, ESTADOPAGARE)

            '20121019: AHSP(BANBIF) - Obtener datos requeridos
            GetDatosCuentas(dr("DLCC"), dr("DLNP"), CUENTAAHORRO)
            GetDatosCastigo(dr("DLCC"), dr("DLNP"), CASTIGORCD)

            '20140827: HAMP -- ONTENER DEVOLUCION
            GetDatosDevolucion(dr("DLCC"), dr("DLNP"), dr("DLAP"), dr("DLMP"), DEVOLUCION, MOTDEVOLUCION)

            'ImporteBIFActualizada
            If dr("DLID") > 0 And dr("SaldoDeudorAcreedor") > 0 Then   'Obtenemos informacion de la deuda dentro del aplicativo de intranet - es decir lo que deberia quedar 
                ' despues de procesar en IBS, en caso de tener algun monto parcial cargado
                importe = dr("SaldoDeudorAcreedor")
            End If

            'ImporteBIFActualizada
            If dr("DLID") > 0 And dr("SaldoDeudorAcreedor") > 0 Then   'Obtenemos informacion de la deuda dentro del aplicativo de intranet - es decir lo que deberia quedar 
                ' despues de procesar en IBS, en caso de tener algun monto parcial cargado
                importe = dr("SaldoDeudorAcreedor")
            End If

            'PAGOIBSPROCESOCOBRANZA
            If dr("DLID") > 0 Then
                dr("PAGOIBSPROCESOCOBRANZA") = PAGOIBSPROCESOCOBRANZA
            Else
                dr("PAGOIBSPROCESOCOBRANZA") = "0.00"
            End If

            dr("ImporteBIFActualizadaReporte") = importe

            dr("PAGOVENTANILLA") = PAGOVENTANILLA
            dr("PAGOINTERNET") = PAGOINTERNET
            dr("PAGOIBS") = PAGOIBS
            'dr("PAGOIBSPROCESOCOBRANZA") = PAGOIBSPROCESOCOBRANZA
            dr("NUMCUOTASACTUAL") = NUMCUOTASACTUAL
            dr("PAGOPARCIAL") = PAGOPARCIAL

            dr("SITUACIONPAGARE") = SITUACIONPAGARE
            dr("CUENTACARGO") = CUENTACARGO
            dr("SITUACIONLABORALACTUAL") = SITUACIONLABORALACTUAL

            dr("DIRECCION") = DIRECCION
            dr("CUIDAD") = CUIDAD
            dr("TELCASA") = TELCASA
            dr("TELTRABAJO") = TELTRABAJO
            dr("TELOTRO") = TELOTRO

            '20121019: AHSP(BANBIF) - Actualizacion de estos estados
            dr("DLDNI") = DLDNI
            dr("ESTADOPAGARE") = ESTADOPAGARE
            dr("CUENTAAHORRO") = CUENTAAHORRO
            dr("CASTIGORCD") = CASTIGORCD

            '20140827 HAMP
            dr("DEVOLUCION") = DEVOLUCION
            '20160211 CALCULO DIFERENCIA
            Dim decDiferencia As Decimal = Convert.ToDecimal(dr("DLID")) - (Convert.ToDecimal(dr("PAGOIBSPROCESOCOBRANZA").ToString()) + Convert.ToDecimal(dr("DEVOLUCION").ToString()))
            dr("DIFERENCIA") = decDiferencia.ToString()
            Dim TIPODEV = ""
            'EA2017-11386
            If String.IsNullOrEmpty(CUENTAAHORRO) Then
                Dim FLAGPEND As Boolean = Seguimiento.getCuentaConDevolucionPendiente(dr("DLCC"), dr("DLNP"), dr("DLAP"), dr("DLMP"))
                If (FLAGPEND) Then
                    TIPODEV = "A PENDIENTE"
                Else
                    TIPODEV = ""
                End If
            Else
                TIPODEV = "A CUENTA"
            End If

            dr("TIPODEVOLUCION") = TIPODEV 'EA2017-11386

            PAGOVENTANILLA = "0.00"
            PAGOINTERNET = "0.00"
            PAGOIBS = "0.00"
            PAGOIBSPROCESOCOBRANZA = "0.00"
            NUMCUOTASACTUAL = "0"
            SITUACIONPAGARE = ""
            CUENTACARGO = ""
            PAGOPARCIAL = "0.00"
            SITUACIONLABORALACTUAL = ""

            DIRECCION = ""
            CUIDAD = ""
            TELCASA = ""
            TELTRABAJO = ""
            TELOTRO = ""

            DLDNI = ""
            ESTADOPAGARE = ""
            CUENTAAHORRO = ""
            CASTIGORCD = ""
            DEVOLUCION = "0.00"
        Next
    End Sub

    'Obtenemos la informacion del numero de pagare desde el DataSet para poder 
    'mostrarlo en pantalla
    Protected Function GetAmountUpdatedDiscount(DLCC As String, numeroPagare As String,
                mAnio As String,
                mMes As String, ByRef NUMCUOTASACTUAL As String, ByRef PAGOPARCIAL As String) As String ', ByRef SITUACIONLABORALACTUAL As String

        Dim dr As DataRow()
        Dim returnValue As String = "0.00"

        If dsIBSCuotas Is Nothing Then
            dsIBSCuotas = Seguimiento.getMontoDescuento(DLCC, mAnio, mMes)
        End If

        dr = dsIBSCuotas.Tables(0).Select("DLNP = " + numeroPagare)

        If dr.GetLength(0) > 0 Then
            returnValue = Format(dr(0).Item("DLEIC"), "#0.00")
            NUMCUOTASACTUAL = dr(0).Item("NUMCOUTASACTUAL")
            PAGOPARCIAL = Format(dr(0).Item("PAGOPARCIAL"), "#0.00")
            'SITUACIONLABORALACTUAL = dr(0).Item("SITUACIONLABORALACTUAL")
        End If

        Return returnValue
    End Function

    Protected Function GetTotalPagosCliente(codigoCliente As String, fechaInicial As String,
    fechaFinal As String, numeroPagare As String, lote As String, ByRef PAGOVENTANILLA As String,
    ByRef PAGOINTERNET As String, ByRef PAGOIBS As String, ByRef PAGOIBSPROCESOCOBRANZA As String, ByRef SITUACIONPAGARE As String, ByRef CUENTACARGO As String) As String

        Dim dr As DataRow()
        Dim returnValue As String = "0.00"
        PAGOVENTANILLA = "0.00"
        PAGOINTERNET = "0.00"
        PAGOIBS = "0.00"
        PAGOIBSPROCESOCOBRANZA = "0.00"
        SITUACIONPAGARE = ""
        CUENTACARGO = ""

        'Obtenemos la informacion de los pagos desde IBS
        If dsIBS Is Nothing Then
            dsIBS = PostConciliacion.getResumenPagosIBS(codigoCliente, fechaInicial, fechaFinal, lote.Trim)
        End If

        If dsIBSTD Is Nothing Then
            dsIBSTD = PostConciliacion.getResumenProcesoIBS(codigoCliente, fechaInicial, fechaFinal)
        End If

        'Otros Pagos
        dr = dsIBS.Tables(0).Select("DLCNP = " + numeroPagare)
        If dr.GetLength(0) > 0 Then
            PAGOVENTANILLA = Format(dr(0).Item("PAGOVENTANILLA"), "#0.00")
            PAGOINTERNET = Format(dr(0).Item("PAGOINTERNET"), "#0.00")
            PAGOIBS = Format(dr(0).Item("PAGOIBS"), "#0.00")
            'PAGOIBSPROCESOCOBRANZA = Format(dr(0).Item("PAGOIBSPROCESOCOBRANZA"), "#0.00")

            returnValue = Format(dr(0).Item("IMPORTE"), "#0.00")
        End If
        'Cargo Intranet 
        dr = dsIBSTD.Tables(0).Select("TTRACC = " + numeroPagare)
        If dr.GetLength(0) > 0 Then
            PAGOIBSPROCESOCOBRANZA = Format(dr(0).Item("TTRAMTS"), "#0.00")
            SITUACIONPAGARE = dr(0).Item("DEASTS")
            CUENTACARGO = dr(0).Item("TTRACR")
        End If

        Return returnValue
    End Function

    'RESPINOZA 20070914 - Obtener informacion de datos adicionales del cliente desde IBS
    Protected Sub GetDatosAdicionales(codigoCliente As String, numeroPagare As String, ByRef SITUACIONLABORAL As String, ByRef DIRECCION As String, ByRef CUIDAD As String, ByRef TELCASA As String, ByRef TELTRABAJO As String, ByRef TELOTRO As String, ByRef DLDNI As String, ByRef ESTADOPAGARE As String)
        Dim dr As DataRow()

        SITUACIONLABORAL = ""
        DIRECCION = ""
        CUIDAD = ""
        TELCASA = ""
        TELTRABAJO = ""
        TELOTRO = ""

        DLDNI = ""
        ESTADOPAGARE = ""

        If dsIBSDatosAdicionales Is Nothing Then
            dsIBSDatosAdicionales = Seguimiento.getDatosAdicionalesClienteConvenio(codigoCliente)
        End If

        dr = dsIBSDatosAdicionales.Tables(0).Select("PAGARE = " + numeroPagare)
        If dr.GetLength(0) > 0 Then
            SITUACIONLABORAL = dr(0).Item("SITUACIONLABORAL")
            DIRECCION = dr(0).Item("DIRECCION")
            CUIDAD = dr(0).Item("CUIDAD")
            TELCASA = dr(0).Item("TELCASA")
            TELTRABAJO = dr(0).Item("TELTRABAJO")
            TELOTRO = dr(0).Item("TELOTRO")

            DLDNI = dr(0).Item("DLDNI")

            If UCase(dr(0).Item("DEASTS")) = "C" Then
                ESTADOPAGARE = "CERRADO"
            Else
                Select Case dr(0).Item("DEADLC")
                    Case 1 : ESTADOPAGARE = "VIGENTE"
                    Case 2 : ESTADOPAGARE = "VENCIDO"
                    Case 3 : ESTADOPAGARE = "ATRASADO"
                    Case 4 : ESTADOPAGARE = "ABOGADO"
                    Case 5 : ESTADOPAGARE = "C/JUDICIAL"
                    Case Else : ESTADOPAGARE = ""
                End Select
            End If

        End If

    End Sub

    'ASANCHEZP 20121005 - Obtener informacion de las Cuentas del Cliente
    Protected Sub GetDatosCuentas(codigoCliente As String, numeroPagare As String, ByRef CUENTAAHORRO As String)
        Dim ldr As DataRow()
        Dim lCuentas() As String
        Dim lTipoCuenta As String

        CUENTAAHORRO = ""

        If dsIBSDatosCuentas Is Nothing Then
            dsIBSDatosCuentas = Seguimiento.getDatosCuentas(codigoCliente, "'" & ConfigurationManager.AppSettings("archivoPostConciliacionCuentas").Replace(",", "','") & "'")
        End If

        ldr = dsIBSDatosCuentas.Tables(0).Select("PAGARE = " + numeroPagare)

        'Si existe al menos una cuenta
        If ldr.GetLength(0) > 0 Then

            'Si existe solo una cuenta escogemos la unica
            If ldr.GetLength(0) = 1 Then
                CUENTAAHORRO = ldr(0).Item("ACMACC")
            Else
                'Si existe mas de una cuenta tenemos que priorizar

                'Obtenemos la priorizacion de las cuentas a mostrar
                lCuentas = ConfigurationManager.AppSettings("archivoPostConciliacionCuentas").Split(",")

                'Nos barremos los tipos de cuentas segun su prioridad
                For Each lTipoCuenta In lCuentas

                    'Nos barremos todas las cuentas disponibles
                    For Each dr As DataRow In ldr
                        If lTipoCuenta = dr.Item("ACMPRO") Then
                            CUENTAAHORRO = dr.Item("ACMACC")
                            Exit Sub
                        End If
                    Next
                Next

            End If
        End If

    End Sub

    'ASANCHEZP 20121009 - Obtener informacion de los Castigos de los Clientes
    Protected Sub GetDatosCastigo(codigoCliente As String, numeroPagare As String, ByRef CASTIGORCD As String)
        Dim ldr As DataRow()

        CASTIGORCD = ""

        If dsIBSDatosCastigo Is Nothing Then
            dsIBSDatosCastigo = Seguimiento.getDatosCastigo(codigoCliente)
        End If

        ldr = dsIBSDatosCastigo.Tables(0).Select("PAGARE = " + numeroPagare)

        'Si existe al menos una cuenta
        If ldr.GetLength(0) > 0 Then
            CASTIGORCD = "SI"
        End If

    End Sub

    Protected Sub GetDatosDevolucion(codigoCliente As String, numeroPagare As String, mAnio As String,
                mMes As String, ByRef DEVOLUCION As String, ByRef MOTDEVOLUCION As String) 'EA2017-11386 SE AGREGA MOTIVO DEVOLUCION

        Try
            DEVOLUCION = "0.0"

            If dsIBSDatosDevolucion Is Nothing Then
                dsIBSDatosDevolucion = Seguimiento.getDatosDevolucion(codigoCliente, numeroPagare, mAnio, mMes)
            End If

            If dsIBSDatosDevolucion.Tables(0).Rows.Count() > 0 Then
                DEVOLUCION = dsIBSDatosDevolucion.Tables(0).Rows(0).Item("DLIDV")
                MOTDEVOLUCION = dsIBSDatosDevolucion.Tables(0).Rows(0).Item("DLMOT")
            End If
            dsIBSDatosDevolucion = Nothing
        Catch ex As Exception
            DEVOLUCION = "0.00"
            dsIBSDatosDevolucion = Nothing
        End Try


    End Sub

#End Region

    Public Function PintarBotonCalendario(IDContenedor As String, nombrecontrol As String) As String
        'Dim respuesta As String = ""
        'respuesta = "<a href=""""><img title=""Calendario"" onclick=""return showCalendar('" & IDContenedor & IIf(IsNothing(IDContenedor), "", "_") & nombrecontrol.ToString.Trim() & "', 'dd/mm/y');"" height=""18"" alt=""Calendario"" src=""../images/calendario.gif"" width=""18"" align=""absMiddle"" border=""0""></A>"
        'Return respuesta

        Return "<a href=""""><img title=""Calendario"" onclick=""return showCalendar('" & nombrecontrol.ToString.Trim() & "', 'dd/mm/y');"" height=""18"" alt=""Calendario"" src=""../images/calendario.gif"" width=""18"" align=""absMiddle"" border=""0""></A>"
    End Function

    'Obtenemos la informacion del numero de pagare desde el DataSet para poder 
    'mostrarlo en pantalla
    Protected Function GetAmount(DLCC As String, numeroPagare As String,
            nombreTrabajador As String, moneda As String, cuotaMes As String,
            importeDescontado As String, deudaPeriodo As String, fechaProceso As Object, TotalPagosCliente As String) As String
        Dim returnValue As String

        If TotalPagosCliente <> "0.00" Then
            returnValue = "<a href =""javascript:ShowDetalle('" + DLCC +
                            "', '" + numeroPagare + "', '" +
                            GetSQLDate(txtFechaDesde.Text) + "', '" +
                            GetSQLDate(txtFechaHasta.Text) + "', '" +
                            nombreTrabajador.Replace("'", "") + "', '" +
                            TotalPagosCliente + "', '" + moneda + "','" +
                            cuotaMes + "','" + importeDescontado +
                            "', '" + deudaPeriodo +
                            "');"">" + TotalPagosCliente + "</a>"
        Else
            returnValue = "N.E."
        End If
        Return returnValue
    End Function

    Private Sub ddlKind_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlKind.SelectedIndexChanged
        dgProcesoResult.CurrentPageIndex = 0
        Me.MostrarInformacionSeguimiento()
        lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
    End Sub

    Private Sub ddlUGE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUGE.SelectedIndexChanged
        dgProcesoResult.CurrentPageIndex = 0
        MostrarInformacionSeguimiento()
        lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
    End Sub

    Private Sub ddlSituacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSituacion.SelectedIndexChanged
        dgProcesoResult.CurrentPageIndex = 0
        MostrarInformacionSeguimiento()
        lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
    End Sub

    Private Sub dgProcesoResult_ItemDataBound(sender As Object, e As Web.UI.WebControls.DataGridItemEventArgs) Handles dgProcesoResult.ItemDataBound
        If e.Item.ItemType = ListItemType.Footer Then
            e.Item.Cells(2).CssClass = "SubHead"
            e.Item.Cells(2).Text = "Total General"
            e.Item.Cells(4).Text = Format(ViewState.Item("dblImporteInformado"), "#,###0.00")
            e.Item.Cells(5).Text = Format(ViewState.Item("dblImporteInstitucion"), "#,###0.00")
            e.Item.Cells(11).Text = Format(ViewState.Item("dblSaldoDeudorAcreedor"), "#,###0.00")
            e.Item.Cells(13).Text = Format(ViewState.Item("dblImporteBIFActualizado"), "#,###0.00")
            e.Item.Cells(12).Text = Format(ViewState.Item("dblPAGOIBSPROCESOCOBRANZA"), "#,###0.00")
        End If
    End Sub

    Private Sub dgProcesoResult_PageIndexChanged(source As Object, e As Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResult.PageIndexChanged

        dgProcesoResult.CurrentPageIndex = IIf(dgProcesoResult.PageCount < e.NewPageIndex, 0, e.NewPageIndex)
        MostrarInformacionSeguimiento()

    End Sub

    Private Sub MostrarInformacionSeguimiento()

        Dim dv As DataView
        Dim condicionFiltro As String = Me.GetCondicionFiltro()

        If Not Session("dsDataPostConciliacion") Is Nothing Then

            dsData = CType(Session("dsDataPostConciliacion"), DataSet)

            dv = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))     'dsData.Tables(0).Select("SaldoDeudorAcreedor>0")
            'dv = (New DataView(dsData.Tables(0), Me.getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))     'dsData.Tables(0).Select("SaldoDeudorAcreedor>0")
            GetCalculoResumenCarga(dv)

            dgProcesoResult.DataSource = dv
            dgProcesoResult.DataBind()

            lblTotalReg.Text = dv.Count

        Else

            dsData = oproc.GetRegistrosResultadoProcesoDescuentos(hdParam1.Value.Trim(), Me.NumeroPagare, Me.NombreCliente)
            ActualizaInformacionDataSet(dsData)
            dv = (New DataView(dsData.Tables("Descuentos"), Me.GetCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))     'dsData.Tables(0).Select("SaldoDeudorAcreedor>0")
            GetCalculoResumenCarga(dv)

            dgProcesoResult.DataSource = dv
            dgProcesoResult.DataBind()
            dgProcesoResult.Visible = True

            lblTotalReg.Text = dv.Count
        End If
    End Sub


    'Obtiene el calculo de los totales de la carga realizada 
    Sub GetCalculoResumenCarga(ByRef dv As DataView)
        Dim dr As DataRowView
        dblImporteInformado = 0
        dblImporteInstitucion = 0
        dblImporteBIFActualizado = 0
        dblSaldoDeudorAcreedor = 0
        dblPAGOIBSPROCESOCOBRANZA = 0
        For Each dr In dv    '--dv.Table.Rows()
            dblImporteInformado += dr("DLIC")
            dblImporteInstitucion += dr("DLID")
            dblImporteBIFActualizado += dr("ImporteBIFActualizada")
            dblSaldoDeudorAcreedor += dr("SaldoDeudorAcreedor")
            dblPAGOIBSPROCESOCOBRANZA += dr("PAGOIBSPROCESOCOBRANZA")
        Next
        ViewState.Add("dblImporteInformado", dblImporteInformado)
        ViewState.Add("dblImporteInstitucion", dblImporteInstitucion)
        ViewState.Add("dblImporteBIFActualizado", dblImporteBIFActualizado)
        ViewState.Add("dblSaldoDeudorAcreedor", dblSaldoDeudorAcreedor)
        ViewState.Add("dblPAGOIBSPROCESOCOBRANZA", dblPAGOIBSPROCESOCOBRANZA)
    End Sub

#Region "Parametros del formulario"

    'Obtener la información del numero de pagare
    Private ReadOnly Property NumeroPagare() As String
        Get
            Dim result As String = "0"
            If ddlBuscarpor.SelectedItem.Value = "Codigo" And chkInclude.Checked Then
                result = IIf(IsNumeric(txtCampo.Text.Trim), txtCampo.Text, "-1")
            End If
            Return result
        End Get
    End Property

    'Obtener la información del nombre 
    Private ReadOnly Property NombreCliente() As String
        Get
            Dim result As String = ""
            If ddlBuscarpor.SelectedItem.Value = "Nombre" And chkInclude.Checked Then
                result = txtCampo.Text
            End If
            Return result
        End Get
    End Property

#End Region


End Class