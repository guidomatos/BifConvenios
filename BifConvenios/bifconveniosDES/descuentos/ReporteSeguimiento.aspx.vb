Imports System.IO

Namespace BIFConvenios

    Partial Class ReporteSeguimiento
        Inherits Page

        Protected dsIBSCuotas As DataSet  'Información actualizada de las cuotas
        Protected dsIBSBloqueoPagare As DataSet  'Información actualizada de las cuotas
        Protected dsIBS As DataSet
        Protected dsIBSDatosAdicionales As DataSet  'Informacion de datos adicionales de los clientes

        Protected dsData As DataSet

        Protected dblImporteInformado As Decimal = 0
        Protected dblImporteInstitucion As Decimal = 0
        Protected dblImporteBIFActualizado As Decimal = 0
        Protected dblSaldoDeudorAcreedor As Decimal = 0

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        'Private Sub ResultadoBusquedaEmpresa(resultadoBusqueda As String)
        '    Dim resultadoArray() As String = Split(resultadoBusqueda, "|")

        '    hdParam1.Value = resultadoArray(0) 'Proceso
        '    hdParam2.Value = resultadoArray(1) 'Año
        '    hdParam3.Value = resultadoArray(2) 'Mes
        '    hdParam4.Value = resultadoArray(6) 'Tipo Documento
        '    hdParam5.Value = resultadoArray(7) 'Numero Documento
        '    txtPeriodo.Text = resultadoArray(5) 'Numero Documento
        '    hdCodigoEmpresa.Value = resultadoArray(3)
        '    txtNombreEmpresa.Text = resultadoArray(4)

        '    dvData.Visible = False

        'End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            'AddHandler ucBuscarParanetroEmpresa.UpdateEvent, AddressOf ResultadoBusquedaEmpresa

            If Not Page.IsPostBack Then
                Utils.AddSwap(lnkBuscar, "Image1", ResolveUrl("/images/buscar_on.jpg"))
                If Request.Params("keep") IsNot Nothing Then

                End If
            End If
            ddlBuscarpor.Enabled = chkInclude.Checked
            txtCampo.Enabled = chkInclude.Checked
        End Sub


        'RESPINOZA 20070529 - Obtener la informacion filtrada en un reporte para poder ser revisado de forma diferente por el usuario
        Private Sub lnkGenerarReporte_Click(sender As Object, e As EventArgs) Handles lnkGenerarReporte.Click
            Dim dv As New DataView()
            Dim fileName As String = Utils.getWebServerDateId() + ".xls"

            MostrarInformacionSeguimiento()
            dv = New DataView(dsData.Tables("Descuentos"), getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)

            'LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dv, AppSettings("GenFolder"), fileName, "DLMO,DLNP,DLCM,DLNE,DLAP,DLMP,DLIC,DLID,DeudaPeriodo,TotalPagosCliente,SaldoDeudorAcreedor,ImporteBIFActualizada,ImporteBIFActualizadaReporte,UGE,NUMCUOTAS", "MONEDA,PAGARE,MODULAR,NOMBRE,ANIO,MES,CUOTA,DESCUENTO,DEUDA,Total Pagos Cliente (IBS),Saldo Deudor(+)/Acreedor(-),Deuda Actual Proyectada (IBS),DEUDA NOTA,UGE,NUMCUOTAS")
            LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dv, ConfigurationManager.AppSettings("GenFolder"), fileName, ConfigurationManager.AppSettings("archivoSeguimientoCampos"), ConfigurationManager.AppSettings("archivoSeguimientoTitulos"))

            Dim ms As System.IO.MemoryStream
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
            Dim dv As DataView
            dsData = Proceso.GetRegistrosDeudasResultadoProcesoDescuentos(hdParam1.Value.Trim(), Me.NumeroPagare, Me.NombreCliente)
            actualizaInformacionDataSet(dsData)
            Session.Add("dsDataSeguimiento", dsData)

            ddlUGE.DataSource = Proceso.getUGES(hdParam1.Value)
            ddlUGE.DataBind()

            If ddlUGE.Items.Count > 0 Then
                ddlUGE.Visible = True
                ddlUGE.Items.Insert(0, New ListItem("Todas las UGE", ""))
            Else
                ddlUGE.Visible = False
            End If


            dv = (New DataView(dsData.Tables("Descuentos"), getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))  'dsData
            getCalculoResumenCarga(dv)
            dgProcesoResult.CurrentPageIndex = 0
            dgProcesoResult.DataSource = dv
            dgProcesoResult.DataBind()
            dgProcesoResult.Visible = True

            lblTotalReg.Text = dsData.Tables(0).Rows.Count
            dvData.Visible = True
            tdMostrar.Visible = True
            tdddl.Visible = True
            ddlSituacion.Visible = True
        End Sub

