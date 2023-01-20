Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports System.Reflection
Imports ADODB

Namespace BIFConvenios

    Public Class Reportes

        Public Function EfectividadRecaudacion(ByVal codigo_ibs As String, ByVal anio As String, ByVal mes As String) As SqlDataAdapter
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_ibs, anio, mes})

            myConnection.Open()
            'Dim result As SqlDataReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            Dim result As SqlDataAdapter = New SqlDataAdapter(myCommand)
            Return result
        End Function


    End Class


End Namespace