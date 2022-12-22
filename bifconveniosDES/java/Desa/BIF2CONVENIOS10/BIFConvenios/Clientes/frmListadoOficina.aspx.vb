Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource
Partial Class Clientes_frmListadoOficina
    Inherits System.Web.UI.Page

    Protected dtResponsable As New DataTable()

    Protected objResponsable As New clsReponsableOficina()
    Protected objResponsableBL As New clsResponsableOficinaBL()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            pnlMensaje.Visible = False
            lblMensaje.Text = ""

            Try

                dtResponsable = objResponsableBL.ObtenerOficinaDesdeAS400PorCriterio(0, "")

                gvQuery.DataSource = dtResponsable
                gvQuery.DataBind()
            Catch ex1 As HandledException
                pnlMensaje.Visible = True
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                pnlMensaje.Visible = True
                lblMensaje.Text = "Error: " + ex2.Message
            End Try

        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            If Len(txtValor.Text) > 0 Then
                dtResponsable = objResponsableBL.ObtenerOficinaDesdeAS400PorCriterio(Convert.ToInt32(ddlCriterio.Text), txtValor.Text)
            Else
                dtResponsable = objResponsableBL.ObtenerOficinaDesdeAS400PorCriterio(0, "")
            End If


            gvQuery.DataSource = dtResponsable
            gvQuery.DataBind()
        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub

    Protected Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        gvQuery.PageIndex = e.NewPageIndex

        lblMensaje.Text = ""

        Try
            If ddlCriterio.Text = "0" Then
                dtResponsable = objResponsableBL.ObtenerListaOficinasDesdeAS400()
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                dtResponsable = objResponsableBL.ObtenerListaOficinasDesdeAS400()
            Else
                dtResponsable = objResponsableBL.ObtenerListaOficinasDesdeAS400()
            End If

            gvQuery.DataSource = dtResponsable
            gvQuery.DataBind()
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try

    End Sub

    Protected Function GetMensajeSeleccionar(ByVal piOficinaId As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + piOficinaId + " ');"">Seleccionar</a>"

        Return strEnlace
    End Function
End Class
