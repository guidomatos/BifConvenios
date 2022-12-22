Imports System.Data
Imports System.Data.SqlClient
Imports BIFConvenios.DataBase
Imports BIFConvenios.DO
Imports BIFUtils

Imports BIFConvenios.Entidad
Imports BIFConvenios.BE
Imports BIFConvenios.Logica
Imports BIFConvenios.BL
Imports Resource

Namespace BIFConvenios

    Partial Class EditarCorreoCoordinadorCliente
        Inherits System.Web.UI.Page

        Private oCliente As New Cliente()

        Protected objCoordinadorCliente As New BIFConvenios.Entidad.clsCoordinadorCliente()
        Protected objCliente As New BIFConvenios.BE.clsCliente()
        Protected objEventoSistemaBE As New clsEventoSistema()

        Protected objCoordinadorClienteBL As New BIFConvenios.Logica.clsCoordinadorClienteBL()
        Protected objClienteBL As New BIFConvenios.BL.clsClienteBL()

        Protected objEventoSistema As New clsEventoSistemaBL()

        Protected dtCoordinadores As New DataTable()
        Protected intCodCliente As Integer
        Protected strNombreCliente As String



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

#Region "Metodos"

        Private Sub BindDG()
            Try
                dtCoordinadores = objCoordinadorClienteBL.ObtieneCoordinadorClientePorCriterio(0, intCodCliente, 1)

                gvCoordinadores.DataSource = dtCoordinadores
                gvCoordinadores.DataBind()

            Catch ex1 As HandledException
                pnlMensaje.Visible = True
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                pnlMensaje.Visible = True
                lblMensaje.Text = "Error: " + ex2.Message
            End Try
        End Sub



        Private Sub LimpiarControles(ByVal pboolHabilitar As Boolean)
            txtEmpresa.Text = ""
            txtNombreCoordinador.Text = ""
            txtNombreCoordinador.Enabled = pboolHabilitar

            txtCorreoElectronico.Text = ""
            txtCorreoElectronico.Enabled = pboolHabilitar

            txtCargo.Text = ""
            txtCargo.Enabled = pboolHabilitar

            txtTipoPlanilla.Text = ""
            txtTipoPlanilla.Enabled = pboolHabilitar

            txtTelefono.Text = ""
            txtTelefono.Enabled = pboolHabilitar

            txtAnexo.Text = ""
            txtAnexo.Enabled = pboolHabilitar

            txtCelular.Text = ""
            txtCelular.Enabled = pboolHabilitar
        End Sub

        Private Function ValidarControles() As String
            Dim strMensaje As String = ""

            If txtEmpresa.Text = "" Or Len(txtEmpresa.Text) = 0 Then
                strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Empresa")
                Return strMensaje
            ElseIf txtNombreCoordinador.Text = "" Or Len(txtNombreCoordinador.Text) = 0 Then
                strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Nombre Coordinador")
                Return strMensaje
            ElseIf txtCorreoElectronico.Text = "" Or Len(txtCorreoElectronico.Text) = 0 Then
                strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Correo Electrónico")
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

                dt = objCoordinadorClienteBL.ObtieneCoordinadorClientePorCriterio(Convert.ToInt32(hdCodCoordinador.Value), Convert.ToInt32(hdCodCliente.Value), -1)

                txtEmpresa.Text = dt.Rows(0)("Nombre_Cliente").ToString()

                txtNombreCoordinador.Text = dt.Rows(0)("vNombreCoordinador").ToString()
                txtNombreCoordinador.Enabled = True

                txtCorreoElectronico.Text = dt.Rows(0)("vEmailCoordinador").ToString()
                txtCorreoElectronico.Enabled = True

                txtCargo.Text = dt.Rows(0)("vCargo").ToString()
                txtCargo.Enabled = True

                txtTipoPlanilla.Text = dt.Rows(0)("vTipoPlanilla").ToString()
                txtTipoPlanilla.Enabled = True

                txtTelefono.Text = dt.Rows(0)("vTelefono").ToString()
                txtTelefono.Enabled = True

                txtAnexo.Text = dt.Rows(0)("vAnexo").ToString()
                txtAnexo.Enabled = True

                txtCelular.Text = dt.Rows(0)("vCelular").ToString()
                txtCelular.Enabled = True

                btnNuevo.Visible = False
                btnRegresar.Visible = False
                btnGuardar.Visible = True
                btnCancelar.Visible = True

                txtNombreCoordinador.Focus()
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
                    objCoordinadorCliente.NombreCoordinador = txtNombreCoordinador.Text
                    objCoordinadorCliente.EmailCoordinador = txtCorreoElectronico.Text
                    objCoordinadorCliente.Telefono = txtTelefono.Text
                    objCoordinadorCliente.Anexo = txtAnexo.Text
                    objCoordinadorCliente.Celular = txtCelular.Text
                    objCoordinadorCliente.Cargo = txtCargo.Text
                    objCoordinadorCliente.TipoPlanilla = txtTipoPlanilla.Text
                    objCoordinadorCliente.EstadoCoordinador = 1
                    objCoordinadorCliente.Estado = True
                    objCoordinadorCliente.UsuarioCreacion = Context.User.Identity.Name.ToString()

                    If hdTipoGuardar.Value = 1 Then
                        objCoordinadorCliente.CodigoCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)

                        objCoordinadorClienteBL.InsertCoordinador(objCoordinadorCliente)

                        objEventoSistemaBE.Hilo = "BIFConvenios"
                        objEventoSistemaBE.Nivel = BIFUtils.Log.Level.Info
                        objEventoSistemaBE.Accion = "InsertarCoordinadorCliente"
                        Dim pMensajeInsert() As String = {"Inicio del Metodo - Parametros: CodigoCliente=", objCoordinadorCliente.CodigoCliente, ", NombreCoordinador=", objCoordinadorCliente.NombreCoordinador, ", EmailCoordinador=", objCoordinadorCliente.EmailCoordinador, ", Telefono=", objCoordinadorCliente.Telefono, ", Anexo=", objCoordinadorCliente.Anexo, ", Celular=", objCoordinadorCliente.Celular, ", Cargo=", objCoordinadorCliente.Cargo, ", TipoPlanilla=", objCoordinadorCliente.TipoPlanilla, ", EstadoCoordinador=", objCoordinadorCliente.EstadoCoordinador, ", UsuarioCreacion=", objCoordinadorCliente.UsuarioCreacion}
                        objEventoSistemaBE.Mensaje = String.Concat(pMensajeInsert)
                        objEventoSistemaBE.Excepcion = ""
                        objEventoSistemaBE.Usuario = Context.User.Identity.Name.ToString()
                        objEventoSistema.Insertar(objEventoSistemaBE)
                        pnlMensaje.Visible = True
                        lblMensaje.Text = clsMensajesGeneric.RegistroExitoso.Replace("&1", "Coordinador")
                    ElseIf hdTipoGuardar.Value = 2 Then
                        objCoordinadorCliente.CodigoCliente = Convert.ToInt32(hdCodCliente.Value)
                        objCoordinadorCliente.CodigoCoordinador = Convert.ToInt32(hdCodCoordinador.Value)
                        objCoordinadorCliente.UsuarioModificacion = Context.User.Identity.Name.ToString()
                        objCoordinadorClienteBL.UpdateCoordinador(objCoordinadorCliente)
                        objEventoSistemaBE.Hilo = "BIFConvenios"
                        objEventoSistemaBE.Nivel = BIFUtils.Log.Level.Info
                        objEventoSistemaBE.Accion = "ActualizarCoordinadorCliente"
                        Dim pMensajeUpdate() As String = {"Inicio del Metodo - Parametros: CodigoCliente=", objCoordinadorCliente.CodigoCliente, ", CodigoCoordinador=", objCoordinadorCliente.CodigoCoordinador, ", NombreCoordinador=", objCoordinadorCliente.NombreCoordinador, ", EmailCoordinador=", objCoordinadorCliente.EmailCoordinador, ", Telefono=", objCoordinadorCliente.Telefono, ", Anexo=", objCoordinadorCliente.Anexo, ", Celular=", objCoordinadorCliente.Celular, ", Cargo=", objCoordinadorCliente.Cargo, ", TipoPlanilla=", objCoordinadorCliente.TipoPlanilla, ", EstadoCoordinador=", objCoordinadorCliente.EstadoCoordinador, ", UsuarioModificacion=", objCoordinadorCliente.UsuarioModificacion}
                        objEventoSistemaBE.Mensaje = String.Concat(pMensajeUpdate)
                        objEventoSistemaBE.Excepcion = ""
                        objEventoSistemaBE.Usuario = Context.User.Identity.Name.ToString()
                        objEventoSistema.Insertar(objEventoSistemaBE)
                        pnlMensaje.Visible = True
                        lblMensaje.Text = clsMensajesGeneric.ActualizadoExitoso.Replace("&1", "Coordinador")
                    End If

                    btnNuevo.Visible = True
                    btnRegresar.Visible = True
                    btnGuardar.Visible = False
                    btnCancelar.Visible = False

                    intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
                    BindDG()
                    LimpiarControles(False)

                Else
                    Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.ErrorValidation, strMensaje)
                End If
            Catch ex1 As HandledException
                pnlMensaje.Visible = True
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

                intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
                BindDG()

            Catch ex2 As Exception
                pnlMensaje.Visible = True
                lblMensaje.Text = "Error: " + ex2.Message

                intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
                BindDG()

            End Try
        End Sub

        Private Sub HabilitarNuevo()
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            hdTipoGuardar.Value = 1

            Try
                intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
                strNombreCliente = objClienteBL.ObtenerClientePorCodigo(intCodCliente).Rows(0)("Nombre_Cliente").ToString()

                LimpiarControles(True)

                txtEmpresa.Text = strNombreCliente

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

            btnNuevo.Visible = True
            btnRegresar.Visible = True
            btnGuardar.Visible = False
            btnCancelar.Visible = False

            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)

            BindDG()
        End Sub
