Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports BIFConvenios.BE
Imports BIFConvenios.BL.BIFConvenios.BL

Imports Resource
Imports Microsoft.VisualBasic

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ToolboxItem(False)> _
Public Class wsEnvioAutomatico
    Inherits System.Web.Services.WebService

    Protected objClienteBL As New BIFConvenios.BL.clsClienteBL
    Protected objProcesoBL As New BIFConvenios.BL.clsProcesoBL
    Protected objCuotaBL As New BIFConvenios.BL.clsCuotaBL
    Protected objProcesoAutomaticoBL As New BIFConvenios.BL.clsProcesosAutomaticosBL()
    Protected objLogEnvioCorreoBL As New BIFConvenios.BL.clsLogEnvioCorreosBL
    Protected objEventoSistemaBL As New BIFConvenios.BL.clsEventoSistemaBL
    Protected objArchivosConvenioBL As New BIFConvenios.BL.clsArchivosConveniosBL()

    Protected objSystemParametersBL As New BIFConvenios.BL.clsSystemParametersBL()
    Protected _dtParametrosEnvioMail As New DataTable()

    Protected objArchivosConvenio As New clsArchivosConvenios
    Protected objCliente As New clsCliente
    Protected objProcesoAutomatico As New clsProcesosAutomaticos
    Protected objLogEnvioCorreo As New clsLogEnvioCorreo
    Protected objEventoSistema As New clsEventoSistema

    <WebMethod()> _
    Public Function ValidarFinProcesoBatch(ByVal codigo_proceso As String) As Boolean
        Return objProcesoBL.ValidarFinProcesoBatch(codigo_proceso)
    End Function

    <WebMethod()> _
    Public Function ObtenerListaProcesosDisponiblesByFecha(ByVal pintDia As Integer) As DataTable
        Dim _dtProcesosIBS As New DataTable()
        Dim _dtProcesosBifConvenios As New DataTable()
        Dim _dtClientes As New DataTable()
        Dim _dsResult As New DataSet()

        _dtProcesosIBS = objProcesoBL.ObtenerInformacionProcesosIBS("").Copy()
        _dtProcesosIBS.TableName = "dtProcesosIBS"
        _dtProcesosBifConvenios = objProcesoBL.ObtenerProcesosRealizadosActual().Copy()
        _dtProcesosBifConvenios.TableName = "dtProcesosBIF"
        _dtClientes = objClienteBL.ObtenerListaClientesByDiaEnvio(pintDia).Copy()
        _dtClientes.TableName = "dtCliente"

        _dsResult.Tables.Add(_dtProcesosIBS)
        _dsResult.Tables.Add(_dtProcesosBifConvenios)
        _dsResult.Tables.Add(_dtClientes)

        'Adicionamos las relaciones entre ambas tablas
        'Son 4 campos en cada tabla
        'Columnas del Padre
        'CUSTID, CUSIDN, DLEAP, DLEMP
        Dim ParentColumns() As DataColumn = New DataColumn() _
                    {_dsResult.Tables("dtProcesosIBS").Columns("CUSTID"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("CUSIDN"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("DLEAP"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("DLEMP")}
        ' Procesos registrados

        'Columnas del hijo
        'TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo
        Dim ChildColumns() As DataColumn = New DataColumn() _
                    {_dsResult.Tables("dtProcesosBIF").Columns("TipoDocumento"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("NumeroDocumento"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("Anio_periodo"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("Mes_Periodo")}
        ' Procesos disponibles AS/400

        Dim CustomerRelation1 As New DataRelation("Division1", ParentColumns, ChildColumns, False)
        _dsResult.Relations.Add(CustomerRelation1)

        'Eliminamos todos los registros del los procesos que existen 
        'en la base de datos SQL Server        
        For Each dr1 As DataRow In _dsResult.Tables("dtProcesosBIF").Rows
            For Each dr2 As DataRow In dr1.GetParentRows(CustomerRelation1)
                dr2.Delete()
            Next
        Next

        _dsResult.Tables("dtProcesosIBS").AcceptChanges()

        Dim ChildProcesos() As DataColumn = New DataColumn() _
                                {_dsResult.Tables("dtProcesosIBS").Columns("CUSTID"), _
                                    _dsResult.Tables("dtProcesosIBS").Columns("CUSIDN")}

        Dim ParentRegistrados() As DataColumn = New DataColumn() _
                                {_dsResult.Tables("dtCliente").Columns("TipoDocumento"), _
                                    _dsResult.Tables("dtCliente").Columns("NumeroDocumento")}

        Dim CustomerRelation2 As New DataRelation("Division2", ParentRegistrados, ChildProcesos, False)
        _dsResult.Relations.Add(CustomerRelation2)

        For Each dr1 As DataRow In _dsResult.Tables("dtProcesosIBS").Rows
            If dr1.GetParentRows(CustomerRelation2).Length = 0 Then
                dr1.Delete()
            End If
        Next

        _dsResult.Tables("dtProcesosIBS").AcceptChanges()

        Return _dsResult.Tables("dtProcesosIBS")
    End Function

    <WebMethod()> _
    Public Function ObtenerListaProcesosDisponibles(ByVal pstrFiltro As String) As DataTable
        Dim _dtProcesosIBS As New DataTable()
        Dim _dtProcesosBifConvenios As New DataTable()
        Dim _dtClientes As New DataTable()
        Dim _dsResult As New DataSet()

        _dtProcesosIBS = objProcesoBL.ObtenerInformacionProcesosIBS(pstrFiltro).Copy()
        _dtProcesosIBS.TableName = "dtProcesosIBS"
        _dtProcesosBifConvenios = objProcesoBL.ObtenerProcesosRealizadosActual().Copy()
        _dtProcesosBifConvenios.TableName = "dtProcesosBIF"
        _dtClientes = objClienteBL.ObtenerListaDocumentosClientesRegistrados().Copy()
        _dtClientes.TableName = "dtCliente"

        _dsResult.Tables.Add(_dtProcesosIBS)
        _dsResult.Tables.Add(_dtProcesosBifConvenios)
        _dsResult.Tables.Add(_dtClientes)

        'Adicionamos las relaciones entre ambas tablas
        'Son 4 campos en cada tabla
        'Columnas del Padre
        'CUSTID, CUSIDN, DLEAP, DLEMP
        Dim ParentColumns() As DataColumn = New DataColumn() _
                    {_dsResult.Tables("dtProcesosIBS").Columns("CUSTID"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("CUSIDN"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("DLEAP"), _
                        _dsResult.Tables("dtProcesosIBS").Columns("DLEMP")}
        ' Procesos registrados

        'Columnas del hijo
        'TipoDocumento, NumeroDocumento, Anio_periodo, Mes_Periodo
        Dim ChildColumns() As DataColumn = New DataColumn() _
                    {_dsResult.Tables("dtProcesosBIF").Columns("TipoDocumento"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("NumeroDocumento"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("Anio_periodo"), _
                        _dsResult.Tables("dtProcesosBIF").Columns("Mes_Periodo")}
        ' Procesos disponibles AS/400

        Dim CustomerRelation1 As New DataRelation("Division1", ParentColumns, ChildColumns, False)
        _dsResult.Relations.Add(CustomerRelation1)

        'Eliminamos todos los registros del los procesos que existen 
        'en la base de datos SQL Server        
        For Each dr1 As DataRow In _dsResult.Tables("dtProcesosBIF").Rows
            For Each dr2 As DataRow In dr1.GetParentRows(CustomerRelation1)
                dr2.Delete()
            Next
        Next

        _dsResult.Tables("dtProcesosIBS").AcceptChanges()

        Dim ChildProcesos() As DataColumn = New DataColumn() _
                                {_dsResult.Tables("dtProcesosIBS").Columns("CUSTID"), _
                                    _dsResult.Tables("dtProcesosIBS").Columns("CUSIDN")}

        Dim ParentRegistrados() As DataColumn = New DataColumn() _
                                {_dsResult.Tables("dtCliente").Columns("TipoDocumento"), _
                                    _dsResult.Tables("dtCliente").Columns("NumeroDocumento")}

        Dim CustomerRelation2 As New DataRelation("Division2", ParentRegistrados, ChildProcesos, False)
        _dsResult.Relations.Add(CustomerRelation2)

        For Each dr1 As DataRow In _dsResult.Tables("dtProcesosIBS").Rows
            If dr1.GetParentRows(CustomerRelation2).Length = 0 Then
                dr1.Delete()
            End If
        Next

        _dsResult.Tables("dtProcesosIBS").AcceptChanges()

        Return _dsResult.Tables("dtProcesosIBS")
    End Function

    <WebMethod()> _
    Public Function ProcesarEnvioNominasByCliente(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoIBS As String, ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String, ByVal pstrMesPeriodo As String, ByVal pstrAnioPeriodo As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrUsuario As String, ByRef pintEstado As Integer) As String
        Dim _dtCliente As New DataTable()

        Dim strCodigoCliente As String = ""
        Dim strNombreCliente As String = ""
        Dim strEnvioAutomatico As String = ""
        Dim strCorreosElectronicos As String = ""
        Dim intDiaEnvioPlanilla As Integer = 0

        Dim strMensajeEvento As String = ""

        Try
            _dtCliente = objClienteBL.ExisteClienteBifConvenio(pstrTipoDocumento, pstrNumeroDocumento)

            strCodigoCliente = _dtCliente.Rows(0)("CodigoCliente").ToString()
            strNombreCliente = _dtCliente.Rows(0)("NombreCliente").ToString().Trim()
            intDiaEnvioPlanilla = Convert.ToInt32(_dtCliente.Rows(0)("DiaEnvioPlanilla").ToString())
            strEnvioAutomatico = _dtCliente.Rows(0)("EnvioAutomaticoListado").ToString()
            strCorreosElectronicos = objClienteBL.ObtenerEmailsEnviosClientes(strCodigoCliente)

            If intDiaEnvioPlanilla.Equals(DateTime.Now.Day) Then ''And strEnvioAutomatico.ToUpper.Equals("S") Then
                'If strCorreosElectronicos.Length > 0 Then
                'Else
                '    strMensajeEvento = clsMensajesGeneric.MensajeNoRegistraCorreos.Replace("&1", strNombreCliente).Replace("&2", strCodigoCliente)
                '    RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, "", Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Cancelado, pstrUsuario)

                '    pintEstado = 1
                '    Return strMensajeEvento
                'End If

                Try
                    Dim strCodigoProceso As String = ""

                    strMensajeEvento = ProcesarCliente(strCodigoCliente, strNombreCliente, pstrCodigoIBS, pstrAnioPeriodo, pstrMesPeriodo, pstrFechaProcesoAS400, pstrUsuario, strCodigoProceso)

                    If strMensajeEvento = "" Then
                        strMensajeEvento = EnviarCorreoCliente(pstrCodigoIBS, strNombreCliente, enumProcessType.Envio, strCodigoProceso, strCorreosElectronicos, pstrAnioPeriodo, pstrMesPeriodo, "*", "*", 0, "-", 0, "xls")

                        If strMensajeEvento = "" Then
                            RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, strCodigoProceso, Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), "", enumLogEnvioCorreo.Enviado, pstrUsuario)

                            pintEstado = 1
                            Return strMensajeEvento
                        Else
                            RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, strCodigoProceso, Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                            pintEstado = 0
                            Return strMensajeEvento
                        End If
                    Else
                        RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, strCodigoProceso, Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                        pintEstado = 0
                        Return strMensajeEvento
                    End If
                Catch ex As Exception
                    strMensajeEvento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "EnviarCorreoNomina").Replace("&2", enumGeneric.SendMailError.ToString()).Replace("&3", ex.ToString())
                    RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, "", Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                    pintEstado = 0
                    Return ex.ToString()
                End Try
            Else
                strMensajeEvento = clsMensajesGeneric.MensajeClienteNoEnvioAutomatico.Replace("&1", strNombreCliente).Replace("&2", strCodigoCliente)
                RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, "", Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                pintEstado = 0
                Return strMensajeEvento
            End If
        Catch ex As HandledException
            strMensajeEvento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ExisteClienteBifConvenio").Replace("&2", enumGeneric.DataBaseError.ToString()).Replace("&3", ex.ErrorMessageFull)
            RegistrarLogEnvio(pintCodigoProcesoAutomatico, strCodigoCliente, pstrCodigoIBS, enumTipoEnvioCorreo.EnvioAutomaticoNomina, "", Convert.ToInt32(pstrAnioPeriodo), Convert.ToInt32(pstrMesPeriodo), strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

            pintEstado = 0
            Return strMensajeEvento
        End Try
    End Function

    <WebMethod()> _
    Public Function ProcesarTodosClientes(ByVal penuTipoEnvio As Integer, ByVal pstrUsuario As String) As String
        Dim strTipoDocumento As String = String.Empty
        Dim strNumeroDocumento As String = String.Empty
        Dim strCodigoIBS As String = String.Empty
        Dim strCodigoCliente As String = String.Empty
        Dim strNombreCliente As String = String.Empty
        Dim strMesProceso As String = String.Empty
        Dim strAnioProceso As String = String.Empty
        Dim strFechaProcesoAS400 As String = String.Empty

        Dim intDiaEnvioPlanilla As Integer = 0
        Dim strEnvioAutomatico As String = String.Empty
        Dim strCorreosElectronicos As String = String.Empty

        Dim strMensajeEvento As String = String.Empty
        Dim intContadorEnvios As Integer = 0
        Dim intContadorError As Integer = 0

        Dim _dtListaProcesosCliente As New DataTable()

        Dim strMensajes As List(Of String) = New List(Of String)
        Dim strMensajeEnviar As String

        RegistrarLogEventoSistema("BifConvenio", enumEstadoLogEnvio.Info, "ProcesarTodosLosClientes", "Inicio del Método - Parametros: penuTipoEnvio = " & penuTipoEnvio & ", pstrUsuiario = " & pstrUsuario, "", pstrUsuario)

        Try
            _dtListaProcesosCliente = ObtenerListaProcesosDisponibles("")

            For Each _dr As DataRow In _dtListaProcesosCliente.Rows
                strTipoDocumento = _dr("CUSTID").ToString()
                strNumeroDocumento = _dr("CUSIDN").ToString()
                strNombreCliente = _dr("CUSNA1").ToString()
                strMesProceso = _dr("DLEMP").ToString()
                strAnioProceso = _dr("DLEAP").ToString()
                strFechaProcesoAS400 = _dr("DLEFP").ToString()
                strCodigoIBS = _dr("CUSCUN").ToString()

                Try
                    Dim _dtCliente As New DataTable()

                    _dtCliente = objClienteBL.ExisteClienteBifConvenio(strTipoDocumento, strNumeroDocumento)

                    strCodigoCliente = _dtCliente.Rows(0)("CodigoCliente").ToString()
                    intDiaEnvioPlanilla = Convert.ToInt32(_dtCliente.Rows(0)("DiaEnvioPlanilla").ToString())
                    strEnvioAutomatico = _dtCliente.Rows(0)("EnvioAutomaticoListado").ToString()
                    strCorreosElectronicos = _dtCliente.Rows(0)("CorreoElectronico").ToString()

                    If intDiaEnvioPlanilla.Equals(DateTime.Now.Day) And strEnvioAutomatico.ToUpper.Equals("S") Then
                        If strCorreosElectronicos.Length > 0 Then
                            Dim strCodigoProceso As String = ""

                            strMensajeEvento = ProcesarCliente(strCodigoCliente, strNombreCliente, strCodigoIBS, strAnioProceso, strMesProceso, strFechaProcesoAS400, pstrUsuario, strCodigoProceso)

                            If strMensajeEvento = "" Then
                                strMensajeEvento = EnviarCorreoCliente(strCodigoIBS, strNombreCliente, enumProcessType.Envio, strCodigoProceso, strCorreosElectronicos, strAnioProceso, strMesProceso, "*", "*", 0, "-", 0, "xls")

                                If strMensajeEvento = "" Then
                                    'RegistrarLogEnvio(pintcodigo strCodigoCliente, strCodigoIBS, penuTipoEnvio, strCodigoProceso, "", enumLogEnvioCorreo.Enviado, pstrUsuario)

                                    strMensajes.Add(clsMensajesGeneric.ProcesoMensajeEnviadoExitoso.Replace("&1", strCodigoProceso).Replace("&2", strCodigoCliente).Replace("&3", strNombreCliente))

                                    intContadorEnvios = intContadorEnvios + 1
                                Else
                                    'RegistrarLogEnvio(strCodigoCliente, strCodigoIBS, penuTipoEnvio, strCodigoProceso, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                                    strMensajes.Add(clsMensajesGeneric.ProcesoMensajeEnviadoError.Replace("&1", strCodigoProceso).Replace("&2", strCodigoCliente).Replace("&3", strNombreCliente))

                                    intContadorError = intContadorError + 1
                                End If
                            Else
                                'RegistrarLogEnvio(strCodigoCliente, strCodigoIBS, penuTipoEnvio, strCodigoProceso, strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                                strMensajes.Add(clsMensajesGeneric.ProcesoYAGenerado.Replace("&1", strCodigoProceso).Replace("&2", strCodigoCliente).Replace("&3", strNombreCliente))

                                intContadorError = intContadorError + 1
                            End If
                        Else
                            strMensajeEvento = "No existe correos para el Cliente con Codigo: " + strCodigoCliente
                            'RegistrarLogEnvio(strCodigoCliente, strCodigoIBS, penuTipoEnvio, "", strMensajeEvento, enumLogEnvioCorreo.Error, pstrUsuario)

                            strMensajes.Add(clsMensajesGeneric.ProcesoNoExisteCorreo.Replace("&1", "").Replace("&2", strCodigoCliente).Replace("&3", strNombreCliente))

                            intContadorError = intContadorError + 1
                        End If

                    End If

                Catch ex As HandledException

                    'RegistrarLogEnvio(strCodigoCliente, strCodigoIBS, penuTipoEnvio, "", ex.Message, enumLogEnvioCorreo.Error, pstrUsuario)
                    strMensajes.Add(clsMensajesGeneric.ProcesoClienteError.Replace("&1", strTipoDocumento).Replace("&2", strNumeroDocumento))

                    intContadorError = intContadorError + 1
                    Continue For
                End Try
            Next
        Catch ex As HandledException
            RegistrarLogEventoSistema("BifConvenio", enumEstadoLogEnvio.Errores, "ProcesarTodosLosClientes", ex.Message, ex.StackTrace, pstrUsuario)

            strMensajes.Add(clsMensajesGeneric.ProcesoListaError)
        End Try

        RegistrarLogEventoSistema("BifConvenio", enumEstadoLogEnvio.Info, "ProcesarTodosLosClientes", "Fin del Método - Parametros: penuTipoEnvio = " & penuTipoEnvio & ", pstrUsuiario = " & pstrUsuario, "", pstrUsuario)

        'strMensajes.Add(clsMensajesGeneric.ProcesoCantidadRegistros.Replace("&1", _dtListaProcesosCliente.Rows.Count).Replace("&2", intContadorEnvios).Replace("&3", intContadorError))
        strMensajeEnviar = clsMensajesGeneric.ProcesoCantidadRegistros.Replace("&1", _dtListaProcesosCliente.Rows.Count).Replace("&2", intContadorEnvios).Replace("&3", intContadorError)

        Return strMensajeEnviar
        'lo que se devolvera
    End Function

    Private Function EnviarCorreoCliente(ByVal pstrCodIBS As String, ByVal pstrNomCliente As String, ByVal penuTipoProceso As enumProcessType, ByVal pstrCodigoProceso As String, ByVal pstrCorreosElectronicos As String, ByVal pstrAnioProceso As String, ByVal pstrMesProceso As String, ByVal pstrDocTrabajador As String, ByVal pstrNomTrabajador As String, ByVal pdecPagare As Decimal, ByVal pstrEstadoTrabajador As String, ByVal pintZonaUse As Integer, ByVal pstrFormatFile As String) As String
        Dim strCorreoElectronicoDE As String = String.Empty
        Dim strCorreoElectronicosPara As String = String.Empty
        Dim strCorreoElectronicosBCC As String = String.Empty
        Dim strCorreoElectronicoAsunto As String = String.Empty
        Dim strCorreoElectronicoNotificarA As String = String.Empty
        Dim strCorreoElectronicoCuerpo As String = String.Empty

        Dim strNameFile As String = String.Empty
        Dim strPathFile As String = String.Empty
        Dim strMensaje As String = String.Empty

        Dim strPath As String = ""

        _dtParametrosEnvioMail = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.ParametroEnvioMail.ToString()))

        strPath = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.RutaDescargaNominas))("vValor").ToString().Trim()

        'Dim strPath As String = ConfigurationManager.AppSettings("ArchivosConvenio").ToString()
        Dim strFullName As String = String.Empty

        Dim _dtCuotas As New DataTable()
        Dim objUtils As New clsUtils

        'Seteando parametros

        'strCorreoElectronicoDE = ConfigurationManager.AppSettings("mailDefault").ToString()
        strCorreoElectronicoDE = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailPorDefecto))("vValor").ToString().Trim()

        'Si no este TEST hacemos el envio regular
        Dim strTestOnly As String = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ModoPrueba))("vValor").ToString().Trim()

        If strTestOnly = "0" Then
            strCorreoElectronicosPara = pstrCorreosElectronicos
        Else    ' en otro caso enviamos el correo a una direccion de prueba
            'strCorreoElectronicosPara = ConfigurationManager.AppSettings("mailTest").ToString()
            strCorreoElectronicosPara = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ListaMailTest))("vValor").ToString().Trim()
        End If

        strCorreoElectronicosBCC = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ListaMailCopias))("vValor").ToString().Trim()
        strCorreoElectronicoAsunto = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.AsuntoNominaAutomatica))("vValor").ToString().Trim()
        strCorreoElectronicoCuerpo = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.CuerpoNominaAutomatica))("vValor").ToString().Trim().Replace("&1", clsPeriodo.NombreMes(pstrMesProceso.ToString()).Replace("&2", pstrAnioProceso))
        strCorreoElectronicoNotificarA = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.MailEnvio))("vValor").ToString().Trim()

        Dim ar As New ArrayList()
        Dim intResultExport As Integer = 0

        For Each str As Object In strCorreoElectronicosPara.Split(";")
            ar.Add(New MailSource(str))
        Next

        'Iniciando el proceso de envio del correo

        Try
            _dtCuotas = objProcesoBL.ExportarRegistrosResultadoProceso(pstrCodigoProceso, pstrDocTrabajador, pstrNomTrabajador, pdecPagare, pstrEstadoTrabajador, pintZonaUse)
            intResultExport = clsFiles.ExportToExcel(_dtCuotas, pstrFormatFile, strPath, pstrNomCliente + "-" + pstrCodIBS, Convert.ToInt32(pstrAnioProceso), Convert.ToInt32(pstrMesProceso), penuTipoProceso.ToString(), strNameFile, strPathFile, strMensaje)

            If intResultExport = 0 Then
                strFullName = strPathFile + "\\" + strNameFile

                objUtils.SendNotification(strCorreoElectronicoDE, strCorreoElectronicosPara, strCorreoElectronicosBCC, strCorreoElectronicoAsunto, strCorreoElectronicoCuerpo, strFullName, True, notifyTo:=(strCorreoElectronicoNotificarA))

                objArchivosConvenio.vCodProceso = pstrCodigoProceso
                objArchivosConvenio.vNombreArchivo = strNameFile
                objArchivosConvenio.vRutaCreacion = strPathFile
                objArchivosConvenio.vRutaModificacion = String.Empty
                objArchivosConvenio.vRutaHistorico = String.Empty
                objArchivosConvenio.iEstado = 1
                objArchivosConvenio.vUsuarioCreacion = Context.User.Identity.Name
                objArchivosConvenio.dFechaCreacion = DateTime.Now()

                Dim iInsert As Integer = objArchivosConvenioBL.Insert(objArchivosConvenio)

                strMensaje = ""
            Else
                Return strMensaje
            End If
        Catch ex As Exception
            Return ex.ToString()
        End Try

        Return strMensaje
    End Function

    Private Function ProcesarCliente(ByVal pstrCodigoCliente As String, ByVal pstrNombreCliente As String, ByVal pstrCodigoIBS As String, ByVal pstrAnio As String, ByVal pstrMes As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrUserName As String, ByRef pstrCodigoProceso As String) As String
        Dim strMensaje As String = String.Empty

        If String.IsNullOrEmpty(pstrCodigoCliente) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo del Cliente")
            Return strMensaje
        ElseIf Not IsNumeric(pstrCodigoCliente) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Codigo del Cliente")
            Return strMensaje
        ElseIf String.IsNullOrEmpty(pstrNombreCliente) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Nombre Cliente")
        ElseIf String.IsNullOrEmpty(pstrCodigoIBS) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo IBS")
        ElseIf Not IsNumeric(pstrCodigoIBS) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Codigo IBS")
        ElseIf String.IsNullOrEmpty(pstrAnio) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Año")
            Return strMensaje
        ElseIf Not IsNumeric(pstrAnio) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Año")
            Return strMensaje
        ElseIf String.IsNullOrEmpty(pstrMes) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Mes")
            Return strMensaje
        ElseIf Not IsNumeric(pstrMes) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Mes")
            Return strMensaje
        ElseIf String.IsNullOrEmpty(pstrFechaProcesoAS400) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Fecha Proceso AS400")
            Return strMensaje
        ElseIf Not IsNumeric(pstrFechaProcesoAS400) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Fecha Proceso AS400")
            Return strMensaje
        End If

        Try
            pstrCodigoProceso = objCuotaBL.ImportaPagareDeIBS(pstrCodigoIBS, pstrAnio, pstrMes, pstrFechaProcesoAS400, pstrCodigoCliente, pstrUserName)

            If pstrCodigoProceso = "-1" Then
                strMensaje = clsMensajesGeneric.ProcesoYAGenerado.Replace("&1", pstrNombreCliente).Replace("&2", pstrAnio).Replace("&3", pstrMes)
                Return strMensaje
            End If
        Catch ex As HandledException
            strMensaje = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ImportaPagaresDeIBS").Replace("&2", ex.ErrorTypeId.ToString()).Replace("&3", ex.ErrorMessageFull)
            Return strMensaje
        End Try

        Return strMensaje
    End Function

#Region "Log"

    <WebMethod()> _
    Public Function RegistrarLogEventoSistema(ByVal pstrHilo As String, ByVal penuNivel As Integer, ByVal pstrAccion As String, ByVal pstrMensaje As String, ByVal pstrExcepcion As String, ByVal pstrUsuario As String) As Integer

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

    <WebMethod()> _
    Public Function RegistrarLogEnvio(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintCodigoCliente As Integer, ByVal pintCodigoIBS As Integer, ByVal penuTipoEnvio As Integer, ByVal pstrCodigoProceso As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrMensaje As String, ByVal penuEstado As enumLogEnvioCorreo, ByVal pstrUsuario As String) As Integer

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

    <WebMethod()> _
    Public Function RegistrarLogProcesosAutomaticos(ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String) As Integer
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

    <WebMethod()> _
    Public Function ActualizarLogProcesosAutomaticos(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pintTotal As Integer, ByVal pintProcesados As Integer, ByVal pintError As Integer, ByVal pstrMensaje As String, ByVal pintEstado As Integer, ByVal pstrUsuario As String) As Integer
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

End Class
