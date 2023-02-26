Imports BIFConvenios
Imports Resource

Partial Public Class controls_BuscarEmpresa
    Inherits UserControl

    Protected oProc As New Proceso()

    Public Delegate Sub UpdateDelegate(resultadoBusqueda As String)
    Public Event UpdateEvent As UpdateDelegate

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        lblMensaje.Text = ""

        If Not Page.IsPostBack Then

            Try

                Call BindGrid()

            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message
            End Try


        End If


    End Sub
    Private Sub BindGrid()

        Dim pCliente As String
        Dim ds As New DataSet

        If Me.txtfEmpresa.Text <> "" Then
            pCliente = txtfEmpresa.Text
        Else
            pCliente = "*"
        End If

        ds = oProc.GetClienteBIFConvenios(pCliente)

        If ds.Tables(0).Rows.Count > 0 Then
            dgDatos.DataSource = ds
            dgDatos.DataBind()
            lblNumReg.Text = dgDatos.Rows.Count.ToString()

        Else
            lblNumReg.Text = 0
            lblMensaje.Text = "La consulta no devolvió datos."

        End If

    End Sub
    Protected Function GetMensajeSeleccionar(ByVal pstrCodIBS As String, ByVal Empresa As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + pstrCodIBS + "','" + Empresa + "');"">Seleccionar</a>"

        Return strEnlace
    End Function
    Protected Sub btnSearchServer_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Call BindGrid()
        mpeBuscarEmpresa.Show()
    End Sub
    Protected Sub dgDatos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgDatos.PageIndexChanging
        dgDatos.PageIndex = e.NewPageIndex
        lblMensaje.Text = ""

        Try
            Call BindGrid()
            mpeBuscarEmpresa.Show()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub
    Protected Sub BtnSeleccionarRegistro_Click(sender As Object, e As EventArgs) Handles BtnSeleccionarRegistro.Click
        Dim ResultadoBusqueda As String = hfCodigoIBS.Value & "|" & hfNombreEmpresa.Value
        RaiseEvent UpdateEvent(ResultadoBusqueda)
    End Sub

End Class