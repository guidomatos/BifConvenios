Imports System.Data.SqlClient
Imports BIFConvenios.BE
Imports BIFConvenios.BL

Namespace BIFConvenios

    Partial Class RegistroCliente
        Inherits Page
        Protected oproc As New Proceso()
        Protected idGenFile As String = ""
        Protected formatoArchivo As String = ""
        Protected strTipoCliente As String = ""
        Protected situacionTrabajador As String = ""
        Protected idP As String = ""
        Protected modalidad As String = ""
        Protected ImporteHistorico As Decimal

        Protected objArchivosConvenioBL As New clsArchivosConveniosBL()
        Protected objArchivosConvenio As New clsArchivosConvenios()

        Protected Documento As String
        Protected Trabajador As String
        Protected Pagare As String
        Protected EstadoTrabajador As String
        Protected ZonaUse As String

        Protected strCodProceso As String
        Protected strCodIBS As String


        Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

            strCodProceso = CType(Session("strCodProceso"), String)
            strCodIBS = CType(Session("strCodIBS"), String)

            If Not Page.IsPostBack Then

                Dim dr As SqlDataReader
                Dim ShowGrid As Boolean

                txtFecha.Text = Date.Now.ToShortDateString
                txtFecCargo.Text = Date.Now.ToShortDateString

                dr = oproc.InformeProceso(strCodProceso)
                'Utils.AddSwap(lnkBack, "Image1", Request.ApplicationPath + "/images/regresar_on.jpg")

                If dr.Read Then
                    ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                    ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                    ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                    'ltrlFechaProceso.Text = CType(dr("Fecha_CargaAS400"), String)
                    ltrlProcesoAS400.Text = Utils.GetFechaCanonica(CType(dr("Fecha_ProcesoAS400"), String))

                    ShowGrid = CType(dr("ShowGrid"), Boolean)
                End If

                Call LimpiarTextbox()

            End If

        End Sub

        Protected Sub btnGrabarCliente_ServerClick(sender As Object, e As EventArgs) Handles btnGrabarCliente.ServerClick
            Dim oProceso As New Proceso()
            Dim Codigo_proceso As String = CType(Session("strCodProceso"), String)

            Dim Pagare, Modular, ApellidoPaterno, ApellidoMaterno, Trabajador, Referencia, Documento As String
            Dim Cuota, CuotasInformada, CuotasPactada, CuotasPagada As Integer
            Dim Importe, MontoOriginal, Deuda As Decimal
            Dim FechaDesembolso, FechaCargo As DateTime

            If Page.IsValid Then

                Pagare = txtPagare.Value.Trim()
                Modular = TxtModular.Value.Trim()
                ApellidoPaterno = txtApellidoP.Value.Trim()
                ApellidoMaterno = txtApelllidoM.Value.Trim()
                Trabajador = txtNombre.Value.Trim()
                Referencia = String.Empty
                Importe = CType(txtImporteNew.Value.Trim(), Decimal)
                Cuota = 0
                Deuda = 0.0
                Documento = txtDocumento.Value.Trim()

                MontoOriginal = CType(txtMontoOriginal.Value.Trim(), Decimal)
                CuotasInformada = 0

                CuotasPactada = txtCuotaPactada.Value.Trim()
                CuotasPagada = txtCuotaPagada.Value.Trim()

                Dim Script As String
                If (txtFecha.Text.Length = 10) Then

                    If (txtFecCargo.Text.Length = 10) Then

                        If (CuotasPagada <= CuotasPactada) Then

                            FechaDesembolso = txtFecha.Text.Trim()
                            FechaCargo = txtFecCargo.Text.Trim()

                            'Registra en la tabla ClienteCuota
                            oProceso.InsertaClienteCuota(CType(Session("strCodProceso"), String), Pagare, Modular, ApellidoPaterno, ApellidoMaterno, Trabajador, Referencia, Importe,
                                                    Cuota, Deuda, Documento, FechaDesembolso, MontoOriginal, CuotasInformada, FechaCargo, CuotasPactada, CuotasPagada)

                            LimpiarTextbox()

                            Script = "<script>alert('El registro ha sido grabado satisfactoriamente.')</script>"
                            ScriptManager.RegisterStartupScript(Me, Me.GetType(), "Script", Script, False)

                        Else
                            Script = "alert('La Cuota Pagada no puede ser mayor la Cuota Pactada');"
                            ClientScript.RegisterStartupScript(Me.GetType(), "script", Script, True)
                        End If

                    Else
                        Script = "alert('El formato de la Fecha de Cuota es incorrecta, debe tener el siguiente formato: dd/MM/yyyy');"
                        ClientScript.RegisterStartupScript(Me.GetType(), "script", Script, True)
                    End If

                Else
                    Script = "alert('El formato de la Fecha de Desembolso es incorrecta, debe tener el siguiente formato: dd/MM/yyyy');"
                    ClientScript.RegisterStartupScript(Me.GetType(), "script", Script, True)
                End If


            End If

        End Sub

        Sub LimpiarTextbox()
            txtFecha.Text = Date.Now.ToShortDateString
            txtFecCargo.Text = Date.Now.ToShortDateString

            txtPagare.Value = String.Empty
            TxtModular.Value = String.Empty
            txtApellidoP.Value = String.Empty
            txtApelllidoM.Value = String.Empty
            txtNombre.Value = String.Empty
            txtImporteNew.Value = String.Empty
            txtDocumento.Value = String.Empty
            txtMontoOriginal.Value = String.Empty
            txtCuotaPactada.Value = String.Empty
            txtCuotaPagada.Value = String.Empty
        End Sub

        Protected Sub btnCerrar_Click(sender As Object, e As EventArgs) Handles btnCerrar.Click
            Response.Redirect("ResultadoProcesoCronogramaFuturo.aspx?id=" + CType(Session("strCodProceso"), String) & "&codIBS=" + CType(Session("strCodIBS"), String) & "&consultar=0")
        End Sub
    End Class

End Namespace


