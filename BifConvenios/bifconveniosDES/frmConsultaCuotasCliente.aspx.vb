Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Imports Resource

Partial Class frmConsultaCuotasCliente
    Inherits System.Web.UI.Page

    Protected oCliente As New BIFConvenios.Cliente()
    'Protected oProceso As New Proceso()

    Protected objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient
    Protected PID As String = ""

    Protected dt As New DataTable()

    Private Sub BindDG(ByVal dt As DataTable)
        pnlMensaje.Visible = False
        lblMensaje.Text = ""

        Try
            Dim dtTemp As New DataTable()
            dtTemp = dt.Clone()

            If ddlCriterio.Text = "0" Then
                For count As Integer = 0 To dt.Rows.Count - 1
                    dtTemp.ImportRow(dt.Rows(count))
                Next
            ElseIf ddlCriterio.Text = "1" And Len(txtValor.Text) > 0 Then
                For Each row As DataRow In dt.Rows
                    If row("DLENP").ToString.Equals(txtValor.Text.ToString()) Then
                        dtTemp.ImportRow(row)
                    End If
                Next
            ElseIf ddlCriterio.Text = "2" And Len(txtValor.Text) > 0 Then
                For Each row As DataRow In dt.Rows
                    Dim len1, len2, len As Integer

                    len1 = row("DLENE").ToString().Length
                    len2 = txtValor.Text.Length

                    len = IIf(len1 >= len2, len2, len1)

                    Dim strCadena As String = row("DLENE").ToString().Substring(0, len)

                    If UCase(strCadena.ToString()) = (UCase(txtValor.Text)) Then
                        dtTemp.ImportRow(row)
                    End If
                Next
            Else
                For count As Integer = 0 To dt.Rows.Count - 1
                    dtTemp.ImportRow(dt.Rows(count))
                Next
            End If

            If dtTemp.Rows.Count > 0 Then
                gvQuery.DataSource = dtTemp
                gvQuery.DataBind()
            Else
                Throw New HandledException(enumGeneric.NoRecords, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If

            pnlQueryResult.Visible = True
        Catch ex1 As HandledException
            pnlMensaje.Visible = True
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            pnlMensaje.Visible = True
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim CodCliente As String = Request.Params("CodCliente").ToString()
            Dim strAnio As String = Request.Params("anio").ToString()
            Dim strMes As String = Request.Params("mes").ToString()
            Dim strFechaIBS As String = Request.Params("FechaIBS").ToString()

            Dim dr As SqlDataReader = oCliente.GetCliente(CodCliente)
            Dim dt As New DataTable()

            If dr.Read Then
                Dim TipoDocumento As String = CType(dr("TipoDocumento"), String)
                Dim NumeroDocumento As String = CType(dr("NumeroDocumento"), String)
                Dim CodClienteIBS = oCliente.GetCustomerNumber(TipoDocumento, NumeroDocumento)

                ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                ltrlDocumento.Text = NumeroDocumento
                ltrlPeriodo.Text = strAnio.ToString() + " - " + strMes.ToString()

                dt = objWSConvenios.ConsultaPagaresDeIBS(CodClienteIBS, strAnio, strMes, strFechaIBS, CodCliente, Context.User.Identity.Name.ToString())

                Session("dtEmpresa") = dt

                BindDG(dt)
            End If
        End If
    End Sub

    Protected Sub gvQuery_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvQuery.PageIndexChanging
        gvQuery.PageIndex = e.NewPageIndex

        Dim dtSessionEmpresa As New DataTable()

        If Not Session("dtEmpresa") Is DBNull.Value Then
            dtSessionEmpresa = CType(Session("dtEmpresa"), DataTable)

            BindDG(dtSessionEmpresa)
        End If
    End Sub

    Protected Sub lnkBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        Response.Redirect("cargageneracioncf.aspx")
    End Sub

    Protected Sub btnSearch_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim dtSessionEmpresa As New DataTable()

        If Not Session("dtEmpresa") Is DBNull.Value Then
            dtSessionEmpresa = CType(Session("dtEmpresa"), DataTable)

            BindDG(dtSessionEmpresa)
        End If
    End Sub

    Protected Sub ddlCriterio_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlCriterio.SelectedIndexChanged
        Dim dtSessionEmpresa As New DataTable()

        Select Case ddlCriterio.SelectedIndex
            Case 0
                txtValor.Text = ""
                txtValor.Enabled = False
            Case 1
                txtValor.Enabled = True
            Case 2
                txtValor.Enabled = True
        End Select

        If Not Session("dtEmpresa") Is DBNull.Value Then
            dtSessionEmpresa = CType(Session("dtEmpresa"), DataTable)

            BindDG(dtSessionEmpresa)
        End If
    End Sub
End Class



