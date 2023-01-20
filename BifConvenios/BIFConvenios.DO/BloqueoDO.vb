Imports BIFUtils
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Data
Imports System.Data.Common
Imports System.Data.OleDb
Imports System.Data.SqlClient

Public Class BloqueoDO
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
    Public Function ActualizaClienteCuotaBloqueo(ByVal pNumeroLote As String, ByVal pDLNP As String, ByVal pESTADO As Boolean) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC UpdateClienteCuota ", pNumeroLote, ",", pDLNP, ",", Conversions.ToString(pESTADO)}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function

    Public Function ActualizaInformacionBloqueosCuotas(ByVal pNumeroLote As String) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim command As New SqlCommand(Conversions.ToString(("EXEC ActualizaInformacionBloqueosCuotas " & pNumeroLote & "")), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function
    Public Function ActualizaLoteBloqueo(ByVal pNumeroLote As String, ByVal pRespuesta As String) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC UpdateLoteBloqueo ", pNumeroLote, ",'", pRespuesta, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function
    Public Function ObtieneInformacionBloqueoDeIBS(ByVal pNumeroLote As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT EDLNPGR, EDLFLG1 ", " FROM edl6376w W, cltsexc ")
        lstrsql = String.Concat(lstrsql, " WHERE     (EDLLOTE = ", pNumeroLote, ") ")
        lstrsql = String.Concat(lstrsql, " AND (wnrpg = EDLNPGR) ")
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ProcesaBloqueoEnIBS(ByVal pNumeroLote As String) As String
        Return TCPClient.SendReceive(ConfigurationManager.AppSettings("ipJavaApp").Trim, ConfigurationManager.AppSettings("portJavaApp").Trim, ("Bloqueo:" & pNumeroLote))
    End Function
End Class
