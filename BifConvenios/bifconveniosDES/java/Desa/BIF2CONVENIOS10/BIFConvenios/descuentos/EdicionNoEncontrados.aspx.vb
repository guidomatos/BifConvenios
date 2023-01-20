Imports System.Data.SqlClient

Namespace BIFConvenios
    Partial Class EdicionNoEncontrados
        Inherits System.Web.UI.Page
        Protected oCliente As New Cliente()
        Protected eid As String = ""
        Protected pid As String = ""

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

            If Not Request.Params("id") Is Nothing And Not Request.Params("pid") Is Nothing Then
                eid = CType(Request.Params("id"), String)
                pid = CType(Request.Params("pid"), String)
            Else
                Exit Sub
            End If
            If Not Page.IsPostBack Then
                Dim dr As SqlDataReader = oCliente.GetRegistroErrorEnvio(pid, eid)
                If dr.Read Then
                    lblConvenio.Text = CType(dr("Convenio"), String)
                    lblMoneda.Text = CType(dr("DLMO"), String)
                    lblNumeroPagare.Text = CType(dr("DLNP"), String)
                    lblCodigoModular.Text = CType(dr("DLCM"), String)
                    lblNombreTrabajador.Text = CType(dr("DLNE"), String)
                    lblCodigoReferencia.Text = CType(dr("DLCR"), String)
                    lblAnio.Text = CType(dr("DLAP"), String)
                    lblMes.Text = Periodo.GetMonthByNumber(CType(dr("DLMP"), String))
                    lblSituacionLaboral.Text = CType(dr("NombreStatusTrabajador"), String)
                    lblCuota.Text = CType(dr("DLIC"), String)
                    StatusMsg1.Message = CType(dr("Estado"), String)
                    StatusMsg1.Visible = True
                End If
                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferer") = Request.UrlReferrer.ToString
                End If

                Utils.AddSwap(lnkModificar, "Image1", "/BIFConvenios/images/aceptar_on.jpg")
                Utils.AddSwap(lnkCancelar, "Image3", "/BIFConvenios/images/cancelar_on.jpg")
            End If
        End Sub

        Private Sub lnkCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ViewState("UrlReferer"))
        End Sub

        Private Sub lnkEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEliminar.Click
            Try
                oCliente.EliminarRegistroErrorEnvio(eid, context.User.Identity.Name)
                Response.Redirect(ViewState("UrlReferer"))
            Catch ex As Exception
                Response.Write(Utils.HandleError(ex))
            End Try
        End Sub

        Private Sub lnkModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkModificar.Click
            If Page.IsValid Then
                Try
                    Dim myConnection As New SqlConnection(GetDBConnectionString)
                    myConnection.Open()
                    oCliente.ActualizaDescuentoRegistroErrorEnvio(myConnection, eid, txtMontoDescuento.Text.Trim, context.User.Identity.Name, Request.Params("pid"), hdInformacionDescuentoRegistro.Value.Split("|")(0), hdInformacionDescuentoRegistro.Value.Split("|")(1))
                    myConnection.Close()
                    Response.Redirect(ViewState("UrlReferer"))
                Catch ex As Exception
                    Response.Write(Utils.HandleError(ex))
                End Try
                Response.Redirect(ViewState("UrlReferer"))
            End If
        End Sub
    End Class
End Namespace