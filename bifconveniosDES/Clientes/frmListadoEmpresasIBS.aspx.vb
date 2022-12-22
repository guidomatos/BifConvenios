Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Clientes_frmListadoEmpresasIBS
    Inherits System.Web.UI.Page

    Protected dtClientes As New DataTable()

    Protected objCliente As New clsCliente()

    Protected objClienteBL As New clsClienteBL()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblMensaje.Text = ""

            Try
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", "", "10")

                gvQuery.DataSource = dtClientes
                gvQuery.DataBind()
            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message
            End Try

        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        lblMensaje.Text = ""

        Try
            If ddlCriterio.Text = "0" Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", "", "10")
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", txtValor.Text, "", "10")
            ElseIf ddlCriterio.Text = "2" And Len(txtValor.Text) > 0 Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", txtValor.Text, "10")
            ElseIf ddlCriterio.Text = "3" Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400(txtValor.Text, "", "", "10")
            Else
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", "", "10")
            End If

            gvQuery.DataSource = dtClientes
            gvQuery.DataBind()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub

    Protected Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        gvQuery.PageIndex = e.NewPageIndex

        lblMensaje.Text = ""

        Try
            If ddlCriterio.Text = "0" Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", "", "10")
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", txtValor.Text, "", "10")
            ElseIf ddlCriterio.Text = "2" And Len(txtValor.Text) > 0 Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", txtValor.Text, "10")
            ElseIf ddlCriterio.Text = "3" Then
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400(txtValor.Text, "", "", "10")
            Else
                dtClientes = objClienteBL.ObtenerListaClienteDesdeAS400("", "", "", "10")
            End If

            gvQuery.DataSource = dtClientes
            gvQuery.DataBind()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
        
    End Sub

    Protected Function GetMensajeSeleccionar(ByVal pstrCodIBS As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + pstrCodIBS + "');"">Seleccionar</a>"

        Return strEnlace
    End Function
End Class
