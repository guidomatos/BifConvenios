Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

Imports BIFConvenios
Imports Resource

Partial Class reportes_buscarEmpresaIBS
    Inherits System.Web.UI.Page

    Protected oProc As New Proceso()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        lblMensaje.Text = ""

        If Not Page.IsPostBack Then

            Try

                Call BindGrid()

            Catch ex1 As HandledException
                lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
            Catch ex2 As Exception
                lblMensaje.Text = "Error: " + ex2.Message
            End Try


        End If


    End Sub

    Private Sub BindGrid()

        Dim pCliente As String
        Dim ds As New DataSet

        If Me.txtfEmpresa.Text <> "" Then
            pCliente = txtfEmpresa.Text
        Else
            pCliente = "*"
        End If

        ds = oProc.GetClienteBIFConvenios(pCliente)

        If ds.Tables(0).Rows.Count > 0 Then
            dgDatos.DataSource = ds
            dgDatos.DataBind()
            lblNumReg.Text = dgDatos.Rows.Count.ToString()

        Else
            lblNumReg.Text = 0
            lblMensaje.Text = "La consulta no devolvió datos."

        End If

        

    End Sub

    Protected Function GetMensajeSeleccionar(ByVal pstrCodIBS As String, ByVal Empresa As String) As String
        Dim strEnlace As String = ""

        strEnlace = "<a href=""JavaScript:Seleccionar('" + pstrCodIBS + "','" + Empresa + "');"">Seleccionar</a>"

        Return strEnlace
    End Function

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Call BindGrid()

    End Sub

    Protected Sub dgDatos_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgDatos.PageIndexChanging

        dgDatos.PageIndex = e.NewPageIndex
        lblMensaje.Text = ""


        Try

            Call BindGrid()

        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try


    End Sub


End Class
