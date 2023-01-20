Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource

Partial Class Alertas_frmListaAlertas
    Inherits System.Web.UI.Page

    Protected objAlerta As New clsAlertas()

    Protected objAlertasBL As New clsAlertasBL()

    Protected dtAlertas As New DataTable()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblMensaje.Text = ""

            Try
                objAlerta.iAlertaId = 0
                objAlerta.iTipoAlerta = 0
                objAlerta.vNombreAlerta = ""
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)

                gvQuery.DataSource = dtAlertas
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
                objAlerta.iAlertaId = 0
                objAlerta.iTipoAlerta = 0
                objAlerta.vNombreAlerta = ""
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                objAlerta.iAlertaId = 0

                Select Case UCase(txtValor.Text)
                    Case "PREPAGO"
                        objAlerta.iTipoAlerta = 1
                    Case "POSTPAGO"
                        objAlerta.iTipoAlerta = 2
                    Case "VENCIMIENTO"
                        objAlerta.iTipoAlerta = 3
                End Select

                objAlerta.vNombreAlerta = ""
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            ElseIf ddlCriterio.Text = "2" Then
                objAlerta.iAlertaId = 0
                objAlerta.iTipoAlerta = 0
                objAlerta.vNombreAlerta = txtValor.Text
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            End If
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
                objAlerta.iAlertaId = 0
                objAlerta.iTipoAlerta = 0
                objAlerta.vNombreAlerta = ""
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                objAlerta.iAlertaId = 0

                Select Case UCase(txtValor.Text)
                    Case "PREPAGO"
                        objAlerta.iTipoAlerta = 1
                    Case "POSTPAGO"
                        objAlerta.iTipoAlerta = 2
                    Case "VENCIMIENTO"
                        objAlerta.iTipoAlerta = 3
                End Select

                objAlerta.vNombreAlerta = ""
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            ElseIf ddlCriterio.Text = "2" Then
                objAlerta.iAlertaId = 0
                objAlerta.iTipoAlerta = 0
                objAlerta.vNombreAlerta = txtValor.Text
                objAlerta.iEstadoAlerta = -1

                dtAlertas = objAlertasBL.ObtieneAlertasPorCriterio(objAlerta)
            End If
        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Function GetMensajeSeleccionar(ByVal pstrCodAlerta As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + pstrCodAlerta + "');"">Seleccionar</a>"

        Return strEnlace
    End Function
End Class
