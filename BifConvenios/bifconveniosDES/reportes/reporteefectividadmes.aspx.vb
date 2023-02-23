Imports System.Data.SqlClient
Namespace BIFConvenios
    Partial Class reporteefectividadmes
        Inherits System.Web.UI.Page
        Protected reporte As New Reportes()
        Private cantidadReg As Integer = 0
        Protected headhtml As String = "<table     border = '1'     rules = 'all'     cellpadding = '0'     cellspacing = '0'     style = 'border-collapse :collapse'>     <tr class = 'TablaNormalBoldBIF'>         <td colspan = '2'></td>         <td colspan = '9'>AnioMes</td>     </tr>     <tr class = 'TablaNormalBoldBIF'>         <td colspan = '2'></td>         <td colspan = '3'>Nomina Enviada</td>         <td colspan = '3'>Nomina retornada</td>         <td colspan = '3'>&nbsp;</td>     </tr>     <tr class = 'TablaNormalBoldBIF'> 		<td>Codigo IBS</td>         <td>Empresa</td>         <td>Nro de Clientes</td>         <td>Monto Soles</td>         <td>Monto Dolares</td>         <td>Nro de Clientes</td>         <td>Monto Soles</td>         <td>Monto Dolares</td>         <td>%Efectividad por num de Clientes</td>         <td>%Efectividad por Monto Soles</td>         <td>%Efectividad por Monto Dolares</td>     </tr>     <tr class = 'TablaNormalBIF'>         <td>"
        Private footHtml As String = "</td></tr><tr><td colspan='2' align='right'>TOTAL</td><td>suma_clientes_envio</td><td>suma_soles_envio</td><td>suma_dolares_envio</td><td>suma_clientes_retorno</td><td>suma_soles_retorno</td><td>suma_dolares_retorno</td><td colspan='3'></td></tr></table>"
        Private AnioMes As String = ""
        Private suma_clientes_envio As Double
        Private suma_soles_envio As Double
        Private suma_dolares_envio As Double
        Private suma_clientes_retorno As Double
        Private suma_soles_retorno As Double
        Private suma_dolares_retorno As Double




        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            pnlexportar.Visible = False
            If Not Page.IsPostBack Then
                ddlPeriodo.Items.Add(New ListItem("--Seleccione--", "-1"))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-6).Month) & " - " & DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-5).Month) & " - " & DateTime.Now.AddMonths(-5).Year, DateTime.Now.AddMonths(-5).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-4).Month) & " - " & DateTime.Now.AddMonths(-4).Year, DateTime.Now.AddMonths(-4).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-3).Month) & " - " & DateTime.Now.AddMonths(-3).Year, DateTime.Now.AddMonths(-3).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-2).Month) & " - " & DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.AddMonths(-1).Month) & " - " & DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month))
                ddlPeriodo.Items.Add(New ListItem(MonthName(DateTime.Now.Month) & " - " & DateTime.Now.Year, DateTime.Now.Month))
            End If
        End Sub

        Protected Sub BindData(ByVal Anio As String, ByVal Mes As String)
            Dim ds As New DataSet()
            reporte.EfectividadRecaudacion("", Anio, IIf(Mes.Length = 1, "0" & Mes, Mes)).Fill(ds, "0")
            gridEfectividad.DataSource = ds.Tables("0")
            cantidadReg = ds.Tables("0").Rows.Count
            If cantidadReg > 0 Then
                AnioMes = ds.Tables("0").Rows(0).Item("AnioMes").ToString()
                Dim i As Integer = 0
                suma_clientes_envio = 0
                suma_soles_envio = 0
                suma_dolares_envio = 0
                suma_clientes_retorno = 0
                suma_soles_retorno = 0
                suma_dolares_retorno = 0
                For i = 0 To cantidadReg - 1
                    suma_clientes_envio = suma_clientes_envio + CType(ds.Tables("0").Rows(i).Item("nro_clientes_enviado").ToString(), Double)
                    suma_soles_envio = suma_soles_envio + CType(ds.Tables("0").Rows(i).Item("nro_monto_enviado_soles").ToString(), Double)
                    suma_dolares_envio = suma_dolares_envio + CType(ds.Tables("0").Rows(i).Item("nro_monto_enviado_dolares").ToString(), Double)
                    suma_clientes_retorno = suma_clientes_retorno + CType(ds.Tables("0").Rows(i).Item("nro_clientes_retornado").ToString(), Double)
                    suma_soles_retorno = suma_soles_retorno + CType(ds.Tables("0").Rows(i).Item("nro_monto_retornado_soles").ToString(), Double)
                    suma_dolares_retorno = suma_dolares_retorno + CType(ds.Tables("0").Rows(i).Item("nro_monto_retornado_dolares").ToString(), Double)
                Next
                footHtml = footHtml.Replace("suma_clientes_envio", suma_clientes_envio)
                footHtml = footHtml.Replace("suma_soles_envio", suma_soles_envio)
                footHtml = footHtml.Replace("suma_dolares_envio", suma_dolares_envio)
                footHtml = footHtml.Replace("suma_clientes_retorno", suma_clientes_retorno)
                footHtml = footHtml.Replace("suma_soles_retorno", suma_soles_retorno)
                footHtml = footHtml.Replace("suma_dolares_retorno", suma_dolares_retorno)
                pnlexportar.Visible = True
            Else
                gridEfectividad.DataSource = Nothing
            End If
            gridEfectividad.DataBind()
        End Sub


        Public Function esVisible(ByVal strValue As Integer) As String
            If strValue = gridEfectividad.PageIndex * gridEfectividad.PageSize Then
                Return headhtml.Replace("AnioMes", AnioMes)
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
            BindData(ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text.Split("-")(1).Trim(), ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Value)
        End Sub

        Protected Sub ddlPeriodo_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlPeriodo.SelectedIndexChanged
            If (ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Value <> "-1") Then
                BindData(ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text.Split("-")(1).Trim(), ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Value)
            End If
        End Sub

        Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
            BindData(ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text.Split("-")(1).Trim(), ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Value)
            Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("NombrePlantillaEfectividadMes"))
            archivExcel.GenerarExcelEfectividadMes(CType(gridEfectividad.DataSource, DataTable), ConfigurationManager.AppSettings("GenFolder") & "EfectividadMes" & ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text & ".xls" _
            , suma_clientes_envio, suma_soles_envio, suma_dolares_envio, suma_clientes_retorno, suma_soles_retorno, suma_dolares_retorno)
            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & "EfectividadMes" & ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text & ".xls")
            Response.TransmitFile(ConfigurationManager.AppSettings("GenFolder") & "EfectividadMes" & ddlPeriodo.Items(ddlPeriodo.SelectedIndex).Text & ".xls")
            Response.End()
        End Sub
    End Class
End Namespace