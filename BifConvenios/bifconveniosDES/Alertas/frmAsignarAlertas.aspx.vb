Imports System.Data
Imports BIFConvenios
Imports BIFConvenios.BE
Imports BIFConvenios.BL

Imports Resource

Partial Class Alertas_frmAsignarAlertas
    Inherits System.Web.UI.Page

    Protected objAlertasClientes As New clsAlertasClientes
    Protected objCliente As New clsCliente

    Protected objAlertasClientesBL As New clsAlertasClientesBL
    Protected objClienteBL As New clsClienteBL

    Protected dtAlertasClientes As New DataTable()

    Protected objWSAlertasAutomaticas As New wsAlertasAutomaticas.WsAlertasAutomaticasClient

    Protected strCodigoCliente As String = ""

#Region "Metodos"
    Private Sub BindDG(ByVal pintClienteId As Integer)
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            objAlertasClientes.iAlertaClienteId = 0
            objAlertasClientes.iAlertaId = 0
            objAlertasClientes.iClienteId = pintClienteId
            objAlertasClientes.iEstado = 1

            pnlQueryAlertasClientes.Visible = True

            gvAlertasClientes.DataSource = objAlertasClientesBL.ObtieneAlertasClientesPorCriterio(objAlertasClientes)
            gvAlertasClientes.DataBind()
        Catch ex1 As HandledException
            pnlQueryAlertasClientes.Visible = False

            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlQueryAlertasClientes.Visible = False

            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Function CargarCliente(ByVal pintCodCliente As Integer) As String
        Dim strMensaje As String = ""
        Dim dtCliente As New DataTable()

        Try
            dtCliente = objClienteBL.ObtenerClientePorCodigo(pintCodCliente)

            txtCodEmpresa.Text = dtCliente.Rows(0)("Codigo_Cliente").ToString()
            txtNomEmpresa.Text = dtCliente.Rows(0)("Nombre_Cliente").ToString()

            btnNuevo.Visible = True
            btnEnviarAlertas.Visible = True

            Return strMensaje
        Catch ex1 As HandledException
            Throw ex1
        Catch ex2 As Exception
            Throw ex2
        End Try

    End Function
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If String.IsNullOrEmpty(hdCodCliente.Value) And Not Request.Params("id") Is Nothing Then
            hdCodCliente.Value = Request.Params("id").ToString()

            CargarCliente(Convert.ToInt32(Request.Params("id").ToString()))
            BindDG(Convert.ToInt32(Request.Params("id").ToString()))
        ElseIf Not String.IsNullOrEmpty(hdCodCliente.Value) Then
            CargarCliente(Convert.ToInt32(hdCodCliente.Value))
            BindDG(Convert.ToInt32(hdCodCliente.Value))
        End If
    End Sub

    Protected Sub lnkEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEliminar.Click
        Try
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            Dim intCodAlertaCliente As Integer = Convert.ToInt32(hdAlertaClienteId.Value)

            objAlertasClientes.iAlertaClienteId = intCodAlertaCliente
            objAlertasClientes.iEstado = 0
            objAlertasClientes.vUsuarioModificacion = Context.User.Identity.Name.ToString()

            objAlertasClientesBL.ChangeStatus(objAlertasClientes)

            pnlMensaje.Visible = True
            lblMensaje.Text = "Registro eliminado correctamente"

            BindDG(Convert.ToInt32(hdCodCliente.Value))
        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        'Response.Redirect(Request.ApplicationPath + "/Alertas/frmEditarAlertaCliente.aspx?id=0&idCliente=" + hdCodCliente.Value, True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmEditarAlertaCliente.aspx") + "?id=0&idCliente=" + hdCodCliente.Value, True)
    End Sub

    Protected Sub lnkCargarCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCargarCliente.Click
        Dim intCodCliente As Integer = Convert.ToInt32(hdCodCliente.Value)
        Dim strMensaje As String = ""

        Try
            strMensaje = CargarCliente(intCodCliente)

            If strMensaje <> "" Then
                Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoExisteRegistro, strMensaje)
            Else
                BindDG(intCodCliente)
            End If

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        ' Response.Redirect(Request.ApplicationPath + "/default.aspx", True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/default.aspx"), True)
    End Sub

    Protected Sub btnEnviarAlertas_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarAlertas.Click
        btnEnviarAlertas.Enabled = False
        btnBuscar.Enabled = False

        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Dim strMensajeEnvio As String = ""
        Dim strValor As String = ""

        Try
            strMensajeEnvio = "" 'objWSAlertasAutomaticas.ProcesarAlerta(hdCodCliente.Value, Context.User.Identity.Name.ToString())

            BindDG(Convert.ToInt32(hdCodCliente.Value))

            pnlMensaje.Visible = True
            lblMensaje.Text = strMensajeEnvio

            btnEnviarAlertas.Enabled = True
            btnBuscar.Enabled = True

        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub
End Class