#Region "Actualizacion de informacion del DataSet desde IBS"
        'Actualizamos los datos del Dataset desde IBS
        Protected Function actualizaInformacionDataSet(ByRef dsData As DataSet)
            Dim dr As DataRow
            Dim importe As String
            Dim PAGOVENTANILLA As String = "0.00", PAGOINTERNET As String = "0.00", PAGOIBS As String = "0.00", PAGOIBSPROCESOCOBRANZA As String = "0.00", NUMCUOTASACTUAL As String = "0", SITUACIONLABORALACTUAL As String = ""
            Dim DIRECCION As String = "", CUIDAD As String = "", TELCASA As String = "", TELTRABAJO As String = "", TELOTRO As String = ""
            Dim Bloquear As String = "0"

            For Each dr In dsData.Tables(0).Rows()

                dr("TotalPagosCliente") = getTotalPagosCliente(dr("DLCC"), Format(dr("DLFP"), "yyyyMMdd"), Format(DateAdd(DateInterval.Month, 1, dr("DLFP")), "yyyyMMdd"), dr("DLNP"), dr("LOTE"), PAGOVENTANILLA, PAGOINTERNET, PAGOIBS, PAGOIBSPROCESOCOBRANZA)
                'RESPINOZA 20061023 - Los pagos son ahora calculados de forma diferente
                '                       Diferencia del Mes = Monto de Envio - (Monto Recibido + Total Pagos)
                dr("SaldoDeudorAcreedor") = Decimal.Parse(IIf(TypeOf (dr("DLIC")) Is DBNull, "0", dr("DLIC"))) - (Decimal.Parse(IIf(TypeOf (dr("DLID")) Is DBNull, "0", dr("DLID"))) + Decimal.Parse(dr("TotalPagosCliente")))   'Decimal.Parse(importe) - Decimal.Parse(IIf(TypeOf (dr("DLID")) Is DBNull, "0", dr("DLID")))


                importe = getAmountUpdatedDiscount(dr("DLCC"), dr("DLNP"), dr("DLAP"), dr("DLMP"), NUMCUOTASACTUAL)
                dr("ImporteBIFActualizada") = importe

                'RESPINOZA 20070914 - Obtenemos datos adicionales del cliente para el reporte 
                getDatosAdicionales(dr("DLCC"), dr("DLNP"), SITUACIONLABORALACTUAL, DIRECCION, CUIDAD, TELCASA, TELTRABAJO, TELOTRO)

                Bloquear = Me.getInformacionBloqueo(dr("DLCC"), dr("DLNP"), dr("DLAP"), dr("DLMP"))
                'Bloquear = 0

                'ImporteBIFActualizada
                If dr("DLID") > 0 And dr("SaldoDeudorAcreedor") > 0 Then   'Obtenemos informacion de la deuda dentro del aplicativo de intranet - es decir lo que deberia quedar 
                    ' despues de procesar en IBS, en caso de tener algun monto parcial cargado
                    importe = dr("SaldoDeudorAcreedor")
                End If

                dr("ImporteBIFActualizadaReporte") = importe

                dr("PAGOVENTANILLA") = PAGOVENTANILLA
                dr("PAGOINTERNET") = PAGOINTERNET
                dr("PAGOIBS") = PAGOIBS
                dr("PAGOIBSPROCESOCOBRANZA") = PAGOIBSPROCESOCOBRANZA
                dr("NUMCUOTASACTUAL") = NUMCUOTASACTUAL
                dr("SITUACIONLABORALACTUAL") = SITUACIONLABORALACTUAL

                dr("DIRECCION") = DIRECCION
                dr("CUIDAD") = CUIDAD
                dr("TELCASA") = TELCASA
                dr("TELTRABAJO") = TELTRABAJO
                dr("TELOTRO") = TELOTRO
                dr("Bloquear") = Bloquear

                PAGOVENTANILLA = "0.00"
                PAGOINTERNET = "0.00"
                PAGOIBS = "0.00"
                PAGOIBSPROCESOCOBRANZA = "0.00"
                NUMCUOTASACTUAL = "0"

                SITUACIONLABORALACTUAL = ""
                DIRECCION = ""
                CUIDAD = ""
                TELCASA = ""
                TELTRABAJO = ""
                TELOTRO = ""
            Next
        End Function

        'Obtiene el calculo de los totales de la carga realizada 
        Sub getCalculoResumenCarga(ByRef dv As DataView)
            Dim dr As DataRowView
            dblImporteInformado = 0
            dblImporteInstitucion = 0
            dblImporteBIFActualizado = 0
            dblSaldoDeudorAcreedor = 0


            For Each dr In dv    '--dv.Table.Rows()
                dblImporteInformado += dr("DLIC")
                dblImporteInstitucion += dr("DLID")
                dblImporteBIFActualizado += dr("ImporteBIFActualizada")
                dblSaldoDeudorAcreedor += dr("SaldoDeudorAcreedor")
            Next
            ViewState.Add("dblImporteInformado", dblImporteInformado)
            ViewState.Add("dblImporteInstitucion", dblImporteInstitucion)
            ViewState.Add("dblImporteBIFActualizado", dblImporteBIFActualizado)
            ViewState.Add("dblSaldoDeudorAcreedor", dblSaldoDeudorAcreedor)
        End Sub