#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                If Not Request.Params("id") Is Nothing Then
                    pnlMensaje.Visible = False
                    lblMensaje.Text = ""

                    hdTipoGuardar.Value = 0

                    intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)

                    BindDG()
                End If
            End If
        End Sub

        Protected Sub lnkEditar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEditar.Click
            HabilitarEdicion()

            intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
            BindDG()
        End Sub

        Protected Sub lnkEliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkEliminar.Click
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            Try
                objCoordinadorCliente.CodigoCliente = Convert.ToInt32(hdCodCliente.Value)
                objCoordinadorCliente.CodigoCoordinador = Convert.ToInt32(hdCodCoordinador.Value)
                objCoordinadorCliente.EstadoCoordinador = 0
                objCoordinadorCliente.EmailCoordinador = hdEmailCoordinador.Value.Trim()

                objCoordinadorClienteBL.ChangeStatusCoordinador(objCoordinadorCliente)
                objEventoSistemaBE.Hilo = "BIFConvenios"
                objEventoSistemaBE.Nivel = BIFUtils.Log.Level.Info
                objEventoSistemaBE.Accion = "EliminarCoordinadorCliente"

                Dim pMensajeDelete() As String = {"Inicio del Metodo - Parametros: CodigoCliente=", objCoordinadorCliente.CodigoCliente, " ,CodigoCoordinador=", objCoordinadorCliente.CodigoCoordinador, ", EmailCoordinador=", objCoordinadorCliente.EmailCoordinador, ", EstadoCoordinador=", objCoordinadorCliente.EstadoCoordinador}
                objEventoSistemaBE.Mensaje = String.Concat(pMensajeDelete)
                objEventoSistemaBE.Excepcion = ""
                objEventoSistemaBE.Usuario = Context.User.Identity.Name.ToString()
                objEventoSistema.Insertar(objEventoSistemaBE)

                HabilitarInicio()

                pnlMensaje.Visible = True
                lblMensaje.Text = clsMensajesGeneric.BorradoExitoso.Replace("&1", "Coordinador")

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

            intCodCliente = IIf(Not Request.Params("id") Is Nothing, Request.Params("id"), 0)
            BindDG()
        End Sub

        Protected Sub btnGuardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
            HabilitarGuardar()
        End Sub

        Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
            HabilitarInicio()
        End Sub

        Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
            Response.Redirect(Request.ApplicationPath + "/clientes/clientes.aspx", True)
        End Sub
    End Class
End Namespace

