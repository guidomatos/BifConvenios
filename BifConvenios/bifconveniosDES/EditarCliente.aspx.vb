Imports System.Data
Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class EditarCliente
        Inherits System.Web.UI.Page
        Private oCliente As New Cliente()

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

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then
                Utils.AddSwap(lnkCancelar, "Image12", "/images/cancelar_on.jpg")
                Utils.AddSwap(lnkAceptar, "Image10", "/images/aceptar_on.jpg")
                ddlTipoDocumento.DataSource = BIFConvenios.TipoDocumento.GetTipoDocumento()
                ddlTipoDocumento.DataBind()
                ddlTipoDocumento.Items.Insert(0, New ListItem("--Seleccione el tipo de documento--", ""))
                'Modificado por Christian Rivera. 16-06-2014 Se añade combo de funcionarios de convenios. EA 273-Mejoras convenios
                ddlFuncionarioConvenios.DataSource = BIFConvenios.FuncionarioConvenios.GetFuncionarioConvenios()
                ddlFuncionarioConvenios.DataBind()
                ddlFuncionarioConvenios.Items.Insert(0, New ListItem("--Seleccione el funcionario de Convenios--", ""))
                ddlEnvioAutListadoDescuentos.Items.Insert(0, New ListItem("--Seleccionar--", ""))
                ddlEnvioAutListadoDescuentos.Items.Insert(1, New ListItem("Sí", "S"))
                ddlEnvioAutListadoDescuentos.Items.Insert(2, New ListItem("No", "N"))
                'Fin

                If Not Request.Params("id") Is Nothing Then
                    Call LoadForm(Request.Params("id"))
                End If

            End If
        End Sub


        Private Sub LoadForm(ByVal id As String)
            Dim dr As SqlDataReader
            dr = oCliente.GetCliente(id)
            If dr.Read Then
                txtNombreCliente.Text = CType(dr("Nombre_Cliente"), String)
                txtNumeroDocumento.Text = CType(dr("NumeroDocumento"), String)
                ddlTipoDocumento.SelectedIndex = ddlTipoDocumento.Items.IndexOf(ddlTipoDocumento.Items.FindByValue(dr("TipoDocumento")))
                'Enlazamos el control con la lista de correos 
                'Modificado por Christian Rivera. 16-06-2014 Se añade combo de funcionarios de convenios. EA 273-Mejoras convenios
                txtDiaEnvioPlanilla.Text = CType(dr("dia_envio_planilla"), String)
                txtDiaCierrePlanilla.Text = CType(dr("dia_cierre_planilla"), String)
                txtMesesAnticipEnvioListado.Text = CType(dr("meses_anticipacion_envio_listado"), String)
                txtDiaCorte.Text = CType(dr("dia_corte"), String)
                ddlFuncionarioConvenios.SelectedIndex = ddlFuncionarioConvenios.Items.IndexOf(ddlFuncionarioConvenios.Items.FindByValue(dr("id_funcionario")))
                ddlEnvioAutListadoDescuentos.SelectedIndex = ddlEnvioAutListadoDescuentos.Items.IndexOf(ddlEnvioAutListadoDescuentos.Items.FindByValue(dr("ind_envio_automatico_listado")))
                txtCodigoIBS.Text = CType(dr("codigo_IBS"), String)
                txtCodigoInstitucion.Text = CType(dr("codigo_institucion"), String)
                txtTelefono1.Text = CType(dr("telefono_1"), String)
                txtTelefono2.Text = CType(dr("telefono_2"), String)
                txtTelefono3.Text = CType(dr("telefono_3"), String)
                'Fin
            End If
        End Sub

        ''Adicionar y enlazar la informacion de los dataset
        'Private Sub BindDS(ByVal id As String)
        '    'Dim str As String
        '    Dim ds As DataSet
        '    Dim dr As DataRow
        '    Dim dr2 As SqlDataReader
        '    dr2 = oCliente.ObtieneCoordinadorCliente(id)

        '    If ViewState("Mails") Is Nothing Then
        '        ds = New DataSet()
        '        Call ConfigureDataSet(ds)
        '    Else
        '        ds = CType(ViewState("Mails"), DataSet)
        '    End If

        '    'For Each str In CorreoElectronico.Split(";")
        '    '    If str.Trim <> "" Then
        '    '        dr = ds.Tables(0).NewRow
        '    '        dr.Item(0) = str
        '    '        ds.Tables(0).Rows.Add(dr)
        '    '    End If
        '    'Next

        '    'If dr2.Read Then
        '    '    dr = ds.Tables(0).NewRow
        '    '    dr.Item(0) = CType(dr2("nombre_coordinador"), String)
        '    '    dr.Item(1) = CType(dr2("email_coordinador"), String)
        '    '    ds.Tables(0).Rows.Add(dr)
        '    'End If

        '    Do While dr2.Read
        '        dr = ds.Tables(0).NewRow
        '        dr.Item(0) = CType(dr2("nombre_coordinador"), String)
        '        dr.Item(1) = CType(dr2("email_coordinador"), String)
        '        ds.Tables(0).Rows.Add(dr)
        '    Loop

        '    ViewState("Mails") = ds
        '    dgMails.DataSource = ds
        '    dgMails.DataBind()

        'End Sub


        ''Configura el DataSet que sera utilizado para mantener la informacion de los correos electronicos
        'Private Sub ConfigureDataSet(ByRef ds As DataSet)
        '    ds.DataSetName = "Emails"
        '    ds.Tables.Add("Table")
        '    ds.Tables(0).Columns.Add("nombre")
        '    ds.Tables(0).Columns.Add("mail")
        'End Sub


        'Obtenemos la lista de los correos electronicos q han sido ingresados
        'Private Function GetEmails() As String
        '    Dim ds As DataSet
        '    Dim dr As DataRow
        '    Dim str As String = ""

        '    If ViewState("Mails") Is Nothing Then
        '        ds = New DataSet()
        '        Call ConfigureDataSet(ds)
        '    Else
        '        ds = CType(viewstate("Mails"), DataSet)
        '    End If

        '    If Not ds Is Nothing Then
        '        For Each dr In ds.Tables(0).Rows
        '            str += CType(dr(0), String) + ";"
        '        Next
        '    End If

        '    If str.Trim <> "" Then
        '        str = str.Substring(0, str.Length - 1)
        '    End If
        '    Return str
        'End Function


        Private Sub lnkAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAceptar.Click
            If Page.IsValid Then
                Dim id As String = ""
                Dim CustomerNumber As String = ""
                id = IIf(Request.Params("id") Is Nothing, "0", CType(Request.Params("id"), String))
                'TODO: REVISAR SI CLIENTE EXISTE EN AS/400
                CustomerNumber = oCliente.GetCustomerNumber(ddlTipoDocumento.SelectedItem.Value, txtNumeroDocumento.Text)
                If CustomerNumber.Trim = "" Then
                    'lblMensaje.Text = "No se encuentra el cliente en AS/400, por favor verifique el tipo y número de documento"
                    lblMensaje.Text = "No se ha encontrado la empresa en IBS, verifica el tipo y número de documento."
                    Return
                End If

                'Si se va a agregar empresa se añade validacion de existencia de la empresa por tipo 
                'y numero de documento EA273 Christian Rivera 11-07-2014
                If id = "0" Then
                    If oCliente.ExisteCliente(ddlTipoDocumento.SelectedItem.Value, Trim(txtNumeroDocumento.Text)) <> "-1" Then
                        lblMensaje.Text = "Ya existe registrada una empresa con ese tipo y número de documento."
                        Return
                    End If
                End If

                oCliente.UpdateCliente(id, Trim(txtNombreCliente.Text), ddlTipoDocumento.SelectedItem.Value, Trim(txtNumeroDocumento.Text), "", Trim(txtTelefono1.Text), Trim(txtTelefono2.Text), Trim(txtTelefono3.Text), "", Trim(txtDiaEnvioPlanilla.Text), Trim(txtDiaCierrePlanilla.Text), Trim(txtMesesAnticipEnvioListado.Text), Trim(txtDiaCorte.Text), ddlFuncionarioConvenios.SelectedItem.Value, Trim(txtCodigoIBS.Text), Trim(txtCodigoInstitucion.Text), ddlEnvioAutListadoDescuentos.SelectedItem.Value, Context.User.Identity.Name, Now, Context.User.Identity.Name, Now)

                'Response.Redirect("/Clientes.aspx")
                Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/Clientes.aspx"))
            End If
        End Sub

        'Private Sub lnkAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEdit.Click
        '    Dim ds As DataSet
        '    Dim dr As DataRow

        '    If ViewState("Mails") Is Nothing Then
        '        ds = New DataSet()
        '        Call ConfigureDataSet(ds)
        '    Else
        '        ds = CType(viewstate("Mails"), DataSet)
        '    End If
        '    If txtCorreoElectronico.Text.Trim <> "" Then
        '        If ds.Tables(0).Select("mail ='" & txtCorreoElectronico.Text.Trim & "'").GetLength(0) = 0 Then
        '            dr = ds.Tables(0).NewRow
        '            dr.Item(0) = txtCorreoElectronico.Text.Trim.ToUpper
        '            ds.Tables(0).Rows.Add(dr)
        '            viewstate("Mails") = ds
        '            dgMails.DataSource = ds
        '            dgMails.DataBind()
        '            txtCorreoElectronico.Text = ""
        '        End If
        '    End If
        'End Sub

        'Private Sub dgMails_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgMails.ItemCommand
        '    If e.CommandName = "delete" Then
        '        Dim ds As DataSet = CType(viewstate("Mails"), DataSet)
        '        Dim dr As DataRow()
        '        dr = ds.Tables(0).Select("mail ='" & e.CommandArgument & "'")

        '        If dr.GetLength(0) > 0 Then
        '            dr(0).Delete()
        '        End If
        '        ViewState("Mails") = ds
        '        dgMails.DataSource = ds
        '        dgMails.DataBind()
        '    End If
        'End Sub

        Private Sub lnkCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelar.Click
            ' Response.Redirect(Request.ApplicationPath + "/clientes/clientes.aspx")
            Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/clientes/clientes.aspx"))
        End Sub
    End Class
End Namespace

