Imports BIFConvenios
Imports Resource

Partial Class controls_BuscarParametrosEmpresa
    Inherits UserControl

    Protected oProc As New Proceso()

    Public Delegate Sub UpdateDelegate(resultadoBusqueda As String)
    Public Event UpdateEvent As UpdateDelegate

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            '-------Informacion del año
            ddlAnio.DataSource = oProc.GetAnioEnvioArchivoAS400()
            ddlAnio.DataBind()
            ddlAnio.Items.Insert(0, New ListItem("-- Seleccione --", ""))
            ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

            'Informacion del mes
            Call GetMonths()
            Call BindGrid()

        End If
    End Sub

    Private Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMes.SelectedIndexChanged
        Call BindGrid()
        mpeBuscarParametroEmpresa.Show()
    End Sub


    Private Sub GetMonths()
        ddlMes.DataSource = GetMonthsByReaderWithOutZero(oProc.GetMesesEnvioArchivoAS400(ddlAnio.SelectedItem.Value))
        ddlMes.DataBind()
        ddlMes.Items.Insert(0, New ListItem("-- Seleccione --", ""))
        ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
    End Sub

    Private Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnio.SelectedIndexChanged
        Call GetMonths()
        mpeBuscarParametroEmpresa.Show()
    End Sub

    Private Sub BindGrid()
        dgDatos.DataSource = oProc.GetProcesosEnvioArchivoAS400(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
        dgDatos.DataBind()
        lblNumReg.Text = dgDatos.Rows.Count.ToString()
        '            If dgDatos.Items.Count = 0 Then
        'TODO: Codigo para hacer invisible las dos ultimas columnas cuando no hay datos
        '           End If
    End Sub
    Protected Sub dgDatos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgDatos.PageIndexChanging
        dgDatos.PageIndex = e.NewPageIndex
        lblMensaje.Text = ""

        Try
            Call BindGrid()
            mpeBuscarParametroEmpresa.Show()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub
    Protected Sub BtnSeleccionarRegistro_Click(sender As Object, e As EventArgs) Handles BtnSeleccionarRegistro.Click
        Dim ResultadoBusqueda As String = hfResultadoBusqueda.Value
        RaiseEvent UpdateEvent(ResultadoBusqueda)
    End Sub

End Class