Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Alertas_frmListaEmpresasBifConvenios
    Inherits System.Web.UI.Page

    Protected dtClientes As New DataTable()

    Protected objCliente As New clsCliente()

    Protected objClienteBL As New clsClienteBL()

    Dim iTotalRow As Integer = 0

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMensaje.Text = ""

            Try
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = ""
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = ""

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)

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
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = ""
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = ""

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = ""
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = txtValor.Text

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
            ElseIf ddlCriterio.Text = "2" And Len(txtValor.Text) > 0 Then
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = txtValor.Text
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = ""

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
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
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = ""
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = ""

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = ""
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = txtValor.Text

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
            ElseIf ddlCriterio.Text = "2" And Len(txtValor.Text) > 0 Then
                objCliente.CodigoCliente = 0
                objCliente.NombreCliente = txtValor.Text
                objCliente.TipoDocumento = ""
                objCliente.NumeroDocumento = ""

                dtClientes = objClienteBL.ObtenerListaClientePorCriterio(objCliente, 0, 0, iTotalRow)
            End If

            gvQuery.DataSource = dtClientes
            gvQuery.DataBind()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Function GetMensajeSeleccionar(ByVal pstrCodCliente As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + pstrCodCliente + "');"">Seleccionar</a>"

        Return strEnlace
    End Function
End Class

