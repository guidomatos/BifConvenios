Imports BIFUtils
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient
Public Class ProrrogaDO
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

    Public Function ActualizaClienteCuotaProrroga(ByVal pNumeroLote As String, ByVal pDLNP As String, ByVal pESTADO As Boolean, ByVal EDFLAGP As String) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC UpdateClienteCuotaProrroga ", pNumeroLote, ",", pDLNP, ",", Conversions.ToString(pESTADO), ",'", EDFLAGP, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function

    Public Function ActualizaInformacionProrrogasCuotas(ByVal pNumeroLote As String) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim command As New SqlCommand(Conversions.ToString(("EXEC ActualizaInformacionProrrogasCuotas " & pNumeroLote & "")), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function

    Public Function ActualizaLoteProrroga(ByVal pNumeroLote As String, ByVal pRespuesta As String) As Integer
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim strArray As String() = New String() {"EXEC UpdateLoteProrroga ", pNumeroLote, ",'", pRespuesta, "'"}
        Dim command As New SqlCommand(Conversions.ToString(String.Concat(strArray)), connection)
        command.CommandType = CommandType.Text
        connection.Open()
        Dim num2 As Integer = Conversions.ToInteger(command.ExecuteScalar)
        connection.Close()
        Return num2
    End Function

    Public Function ObtieneInformacionProrrogaDeIBS(ByVal pNumeroLote As String) As DataSet
        Dim lds As DataSet = New DataSet()
        Dim lstrsql As String = ""
        lstrsql = String.Concat("SELECT EDLNPGR, CASE WHEN trim(edflagp) = '' OR trim(edflagp) = '3' THEN '' ELSE 'P' END AS WFLG1, EDFLAGP  FROM EDL6378W w  WHERE edllote = ", pNumeroLote)
        Dim num As Integer = (New OleDbDataAdapter(lstrsql, Me.conexionIBS)).Fill(lds)
        Return lds
    End Function

    Public Function ProcesaProrrogaEnIBS(ByVal pNumeroLote As String) As String
        Return TCPClient.SendReceive(ConfigurationManager.AppSettings("ipJavaApp").Trim, ConfigurationManager.AppSettings("portJavaApp").Trim, ("Prorroga:" & pNumeroLote))
    End Function
End Class
