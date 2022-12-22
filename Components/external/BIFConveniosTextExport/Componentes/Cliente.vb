Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet

Public Class Cliente

    'Obtenemos la informacion del formato de salida del archivo
    Public Shared Function getInformacionFormatoSalida(ByVal codigoProceso As String) As String
        Dim myConnection As New SqlConnection(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))
        Dim returnValue As String
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {codigoProceso})

        myConnection.Open()
        returnValue = CType(myCommand.ExecuteScalar(), String)
        myConnection.Close()
        Return returnValue
    End Function

End Class
