Namespace BIFConvenios

    Partial Class Clientes
        Inherits System.Web.UI.Page

        Private oCliente As New Cliente()

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
                Call RebindGrid()
                Utils.AddSwap(lnkBuscar, "Image1", "/images/buscar_on.jpg")
            End If
        End Sub

        Private Sub RebindGrid(Optional ByVal name As String = "")
            dgClientes.DataSource = oCliente.GetClientes(name)
            dgClientes.DataBind()
            lblNumReg.Text = dgClientes.Items.Count.ToString
        End Sub


        Private Sub lnkDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkDelete.Click
            oCliente.DeleteCliente(hdData.Value, context.User.Identity.Name)
            Call RebindGrid()

        End Sub


        Private Sub lnkBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkBuscar.Click
            RebindGrid(txtNombreCliente.Text)
        End Sub
    End Class
End Namespace
