Imports BIFConvenios.DO
Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Transactions
Public Class BloqueoBL
    ' Fields
    Private lLog As Log = New Log

    ' Methods
    Public Function ProcesoBloqueo(ByVal pNumeroLote As String, ByVal pUsuario As String) As Integer
        Dim enumerator As IEnumerator = Nothing
        Dim lBloqueoDO As BloqueoDO = New BloqueoDO()
        Dim lResultado As String = ""
        Dim lResult As Integer = 0
        Try
            Me.lLog.GrabarLog(Log.Level.Info, "ProcesoBloqueo", String.Concat("Inicio del Metodo - Parametros:  pNumeroLote=", pNumeroLote), "", pUsuario)
            lResultado = lBloqueoDO.ProcesaBloqueoEnIBS(pNumeroLote)
            Dim lds As DataSet = lBloqueoDO.ObtieneInformacionBloqueoDeIBS(pNumeroLote)
            Using oScope As TransactionScope = New TransactionScope(TransactionScopeOption.RequiresNew)
                Me.lLog.GrabarLog(Log.Level.Info, "ProcesoBloqueo", String.Concat("Inicio de Tx: pNumeroLote=", pNumeroLote), "", pUsuario)
                lBloqueoDO.ActualizaLoteBloqueo(pNumeroLote, lResultado)
                If (lds.Tables.Count > 0) Then
                    Dim ldt As DataTable = lds.Tables(0)
                    Try
                        enumerator = ldt.Rows.GetEnumerator()
                        While enumerator.MoveNext()
                            Dim ldr As DataRow = DirectCast(enumerator.Current, DataRow)
                            lBloqueoDO.ActualizaClienteCuotaBloqueo(pNumeroLote, Conversions.ToString(ldr("EDLNPGR")), Conversions.ToBoolean(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(lResultado.Trim(), "", False) = 0 Or Microsoft.VisualBasic.CompilerServices.Operators.CompareString(lResultado.Trim(), "error", False) = 0, False, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ldr("EDLFLG1").ToString().Trim(), "", False) = 0)))
                        End While
                    Finally
                        If (TypeOf enumerator Is IDisposable) Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                End If
                lBloqueoDO.ActualizaInformacionBloqueosCuotas(pNumeroLote)
                oScope.Complete()
            End Using
            Me.lLog.GrabarLog(Log.Level.Info, "ProcesoBloqueo", String.Concat("Fin del Metodo - Parametros:  pNumeroLote=", pNumeroLote), "", pUsuario)
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex As System.Exception = exception
            Me.lLog.GrabarLog(Log.Level.Errores, "ProcesoBloqueo", ex.Message, ex.StackTrace, pUsuario)
            lResult = 1
            ProjectData.ClearProjectError()
        End Try
        Return lResult
    End Function
End Class
