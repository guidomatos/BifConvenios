Imports BIFConvenios
Partial Class selectFilterReport
    Inherits Page

    Protected idP As String
    Protected nombre As String
    Protected anio As String
    Protected mes As String
    Protected oProceso As New Proceso()
    Protected fechaProcesoAS400 As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            idP = Request.Params("id")
            nombre = Request.Params("nombre")
            anio = Request.Params("anio")
            mes = Request.Params("mes")
            fechaProcesoAS400 = Request.Params("fechaProcesoAS400")

            ddlEstadoTrabajador.DataSource = oProceso.GetEstadosTrabajador(idP)
            ddlEstadoTrabajador.DataBind()
            ddlEstadoTrabajador.Items.Insert(0, New ListItem("Todos", "-"))

            lstModalidad.DataSource = oProceso.GetModalidad(idP)
            lstModalidad.DataBind()
            lstModalidad.Items.Insert(0, New ListItem("Todos", "-"))

        End If

    End Sub
End Class
