
Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Reflection

Public Class clsParametroDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    Public Function ObtenerParametro(ByVal pIdGrupo As Integer, ByVal pIdParametro As Integer) As clsParametro
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Dim parametro As New clsParametro
        Try
            dasql.CommandProperties(command, "[dbo].[ObtenerParametros]")
            dasql.AddParameter(command, "@GrupoId", pIdGrupo, SqlDbType.Int)
            dasql.AddParameter(command, "@ParametroId", pIdParametro, SqlDbType.Int)

            Dim table As New DataTable
            Dim reader As DataTableReader

            table = dasql.ExecuteReader(command)
            reader = table.CreateDataReader()

            If (reader.Read) Then
                parametro = New clsParametro
                parametro.GrupoId = reader.GetInt32(reader.GetOrdinal("iGrupoId"))
                parametro.ParametroId = reader.GetInt32(reader.GetOrdinal("iParametroId"))
                parametro.Descripcion = reader.GetString(reader.GetOrdinal("vDescripcion"))
                parametro.Valor = reader.GetString(reader.GetOrdinal("vValor"))
                parametro.Orden = reader.GetInt32(reader.GetOrdinal("iOrden"))
                parametro.Visible = reader.GetInt32(reader.GetOrdinal("iVisible"))
            End If

            dasql.ConnectionClose()
            dasql = Nothing

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
        Return parametro
    End Function
End Class
