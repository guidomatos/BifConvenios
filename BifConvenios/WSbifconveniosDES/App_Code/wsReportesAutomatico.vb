Imports System.Web.Services
Imports System.Data
Imports CrystalDecisions.Shared
Imports Resource
Imports BIFConvenios.BL
Imports BIFConvenios.BE

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<CompilerServices.DesignerGenerated()>
Public Class wsReportesAutomatico

    Inherits WebService

    Protected ds As New DataSet()
    Protected idp As String

    Protected oRepEnvioAutomatico As New RepListadoEnvioAutomatico()
    Protected dsNominaAutomatica As New DataSetEnviosAutomatico()

    Protected objSystemParametersBL As New clsSystemParametersBL()
    Protected _dtParametrosEnvioMail As New DataTable()

    'Protected oReporteAutomatico As New BIFConvenios.ReporteAutomatico
    'Protected oFuncionario As New BIFConvenios.Funcionario()

    Protected strCEFuncionarios As String = String.Empty
    Protected objProcesoAutomaticoBL As New clsProcesosAutomaticosBL()
    Protected objProcesoAutomatico As New clsProcesosAutomaticos
    Protected objLogEnvioCorreoBL As New clsLogEnvioCorreosBL()
    Protected objLogEnvioCorreo As New clsLogEnvioCorreo
    Protected objEventoSistemaBL As New clsEventoSistemaBL()
    Protected objEventoSistema As New clsEventoSistema
    Protected objUtils As New clsUtils
    Protected objFuncionarioBL As New ClsFuncionarioBL
    Protected objReporteAutomaticoBL As New ClsReporteAutomaticoBL

#Region "Log"

    <WebMethod()>
    Public Function RegistrarLogEventoSistema(pstrHilo As String, penuNivel As Integer, pstrAccion As String, pstrMensaje As String, pstrExcepcion As String, pstrUsuario As String) As Integer

        Try
            objEventoSistema.Hilo = pstrHilo
            objEventoSistema.Nivel = penuNivel.ToString()
            objEventoSistema.Accion = pstrAccion
            objEventoSistema.Mensaje = pstrMensaje
            objEventoSistema.Excepcion = pstrExcepcion
            objEventoSistema.Usuario = pstrUsuario

            objEventoSistema.IdEventoSistema = objEventoSistemaBL.Insertar(objEventoSistema)

            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

    <WebMethod()>
    Public Function RegistrarLogEnvio(pintCodigoProcesoAutomatico As Integer, pintCodigoCliente As Integer, pintCodigoIBS As Integer, penuTipoEnvio As Integer, pstrCodigoProceso As String, pintAnioPeriodo As Integer, pintMesPeriodo As Integer, pstrMensaje As String, penuEstado As enumLogEnvioCorreo, pstrUsuario As String) As Integer

        Try
            objLogEnvioCorreo.iProcesoAutomaticoId = pintCodigoProcesoAutomatico
            objLogEnvioCorreo.iCodigoCliente = pintCodigoCliente
            objLogEnvioCorreo.iCodigoIBS = pintCodigoIBS
            objLogEnvioCorreo.iTipoEnvioCorreoId = penuTipoEnvio
            objLogEnvioCorreo.vCodigoProceso = pstrCodigoProceso
            objLogEnvioCorreo.iAnioPeriodo = pintAnioPeriodo
            objLogEnvioCorreo.iMesPeriodo = pintMesPeriodo
            objLogEnvioCorreo.vMensajeProceso = pstrMensaje
            objLogEnvioCorreo.iEstado = penuEstado
            objLogEnvioCorreo.vUsuarioCreacion = pstrUsuario

            objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)

            Return 1
        Catch ex As Exception
            Return 0
        End Try

    End Function

    <WebMethod()>
    Public Function RegistrarLogProcesosAutomaticos(pintTotal As Integer, pintProcesados As Integer, pintError As Integer, pstrMensaje As String, pintEstado As Integer, pstrUsuario As String) As Integer
        Try
            objProcesoAutomatico.iTotalRegistros = pintTotal
            objProcesoAutomatico.iProcesados = pintProcesados
            objProcesoAutomatico.iErroneos = pintError
            objProcesoAutomatico.vMensajeProceso = pstrMensaje
            objProcesoAutomatico.iEstado = pintEstado
            objProcesoAutomatico.vUsuarioCreacion = pstrUsuario

            Return objProcesoAutomaticoBL.Insert(objProcesoAutomatico)
        Catch ex As Exception
            Return 0
        End Try

    End Function

    <WebMethod()>
    Public Function ActualizarLogProcesosAutomaticos(pintCodigoProcesoAutomatico As Integer, pintTotal As Integer, pintProcesados As Integer, pintError As Integer, pstrMensaje As String, pintEstado As Integer, pstrUsuario As String) As Integer
        Try
            objProcesoAutomatico.iProcesoAutomaticoId = pintCodigoProcesoAutomatico
            objProcesoAutomatico.iTotalRegistros = pintTotal
            objProcesoAutomatico.iProcesados = pintProcesados
            objProcesoAutomatico.iErroneos = pintError
            objProcesoAutomatico.vMensajeProceso = pstrMensaje
            objProcesoAutomatico.iEstado = pintEstado
            objProcesoAutomatico.vUsuarioModificacion = pstrUsuario

            objProcesoAutomaticoBL.Update(objProcesoAutomatico)

            Return 1
        Catch ex As Exception
            Return 0
        End Try
    End Function

