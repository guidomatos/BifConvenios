Imports BIFConvenios
Partial Class selectFilterReport
    Inherits System.Web.UI.Page

    Protected idP As String
    Protected nombre As String
    Protected anio As String
    Protected mes As String
    Protected oProceso As New Proceso()
    Protected fechaProcesoAS400 As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            idP = CType(Request.Params("id"), String)
            nombre = CType(Request.Params("nombre"), String)
            anio = CType(Request.Params("anio"), String)
            mes = CType(Request.Params("mes"), String)
            fechaProcesoAS400 = CType(Request.Params("fechaProcesoAS400"), String)

            ddlEstadoTrabajador.DataSource = oProceso.GetEstadosTrabajador(idP)
            ddlEstadoTrabajador.DataBind()
            ddlEstadoTrabajador.Items.Insert(0, New ListItem("Todos", "-"))

            lstModalidad.DataSource = oProceso.GetModalidad(idP)
            lstModalidad.DataBind()
            lstModalidad.Items.Insert(0, New ListItem("Todos", "-"))

        End If

    End Sub
End Class
