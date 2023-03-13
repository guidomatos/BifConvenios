Imports System.Data.SqlClient
Namespace BIFConvenios

    Partial Class ReporteCronogramaFuturo
        Inherits Page
        Protected WithEvents pnlClientes As Panel
        Protected WithEvents hdDataFile As HtmlInputHidden
        Protected idGenFile As String = ""
        Protected oproc As New Proceso()
        Protected oCliente As New Cliente()
        Protected Codigo_proceso As String = ""
        Protected TipoDocumento As String = ""
        Protected NumeroDocumento As String = ""
        Protected CustomerNumber As String = ""
        Protected Anio_periodo As String = ""
        Protected Mes_Periodo As String = ""
        Protected formatoArchivo As String = ""
        Protected strTipoCliente As String = ""
        Protected situacionTrabajador As String = ""
        Protected modalidad As String = ""
        Protected idP As String = ""
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        'Modificacion: Alan Azabache
        '<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        'End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            'Modificacion: Alan Azabache
            'InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Not Page.IsPostBack Then

                '-------Informacion del año
                ddlAnio.DataSource = oproc.GetAnioProceso()
                ddlAnio.DataBind()
                ddlAnio.Items.Insert(0, New ListItem("-- Todos los años --", ""))
                ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

                'Informacion del mes
                Call BindMonth()
                Call BindGrid()
            End If

        End Sub

        Private Sub BindMonth()
            ddlMes.DataSource = Periodo.GetMonthsByReaderWithOutZero(oproc.GetMesesProceso(ddlAnio.SelectedItem.Value))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Todos los meses --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
        End Sub

        Private Sub BindGrid()
            dgProcesos.DataSource = oproc.GetProcesos(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
            dgProcesos.DataBind()
            lblNumReg.Text = dgProcesos.Items.Count.ToString
            pnlGenArchivos.Visible = False
            pnlGenReporte.Visible = False
        End Sub

        Private Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMes.SelectedIndexChanged
            Call BindGrid()
        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnio.SelectedIndexChanged
            Call BindMonth()
            Call BindGrid()
        End Sub

        'Permite mostrar el elemento de eliminacion si el proceso puede ser eliminado
        Protected Function MostrarElemento(b As Boolean, codigo_proceso As Guid, nombre As String, anio As String, mes As String, fechaProcesoAS400 As String) As String
            Dim returnValue As String = "El proceso no puede ser eliminado"
            If b Then
                returnValue = "<a href='javascript:EliminaProceso(""" + codigo_proceso.ToString + """, """ + nombre.Replace("'", "") + """, """ + anio + """, """ + mes + """, """ + BIFConvenios.Utils.GetFechaCanonica(fechaProcesoAS400) + """ )' " +
                ">Eliminar</a>"
            End If
            Return returnValue
        End Function

        Protected Function MostrarGenFile(b As Boolean, codigo_proceso As Guid, nombre As String, anio As String, mes As String, fechaProcesoAS400 As String) As String
            Dim returnValue As String = "No puede generar archivo"
            If b Then
                returnValue = "<a href='javascript:GenerarArchivo(""" + codigo_proceso.ToString + """, """ + nombre.Replace("'", "") + """, """ + anio + """, """ + BIFConvenios.Periodo.GetMonthByNumber(mes) + """, """ + BIFConvenios.Utils.GetFechaCanonica(fechaProcesoAS400) + """ )' " +
                ">Consultar/Editar</a>"
            End If

            Return returnValue
        End Function

        Protected Function MostrarPaginaConsulta(codigo_proceso As Guid, codigo_cliente As String) As String
            Dim returnValue As String = "No se puede Modificar"

            Using dr As SqlDataReader = oCliente.GetCliente(codigo_cliente)

                If dr.Read Then
                    TipoDocumento = CType(dr("TipoDocumento"), String)
                    NumeroDocumento = CType(dr("NumeroDocumento"), String)

                    CustomerNumber = oCliente.GetCustomerNumber(TipoDocumento, NumeroDocumento)
                End If

                returnValue = "<a href='javascript:GenerarArchivo2(""" + codigo_proceso.ToString + """, """ + CustomerNumber + """ )' " +
                    ">Consultar/Editar</a>"

            End Using

            Return returnValue
        End Function

        Protected Sub RedirectReporte(codigo_proceso_ As String, modalidad As String, situacionTrabajador As String)
            Session.Add("Info", Me.getDataSetListadoCuotasFin(codigo_proceso_, modalidad, situacionTrabajador))
            Response.Redirect(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx?idx=" & codigo_proceso_))
        End Sub


        'Eliminar un proceso de carga
        Private Sub lnkDeleteProcess_Click(sender As Object, e As EventArgs) Handles lnkDeleteProcess.Click
            If hdData.Value <> "" Then
                oproc.DelProceso(hdData.Value.Trim(), Context.User.Identity.Name)
                hdData.Value = ""
                Call BindGrid()
            End If
        End Sub


        Private Sub lnkGenerarArchivo_Click(sender As Object, e As EventArgs) Handles lnkGenerarArchivo.Click
            If hdId.Value IsNot Nothing And hdId.Value.Trim <> "" Then
                oproc.UpdRestauraEstadoInicial(hdId.Value.Trim.Split("|")(0), Context.User.Identity.Name)
                idGenFile = hdId.Value.Trim
                pnlGenArchivos.Visible = True
                hdId.Value = ""
                idP = idGenFile.Trim.Split("|")(0)
                formatoArchivo = idGenFile.Trim.Split("|")(1)
                'strTipoCliente = idGenFile.Trim.Split("|")(2)
                situacionTrabajador = idGenFile.Trim.Split("|")(2)
                modalidad = idGenFile.Trim.Split("|")(3) ' ADD JCHAVEZH 21/10/2014
            End If

        End Sub


        'Private Sub dgProcesos_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgProcesos.ItemCommand
        '    If e.CommandName = "report" Then
        '        'Session.Add("id", e.CommandArgument.ToString())
        '        ' Server.Transfer(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx"))
        '        Session.Add("Info", Me.getDataSetListadoCuotasFin(e.CommandArgument.ToString()))
        '        'MOD NCA 08/07/2014 EA2013-273 OPT. PROCESO CONVENIOS - ARCHIVO DESCUENTOS
        '        Response.Redirect(ResolveUrl("consultas/ContainerListadoConsultaEnvio.aspx?idx=" & e.CommandArgument.ToString()))
        '        'END
        '    End If
        'End Sub

        Private Function getDataSetListadoCuotasFin(id As String, modalidad As String, situacionTrabajador As String) As DataSetListadoCuotasFin
            Dim ds As New DataSetListadoCuotasFin()
            Dim ds3 As DataSet
            Dim Codigo_proceso As String

            ds.Reset()
            Codigo_proceso = id
            Dim dr As SqlDataReader = oCliente.GetInfoClienteProceso(Codigo_proceso)
            If dr.Read Then
                TipoDocumento = CType(dr("TipoDocumento"), String)
                NumeroDocumento = CType(dr("NumeroDocumento"), String)
                Anio_periodo = CType(dr("Anio_periodo"), String)
                Mes_Periodo = CType(dr("Mes_Periodo"), String)
            End If

            Dim ds2 As DataSet = oCliente.GetInfoContactoConvenio(TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo)
            ds.Tables.Add(ds2.Tables(0).Copy)
            ds3 = oCliente.GetListadoCuotasPorVencerCliente2(Codigo_proceso, modalidad, situacionTrabajador)
            'ds3 = oCliente.GetListadoCuotasPorVencerCliente(Codigo_proceso)

            'Copiamos la lista de cuotas
            ds3.Tables(0).TableName = "Cliente"
            ds.Tables.Add(ds3.Tables(0).Copy)
            Return ds
        End Function


        Protected Sub lnkGenerarReporte_Click(sender As Object, e As EventArgs) Handles lnkGenerarReporte.Click
            If hdFilter.Value IsNot Nothing And hdFilter.Value.Trim <> "" Then
                pnlGenReporte.Visible = True
                idP = hdFilter.Value.Trim.Trim.Split("|")(0)
                modalidad = hdFilter.Value.Trim.Trim.Split("|")(1)
                situacionTrabajador = hdFilter.Value.Trim.Trim.Split("|")(2)
            End If
        End Sub

        Protected Sub lnkConsultar_Click(sender As Object, e As EventArgs) Handles lnkConsultar.Click

            'Obtenemos el tipo y el numero de documento del cliente

            'If (hdCodigoCliente.Value.Trim <> "" And _
            '            hdCodigoIBS.Value.Trim <> "") Then

            'Codigo_proceso = hdCodigoCliente.Value
            'CustomerNumber = hdCodigoIBS.Value

            'la carga de la pagina
            'Response.Redirect("ResultadoProcesoCronogramaFuturo.aspx?id=" & Codigo_proceso + "&codIBS=" + CustomerNumber + "&consultar=1")
            'End If

            If (hdCodigoCliente.Value.Trim <> "" And
                        hdCodigo.Value.Trim <> "" And
                        hdTipoDocumento.Value.Trim <> "" And
                        hdNumeroDocumento.Value.Trim <> "") Then

                Codigo_proceso = hdCodigoCliente.Value
                Dim codigo_cliente As String = hdCodigo.Value
                TipoDocumento = hdTipoDocumento.Value
                NumeroDocumento = hdNumeroDocumento.Value

                CustomerNumber = oCliente.GetCustomerNumber(TipoDocumento, NumeroDocumento)

                'la carga de la pagina
                Response.Redirect("ResultadoProcesoCronogramaFuturo.aspx?id=" & Codigo_proceso + "&codIBS=" + CustomerNumber + "&consultar=1")

            End If

        End Sub
    End Class
End Namespace