#End Region

    <WebMethod()>
    Public Function GetFuncionarios() As DataTable
        Try
            Return objFuncionarioBL.GetFuncionarios()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()>
    Public Function EnviarReporteAutomaticoByFuncionario(pintCodigoProcesoAutomatico As Integer, pintFuncionarioId As Integer, pstrNombreFuncionario As String, pstrCEFuncionario As String, pstrUsuario As String, ByRef pintEstado As Integer) As String
        Dim strMensajeEvento As String

        Try
            'Valida si el funcionario tiene registro a enviar en el reporte (toma getdate())
            Dim Cantidad As Integer = Convert.ToInt32(objReporteAutomaticoBL.ValidaExistenciaFuncionario(pintFuncionarioId, 0).Rows(0)("Cantidad").ToString())

            If Cantidad = 0 Then

                Dim _dtCabecera As New DataTable()
                Dim _dtDetalle As New DataTable()

                Try
                    _dtCabecera = objReporteAutomaticoBL.ReporteNominaAutomaticaCabecera(pintFuncionarioId)

                    If _dtCabecera.Rows.Count > 0 Then
                        For Each _drw As DataRow In _dtCabecera.Rows
                            dsNominaAutomatica.Cabecera.AddCabeceraRow(_drw("iTotalEmpresas").ToString(), _drw("iNumeroCreditos").ToString, _drw("dTotalImporte").ToString())
                        Next
                    End If
                Catch ex As HandledException
                    strMensajeEvento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ReporteNominaAutomaticaCabecera").Replace("&2", enumGeneric.SendMailError.ToString()).Replace("&3", ex.ToString())
                    RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                    pintEstado = 0
                    Return strMensajeEvento
                End Try

                Try
                    _dtDetalle = objReporteAutomaticoBL.ReporteNominaAutomaticaDetalle(pintFuncionarioId)

                    If _dtDetalle.Rows.Count > 0 Then
                        For Each _drw As DataRow In _dtDetalle.Rows
                            dsNominaAutomatica.Detalle.AddDetalleRow(_drw("EMPRESA").ToString(), _drw("RUC").ToString(), _drw("CODIGO_IBS").ToString(), _drw("NROCREDITOS").ToString(), _drw("IMPORTE").ToString())
                        Next
                    End If
                Catch ex As HandledException
                    strMensajeEvento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ReporteNominaAutomaticaDetalle").Replace("&2", enumGeneric.SendMailError.ToString()).Replace("&3", ex.ToString())
                    RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                    pintEstado = 0
                    Return strMensajeEvento
                End Try

                oRepEnvioAutomatico.SetDataSource(dsNominaAutomatica)

                strMensajeEvento = EnviarCorreoCliente(pstrNombreFuncionario, pstrCEFuncionario)


                If strMensajeEvento = "" Then
                    RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, "", enumLogEnvioCorreo.Enviado, pstrUsuario)

                    pintEstado = 1
                    Return strMensajeEvento
                Else
                    RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                    pintEstado = 0
                    Return strMensajeEvento
                End If
            Else
                strMensajeEvento = clsMensajesGeneric.MensajeFuncionarioNoEnvioAutomatico.Replace("&1", pstrNombreFuncionario).Replace("&2", DateTime.Now.ToShortDateString())
                RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, strMensajeEvento, enumLogEnvioCorreo.Cancelado, pstrUsuario)

                pintEstado = 1
                Return strMensajeEvento
            End If
        Catch ex1 As HandledException
            strMensajeEvento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "EnviarCorreoNomina").Replace("&2", enumGeneric.SendMailError.ToString()).Replace("&3", ex1.ErrorMessageFull)
            RegistrarLogEnvio(pintCodigoProcesoAutomatico, pintFuncionarioId, 0, enumTipoEnvioCorreo.ReporteAutomatico, "", 0, 0, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

            pintEstado = 0
            Return strMensajeEvento
        End Try

    End Function

    Private Function EnviarCorreoCliente(pstrNombreFuncionario As String, pstrCEFuncionario As String) As String
        'Envio Correo a Funcionarios
        Dim strCorreoElectronicoDE As String = String.Empty
        Dim strCorreoElectronicosPara As String = String.Empty
        Dim strCorreoElectronicosBCC As String = String.Empty
        Dim strCorreoElectronicoAsunto As String = String.Empty
        Dim strCorreoElectronicoNotificarA As String = String.Empty
        Dim strCorreoElectronicoCuerpo As String = String.Empty

        _dtParametrosEnvioMail = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.ParametroEnvioMail.ToString()))

        strCorreoElectronicoDE = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailPorDefecto))("vValor").ToString().Trim()
        strCorreoElectronicosPara = pstrCEFuncionario
        strCorreoElectronicosBCC = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ListaMailCopias))("vValor").ToString().Trim()
        strCorreoElectronicoAsunto = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.AsuntoReporteAutomatico))("vValor").ToString().Trim()
        strCorreoElectronicoCuerpo = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.CuerpoReporteAutomatico))("vValor").ToString().Trim()
        strCorreoElectronicoNotificarA = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailEnvio))("vValor").ToString().Trim()

        Dim strRoot As String = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.RutaDescargaReportes))("vValor").ToString().Trim()
        Dim name As String = objUtils.getWebServerDateId() & ".xls"

        Dim intAnio As Integer = Year(Date.Now)
        Dim intMonth As Integer = Month(Date.Now)
        Dim strPathFile As String = String.Empty
        Dim strMensaje As String = String.Empty

        Dim intResultExport As Integer = 0

        Try
            intResultExport = clsFiles.VerifyPath(strRoot, intAnio, intMonth, strPathFile, pstrNombreFuncionario)

            If intResultExport = 1 Then
                'Obtiene la ruta completa, para guardar el archivo excel
                Dim strFullName As String = strPathFile + "\\" + name

                'Procedimiento para el grabado del archivo excel
                With oRepEnvioAutomatico.ExportOptions
                    .ExportDestinationType = ExportDestinationType.DiskFile
                    .ExportFormatType = ExportFormatType.Excel
                    .DestinationOptions = New DiskFileDestinationOptions With {
                        .DiskFileName = strFullName
                    }
                End With

                oRepEnvioAutomatico.Export()

                objUtils.SendNotification(strCorreoElectronicoDE, strCorreoElectronicosPara, strCorreoElectronicosBCC, strCorreoElectronicoAsunto, strCorreoElectronicoCuerpo, strFullName, True, notifyTo:=(strCorreoElectronicoNotificarA))

                strMensaje = ""
            End If
        Catch ex As Exception
            strMensaje = ex.ToString()
        End Try

        Return strMensaje
    End Function


    <WebMethod()> _
    Public Sub ReportesAutomaticos()

        Dim strDE As String = ConfigurationManager.AppSettings("mailDefault").ToString()
        Dim pstrRoot As String = ConfigurationManager.AppSettings("ReportesAutomatico").ToString()
        Dim Empresa, RUC, CodigoIBS, NombreFuncionario, Motivo, FechaEnvio, strComentario As String
        Dim sCodigoCliente, sCodigoIBS, sCodigoProceso, sMensaje As String
        Dim intResult, intFuncionario, TotalEmpresa, TotalCreditos, Creditos As Integer
        Dim TotalImporte, Importe As Decimal

        Try

            'Obtiene la lista de funcionarios
            Dim dt As DataTable = objFuncionarioBL.GetFuncionarios()

            For Each drF As DataRow In dt.Rows

                intFuncionario = drF("id_funcionario")
                NombreFuncionario = drF("nombre_funcionario")
                strCEFuncionarios = drF("email_funcionario")

                'Valida si el funcionario, tiene asignado empresas
                Dim dtF As DataTable = objReporteAutomaticoBL.ValidaExistenciaFuncionario(intFuncionario, 0)
                If dtF.Rows.Count > 0 Then

                    sCodigoCliente = dtF.Rows(0)("Codigo_cliente").ToString()
                    sCodigoIBS = dtF.Rows(0)("Codigo_IBS").ToString()
                    sCodigoProceso = dtF.Rows(0)("codigo_proceso").ToString()

                    'Obtiene la cabecera de la nómina enviada
                    Dim dtRC As DataTable = objReporteAutomaticoBL.ReporteNominaAutomaticaCabecera(intFuncionario)
                    For Each dr As DataRow In dtRC.Rows
                        TotalEmpresa = dr("TOTAL_EMPRESA")
                        TotalCreditos = IIf(dr("TOTAL_CREDITOS") Is DBNull.Value, 0, dr("TOTAL_CREDITOS"))
                        TotalImporte = IIf(dr("TOTAL_IMPORTE") Is DBNull.Value, 0, dr("TOTAL_IMPORTE"))

                        If TotalEmpresa <> 0 Then
                            dsNominaAutomatica.Cabecera.AddCabeceraRow(TotalEmpresa, TotalCreditos, TotalImporte)
                        End If

                    Next

                    'Obtiene el detalle de la nómina enviada
                    Dim dtRD As DataTable = objReporteAutomaticoBL.ReporteNominaAutomaticaDetalle(intFuncionario)
                    For Each drD As DataRow In dtRD.Rows
                        Empresa = drD("EMPRESA")
                        RUC = drD("RUC")
                        CodigoIBS = drD("CODIGO_IBS")
                        Creditos = drD("NROCREDITOS")
                        Importe = drD("IMPORTE")
                        dsNominaAutomatica.Detalle.AddDetalleRow(Empresa, RUC, CodigoIBS, Creditos, Importe)
                    Next

                    'Obtiene la cabecera de la nómina enviada observada
                    Dim dtRCO As DataTable = objReporteAutomaticoBL.ReporteNominaAutomaticaCabeceraObservada(intFuncionario)
                    For Each drCO As DataRow In dtRCO.Rows
                        TotalEmpresa = drCO("TOTAL_EMPRESA")
                        TotalCreditos = IIf(drCO("TOTAL_CREDITOS") Is DBNull.Value, 0, drCO("TOTAL_CREDITOS"))
                        TotalImporte = IIf(drCO("TOTAL_IMPORTE") Is DBNull.Value, 0, drCO("TOTAL_IMPORTE"))

                        'If TotalEmpresa <> 0 Then
                        '    dsNominaAutomatica.CabeceraObservada.AddCabeceraObservadaRow(TotalEmpresa, TotalCreditos, TotalImporte)
                        'End If

                    Next

                    'Obtiene el detalle de la nómina enviada observada
                    Dim dtRDO As DataTable = objReporteAutomaticoBL.ReporteNominaAutomaticaDetalleObservada(intFuncionario)
                    For Each drDO As DataRow In dtRDO.Rows
                        Empresa = drDO("EMPRESA")
                        RUC = drDO("RUC")
                        CodigoIBS = drDO("CODIGO_IBS")
                        Creditos = drDO("NROCREDITOS")
                        Importe = drDO("IMPORTE")
                        Motivo = drDO("MOTIVO")
                        FechaEnvio = drDO("FECHA_ENVIO")
                        'dsNominaAutomatica.DetalleObservado.AddDetalleObservadoRow(Empresa, RUC, CodigoIBS, Creditos, Importe, Motivo, FechaEnvio)
                    Next

                    'Se añade el dataset al objeto set del crystal report
                    oRepEnvioAutomatico.SetDataSource(dsNominaAutomatica)

                    'Se obtiene el nombre del archivo excel a generar
                    Dim name As String = objUtils.getWebServerDateId() & ".xls"

                    'Se obtiene el la ruta a exportar
                    Dim intAnio As Integer = Year(Date.Now)
                    Dim intMonth As Integer = Month(Date.Now)
                    Dim strPathFile As String = String.Empty

                    intResult = clsFiles.VerifyPath(pstrRoot, intAnio, intMonth, strPathFile, NombreFuncionario)

                    If intResult = 1 Then

                        'Obtiene la ruta completa, para guardar el archivo excel
                        Dim strFullName As String = strPathFile + "\\" + name

                        'Procedimiento para el grabado del archivo excel
                        With oRepEnvioAutomatico.ExportOptions
                            .ExportDestinationType = ExportDestinationType.DiskFile
                            .ExportFormatType = ExportFormatType.Excel
                            .DestinationOptions = New DiskFileDestinationOptions With {
                                .DiskFileName = strFullName
                            }
                        End With

                        oRepEnvioAutomatico.Export()

                        'Envio Correo a Funcionarios
                        strCEFuncionarios = ConfigurationManager.AppSettings("mailFuncionarios").ToString()

                        strComentario = "El Reporte de Nóminas, se ha generado de forma automática a través del Sistema BIFConvenios"

                        'BIFConvenios.Utils.SendNotification(strDE, strCEFuncionarios, strCEFuncionarios, _
                        '                   ConfigurationManager.AppSettings("mailCFSubject").ToString(), strComentario, strFullName, _
                        '                  notifyTo:=(ConfigurationManager.AppSettings("mailSender").ToString()))

                        'Inserta en la tabla Log_Envio_Correo
                        sMensaje = "Envío de Reporte exitoso."

                        objLogEnvioCorreo.iCodigoCliente = sCodigoCliente
                        objLogEnvioCorreo.iCodigoIBS = sCodigoIBS
                        objLogEnvioCorreo.iTipoEnvioCorreoId = 3
                        objLogEnvioCorreo.vMensajeProceso = sMensaje
                        objLogEnvioCorreo.iEstado = 1
                        objLogEnvioCorreo.vUsuarioCreacion = 1
                        objLogEnvioCorreo.vCodigoProceso = sCodigoProceso
                        objLogEnvioCorreo.dFechaCreacion = FormatDateTime(Now, DateFormat.ShortDate)
                        objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)

                    End If

                Else

                    sMensaje = "El funcionario no tiene registros de envío."

                    objLogEnvioCorreo.iCodigoCliente = 0
                    objLogEnvioCorreo.iCodigoIBS = 0
                    objLogEnvioCorreo.iTipoEnvioCorreoId = 3
                    objLogEnvioCorreo.vMensajeProceso = sMensaje
                    objLogEnvioCorreo.iEstado = 1
                    objLogEnvioCorreo.vUsuarioCreacion = 1
                    objLogEnvioCorreo.vCodigoProceso = ""
                    objLogEnvioCorreo.dFechaCreacion = FormatDateTime(Now, DateFormat.ShortDate)
                    objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)


                End If

            Next

        Catch ex As Exception

            'Inserta en la tabla Log_Envio_Correo
            sMensaje = "Error en el envío de Reportes."

            objLogEnvioCorreo.iCodigoCliente = 0
            objLogEnvioCorreo.iCodigoIBS = 0
            objLogEnvioCorreo.iTipoEnvioCorreoId = 3
            objLogEnvioCorreo.vMensajeProceso = sMensaje
            objLogEnvioCorreo.iEstado = 1
            objLogEnvioCorreo.vUsuarioCreacion = 1
            objLogEnvioCorreo.vCodigoProceso = ""
            objLogEnvioCorreo.dFechaCreacion = FormatDateTime(Now, DateFormat.ShortDate)
            objLogEnvioCorreoBL.Insert(objLogEnvioCorreo)

        End Try

    End Sub

End Class
