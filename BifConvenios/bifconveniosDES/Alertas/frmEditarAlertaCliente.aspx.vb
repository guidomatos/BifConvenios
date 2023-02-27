Imports System.Data
Imports BIFConvenios
Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Alertas_frmEditarAlertaCliente
    Inherits System.Web.UI.Page

    Protected objAlertas As New clsAlertas
    Protected objAlertasClientes As New clsAlertasClientes

    Protected objClienteBL As New clsClienteBL
    Protected objAlertasBL As New clsAlertasBL
    Protected objAlertasClientesBL As New clsAlertasClientesBL

#Region "Metodos"

    Private Sub LimpiarControles()
        txtCodAlerta.Text = ""
        txtNomAlerta.Text = ""
        txtAsuntoAlerta.Text = ""
        txtCuerpoAlerta.Text = ""
        txtDiasAntes.Text = ""
        txtDiasDespues.Text = ""
        chkAdjunto.Checked = False
    End Sub

    Private Function CargarCliente(ByVal pintCodCliente As Integer) As String
        Dim strMensaje As String = ""
        Dim dtCliente As New DataTable()

        Try
            dtCliente = objClienteBL.ObtenerClientePorCodigo(pintCodCliente)

            txtCodEmpresa.Text = dtCliente.Rows(0)("Codigo_Cliente").ToString()
            txtNomEmpresa.Text = dtCliente.Rows(0)("Nombre_Cliente").ToString()

            Return strMensaje
        Catch ex1 As HandledException
            Throw ex1
        Catch ex2 As Exception
            Throw ex2
        End Try

    End Function

    Private Function CargarAlerta(ByVal pstrCodAlerta As String) As String
        Dim strMensaje As String = ""
        Dim dtAlertas As New DataTable()

        Try
            objAlertas.iAlertaId = Convert.ToInt32(pstrCodAlerta)
            objAlertas.iTipoAlerta = 0
            objAlertas.vNombreAlerta = ""
            objAlertas.iEstadoAlerta = -1

            dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlertas)

            hdTipoAlerta.Value = dtAlertas.Rows(0)("iTipoAlerta").ToString()

            txtCodAlerta.Text = dtAlertas.Rows(0)("iAlertaId").ToString()
            txtNomAlerta.Text = dtAlertas.Rows(0)("vNombreAlerta").ToString()
            txtAsuntoAlerta.Text = dtAlertas.Rows(0)("vAsuntoMensaje").ToString()
            txtCuerpoAlerta.Text = dtAlertas.Rows(0)("vCuerpoMensaje").ToString()

            Return strMensaje
        Catch ex1 As HandledException
            Throw ex1
        Catch ex2 As Exception
            Throw ex2
        End Try
    End Function

    Private Sub HabilitarNuevo()
        lblMensaje.Text = ""
        Dim strMensaje As String = ""
        Dim intCodCliente As Integer = Convert.ToInt32(Request.Params("idCliente").ToString())

        Try
            strMensaje = CargarCliente(intCodCliente)

            If strMensaje <> "" Then
                Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoExisteRegistro, strMensaje)
            End If

            hdCodCliente.Value = Convert.ToInt32(Request.Params("idCliente").ToString())
            hdTipoAlerta.Value = 0
            hdTipoGuardar.Value = 1

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub

    Private Sub HabilitarEdicion()
        lblMensaje.Text = ""

        hdTipoGuardar.Value = 2

        Try
            Dim dt1, dt2 As New DataTable()

            hdCodAlertaCliente.Value = Request.Params("id").ToString()
            hdCodCliente.Value = Convert.ToInt32(Request.Params("idCliente").ToString())

            objAlertasClientes.iAlertaClienteId = Convert.ToInt32(hdCodAlertaCliente.Value)
            objAlertasClientes.iAlertaId = 0
            objAlertasClientes.iClienteId = 0
            objAlertasClientes.iEstado = -1

            dt1 = objAlertasClientesBL.ObtieneAlertasClientesPorCriterio(objAlertasClientes)

            objAlertas.iAlertaId = Convert.ToInt32(dt1.Rows(0)("iAlertaId").ToString())
            objAlertas.iTipoAlerta = 0
            objAlertas.vNombreAlerta = ""
            objAlertas.iEstadoAlerta = 1

            dt2 = objAlertasBL.ObtieneAlertasPorCriterio(objAlertas)

            txtCodEmpresa.Text = dt1.Rows(0)("iClienteId").ToString()
            txtNomEmpresa.Text = dt1.Rows(0)("Nombre_Cliente").ToString()

            hdTipoAlerta.Value = Convert.ToInt32(dt2.Rows(0)("iTipoAlerta").ToString())

            txtCodAlerta.Text = dt2.Rows(0)("iAlertaId").ToString()
            txtNomAlerta.Text = dt2.Rows(0)("vNombreAlerta").ToString()
            txtAsuntoAlerta.Text = dt2.Rows(0)("vAsuntoMensaje").ToString()
            txtCuerpoAlerta.Text = dt2.Rows(0)("vCuerpoMensaje").ToString()

            txtDiasAntes.Text = dt1.Rows(0)("iDiasAntes").ToString()
            txtDiasDespues.Text = dt1.Rows(0)("iDiasDespues").ToString()
            chkAdjunto.Checked = Convert.ToInt32(dt1.Rows(0)("iAdjunto").ToString())

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Private Function ValidarObjetoAlertaCliente() As String
        Dim dtAlertasClientes As New DataTable()

        Dim strMensaje As String = ""
        Dim intCantidad As Integer = 0

        lblMensaje.Text = ""

        If txtCodEmpresa.Text = "" Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo Empresa")
            Return strMensaje
        End If

        Try
            objAlertasClientes.iAlertaClienteId = 0
            objAlertasClientes.iAlertaId = 0
            objAlertasClientes.iClienteId = Convert.ToInt32(hdCodCliente.Value)
            objAlertasClientes.iEstado = 1
            dtAlertasClientes = objAlertasClientesBL.ObtieneAlertasClientesPorCriterio(objAlertasClientes)

            If txtCodAlerta.Text = "" Then
                strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo Alerta")
                Return strMensaje
            Else
                If hdTipoGuardar.Value = 1 Then
                    For Each drw As DataRow In dtAlertasClientes.Rows
                        If Convert.ToInt32(dtAlertasClientes.Rows(0)("iAlertaId").ToString()) = Convert.ToInt32(hdCodAlerta.Value) Then
                            strMensaje = clsMensajesGeneric.RegistroYaAsignado.Replace("&1", "La Alerta").Replace("&2", " El cliente")
                            Return strMensaje
                        End If
                    Next
                End If
            End If
        Catch ex1 As HandledException
            intCantidad = 0
        End Try

        If hdTipoAlerta.Value = 1 Then
            If txtDiasAntes.Text = "" Then
                strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Dias antes")
                Return strMensaje
            End If
        End If

        Return strMensaje
    End Function

    Private Sub HabilitarGuardar(ByVal pstrCadena As String)
        Dim strMensaje As String = ""

        lblMensaje.Text = ""

        Try
            Dim strValores As String() = pstrCadena.Split("|")

            If Convert.ToInt32(hdTipoGuardar.Value) = 2 Then
                objAlertasClientes.iAlertaClienteId = Convert.ToInt32(Request.Params("id").ToString())
            End If

            objAlertasClientes.iAlertaId = Convert.ToInt32(strValores(0).ToString())
            objAlertasClientes.iClienteId = Convert.ToInt32(strValores(1).ToString())

            If Len(strValores(2).ToString()) = 0 Then
                objAlertasClientes.iDiasAntes = 0
            Else
                objAlertasClientes.iDiasAntes = Convert.ToInt32(strValores(2).ToString())
            End If

            If Len(strValores(3).ToString()) = 0 Then
                objAlertasClientes.iDiasDespues = 0
            Else
                objAlertasClientes.iDiasDespues = Convert.ToInt32(strValores(3).ToString())
            End If

            objAlertasClientes.iAdjunto = IIf(strValores(4).ToString() = "TRUE", 1, 0)
            objAlertasClientes.iEstado = Convert.ToInt32(strValores(5).ToString())

            If Convert.ToInt32(hdTipoGuardar.Value) = 1 Then
                objAlertasClientes.vUsuarioCreacion = Context.User.Identity.Name.ToString()

                objAlertasClientesBL.Insert(objAlertasClientes)
            ElseIf Convert.ToInt32(hdTipoGuardar.Value) = 2 Then
                objAlertasClientes.vUsuarioModificacion = Context.User.Identity.Name.ToString()

                objAlertasClientesBL.Update(objAlertasClientes)
            End If

            ' Response.Redirect(Request.ApplicationPath + "/Alertas/frmAsignarAlertas.aspx?id=" + objAlertasClientes.iClienteId.ToString())
            Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmAsignarAlertas.aspx") + "?id=" + objAlertasClientes.iClienteId.ToString())
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub
#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LimpiarControles()

            If Convert.ToInt32(Request.Params("id")) = 0 Then
                HabilitarNuevo()
            Else
                HabilitarEdicion()
            End If
        End If

        If Not String.IsNullOrEmpty(hdCodAlerta.Value) Then
            lnkCargarAlerta_Click(Nothing, Nothing)
        End If
    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        'Response.Redirect(Request.ApplicationPath + "/Alertas/frmAsignarAlertas.aspx?id=" + hdCodCliente.Value, True)
        Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Alertas/frmAsignarAlertas.aspx") + "?id=" + hdCodCliente.Value, True)
    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Dim strMensajeValidar As String = ValidarObjetoAlertaCliente()

        Try
            If strMensajeValidar = "" Then
                Dim strCadena As String = txtCodAlerta.Text + "|" + txtCodEmpresa.Text + "|" + txtDiasAntes.Text + "|" + txtDiasDespues.Text + "|" + UCase(chkAdjunto.Checked.ToString()) + "|" + "1"
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

    Protected Sub lnkCargarAlerta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCargarAlerta.Click
        Dim strCodAlerta As String = hdCodAlerta.Value
        Dim strMensaje As String = ""

        Try
            strMensaje = CargarAlerta(strCodAlerta)

            If strMensaje <> "" Then
                Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoExisteRegistro, strMensaje)
            End If

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub
End Class
