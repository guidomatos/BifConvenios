Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Namespace BIFConvenios

    Public Class TipoDocumento

        Public Shared Function GetTipoDocumento() As SqlDataReader
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Return result
        End Function
    End Class
End Namespace
