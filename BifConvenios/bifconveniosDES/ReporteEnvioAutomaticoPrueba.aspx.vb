'Imports CrystalDecisions.Shared
'Imports CrystalDecisions.CrystalReports.Engine

Imports Resource
Imports System.IO
Imports System.Data.SqlClient
Imports BIFConvenios.BL
Imports BIFConvenios.BE
Imports Microsoft.Reporting.WebForms
Imports BIFConvenios.Entidad

Partial Class ReporteEnvioAutomaticoPrueba
    Inherits System.Web.UI.Page

    Protected ds As New DataSet()
    Protected idp As String
    'Protected oRepEnvioAutomatico As New RepListadoEnvioAutomatico()
    Protected dsNominaAutomatica As New DataSetEnviosAutomatico()
    Protected oReporteAutomatico As New BIFConvenios.ReporteAutomatico
    Protected oFuncionario As New BIFConvenios.Funcionario()
    Protected strCEFuncionarios As String = String.Empty
    Protected objLogEnvioCorreoBL As New clsLogEnvioCorreosBL
    Protected objLogEnvioCorreo As New clsLogEnvioCorreo

    Protected Sub form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles form1.Load

        If Not Page.IsPostBack Then
            Call ReportesAutomaticos()
        End If

    End Sub

    Public Sub ReportesAutomaticos()

        Dim strDE As String = System.Configuration.ConfigurationManager.AppSettings("mailDefault").ToString()
        Dim pstrRoot As String = System.Configuration.ConfigurationManager.AppSettings("ReportesAutomatico").ToString()
        Dim Empresa, RUC, CodigoIBS, NombreFuncionario, Motivo, FechaEnvio, strComentario, strMail As String
        Dim sCodigoCliente, sCodigoIBS, sCodigoProceso, sMensaje As String
        Dim intResult, intFuncionario, TotalEmpresa, TotalCreditos, Creditos As Integer
        Dim TotalImporte, Importe As Decimal

        Try

            If Request.Params("idp") Is Nothing Then

                'Obtiene la lista de funcionarios
                Dim ds As DataSet = oFuncionario.GetFuncionarios()

                For Each drF As DataRow In ds.Tables("Reportes").Rows

                    intFuncionario = drF("id_funcionario")
                    NombreFuncionario = drF("nombre_funcionario")
                    strCEFuncionarios = drF("email_funcionario")

                    'Valida si el funcionario, tiene asignado empresas
                    Dim dsF As DataSet = oReporteAutomatico.ValidaExistenciaFuncionario(intFuncionario)
                    If dsF.Tables(0).Rows.Count > 0 Then

                        sCodigoCliente = dsF.Tables(0).Rows(0)("Codigo_cliente").ToString()
                        sCodigoIBS = dsF.Tables(0).Rows(0)("Codigo_IBS").ToString()
                        sCodigoProceso = dsF.Tables(0).Rows(0)("codigo_proceso").ToString()

                        'Obtiene la cabecera de la nómina enviada
                        Dim dsRC As DataSet = oReporteAutomatico.ReporteNominaAutomaticaCabecera(intFuncionario)

                        For Each dr As DataRow In dsRC.Tables(0).Rows
                            'TotalEmpresa = dr("TOTAL_EMPRESA")
                            'TotalCreditos = IIf(dr("TOTAL_CREDITOS") Is DBNull.Value, 0, dr("TOTAL_CREDITOS"))
                            'TotalImporte = IIf(dr("TOTAL_IMPORTE") Is DBNull.Value, 0, dr("TOTAL_IMPORTE"))

                            TotalEmpresa = dr("iTotalEmpresas")
                            TotalCreditos = IIf(dr("iNumeroCreditos") Is DBNull.Value, 0, dr("iNumeroCreditos"))
                            TotalImporte = IIf(dr("dTotalImporte") Is DBNull.Value, 0, dr("dTotalImporte"))

                            If TotalEmpresa <> 0 Then
                                dsNominaAutomatica.Cabecera.AddCabeceraRow(TotalEmpresa, TotalCreditos, TotalImporte)
                            End If

                        Next

                        'Obtiene el detalle de la nómina enviada
                        Dim dsRD As DataSet = oReporteAutomatico.ReporteNominaAutomaticaDetalle(intFuncionario)
                        For Each drD As DataRow In dsRD.Tables(0).Rows
                            Empresa = drD("EMPRESA")
                            RUC = drD("RUC")
                            CodigoIBS = drD("CODIGO_IBS")
                            Creditos = drD("NROCREDITOS")
                            Importe = drD("IMPORTE")
                            dsNominaAutomatica.Detalle.AddDetalleRow(Empresa, RUC, CodigoIBS, Creditos, Importe)
                        Next

                        'Obtiene la cabecera de la nómina enviada observada
                        'Dim dsRCO As DataSet = oReporteAutomatico.ReporteNominaAutomaticaCabeceraObservada(intFuncionario)
                        Dim dsRCO As DataSet = oReporteAutomatico.ReporteNominaAutomaticaCabecera(intFuncionario)
                        For Each drCO As DataRow In dsRCO.Tables(0).Rows
                            'TotalEmpresa = drCO("TOTAL_EMPRESA")
                            'TotalCreditos = IIf(drCO("TOTAL_CREDITOS") Is DBNull.Value, 0, drCO("TOTAL_CREDITOS"))
                            'TotalImporte = IIf(drCO("TOTAL_IMPORTE") Is DBNull.Value, 0, drCO("TOTAL_IMPORTE"))
                            TotalEmpresa = drCO("iTotalEmpresas")
                            TotalCreditos = IIf(drCO("iNumeroCreditos") Is DBNull.Value, 0, drCO("iNumeroCreditos"))
                            TotalImporte = IIf(drCO("dTotalImporte") Is DBNull.Value, 0, drCO("dTotalImporte"))

                            If TotalEmpresa <> 0 Then
                                dsNominaAutomatica.CabeceraObservada.AddCabeceraObservadaRow(TotalEmpresa, TotalCreditos, TotalImporte)
                            End If

                        Next

                        'Obtiene el detalle de la nómina enviada observada
                        ' Dim dsRDO As DataSet = oReporteAutomatico.ReporteNominaAutomaticaDetalleObservada(intFuncionario)
                        Dim dsRDO As DataSet = oReporteAutomatico.ReporteNominaAutomaticaDetalle(intFuncionario)
                        For Each drDO As DataRow In dsRDO.Tables(0).Rows
                            Empresa = drDO("EMPRESA")
                            RUC = drDO("RUC")
                            CodigoIBS = drDO("CODIGO_IBS")
                            Creditos = drDO("NROCREDITOS")
                            Importe = drDO("IMPORTE")
                            Motivo = drDO("MOTIVO")
                            FechaEnvio = drDO("FECHA_ENVIO")
                            dsNominaAutomatica.DetalleObservado.AddDetalleObservadoRow(Empresa, RUC, CodigoIBS, Creditos, Importe, Motivo, FechaEnvio)
                        Next

                        'Se añade el dataset al objeto set del crystal report
                        'oRepEnvioAutomatico.SetDataSource(dsNominaAutomatica)

                        Dim rdsCabecera As New ReportDataSource()
                        rdsCabecera.Name = "DataSet1"

                        If Not IsNothing(dsNominaAutomatica) Then
                            If dsNominaAutomatica.Tables.Count > 0 Then
                                rdsCabecera.Value = dsNominaAutomatica.Tables(0)
                            End If
                        End If

                        ReportViewer1.LocalReport.DataSources.Add(rdsCabecera)

                        'Se obtiene el nombre del archivo excel a generar
                        Dim name As String = BIFConvenios.Utils.getWebServerDateId() & ".xls"

                        'Se obtiene el la ruta a exportar
                        Dim intAnio As Integer = Year(DateTime.Now)
                        Dim intMonth As Integer = Month(DateTime.Now)
                        Dim strPathFile As String = String.Empty

                        intResult = clsFiles.VerifyPath(pstrRoot, intAnio, intMonth, strPathFile, NombreFuncionario)

                        If intResult = 1 Then

                            'Obtiene la ruta completa, para guardar el archivo excel
                            Dim strFullName As String = strPathFile + "\\" + name

                            'Procedimiento para el grabado del archivo excel
                            'With oRepEnvioAutomatico.ExportOptions
                            '    .ExportDestinationType = ExportDestinationType.DiskFile
                            '    .ExportFormatType = ExportFormatType.Excel
                            '    .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
                            '    .DestinationOptions.DiskFileName = strFullName
                            'End With

                            'oRepEnvioAutomatico.Export()

                            'exporta a archivo fisico
                            Dim byteFileRdlc = BIFUtils.WS.Utils.RDLC_ExportarExcel(ReportViewer1)

                            If Not BIFUtils.WS.Utils.RDLC_ExportFile(strFullName, byteFileRdlc) Then
                                Throw New Exception("Error al crear archivo excel en servidor")
                            End If

                            'Envio Correo a Funcionarios
                            strCEFuncionarios = System.Configuration.ConfigurationManager.AppSettings("mailFuncionarios").ToString()

                            strComentario = "El Reporte de Nóminas, se ha generado de forma automática a través del Sistema BIFConvenios"

                            BIFConvenios.Utils.SendNotification(strDE, strCEFuncionarios, strCEFuncionarios,
                                               ConfigurationManager.AppSettings("mailCFSubject").ToString(), strComentario, strFullName,
                                              notifyTo:=(ConfigurationManager.AppSettings("mailSender").ToString()))

                            'Inserta en la tabla Log_Envio_Correo
                            sMensaje = "Envío de Reporte exitoso."

                            objLogEnvioCorreo.iCodigoCliente = sCodigoCliente
                            objLogEnvioCorreo.iCodigoIBS = sCodigoIBS
                            objLogEnvioCorreo.iTipoEnvioCorreoId = 3
                            objLogEnvioCorreo.vMensajeProceso = sMensaje
                            objLogEnvioCorreo.iEstado = 1
                            objLogEnvioCorreo.vUsuarioCreacion = Context.User.Identity.Name
                            objLogEnvioCorreo.vCodigoProceso = sCodigoProceso
                            objLogEnvioCorreo.dFechaCreacion = FormatDateTime(Now, DateFormat.ShortDate)
                            objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)

                            'Limpia los datatables del dataset de dsNominaAutomatica
                            dsNominaAutomatica.Cabecera.Clear()
                            dsNominaAutomatica.Detalle.Clear()
                            dsNominaAutomatica.CabeceraObservada.Clear()
                            dsNominaAutomatica.DetalleObservado.Clear()

                        End If

                    End If

                Next

            End If

        Catch ex As Exception

            'Inserta en la tabla Log_Envio_Correo
            sMensaje = "Error en el envío de Reportes."

            objLogEnvioCorreo.iCodigoCliente = sCodigoCliente
            objLogEnvioCorreo.iCodigoIBS = sCodigoIBS
            objLogEnvioCorreo.iTipoEnvioCorreoId = 3
            objLogEnvioCorreo.vMensajeProceso = sMensaje
            objLogEnvioCorreo.iEstado = 1
            objLogEnvioCorreo.vUsuarioCreacion = Context.User.Identity.Name
            objLogEnvioCorreo.vCodigoProceso = sCodigoProceso
            objLogEnvioCorreo.dFechaCreacion = FormatDateTime(Now, DateFormat.ShortDate)
            objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)


        End Try

    End Sub

End Class


