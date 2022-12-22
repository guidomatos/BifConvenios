
Imports BusinessLogic
Imports System.Collections
Imports System.Collections.Generic

Partial Class Detalle_cuotas_empresa
    Inherits System.Web.UI.Page

    Dim reporte_deuda As New Reporte_Deuda_cliente_IBScs()


    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

       
        If Not Page.IsPostBack Then

            '' Dim reporte_deuda2 As New Reporte_Deuda_cliente_IBScs()

            Dim lista_deuda_anio As New List(Of Int32)

            'lista_deuda_anio = reporte_deuda.Listar_anio_DLCCR()

            'For Each deuda As Int32 In lista_deuda_anio
            '    ddl_anio.Items.Add(New ListItem(Convert.ToString(deuda + 2000), deuda.ToString()))
            'Next


            'Dim lista_deuda_mes As New List(Of Int32)

            'lista_deuda_mes = reporte_deuda.Listar_mes_DLCCR()

            'For Each deuda2 As Int32 In lista_deuda_mes
            '    ddl_mes.Items.Add(New ListItem(deuda2.ToString()))
            'Next

        End If

    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)


        gvw_deuda_cliente.DataSource = reporte_deuda.lista_deuda_empresa(ddl_mes.SelectedValue.Trim(), ddl_anio.SelectedValue.Trim())
        gvw_deuda_cliente.DataBind()
    End Sub
End Class
