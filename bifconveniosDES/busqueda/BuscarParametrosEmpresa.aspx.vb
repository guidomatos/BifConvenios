Imports BIFConvenios
Namespace BIFConvenios


    Partial Class BuscarParametrosEmpresa
        Inherits System.Web.UI.Page

        Protected oProc As New Proceso()
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
                ddlAnio.DataSource = oProc.GetAnioEnvioArchivoAS400()
                ddlAnio.DataBind()
                ddlAnio.Items.Insert(0, New ListItem("-- Seleccione --", ""))
                ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

                'Informacion del mes
                Call GetMonths()
                Call BindGrid()

            End If

        End Sub

        Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
            Call BindGrid()
        End Sub


        Private Sub GetMonths()
            ddlMes.DataSource = BIFConvenios.Periodo.GetMonthsByReaderWithOutZero(oProc.GetMesesEnvioArchivoAS400(ddlAnio.SelectedItem.Value))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Seleccione --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
            Call GetMonths()
        End Sub

        Private Sub BindGrid()
            dgDatos.DataSource = oProc.GetProcesosEnvioArchivoAS400(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
            dgDatos.DataBind()
            lblNumReg.Text = dgDatos.Items.Count.ToString
            '            If dgDatos.Items.Count = 0 Then
            'TODO: Codigo para hacer invisible las dos ultimas columnas cuando no hay datos
            '           End If
        End Sub
    End Class
End Namespace