#Region "Actualizacion de la informacion del Dataset desde IBS"

        'Obtenemos la informacion del numero de pagare desde el DataSet para 
        'poder filtrar los clientes que necesitan bloqueo
        Protected Function getInformacionBloqueo(DLCC As String, numeroPagare As String,
                    mAnio As String,
                    mMes As String) As String
            Dim dr As DataRow()
            Dim returnValue As String = "0"

            If dsIBSBloqueoPagare Is Nothing Then
                dsIBSBloqueoPagare = Seguimiento.getInformacionBloqueos(DLCC, mAnio, mMes)
            End If

            dr = dsIBSBloqueoPagare.Tables(0).Select("DLACC = " + numeroPagare)

            If dr.GetLength(0) > 0 Then
                returnValue = "1"
            End If

            Return returnValue
        End Function


        'Obtenemos la informacion del numero de pagare desde el DataSet para poder 
        'mostrar en pantalla el monto a descuento y las cuotas actualizadas
        Protected Function getAmountUpdatedDiscount(DLCC As String, numeroPagare As String,
                    mAnio As String,
                    mMes As String, ByRef NUMCUOTASACTUAL As String) As String
            Dim dr As DataRow()
            Dim returnValue As String = "0.00"

            If dsIBSCuotas Is Nothing Then
                dsIBSCuotas = Seguimiento.getMontoDescuento(DLCC, mAnio, mMes)
            End If

            dr = dsIBSCuotas.Tables(0).Select("DLNP = " + numeroPagare)

            If dr.GetLength(0) > 0 Then
                returnValue = Format(dr(0).Item("DLEIC"), "#0.00")
                NUMCUOTASACTUAL = dr(0).Item("NUMCOUTASACTUAL")


            End If

            Return returnValue
        End Function

        'RESPINOZA 20070914 - Obtener informacion de datos adicionales del cliente desde IBS
        Protected Function getDatosAdicionales(codigoCliente As String, numeroPagare As String, ByRef SITUACIONLABORAL As String, ByRef DIRECCION As String, ByRef CUIDAD As String, ByRef TELCASA As String, ByRef TELTRABAJO As String, ByRef TELOTRO As String)
            Dim dr As DataRow()

            SITUACIONLABORAL = ""
            DIRECCION = ""
            CUIDAD = ""
            TELCASA = ""
            TELTRABAJO = ""
            TELOTRO = ""

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
            End If

        End Function

        Protected Function getTotalPagosCliente(codigoCliente As String, fechaInicial As String,
        fechaFinal As String, numeroPagare As String, lote As String, ByRef PAGOVENTANILLA As String,
        ByRef PAGOINTERNET As String, ByRef PAGOIBS As String, ByRef PAGOIBSPROCESOCOBRANZA As String)
            Dim dr As DataRow()
            Dim returnValue As String = "0.00"
            If dsIBS Is Nothing Then
                dsIBS = PostConciliacion.getResumenPagosIBS(codigoCliente, fechaInicial, fechaFinal, lote.Trim)
            End If

            dr = dsIBS.Tables(0).Select("DLCNP = " + numeroPagare)

            If dr.GetLength(0) > 0 Then
                PAGOVENTANILLA = Format(dr(0).Item("PAGOVENTANILLA"), "#0.00")
                PAGOINTERNET = Format(dr(0).Item("PAGOINTERNET"), "#0.00")
                PAGOIBS = Format(dr(0).Item("PAGOIBS"), "#0.00")
                PAGOIBSPROCESOCOBRANZA = Format(dr(0).Item("PAGOIBSPROCESOCOBRANZA"), "#0.00")

                returnValue = Format(dr(0).Item("IMPORTE"), "#0.00")
            End If
            Return returnValue
        End Function

