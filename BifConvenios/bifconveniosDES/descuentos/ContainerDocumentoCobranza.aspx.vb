Imports Microsoft.Reporting.WebForms
Imports BIFConvenios

Partial Class descuentos_ContainerDocumentoCobranza
    Inherits Page

    Protected ds As New DataSetDocumentoCobranza()
    Protected dsC As New DataSetCuotasPendientes()

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles MyBase.Load
        If Not Page.IsPostBack Then

            'Put user code to initialize the page here
            Dim documentType As String
            Dim pagares As String
            Dim amounts As String
            Dim anio As String
            Dim mes As String
            Dim proceso As String
            Dim dsSign As DataSet
            'Dim dsProceso As DataSet
            Dim lstrCodigo_Cliente As String

            Dim rdsDataset As New ReportDataSource()
            rdsDataset.Name = "DataSet1"
            Dim rdsCobranza As New ReportDataSource()
            rdsCobranza.Name = "DatosCobranza"

            If Not Request.Params("dt") Is Nothing And Not Request.Params("p") Is Nothing Then
                documentType = Request.Params("dt")
                pagares = Request.Params("p")
                proceso = Request.Params("proceso")
                pagares = (pagares.Substring(0, pagares.IndexOf("end"))).Replace("!", "'")
                amounts = Request.Params("a")
                amounts = (amounts.Substring(0, amounts.IndexOf("end"))).Replace("!", "") + ","
                mes = Request.Params("mes")
                anio = Request.Params("anio")

                If documentType.Trim = "N" Then

                    ds = CType(Seguimiento.GetDocumentoPagosPendientesMontosPendientes(pagares, amounts, anio, mes), DataSetDocumentoCobranza)

                    'RESPINOZA 20070523 - Obtenemos la informacion de los montos pendientes de pago debido al cobro parcial de cuotas 
                    'para establecer ese monto en el documento de cobranza en vez del mostrado en IBS
                    'dsProceso = Seguimiento.GetDeudaAntesProcesoEnvio(proceso)

                    'RESPINOZA 20061202 - Es necesario establecer la informacion de las firmas y de los funcionarios asociados a los reportes 
                    ' Se crea tabla con informacion de firma
                    'ds.Tables (0).    
                    dsSign = DocumentoCobranza.getInformacionFirmantes(Context.User.Identity.Name, proceso)
                    Dim dr As DataRow
                    For Each dr In ds.Tables("DatosCobranza").Rows
                        If dsSign.Tables(0).Rows.Count = 2 Then
                            dr("SIGN1NAME") = dsSign.Tables(0).Rows(0).Item("EjecutivoNombre")      ' ""
                            dr("SIGN2NAME") = dsSign.Tables(0).Rows(1).Item("EjecutivoNombre")
                            dr("SIGN1POSITION") = dsSign.Tables(0).Rows(0).Item("EjecutivoCargo")
                            dr("SIGN2POSITION") = dsSign.Tables(0).Rows(1).Item("EjecutivoCargo")

                            dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(0).Item("EjecutivoImagePath"))  ' 
                            dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(1).Item("EjecutivoImagePath"))  ' 

                        Else
                            dr("SIGN1NAME") = "" ' ""
                            dr("SIGN2NAME") = ""  ' ""
                            dr("SIGN1POSITION") = ""  ' ""
                            dr("SIGN2POSITION") = ""  ' ""
                            dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID0.bmp")   ' 
                            dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID1.bmp")  ' 
                        End If
                    Next
                    Session.Add("ds", ds)
                Else
                    'ds = CType(Seguimiento.GetDocumentoPagosPendientes(pagares, mes, anio), DataSetDocumentoCobranza)
                    dsC = Seguimiento.GetDetailPagaresJoin(pagares, mes, anio)

                    dsSign = DocumentoCobranza.getInformacionFirmantes(Context.User.Identity.Name, proceso)
                    Dim dr As DataRow
                    For Each dr In dsC.Tables("DatosDescuentoHeader").Rows
                        If dsSign.Tables(0).Rows.Count = 2 Then
                            dr("SIGN1NAME") = dsSign.Tables(0).Rows(0).Item("EjecutivoNombre")      ' ""
                            dr("SIGN2NAME") = dsSign.Tables(0).Rows(1).Item("EjecutivoNombre")
                            dr("SIGN1POSITION") = dsSign.Tables(0).Rows(0).Item("EjecutivoCargo")
                            dr("SIGN2POSITION") = dsSign.Tables(0).Rows(1).Item("EjecutivoCargo")

                            dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(0).Item("EjecutivoImagePath"))  ' 
                            dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(1).Item("EjecutivoImagePath"))  ' 

                        Else
                            dr("SIGN1NAME") = "" ' ""
                            dr("SIGN2NAME") = ""  ' ""
                            dr("SIGN1POSITION") = ""  ' ""
                            dr("SIGN2POSITION") = ""  ' ""
                            dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID0.bmp")   ' 
                            dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID1.bmp")  ' 
                        End If
                    Next

                    Session.Add("ds", dsC)
                End If

                DocumentoCobranza.procesaLogDocumentosPagares(proceso, pagares, Format(Now, "yyyyMMdd"), documentType.Trim, Context.User.Identity.Name, amounts)


                Session.Add("documentType", documentType)
                Session.Add("pagares", pagares)

                'TODO: AHSP 20080424 - Aqui obtenemos el Cliente del Proceso
                lstrCodigo_Cliente = DocumentoCobranza.getClienteProceso(proceso)
                Session.Add("clienteProceso", lstrCodigo_Cliente)
            Else
                If TypeOf (Session("ds")) Is DataSetDocumentoCobranza Then
                    ds = Session("ds")
                Else
                    dsC = Session("ds")
                End If

                documentType = Session("documentType")
                pagares = Session("pagares")

                'TODO: AHSP 20080424 - Aqui obtenemos el Cliente del Proceso
                lstrCodigo_Cliente = Session("clienteProceso")
            End If

            Select Case documentType.Trim
                Case "CN"
                    rdsCobranza.Value = ds.Tables(0)
                    ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaNotarial.rdlc"
                    ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                Case "C"
                    'TODO: AHSP 20080425-Clientes con Reporte especial por Lluvias
                    If lstrCodigo_Cliente = "190828057" Then    'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                        rdsDataset.Value = dsC.Tables(0)
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLightLluvia2.rdlc"
                        ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                    ElseIf lstrCodigo_Cliente = "190828058" Then 'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                        rdsDataset.Value = dsC.Tables(0)
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLightLluvia.rdlc"
                        ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                    Else
                        rdsDataset.Value = dsC.Tables(0)
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLight.rdlc"
                        ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                    End If
                Case "N"
                    'TODO: AHSP 20080424-Clientes con Reporte especial por Lluvias
                    If lstrCodigo_Cliente = "190828057" Then        'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                        rdsCobranza.Value = ds.Tables(0)
                        ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                    ElseIf lstrCodigo_Cliente = "190828058" Then    'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                        rdsCobranza.Value = ds.Tables(0)
                        ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                    Else
                        'Reporte Normal
                        rdsCobranza.Value = ds.Tables(0)
                        ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                    End If

                    If Request.Params("export") Is Nothing Then
                        'TODO: AHSP 20080424-Clientes con Reporte especial por Lluvias
                        If lstrCodigo_Cliente = "190828057" Then        'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                            ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranzaLluvia2.rdlc"
                        ElseIf lstrCodigo_Cliente = "190828058" Then    'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                            ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranzaLluvia.rdlc"
                        Else
                            'Reporte Normal
                            ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranza.rdlc"
                        End If
                    Else
                        '------------------------
                        Dim name As String = getWebServerDateId() & ".pdf"
                        Dim filename As String = ConfigurationManager.AppSettings("GenFolder") & name  'Server.MapPath(".") & "\export\" & name
                        RemoveFiles(ConfigurationManager.AppSettings("GenFolder"), New TimeSpan(0, 6, 0, 0))

                        'exportar a archivo fisico
                        Dim byteFileRdlc = BIFUtils.WS.Utils.RDLC_ExportarPDF(ReportViewer1)

                        With HttpContext.Current.Response
                            .ClearContent()
                            .ClearHeaders()
                            .ContentType = "application/pdf"
                            .AddHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".PDF")
                            .BinaryWrite(BIFUtils.WS.Utils.RDLC_ExportarPDF(ReportViewer1))
                            '.BinaryWrite(ms.ToArray)

                            .End()
                        End With

                        '--------------------------
                    End If

                Case Else
                    rdsCobranza.Value = ds.Tables(0)
                    ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
            End Select

        End If
    End Sub


End Class