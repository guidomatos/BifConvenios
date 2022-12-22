Imports ADODB
Imports System.Data.SqlClient
Imports System.Reflection
Imports BIFData.GOIntranet

Public Class Prorroga

#Region "Operaciones en AS/400"

    'Obtener la informacion de los Prorrogas que han sido realizados despues de procesar la operacion
    Public Shared Function getInformacionProrroga(ByVal numeroLote As String) As DataSet
        Dim str As String
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim oDS As New DataSet()
        Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))


        '        str = "SELECT EDLNPGR, CASE (SELECT COUNT(1) FROM dlmvto v " + _
        '                " WHERE w.EDLNPGR = v.DLMACC AND w.edlfecr = v.dlmfcp) WHEN 0 THEN 'P' ELSE '' END AS WFLG1 " + _
        '               " FROM EDL6378W w " + _
        '              " WHERE     edllote = " & numeroLote

        str = "SELECT EDLNPGR, CASE WHEN trim(edflagp) = '' OR trim(edflagp) = '3' THEN '' ELSE 'P' END AS WFLG1, EDFLAGP " + _
                     " FROM EDL6378W w " + _
                     " WHERE edllote = " & numeroLote

        result = myConnection.Execute(str)
        result.ActiveConnection = Nothing
        myConnection.Close()
        myConnection = Nothing
        daTransform.Fill(oDS, result, "Result")

        Return oDS
    End Function
#End Region

#Region "Operaciones en SQL Server"
    Dim myConnection As New SqlConnection(GetDBConnectionString)
    Dim transaction As SqlTransaction

    Protected Function UpdateLoteProrroga(ByVal numeroLote As String, ByVal respuesta As String) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote, respuesta})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function
    'Funcion para procesar el final de la prorroga en error de conexion
    Public Shared Function UpdateLoteProrroga2(ByVal numeroLote As String, ByVal respuesta As String) As Integer
        Dim returnValue As Integer
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote, respuesta})

        myConnection.Open()
        returnValue = myCommand.ExecuteNonQuery()
        myConnection.Close()
        Return returnValue
    End Function

    Protected Function ActualizaInformacionProrrogasCuotas(ByVal numeroLote As String) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function


    Protected Function UpdateClienteCuotaProrroga(ByVal numeroLote As String, ByVal DLNP As String, ByVal ESTADO As Boolean, ByVal EDFLAGP As String) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote, DLNP, ESTADO, EDFLAGP})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function

    'Obtenemos la informacion del Prorroga desde AS/400 y procesamos la informacion para actualizar el aplicativo
    Public Shared Function procesaConciliacionProrroga(ByVal numeroLote As String, ByVal returnMessage As String) As Boolean
        Dim ds As DataSet = Prorroga.getInformacionProrroga(numeroLote)
        Dim b As New Prorroga()

        Try

            b.myConnection.Open()
            b.transaction = b.myConnection.BeginTransaction(IsolationLevel.ReadUncommitted)

            b.UpdateLoteProrroga(numeroLote, returnMessage)
            Dim dt As DataTable = ds.Tables("Result")
            Dim dr As DataRow

            For Each dr In dt.Rows
                b.UpdateClienteCuotaProrroga(numeroLote, dr("EDLNPGR"), IIf(returnMessage.Trim() = "" Or returnMessage.Trim() = "error", False, (dr("WFLG1").ToString.Trim = "")), dr("EDFLAGP").ToString.Trim)
            Next

            b.ActualizaInformacionProrrogasCuotas(numeroLote)
            b.transaction.Commit() 'Procesamos la transaccion en SQL 
        Catch e As Exception
            b.transaction.Rollback() 'Hacemos un rollback cuando hay error en la transaccion en AS/400
            Throw e
        Finally
            b.myConnection.Close()
        End Try
    End Function

#End Region
End Class
