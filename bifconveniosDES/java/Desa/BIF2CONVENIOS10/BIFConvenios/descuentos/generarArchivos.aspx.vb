Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings

Namespace BIFConvenios

    Partial Class generarArchivos
        Inherits System.Web.UI.Page

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
            If Not Request.Params("id") Is Nothing And Not Request.Params("type") Is Nothing Then
                Dim id As String = Request.Params("id")
                Dim type As String = Request.Params("type")

                Dim oReport As New CSVWebReport()
                Dim dr As SqlDataReader = Proceso.GetRegistrosResultadoProcesoDescuentosErrores(id, True)
                'ltrlEnlace.Text = oReport.GenerateCSVReport(dr, AppSettings("GenFolder"), AppSettings("virtualPath"), False, False, "Descargar Archivo")
                ltrlEnlace.Text = "<a href='" + ResolveUrl("../DownloadFile.aspx?File=") + _
                oReport.GenerateCSVReport(dr, AppSettings("GenFolder"), AppSettings("virtualPath"), True, False, "Descargar Archivo", _
                "", True) + "'>Descargar Archivo</a>"

            End If
        End Sub

    End Class
End Namespace
