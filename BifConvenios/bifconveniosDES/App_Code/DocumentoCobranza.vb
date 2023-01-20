Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet

Namespace BIFConvenios

    Public Class DocumentoCobranza
        Protected myConnection As SqlConnection
        'Adicionamos la informacion del documento generado
        Protected Function addLogDocumentoGenerado(ByVal Codigo_proceso As String, ByVal DLCC As String, _
                ByVal fecha As String, ByVal TipoDocumento As String, ByVal UserId As String, ByVal amount As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, DLCC, fecha, TipoDocumento, UserId, amount})
            returnValue = myCommand.ExecuteNonQuery
            Return returnValue
        End Function

        'Obtener informacion acerca de las firmas del documento 
        Public Shared Function getClienteProceso(ByVal proceso As String) As String
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {proceso})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "clienteProceso")
            myConnection.Close()
            If oDS.Tables(0).Rows.Count > 0 Then
                Return oDS.Tables(0).Rows(0).Item("Codigo_Cliente")
            Else
                Return ""
            End If

        End Function

        'Obtener informacion acerca de las firmas del documento 
        Public Shared Function getInformacionFirmantes(ByVal UserId As String, ByVal proceso As String) As DataSet
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {UserId, proceso})

            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "firma")
            myConnection.Close()
            Return oDS
        End Function

        'Procesa la informacion de los documentos
        Public Shared Function procesaLogDocumentosPagares(ByVal Codigo_proceso As String, ByVal pagares As String, _
                ByVal fecha As String, ByVal TipoDocumento As String, ByVal UserId As String, ByVal amounts As String) As Integer
            Dim d As New DocumentoCobranza()
            d.myConnection = New SqlConnection(GetDBConnectionString)
            d.myConnection.Open()

            Dim s As String()
            Dim str As String
            Dim i As Integer = 0
            Dim data, search, strFinded, amount As String

            s = pagares.Replace("'", "").Split(",")

            For Each str In s
                data = amounts
                search = str
                strFinded = Mid(data, InStr(data, search))
                amount = Mid(strFinded, InStr(strFinded, "=") + 1, InStr(strFinded, ",") - InStr(strFinded, "=") - 1)
                i = i + d.addLogDocumentoGenerado(Codigo_proceso, str, fecha, TipoDocumento, UserId, amount)
            Next
            d.myConnection.Close()
        End Function


    End Class
End Namespace
