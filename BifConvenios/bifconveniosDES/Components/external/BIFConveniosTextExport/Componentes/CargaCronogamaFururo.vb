Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet

Public Class CargaCronogamaFururo
    'Adiciona los datos del proceso
    Public Function EnviaEspacioTrabajo(ByVal Codigo_proceso As String, _
                                        ByVal usuario As String) As String
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, usuario})

        myConnection.Open()
        returnValue = CType(myCommand.ExecuteScalar(), String)
        myConnection.Close()
        Return returnValue
    End Function

    'Establecemos el error del proceso
    Public Sub EstableceErrorProceso(ByVal Codigo_proceso As String, ByVal usuario As String)
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, usuario})

        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub

    'Actualizamos el estado de proceso 
    Public Sub UpdateEstadoProceso(ByVal Codigo_proceso As String, ByVal Estado As String, ByVal usuario As String)
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {Codigo_proceso, Estado, usuario})

        myConnection.Open()
        myCommand.ExecuteNonQuery()
        myConnection.Close()
    End Sub
End Class
