Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsEventoSistemaDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function Insertar(ByVal pobjEventoSistema As clsEventoSistema) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[EventoSistemaInsert]")
            dasql.AddParameter(command, "@vHilo", pobjEventoSistema.Hilo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vNivel", pobjEventoSistema.Nivel, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vAccion", pobjEventoSistema.Accion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vMensaje", pobjEventoSistema.Mensaje, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vExcepcion", pobjEventoSistema.Excepcion, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vUsuario", pobjEventoSistema.Usuario, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            num = 1
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return num
    End Function

End Class
