'******************************************************
' Proyecto - BIFConvenios
' Autor      : RESPINOZA
' Parametros : 
' -------------------------------------------------------------------------------------
' - Tipo     : Objeto de Session 
' - Nombre   : Info
' - Descripci?n: Se debe proporcionar a esta p?gina un objeto DataSetListadoCuotasFin 
'              usando el parametro de sesi?n Info
' -------------------------------------------------------------------------------------
' - Tipo    : Parametro 
' - Nombre  : export
' - Descripcion: Determina si se realiza exportacion directa del formato

Imports CrystalDecisions.CrystalReports.Engine
'******************************************************
Imports System.Data.SqlClient
Imports System.IO
Imports CrystalDecisions.Shared
Namespace BIFConvenios

    Partial Class ContainerListadoConsultaEnvio
        Inherits System.Web.UI.Page
        Protected oCliente As New Cliente()
        Protected Codigo_proceso As String = ""
        Protected TipoDocumento As String = ""
        Protected NumeroDocumento As String = ""
        Protected Anio_periodo As String = ""
        Protected Mes_Periodo As String = ""
        Protected ds As New DataSetListadoCuotasFin()
        Protected oRepListadoCuotasPorVencer As New RepListadoCuotasPorVencer()
        Protected idp As String

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

            If Request.Params("idp") Is Nothing Then
                If (Not Session.Item("Info") Is Nothing) Then
                    ds = CType(Session.Item("Info"), DataSetListadoCuotasFin)
                    'ds.WriteXml("C:/DATASET.TXT")
                    oRepListadoCuotasPorVencer.SetDataSource(ds)
                End If
            Else
                idp = Request.Params("idp")
                'Session.Add("Info", ArchivosDescuento.getDataSetListadoCuotasFin(idp))
                ds = CType(ArchivosDescuento.getDataSetListadoCuotasFin(idp, 1, 1), DataSetListadoCuotasFin) 'parametros agregados

                'ds.WriteXml("C:/DATASET.TXT")

                oRepListadoCuotasPorVencer.SetDataSource(ds)
                Dim oProceso As New BIFConvenios.Proceso()
                oProceso.UpdateFechaObtencionArchivo(idp, False, context.User.Identity.Name)
                Proceso.UpdEstadoGeneracionExito(idp, context.User.Identity.Name)
            End If

            If Request.Params("export") Is Nothing Then
                CrystalReportViewer1.ReportSource = oRepListadoCuotasPorVencer
            Else
                Dim name As String = getWebServerDateId() & ".xls"
                Dim filename As String = Server.MapPath(".") & "\export\" & name
                RemoveFiles(Server.MapPath(".") & "\export\", New TimeSpan(0, 6, 0, 0))

                With oRepListadoCuotasPorVencer.ExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.Excel
                    .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                    .DestinationOptions.DiskFileName = filename
                End With
                oRepListadoCuotasPorVencer.Export()

                Dim ms As System.IO.MemoryStream
                Dim fileS As FileStream = File.OpenRead(filename)

                ms = New MemoryStream(fileS.Length)
                Dim br As BinaryReader = New BinaryReader(fileS)
                Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

                ms.Write(bytesRead, 0, fileS.Length)

                If Request.Params("idp") Is Nothing Then

                    With HttpContext.Current.Response
                        .ClearContent()
                        .ClearHeaders()
                        .ContentType = "application/vnd.ms-excel"
                        .AddHeader("Content-Disposition", "inline; filename=" & getWebServerDateId() & ".xls")
                        .BinaryWrite(ms.ToArray)

                        .End()
                    End With
                Else
                    Response.Redirect("EnvioReporteMail.aspx?idP=" + idp + "&archivo=export\" & name)
                End If
            End If
        End Sub
    End Class
End Namespace
