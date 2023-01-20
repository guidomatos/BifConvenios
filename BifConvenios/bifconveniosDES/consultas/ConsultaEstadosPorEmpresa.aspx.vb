Namespace BIFConvenios
    Partial Class consultas_ConsultaEstadosPorEmpresa
        Inherits System.Web.UI.Page

        Protected oproc As New Proceso()

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not Page.IsPostBack Then

                ddlFuncionario.DataSource = FuncionarioConvenios.GetFuncionarioConvenios
                ddlFuncionario.DataBind()
                ddlFuncionario.Items.Insert(0, New ListItem("-- Todos los funcionarios --", "0"))

                'Informacion del año

                'Informacion del mes
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-6).Month)) & " - " & DateTime.Now.AddMonths(-6).Year, DateTime.Now.AddMonths(-6).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-5).Month)) & " - " & DateTime.Now.AddMonths(-5).Year, DateTime.Now.AddMonths(-5).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-4).Month)) & " - " & DateTime.Now.AddMonths(-4).Year, DateTime.Now.AddMonths(-4).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-3).Month)) & " - " & DateTime.Now.AddMonths(-3).Year, DateTime.Now.AddMonths(-3).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-2).Month)) & " - " & DateTime.Now.AddMonths(-2).Year, DateTime.Now.AddMonths(-2).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(-1).Month)) & " - " & DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.Month)) & " - " & DateTime.Now.Year, DateTime.Now.Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(1).Month)) & " - " & DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month))
                ddlMes.Items.Add(New ListItem(primeraMayuscula(MonthName(DateTime.Now.AddMonths(2).Month)) & " - " & DateTime.Now.AddMonths(2).Year, DateTime.Now.AddMonths(2).Month))

                ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))

                txtMes.Text = CType(Request.Params("txtMes"), String)
                txtAnio.Text = CType(Request.Params("txtAnio"), String)
                txtIdFuncionario.Text = CType(Request.Params("txtIdFuncionario"), String)
                txtNombreEmpresa.Text = CType(Request.Params("txtNombreEmpresa"), String)

            End If
        End Sub
        Private Function primeraMayuscula(ByVal palabra As String) As String
            Return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(palabra)
        End Function
        Protected Sub lnkBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBuscar.Click
            'If Not txtEmpresa.Text.Trim = "" Then
            'lblMensaje.Text = ""
            Dim ds As DataSet
            'se guardan los campos utilizados para realizar el filtro
            txtMes.Text = ddlMes.Items(ddlMes.SelectedIndex).Value
            txtAnio.Text = ddlMes.Items(ddlMes.SelectedIndex).Text.Substring(ddlMes.Items(ddlMes.SelectedIndex).Text.Length - 4)
            txtIdFuncionario.Text = ddlFuncionario.Items(ddlFuncionario.SelectedIndex).Value
            txtNombreEmpresa.Text = txtEmpresa.Text

            dgConsultaEstado.CurrentPageIndex = 0
            ds = oproc.consultaEstadoPorEmpresa(txtEmpresa.Text, txtIdFuncionario.Text, txtAnio.Text, txtMes.Text)
            dgConsultaEstado.DataSource = ds
            dgConsultaEstado.DataBind()
            dgConsultaEstado.Visible = True

            dvData.Visible = True
            numRegistros.Visible = True
            botonera.Visible = True

            lblNumReg.Text = ds.Tables(0).Rows.Count
            'Else
            'lblMensaje.Text = "Ingrese el nombre de la empresa a consultar."
            'End If



        End Sub


        Protected Sub exportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles exportar.Click
            Dim ds As DataSet = oproc.consultaEstadoPorEmpresa(txtNombreEmpresa.Text, txtIdFuncionario.Text, txtAnio.Text, txtMes.Text)
            Dim archivExcel As New ExcelWriter(ConfigurationManager.AppSettings("Plantillas") & ConfigurationManager.AppSettings("NombrePlantillaConsulta"))
            Dim nombreArchivo As String
            nombreArchivo = "exportarConsultaEstadosPorEmpresa.xls"
            archivExcel.GenerarExcelConsultarEstadoEmpresa(ds.Tables(0), ConfigurationManager.AppSettings("GenFolder") & nombreArchivo)

            Response.ContentType = "application/vnd.ms-excel"
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & nombreArchivo)
            Response.TransmitFile(ConfigurationManager.AppSettings("GenFolder") & nombreArchivo)
            Response.End()
        End Sub

        Protected Sub dgConsultaEstado_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dgConsultaEstado.PageIndexChanged
            dgConsultaEstado.CurrentPageIndex = e.NewPageIndex
            dgConsultaEstado.DataSource = oproc.consultaEstadoPorEmpresa(txtEmpresa.Text, txtIdFuncionario.Text, txtAnio.Text, txtMes.Text)
            dgConsultaEstado.DataBind()
        End Sub
    End Class
End Namespace
