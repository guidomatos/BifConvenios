'******************************************************
' Proyecto - BIFConvenios
' Autor      : RESPINOZA
' Parametros : 
' -------------------------------------------------------------------------------------
' - Tipo     : Objeto de Session 
' - Nombre   : Info
' - Descripción: Se debe proporcionar a esta página un objeto DataSetListadoCuotasFin 
'              usando el parametro de sesión Info
' -------------------------------------------------------------------------------------
' - Tipo    : Parametro 
' - Nombre  : export
' - Descripcion: Determina si se realiza exportacion directa del formato

Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Namespace BIFConvenios

    Public Class ContainerListadoConsultaEnvio
        Inherits System.Web.UI.Page
        Protected oCliente As New Cliente()
        Protected Codigo_proceso As String = ""
        Protected TipoDocumento As String = ""
        Protected NumeroDocumento As String = ""
        Protected Anio_periodo As String = ""
        'Protected WithEvents CrystalReportViewer1 As CrystalDecisions.Web.CrystalReportViewer
        Protected Mes_Periodo As String = ""
        'MOD: AAZ - 2017/12/06 - Incidencia 26
        'Protected ds As New DataSetListadoCuotasFin()
        'Protected oRepListadoCuotasPorVencer As New RepListadoCuotasPorVencer()
        'FIN MOD
        Protected idp As String

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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

            If Not Page.IsPostBack Then
                'ADD: AAZ - 2017/12/06 - Incidencia 26
                'Inicializamos e instanciamos el objeto CrystalReportViewer, DataSet
                'Dim oRepListadoCuotasPorVencer As New RepListadoCuotasPorVencer()
                Dim ds As New DataSetListadoCuotasFin()
                Dim rdsDlinfo As New ReportDataSource With {
                    .Name = "DLinfo"
                }
                Dim rdsCliente As New ReportDataSource With {
                    .Name = "Cliente"
                }
                'FIN ADD

                If Request.Params("idp") Is Nothing Then
                    If (Session.Item("Info") IsNot Nothing) Then

                        ds = CType(Session.Item("Info"), DataSetListadoCuotasFin)
                        'ds.WriteXml("C:/DATASET.TXT")

                        'oRepListadoCuotasPorVencer.SetDataSource(ds)
                        If Not IsNothing(ds) Then
                            If ds.Tables.Count > 0 Then
                                rdsDlinfo.Value = ds.Tables(0)
                                rdsCliente.Value = ds.Tables(1)
                            End If
                        End If
                        'ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS - ARCHIVOS DESCUENTOS
                        If Not (Session.Item("idx") Is Nothing) Then hdId.Value = Session.Item("idx").ToString
                        'END

                    End If
                Else

                    idp = Request.Params("idp")
                    'ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS - ARCHIVOS DESCUENTOS
                    hdId.Value = idp
                    'END

                    'Session.Add("Info", ArchivosDescuento.getDataSetListadoCuotasFin(idp))
                    ds = ArchivosDescuento.getDataSetListadoCuotasFin(idp, "-", "-")

                    'ds.WriteXml("C:/DATASET.TXT")
                    ' oRepListadoCuotasPorVencer.SetDataSource(ds)
                    If Not IsNothing(ds) Then
                        If ds.Tables.Count > 0 Then
                            rdsDlinfo.Value = ds.Tables(0)
                            rdsCliente.Value = ds.Tables(1)
                        End If
                    End If
                    Dim oProceso As New Proceso()
                    oProceso.UpdateFechaObtencionArchivo(idp, False, Context.User.Identity.Name)
                    oProceso.UpdEstadoGeneracionExito(idp, Context.User.Identity.Name)

                End If

                If Request.Params("export") Is Nothing Then
                    'MIGRAR INNOVA FALTA
                    'CrystalReportViewer1.ReportSource = oRepListadoCuotasPorVencer
                    If Not IsNothing(ds) Then
                        If ds.Tables.Count > 0 Then
                            rdsDlinfo.Value = ds.Tables(0)
                            rdsCliente.Value = ds.Tables(1)
                        End If
                    End If
                    ReportViewer1.LocalReport.DataSources.Add(rdsDlinfo)
                    ReportViewer1.LocalReport.DataSources.Add(rdsCliente)
                Else

                    Dim name As String = getWebServerDateId() & ".xls"
                    Dim filename As String = Server.MapPath(".") & "\export\" & name
                    RemoveFiles(Server.MapPath(".") & "\export\", New TimeSpan(0, 6, 0, 0))

                    'With oRepListadoCuotasPorVencer.ExportOptions
                    '    .ExportDestinationType = ExportDestinationType.DiskFile
                    '    .ExportFormatType = ExportFormatType.Excel
                    '    .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                    '    .DestinationOptions.DiskFileName = filename
                    'End With

                    'oRepListadoCuotasPorVencer.Export()

                    ''ADD: AAZ - 2017/12/06 - Incidencia 26
                    ''Cerramos y limpiamos el Objeto Crystal
                    'oRepListadoCuotasPorVencer.Close()
                    'oRepListadoCuotasPorVencer.Dispose()
                    'FIN ADD

                    ReportViewer1.LocalReport.DataSources.Add(rdsDlinfo)
                    ReportViewer1.LocalReport.DataSources.Add(rdsCliente)


                    'exportar a archivo fisico
                    Dim byteFileRdlc = BIFUtils.WS.Utils.RDLC_ExportarExcel(ReportViewer1)

                    'If Not BIFUtils.WS.Utils.RDLC_ExportFile(filename, byteFileRdlc) Then
                    '    Throw New Exception("Error al crear archivo excel en servidor")
                    'End If

                    'Dim ms As System.IO.MemoryStream
                    'Dim fileS As FileStream = File.OpenRead(filename)

                    'ms = New MemoryStream(fileS.Length)
                    'Dim br As BinaryReader = New BinaryReader(fileS)
                    'Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

                    'ms.Write(bytesRead, 0, fileS.Length)

                    If Request.Params("idp") Is Nothing Then

                        With HttpContext.Current.Response
                            .ClearContent()
                            .ClearHeaders()
                            .ContentType = "application/vnd.ms-excel"
                            .AddHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".xls")
                            .BinaryWrite(byteFileRdlc)

                            .End()
                        End With
                    Else
                        Response.Redirect("EnvioReporteMail.aspx?idP=" + idp + "&archivo=export\" & name)
                    End If

                End If
            End If


        End Sub

        'ADD 24/06 NCA: REQ EA2013-273 GENERACION ARCHIVO DESCUENTOS.
        Protected Sub lnkExportarArchivoDescuentos_Click(sender As Object, e As EventArgs) Handles lnkExportarArchivoDescuentos.Click
            Dim strNombreArchivo As String = getWebServerDateId() & ".xls"
            Call (New ArchivosDescuento).GenerarArchivExcelDescuentos(hdId.Value, strNombreArchivo, "-", 0, 0, 0, "-")
            Call Utils.InvocarArchivo(ConfigurationManager.AppSettings("GenFolder"), strNombreArchivo)
        End Sub

        Protected Function getDataTableListadoCuotasFin(strCodigoProceso As String) As DataTable
            Dim ds As New DataSetListadoCuotasFin()
            Dim dsListadoCuotas As DataSet

            Dim dr As SqlDataReader = oCliente.GetInfoClienteProceso(strCodigoProceso)
            If dr.Read Then
                TipoDocumento = CType(dr("TipoDocumento"), String)
                NumeroDocumento = CType(dr("NumeroDocumento"), String)
                Anio_periodo = CType(dr("Anio_periodo"), String)
                Mes_Periodo = CType(dr("Mes_Periodo"), String)
            End If

            Dim dsInfoContactoConvenio As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)
            dsListadoCuotas = oCliente.GetDatosArchivoCuotasPorVencerCliente(strCodigoProceso)

            Dim dtRet As DataTable = dsListadoCuotas.Tables(0).Copy
            Return dtRet
        End Function


        'END ADD

    End Class
End Namespace
