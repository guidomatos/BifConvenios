Imports System.Data.SqlClient
Namespace BIFConvenios

    Partial Class EdicionErroresProceso
        Inherits System.Web.UI.Page

        Protected ocliente As New Cliente()
        Protected rid As String = ""
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
            If Not Request.Params("id") Is Nothing Then
                rid = CType(Request.Params("id"), String)
            Else
                Exit Sub
            End If
            If Not Page.IsPostBack Then

                Utils.AddSwap(lnkModificar, "Image11", "/BIFConvenios/images/aceptar_on.jpg")
                Utils.AddSwap(lnkCancelar, "Image10", "/BIFConvenios/images/cancelar_on.jpg")

                If Not Request.UrlReferrer Is Nothing Then
                    ViewState("UrlReferer") = Request.UrlReferrer.ToString
                End If

                Dim dr As SqlDataReader = ocliente.GetDetalleErrorRegistro(rid)
                If dr.Read Then
                    lblNombreTrabajador.Text = CType(dr("NombreTrabajador"), String)
                    txtCodigoModular.Text = CType(dr("CodigoModular"), String)
                    txtCodigoReferencia.Text = CType(dr("CodigoReferencia"), String)
                    ddlSituacionLaboral.SelectedIndex = ddlSituacionLaboral.Items.IndexOf(ddlSituacionLaboral.Items.FindByValue(dr("SituacionLaboral")))

                    txtNumeroPagare.Text = CType(dr("NumeroPagare"), String)

                    ddlMoneda.SelectedIndex = ddlMoneda.Items.IndexOf(ddlMoneda.Items.FindByValue(CType(dr("Moneda"), String)))
                    txtCuota.Text = CType(dr("Cuota"), String)
                    txtMontoDescuento.Text = CType(dr("MontoDescuento"), String)

                    'Adicionamos la informacion del año 
                    ddlAnio.Items.Add(New ListItem("-No Asignado-", ""))
                    If CType(dr("Anio"), String).Trim <> "" Then
                        ddlAnio.Items.Add(New ListItem(CType(dr("Anio"), String), CType(dr("Anio"), String)))
                    End If
                    If CType(dr("Anio_periodo"), String).Trim <> "" And CType(dr("Anio_periodo"), String).Trim <> CType(dr("Anio"), String).Trim Then
                        ddlAnio.Items.Add(New ListItem(CType(dr("Anio_periodo"), String), CType(dr("Anio_periodo"), String)))
                    End If

                    If CType(dr("Anio"), String).Trim <> "" Then
                        ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(CType(dr("Anio"), String).Trim))
                    End If

                    'Adicionamos la informacion del mes
                    ddlMes.Items.Add(New ListItem("-No Asignado-", ""))
                    If CType(dr("Mes"), String).Trim <> "" And IsNumeric(CType(dr("Mes"), String).Trim) Then

                        ddlMes.Items.Add(New ListItem(Periodo.GetMonthByNumber(CType(dr("Mes"), String)), CType(dr("Mes"), String)))
                    End If
                    If CType(dr("Mes_periodo"), String).Trim <> "" _
                            And CType(dr("Mes_periodo"), String).Trim <> CType(dr("Mes"), String).Trim _
                            And IsNumeric(CType(dr("Mes_periodo"), String).Trim) Then
                        ddlMes.Items.Add(New ListItem(Periodo.GetMonthByNumber(CType(dr("Mes_periodo"), String)), CType(dr("Mes_periodo"), String)))
                    End If

                    If CType(dr("Mes"), String).Trim <> "" And IsNumeric(CType(dr("Mes"), String).Trim) Then
                        ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(CType(dr("Mes"), String).Trim))
                    End If
                    StatusMsg1.Visible = True
                    StatusMsg1.Message = "<b>" + ocliente.GetError(CType(dr("Estado"), String)) + "</b>"
                    Call AddInsertBehavior(CType(dr("Estado"), String))
                End If
            End If
        End Sub

        'Adiciona el comportamiento de insercion al formulario 
        'de acuerdo al estado actual del registro
        Private Sub AddInsertBehavior(ByVal Estado As String)
            If Estado.Trim = "CB" Or Estado.Trim = "CG" Then
                txtCodigoModular.Attributes.Add("onchange", "JavaScript:changeObjs();")
                txtCodigoReferencia.Attributes.Add("onchange", "JavaScript:changeObjs();")
                txtCuota.Attributes.Add("onchange", "JavaScript:changeObjs();")
                txtMontoDescuento.Attributes.Add("onchange", "JavaScript:changeObjs();")
                txtNumeroPagare.Attributes.Add("onchange", "JavaScript:changeObjs();")
                ddlAnio.Attributes.Add("onchange", "JavaScript:changeObjs();")
                ddlMes.Attributes.Add("onchange", "JavaScript:changeObjs();")
                ddlMoneda.Attributes.Add("onchange", "JavaScript:changeObjs();")
                ddlSituacionLaboral.Attributes.Add("onchange", "JavaScript:changeObjs();")
                aIns.Visible = True
            End If
        End Sub

        Private Sub lnkCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkCancelar.Click
            Response.Redirect(ViewState("UrlReferer"))
        End Sub

        Private Sub lnkModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkModificar.Click
            If Page.IsValid Then
                Try
                    ocliente.ActualizaRegistroErroneo(ddlMoneda.SelectedItem.Value, txtNumeroPagare.Text.Trim, txtCodigoModular.Text.Trim, _
                        txtCodigoReferencia.Text.Trim, ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtCuota.Text.Trim, _
                        ddlSituacionLaboral.SelectedItem.Value, txtMontoDescuento.Text.Trim, rid, False, context.User.Identity.Name)
                    Response.Redirect(ViewState("UrlReferer"))
                Catch ex As Exception
                    Response.Write(Utils.HandleError(ex))
                End Try
            End If
        End Sub

        Private Sub lnkInsertar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkInsertar.Click
            '  If Page.IsValid Then
            Try
                ocliente.ActualizaRegistroErroneo(ddlMoneda.SelectedItem.Value, txtNumeroPagare.Text.Trim, txtCodigoModular.Text.Trim, _
                    txtCodigoReferencia.Text.Trim, ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value, txtCuota.Text.Trim, _
                    ddlSituacionLaboral.SelectedItem.Value, txtMontoDescuento.Text.Trim, rid, True, context.User.Identity.Name)
                Response.Redirect(ViewState("UrlReferer"))
            Catch ex As Exception
                Response.Write(Utils.HandleError(ex))
            End Try
            '  End If
        End Sub

        Private Sub lnkEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEliminar.Click
            Try
                ocliente.DeleteRegistroError(rid, context.User.Identity.Name)
                Response.Redirect(ViewState("UrlReferer"))
            Catch ex As Exception
                Response.Write(Utils.HandleError(ex))
            End Try
        End Sub
    End Class
End Namespace
