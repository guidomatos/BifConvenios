Imports BIFConvenios.Container

Namespace BIFConvenios


    Partial Class IngresaDatosArchivoCarga
        Inherits System.Web.UI.Page
        Protected WithEvents lnkModificar As System.Web.UI.WebControls.LinkButton
        Protected WithEvents lnkEliminar As System.Web.UI.WebControls.LinkButton
        Dim idP As String
        Dim pro As String
        Dim lin As String

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



            If Not Page.IsPostBack Then

                If Not Request.Params("id") Is Nothing And _
                    Not Request.Params("pro") Is Nothing And _
                    Not Request.Params("lin") Is Nothing Then

                    idP = Request.Params("id")
                    pro = Request.Params("pro")
                    lin = Request.Params("lin")

                End If

                Dim o As RegistroArchivoNoProcesado = BIFConvenios.ArchivosDescuento.getDatosRegistroNoProcesado(idP, pro, lin)

                lblNombreTrabajador.Text = o.Nombre
                lblNombreTrabajador.Visible = Not (Trim(o.Nombre) = "")
                txtNombreTrabajador.Visible = (Trim(o.Nombre) = "")

                txtMontoDescuento.Text = o.MontoDescuento
                txtNumeroPagare.Text = o.NumeroPagare
                txtCodigoModular.Text = o.CodigoModular

                ddlAnio.Items.Add(New ListItem(CType(o.Anio_periodo, String), CType(o.Anio_periodo, String)))
                ddlMes.Items.Add(New ListItem(Periodo.GetMonthByNumber(o.Mes_Periodo), o.Mes_Periodo))

                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferer") = Request.UrlReferrer.ToString
                End If

                Utils.AddSwap(lnkCancelar, "Image10", "/BIFConvenios/images/cancelar_on.jpg")
            End If
        End Sub

        Private Sub lnkInsertar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkInsertar.Click
            idP = Request.Params("id")
            pro = Request.Params("pro")
            lin = Request.Params("lin")

            BIFConvenios.ArchivosDescuento.AdicionaRegistroErroneoArchivoTexto(idP, pro, lin, _
                ddlMoneda.SelectedItem.Value, txtNumeroPagare.Text, txtCodigoModular.Text, txtCodigoReferencia.Text, _
                ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtCuota.Text, ddlSituacionLaboral.SelectedItem.Value, _
                txtMontoDescuento.Text, IIf(txtNombreTrabajador.Visible, txtNombreTrabajador.Text, lblNombreTrabajador.Text), context.User.Identity.Name)
            Response.Redirect(ViewState("UrlReferer"))
        End Sub

        Private Sub lnkCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ViewState("UrlReferer"))
        End Sub
    End Class
End Namespace
