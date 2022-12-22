Partial Class EsperaFinalAnulacion
    Inherits System.Web.UI.Page
    Protected oProcess As New BIFConvenios.Proceso()

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
        If Not Request.Params("id") Is Nothing Then

            If oProcess.EsperaFinalAnulacionProceso(CType(Request.Params("id"), String)) Then
                pnlSwf.Visible = False
                pnlFinal.Visible = True
            Else
                pnlSwf.Visible = True
                pnlFinal.Visible = False
            End If
        End If
    End Sub

End Class
