Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet

Namespace BIFConvenios

    Public Class Usuario
        Protected myConnection As SqlConnection

        Public Shared Function spObtenerCorreoUsuario(ByVal UserName As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserName})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Usuario")
            myConnection.Close()
            If oDS.Tables(0).Rows.Count > 0 Then
                Return oDS.Tables(0).Rows(0).Item("CorreoElectronico")
            Else
                Return ""
            End If
        End Function
    End Class
End Namespace
