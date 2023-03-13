Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class frmVisorEnviosAutomaticos
    Inherits Page

    Protected objProcesoAutomatico As New clsProcesosAutomaticos()
    Protected objSystemParameters As New clsSystemParameters()
    Protected objProcesoAutomaticoBL As New clsProcesosAutomaticosBL()
    Protected objSystemParametersBL As New clsSystemParametersBL()
    Protected _dtProcesosAutomaticos As New DataTable()
    Protected _dtSystemParameters As New DataTable()

    Private Sub BindDG(pintEstado As Integer)
        pnlQueryResult.Visible = True
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            objProcesoAutomatico.iEstado = pintEstado

            _dtProcesosAutomaticos = objProcesoAutomaticoBL.Seleccionar(objProcesoAutomatico)

            gvQuery.DataSource = _dtProcesosAutomaticos
            gvQuery.DataBind()

        Catch ex As HandledException
            pnlQueryResult.Visible = False
            pnlMensaje.Visible = True
            lblMensaje.Text = ex.ErrorMessage + ": " + ex.ErrorMessageFull
        End Try
    End Sub

    Private Sub LlenarCombos()
        Dim dtTipoEnvio As New DataTable()
        Dim dtEstadoEnvio As New DataTable()

        _dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.EstadoProcesoAutomatico).ToString())

        ddlEstado.DataSource = _dtSystemParameters
        ddlEstado.DataBind()
        ddlEstado.Items.Insert(0, New ListItem("-- Todos --", "0"))
    End Sub

    Protected Sub gvQuery_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        gvQuery.PageIndex = e.NewPageIndex

        BindDG(Convert.ToInt32(ddlEstado.Text))
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LlenarCombos()

            pnlQueryResult.Visible = True
            pnlMensaje.Visible = False

            BindDG(Convert.ToInt32(ddlEstado.Text))
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        BindDG(Convert.ToInt32(ddlEstado.Text))
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Response.Redirect("cargageneracioncf.aspx")
    End Sub
End Class
