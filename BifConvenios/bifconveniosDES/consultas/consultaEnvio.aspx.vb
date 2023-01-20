Partial Class consultaEnvio
    Inherits System.Web.UI.Page
    Protected WithEvents pnlControls As System.Web.UI.WebControls.Panel
    Protected oproc As New BIFConvenios.Proceso()


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

        'Put user code to initialize the page here

        If Not Page.IsPostBack Then
            '-------Informacion del año
            ddlAnio.DataSource = oproc.GetAnioEnvioArchivoAS400()
            ddlAnio.DataBind()
            ddlAnio.Items.Insert(0, New ListItem("-- Todos los años --", ""))
            ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

            'Informacion del mes
            ddlMes.DataSource = BIFConvenios.Periodo.GetMonthsByReaderWithOutZero(oproc.GetMesesEnvioArchivoAS400(Now.Year.ToString))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Todos los meses --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
            Call BindGrid()
        End If
    End Sub

    Private Sub BindGrid()
        dgProcesos.DataSource = oproc.GetProcesosEnvioArchivoAS400(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
        dgProcesos.DataBind()
        lblNumReg.text = dgProcesos.Items.Count.ToString
    End Sub


    Private Sub ddlAnio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
        Call BindGrid()
    End Sub

    Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
        Call BindGrid()
    End Sub
End Class
