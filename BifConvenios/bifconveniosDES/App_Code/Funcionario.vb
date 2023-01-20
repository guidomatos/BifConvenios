Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB


Namespace BIFConvenios

    Public Class Funcionario


        Public Function GetFuncionarios() As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reportes")
            myConnection.Close()
            Return oDS
        End Function






    End Class



End Namespace


