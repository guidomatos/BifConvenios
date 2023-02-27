Namespace BIFConvenios
    Partial Class ConsultaEnvioPorTrabajador
        Inherits System.Web.UI.Page
        Protected oCliente As New Cliente()
        Protected oproc As New Proceso()

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
                ddlEmpresa.DataSource = oCliente.GetClientesProcesoEnviado()
                ddlEmpresa.DataBind()

                '-------Informacion del año
                ddlAnio.DataSource = oproc.GetAnioEnvioArchivoAS400()
                ddlAnio.DataBind()
                ddlAnio.Items.Insert(0, New ListItem("-- Todos los años --", ""))
                ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

                Call BindMonth()
                Call bindGrid()

                Ordenar_DropDownList(ddlAnio)

            End If
        End Sub
        Private Sub Ordenar_DropDownList(ByRef ddList As DropDownList)
            Dim i As Integer
            Dim count As Integer = ddList.Items.Count
            Dim a As Integer = 0
            While count > 1
                For i = 0 To count - 2
                    If String.Compare(ddList.Items(i).Text, ddList.Items(i + 1).Text) > 0 Then
                        Reemplazar_DropDownListItems(ddList, i, i + 1)
                    End If
                Next
                count = count - 1
            End While
        End Sub
        Private Sub Reemplazar_DropDownListItems(ByRef ddList As DropDownList, ByVal index1 As Integer, ByVal index2 As Integer)
            Dim TempLI As New ListItem
            TempLI = ddList.Items(index2)
            ddList.Items.RemoveAt(index2)
            ddList.Items.Insert(index1, TempLI)
        End Sub

        'Enlaza la informacion a la grilla de datos para visualizacion
        Private Sub bindGrid()
            If ddlEmpresa.Items.Count > 0 Then
                dgProcesos.DataSource = oCliente.GetClienteCuotaEnvio(ddlEmpresa.SelectedItem.Value, _
                                    ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtTrabajador.Text)
            dgProcesos.DataBind()
                lblNumReg.Text = dgProcesos.Items.Count.ToString
            End If

        End Sub

        Private Sub BindMonth()
            'Informacion del mes
            ddlMes.DataSource = BIFConvenios.Periodo.GetMonthsByReaderWithOutZero(oproc.GetMesesEnvioArchivoAS400(ddlAnio.SelectedItem.Value))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Todos los meses --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))

        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
            Call BindMonth()
            Call bindGrid()
        End Sub

        Private Sub lnkBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBuscar.Click
            Call bindGrid()
        End Sub

        Private Sub ddlEmpresa_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlEmpresa.SelectedIndexChanged
            Call bindGrid()
        End Sub

        Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
            Call bindGrid()
        End Sub
    End Class
End Namespace
