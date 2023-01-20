Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Imports System.Collections
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Imports BIFConvenios.BE
Imports BIFConvenios.BL
Imports Resource
Imports System.IO

Partial Class frmVisorEnviosAutomaticos
    Inherits System.Web.UI.Page

    'Protected objLogEnvioCorreo As New clsLogEnvioCorreo()
    Protected objProcesoAutomatico As New clsProcesosAutomaticos()
    Protected objSystemParameters As New clsSystemParameters()

    'Protected objLogEnvioCorreoBL As New clsLogEnvioCorreosBL()
    Protected objProcesoAutomaticoBL As New clsProcesosAutomaticosBL()
    Protected objSystemParametersBL As New clsSystemParametersBL()

    'Protected dtLogEnvioCorreo As New DataTable()
    Protected _dtProcesosAutomaticos As New DataTable()
    Protected _dtSystemParameters As New DataTable()

    Private Sub BindDG(ByVal pintEstado As Integer)
        pnlQueryResult.Visible = True
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            objProcesoAutomatico.iEstado = pintEstado

            _dtProcesosAutomaticos = objProcesoAutomaticoBL.Seleccionar(objProcesoAutomatico)

            gvQuery.DataSource = _dtProcesosAutomaticos
            gvQuery.DataBind()

        Catch ex As HandledException
            pnlQueryResult.Visible = False
            pnlMensaje.Visible = True
            lblMensaje.Text = ex.ErrorMessage + ": " + ex.ErrorMessageFull
        End Try
    End Sub

    Private Sub LlenarCombos()
        Dim dtTipoEnvio As New DataTable()
        Dim dtEstadoEnvio As New DataTable()
        Dim strCriterio As String = String.Empty

        _dtSystemParameters = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.EstadoProcesoAutomatico).ToString())

        ddlEstado.DataSource = _dtSystemParameters
        ddlEstado.DataBind()
        ddlEstado.Items.Insert(0, New ListItem("-- Todos --", "0"))
    End Sub

    Protected Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        Dim strValor As String = String.Empty

        gvQuery.PageIndex = e.NewPageIndex

        BindDG(Convert.ToInt32(ddlEstado.Text))
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LlenarCombos()

            pnlQueryResult.Visible = True
            pnlMensaje.Visible = False

            BindDG(Convert.ToInt32(ddlEstado.Text))
        End If
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim strValor As String = String.Empty

        BindDG(Convert.ToInt32(ddlEstado.Text))
    End Sub

    Protected Sub lnkBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        Response.Redirect("cargageneracioncf.aspx")
    End Sub
End Class
