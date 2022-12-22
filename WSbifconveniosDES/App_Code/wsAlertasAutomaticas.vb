Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Text
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
Public Class wsAlertasAutomaticas
    Inherits System.Web.Services.WebService

    Protected objAlertasBL As New BIFConvenios.BL.clsAlertasBL
    Protected objAlertasClienteBL As New BIFConvenios.BL.clsAlertasClientesBL
    Protected objClienteBL As New BIFConvenios.BL.clsClienteBL
    Protected objProcesoAutomaticoBL As New BIFConvenios.BL.clsProcesosAutomaticosBL()
    Protected objLogEnvioCorreoBL As New BIFConvenios.BL.clsLogEnvioCorreosBL
    Protected objEventoSistemaBL As New BIFConvenios.BL.clsEventoSistemaBL
    Protected objArchivosConvenioBL As New BIFConvenios.BL.clsArchivosConveniosBL
    Protected objProcesosBL As New BIFConvenios.BL.clsProcesoBL

    Protected objSystemParametersBL As New BIFConvenios.BL.clsSystemParametersBL()
    Protected _dtParametrosEnvioMail As New DataTable()

    Protected objAlertas As New clsAlertas
    Protected objAlertasClientes As New clsAlertasClientes
    Protected objClientes As New clsCliente
    Protected objProcesoAutomatico As New clsProcesosAutomaticos
    Protected objLogEnvioCorreo As New clsLogEnvioCorreo
    Protected objEventoSistema As New clsEventoSistema
    Protected objArchivosConvenio As New clsArchivosConvenios
    Protected objProcesos As New clsProceso

    Dim dtAlertasCliente As New DataTable()

    Dim intContadorEnvios As Integer = 0
    Dim intContadorError As Integer = 0

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

