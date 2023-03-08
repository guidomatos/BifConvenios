Imports System.Data.SqlClient
Imports Microsoft.Reporting.WebForms

Namespace BIFConvenios
    Public Class ContainerListadoConsultaEnvioFinCierre
        Inherits Page
        Protected oCliente As New Cliente()
        Protected Codigo_proceso As String = ""
        Protected TipoDocumento As String = ""
        Protected NumeroDocumento As String = ""
        Protected Anio_periodo As String = ""
        Protected Mes_Periodo As String = ""

        Protected idp As String

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()


        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            InitializeComponent()
        End Sub

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            Dim ds As New DsListadoCuotasFinCierre()
            'System.Threading.Thread.CurrentThread.CurrentUICulture = New System.Globalization.CultureInfo("es-PE")
            'System.Threading.Thread.CurrentThread.CurrentUICulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
            'System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyDecimalSeparator = "."
            'System.Threading.Thread.CurrentThread.CurrentUICulture.NumberFormat.CurrencyGroupSeparator = ","

            If Request.Params("idp") Is Nothing Then
                If (Session.Item("Info") IsNot Nothing) Then
                    ds = CType(Session.Item("Info"), DsListadoCuotasFinCierre)

                    ReportViewer1.ProcessingMode = ProcessingMode.Local
                    ReportViewer1.LocalReport.DataSources.Clear()
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes\RepListadoCuotasFinCierre.rdlc")
                    Dim DatasourceDLinfo As New ReportDataSource("DsListadoCuotasFinCierre_DLinfo", ds.Tables(0))
                    ReportViewer1.LocalReport.DataSources.Add(DatasourceDLinfo)
                    Dim DatasourceCliente As New ReportDataSource("DsListadoCuotasFinCierre_Cliente", ds.Tables(1))
                    ReportViewer1.LocalReport.DataSources.Add(DatasourceCliente)

                    'ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS - ARCHIVOS DESCUENTOS
                    If Not (Session.Item("idx") Is Nothing) Then hdId.Value = Session.Item("idx").ToString
                    'END

                    'LIBERA EL OBJETO INSTANCIADO DE LA MEMORIA
                    ds.Dispose()
                End If
            Else
                idp = Request.Params("idp")
                'ADD NCA 08/07/2014 EA2013-273 OPT. PROCESOS CONVENIOS - ARCHIVOS DESCUENTOS
                hdId.Value = idp
                'END
                ds = ArchivosDescuento.getDataSetListadoCuotasFinCierre(idp, "-", "-")
                ReportViewer1.ProcessingMode = ProcessingMode.Local
                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes\RepListadoCuotasFinCierre.rdlc")
                Dim DatasourceDLinfo As New ReportDataSource("DsListadoCuotasFinCierre_DLinfo", ds.Tables(0))
                ReportViewer1.LocalReport.DataSources.Add(DatasourceDLinfo)
                Dim DatasourceCliente As New ReportDataSource("DsListadoCuotasFinCierre_Cliente", ds.Tables(1))
                ReportViewer1.LocalReport.DataSources.Add(DatasourceCliente)

                Dim oProceso As New Proceso()
                oProceso.UpdateFechaObtencionArchivo(idp, False, Context.User.Identity.Name)
                oProceso.UpdEstadoGeneracionExito(idp, Context.User.Identity.Name)
            End If
            If Request.Params("export") Is Nothing Then
                ReportViewer1.ProcessingMode = ProcessingMode.Local
                ReportViewer1.LocalReport.DataSources.Clear()
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Reportes\RepListadoCuotasFinCierre.rdlc")
                Dim DatasourceDLinfo As New ReportDataSource("DsListadoCuotasFinCierre_DLinfo", ds.Tables(0))
                ReportViewer1.LocalReport.DataSources.Add(DatasourceDLinfo)
                Dim DatasourceCliente As New ReportDataSource("DsListadoCuotasFinCierre_Cliente", ds.Tables(1))
                ReportViewer1.LocalReport.DataSources.Add(DatasourceCliente)
            Else

                Dim name As String = getWebServerDateId() & ".xls"
                Dim filename As String = Server.MapPath(".") & "\export\" & name
                RemoveFiles(Server.MapPath(".") & "\export\", New TimeSpan(0, 6, 0, 0))

                If Request.Params("idp") Is Nothing Then

                    Dim warnings As Warning()
                    Dim streamIds As String()
                    Dim contentType As String
                    Dim encoding As String
                    Dim extension As String

                    If ReportViewer1.LocalReport.DataSources.Count > 0 Then
                        Dim bytes As Byte() = ReportViewer1.LocalReport.Render("EXCEL", Nothing, contentType, encoding, extension, streamIds, warnings)
                        Response.Clear()
                        Response.Buffer = True
                        Response.Charset = ""
                        Response.Cache.SetCacheability(HttpCacheability.NoCache)
                        Response.ContentType = contentType
                        Response.AppendHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".xls")
                        Response.BinaryWrite(bytes)
                        Response.Flush()
                        Response.End()
                        ReportViewer1.Dispose()
                    End If
                Else
                    Response.Redirect("EnvioReporteMail.aspx?idP=" + idp + "&archivo=export\" & name)
                End If
            End If
        End Sub

        Protected Function getDataTableListadoCuotasFin(strCodigoProceso As String) As DataTable
            Dim ds As New DsListadoCuotasFinCierre()
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
            ds.Dispose()
            dsListadoCuotas.Dispose()
            Return dtRet
        End Function

    End Class
End Namespace

