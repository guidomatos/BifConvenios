Imports System.Data
Imports BIFConvenios
Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Alertas_frmAlertas
    Inherits System.Web.UI.Page

    Protected objSystemParameters As New clsSystemParameters
    Protected objAlertas As New clsAlertas

    Protected objSystemParametersBL As New clsSystemParametersBL
    Protected objAlertasBL As New clsAlertasBL

    Protected dtSystemParameters As New DataTable()
    Protected dtAlertas As New DataTable()

#Region "Metodos"

    Private Sub LlenarCombos()
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoAlerta).ToString())

            ddlTipoAlerta.DataSource = dtSystemParameters
            ddlTipoAlerta.DataBind()
            ddlTipoAlerta.Items.Insert(0, New ListItem("-- Todos --", "0"))

        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Sub BindDG(ByVal pintTipoAlerta As Integer, ByVal pstrNombreAlerta As String)

        Try
            objAlertas.iAlertaId = 0
            objAlertas.iTipoAlerta = pintTipoAlerta
            objAlertas.vNombreAlerta = pstrNombreAlerta
            objAlertas.iEstadoAlerta = -1

            dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlertas)

            pnlQueryAlertas.Visible = True
            pnlMensaje.Visible = False

            gvAlertas.DataSource = dtAlertas
            gvAlertas.DataBind()
        Catch ex1 As HandledException
            pnlQueryAlertas.Visible = False
            pnlMensaje.Visible = True

            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlQueryAlertas.Visible = False
            pnlMensaje.Visible = True

            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            LlenarCombos()

            If Not Session("Alerta") Is Nothing Then
                txtNombreAlerta.Text = Session("alerta").ToString()
            End If

            If Not Session("TipoAlerta") Is Nothing Then
                ddlTipoAlerta.SelectedValue = Session("TipoAlerta").ToString()
            End If

            BindDG(ddlTipoAlerta.Text, txtNombreAlerta.Text)
        End If

    End Sub

    Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Dim intTipoAlerta As Integer = Convert.ToInt32(ddlTipoAlerta.Text)

        Session("Alerta") = txtNombreAlerta.Text
        Session("TipoAlerta") = intTipoAlerta

        BindDG(intTipoAlerta, txtNombreAlerta.Text)
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'Response.Redirect("frmEditarAlertas.aspx", True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmEditarAlertas.aspx"), True)
    End Sub

    Protected Sub gvAlertas_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvAlertas.PageIndexChanging
        gvAlertas.PageIndex = e.NewPageIndex

        Dim intTipoAlerta As Integer = Convert.ToInt32(ddlTipoAlerta.Text)

        BindDG(intTipoAlerta, txtNombreAlerta.Text)
    End Sub

    Protected Sub lnkCambiarEstado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCambiarEstado.Click
        Try
            Dim intCodAlerta As Integer = Convert.ToInt32(hdIdAlerta.Value)
            Dim intEstadoAlerta As Integer = Convert.ToInt32(hdEstado.Value)
            Dim intTipoAlerta As Integer = Convert.ToInt32(ddlTipoAlerta.Text)

            If intEstadoAlerta = 1 Then
                objAlertas.iEstadoAlerta = 0
            ElseIf intEstadoAlerta = 0 Then
                objAlertas.iEstadoAlerta = 1
            End If

            objAlertas.iAlertaId = intCodAlerta
            objAlertas.vUsuarioModificacion = Context.User.Identity.Name

            objAlertasBL.ChangeStatus(objAlertas)

            BindDG(intTipoAlerta, txtNombreAlerta.Text)

        Catch ex1 As HandledException
            pnlQueryAlertas.Visible = False
            pnlMensaje.Visible = True

            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlQueryAlertas.Visible = False
            pnlMensaje.Visible = True

            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        ' Response.Redirect(Request.ApplicationPath + "/default.aspx", True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/default.aspx"), True)
    End Sub
End Class