#Region "Metodos Auxiliares"

    Public Function ConvertirBody(ByVal pstrBody As String) As String
        Dim strHtmlBody As New StringBuilder()
        Dim strBody As New StringBuilder()

        Dim filas() As String = Split(pstrBody.ToString(), "&E")

        For i As Integer = LBound(filas) To UBound(filas)
            With strBody
                If filas(i) = "" Then
                    .Append("<p style='margin-top:0; margin-bottom:0;'>&nbsp;</p>")
                Else
                    Dim strCadena As String = String.Empty
                    Dim strCaux As String = String.Empty

                    strCaux = filas(i).Trim()

                    If strCaux.Length > 200 Then
                        strCaux.Insert(200, "<br />")
                    End If

                    For x As Integer = 0 To (strCaux.Length - 1)
                        If strCaux.Chars(x) = " " Then
                            strCadena = strCadena & "&nbsp;"
                        Else
                            strCadena = strCadena & strCaux.Chars(x)
                        End If
                    Next

                    .Append("<p style='margin-top;0; margin-bottom:0;'>" & strCadena & "</p>")

                End If
            End With
        Next

        With strHtmlBody
            .Append("<html>")
            .Append("<head> ")
            .Append("<meta name='ProgId' content='FrontPage.Editor.Document'>")
            .Append("<meta http-equiv='Content-Type' content='text/html; charset=windows-1252'>")
            .Append("<title>Correo Electrónico Autogenerado </title>")
            .Append("</head>")
            .Append("<body>")
            .Append(strBody.ToString())
            .Append("</body>")
            .Append("</html>")

        End With

        Return strHtmlBody.ToString()
    End Function

    Private Function ReemplazarMetadata(ByVal pstrCadena As String, ByVal pdr As DataRow) As String
        Dim strResult As String = ""

        strResult = pstrCadena
        strResult = strResult.Replace("[Nombre_Empresa]", pdr("vNombreEmpresa").ToString())
        strResult = strResult.Replace("[Fecha_Pago]", pdr("iFechaPago").ToString())
        strResult = strResult.Replace("[Cuenta_Abono]", pdr("iCuentaAbono").ToString())
        strResult = strResult.Replace("[Nombre_Funcionario_Convenios]", pdr("vNombreFuncionario").ToString())
        strResult = strResult.Replace("[Email_Funcionario_Convenios]", pdr("vEmailFuncionario").ToString())
        strResult = strResult.Replace("[Anexo_Funcionario_Convenios]", pdr("vAnexoFuncionario").ToString())
        strResult = strResult.Replace("[Mes]", clsPeriodo.NombreMes(Convert.ToInt32(pdr("iMesCuota").ToString())))
        strResult = strResult.Replace("[Anio]", pdr("iAñoCuota").ToString())
        strResult = strResult.Replace("[FECHA]", pdr("iFechaPago").ToString() + "/" + pdr("iMesCuota").ToString() + "/" + pdr("iAñoCuota").ToString())
        strResult = strResult.Replace("&R1", "<strong>")
        strResult = strResult.Replace("&R2", "</strong>")

        Return strResult
    End Function

    Private Function EnviarCorreoAlerta(ByVal pintCodigoIBS As Integer, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrCorreoDE As String, ByVal pstrCorreosPara As String, ByVal pstrCorreosCopia As String, ByVal pstrAsunto As String, ByVal pstrCuerpo As String, ByVal pstrAdjunto As String)
        Dim strCorreoElectronicoDE As String = String.Empty
        Dim strCorreoElectronicosPara As String = String.Empty
        Dim strCorreoElectronicosBCC As String = String.Empty
        Dim strCorreoElectronicoAsunto As String = String.Empty
        Dim strCorreoElectronicoNotificarA As String = String.Empty
        Dim strCorreoElectronicoCuerpo As String = String.Empty
        'Dim strPath As String = ConfigurationManager.AppSettings("ArchivosConvenio").ToString()
        Dim strPath As String = ""

        _dtParametrosEnvioMail = objSystemParametersBL.Seleccionar(ConfigurationManager.AppSettings(clsTiposSystemParameters.ParametroEnvioMail.ToString()))

        strPath = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.RutaDescargaNominas))("vValor").ToString().Trim()

        Dim intExport As Integer
        Dim strNameFile As String = String.Empty
        Dim strPathFile As String = String.Empty
        Dim strMensajeFile As String = String.Empty

        Dim strMensaje As String = ""

        Dim objUtils As New clsUtils

        If pstrAdjunto = 1 Then
            Try
                Dim _dtCuotas As New DataTable()

                _dtCuotas = objAlertasClienteBL.ObtenerCuotasVencidasAlertasEnviar(pintCodigoIBS)

                intExport = clsFiles.ExportToExcel(_dtCuotas, "xls", strPath, "", pintAnioPeriodo, pintMesPeriodo, "Alerta", strNameFile, strPathFile, strMensajeFile)
            Catch ex As Exception
                Throw ex
            End Try
        End If

        'Si no este TEST hacemos el envio regular
        Dim strTestOnly As String = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ModoPrueba))("vValor").ToString().Trim()

        If strTestOnly = "0" Then
            strCorreoElectronicosPara = pstrCorreosPara
        Else    ' en otro caso enviamos el correo a una direccion de prueba
            'strCorreoElectronicosPara = ConfigurationManager.AppSettings("mailTest").ToString()
            strCorreoElectronicosPara = _dtParametrosEnvioMail.Rows(Convert.ToInt32(enumParametroEnvioMail.ListaMailTest))("vValor").ToString().Trim()
        End If

        strCorreoElectronicoDE = pstrCorreoDE
        strCorreoElectronicosBCC = pstrCorreosCopia

        strCorreoElectronicoAsunto = pstrAsunto
        strCorreoElectronicoCuerpo = pstrCuerpo

        Dim ar As New ArrayList()

        For Each str As Object In strCorreoElectronicosPara.Split(";")
            ar.Add(New MailSource(str))
        Next

        Try
            If intExport = 0 And pstrAdjunto = 1 Then
                Dim strFullName = strPathFile + "\\" + strNameFile

                objUtils.SendNotification(strCorreoElectronicoDE, strCorreoElectronicosPara, strCorreoElectronicosBCC, strCorreoElectronicoAsunto, strCorreoElectronicoCuerpo, strFullName, True, "")
            Else
                objUtils.SendNotification(strCorreoElectronicoDE, strCorreoElectronicosPara, strCorreoElectronicosBCC, strCorreoElectronicoAsunto, strCorreoElectronicoCuerpo, "", True, "")
            End If

            Return strMensaje
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function FormarCorreoCopias(ByVal pintCodIBS As Integer) As String
        Dim strCorreo As String = ""
        Dim _dt As New DataTable()

        Try
            _dt = objClienteBL.ObtenerGestorConvenioPorCodigoIBSDesdeAS400(pintCodIBS)

            strCorreo = _dt.Rows(0)("GSTCOR").ToString()

            Return strCorreo
        Catch ex As Exception
            Return strCorreo
        End Try
    End Function

