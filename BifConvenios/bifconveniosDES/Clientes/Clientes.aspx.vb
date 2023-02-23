Imports BIFConvenios.BL
Imports BIFConvenios.BE
Imports Resource

Namespace BIFConvenios

    Partial Class Clientes
        Inherits System.Web.UI.Page

        Private oCliente As New Cliente()
        Private objClienteBL As New clsClienteBL()
        Private objCliente As New clsCliente()

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

        Private Sub BindDG(ByVal pstrNombreCliente As String)
            Dim iTotalRows As Integer = 0
            Dim dt As New DataTable()

            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            Try
                objCliente.NombreCliente = pstrNombreCliente
                dt = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRows)

                gvClientes.DataSource = dt
                gvClientes.DataBind()

                pnlQueryClientes.Visible = True

            Catch ex As HandledException
                pnlQueryClientes.Visible = False
                pnlMensaje.Visible = True
                lblMensaje.Text = ex.ErrorMessage + ": " + ex.ErrorMessageFull
            End Try
        End Sub

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here

            If Not IsPostBack Then
                pnlQueryClientes.Visible = True
                pnlMensaje.Visible = False

                If Not Session("cliente") Is Nothing Then
                    txtNombreCliente.Text = Session("cliente").ToString()
                End If

                BindDG(txtNombreCliente.Text)
            Else
                btnClientSearch_Click(Nothing, Nothing)
            End If
        End Sub

        Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
            'oCliente.DeleteCliente(hdData.Value, Context.User.Identity.Name)
            Try
                objClienteBL.EliminarCliente(hdData.Value, Context.User.Identity.Name)

                BindDG(txtNombreCliente.Text)
            Catch ex1 As HandledException
                pnlQueryClientes.Visible = False
                pnlMensaje.Visible = True

                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                pnlQueryClientes.Visible = False
                pnlMensaje.Visible = True

                lblMensaje.Text = "Error: " + ex2.Message
            End Try
        End Sub

        Protected Sub btnClientSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnClientSearch.Click
            Session("cliente") = txtNombreCliente.Text.Trim()

            BindDG(txtNombreCliente.Text.Trim())
        End Sub

        Protected Sub btnNuevoCliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevoCliente.Click
            'Dim strResult As String = "editarcliente.aspx"
            Dim strResult As String = "/clientes/editarcliente.aspx"
            Response.Redirect(Utils.getUrlPathApplicationRedirectPage(strResult), True)
            'Response.Redirect(strResult, True)
        End Sub

        Protected Sub gvClientes_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvClientes.PageIndexChanging
            gvClientes.PageIndex = e.NewPageIndex

            BindDG(txtNombreCliente.Text)
        End Sub

        Protected Sub btnRegresar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRegresar.Click
            'Response.Redirect(Request.ApplicationPath + "/default.aspx", True)
            'Response.Redirect("~/default.aspx", True)
            Response.Redirect(Utils.getUrlPathApplicationRedirectPage("/default.aspx"), True)
        End Sub
    End Class
End Namespace
