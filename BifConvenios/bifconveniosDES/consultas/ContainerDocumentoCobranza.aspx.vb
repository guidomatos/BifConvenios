'Imports CrystalDecisions.Web
'Imports CrystalDecisions.Shared
Imports System.IO
Imports Microsoft.Reporting.WebForms

Namespace BIFConvenios

    Partial Class consultas_ContainerDocumentoCobranza
        Inherits System.Web.UI.Page

        ''Protected WithEvents CrystalReportViewer1 As CrystalDecisions.Web.CrystalReportViewer
        Protected ds As New DataSetDocumentoCobranza()
        Protected dsC As New DataSetCuotasPendientes()

        'Protected oRepCartaCobranza As New repCartaCobranza()
        'Protected orepCartaCobranzaLight As New repCartaCobranzaLight()
        'Protected orepCartaCobranzaLightLluvia As New repCartaCobranzaLightLluvia()     'TODO: AHSP 200804025- Adicionado x Lluvias
        'Protected orepCartaCobranzaLightLluvia2 As New repCartaCobranzaLightLluvia2()   'TODO: AHSP 200804029- Adicionado x Lluvias

        'Protected oRepCartaNotarial As New repCartaNotarial()
        'Protected oRepNotaCobranza As New repNotaCobranza()
        'Protected oRepNotaCobranzaLluvia As New repNotaCobranzaLluvia()                 'TODO: AHSP 200804024- Adicionado x Lluvias
        'Protected oRepNotaCobranzaLluvia2 As New repNotaCobranzaLluvia2()


        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then

                'Put user code to initialize the page here
                Dim documentType As String
                Dim pagares As String
                Dim amounts As String
                Dim anio As String
                Dim mes As String
                Dim proceso As String
                Dim dsSign As DataSet
                Dim dsProceso As DataSet
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
                        'oRepCartaNotarial.SetDataSource(ds)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repCartaNotarial.rdlc")
                        ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                        'CrystalReportViewer1.ReportSource = oRepCartaNotarial
                    Case "C"
                        'TODO: AHSP 20080425-Clientes con Reporte especial por Lluvias
                        If lstrCodigo_Cliente = "190828057" Then    'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                            rdsDataset.Value = dsC.Tables(0)
                            ' orepCartaCobranzaLightLluvia2.SetDataSource(dsC)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repCartaCobranzaLightLluvia2.rdlc")
                            ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                            'CrystalReportViewer1.ReportSource = orepCartaCobranzaLightLluvia2
                        ElseIf lstrCodigo_Cliente = "190828058" Then 'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                            rdsDataset.Value = dsC.Tables(0)
                            'orepCartaCobranzaLightLluvia.SetDataSource(dsC)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repCartaCobranzaLightLluvia.rdlc")
                            ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                            'CrystalReportViewer1.ReportSource = orepCartaCobranzaLightLluvia
                        Else
                            'oRepCartaCobranza.SetDataSource(ds)
                            'CrystalReportViewer1.ReportSource = oRepCartaCobranza
                            rdsDataset.Value = dsC.Tables(0)
                            'orepCartaCobranzaLight.SetDataSource(dsC)
                            ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repCartaCobranzaLight.rdlc")
                            ReportViewer1.LocalReport.DataSources.Add(rdsDataset)
                            'CrystalReportViewer1.ReportSource = orepCartaCobranzaLight
                        End If
                    Case "N"
                        'TODO: AHSP 20080424-Clientes con Reporte especial por Lluvias
                        If lstrCodigo_Cliente = "190828057" Then        'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                            rdsCobranza.Value = ds.Tables(0)
                            ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                            'oRepNotaCobranzaLluvia2.SetDataSource(ds)
                        ElseIf lstrCodigo_Cliente = "190828058" Then    'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                            ' oRepNotaCobranzaLluvia.SetDataSource(ds)
                            rdsCobranza.Value = ds.Tables(0)
                            ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                        Else
                            'Reporte Normal
                            rdsCobranza.Value = ds.Tables(0)
                            ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                            ' oRepNotaCobranza.SetDataSource(ds)
                        End If

                        If Request.Params("export") Is Nothing Then
                            'TODO: AHSP 20080424-Clientes con Reporte especial por Lluvias
                            If lstrCodigo_Cliente = "190828057" Then        'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repNotaCobranzaLluvia2.rdlc")
                                'CrystalReportViewer1.ReportSource = oRepNotaCobranzaLluvia2
                            ElseIf lstrCodigo_Cliente = "190828058" Then    'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repNotaCobranzaLluvia.rdlc")
                                'CrystalReportViewer1.ReportSource = oRepNotaCobranzaLluvia
                            Else
                                'Reporte Normal
                                ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repNotaCobranza.rdlc")
                                ' CrystalReportViewer1.ReportSource = oRepNotaCobranza
                            End If
                        Else
                            '------------------------
                            Dim name As String = getWebServerDateId() & ".pdf"
                            Dim filename As String = ConfigurationSettings.AppSettings("GenFolder") & name  'Server.MapPath(".") & "\export\" & name
                            RemoveFiles(ConfigurationSettings.AppSettings("GenFolder"), New TimeSpan(0, 6, 0, 0))

                            'TODO: AHSP 20080424-Clientes con Reporte especial por Lluvias
                            'If lstrCodigo_Cliente = "190828057" Then        'DIRECCION REGIONAL DE EDUCACION LAMBAYEQUE
                            '    With oRepNotaCobranzaLluvia2.ExportOptions
                            '        .ExportDestinationType = ExportDestinationType.DiskFile
                            '        .ExportFormatType = ExportFormatType.PortableDocFormat
                            '        .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            '        .DestinationOptions.DiskFileName = filename
                            '    End With
                            '    oRepNotaCobranzaLluvia2.Export()
                            'ElseIf lstrCodigo_Cliente = "190828058" Then    'DIRECCION REGIONAL DE EDUC AMAZONAS CHACHAPOY
                            '    With oRepNotaCobranzaLluvia.ExportOptions
                            '        .ExportDestinationType = ExportDestinationType.DiskFile
                            '        .ExportFormatType = ExportFormatType.PortableDocFormat
                            '        .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            '        .DestinationOptions.DiskFileName = filename
                            '    End With
                            '    oRepNotaCobranzaLluvia.Export()
                            'Else
                            '    'Reporte Normal
                            '    With oRepNotaCobranza.ExportOptions
                            '        .ExportDestinationType = ExportDestinationType.DiskFile
                            '        .ExportFormatType = ExportFormatType.PortableDocFormat
                            '        .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            '        .DestinationOptions.DiskFileName = filename
                            '    End With
                            '    oRepNotaCobranza.Export()
                            'End If


                            'exportar a archivo fisico
                            Dim byteFileRdlc = BIFUtils.WS.Utils.RDLC_ExportarPDF(ReportViewer1)
                            'If Not BIFUtils.WS.Utils.RDLC_ExportFile(filename, byteFileRdlc) Then
                            '    Throw New Exception("Error al crear archivo pdf en servidor")
                            'End If


                            'Dim ms As System.IO.MemoryStream
                            'Dim fileS As FileStream = File.OpenRead(filename)

                            'ms = New MemoryStream(fileS.Length)
                            'Dim br As BinaryReader = New BinaryReader(fileS)
                            'Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

                            'ms.Write(bytesRead, 0, fileS.Length)

                            With HttpContext.Current.Response
                                .ClearContent()
                                .ClearHeaders()
                                .ContentType = "application/pdf"
                                .AddHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".PDF")
                                .BinaryWrite(byteFileRdlc)
                                '.BinaryWrite(ms.ToArray)

                                .End()
                            End With

                            '--------------------------
                        End If

                    Case Else
                        ' oRepNotaCobranza.SetDataSource(ds)
                        rdsCobranza.Value = ds.Tables(0)
                        ReportViewer1.LocalReport.DataSources.Add(rdsCobranza)
                        ReportViewer1.LocalReport.ReportPath = Server.MapPath("descuentos/repNotaCobranza.rdlc")
                        'CrystalReportViewer1.ReportSource = oRepNotaCobranza
                End Select


            End If
        End Sub

    End Class





End Namespace
