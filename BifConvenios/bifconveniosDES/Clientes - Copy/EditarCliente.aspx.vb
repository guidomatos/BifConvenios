Imports System.Data
Imports System.Data.SqlClient

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Namespace BIFConvenios

    Partial Class EditarCliente
        Inherits System.Web.UI.Page

        Protected objCliente As New clsCliente
        Protected objSystemParameters As New clsSystemParameters

        Protected objClienteBL As New clsClienteBL
        Protected objSystemParametersBL As New clsSystemParametersBL

        Protected dtSystemParameters As New DataTable()
        Protected dtCliente As New DataTable()

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

        Private Sub LlenarCombos()
            dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoArchivoEnvio).ToString())

            'Modificado por Christian Rivera. 16-06-2014 Se añade combo de funcionarios de convenios. EA 273-Mejoras convenios
            ddlFuncionarioConvenios.DataSource = BIFConvenios.FuncionarioConvenios.GetFuncionarioConvenios()

            ddlFuncionarioConvenios.DataBind()
            ddlFuncionarioConvenios.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

            ddlEnvioAutListadoDescuentos.Items.Insert(0, New ListItem("Seleccione", "0"))
            ddlEnvioAutListadoDescuentos.Items.Insert(1, New ListItem("Sí", "S"))
            ddlEnvioAutListadoDescuentos.Items.Insert(2, New ListItem("No", "N"))

            ddlTipoArchivo.DataSource = dtSystemParameters
            ddlTipoArchivo.DataBind()
            ddlTipoArchivo.Items.Insert(0, New ListItem("-- Seleccione --", "0"))

        End Sub

        Private Function CargarClienteDesdeIBS(ByVal pstrCodIBS As String) As String
            Dim strMensaje As String = ""
            Dim dtClienteIBS As New DataTable()

            Try
                dtClienteIBS = objClienteBL.ObtenerClienteDesdeAS400PorCodIBS(Convert.ToInt32(pstrCodIBS))

                txtCodigoIBS.Text = dtClienteIBS.Rows(0)("CUSCUN").ToString()
                txtTipoDocumento.Text = objSystemParametersBL.GetNombreParametro(Convert.ToInt32(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoDocumento).ToString()), Convert.ToInt32(IIf(String.IsNullOrEmpty(dtClienteIBS.Rows(0)("TIPODOCUMENTO").ToString()), 0, dtClienteIBS.Rows(0)("TIPODOCUMENTO").ToString())))
                txtNumeroDocumento.Text = dtClienteIBS.Rows(0)("NUMERODOCUMENTO").ToString()
                txtNombreCliente.Text = dtClienteIBS.Rows(0)("NOMEMPRESA").ToString()
                txtCodOficina.Text = dtClienteIBS.Rows(0)("CODOFICINA").ToString()
                txtNomOficina.Text = dtClienteIBS.Rows(0)("NOMOFICINA").ToString()
                txtCodGestor.Text = dtClienteIBS.Rows(0)("CODGESTOR").ToString()
                txtNomGestor.Text = dtClienteIBS.Rows(0)("NOMGESTOR").ToString()

                Return strMensaje
            Catch ex1 As HandledException
                Throw ex1
            Catch ex2 As Exception
                Throw ex2
            End Try

        End Function

        Private Function ValidarObjetoCliente(ByVal pintTipoGuardar As Integer, ByRef pobjCliente As clsCliente) As String
            Dim strMensaje As String = ""

            Try
                pobjCliente.CodigoIBS = Convert.ToInt32(txtCodigoIBS.Text)
                pobjCliente.NombreCliente = Trim(txtNombreCliente.Text)
                pobjCliente.FormatoArchivo = ddlTipoArchivo.Text
                pobjCliente.TipoDocumento = objSystemParametersBL.GetCodigoParametro(ConfigurationManager.AppSettings(clsTiposSystemParameters.TipoDocumento).ToString(), txtTipoDocumento.Text)
                pobjCliente.NumeroDocumento = txtNumeroDocumento.Text
                pobjCliente.CorreoElectronico = ""
                pobjCliente.Telefono1 = txtTelefono1.Text
                pobjCliente.Telefono2 = txtTelefono2.Text
                pobjCliente.Telefono3 = txtTelefono3.Text
                pobjCliente.Telefono4 = ""

                Try
                    pobjCliente.CodigoCliente = objClienteBL.ExisteClienteBifConvenio(pobjCliente.TipoDocumento, pobjCliente.NumeroDocumento).Rows(0)("CodigoCliente")
                Catch ex As Exception
                    pobjCliente.CodigoCliente = 0
                End Try

                If pobjCliente.CodigoCliente > 0 And pintTipoGuardar = 1 Then
                    strMensaje = clsMensajesGeneric.MensajeExisteClienteBifConvenios
                    Return strMensaje
                End If

                If ddlFuncionarioConvenios.Text = "0" Then
                    strMensaje = clsMensajesGeneric.Seleccionar.Replace("&1", "Funcionario del Convenio")
                    Return strMensaje
                Else
                    pobjCliente.IdFuncionario = Convert.ToInt32(ddlFuncionarioConvenios.Text)
                End If

                If ddlTipoArchivo.Text = "0" Then
                    strMensaje = clsMensajesGeneric.Seleccionar.Replace("&1", "Tipo de Archivo")
                    Return strMensaje
                Else
                    pobjCliente.TipoArchivoEnviar = ddlTipoArchivo.Text
                End If

                pobjCliente.CodigoInstitucion = txtCodigoInstitucion.Text

                If ddlEnvioAutListadoDescuentos.Text = "0" Then
                    strMensaje = clsMensajesGeneric.Seleccionar.Replace("&1", "Envio automatico de listado")
                    Return strMensaje
                ElseIf ddlEnvioAutListadoDescuentos.Text = "S" Then
                    pobjCliente.IndEnvioAutomaticoListado = ddlEnvioAutListadoDescuentos.Text

                    If txtDiaEnvioPlanilla.Text = "" Or Len(txtDiaEnvioPlanilla.Text) = 0 Then
                        strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Dia envio planilla")
                        Return strMensaje
                    Else
                        pobjCliente.DiaEnvioPlanilla = txtDiaEnvioPlanilla.Text
                    End If

                    If txtDiaCierrePlanilla.Text = "" Or Len(txtDiaCierrePlanilla.Text) = 0 Then
                        strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Dia cierre planilla")
                        Return strMensaje
                    Else
                        pobjCliente.DiaCierrePlanilla = txtDiaCierrePlanilla.Text
                    End If

                    If txtMesesAnticipoEnvioListado.Text = "" Or Len(txtMesesAnticipoEnvioListado.Text) = 0 Then
                        strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Meses de Anticipación envio de Listado")
                        Return strMensaje
                    Else
                        pobjCliente.MesesAnticipacionEnvioListado = txtMesesAnticipoEnvioListado.Text
                    End If

                    If txtDiaCorte.Text = "" Or Len(txtDiaCorte.Text) = 0 Then
                        strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Dia de corte")
                        Return strMensaje
                    Else
                        pobjCliente.DiaCorte = txtDiaCorte.Text
                    End If
                ElseIf ddlEnvioAutListadoDescuentos.Text = "N" Then
                    pobjCliente.IndEnvioAutomaticoListado = ddlEnvioAutListadoDescuentos.Text
                    pobjCliente.DiaEnvioPlanilla = txtDiaEnvioPlanilla.Text
                    pobjCliente.DiaCierrePlanilla = txtDiaCierrePlanilla.Text
                    pobjCliente.MesesAnticipacionEnvioListado = txtMesesAnticipoEnvioListado.Text
                    pobjCliente.DiaCorte = txtDiaCorte.Text
                End If

                If Len(txtCodOficina.Text) = 0 Then
                    pobjCliente.CodigoOficina = 0
                Else
                    pobjCliente.CodigoOficina = Convert.ToInt32(txtCodOficina.Text)
                End If

                If Len(txtCodGestor.Text) = 0 Then
                    pobjCliente.CodigoGestor = 0
                Else
                    pobjCliente.CodigoGestor = Convert.ToInt32(txtCodGestor.Text)
                End If

                pobjCliente.BloquearCredito = IIf(chkBloquear.Checked, 1, 0)
                pobjCliente.NombreOficina = txtNomOficina.Text
                pobjCliente.NombreGestor = txtNomGestor.Text
                pobjCliente.Estado = 1
                pobjCliente.UsuarioCreacion = Context.User.Identity.Name.ToString()
            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message
            End Try

            Return strMensaje
        End Function

        Private Sub CargarCliente(ByVal pintCodCliente As Integer)
            lblMensaje.Text = ""

            Dim strMensaje As String = ""

            hdTipoGuardar.Value = 2

            Try
                dtCliente = objClienteBL.ObtenerClientePorCodigo(pintCodCliente)

                hdIdCliente.Value = pintCodCliente
                hdIdIBS.Value = dtCliente.Rows(0)("codigo_IBS")

                If hdIdIBS.Value Is Nothing Or hdIdIBS.Value = "" Then
                    Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoRecords, clsMensajesGeneric.RegistroNoExiste.Replace("&1", "La Empresa").Replace("&2", "IBS"))
                Else
                    strMensaje = CargarClienteDesdeIBS(hdIdIBS.Value)

                    If strMensaje = "" Then
                        txtDiaEnvioPlanilla.Text = dtCliente.Rows(0)("dia_envio_planilla").ToString()
                        txtDiaCierrePlanilla.Text = dtCliente.Rows(0)("dia_cierre_planilla").ToString()
                        txtMesesAnticipoEnvioListado.Text = dtCliente.Rows(0)("meses_anticipacion_envio_listado").ToString()
                        txtDiaCorte.Text = dtCliente.Rows(0)("dia_corte").ToString()
                        ddlFuncionarioConvenios.SelectedIndex = ddlFuncionarioConvenios.Items.IndexOf(ddlFuncionarioConvenios.Items.FindByValue(dtCliente.Rows(0)("id_funcionario")))
                        ddlTipoArchivo.SelectedIndex = ddlTipoArchivo.Items.IndexOf(ddlTipoArchivo.Items.FindByText(dtCliente.Rows(0)("TipoArchivoEnviar")))
                        txtCodigoInstitucion.Text = dtCliente.Rows(0)("codigo_institucion").ToString()
                        ddlEnvioAutListadoDescuentos.SelectedIndex = ddlEnvioAutListadoDescuentos.Items.IndexOf(ddlEnvioAutListadoDescuentos.Items.FindByValue(dtCliente.Rows(0)("ind_envio_automatico_listado")))

                        chkBloquear.Checked = dtCliente.Rows(0)("BloquearCredito").ToString()

                        txtTelefono1.Text = dtCliente.Rows(0)("telefono_1").ToString()
                        txtTelefono2.Text = dtCliente.Rows(0)("telefono_2").ToString()
                        txtTelefono3.Text = dtCliente.Rows(0)("telefono_3").ToString()

                        btnSearch.Visible = False
                        btnAceptar.Enabled = True
                        btnCancelar.Enabled = True
                    Else
                        Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoExisteRegistro, strMensaje)
                    End If
                End If

            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

                btnSearch.Visible = True
                btnAceptar.Enabled = False
                btnCancelar.Enabled = True

            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message

                btnSearch.Visible = True
                btnAceptar.Enabled = False
                btnCancelar.Enabled = True
            End Try

        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            If Not Page.IsPostBack Then
                LlenarCombos()

                hdTipoGuardar.Value = 1

                If Not Request.Params("id") Is Nothing Then
                    CargarCliente(Convert.ToInt32(Request.Params("id").ToString()))
                End If
            End If

            If hdIdIBS.Value.Length > 0 Then
                lnkCargarClienteIBS_Click(Nothing, Nothing)
            End If
        End Sub

        Protected Sub lnkCargarClienteIBS_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCargarClienteIBS.Click
            Dim strCodIBS As String = hdIdIBS.Value
            Dim strMensaje As String = ""

            Try
                strMensaje = CargarClienteDesdeIBS(hdIdIBS.Value)

                If strMensaje <> "" Then
                    Throw New HandledException(enumGeneric.ErrorMessage, clsConstantsGeneric.NoExisteRegistro, strMensaje)
                End If

            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull

                btnSearch.Visible = True
                btnAceptar.Enabled = False
                btnCancelar.Enabled = True

            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message

                btnSearch.Visible = True
                btnAceptar.Enabled = False
                btnCancelar.Enabled = True
            End Try

        End Sub

        Protected Sub btnAceptar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
            Dim intResult As Integer = 0
            Dim strMensajeVerificacion As String = ""

            lblMensaje.Text = ""

            If Page.IsValid Then

                objCliente.CodigoCliente = IIf(Request.Params("id") Is Nothing, 0, CType(Request.Params("id"), Integer))

                If txtCodigoIBS.Text.Trim() = "" Then
                    lblMensaje.Text = clsMensajesGeneric.MensajeValidarClienteIBS.Replace("&1", "Seleccionar")
                    Return
                End If

                strMensajeVerificacion = ValidarObjetoCliente(Convert.ToInt32(hdTipoGuardar.Value), objCliente)

                If strMensajeVerificacion = "" Then
                    Try
                        intResult = objClienteBL.ActualizarCliente(objCliente)

                        Response.Redirect("Clientes.aspx")
                    Catch ex1 As HandledException
                        lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
                    Catch ex2 As Exception
                        lblMensaje.Text = "Error: " + ex2.Message
                    End Try
                Else
                    lblMensaje.Text = strMensajeVerificacion
                End If

            End If

        End Sub

        Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
            Response.Redirect(Request.ApplicationPath + "/clientes/clientes.aspx", True)
        End Sub
    End Class
End Namespace

