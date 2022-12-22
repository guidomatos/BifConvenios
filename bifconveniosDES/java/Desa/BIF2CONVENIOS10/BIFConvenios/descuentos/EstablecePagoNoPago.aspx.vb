'===========================================================================
' This file was modified as part of an ASP.NET 2.0 Web project conversion.
' The class name was changed and the class modified to inherit from the abstract base class 
' in file 'App_Code\Migrated\descuentos\Stub_establecepagonopago_aspx_vb.vb'.
' During runtime, this allows other classes in your web application to bind and access 
' the code-behind page using the abstract base class.
' The associated content page 'descuentos\establecepagonopago.aspx' was also modified to refer to the new class name.
' For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
'===========================================================================



Namespace BIFConvenios


    Partial Class EstablecePagoNoPago
        Inherits System.Web.UI.Page
        'Protected WithEvents dgPagoNoPago As System.Web.UI.WebControls.DataGrid
        'Protected WithEvents lnkRegresar As System.Web.UI.WebControls.LinkButton
        Protected oProc As New BIFConvenios.Proceso()
        Protected Codigo_Proceso As String = ""

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
                Codigo_Proceso = CType(Request.Params("id"), String)
            Else
                Exit Sub
            End If

            If Not Page.IsPostBack Then
                dgPagoNoPago.DataSource = oProc.GetInfoPagoNoPago(Codigo_Proceso)
                dgPagoNoPago.DataBind()
            End If
        End Sub

        Private Sub lnkRegresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkRegresar.Click
            Response.Redirect(ResolveUrl("ReporteProcesoDescuento.aspx"))

        End Sub

        'Obtenemos el nombre de la imagen checkada
        Protected Function GetImage(ByVal checked As Boolean)
            Dim imageChecked As String = "checked.gif"
            Dim imageunChecked As String = "unchecked.gif"
            If checked Then
                Return imageChecked
            Else
                Return imageunChecked
            End If
        End Function

        Private Sub dgPagoNoPago_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgPagoNoPago.ItemCommand
            If e.CommandName.Trim = "Select" Then
                'Response.Write("Seleccionado:" + dgPagoNoPago.DataKeys(e.Item.ItemIndex).ToString)
                oProc.EstablecePagoNoPago(Codigo_Proceso, dgPagoNoPago.DataKeys(e.Item.ItemIndex).ToString)
                dgPagoNoPago.DataSource = oProc.GetInfoPagoNoPago(Codigo_Proceso)
                dgPagoNoPago.DataBind()
            End If
        End Sub
    End Class
End Namespace
