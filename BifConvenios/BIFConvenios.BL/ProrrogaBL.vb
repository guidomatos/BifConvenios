Imports BIFConvenios.DO
Imports BIFUtils
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections
Imports System.Data
Imports System.Transactions
Public Class ProrrogaBL
    Private lLog As Log

    Public Sub New()
        MyBase.New()
        Me.lLog = New Log()
    End Sub
    ' Methods
    Public Function ProcesoProrroga(ByVal pNumeroLote As String, ByVal pUsuario As String) As Integer
        Dim enumerator As IEnumerator = Nothing
        Dim lProrrogaDO As ProrrogaDO = New ProrrogaDO()
        Dim lResultado As String = ""
        Dim lResult As Integer = 0
        Try
            Me.lLog.GrabarLog(Log.Level.Info, "ProcesoProrroga", String.Concat("Inicio del Metodo - Parametros:  pNumeroLote=", pNumeroLote), "", pUsuario)
            lResultado = lProrrogaDO.ProcesaProrrogaEnIBS(pNumeroLote)
            Dim lds As DataSet = lProrrogaDO.ObtieneInformacionProrrogaDeIBS(pNumeroLote)
            Using oScope As TransactionScope = New TransactionScope(TransactionScopeOption.RequiresNew)
                Me.lLog.GrabarLog(Log.Level.Info, "ProcesoProrroga", String.Concat("Inicio de Tx: pNumeroLote=", pNumeroLote), "", pUsuario)
                lProrrogaDO.ActualizaLoteProrroga(pNumeroLote, lResultado)
                If (lds.Tables.Count > 0) Then
                    Dim ldt As DataTable = lds.Tables(0)
                    Try
                        enumerator = ldt.Rows.GetEnumerator()
                        While enumerator.MoveNext()
                            Dim ldr As DataRow = DirectCast(enumerator.Current, DataRow)
                            lProrrogaDO.ActualizaClienteCuotaProrroga(pNumeroLote, Conversions.ToString(ldr("EDLNPGR")), Conversions.ToBoolean(Interaction.IIf(Microsoft.VisualBasic.CompilerServices.Operators.CompareString(lResultado.Trim(), "", False) = 0 Or Microsoft.VisualBasic.CompilerServices.Operators.CompareString(lResultado.Trim(), "error", False) = 0, False, Microsoft.VisualBasic.CompilerServices.Operators.CompareString(ldr("WFLG1").ToString().Trim(), "", False) = 0)), ldr("EDFLAGP").ToString().Trim())
                        End While
                    Finally
                        If (TypeOf enumerator Is IDisposable) Then
                            TryCast(enumerator, IDisposable).Dispose()
                        End If
                    End Try
                End If
                lProrrogaDO.ActualizaInformacionProrrogasCuotas(pNumeroLote)
                oScope.Complete()
            End Using
            Me.lLog.GrabarLog(Log.Level.Info, "ProcesoProrroga", String.Concat("Fin del Metodo - Parametros:  pNumeroLote=", pNumeroLote), "", pUsuario)
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex As System.Exception = exception
            Me.lLog.GrabarLog(Log.Level.Errores, "ProcesoProrroga", ex.Message, ex.StackTrace, pUsuario)
            lResult = 1
            ProjectData.ClearProjectError()
        End Try
        Return lResult
    End Function
End Class
