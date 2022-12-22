Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Collections
Imports System.Data
Imports System.Transactions
Public Class clsCuotaBL
    Private objEventoSistema As clsEventoSistema

    Private objProceso As clsProceso

    Private objCuotaDO As clsCuotaDO

    Private objCuotaDO2 As CuotaDO

    Private objProcesoBL As clsProcesoBL

    Private objEventoSistemaBL As clsEventoSistemaBL

    Public Sub New()
        MyBase.New()
        Me.objCuotaDO = New clsCuotaDO()
        Me.objCuotaDO2 = New CuotaDO()
        Me.objProcesoBL = New clsProcesoBL()
        Me.objEventoSistemaBL = New clsEventoSistemaBL()
    End Sub
    ' Methods
    Public Function ImportaPagareDeIBS(ByVal pstrCodigoClienteIBS As String, ByVal pstrAnio As String, ByVal pstrMes As String, ByVal pstrFechaProcesoAS400 As String, ByVal pstrCodigoCliente As String, ByVal pstrUsuario As String) As String
        Dim str As String
        Dim enumerator As IEnumerator = Nothing
        Dim enumerator1 As IEnumerator = Nothing
        Dim strCodigoProceso As String = ""
        Dim dtPagareIBS As DataTable = New DataTable()
        Dim dtDeudaIBS As DataTable = New DataTable()
        Try
            Dim _clsEventoSistemaBL As clsEventoSistemaBL = Me.objEventoSistemaBL
            Dim str1 As String = enumEstadoLog.Info.ToString()
            Dim strArrays() As String = {"Inicio del Metodo - Parametros: pstrCodigoClienteIBS=", pstrCodigoClienteIBS, ", pstrAnio=", pstrAnio, ", pstrMes=", pstrMes, ", pstrCodigoCliente=", pstrCodigoCliente}
            Me.objEventoSistema = _clsEventoSistemaBL.DevolverObjeto("BifConvenios", str1, "ImportaPagareDeIBS", String.Concat(strArrays), "", pstrUsuario)
            Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
            'dtPagareIBS = Me.objCuotaDO.ObtenerPagareDeIBS(pstrCodigoClienteIBS, pstrAnio, pstrMes)
            'dtDeudaIBS = Me.objCuotaDO.ObtenerDeudaDeIBS(pstrCodigoClienteIBS, pstrAnio, pstrMes)
            'Add 2022-06-03 Usar misma funcion que web
            dtPagareIBS = Me.objCuotaDO2.ObtenerPagaresDeIBS("", pstrCodigoClienteIBS, pstrAnio, pstrMes).Tables(0)
            dtDeudaIBS = Me.objCuotaDO2.ObtenerDeudaDeIBS("", pstrCodigoClienteIBS, pstrAnio, pstrMes).Tables(0)
            'end Add

            Using oScope As TransactionScope = New TransactionScope(TransactionScopeOption.RequiresNew, TimeSpan.FromMinutes(20))
                Try
                    Me.objProceso = Me.objProcesoBL.DevolverObjeto(pstrCodigoCliente, pstrAnio, pstrMes, pstrFechaProcesoAS400, pstrUsuario)
                    strCodigoProceso = Me.objProcesoBL.AdicionarProceso(Me.objProceso)
                    If (Microsoft.VisualBasic.CompilerServices.Operators.CompareString(strCodigoProceso, "-1", False) <> 0) Then
                        Try
                            enumerator = dtPagareIBS.Rows.GetEnumerator()
                            While enumerator.MoveNext()
                                Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                                current(0) = strCodigoProceso
                                Me.objCuotaDO.InsertaDLENV(current)
                            End While
                        Finally
                            If (TypeOf enumerator Is IDisposable) Then
                                TryCast(enumerator, IDisposable).Dispose()
                            End If
                        End Try
                        Try
                            enumerator1 = dtDeudaIBS.Rows.GetEnumerator()
                            While enumerator1.MoveNext()
                                Dim dr As DataRow = DirectCast(enumerator1.Current, DataRow)
                                dr(0) = strCodigoProceso
                                Me.objCuotaDO.InsertarHistoricoDLCCR(dr)
                            End While
                        Finally
                            If (TypeOf enumerator1 Is IDisposable) Then
                                TryCast(enumerator1, IDisposable).Dispose()
                            End If
                        End Try
                        Me.objCuotaDO.FinalizaImportacionPagares(strCodigoProceso, pstrUsuario)
                        Me.objEventoSistema = Me.objEventoSistemaBL.DevolverObjeto("BifConvenios", Conversions.ToString(1), "ImportaPagaresDeIBS", String.Concat("Finalizó Importación de Pagares: pCodigo_proceso=", strCodigoProceso), "", pstrUsuario)
                        Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
                        oScope.Complete()
                    Else
                        oScope.Dispose()
                        str = strCodigoProceso
                        Return str
                    End If
                Catch transactionException1 As System.Transactions.TransactionException
                    ProjectData.SetProjectError(transactionException1)
                    Dim transactionException As System.Transactions.TransactionException = transactionException1
                    oScope.Dispose()
                    Me.objEventoSistema = Me.objEventoSistemaBL.DevolverObjeto("BifConvenios", Conversions.ToString(3), "ImportaPagaresDeIBS", transactionException.Message, transactionException.StackTrace, pstrUsuario)
                    Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
                    Throw transactionException
                Catch handledException As Resource.HandledException
                    ProjectData.SetProjectError(handledException)
                    Dim ex2 As Resource.HandledException = handledException
                    oScope.Dispose()
                    Me.objEventoSistema = Me.objEventoSistemaBL.DevolverObjeto("BifConvenios", Conversions.ToString(3), "ImportaPagaresDeIBS", ex2.ErrorMessageFull, ex2.StackTrace, pstrUsuario)
                    Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
                    Throw ex2
                Catch exception As System.Exception
                    ProjectData.SetProjectError(exception)
                    Dim ex3 As System.Exception = exception
                    oScope.Dispose()
                    Me.objEventoSistema = Me.objEventoSistemaBL.DevolverObjeto("BifConvenios", Conversions.ToString(3), "ImportaPagaresDeIBS", ex3.Message, ex3.StackTrace, pstrUsuario)
                    Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
                    Throw ex3
                End Try
            End Using
            Dim _clsEventoSistemaBL1 As clsEventoSistemaBL = Me.objEventoSistemaBL
            Dim str2 As String = Conversions.ToString(1)
            strArrays = New String() {"Fin del Metodo - Parametros: pstrCodigoClienteIBS=", pstrCodigoClienteIBS, ", pstrAnio=", pstrAnio, ", pstrMes=", pstrMes, ", pstrCodigoCliente=", pstrCodigoCliente}
            Me.objEventoSistema = _clsEventoSistemaBL1.DevolverObjeto("BifConvenios", str2, "ImportaPagaresDeIBS", String.Concat(strArrays), "", pstrUsuario)
            Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
        Catch exception1 As System.Exception
            ProjectData.SetProjectError(exception1)
            Dim ex1 As System.Exception = exception1
            Me.objEventoSistema = Me.objEventoSistemaBL.DevolverObjeto("BifConvenios", Conversions.ToString(3), "ImportaPagaresDeIBS", ex1.Message, ex1.StackTrace, pstrUsuario)
            Me.objEventoSistemaBL.Insertar(Me.objEventoSistema)
            Throw ex1
        End Try
        str = strCodigoProceso
        Return str
    End Function
End Class
