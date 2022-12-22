Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsAccesoDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function GetBuscarPerfilUsuario(ByVal pstridUsuario As String) As String
        Dim str As String
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[BUSCAR_PERFIL_USUARIO]")
            dasql.AddParameter(command, "ID_USUARIO", pstridUsuario, SqlDbType.VarChar)
            pstridUsuario = Conversions.ToString(command.ExecuteScalar)
            dasql.ConnectionClose()
            dasql = Nothing
            str = pstridUsuario
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
        Return str
    End Function
End Class
