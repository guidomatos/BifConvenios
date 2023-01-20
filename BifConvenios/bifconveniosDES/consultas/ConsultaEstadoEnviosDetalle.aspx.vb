Imports System.Data
Imports System.Data.SqlClient

Imports BIFConvenios

Partial Class ConsultaEstadoEnviosDetalle
    Inherits System.Web.UI.Page
    Protected oproc As New Proceso()

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
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then

            Dim accion As String = CType(Request.Params("accion"), String)
            If (accion.CompareTo("detalle") = 0) Then
                Utils.AddSwap(lnkBack, "Image1", "/BIFConvenios/images/regresar_on.jpg")
                Dim dr As SqlDataReader
                dr = oproc.InformeProceso(CType(Request.Params("id"), String))
                If dr.Read Then
                    ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlperiodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                    ltrlfechaRetorno.Text = dr("Fecha_ProcesoAD")

                    Dim ds As DataSet = oproc.GetArchivosDescuentosHistDet(Request.Params("id"))
                    Session.Add("detalleArchivosDescuentosHistDet", ds)
                    Session.Add("detalleArchivosDescuentosHistCab", dr)
                    dgProcesoResult.DataSource = ds
                    dgProcesoResult.DataBind()
                    dgProcesoResult.Visible = True
                    lblTotalReg.Text = ds.Tables(0).Rows.Count 'dgProcesoResult.Items.Count.ToString.Trim()
                    Dim i As Integer
                    Dim SumaRegistrosOK As Integer = 0
                    Dim SumaRegistrosKO As Integer = 0

                    For i = 0 To ds.Tables(0).Rows.Count() - 1
                        If (ds.Tables(0).Rows(i).Item("Estado").ToString().CompareTo("C0") = 0) Then
                            SumaRegistrosOK += 1
                        Else
                            SumaRegistrosKO += 1
                        End If
                    Next

                    TotalProcesados.Text = SumaRegistrosOK
                    TotalRechazados.Text = SumaRegistrosKO
                    TotalRecibidos.Text = (SumaRegistrosOK + SumaRegistrosKO)
                End If

            ElseIf (accion.CompareTo("exportar") = 0) Then
                Dim ds As DataSet = Session.Item("detalleArchivosDescuentosHistDet")
                Dim dr As SqlDataReader = Session.Item("detalleArchivosDescuentosHistCab")
                Dim nombreArchivo As String = "ReporteEstadoEnvio.xls"
                Dim RutaArchivo As String = ConfigurationManager.AppSettings("GenFolder") & nombreArchivo
                Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("PlantillaExportarEstadoEnvio"))
                archivExcel.GenerarExcelConsultaEstadoEnvio(ds.Tables(0), RutaArchivo, CType(dr("Nombre_Cliente"), String))

                Response.ContentType = "image/jpeg"
                Response.AppendHeader("Content-Disposition", "attachment; filename=" & nombreArchivo)
                Response.TransmitFile(RutaArchivo)
                Response.End()
            End If

        End If
    End Sub

    Private Sub lnkBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        If Not Request.Params("id") Is Nothing Then
            Response.Redirect(ResolveUrl("ConsultaEstadoEnvios.aspx")) 'ReporteProcesoDescuentosResumen.aspx?id=" & CType(Request.Params("id"), String)))
        End If
    End Sub

    Private Sub dgProcesoResult_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgProcesoResult.PageIndexChanged
        dgProcesoResult.CurrentPageIndex = IIf(dgProcesoResult.PageCount < e.NewPageIndex, 0, e.NewPageIndex)
        Dim dsData As DataSet
        If Not Session("dsDataPostConciliacion") Is Nothing Then
            dsData = CType(Session("detalleConsultaEnvio"), DataSet)
            dgProcesoResult.DataSource = dsData
            dgProcesoResult.DataBind()
        Else
            dsData = oproc.GetRegistrosResultadoProcesoDescuentos(Request.Params("id"), "0", "")
            dgProcesoResult.DataSource = dsData
            dgProcesoResult.DataBind()

        End If

    End Sub
End Class
