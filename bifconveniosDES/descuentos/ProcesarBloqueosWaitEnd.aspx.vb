Namespace BIFConvenios
    Partial Class ProcesarBloqueosWaitEnd
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

            Try
                If Not Bloqueo.isLoteBloqueoProcesado(Request.Params("numeroLote")) Then
                    ltrlScript.Visible = False
                    lrtlSwf.Visible = True
                Else
                    ltrlScript.Visible = True
                    lrtlSwf.Visible = True
                End If

            Catch
                ltrlScript.Visible = True
                lrtlSwf.Visible = False

            End Try

        End Sub

    End Class
End Namespace
