Imports System.Configuration

Partial Class Banner
    Inherits System.Web.UI.UserControl

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        ' If Not Page.IsPostBack Then
        Dim strPath As String = Request.ApplicationPath

        ctlMenu.MenuData = "<root>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""1"" title=""Empresas"">"
        If BIFConvenios.Utils.isAccessallowed(context.User.Identity.Name, "Clientes.aspx") Then
            ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""2"" title=""Registro de Empresas"" url=""" + strPath + "/Clientes/Clientes.aspx"" />"
        End If
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""3"" title=""Fechas de Cargo por Empresa"" url=""" + strPath + "/Default.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""4"" title=""Actualizar Nuevos Convenios"" url=""" + strPath + "/Clientes/ActualizarTablas.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""27"" title=""Registro de Alertas"" url=""" + strPath + "/Alertas/frmAlertas.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""29"" title=""Asignar Alertas a Empresas"" url=""" + strPath + "/Alertas/frmAsignarAlertas.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""30"" title=""Asignar Responsables"" url=""" + strPath + "/Clientes/frmResponsableOficina.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""5"" title=""Enviar Archivo"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""6"" title=""Obtener Cuotas para Envío a Empresa"" url=""" + strPath + "/CargaGeneracioncf.aspx""></menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""7"" title=""Generar Archivo de Cobranzas"" url=""" + strPath + "/ReporteCronogramaFuturo.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""26"" title=""Visor de Envios Automaticos"" url=""" + strPath + "/frmVisorEnviosAutomaticos.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"

        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""8"" title=""Recibir Archivo"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""9"" title=""Cargar Cuotas Descontadas (Empresa)"" url=""" + strPath + "/descuentos/ProcesarArchivoDescuento.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"

        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""10"" title=""Conciliación de Cobranza"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""11"" title=""Seguimiento de Cobranza"" url=""" + strPath + "/descuentos/ReporteSeguimiento.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""12"" title=""Enviar Cobranza a IBS"" url=""" + strPath + "/descuentos/ReporteProcesoDescuento.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "			<menuitem id=""13"" title=""Post Conciliación"" url=""" + strPath + "/descuentos/ReportePostConciliacion.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	<menuitem id=""28"" title=""Consultar Casillero"" url=""" + strPath + "/reportes/reporteCasillero.aspx"" />"

        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""14"" title=""Consultas"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "	<menuitem id=""15"" title=""Consultar Envíos por Empresa/Periodo"" url=""" + strPath + "/consultas/consultaEnvio.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	<menuitem id=""16"" title=""Consultar Pagos del Empleado"" url=""" + strPath + "/consultas/ConsultaEnvioPorTrabajador.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	<menuitem id=""21"" title=""Consultar Estado de Envíos"" url=""" + strPath + "/consultas/ConsultaEstadoEnvios.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	<menuitem id=""22"" title=""Consultar Estado por Empresa"" url=""" + strPath + "/consultas/ConsultaEstadosPorEmpresa.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""17"" title=""Cuotas"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""18"" title=""Mantenimiento Cuotas"" url=""" + strPath + "/MantenimientoCuotas.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""19"" title=""Listado Cuotas"" url=""" + strPath + "/Detalle_cuotas_empresa.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""20"" title=""Listado Cuotas Negativas"" url=""" + strPath + "/Reporte_Cuotas_negativas.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""23"" title=""Reportes"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""24"" title=""Efectividad de Recaudacion por Empresa"" url=""" + strPath + "/reportes/reporteefectividad.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""25"" title=""Efectividad de Recaudacion por Mes"" url=""" + strPath + "/reportes/reporteefectividadmes.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "<menuitem id=""31"" title=""Mantenimiento"">"
        ctlMenu.MenuData = ctlMenu.MenuData + "	    <menuitem id=""32"" title=""Mantenimiento Parámetro"" url=""" + strPath + "/mantenedor/MantenedorParametro.aspx"" />"
        ctlMenu.MenuData = ctlMenu.MenuData + "</menuitem>"
        ctlMenu.MenuData = ctlMenu.MenuData + "</root>"
        ctlMenu.DataBind()
        'End If

    End Sub

    Public Property Title() As String
        Get
            Return ltrlTitle.Text
        End Get
        Set(ByVal Value As String)
            ltrlTitle.Text = Value
        End Set
    End Property
End Class
