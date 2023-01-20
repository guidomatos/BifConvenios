
Imports System.Data
Imports BusinessLogic
Imports BIFConvenios.BL

Namespace BIFConvenios

    Partial Class MantenimientoCuotas
        Inherits System.Web.UI.Page

        Private oCliente As New Cliente()
        Private objClienteBL As New clsClienteBL()

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load

            If Not Page.IsPostBack Then
                Call RebindGrid()
            End If

        End Sub

        Private Sub RebindGrid(Optional ByVal name As String = "")

            ''ViewState("Data_cliente") = oCliente.GetClientes(name)
            ViewState("Data_cliente") = oCliente.GetClientesDS_MantenimientoCuotas(String.Empty) ''.GetClientesDS_MantenimientoCuotas(String.Empty)
            'ViewState("Data_cliente") = objClienteBL.ObtenerListaClienteMantenimientoCuotas(String.Empty)
            Gvw_datos_cliente.DataSource = ViewState("Data_cliente") ''oCliente.GetClientes(name)
            Gvw_datos_cliente.DataBind()
            ''lblNumReg.Text = dgClientes.Items.Count.ToString
        End Sub

        Protected Sub Gvw_datos_cliente_RowUpdating(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
            Gvw_datos_cliente.EditIndex = e.RowIndex
            actualizar_data_row(e)

        End Sub

        Protected Sub Gvw_datos_cliente_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs)

        End Sub

        Protected Sub Gvw_datos_cliente_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)

        End Sub

        Protected Sub Gvw_datos_cliente_RowEditing(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewEditEventArgs)

            Gvw_datos_cliente.EditIndex = e.NewEditIndex
            Gvw_datos_cliente.DataSource = ViewState("Data_cliente")
            Gvw_datos_cliente.DataBind()

        End Sub

        Protected Sub Gvw_datos_cliente_RowCancelingEdit(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
            Gvw_datos_cliente.EditIndex = -1
            Gvw_datos_cliente.DataSource = ViewState("Data_cliente")
            Gvw_datos_cliente.DataBind()
        End Sub

        Protected Sub actualizar_data_row(ByVal e As GridViewUpdateEventArgs)

            Dim ds As DataSet

            Dim cliente_id As Label = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("lbl_cliente_id"), Label)

            Dim ultimo_anio As Label = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("lblultimoanioCargo_cliente"), Label)
            Dim ultimo_mes As Label = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("lblultimoMesCargo_cliente"), Label)

            Dim anio_actual As HiddenField = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("Hidd_AnioCargo_Cliente"), HiddenField)
            Dim mes_actual As HiddenField = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("HiddMesCargo_Cliente"), HiddenField)

            Dim anio_act As TextBox = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("txtAnioCargo_Cliente"), TextBox)
            Dim mes_act As TextBox = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("txtMesCargo_Cliente"), TextBox)
            Dim cb_tipo_cuota As DropDownList = CType(Gvw_datos_cliente.Rows(e.RowIndex).FindControl("ddl_tipocuota"), DropDownList)


            ds = CType(ViewState("Data_cliente"), System.Data.DataSet)

            If (Actualizar_cuota(Convert.ToInt32(cliente_id.Text), _
                                 Convert.ToInt32(ultimo_anio.Text), _
                                 Convert.ToInt32(ultimo_mes.Text), _
                                 Convert.ToInt32(anio_actual.Value), _
                                 Convert.ToInt32(mes_actual.Value), _
                                 Convert.ToInt32(anio_act.Text), _
                                 Convert.ToInt32(mes_act.Text), _
                                 e, _
                                 Convert.ToInt32(cb_tipo_cuota.SelectedValue.Trim()))) Then


                ds.Tables(0).Rows(e.RowIndex)(2) = anio_act.Text
                ds.Tables(0).Rows(e.RowIndex)(3) = mes_act.Text
                ds.Tables(0).Rows(e.RowIndex)(6) = cb_tipo_cuota.SelectedValue
                ds.Tables(0).Rows(e.RowIndex)(7) = cb_tipo_cuota.SelectedItem

            End If

            '((HiddenField)grvActivoFijoInmuebles.Rows[e.RowIndex].FindControl("hidSecuenciaID")).Value

            ViewState("Data_cliente") = ds

            Gvw_datos_cliente.EditIndex = -1
            Gvw_datos_cliente.DataSource = ViewState("Data_cliente")
            Gvw_datos_cliente.DataBind()
        End Sub



        Private Function Actualizar_cuota(ByVal Cliente_id As Integer, _
                        ByVal anio_ultimo_cargo As Integer, _
                        ByVal mes_ultimo_cargo As Integer, _
                        ByVal anio_actual_cargo As Integer, _
                        ByVal mes_actual_cargo As Integer, _
                        ByVal anio_actualizar As Integer, _
                        ByVal mes_actualizar As Integer, _
                        ByVal e As GridViewUpdateEventArgs, _
                        ByVal Tipo_cuota As Integer) As Boolean

            Dim result As Boolean = False


            If validar_actualizacion(anio_ultimo_cargo, mes_ultimo_cargo, anio_actual_cargo, mes_actual_cargo, anio_actualizar, mes_actualizar, e) Then

                Dim Objcuota As New Cuota()
                Dim obj_dlemp As New DLEMP_logic()
                Dim obj_Deuda As New Reporte_Deuda_cliente_IBScs()
                Dim oCliente As New Cliente()
                Dim numero_prestamo As String = ""
                Dim modalidadNew As String = ""
                'Dim modalidad As String = ""
                Dim dsdb2 As DataSet = Nothing
                Dim resultUpd As Integer = -1

                Try
                    If Objcuota.Actualizar_CUOTA(Cliente_id, anio_actualizar, mes_actualizar, Tipo_cuota) And _
                         obj_dlemp.actualizar_dlemp(Cliente_id, anio_actualizar, mes_actualizar) Then

                        'Dim dt As DataTable = obj_Deuda.LISTA_DETALLE_CUOTA_EMPRESA_IBS(Cliente_id, mes_actual_cargo, anio_actual_cargo)
                        'Dim i As Integer = 0
                        'For i = 0 To dt.Rows.Count() - 1
                        '    numero_prestamo = dt.Rows(i).Item("PAGARE").ToString()
                        '    dsdb2 = oCliente.getNuevosDLCCR(numero_prestamo, Cliente_id)
                        '    If (dsdb2.Tables("DLCCR").Rows.Count > 0) Then
                        '        modalidadNew = dsdb2.Tables("DLCCR").Rows(0).Item("MODALIDAD")
                        '    Else
                        '        dsdb2 = oCliente.getReembolsosPLPAD(numero_prestamo, Cliente_id)
                        '        If (dsdb2.Tables("PLPAD").Rows.Count > 0) Then
                        '            modalidadNew = dsdb2.Tables("PLPAD").Rows(0).Item("MODALIDAD")
                        '        Else
                        '            dsdb2 = oCliente.getModificadosMNTOR(numero_prestamo)
                        '            If (dsdb2.Tables("MNTOR").Rows.Count > 0) Then
                        '                modalidadNew = dsdb2.Tables("MNTOR").Rows(0).Item("MODALIDAD")
                        '            Else
                        '                modalidadNew = ""
                        '            End If
                        '        End If
                        '    End If

                        '    Try
                        '        resultUpd = oCliente.UpdateClienteModalidad(Cliente_id, numero_prestamo, modalidadNew)
                        '    Catch ex As Exception
                        '        resultUpd = -1
                        '        Exit For
                        '    End Try
                        'Next

                        'If resultUpd = 1 Then
                        result = True
                        lbl_resultado.Text = "Actualización correcta."
                        'Else
                        '    lbl_resultado.Text = "Error en proceso de Actualización."
                        '    result = False
                        'End If
                    Else
                        lbl_resultado.Text = "Error en proceso de Actualización."
                        result = False
                    End If
                Catch ex As Exception
                    lbl_resultado.Text = "Error en proceso de Actualización."
                End Try
            Else
                lbl_resultado.Text = "Datos no válidos."
            End If

            Return result

        End Function


        Private Function validar_actualizacion(ByVal anio_ultimo_cargo As Integer, _
                        ByVal mes_ultimo_cargo As Integer, _
                        ByVal anio_actual_cargo As Integer, _
                        ByVal mes_actual_cargo As Integer, _
                        ByVal anio_actualizar As Integer, _
                        ByVal mes_actualizar As Integer, _
                        ByVal e As GridViewUpdateEventArgs) As Boolean
            Dim result As Boolean = False

            Try

                If e.RowIndex <> -1 Then


                    Dim fecha_actualizar As DateTime = Convert.ToDateTime("01/" & mes_actualizar.ToString() & "/" & anio_actualizar.ToString())
                    Dim fecha_actual As DateTime = Convert.ToDateTime("01/" & mes_actual_cargo.ToString() & "/" & anio_actual_cargo.ToString())
                    Dim fecha_ultimo As DateTime = Convert.ToDateTime("01/" & mes_ultimo_cargo.ToString() & "/" & anio_ultimo_cargo.ToString())

                    If fecha_actualizar > fecha_actual Then
                        If meses_diferencia(fecha_actualizar, fecha_actual) <= Convert.ToInt32(ConfigurationManager.AppSettings("periodos_adelanto")) AndAlso meses_diferencia(fecha_actualizar, fecha_ultimo) <= Convert.ToInt32(ConfigurationManager.AppSettings("periodos_adelanto")) + 1 Then
                            result = True
                        Else
                            result = False
                        End If
                    Else
                        If fecha_actualizar >= fecha_ultimo Then
                            result = True
                        Else
                            result = False
                        End If
                    End If
                End If

            Catch ex As Exception
                result = False

            End Try
            
            Return result
        End Function


        Private Function meses_diferencia(ByVal fecha1 As DateTime, ByVal fecha2 As DateTime) As Integer

            Dim meses As Integer = 0

            If fecha1.Year = fecha2.Year Then
                meses = fecha1.Month - fecha2.Month
            Else
                If fecha1 > fecha2 Then
                    meses = fecha1.Month + (12 - fecha2.Month)
                Else
                    meses = fecha2.Month + (12 - fecha1.Month)
                End If
            End If
            Return Math.Abs(meses)
        End Function



        Protected Function Get_Tipo_cuota() As DataTable
            Dim dt As DataTable = New Cuota().GetTipoCuota()
            Return dt
        End Function


    End Class

End Namespace



