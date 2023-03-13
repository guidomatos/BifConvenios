Imports BIFConvenios
Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class frmDetalleEnviosAutomaticos
    Inherits Page

    Protected objLogEnvioCorreo As New clsLogEnvioCorreo()
    Protected objSystemParameters As New clsSystemParameters()

    Protected objLogEnvioCorreoBL As New clsLogEnvioCorreosBL()
    Protected objSystemParametersBL As New clsSystemParametersBL()

    Protected dtLogEnvioCorreo As New DataTable()
    Protected dtSystemParameters As New DataTable()

    Private intCodigoProcesoAutomatico As Integer

    Private Sub BindDG(pintProcesoAutomatico As Integer, pintCodigoCliente As String, pintCodigoIBS As String, pintTipoEnvioCorreo As Integer, pintEstadoEnvio As Integer)
        pnlQueryResult.Visible = True
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            objLogEnvioCorreo.iProcesoAutomaticoId = pintProcesoAutomatico
            objLogEnvioCorreo.iEnvioCorreoId = 0
            objLogEnvioCorreo.iTipoEnvioCorreoId = pintTipoEnvioCorreo
            objLogEnvioCorreo.iCodigoCliente = Convert.ToInt32(pintCodigoCliente)
            objLogEnvioCorreo.iCodigoIBS = Convert.ToInt32(pintCodigoIBS)
            objLogEnvioCorreo.iEstado = pintEstadoEnvio

            dtLogEnvioCorreo = objLogEnvioCorreoBL.Seleccionar(objLogEnvioCorreo)

            gvQuery.DataSource = dtLogEnvioCorreo
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

        dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoEnvioCorreo).ToString())

        ddlTipoEnvio.DataSource = dtSystemParameters
        ddlTipoEnvio.DataBind()
        ddlTipoEnvio.Items.Insert(0, New ListItem("-- Todos --", "0"))

        ddlTipoEnvio.Items(0).Selected = True

        dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.EstadoEnvioCorreo).ToString())

        ddlEstado.DataSource = dtSystemParameters
        ddlEstado.DataBind()
        ddlEstado.Items.Insert(0, New ListItem("-- Todos --", "0"))

        ddlEstado.Items(0).Selected = True
    End Sub

    Protected Sub gvQuery_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        gvQuery.PageIndex = e.NewPageIndex

        Dim strValor As String

        If Len(txtValor.Text) > 0 Then
            strValor = txtValor.Text
        Else
            strValor = "0"
        End If

        If ddlCriterio.Text = "0" Then
            BindDG(intCodigoProcesoAutomatico, 0, 0, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        ElseIf ddlCriterio.Text = "1" Then
            BindDG(intCodigoProcesoAutomatico, strValor, 0, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        ElseIf ddlCriterio.Text = 2 Then
            BindDG(intCodigoProcesoAutomatico, 0, strValor, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Request.Params("id") IsNot Nothing Then
            intCodigoProcesoAutomatico = Convert.ToInt32(Request.Params("id").ToString())
        Else
            intCodigoProcesoAutomatico = 0
        End If

        If Not IsPostBack Then
            LlenarCombos()

            pnlQueryResult.Visible = True
            pnlMensaje.Visible = False

            BindDG(intCodigoProcesoAutomatico, 0, 0, ddlTipoEnvio.Text, ddlEstado.Text)
        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Dim strValor As String

        If Len(txtValor.Text) > 0 Then
            strValor = txtValor.Text
        Else
            strValor = "0"
        End If

        pnlQueryResult.Visible = True
        pnlMensaje.Visible = False

        If ddlCriterio.Text = "0" Then
            BindDG(intCodigoProcesoAutomatico, 0, 0, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        ElseIf ddlCriterio.Text = "1" Then
            BindDG(intCodigoProcesoAutomatico, strValor, 0, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        ElseIf ddlCriterio.Text = 2 Then
            BindDG(intCodigoProcesoAutomatico, 0, strValor, Convert.ToInt32(ddlTipoEnvio.Text), Convert.ToInt32(ddlEstado.Text))
        End If
    End Sub

    Protected Sub lnkBack_Click(sender As Object, e As EventArgs) Handles lnkBack.Click
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/frmVisorEnviosAutomaticos.aspx"))
    End Sub
End Class
