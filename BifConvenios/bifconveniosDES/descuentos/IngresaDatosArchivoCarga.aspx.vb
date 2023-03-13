Imports BIFConvenios.Container

Namespace BIFConvenios

    Partial Class IngresaDatosArchivoCarga
        Inherits Page
        Protected WithEvents lnkModificar As LinkButton
        Protected WithEvents lnkEliminar As LinkButton
        Dim idP As String
        Dim pro As String
        Dim lin As String

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then

                If Request.Params("id") IsNot Nothing And
                    Request.Params("pro") IsNot Nothing And
                    Request.Params("lin") IsNot Nothing Then

                    idP = Request.Params("id")
                    pro = Request.Params("pro")
                    lin = Request.Params("lin")

                End If

                Dim o As RegistroArchivoNoProcesado = ArchivosDescuento.getDatosRegistroNoProcesado(idP, pro, lin)

                lblNombreTrabajador.Text = o.Nombre
                lblNombreTrabajador.Visible = Not (Trim(o.Nombre) = "")
                txtNombreTrabajador.Visible = (Trim(o.Nombre) = "")

                txtMontoDescuento.Text = o.MontoDescuento
                txtNumeroPagare.Text = o.NumeroPagare
                txtCodigoModular.Text = o.CodigoModular

                ddlAnio.Items.Add(New ListItem(o.Anio_periodo, o.Anio_periodo))
                ddlMes.Items.Add(New ListItem(Periodo.GetMonthByNumber(o.Mes_Periodo), o.Mes_Periodo))

                If Request.UrlReferrer IsNot Nothing Then
                    ViewState("UrlReferer") = Request.UrlReferrer.ToString
                End If

                Utils.AddSwap(lnkCancelar, "Image10", ResolveUrl("/images/cancelar_on.jpg"))
            End If
        End Sub

        Private Sub lnkInsertar_Click(sender As Object, e As EventArgs) Handles lnkInsertar.Click
            idP = Request.Params("id")
            pro = Request.Params("pro")
            lin = Request.Params("lin")

            ArchivosDescuento.AdicionaRegistroErroneoArchivoTexto(idP, pro, lin,
                ddlMoneda.SelectedItem.Value, txtNumeroPagare.Text, txtCodigoModular.Text, txtCodigoReferencia.Text,
                ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtCuota.Text, ddlSituacionLaboral.SelectedItem.Value,
                txtMontoDescuento.Text, IIf(txtNombreTrabajador.Visible, txtNombreTrabajador.Text, lblNombreTrabajador.Text), Context.User.Identity.Name)
            Response.Redirect(ViewState("UrlReferer"))
        End Sub

        Private Sub lnkCancelar_Click(sender As Object, e As EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ViewState("UrlReferer"))
        End Sub
    End Class
End Namespace
