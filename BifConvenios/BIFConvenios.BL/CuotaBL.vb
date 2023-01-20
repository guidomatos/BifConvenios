Imports BIFConvenios.DO
Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data
Imports System.Transactions
Public Class CuotaBL
    Private lRutaGeneracionArchivos As String

    Private lLog As Log

    Public Sub New()
        MyBase.New()
        Me.lRutaGeneracionArchivos = ConfigurationManager.AppSettings("RutaGeneracionArchivos").Trim()
        Me.lLog = New Log()
    End Sub

    ' Methods
    Public Function ConsultarPagaresDeIBS(ByVal pCodigo_ClienteIBS As String, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String, ByVal pCodigo_Cliente As String, ByVal pUsuario As String) As DataTable
        Dim table As DataTable
        Dim ado As New CuotaDO
        Dim odo As New ProcesoDO
        Try
            Me.lLog.GrabarLog(1, "ConsultarPagareDeIBS", String.Concat(New String() {"Inicio del Metodo - Parametros: pCodigo_ClienteIBS=", pCodigo_ClienteIBS, ", pAnio=", pAnio, ", pMes=", pMes, ", pCodigo_Cliente=", pCodigo_Cliente}), "", pUsuario)
            Dim table2 As DataTable = ado.ObtenerPagaresDeIBS("", pCodigo_ClienteIBS, pAnio, pMes).Tables(0)
            Me.lLog.GrabarLog(1, "ImportaPagaresDeIBS", String.Concat(New String() {"Fin del Metodo - Parametros: pCodigo_ClienteIBS=", pCodigo_ClienteIBS, ", pAnio=", pAnio, ", pMes=", pMes, ", pCodigo_Cliente=", pCodigo_Cliente}), "", pUsuario)
            table = table2
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            Me.lLog.GrabarLog(3, "ImportaPagaresDeIBS", exception.Message, exception.StackTrace, pUsuario)
            table = Nothing
            ProjectData.ClearProjectError()
            Return table
        End Try
        Return table
    End Function

    Private Function CronogramaFuturo(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Dim pNombreFormatoArchivo As String = New FormatoArchivoClienteDO().ObtieneNombreFormatoArchivo(pCodigo_proceso)
        Return New CuotaDO().CronogramaFuturo(pCodigo_proceso, pSituacionTrabajador, pNombreFormatoArchivo)
    End Function

    Private Function CronogramaFuturoDefault(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Return New CuotaDO().CronogramaFuturoDefault(pCodigo_proceso, pSituacionTrabajador)
    End Function

    Private Function CronogramaFuturoDefault_ConDescuento(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Return New CuotaDO().CronogramaFuturoDefault_ConDescuento(pCodigo_proceso, pSituacionTrabajador)
    End Function

    Private Function CronogramaFuturoExecutor(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String, ByVal pFormato As String) As DataSet
        Return New CuotaDO().CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, pFormato)
    End Function

    Private Function CronogramaFuturoTexto(ByVal pCodigo_proceso As String, ByVal pSituacionTrabajador As String) As DataSet
        Dim pNombreFormatoArchivo As String = New FormatoArchivoClienteDO().ObtieneNombreFormatoArchivo(pCodigo_proceso)
        Return New CuotaDO().CronogramaFuturoTexto(pCodigo_proceso, pSituacionTrabajador, pNombreFormatoArchivo)
    End Function

    Public Function GeneraCronogramaFuturo(ByVal pCodigo_proceso As String, ByVal pTipoFormatoArchivo As String, ByVal pSituacionTrabajador As String, ByVal pUsuario As String) As Integer
        Dim lds As DataSet
        Dim chrArray As Char()
        Dim lProcesoDO As ProcesoDO = New ProcesoDO()
        Dim lNombreArchivoProceso As String = ""
        Dim lPrefijoArchivoProceso As String = ""
        Dim lTipoCliente As String = ""
        Dim lFormatoArchivo As String = ""
        Dim lResult As Integer = 0
        Try
            Dim log As BIFUtils.Log = Me.lLog
            Dim pCodigoProceso() As String = {"Inicio del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ", pTipoFormatoArchivo=", pTipoFormatoArchivo, ", pSituacionTrabajador=", pSituacionTrabajador}
            log.GrabarLog(BIFUtils.Log.Level.Info, "GeneraCronogramaFuturo", String.Concat(pCodigoProceso), "", pUsuario)

            'If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pSituacionTrabajador.Trim(), "", False) = 0) Then
            'lTipoCliente = ""
            'Else
            '	lTipoCliente = If(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pSituacionTrabajador.Trim(), "-", False) <> 0, String.Concat("-", pSituacionTrabajador), "-Todos")
            'End If

            'Variables para Nombre de Archivo
            If pSituacionTrabajador.Trim <> "" Then
                If pSituacionTrabajador.Trim = "-" Then
                    lTipoCliente = "-Todos"
                Else
                    lTipoCliente = "-" + pSituacionTrabajador
                End If
            Else
                lTipoCliente = ""
            End If

            'lPrefijoArchivoProceso = lProcesoDO.ObtieneNombreArchivoProceso(pCodigo_proceso)
            'lNombreArchivoProceso = String.Concat(lPrefijoArchivoProceso, lTipoCliente)
            If (Strings.InStr(pTipoFormatoArchivo, "=", CompareMethod.Text) > 0) Then
                chrArray = New Char() {"="c}
                lFormatoArchivo = pTipoFormatoArchivo.Split(chrArray)(0)
                chrArray = New Char() {"="c}
                pTipoFormatoArchivo = pTipoFormatoArchivo.Split(chrArray)(1)
            End If

            lPrefijoArchivoProceso = lProcesoDO.ObtieneNombreArchivoProceso(pCodigo_proceso, lFormatoArchivo)
            'POB - 10/07/2020
            If (lFormatoArchivo.Equals("TXTMinsaCAS") Or lFormatoArchivo.Equals("PRNMinsaPensionistasYNombrados") Or lFormatoArchivo.Equals("ESSALUDCESANTES") Or lFormatoArchivo.Equals("ESSALUDACTIVOS") Or lFormatoArchivo.Equals("ESSALUDDESEMBOLSOS") Or lFormatoArchivo.Equals("MDGPEXCELCESANTES") Or lFormatoArchivo.Equals("MDGPEXCELACTIVOS") Or lFormatoArchivo.Equals("MDGPTXTCESANTES") Or lFormatoArchivo.Equals("MDGPTXTACTIVOS")) Then
                lNombreArchivoProceso = lPrefijoArchivoProceso
            Else
                lNombreArchivoProceso = String.Concat(lPrefijoArchivoProceso, lTipoCliente)
            End If
            'POB - 10/07/2020   

            Dim log1 As BIFUtils.Log = Me.lLog
            pCodigoProceso = New String() {"Inicio de Generación de Archivos: pCodigo_proceso=", pCodigo_proceso, ", pTipoFormatoArchivo=", pTipoFormatoArchivo, ", lFormatoArchivo=", lFormatoArchivo, ", pSituacionTrabajador=", pSituacionTrabajador}
            log1.GrabarLog(BIFUtils.Log.Level.Info, "GeneraCronogramaFuturo", String.Concat(pCodigoProceso), "", pUsuario)
            Dim str As String = Strings.UCase(pTipoFormatoArchivo)
            If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "CSV", False) = 0) Then
                lds = Me.CronogramaFuturo(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoCSV As BIFUtils.ArchivoCSV = New BIFUtils.ArchivoCSV()
                archivoCSV.ExportaCSV(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".csv"))
                archivoCSV = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "CSV2", False) = 0) Then
                lds = Me.CronogramaFuturoDefault(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoCSV1 As BIFUtils.ArchivoCSV = New BIFUtils.ArchivoCSV()
                archivoCSV1.ExportaCSV(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".csv"))
                archivoCSV1 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "CSVD", False) = 0) Then
                lds = Me.CronogramaFuturoDefault_ConDescuento(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoCSV2 As BIFUtils.ArchivoCSV = New BIFUtils.ArchivoCSV()
                archivoCSV2.ExportaCSV(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".csv"))
                archivoCSV2 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "TXT", False) = 0) Then
                lds = Me.CronogramaFuturoTexto(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoTXT As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".txt"))
                archivoTXT = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "PRN", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoTXT1 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT1.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".prn"))
                archivoTXT1 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "TXT2", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoTXT1 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT1.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".txt"))
                archivoTXT1 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "XLS", False) = 0) Then
                lds = Me.CronogramaFuturoTexto(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoXL As ArchivoXLS = New ArchivoXLS()
                archivoXL.ExportaXLS(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".xls"))
                archivoXL = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "XLS2", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoXL1 As ArchivoXLS = New ArchivoXLS()
                archivoXL1.ExportaXLS(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".xls"))
                archivoXL1 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "DEFAULTXLS", False) = 0) Then
                lds = Me.CronogramaFuturoDefault(pCodigo_proceso, pSituacionTrabajador)
                Dim archivoXL2 As ArchivoXLS = New ArchivoXLS()
                archivoXL2.ExportaXLS(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".xls"))
                archivoXL2 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "SANFERNAND", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoTXT2 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT2.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".txt"))
                archivoTXT2 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "ADBF", False) = 0) Then
                Dim archivoDBF As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                chrArray = New Char() {"-"c}
                archivoDBF.ExportaDBFAMAZONAS(lds, String.Concat(pSituacionTrabajador, lNombreArchivoProceso.Split(chrArray)(0), ".26"))
                archivoDBF = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "JDBF", False) = 0) Then
                Dim archivoDBF1 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(lPrefijoArchivoProceso, "UNIDAD", False) <> 0) Then
                    lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Else
                    lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                    chrArray = New Char() {"-"c}
                    archivoDBF1.ExportaDBFChulucanas(lds, String.Concat(pSituacionTrabajador, lNombreArchivoProceso.Split(chrArray)(0), ".26"))
                End If
                archivoDBF1 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "XDBF", False) = 0) Then
                Dim archivoDBF2 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                chrArray = New Char() {"-"c}
                archivoDBF2.ExportaDBFJAEN(lds, String.Concat(pSituacionTrabajador, lNombreArchivoProceso.Split(chrArray)(0), ".26"))
                archivoDBF2 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "IDBF", False) = 0) Then
                Dim archivoDBF3 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "BDBF", False) = 0) Then
                Dim archivoDBF4 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                chrArray = New Char() {"-"c}
                archivoDBF4.ExportaDBFSanIgnacio(lds, String.Concat(pSituacionTrabajador, lNombreArchivoProceso.Split(chrArray)(0), ".26"))
                archivoDBF4 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "UDBF", False) = 0) Then
                Dim archivoDBF5 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                archivoDBF5.ExportaDBFUNICA(lds, String.Concat(lNombreArchivoProceso, ".DBF"))
                archivoDBF5 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "VDBF", False) = 0) Then
                Dim archivoDBF6 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                archivoDBF6.ExportaDBFUNFV(lds, String.Concat(lNombreArchivoProceso, ".DBF"))
                archivoDBF6 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "MINSA", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoTXT3 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT3.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".txt"))
                archivoTXT3 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "ASDF", False) = 0) Then
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                Dim archivoTXT4 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                archivoTXT4.ExportaTXT(lds, Me.lRutaGeneracionArchivos, Conversions.ToString(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(Microsoft.VisualBasic.CompilerServices.Operators.AddObject(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(pSituacionTrabajador, "-", False) = 0, "T", pSituacionTrabajador), lPrefijoArchivoProceso), ".0205")))
                archivoTXT4 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "SDBF", False) = 0) Then
                Dim archivoDBF7 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                archivoDBF7.ExportaDBFSIGA(lds, String.Concat(lNombreArchivoProceso, ".DBF"))
                archivoDBF7 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "MDBF", False) = 0) Then
                Dim archivoDBF8 As BIFUtils.ArchivoDBF = New BIFUtils.ArchivoDBF()
                lds = Me.CronogramaFuturoExecutor(pCodigo_proceso, pSituacionTrabajador, lFormatoArchivo)
                archivoDBF8.ExportaDBFUNMSM(lds, String.Concat(lNombreArchivoProceso, ".DBF"), pSituacionTrabajador)
                archivoDBF8 = Nothing
            ElseIf (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(str, "XLS3", False) <> 0) Then
                Dim archivoTXT5 As BIFUtils.ArchivoTXT = New BIFUtils.ArchivoTXT()
                lds = Me.CronogramaFuturoTexto(pCodigo_proceso, pSituacionTrabajador)
                archivoTXT5.ExportaTXT(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".txt"))
                archivoTXT5 = Nothing
            Else
                lds = Me.CronogramaFuturoDefault(pCodigo_proceso, pSituacionTrabajador)
                Dim lArchivo As ArchivoXLS = New ArchivoXLS()
                lArchivo.ExportaXLS(lds, Me.lRutaGeneracionArchivos, String.Concat(lNombreArchivoProceso, ".xls"))
                lArchivo = Nothing
            End If
            Me.lLog.GrabarLog(BIFUtils.Log.Level.Info, "GeneraCronogramaFuturo", String.Concat("Actualiza estado del Proceso: pCodigo_proceso=", pCodigo_proceso), "", pUsuario)
            lProcesoDO.ActualizaGeneracionArchivo(pCodigo_proceso, "Server")
            Me.lLog.GrabarLog(BIFUtils.Log.Level.Info, "GeneraCronogramaFuturo", String.Concat("Finaliza estado del Proceso: pCodigo_proceso=", pCodigo_proceso), "", pUsuario)
            lProcesoDO.FinalizaGeneracionArchivo(pCodigo_proceso, "Server")
            Dim log2 As BIFUtils.Log = Me.lLog
            pCodigoProceso = New String() {"Fin del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ", pTipoFormatoArchivo=", pTipoFormatoArchivo, ", pSituacionTrabajador=", pSituacionTrabajador}
            log2.GrabarLog(BIFUtils.Log.Level.Info, "GeneraCronogramaFuturo", String.Concat(pCodigoProceso), "", pUsuario)
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex As System.Exception = exception
            Me.lLog.GrabarLog(BIFUtils.Log.Level.Errores, "GeneraCronogramaFuturo", ex.Message, ex.StackTrace, pUsuario)
            lResult = 1
            ProjectData.ClearProjectError()
        End Try
        Return lResult
    End Function

    Public Function ImportaPagaresDeIBS(ByVal pCodigo_ClienteIBS As String, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String, ByVal pCodigo_Cliente As String, ByVal pUsuario As String) As String
        Dim num As Integer
        Dim ado As New CuotaDO
        Dim odo As New ProcesoDO
        Dim str2 As String = ""
        Dim table2 As New DataTable
        Dim table As New DataTable
        Try
            Me.lLog.GrabarLog(1, "ImportaPagaresDeIBS", String.Concat(New String() {"Inicio del Metodo - Parametros: pCodigo_ClienteIBS=", pCodigo_ClienteIBS, ", pAnio=", pAnio, ", pMes=", pMes, ", pCodigo_Cliente=", pCodigo_Cliente}), "", pUsuario)
            table2 = ado.ObtenerPagaresDeIBS("", pCodigo_ClienteIBS, pAnio, pMes).Tables(0)
            table = ado.ObtenerDeudaDeIBS("", pCodigo_ClienteIBS, pAnio, pMes).Tables(0)
            Dim objA As New TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(10))
            Try
                str2 = odo.AdicionaProceso(Conversions.ToInteger(pCodigo_Cliente), pAnio, pMes, pFecha_ProcesoAS400, pUsuario) ''pMes
                Dim flag As Boolean = (str2 = "-1")
                If Not flag Then
                    Dim enumerator As IEnumerator = Nothing
                    Dim enumerator2 As IEnumerator = Nothing
                    Try
                        enumerator = table2.Rows.GetEnumerator
                        Do While True
                            flag = enumerator.MoveNext
                            If Not flag Then
                                Exit Do
                            End If
                            Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                            current(0) = str2
                            ado.InsertaDLENV(current)
                        Loop
                    Finally
                        If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                    Try
                        enumerator2 = table.Rows.GetEnumerator
                        Do While True
                            flag = enumerator2.MoveNext
                            If Not flag Then
                                Exit Do
                            End If
                            Dim current As DataRow = DirectCast(enumerator2.Current, DataRow)
                            current(0) = str2
                            ado.InsertaHistoricoDLCCR(current)
                        Loop
                    Finally
                        If Not Object.ReferenceEquals(TryCast(enumerator2, IDisposable), Nothing) Then
                            TryCast(enumerator2, IDisposable).Dispose()
                        End If
                    End Try
                    ado.FinalizaImportacionPagares(str2, pUsuario)
                    Me.lLog.GrabarLog(1, "ImportaPagaresDeIBS", ("Finalizó Importación de Pagares: pCodigo_proceso=" & str2), "", pUsuario)
                    objA.Complete()
                Else
                    objA.Dispose()
                    Return str2
                End If
            Catch exception1 As TransactionException
                Dim ex As TransactionException = exception1
                ProjectData.SetProjectError(ex)
                Dim exception As TransactionException = ex
                Dim str3 As String = exception.ToString
                objA.Dispose()
                Me.lLog.GrabarLog(3, "ImportaPagaresDeIBS", exception.Message, exception.StackTrace, pUsuario)
                num = 1
                ProjectData.ClearProjectError()
            Finally
                If Not Object.ReferenceEquals(objA, Nothing) Then
                    objA.Dispose()
                End If
            End Try
            Me.lLog.GrabarLog(1, "ImportaPagaresDeIBS", String.Concat(New String() {"Fin del Metodo - Parametros: pCodigo_ClienteIBS=", pCodigo_ClienteIBS, ", pAnio=", pAnio, ", pMes=", pMes, ", pCodigo_Cliente=", pCodigo_Cliente}), "", pUsuario)
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Dim exception2 As Exception = ex
            Dim str4 As String = exception2.ToString
            Me.lLog.GrabarLog(3, "ImportaPagaresDeIBS", exception2.Message, exception2.StackTrace, pUsuario)
            num = 1
            ProjectData.ClearProjectError()
        End Try
        Return str2
    End Function

    Public Function ObtenerCabeceraCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Return New CuotaDO().ObtenerCabeceraCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function

    Public Function ObtenerDetalleCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Return New CuotaDO().ObtenerDetalleCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function

    Public Function ObtenerMotivosDeIBS(ByVal pCodigo_proceso As String, ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Return New CuotaDO().ObtenerMotivosDeIBS(pCodigo_proceso, pCodigo_Cliente, pAnio, pMes)
    End Function

End Class
