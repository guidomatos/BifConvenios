Namespace BIFConvenios


    Partial Class EstablecePagoNoPago
        Inherits Page
        'Protected WithEvents dgPagoNoPago As Web.UI.WebControls.DataGrid
        'Protected WithEvents lnkRegresar As Web.UI.WebControls.LinkButton
        Protected oProc As New Proceso()
        Protected Codigo_Proceso As String = ""

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Request.Params("id") IsNot Nothing Then
                Codigo_Proceso = CType(Request.Params("id"), String)
            Else
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                dgPagoNoPago.DataSource = oProc.GetInfoPagoNoPago(Codigo_Proceso)
                dgPagoNoPago.DataBind()
            End If
        End Sub

        Private Sub lnkRegresar_Click(sender As Object, e As EventArgs) Handles lnkRegresar.Click
            Response.Redirect(ResolveUrl("ReporteProcesoDescuento.aspx"))

        End Sub

        'Obtenemos el nombre de la imagen checkada
        Protected Function GetImage(checked As Boolean)
            Dim imageChecked As String = "checked.gif"
            Dim imageunChecked As String = "unchecked.gif"
            If checked Then
                Return imageChecked
            Else
                Return imageunChecked
            End If
        End Function

        Private Sub dgPagoNoPago_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgPagoNoPago.ItemCommand
            If e.CommandName.Trim = "Select" Then
                'Response.Write("Seleccionado:" + dgPagoNoPago.DataKeys(e.Item.ItemIndex).ToString)
                oProc.EstablecePagoNoPago(Codigo_Proceso, dgPagoNoPago.DataKeys(e.Item.ItemIndex).ToString)
                dgPagoNoPago.DataSource = oProc.GetInfoPagoNoPago(Codigo_Proceso)
                dgPagoNoPago.DataBind()
            End If
        End Sub
    End Class
End Namespace
