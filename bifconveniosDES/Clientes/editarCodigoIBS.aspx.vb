Imports BIFConvenios.Logica
Imports BIFConvenios.Entidad
Imports BIFConvenios.BL

Partial Class Clientes_editarCodigoIBS
    Inherits System.Web.UI.Page
    Protected objClienteLogica As New BIFConvenios.Logica.clsClienteBL
    Protected objClientes As New BIFConvenios.Entidad.clsClienteBE
    Protected codigoIBS As Integer
    Protected objClienteBL As New BIFConvenios.BL.clsClienteBL


    Protected Sub btnOkay_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOkay.Click
        Try
            'MultiViewExpanse.ActiveViewIndex = 1
            'Response.Redirect("clientes.aspx")

            Dim intResult As Integer = 0
            codigoIBS = Session("CodigoIBS")
            Dim intExiste As Integer = objClienteLogica.ExisteClienteDesdeAS400PorCodIBS(txtcodigoibsnuevo.Value.Trim())
            hdExiste.Value = intExiste
            'Session("ExisteCodigoIBS") = intExiste
            Dim intExisteSQL As Integer = objClienteLogica.GetClienteCodigoIBS(txtcodigoibsnuevo.Value.Trim())
            objClientes.CodigoCliente = codigoIBS
            objClientes.CodigoIBS = Integer.Parse(txtcodigoibsnuevo.Value)
            objClientes.UsuarioModificacion = Context.User.Identity.Name.ToString()

            If intExiste <> 0 Then
                If intExisteSQL = 0 Then
                    intResult = objClienteLogica.ActualizarClienteCodigoIBS(objClientes)
                Else
                    Session("ExisteCodigoIBS") = 2
                End If
            Else
                Session("ExisteCodigoIBS") = 1
            End If
            ClientScript.RegisterStartupScript(Me.GetType(), "onload", "onSuccess();", True)
        Catch ex As Exception
            ClientScript.RegisterStartupScript(Me.GetType(), "onload", "onError();", True)
            'MultiViewExpanse.ActiveViewIndex = 1
        End Try

    End Sub

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load
        If Not IsPostBack Then

            codigoIBS = Request.QueryString("codigoIBS")
            Session("CodigoIBS") = codigoIBS
            Session("ExisteCodigoIBS") = -1
            MultiViewExpanse.ActiveViewIndex = 0
        End If
    End Sub


    Protected Sub btnvalidar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        'Dim intExiste As Integer = objClienteBL.ExisteClienteDesdeAS400PorCodIBS(codigoIBS)
        'hdExiste.Value = 456
        'ScriptManager.RegisterStartupScript(Me, Me.GetType(), "myexiste", "ExisteCodigoIBS();", True)
    End Sub


End Class
