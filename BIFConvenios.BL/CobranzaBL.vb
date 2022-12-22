Imports BIFConvenios.DO
Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Collections
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Transactions
Public Class CobranzaBL
    Private lRutaImportacionArchivos As String ' = ConfigurationManager.AppSettings("RutaImportacionArchivos").Trim()
    Private lLog As Log

    Public Sub New()
        MyBase.New()
        Me.lRutaImportacionArchivos = ConfigurationManager.AppSettings("RutaImportacionArchivos").Trim()
        Me.lLog = New Log()
    End Sub

    ' Methods
    Public Function ActivarPagoIBSOnline(ByVal strTipoOperacion As String, ByVal strCodigoClienteIBS As String, ByVal strFecha As String) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of CobranzaDO).Create.ActivarPagoIBSOnline(strTipoOperacion, strCodigoClienteIBS, strFecha)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return num
    End Function

    Public Function AnulaEnvioCobranzaIBS(ByVal pCodigo_proceso As String, ByVal pUsuario As String) As Integer
        Dim ado As New CobranzaDO
        Dim odo As New ProcesoDO
        Dim edo As New ClienteDO
        Dim table As New DataTable
        Dim pMes As String = ""
        Dim pAnio As String = ""
        Dim str3 As String = ""
        Dim pTipoDocumento As String = ""
        Dim pNumeroDocumento As String = ""
        Dim str2 As String = ""
        Dim num2 As Integer = 0
        Try
            Me.lLog.GrabarLog(1, "AnulaEnvioCobranzaIBS", ("Inicio del Metodo - Parametros:  pCodigo_proceso=" & pCodigo_proceso & ", pUsuario=" & pUsuario), "", pUsuario)
            table = odo.ObtieneProcesoCliente(pCodigo_proceso)
            If (table.Rows.Count > 0) Then
                pMes = Conversions.ToString(table.Rows(0)("Mes_Periodo"))
                pAnio = Conversions.ToString(table.Rows(0)("Anio_periodo"))
                str3 = Conversions.ToString(table.Rows(0)("Fecha_ProcesoAS400"))
                pTipoDocumento = Conversions.ToString(table.Rows(0)("TipoDocumento"))
                pNumeroDocumento = Conversions.ToString(table.Rows(0)("NumeroDocumento"))
            End If
            str2 = edo.ObtenerCodigoClienteIBS(pTipoDocumento, pNumeroDocumento)
            Dim objA As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                ado.EliminaCobranzaMasivaEnIBS(str2, pAnio, pMes, str3)
                ado.AnulaCobranza(pCodigo_proceso, pUsuario)
                objA.Complete()
            Finally
                If Not Object.ReferenceEquals(objA, Nothing) Then
                    objA.Dispose()
                End If
            End Try
            Me.lLog.GrabarLog(1, "AnulaEnvioCobranzaIBS", ("Fin del Metodo - Parametros:  pCodigo_proceso=" & pCodigo_proceso & ", pUsuario=" & pUsuario), "", pUsuario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            Me.lLog.GrabarLog(3, "AnulaEnvioCobranzaIBS", exception.Message, exception.StackTrace, pUsuario)
            num2 = 1
            ProjectData.ClearProjectError()
        End Try
        Return num2
    End Function

    Public Function EnvioCobranzaIBS(ByVal pCodigo_proceso As String, ByVal pUsuario As String) As Integer
        Dim ado As New CobranzaDO
        Dim odo As New ProcesoDO
        Dim table As New DataTable
        Dim num3 As Integer = 0
        Try
            Me.lLog.GrabarLog(1, "EnvioCobranzaIBSProcesa", ("Inicio del Metodo - Parametros:  pCodigo_proceso=" & pCodigo_proceso), "", pUsuario)
            Dim flag2 As Boolean = (odo.ObtieneProcesoCliente(pCodigo_proceso).Rows.Count > 0)
            If Not flag2 Then
                Me.lLog.GrabarLog(1, "EnvioCobranzaIBSProcesa", ("Parametros:  pCodigo_proceso=" & pCodigo_proceso), "El proceso no tiene datos de cobranzas.", pUsuario)
                GoTo TR_0002
            Else
                Dim enumerator As IEnumerator = Nothing
                odo.InicioEnvioCobranza(pCodigo_proceso, pUsuario)
                table = ado.ObtieneCobranzas(pCodigo_proceso)
                Try
                    enumerator = table.Rows.GetEnumerator
                    Do While True
                        flag2 = enumerator.MoveNext
                        If Not flag2 Then
                            Exit Do
                        End If
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        Try
                            ado.InsertaCobranzaEnIBS(current)
                            ado.ActualizaCobranza(pCodigo_proceso, current("DLNP").ToString.Trim, "E4")
                        Catch exception1 As Exception
                            Dim ex As Exception = exception1
                            ProjectData.SetProjectError(ex)
                            Dim exception As Exception = ex
                            Me.lLog.GrabarLog(3, "EnvioCobranzaIBSProcesa - Eliminamos Cobranza en IBS", exception.Message, exception.StackTrace, pUsuario)
                            ado.EliminaCobranzaEnIBS(current)
                            ado.ActualizaCobranza(pCodigo_proceso, current("DLNP").ToString.Trim, "EE")
                            ProjectData.ClearProjectError()
                        End Try
                    Loop
                Finally
                    If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                        TryCast(enumerator, IDisposable).Dispose()
                    End If
                End Try
            End If
            GoTo TR_0003
TR_0002:
            Me.lLog.GrabarLog(1, "EnvioCobranzaIBSProcesa", ("Fin del Metodo - Parametros:  pCodigo_proceso=" & pCodigo_proceso), "", pUsuario)
            ado.InsertaNominaEntradaSalida(pCodigo_proceso, 0, 0, 0, "R", "A", "O", pUsuario)
            Return num3
TR_0003:
            odo.FinalizaEnvioCobranza(pCodigo_proceso)
            GoTo TR_0002
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Dim exception2 As Exception = ex
            Me.lLog.GrabarLog(3, "EnvioCobranzaIBSProcesa", exception2.Message, exception2.StackTrace, pUsuario)
            num3 = 1
            ProjectData.ClearProjectError()
        End Try
        Return num3
    End Function

    Public Function ImportaDescuentosEmpresa(ByVal pNombreArchivo As String, ByVal pUsuario As String) As Integer
        Dim num As Integer
        Dim odo As New ProcesoDO
        Dim ado As New CobranzaDO
        Try
            Me.lLog.GrabarLog(1, "ImportaDescuentoEmpresa", ("Inicio del Metodo - Parametros: pNombreArchivo=" & pNombreArchivo & ",pUsuario=" & pUsuario), "", pUsuario)
            Dim str2 As String = String.Empty
            If ((clsFiles.ConvertToDateTable(pNombreArchivo, str2).Rows.Count > 0) And (str2 = "")) Then
            End If
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            ProjectData.ClearProjectError()
        End Try
        Return num
    End Function

    Public Function ImportaDescuentosEmpresa(ByVal pCodigo_proceso As String, ByVal pNombreArchivo As String, ByVal pUsuario As String) As String
        Dim table As DataTable = Nothing
        Dim table2 As DataTable = Nothing
        Dim odo As New ProcesoDO
        Dim ado As New CobranzaDO
        Dim str2 As String = String.Empty
        Try
            Dim strArray As String() = New String() {"Inicio del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ",pNombreArchivo=", pNombreArchivo, ",pUsuario=", pUsuario}
            Me.lLog.GrabarLog(1, "ImportaDescuentoEmpresa", String.Concat(strArray), "", pUsuario)
            table2 = clsFiles.ConvertToDateTable(pNombreArchivo, str2)
            Dim flag As Boolean = ((table2.Rows.Count > 0) And (str2 = ""))
            If Not flag Then
                Me.lLog.GrabarLog(3, "ImportaDescuentoEmpresa", str2, str2, pUsuario)
            Else
                Dim objA As New TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(10))
                Try
                    Dim enumerator As IEnumerator
                    odo.InicioDescuentoEmpresa(pCodigo_proceso, pUsuario)
                    table = New ArchivoXLS().FiltraSoloRegistrosXLS(table2)
                    Try
                        enumerator = table.Rows.GetEnumerator
                        Do While True
                            flag = enumerator.MoveNext
                            If Not flag Then
                                Exit Do
                            End If
                            Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                            ado.ArchivoDescuentosInserta(pCodigo_proceso, current)
                        Loop
                    Finally
                        If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                    ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                    objA.Complete()
                Finally
                    If Not Object.ReferenceEquals(objA, Nothing) Then
                        objA.Dispose()
                    End If
                End Try
                Me.lLog.GrabarLog(1, "ImportaDescuentoEmpresa", String.Concat(New String() {"Fin del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ",pNombreArchivo=", pNombreArchivo, ",pUsuario=", pUsuario}), "", pUsuario)
            End If
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            Me.lLog.GrabarLog(3, "ImportaDescuentoEmpresa", exception.ToString, exception.StackTrace, pUsuario)
            str2 = exception.ToString
            ProjectData.ClearProjectError()
        End Try
        Return str2
    End Function

    Public Function ImportaDescuentosEmpresa(ByVal pCodigo_proceso As String, ByVal pNombreArchivo As String, ByVal pTipoFormatoArchivo As String, ByVal pUsuario As String) As Integer
        Dim ado As New CobranzaDO
        Dim odo As New ProcesoDO
        Dim pData As String = ""
        Dim pNroLinea As Integer = 0
        Dim pFechaFormateada As String = Strings.Format(DateAndTime.Now, "yyyyMMddhhmmss")
        Dim obj2 As Object = (Me.lRutaImportacionArchivos & pNombreArchivo)
        Dim table As DataTable = Nothing
        Dim num3 As Integer = 0
        Try
            Dim strArray As String() = New String() {"Inicio del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ",pNombreArchivo=", pNombreArchivo, ",pTipoFormatoArchivo=", pTipoFormatoArchivo, ",pUsuario=", pUsuario}
            Me.lLog.GrabarLog(1, "ImportaDescuentoEmpresa", String.Concat(strArray), "", pUsuario)
            Dim flag As Boolean = (Strings.UCase(pTipoFormatoArchivo) = "XLS")
            If flag Then
                table = New ArchivoXLS().ImportaXLS((Me.lRutaImportacionArchivos & pNombreArchivo))
            End If
            Dim objA As New TransactionScope(TransactionScopeOption.RequiresNew)
            Try
                Dim current As DataRow
                Dim table2 As DataTable
                odo.InicioDescuentoEmpresa(pCodigo_proceso, pUsuario)
                Dim str4 As String = Strings.UCase(pTipoFormatoArchivo)
                flag = (str4 = "XLS")
                If flag Then
                    Dim enumerator As IEnumerator
                    table2 = New ArchivoXLS().FiltraSoloRegistrosXLS(table, ConfigurationManager.AppSettings("ArchivoDescuentosCampos").Split(New Char() {"|"c}))
                    Try
                        enumerator = table2.Rows.GetEnumerator
                        Do While True
                            flag = enumerator.MoveNext
                            If Not flag Then
                                Exit Do
                            End If
                            current = DirectCast(enumerator.Current, DataRow)
                            ado.ArchivoDescuentosInserta(pCodigo_proceso, current)
                        Loop
                    Finally
                        If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                    ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                Else
                    flag = (str4 = "CSV")
                    If flag Then
                        Dim enumerator As IEnumerator
                        table2 = New ArchivoCSV().ImportaCSV((Me.lRutaImportacionArchivos & pNombreArchivo))
                        Try
                            enumerator = table2.Rows.GetEnumerator
                            Do While True
                                flag = enumerator.MoveNext
                                If Not flag Then
                                    Exit Do
                                End If
                                current = DirectCast(enumerator.Current, DataRow)
                                ado.ArchivoDescuentosInserta(pCodigo_proceso, current)
                            Loop
                        Finally
                            If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                TryCast(enumerator, IDisposable).Dispose()
                            End If
                        End Try
                        ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                    Else
                        flag = (str4 = "TXT")
                        If flag Then
                            Dim enumerator As IEnumerator
                            'table2 = New ArchivoTXT().ImportaTXTEncolumnado((Me.lRutaImportacionArchivos & pNombreArchivo))
                            table2 = New ArchivoTXT().ImportaTXTGenerico((Me.lRutaImportacionArchivos & pNombreArchivo))
                            Try
                                enumerator = table2.Rows.GetEnumerator
                                Do While True
                                    flag = enumerator.MoveNext
                                    If Not flag Then
                                        Exit Do
                                    End If
                                    current = DirectCast(enumerator.Current, DataRow)
                                    '************************************************************************************************
                                    pData = current(0).ToString
                                    ado.TemporalArchivoTextoInserta(pCodigo_proceso, pUsuario, pNroLinea, pData, pFechaFormateada)
                                    pNroLinea += 1
                                    '************************************************************************************************
                                    'ado.ArchivoDescuentosInserta(pCodigo_proceso, current)
                                Loop
                            Finally
                                If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                    TryCast(enumerator, IDisposable).Dispose()
                                End If
                            End Try
                            ado.ImportaDescuentosClientePrepara(pCodigo_proceso, pFechaFormateada)
                            ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                        Else
                            flag = (str4 = "SDBF")
                            If flag Then
                                Dim enumerator As IEnumerator
                                table2 = New ArchivoDBF().LeerDBF((Me.lRutaImportacionArchivos & pNombreArchivo))
                                pNroLinea = 0
                                Try
                                    enumerator = table2.Rows.GetEnumerator
                                    Do While True
                                        flag = enumerator.MoveNext
                                        If Not flag Then
                                            Exit Do
                                        End If
                                        current = DirectCast(enumerator.Current, DataRow)
                                        strArray = New String() {Strings.LSet(Conversions.ToString(current("dni")), 15), Strings.RSet(Strings.Format(current("process"), "##########0.00"), 15), " ", Strings.Left(Strings.LSet((Strings.Trim(Conversions.ToString(current("apellidos"))) & " " & Strings.Trim(Conversions.ToString(current("nombres")))), 50), 50), " ", Strings.RSet(Strings.Format(current("monto"), "##########0.00"), 15)}
                                        pData = String.Concat(strArray)
                                        ado.TemporalArchivoTextoInserta(pCodigo_proceso, pUsuario, pNroLinea, pData, pFechaFormateada)
                                        pNroLinea += 1
                                    Loop
                                Finally
                                    If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                        TryCast(enumerator, IDisposable).Dispose()
                                    End If
                                End Try
                                ado.ImportaDescuentosClientePrepara(pCodigo_proceso, pFechaFormateada)
                                ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                            Else
                                flag = (str4 = "VDBF")
                                If flag Then
                                    Dim enumerator As IEnumerator
                                    table2 = New ArchivoDBF().LeerDBF((Me.lRutaImportacionArchivos & pNombreArchivo))
                                    pNroLinea = 0
                                    Try
                                        enumerator = table2.Rows.GetEnumerator
                                        Do While True
                                            flag = enumerator.MoveNext
                                            If Not flag Then
                                                Exit Do
                                            End If
                                            current = DirectCast(enumerator.Current, DataRow)
                                            strArray = New String() {Strings.LSet(Strings.Right(("0000000000" & Strings.Trim(Conversions.ToString(current("CODIGO")))), 10), 15), Strings.RSet(Strings.Format(current("MONVAR"), "##########0.00"), 15), " ", Strings.Left(Strings.LSet(Strings.Trim(Conversions.ToString(current("NOMPER"))), 50), 50), " ", Strings.LSet(Conversions.ToString(current("CLASE")), 15)}
                                            pData = String.Concat(strArray)
                                            ado.TemporalArchivoTextoInserta(pCodigo_proceso, pUsuario, pNroLinea, pData, pFechaFormateada)
                                            pNroLinea += 1
                                        Loop
                                    Finally
                                        If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                            TryCast(enumerator, IDisposable).Dispose()
                                        End If
                                    End Try
                                    ado.ImportaDescuentosClientePrepara(pCodigo_proceso, pFechaFormateada)
                                    ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                                Else
                                    flag = (str4 = "UTES")
                                    If flag Then
                                        Dim enumerator As IEnumerator
                                        table2 = New ArchivoTXT().ImportaTXTGenerico((Me.lRutaImportacionArchivos & pNombreArchivo))
                                        pNroLinea = 0
                                        Try
                                            enumerator = table2.Rows.GetEnumerator
                                            Do While True
                                                flag = enumerator.MoveNext
                                                If Not flag Then
                                                    Exit Do
                                                End If
                                                current = DirectCast(enumerator.Current, DataRow)
                                                pData = current(0).ToString.Replace(ChrW(27), "0")
                                                ado.TemporalArchivoTextoInserta(pCodigo_proceso, pUsuario, pNroLinea, pData, pFechaFormateada)
                                                pNroLinea += 1
                                            Loop
                                        Finally
                                            If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                                TryCast(enumerator, IDisposable).Dispose()
                                            End If
                                        End Try
                                        ado.ImportaDescuentosClientePrepara(pCodigo_proceso, pFechaFormateada)
                                        ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                                    Else
                                        Dim enumerator As IEnumerator
                                        table2 = New ArchivoTXT().ImportaTXTGenerico((Me.lRutaImportacionArchivos & pNombreArchivo))
                                        pNroLinea = 0
                                        Try
                                            enumerator = table2.Rows.GetEnumerator
                                            Do While True
                                                flag = enumerator.MoveNext
                                                If Not flag Then
                                                    Exit Do
                                                End If
                                                current = DirectCast(enumerator.Current, DataRow)
                                                pData = current(0).ToString
                                                ado.TemporalArchivoTextoInserta(pCodigo_proceso, pUsuario, pNroLinea, pData, pFechaFormateada)
                                                pNroLinea += 1
                                            Loop
                                        Finally
                                            If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                                                TryCast(enumerator, IDisposable).Dispose()
                                            End If
                                        End Try
                                        ado.ImportaDescuentosClientePrepara(pCodigo_proceso, pFechaFormateada)
                                        ado.ImportaDescuentosClienteProcesa(pCodigo_proceso, pUsuario)
                                    End If
                                End If
                            End If
                        End If
                    End If
                End If
                objA.Complete()
            Finally
                If Not Object.ReferenceEquals(objA, Nothing) Then
                    objA.Dispose()
                End If
            End Try
            Me.lLog.GrabarLog(1, "ImportaDescuentosClientes", String.Concat(New String() {"Fin del Metodo - Parametros:  pCodigo_proceso=", pCodigo_proceso, ",pNombreArchivo=", pNombreArchivo, ",pTipoFormatoArchivo=", pTipoFormatoArchivo, ",pUsuario=", pUsuario}), "", pUsuario)
        Catch exception1 As Exception
            Dim ex As Exception = exception1
            ProjectData.SetProjectError(ex)
            Dim exception As Exception = ex
            Me.lLog.GrabarLog(3, "ImportaDescuentosClientes", exception.Message, exception.StackTrace, pUsuario)
            num3 = 1
            ProjectData.ClearProjectError()
        End Try
        Return num3
    End Function

    Public Function ImportaDescuentosEmpresaAutomatico(ByVal pNombreArchivo As String, ByVal pUsuario As String, ByRef pCodigoProceso As String, ByRef pEstado As Integer, ByRef pCodigoCliente As Integer, ByRef pCodigoIBS As Integer, ByRef pAnioPeriodo As Integer, ByRef pMesPeriodo As Integer) As String
        Dim table As DataTable = Nothing
        Dim table2 As DataTable = Nothing
        Dim table3 As DataTable = Nothing
        Dim table5 As DataTable = Nothing
        Dim odo As New clsProcesoDO
        Dim ado As New CobranzaDO
        Dim edo As New clsClienteDO
        Dim sdo As New clsSystemParametersDO
        Dim pintCodigoCliente As Integer = 0
        Dim str6 As String = ""
        Dim num2 As Integer = 0
        Dim str2 As String = String.Empty
        Dim str5 As String = String.Empty
        Dim procesandoArchivoDescuento As String = String.Empty
        Dim num3 As Integer = 0
        Dim num4 As Integer = 0
        Dim num9 As Integer = 0
        Dim num8 As Integer = 0
        Dim num5 As Integer = 0
        Dim num7 As Integer = 0
        Dim str3 As String = ""
        Try
            odo.ActualizaFlagCargaAutomatica("L", 1)
            table3 = clsFiles.ConvertToDateTable(pNombreArchivo, procesandoArchivoDescuento)
            Dim flag4 As Boolean = ((table3.Rows.Count > 0) And (procesandoArchivoDescuento = ""))
            If Not flag4 Then
                procesandoArchivoDescuento = "Error: El archivo no contiene registros a Procesar."
                pEstado = 3
            Else
                Try
                    pintCodigoCliente = Convert.ToInt32(table3.Rows(0)("CodigoEmpresa").ToString)
                    pCodigoCliente = pintCodigoCliente
                    str2 = table3.Rows(0)("Anio").ToString
                    pAnioPeriodo = Convert.ToInt32(str2)
                    str5 = table3.Rows(0)("Mes").ToString
                    pMesPeriodo = Convert.ToInt32(str5)
                    Dim str7 As String = sdo.Seleccionar(Conversions.ToInteger(ConfigurationManager.AppSettings(clsTiposSystemParameters.ParametroEnvioMail.ToString))).Rows(Convert.ToInt32(1))("vValor").ToString.Trim
                    table = edo.ObtenerClientePorCodigo(pintCodigoCliente)
                    str6 = table.Rows(0)("Nombre_Cliente").ToString.Trim
                    num2 = Conversions.ToInteger(table.Rows(0)("codigo_IBS"))
                    pCodigoIBS = num2
                    table5 = odo.ObtenerListaProcesosEsperaArchivoDescuentoByCliente(str2, str5, pintCodigoCliente)
                    pCodigoProceso = table5.Rows(0)("Codigo_proceso").ToString
                    str3 = table5.Rows(0)("Estado").ToString
                    If Not (((str3 <> "A2") And (str3 <> "A3")) And (str3 <> "EA")) Then
                        procesandoArchivoDescuento = ("Error: El archivo del Cliente con Codigo: " & pintCodigoCliente.ToString & ", ya fue enviado al AS-400")
                        pEstado = 3
                    Else
                        Dim num10 As Integer = CInt(-odo.ConsultaFlagCargaAutomatica)
                        flag4 = (-Not odo.ArchivoDescuentosEnProceso And Not num10)
                        If Not flag4 Then
                            procesandoArchivoDescuento = clsMensajesGeneric.ProcesandoArchivoDescuento
                            pEstado = 2
                        Else
                            Dim objA As New TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(10))
                            Try
                                Dim enumerator As IEnumerator
                                odo.IniciarDescuentoEmpresa(pCodigoProceso, pUsuario)
                                table2 = New ArchivoXLS().FiltraSoloRegistrosXLS(table3)
                                Try
                                    enumerator = table2.Rows.GetEnumerator
                                    Do While True
                                        flag4 = enumerator.MoveNext
                                        If Not flag4 Then
                                            Exit Do
                                        End If
                                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                                        ado.ArchivoDescuentosInserta(pCodigoProceso, current)
                                    Loop
                                Finally
                                    flag4 = Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing)
                                    If flag4 Then
                                        TryCast(enumerator, IDisposable).Dispose()
                                    End If
                                End Try
                                ado.ImportaDescuentosClienteProcesa(pCodigoProceso, pUsuario)
                                objA.Complete()
                            Finally
                                flag4 = Not Object.ReferenceEquals(objA, Nothing)
                                If flag4 Then
                                    objA.Dispose()
                                End If
                            End Try
                            flag4 = odo.FinalProcesoCargaDescuentos(pCodigoProceso)
                            If Not flag4 Then
                                procesandoArchivoDescuento = clsMensajesGeneric.NoTerminoCargaProcesoDescuento.Replace("&1", pintCodigoCliente.ToString)
                                pEstado = 3
                            Else
                                Dim resumenProcesoDescuentos As New DataTable
                                Try
                                    resumenProcesoDescuentos = odo.GetResumenProcesoDescuentos(pCodigoProceso)
                                    num3 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("ErroresArchivo").ToString)
                                    num4 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("ErroresArchivoDescuento").ToString)
                                    num9 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("Validos").ToString)
                                    num8 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("TotalArchivo").ToString)
                                    num5 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("ErroresTablaEnvio").ToString)
                                    num7 = Convert.ToInt32(resumenProcesoDescuentos.Rows(0)("Total").ToString)
                                    Dim flag As Boolean = Convert.ToBoolean(resumenProcesoDescuentos.Rows(0)("flgCorreccion").ToString)
                                    Dim strArray As String() = New String() {str7, "\\", str6, "-", num2.ToString, "\\", str2, "\\", clsFiles.ConvertMes(Convert.ToInt32(str5))}
                                    strArray(9) = "\\Recepcion"
                                    Dim path As String = String.Concat(strArray)
                                    If Not Directory.Exists(path) Then
                                        Directory.CreateDirectory(path)
                                    End If
                                    strArray = New String() {path, "\\", str6, "-", num2.ToString, "(", DateTime.Now.Year.ToString, DateTime.Now.Month.ToString, DateTime.Now.Day.ToString}
                                    strArray(9) = "-"
                                    strArray(10) = DateTime.Now.Hour.ToString
                                    strArray(11) = DateTime.Now.Minute.ToString
                                    strArray(12) = DateTime.Now.Second.ToString
                                    strArray(13) = ").xls"
                                    Dim destFileName As String = String.Concat(strArray)
                                    File.Copy(pNombreArchivo, destFileName, True)
                                    File.Delete(pNombreArchivo)
                                Catch exception1 As HandledException
                                    Dim ex As HandledException = exception1
                                    ProjectData.SetProjectError(ex)
                                    procesandoArchivoDescuento = ex.ErrorMessageFull
                                    pCodigoProceso = ""
                                    ProjectData.ClearProjectError()
                                End Try
                                pEstado = 1
                            End If
                        End If
                    End If
                Catch exception4 As HandledException
                    Dim ex As HandledException = exception4
                    ProjectData.SetProjectError(ex)
                    Dim exception2 As HandledException = ex
                    procesandoArchivoDescuento = IIf((exception2.ErrorTypeId <> -400), clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ObtenerListaProcesosEsperaArchivoDescuentoByCliente").Replace("&2", DirectCast(2, enumGeneric).ToString).Replace("&3", exception2.ErrorMessageFull), clsMensajesGeneric.ProcesoNoValidoPago.Replace("&1", pintCodigoCliente.ToString).Replace("&2", str2.ToString).Replace("&3", str5.ToString))
                    pEstado = 3
                    ProjectData.ClearProjectError()
                End Try
            End If
            odo.ActualizaFlagCargaAutomatica("L", 0)
        Catch exception5 As HandledException
            Dim ex As HandledException = exception5
            ProjectData.SetProjectError(ex)
            Dim exception3 As HandledException = ex
            procesandoArchivoDescuento = clsMensajesGeneric.ExcepcionControlada.Replace("&1", "ConvertToTable").Replace("&2", DirectCast(2, enumGeneric).ToString).Replace("&3", exception3.ErrorMessageFull)
            odo.ActualizaFlagCargaAutomatica("L", 0)
            pEstado = 3
            ProjectData.ClearProjectError()
        End Try
        Return procesandoArchivoDescuento
    End Function
End Class
