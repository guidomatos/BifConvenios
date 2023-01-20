Imports BIFUtils
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class ClienteDO
    ' Fields
    Private cUtils As WS.Utils = New WS.Utils
    Private conexionIBS As String
    Private conexionConvenios As String

    Public Sub New()
        MyBase.New()
        Me.cUtils = New WS.Utils()
        Me.conexionIBS = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
        Me.conexionConvenios = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    End Sub
    Public Function ExisteCodigoIBS(ByVal codibs As Integer) As Boolean
        Dim flag As Boolean
        Dim connection As New SqlConnection(Me.conexionConvenios)
        connection.Open()
        Dim objA As New SqlCommand("EXEC VALIDA_NO_EXISTA", connection)
        Try
            objA.CommandType = CommandType.StoredProcedure
            objA.Parameters.Add("@codibs", SqlDbType.Int).Value = codibs
            Return Conversions.ToBoolean(objA.ExecuteScalar)
        Finally
            If Not Object.ReferenceEquals(objA, Nothing) Then
                objA.Dispose()
            End If
        End Try
        connection.Close()
        Return flag
    End Function

    Public Function ObtenerCodigoClienteIBS(ByVal pTipoDocumento As String, ByVal pNumeroDocumento As String) As String
        Dim connection As New OleDbConnection(Me.conexionIBS)
        Dim str As String = ""
        'New OleDbCommand.CommandType = CommandType.Text
        Dim strArray As String() = New String() {"SELECT CUSCUN FROM CUMST WHERE CUSTID  ='", pTipoDocumento.Trim, "' AND CUSIDN = '", pNumeroDocumento.Trim, "'"}
        Dim obj2 As Object = String.Concat(strArray)
        connection.Open()
        str = Conversions.ToString(New OleDbCommand(Conversions.ToString(obj2), connection).ExecuteScalar)
        connection.Close()
        Return str
    End Function
End Class