#End Region




#End Region

        'Obtenemos la informacion del numero de pagare desde el DataSet para poder 
        'mostrarlo en pantalla
        Protected Function getAmount(DLCC As String, numeroPagare As String,
                nombreTrabajador As String, moneda As String, cuotaMes As String,
                importeDescontado As String, deudaPeriodo As String, fechaProceso As Object, TotalPagosCliente As String) As String
            Dim returnValue As String

            If TotalPagosCliente <> "0.00" Then
                returnValue = "<a href =""javascript:ShowDetalle('" + DLCC +
                                "', '" + numeroPagare + "', '" +
                                Format(fechaProceso, "yyyyMMdd") + "', '" +
                                Format(DateAdd(DateInterval.Month, 1, fechaProceso), "yyyyMMdd") + "', '" +
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
            MostrarInformacionSeguimiento()
            lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
        End Sub

        Private Sub ddlUGE_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlUGE.SelectedIndexChanged
            dgProcesoResult.CurrentPageIndex = 0
            MostrarInformacionSeguimiento()

            lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
        End Sub

        Private Sub ddlSituacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlSituacion.SelectedIndexChanged
            dgProcesoResult.CurrentPageIndex = 0
            MostrarInformacionSeguimiento()
            lblTotalReg.Text = (New DataView(dsData.Tables("Descuentos"), getCondicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows)).Count
        End Sub


        Private Sub dgProcesoResult_PageIndexChanged(source As Object, e As DataGridPageChangedEventArgs) Handles dgProcesoResult.PageIndexChanged
            dgProcesoResult.CurrentPageIndex = IIf(dgProcesoResult.PageCount < e.NewPageIndex, 0, e.NewPageIndex)
            MostrarInformacionSeguimiento()
        End Sub


        'Establecemos el filtro adicional a la informacion que se esta mostrando 
        Private Function getCondicionFiltro() As String
            Dim condicionFiltro As String = ""
            Dim condicionFiltro2 As String = ddlKind.SelectedItem.Value
            If ddlUGE.SelectedItem IsNot Nothing Then
                condicionFiltro = IIf(ddlUGE.SelectedItem.Value.Trim <> "", " UGE='" + ddlUGE.SelectedItem.Value.Trim + "'", "")
                If condicionFiltro = " UGE='CH'" Then
                    condicionFiltro = " (UGE='AV' or UGE='AW' or UGE='AX')"
                ElseIf condicionFiltro = " UGE='CA'" Then

                    condicionFiltro = " (UGE='AM' or UGE='AN' or UGE='AO' or UGE='AP' or UGE='AQ' or UGE='AR' or UGE='AS' or UGE='AT')"
                ElseIf condicionFiltro = " UGE='CU'" Then
                    condicionFiltro = " (UGE='CT')"
                ElseIf condicionFiltro = " UGE='JA'" Then
                    condicionFiltro = " (UGE='AY')"
                ElseIf condicionFiltro = " UGE='IG'" Then
                    condicionFiltro = " (UGE='AZ')"
                End If

                '' condicionFiltro = condicionFiltro2 + IIf(ddlKind.SelectedItem.Value.Trim() <> "", " AND ", "") + condicionFiltro
                condicionFiltro = condicionFiltro2 + IIf(condicionFiltro.Trim() <> "" And condicionFiltro2.Trim() <> "", " AND ", "") + condicionFiltro
            End If



            ''condicionFiltro = condicionFiltro + IIf(ddlSituacion.SelectedItem.Value.Trim <> "", IIf(condicionFiltro.Trim <> "", " AND ", "") + ddlSituacion.SelectedItem.Value, "")
            ''condicionFiltro = condicionFiltro2 + IIf(ddlSituacion.SelectedItem.Value.Trim <> "", IIf(condicionFiltro.Trim <> "", " AND ", "") + ddlSituacion.SelectedItem.Value, "")
            If condicionFiltro.Trim() <> "" Then
                condicionFiltro = condicionFiltro + IIf(ddlSituacion.SelectedItem.Value.Trim <> "", IIf(condicionFiltro.Trim <> "", " AND ", "") + ddlSituacion.SelectedItem.Value, "")

            Else
                condicionFiltro = IIf(condicionFiltro2.Trim() <> "", (condicionFiltro2 + IIf(ddlSituacion.SelectedItem.Value.Trim <> "", " AND " + ddlSituacion.SelectedItem.Value.Trim, "")), IIf(ddlSituacion.SelectedItem.Value.Trim <> "", ddlSituacion.SelectedItem.Value.Trim, ""))
            End If

            Return condicionFiltro
        End Function

        Private Sub MostrarInformacionSeguimiento()
            Dim dv As DataView
            Dim condicionFiltro As String = getCondicionFiltro()
            ''condicionFiltro = " UGE='CU'"
            If Not Session("dsDataSeguimiento") Is Nothing Then

                dsData = CType(Session("dsDataSeguimiento"), DataSet)
                'If condicionFiltro = " UGE='CH'" Then
                '    condicionFiltro = " UGE='AV' or UGE='AW' or UGE='AX'"
                'ElseIf condicionFiltro = " UGE='CA'" Then

                '    condicionFiltro = " UGE='AM' or UGE='AN' or UGE='AO' or UGE='AP' or UGE='AQ' or UGE='AR' or UGE='AS' or UGE='AT'"
                'ElseIf condicionFiltro = " UGE='CU'" Then
                '    condicionFiltro = " UGE='CT'"
                'ElseIf condicionFiltro = " UGE='JA'" Then
                '    condicionFiltro = " UGE='AY'"
                'ElseIf condicionFiltro = " UGE='IG'" Then
                '    condicionFiltro = " UGE='AZ'"
                'End If

                dv = (New DataView(dsData.Tables("Descuentos"), condicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))      'dsData.Tables(0).Select("SaldoDeudorAcreedor>0")

                getCalculoResumenCarga(dv)
                dgProcesoResult.DataSource = dv
                dgProcesoResult.DataBind()
                dgProcesoResult.Visible = True
            Else
                dsData = Proceso.GetRegistrosDeudasResultadoProcesoDescuentos(hdParam1.Value.Trim(), Me.NumeroPagare, Me.NombreCliente)
                actualizaInformacionDataSet(dsData)

                Session.Add("dsDataSeguimiento", dsData)
                dv = (New DataView(dsData.Tables("Descuentos"), condicionFiltro, "SaldoDeudorAcreedor desc", DataViewRowState.CurrentRows))    '
                getCalculoResumenCarga(dv)

                dgProcesoResult.DataSource = dv
                dgProcesoResult.DataBind()
                dgProcesoResult.Visible = True
            End If
        End Sub



        Private Sub dgProcesoResult_ItemDataBound(sender As Object, e As DataGridItemEventArgs) Handles dgProcesoResult.ItemDataBound
            'Se establecen los totales generales 
            If e.Item.ItemType = ListItemType.Footer Then
                e.Item.Cells(2).CssClass = "SubHead"
                e.Item.Cells(2).Text = "Total General"
                e.Item.Cells(4).Text = Format(ViewState.Item("dblImporteInformado"), "#,###0.00")
                e.Item.Cells(5).Text = Format(ViewState.Item("dblImporteInstitucion"), "#,###0.00")
                e.Item.Cells(11).Text = Format(ViewState.Item("dblSaldoDeudorAcreedor"), "#,###0.00")
                e.Item.Cells(12).Text = Format(ViewState.Item("dblImporteBIFActualizado"), "#,###0.00")
            End If

        End Sub





#Region "Obtener parametros del formulario"
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
End Namespace
