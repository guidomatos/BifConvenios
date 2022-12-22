Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Collections
Imports System.Data
Public Class clsSystemParametersBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function GetCodigoParametro(ByVal pintGrupoId As Integer, ByVal pstrNonbre As String) As Integer
        Dim num As Integer
        Dim table As New DataTable
        Dim num2 As Integer = 0
        Try
            Dim enumerator As IEnumerator = Nothing
            table = Singleton(Of clsSystemParametersDO).Create.Seleccionar(pintGrupoId)
            Try
                enumerator = table.Rows.GetEnumerator
                Do While True
                    If enumerator.MoveNext Then
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        If Not Operators.ConditionalCompareObjectEqual(current("vDescripcion"), pstrNonbre, False) Then
                            Continue Do
                        End If
                        num2 = Convert.ToInt32(current("vValor").ToString)
                    End If
                    Exit Do
                Loop
            Finally
                If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            num = num2
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

    Public Function GetNombreParametro(ByVal pintGrupoId As Integer, ByVal pintValor As Integer) As String
        Dim str As String
        Dim table As New DataTable
        Dim str2 As String = ""
        Try
            Dim enumerator As IEnumerator
            table = Singleton(Of clsSystemParametersDO).Create.Seleccionar(pintGrupoId)
            Try
                enumerator = table.Rows.GetEnumerator
                Do While True
                    If enumerator.MoveNext Then
                        Dim current As DataRow = DirectCast(enumerator.Current, DataRow)
                        If (Convert.ToInt32(current("vValor").ToString) <> pintValor) Then
                            Continue Do
                        End If
                        str2 = current("vDescripcion").ToString
                    End If
                    Exit Do
                Loop
            Finally
                If Not Object.ReferenceEquals(TryCast(enumerator, IDisposable), Nothing) Then
                    TryCast(enumerator, IDisposable).Dispose()
                End If
            End Try
            str = str2
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return str
    End Function

    Public Function Insert(ByVal objSystemParameters As clsSystemParameters) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsSystemParametersDO).Create.Insert(objSystemParameters)
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

    Public Function Seleccionar(ByVal pintGrupoId As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsSystemParametersDO).Create.Seleccionar(pintGrupoId)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function SeleccionarGrupos() As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsSystemParametersDO).Create.SeleccionarGrupos
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function
End Class
