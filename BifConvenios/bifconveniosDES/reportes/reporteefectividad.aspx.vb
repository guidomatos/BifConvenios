Imports System.Data
Imports System.io
Imports System.Data.SqlClient


Namespace BIFConvenios

    Partial Class reporteefectividad

        Inherits System.Web.UI.Page
        Protected reporte As New Reportes()
        Private cantidadReg As Integer = 0
        Protected headhtml As String = "<table     border = '1'     rules = 'all'     cellpadding = '0'     cellspacing = '0'     style = 'border-collapse :collapse'>     <tr class = 'TablaNormalBoldBIF'>         <td>Codigo IBS</td>         <td colspan = '9'>Nombre_empresa</td>     </tr>     <tr class = 'TablaNormalBoldBIF'>         <td>codigo_clienteIBS</td>         <td colspan = '3'>Nomina Enviada</td>         <td colspan = '3'>Nomina retornada</td>         <td colspan = '3'>&nbsp;</td>     </tr>     <tr class = 'TablaNormalBoldBIF'>         <td>Mes</td>         <td>Nro de Clientes</td>         <td>Monto Soles</td>         <td>Monto Dolares</td>         <td>Nro de Clientes</td>         <td>Monto Soles</td>         <td>Monto Dolares</td>         <td>%Efectividad por num de Clientes</td>         <td>%Efectividad por Monto Soles</td>         <td>%Efectividad por Monto Dolares</td>     </tr>     <tr class = 'TablaNormalBIF'>         <td> "
        Protected footHtml As String = "</td></tr></table>"
        Private codigo_IBS As String = ""
        Private Nombre_empresa As String = ""
        Protected oProceso As New BIFConvenios.Proceso()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

            lblMensaje.Text = ""
            pnlexportar.Visible = False
            form1.DefaultButton = "Button1"
            If Not Page.IsPostBack Then
                'BindData()
            End If

        End Sub

        Protected Sub BindData(ByVal codigoIBS As String, ByVal empresa As String)

            Dim ds As New DataSet()

            reporte.EfectividadRecaudacion(codigoIBS, "", "").Fill(ds, "0")

            gridEfectividad.DataSource = ds.Tables("0")
            cantidadReg = ds.Tables("0").Rows.Count

            If cantidadReg > 0 Then
                codigo_IBS = ds.Tables("0").Rows(0).Item("codigo_clienteIBS").ToString()
                Nombre_empresa = ds.Tables("0").Rows(0).Item("Nombre_empresa").ToString()
                pnlexportar.Visible = True
                lblMensaje.Text = ""
            Else
                lblMensaje.Text = "La consulta no devuelve datos."
                gridEfectividad.DataSource = Nothing
            End If
            gridEfectividad.DataBind()

        End Sub

        Public Function esVisible(ByVal strValue As Integer) As String
            If strValue = gridEfectividad.PageIndex * gridEfectividad.PageSize Then
                Return headhtml.Replace("codigo_clienteIBS", codigo_IBS).Replace("Nombre_empresa", Nombre_empresa)
            Else
                Return ""
            End If
        End Function

        Public Function esVisible2(ByVal strValue As Integer) As String
            If (strValue = ((gridEfectividad.PageIndex + 1) * gridEfectividad.PageSize) - 1 _
                Or (strValue = (gridEfectividad.PageIndex * gridEfectividad.PageSize) + (cantidadReg Mod gridEfectividad.PageSize) - 1) _
                And Int(cantidadReg / gridEfectividad.PageSize) = gridEfectividad.PageIndex) Then
                Return footHtml
            Else
                Return ""
            End If
        End Function

        Protected Sub gridEfectividad_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gridEfectividad.PageIndexChanging

            gridEfectividad.PageIndex = e.NewPageIndex

            Dim CodigoIBS, Empresa As String

            If txtCodigoIBS.Text <> "" Then
                CodigoIBS = txtCodigoIBS.Text
            Else
                CodigoIBS = 0
            End If

            If txtNombreEmpresa.Text <> "" Then
                Empresa = txtNombreEmpresa.Text
            Else
                Empresa = "*"
            End If

            BindData(CodigoIBS, Empresa)

        End Sub

        Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

            Dim CodigoIBS, Empresa As String

            If txtCodigoIBS.Text <> "" Then
                CodigoIBS = Trim(txtCodigoIBS.Text)
            Else
                CodigoIBS = 0
            End If

            If txtNombreEmpresa.Text <> "" Then
                Empresa = txtNombreEmpresa.Text
            Else
                Empresa = "*"
            End If

            BindData(CodigoIBS, Empresa)

        End Sub


        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click

            Dim CodigoIBS, Empresa As String

            If txtCodigoIBS.Text <> "" Then
                CodigoIBS = Trim(txtCodigoIBS.Text)
            Else
                CodigoIBS = 0
            End If

            If txtNombreEmpresa.Text <> "" Then
                Empresa = txtNombreEmpresa.Text
            Else
                Empresa = "*"
            End If

            BindData(CodigoIBS, Empresa)

            Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("NombrePlantillaEfectividadEmpresa"))
            archivExcel.GenerarExcelEfectividadEmpresa(CType(gridEfectividad.DataSource, DataTable), ConfigurationManager.AppSettings("GenFolder") & "EfectividadEmpresa" & txtCodigoIBS.Text & ".xls")
            'Call Utils.InvocarArchivo(ConfigurationManager.AppSettings("GenFolder"), "EfectividadEmpresa" & codigoIBS.Text & ".xls")
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & "EfectividadEmpresa" & txtCodigoIBS.Text & ".xls")
            Response.TransmitFile(ConfigurationManager.AppSettings("GenFolder") & "EfectividadEmpresa" & txtCodigoIBS.Text & ".xls")
            Response.End()
        End Sub


    End Class

End Namespace