Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Public Class clsArchivosConveniosBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insert(ByVal objArchivosConvenio As clsArchivosConvenios) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsArchivosConveniosDO).Create.Insert(objArchivosConvenio)
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

    Public Function Seleccionar(ByVal objArchivosConvenio As clsArchivosConvenios, ByVal iStartRowIndex As Integer, ByVal iMaxRows As Integer, ByRef iTotalRows As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsArchivosConveniosDO).Create.Seleccionar(objArchivosConvenio, iStartRowIndex, iMaxRows, iTotalRows)
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
