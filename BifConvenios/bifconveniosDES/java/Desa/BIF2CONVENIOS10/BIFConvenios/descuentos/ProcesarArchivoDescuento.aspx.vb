Imports BIFConvenios.BL

Imports Resource

Namespace BIFConvenios

    Partial Class ProcesarArchivoDescuento
        Inherits System.Web.UI.Page
        Protected oProc As New Proceso()
        Protected objProcesoBL As New clsProcesoBL()

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
            If Not Page.IsPostBack Then
                '-------Informacion del año
                ddlAnio.DataSource = oProc.GetAnioProcesoEspera()
                ddlAnio.DataBind()
                ddlAnio.Items.Insert(0, New ListItem("-- Todos --", ""))
                ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

                'Informacion del mes
                Call GetMonths()
                Call BindGrid()
            End If
        End Sub

        'Enlaza la informacion a datos
        Private Sub BindGrid()
            pnlInputProceso.Visible = True
            pnlMensaje.Visible = False

            Try
                'gvDatosCarga.DataSource = objProcesoBL.ObtenerListaProcesosEsperaArchivoDescuento(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
                gvDatosCarga.DataSource = objProcesoBL.ObtenerListaProcesosEsperaArchivoDescuentoByNombreCliente(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtCliente.Text)
                gvDatosCarga.DataBind()
            Catch ex As HandledException
                If ex.ErrorTypeId = -400 Then
                    pnlInputProceso.Visible = False
                    pnlMensaje.Visible = True

                    lblMensaje.Text = "Error: " + ex.ErrorMessageFull
                Else
                    pnlInputProceso.Visible = False
                    pnlMensaje.Visible = True

                    lblMensaje.Text = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "Listar Procesos Espera Descuento").Replace("&2", ex.ErrorTypeId).Replace("&3", ex.ErrorMessageFull)
                End If
            End Try
        End Sub

        Private Sub GetMonths()
            ddlMes.DataSource = Periodo.GetMonthsByReader(oProc.GetMesesProcesoEspera(ddlAnio.SelectedItem.Value))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Todos --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
        End Sub

        'Obtenemos el mensaje que se mostrara en el boton de proceso 
        Protected Function GetMensajeProceso(ByVal Estado As String) As String
            Dim returnValue As String

            If Estado = "G1" Or Estado = "EG" Or Estado = "ED" Then
                returnValue = "Procesar archivo de cuotas (Empresa)"
            ElseIf Estado = "D2" Then
                returnValue = "Procesar archivo de cuotas (Empresa)"
            ElseIf Estado = "D0" Then
                returnValue = "Procesar archivo de cuotas (Empresa)"
            ElseIf Estado = "A3" Then
                returnValue = "Procesar archivo de cuotas (Empresa)"
            End If
            Return returnValue
        End Function

        Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
            Call BindGrid()
        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
            GetMonths()
        End Sub

        Protected Sub gvDatosCarga_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles gvDatosCarga.PageIndexChanging
            gvDatosCarga.PageIndex = e.NewPageIndex

            BindGrid()
        End Sub

        Protected Sub btnBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
            Call BindGrid()
        End Sub
    End Class
End Namespace