#End Region

    <WebMethod()> _
    Public Function ObtenerListClienteUltimoProceso() As DataTable
        Try
            Return objProcesosBL.ObtenerListaClienteUltimoProceso()
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    <WebMethod()> _
    Public Function ProcesarAlerta(ByVal pintCodigoProcesoAutomatico As Integer, ByVal pstrCodigoCliente As String, ByVal pintAnioPeriodo As Integer, ByVal pintMesPeriodo As Integer, ByVal pstrUsuario As String, ByRef pintEstado As Integer) As String
        Dim strMensaje As String = ""

        If String.IsNullOrEmpty(pstrCodigoCliente) Then
            strMensaje = clsMensajesGeneric.ParametroVacio.Replace("&1", "Codigo del Cliente")

            pintEstado = 0
            Return strMensaje
        ElseIf Not IsNumeric(pstrCodigoCliente) Then
            strMensaje = clsMensajesGeneric.CampoNoNumerico.Replace("&1", "Codigo del Cliente")

            pintEstado = 0
            Return strMensaje
        End If

        Dim _dtCliente As New DataTable()

        Try
            objClientes.CodigoCliente = Convert.ToInt32(pstrCodigoCliente)

            _dtCliente = objClienteBL.ObtenerClientePorCodigo(objClientes.CodigoCliente)

            Dim intCodIBS As Integer = Convert.ToInt32(_dtCliente.Rows(0)("Codigo_IBS").ToString())
            Dim decSaldoContable As Decimal

            Dim _dtInfoClienteIBS As New DataTable()

            Try
                _dtInfoClienteIBS = objClienteBL.ObtenerSaldoContablePorCodigoIBS(intCodIBS.ToString())

                decSaldoContable = Convert.ToDecimal(-1) * Convert.ToDecimal(_dtInfoClienteIBS.Rows(0)("ACMMGB").ToString())

                objAlertasClientes.iAlertaClienteId = 0
                objAlertasClientes.iAlertaId = 0
                objAlertasClientes.iClienteId = Convert.ToInt32(pstrCodigoCliente)
                objAlertasClientes.iEstado = 1

                Dim intTipoAlerta As Integer = 0
                Dim strAsunto As String = ""
                Dim strCuerpo As String = ""
                Dim strCorreoCopia As String = ""

                Dim _dtAlertasClientes As New DataTable()

                Try
                    _dtAlertasClientes = objAlertasClienteBL.ObtenerAlertasClientesEnviar(objAlertasClientes.iClienteId, decSaldoContable, pintAnioPeriodo, pintMesPeriodo)

                    If (_dtAlertasClientes.Rows(0)("vCorreosEnviar").ToString().Length > 0) Then
                        intTipoAlerta = Convert.ToInt32(_dtAlertasClientes.Rows(0)("iTipoAlerta").ToString())
                        strCorreoCopia = FormarCorreoCopias(intCodIBS)
                        strCorreoCopia = IIf(Len(strCorreoCopia) > 0, strCorreoCopia + "," + _dtAlertasClientes.Rows(0)("vEmailFuncionario").ToString(), _dtAlertasClientes.Rows(0)("vEmailFuncionario").ToString())
                        strAsunto = _dtAlertasClientes.Rows(0)("vAlerta").ToString() + " - " + ReemplazarMetadata(_dtAlertasClientes.Rows(0)("vAsuntoMensaje").ToString(), _dtAlertasClientes.Rows(0))
                        strCuerpo = ReemplazarMetadata(_dtAlertasClientes.Rows(0)("vCuerpoMensaje").ToString(), _dtAlertasClientes.Rows(0))
                        strCuerpo = ConvertirBody(strCuerpo)

                        Try
                            strMensaje = EnviarCorreoAlerta(intCodIBS, pintAnioPeriodo, pintMesPeriodo, _dtAlertasClientes.Rows(0)("vEmailFuncionario").ToString(), _dtAlertasClientes.Rows(0)("vCorreosEnviar").ToString(), strCorreoCopia, strAsunto, strCuerpo, _dtAlertasClientes.Rows(0)("vAdjunto").ToString())

                            If (strMensaje = "") Then
                                RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Enviado, pstrUsuario)

                                pintEstado = 1
                                Return strMensaje
                            Else
                                RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

                                pintEstado = 0
                                Return strMensaje
                            End If

                        Catch ex1 As Exception
                            strMensaje = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "EnviarCorreoAlerta").Replace("&2", enumGeneric.NoRecords.ToString()).Replace("&3", ex1.ToString())
                            RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

                            pintEstado = 0
                            Return strMensaje
                        End Try
                    Else
                        strMensaje = clsMensajesGeneric.MensajeNoRegistraCorreos.Replace("&1", _dtCliente.Rows(0)("Nombre_Cliente").ToString()).Replace("&2", pstrCodigoCliente)
                        RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Cancelado, pstrUsuario)

                        pintEstado = 1
                        Return strMensaje
                    End If

                Catch ex1 As HandledException
                    If ex1.ErrorTypeId = -400 Then
                        strMensaje = clsMensajesGeneric.MensajeNoAlertasEnviar.Replace("&1", _dtCliente.Rows(0)("Nombre_Cliente").ToString()).Replace("&2", pstrCodigoCliente)
                        RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

                        pintEstado = 0
                        Return strMensaje
                    Else
                        strMensaje = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ObtenerAlertasClientesEnviar").Replace("&2", enumGeneric.ErrorMessage.ToString()).Replace("&3", ex1.ErrorMessageFull)
                        RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, intTipoAlerta, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

                        pintEstado = 0
                        Return strMensaje
                    End If
                End Try

            Catch ex1 As Exception
                strMensaje = clsMensajesGeneric.ProcesoNoEncontrado.Replace("&1", _dtCliente.Rows(0)("Nombre_Cliente").ToString()).Replace("&2", pstrCodigoCliente)
                RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, intCodIBS, 0, "", pintAnioPeriodo, pintMesPeriodo, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

                pintEstado = 0
                Return strMensaje
            End Try

        Catch ex1 As HandledException
            strMensaje = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ObtenerClientePorCodigo").Replace("&2", ex1.ErrorTypeId.ToString()).Replace("&3", ex1.ErrorMessageFull)
            RegistrarLogEnvio(pintCodigoProcesoAutomatico, pstrCodigoCliente, 0, 0, "", 0, 0, strMensaje, enumLogEnvioCorreo.Error, pstrUsuario)

            pintEstado = 0
            Return strMensaje
        End Try

    End Function

End Class
