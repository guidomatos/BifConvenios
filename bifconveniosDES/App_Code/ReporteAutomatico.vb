Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet
Imports ADODB
Imports BIFConvenios.Container

Namespace BIFConvenios

    Public Class ReporteAutomatico

        Public Function ReporteNominaAutomaticaCabecera(ByVal idFuncionario As Integer) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idFuncionario})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reporte")
            myConnection.Close()
            Return oDS
        End Function

        Public Function ReporteNominaAutomaticaDetalle(ByVal idFuncionario As Integer) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idFuncionario})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reporte")
            myConnection.Close()
            Return oDS
        End Function

        Public Function ReporteNominaAutomaticaCabeceraObservada(ByVal idFuncionario As Integer) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idFuncionario})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reporte")
            myConnection.Close()
            Return oDS
        End Function

        Public Function ReporteNominaAutomaticaDetalleObservada(ByVal idFuncionario As Integer) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idFuncionario})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reporte")
            myConnection.Close()
            Return oDS
        End Function

        Public Function ValidaExistenciaFuncionario(ByVal idFuncionario As Integer) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            ' Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {idFuncionario})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Reporte")
            myConnection.Close()
            Return oDS
        End Function


    End Class

End Namespace




