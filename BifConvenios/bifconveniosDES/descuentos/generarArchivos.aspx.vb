Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class generarArchivos
        Inherits Page

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

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Request.Params("id") IsNot Nothing And Request.Params("type") IsNot Nothing Then
                Dim id As String = Request.Params("id")
                Dim type As String = Request.Params("type")

                Dim oReport As New CSVWebReport()
                Dim dr As SqlDataReader = Proceso.GetRegistrosResultadoProcesoDescuentosErrores(id, True)
                'ltrlEnlace.Text = oReport.GenerateCSVReport(dr, AppSettings("GenFolder"), AppSettings("virtualPath"), False, False, "Descargar Archivo")
                ltrlEnlace.Text = "<a href='" + ResolveUrl("../DownloadFile.aspx?File=") +
                oReport.GenerateCSVReport(dr, ConfigurationManager.AppSettings("GenFolder"), ConfigurationManager.AppSettings("virtualPath"), True, False, "Descargar Archivo",
                "", True) + "'>Descargar Archivo</a>"

            End If
        End Sub

    End Class
End Namespace
