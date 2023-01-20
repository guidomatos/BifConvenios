Imports System.Data
Imports System.Data.SqlClient

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource
Partial Class Clientes_frmResponsableOficina
    Inherits System.Web.UI.Page

    Protected objResponsableOficina As New clsReponsableOficina()
    Protected objCliente As New clsCliente()

    Protected objResponsableOficinaBL As New clsResponsableOficinaBL()
    Protected objClienteBL As New clsClienteBL()

    Protected dtResponsable As New DataTable()
    
#Region "Metodos"

    Private Sub BindDG()
        Try
            objResponsableOficina.iResponsableId = 0
            objResponsableOficina.vOficina = ""
            objResponsableOficina.vNombreResponsable = ""

            dtResponsable = objResponsableOficinaBL.ObtenerResponsableOficinaPorCriterio(objResponsableOficina)

            gvResponsables.DataSource = dtResponsable
            gvResponsables.DataBind()

        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Sub LimpiarControles(ByVal pboolHabilitar As Boolean)
        txtCodOficina.Text = ""
        txtNomOficina.Text = ""

        txtNombreResponsable.Text = ""
        txtNombreResponsable.Enabled = pboolHabilitar

        txtCorreoResponsable.Text = ""
        txtCorreoResponsable.Enabled = pboolHabilitar

    End Sub

    Private Function ValidarControles() As String
        Dim strMensaje As String = ""
        Dim intValida As Integer

        If Convert.ToInt32(hdTipoGuardar.Value) = 1 Then
            'Try
            '    Dim _dtOficina As New DataTable()

            '    objResponsableOficina.iResponsableId = 0
            '    objResponsableOficina.vOficina = txtNomOficina.Text
            '    objResponsableOficina.vNombreResponsable = ""

            '    _dtOficina = objResponsableOficinaBL.ObtenerResponsableOficinaPorCriterio(objResponsableOficina)

            '    intValida = _dtOficina.Rows.Count
            'Catch ex1 As HandledException
            '    If ex1.ErrorTypeId = -400 Then
            '        intValida = 0
            '    Else
            '        Throw ex1
            '    End If
            'End Try
            intValida = 0
        Else
            intValida = 0
        End If

        If intValida > 0 Then
            strMensaje = clsMensajesGeneric.RegistroYaAsignado.Replace("&1", "La Oficina").Replace("&2", " un Responsable")
        ElseIf txtCodOficina.Text = "" Or Len(txtCodOficina.Text) = 0 Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo Oficina")
            Return strMensaje
        ElseIf txtNomOficina.Text = "" Or Len(txtNomOficina.Text) = 0 Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Nombre Oficina")
            Return strMensaje
        ElseIf txtNombreResponsable.Text = "" Or Len(txtNombreResponsable.Text) = 0 Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Nombre Responsable")
            Return strMensaje
        ElseIf txtCorreoResponsable.Text = "" Or Len(txtCorreoResponsable.Text) = 0 Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Correo Responsable")
            Return strMensaje
        End If

        Return strMensaje
    End Function

    Private Sub HabilitarEdicion()
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        hdTipoGuardar.Value = 2

        Try
            Dim dt As New DataTable()

            objResponsableOficina.iResponsableId = Convert.ToInt32(hdResponsableId.Value)

            dtResponsable = objResponsableOficinaBL.ObtenerResponsableOficinaPorCriterio(objResponsableOficina)

            txtCodOficina.Text = dtResponsable.Rows(0)("iOficinaId").ToString()
            txtNomOficina.Text = dtResponsable.Rows(0)("vOficina").ToString()

            txtNombreResponsable.Text = dtResponsable.Rows(0)("vNombreResponsable").ToString()
            txtNombreResponsable.Enabled = True

            txtCorreoResponsable.Text = dtResponsable.Rows(0)("vCorreoResponsable").ToString()
            txtCorreoResponsable.Enabled = True

            btnSearch.Enabled = False

            btnNuevo.Visible = False
            btnRegresar.Visible = False
            btnGuardar.Visible = True
            btnCancelar.Visible = False

            txtNombreResponsable.Focus()
        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message

        End Try
    End Sub

    Private Sub HabilitarGuardar()
        Dim strMensaje As String = ""

        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            strMensaje = ValidarControles()

            If strMensaje = "" Or Len(strMensaje) = 0 Then
                objResponsableOficina.iOficinaId = txtCodOficina.Text
                objResponsableOficina.vOficina = txtNomOficina.Text
                objResponsableOficina.vNombreResponsable = txtNombreResponsable.Text
                objResponsableOficina.vCorreoResponsable = txtCorreoResponsable.Text
                objResponsableOficina.vUsuarioCreacion = Context.User.Identity.Name.ToString()

                If hdTipoGuardar.Value = 1 Then
                    objResponsableOficinaBL.Insert(objResponsableOficina)

                    pnlMensaje.Visible = True
                    lblMensaje.Text = clsMensajesGeneric.RegistroExitoso.Replace("&1", "Responsable")
                ElseIf hdTipoGuardar.Value = 2 Then
                    objResponsableOficina.iResponsableId = Convert.ToInt32(hdResponsableId.Value)

                    objResponsableOficinaBL.Update(objResponsableOficina)

                    pnlMensaje.Visible = True
                    lblMensaje.Text = clsMensajesGeneric.ActualizadoExitoso.Replace("&1", "Responsable")
                End If

                btnSearch.Enabled = False

                btnNuevo.Visible = True
                btnRegresar.Visible = True
                btnGuardar.Visible = False
                btnCancelar.Visible = False

                BindDG()
                LimpiarControles(False)

            Else
                Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.ErrorValidation, strMensaje)

            End If
        Catch ex1 As HandledException
            BindDG()

            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            BindDG()

            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Sub HabilitarNuevo()
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        hdTipoGuardar.Value = 1

        Try
            LimpiarControles(True)

            btnSearch.Enabled = True

            btnNuevo.Visible = False
            btnRegresar.Visible = False
            btnGuardar.Visible = True
            btnCancelar.Visible = True

        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message

        End Try
    End Sub

    Private Sub HabilitarInicio()
        LimpiarControles(False)

        hdTipoGuardar.Value = 0

        btnSearch.Enabled = False

        btnNuevo.Visible = True
        btnRegresar.Visible = True
        btnGuardar.Visible = False
        btnCancelar.Visible = False

        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        BindDG()
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            HabilitarInicio()

            BindDG()
        End If

        If hdId.Value.Length > 0 Then
            lnkCargarOficinaIBS_Click(Nothing, Nothing)
        End If
    End Sub

    Protected Sub lnkEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEditar.Click
        HabilitarEdicion()

        BindDG()
    End Sub

    Protected Sub lnkEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEliminar.Click
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            objResponsableOficina.iResponsableId = Convert.ToInt32(hdResponsableId.Value)

            objResponsableOficinaBL.ChangeStatus(objResponsableOficina)

            HabilitarInicio()

            pnlMensaje.Visible = True
            lblMensaje.Text = clsMensajesGeneric.BorradoExitoso.Replace("&1", "Responsable")

        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message

        End Try

    End Sub

    Protected Sub btnNuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        HabilitarNuevo()

        BindDG()
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        HabilitarGuardar()
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        HabilitarInicio()
    End Sub

    Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
        Response.Redirect(Request.ApplicationPath + "/default.aspx", True)
    End Sub

    Protected Sub lnkCargarOficinaIBS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCargarOficinaIBS.Click
        Dim intCodigoOficina As Integer = Convert.ToInt32(hdId.Value.ToString())

        Dim _dtOficina As New DataTable()

        Try
            _dtOficina = objResponsableOficinaBL.ObtenerOficinaDesdeAS400PorCriterio(1, intCodigoOficina)

            txtCodOficina.Text = intCodigoOficina
            txtNomOficina.Text = _dtOficina.Rows(0)("BRNNME").ToString()

            BindDG()
        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message

        End Try

    End Sub

    Protected Sub gvResponsables_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvResponsables.PageIndexChanging
        gvResponsables.PageIndex = e.NewPageIndex

        BindDG()
    End Sub

End Class
