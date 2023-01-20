
Namespace BIFConvenios
    Partial Class ProcesarBloqueosTestWait
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

            '20121016: AHSP(BANBIF) - Cambio del Proceso de Bloqueo
            'If Not Utils.TestServer() Then
            '    lblMensaje.Text = Utils.SERVER_UNAVAILABLE + "&nbsp;<a href=""javascript:close();"" class=""CommandButton"">Regresar</a>"
            '    lrtlSwf.Visible = False
            '    ltrlScript.Visible = False
            '    pnlError.Visible = True
            '    Exit Sub
            'Else
            '    lrtlSwf.Visible = True
            '    ltrlScript.Visible = True
            '    lblMensaje.Visible = False
            '    pagares.Value = Request.Params("pagares")
            '    proceso.Value = Request.Params("proceso")
            'End If
            
            lrtlSwf.Visible = True
            ltrlScript.Visible = True
            lblMensaje.Visible = False
            pagares.Value = Request.Params("pagares")
            proceso.Value = Request.Params("proceso")

        End Sub

    End Class
End Namespace