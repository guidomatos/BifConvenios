Imports BIFUtils
Imports Microsoft.VisualBasic.CompilerServices
Imports System
Imports System.Configuration
Imports System.Data.SqlClient
Public Class FormatoArchivoClienteDO
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
    Public Function ObtieneNombreFormatoArchivo(ByVal pCodigo_proceso As String) As String
        Dim connection As New SqlConnection(Me.conexionConvenios)
        Dim command As New SqlCommand(("EXEC GetNombreFormatoArchivo '" & pCodigo_proceso & "'"), connection)
        Dim str As String = ""
        connection.Open()
        str = Conversions.ToString(command.ExecuteScalar)
        connection.Close()
        Return str
    End Function
End Class
