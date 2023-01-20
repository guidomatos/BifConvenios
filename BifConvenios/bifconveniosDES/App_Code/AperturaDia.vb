Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports ADODB

Namespace BIFConvenios

    Public Class AperturaDia

        'Establece si el lote de bloqueo ha sido procesado 
        Public Shared Function ValidarFinProcesoBatch(ByVal codigo_proceso As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {codigo_proceso})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, Integer)
            myConnection.Close()
            Return returnValue
        End Function

        'Obtiene la fecha de la ultima actualizacion del proceso batch 
        Public Shared Function ObtenerUltimaActualizacionProcesoBatch() As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As String
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, String)
            myConnection.Close()
            Return returnValue
        End Function

    End Class
End Namespace