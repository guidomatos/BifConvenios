Imports System.Data
Imports BIFConvenios
Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Alertas_frmEditarAlertas
    Inherits System.Web.UI.Page

    Protected objAlertas As New clsAlertas
    Protected objSystemParameters As New clsSystemParameters

    Protected objAlertasBL As New clsAlertasBL
    Protected objSystemParametersBL As New clsSystemParametersBL

    Protected dtAlertas As New DataTable()
    Protected dtSystemParameters As New DataTable()

#Region "Metodos"

    Private Sub LlenarCombos()
        lblMensaje.Text = ""

        Try
            dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoMetadata).ToString())

            ddlMetadataAsunto.DataSource = dtSystemParameters
            ddlMetadataAsunto.DataBind()
            ddlMetadataAsunto.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

            ddlMetadataCuerpo.DataSource = dtSystemParameters
            ddlMetadataCuerpo.DataBind()
            ddlMetadataCuerpo.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

            dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoAlerta).ToString())

            ddlTipoAlerta.DataSource = dtSystemParameters
            ddlTipoAlerta.DataBind()
            ddlTipoAlerta.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Function QuitarSaltosLinea(ByVal pstrTexto As String, ByVal pstrCaracter As String) As String
        Dim strTexto As String = ""

        strTexto = Replace(Replace(Replace(pstrTexto, Chr(10), pstrCaracter), Chr(13), pstrCaracter), Chr(39), pstrCaracter)

        Return strTexto
    End Function

    Private Sub HabilitarNuevo()
        lblMensaje.Text = ""

        hdTipoGuardar.Value = 1
    End Sub

    Private Sub HabilitarEdicion()
        lblMensaje.Text = ""

        hdTipoGuardar.Value = 2

        Try
            Dim dt As New DataTable()

            hdCodAlerta.Value = Request.Params("id").ToString()

            objAlertas.iAlertaId = Convert.ToInt32(hdCodAlerta.Value)
            objAlertas.iTipoAlerta = 0
            objAlertas.vNombreAlerta = ""
            objAlertas.iEstadoAlerta = -1

            dt = objAlertasBL.ObtieneAlertasPorCriterio(objAlertas)

            ddlTipoAlerta.SelectedIndex = ddlTipoAlerta.Items.IndexOf(ddlTipoAlerta.Items.FindByValue(dt.Rows(0)("iTipoAlerta")))
            txtNombreAlerta.Text = dt.Rows(0)("vNombreAlerta").ToString()
            txtDescripcion.Text = dt.Rows(0)("vDescripcionAlerta").ToString()
            txtAsunto.Text = dt.Rows(0)("vAsuntoMensaje").ToString()
            txtCuerpo.Text = dt.Rows(0)("vCuerpoMensaje").ToString()

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Function ValidarObjetoAlerta() As String
        Dim strMensaje As String = ""

        If ddlTipoAlerta.Text = "0" Then
            strMensaje = clsMensajesGeneric.Seleccionar.Replace("&1", "Tipo de Alerta")
            Return strMensaje
        End If

        If txtNombreAlerta.Text = "" Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Nombre Alerta")
            Return strMensaje
        End If

        If txtAsunto.Text = "" Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Asunto Alerta")
            Return strMensaje
        End If

        If txtCuerpo.Text = "" Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Cuerpo Alerta")
            Return strMensaje
        End If

        Return strMensaje
    End Function

    Private Sub HabilitarGuardar(ByVal pstrCadena As String)
        Dim strMensaje As String = ""

        lblMensaje.Text = ""

        Try
            Dim strValores As String() = pstrCadena.Split("|")

            If Convert.ToInt32(hdTipoGuardar.Value) = 2 Then
                objAlertas.iAlertaId = Convert.ToInt32(Request.Params("id").ToString())
            End If

            objAlertas.iTipoAlerta = strValores(0).ToString()
            objAlertas.vNombreAlerta = strValores(1).ToString()
            objAlertas.vDescripcionAlerta = strValores(2).ToString()
            objAlertas.vAsuntoMensaje = strValores(3).ToString()

            If strValores(4).ToString().Length < 5000 Then
                objAlertas.vCuerpoMensaje = strValores(4).ToString()
            Else
                objAlertas.vCuerpoMensaje = strValores(4).ToString().Substring(0, 5000)
            End If

            objAlertas.iEstadoAlerta = Convert.ToInt32(strValores(5).ToString())

            If hdTipoGuardar.Value = 1 Then
                objAlertas.vUsuarioCreacion = Context.User.Identity.Name.ToString()

                objAlertasBL.Insert(objAlertas)
            ElseIf hdTipoGuardar.Value = 2 Then
                objAlertas.vUsuarioModificacion = Context.User.Identity.Name.ToString()

                objAlertasBL.Update(objAlertas)
            End If

            ' Response.Redirect(Request.ApplicationPath + "/Alertas/frmAlertas.aspx", True)
            Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmAlertas.aspx"), True)
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Sub LimpiarControles()
        txtNombreAlerta.Text = ""
        txtDescripcion.Text = ""
        txtAsunto.Text = ""
        txtCuerpo.Text = ""
    End Sub

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LimpiarControles()
            LlenarCombos()

            If Not Request.Params("id") Is Nothing Then
                HabilitarEdicion()
            Else
                HabilitarNuevo()
            End If
        End If

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Response.Redirect(Request.ApplicationPath + "/Alertas/frmAlertas.aspx", True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmAlertas.aspx"), True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim strMensajeValidar As String = ValidarObjetoAlerta()

        Try
            If strMensajeValidar = "" Then
                Dim strCadena As String = ddlTipoAlerta.Text + "|" + txtNombreAlerta.Text + "|" + txtDescripcion.Text + "|" + txtAsunto.Text + "|" + QuitarSaltosLinea(txtCuerpo.Text, " ") + "|" + "1"
                strCadena = "ValidarGuardar('" + strCadena + "');"

                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "ValidarGuardar", strCadena, True)
            Else
                Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.ErrorValidation, strMensajeValidar)
            End If
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub

    Protected Sub lnkGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGuardar.Click
        Dim strGuardar As String = hdGuardar.Value

        HabilitarGuardar(strGuardar)
    End Sub

    Protected Sub btnAgregar1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar1.Click
        Dim strCadena As String = txtAsunto.Text

        If strCadena = "" And ddlMetadataAsunto.Text <> "0" Then
            strCadena = ddlMetadataAsunto.Text

            txtAsunto.Text = strCadena
        Else
            If ddlMetadataAsunto.Text <> "0" Then
                strCadena = strCadena + " " + ddlMetadataAsunto.Text

                txtAsunto.Text = strCadena
            End If
        End If
    End Sub

    Protected Sub btnAgregar2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar2.Click
        Dim strCadena As String = txtCuerpo.Text

        If strCadena = "" And ddlMetadataCuerpo.Text <> "0" Then
            strCadena = ddlMetadataCuerpo.Text

            txtCuerpo.Text = strCadena
        Else
            If ddlMetadataCuerpo.Text <> "0" Then
                strCadena = strCadena + " " + ddlMetadataCuerpo.Text

                txtCuerpo.Text = strCadena
            End If
        End If
    End Sub
End Class
