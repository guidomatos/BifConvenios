Imports ADODB
Imports System.Data.SqlClient
Imports System.Reflection
Imports BroadcasterClass.GOIntranet
Imports BIFData.GOIntranet

Public Class Bloqueo

#Region "Operaciones en AS/400"

    'Obtener la informacion de los bloqueos que han sido realizados despues de procesar la operacion
    Public Shared Function getInformacionBloqueo(ByVal numeroLote As String) As DataSet
        Dim str As String
        Dim myConnection As New ADODB.Connection()
        Dim result As New ADODB.Recordset()
        Dim oDS As New DataSet()
        Dim daTransform As New System.Data.OleDb.OleDbDataAdapter()


        myConnection.CursorLocation = CursorLocationEnum.adUseClient
        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios"))

        str = "SELECT EDLNPGR, EDLFLG1 "
        str = str + " FROM edl6376w W, cltsexc "
        str = str + " WHERE     (EDLLOTE = " & numeroLote & ") "
        str = str + " AND (wnrpg = EDLNPGR) " 'AND WPRCD = DAY(CURRENT DATE) AND WPRCM = MONTH(CURRENT DATE) AND "
        'str = str + " WPRCY = (YEAR(CURRENT DATE) - 2000) "

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

    Protected Function UpdateLoteBloqueo(ByVal numeroLote As String, ByVal respuesta As String) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote, respuesta})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function

    Public Shared Function UpdateLoteBloqueo2(ByVal numeroLote As String, ByVal respuesta As String) As Integer
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

    Protected Function UpdateClienteCuota(ByVal numeroLote As String, ByVal DLNP As String, ByVal ESTADO As Boolean) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote, DLNP, ESTADO})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function

    Protected Function ActualizaInformacionBloqueosCuotas(ByVal numeroLote As String) As Integer
        Dim returnValue As Integer
        Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
            CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            New Object() {numeroLote})

        myCommand.Transaction = transaction

        returnValue = CType(myCommand.ExecuteScalar(), Integer)
        Return returnValue
    End Function

    'Obtenemos la informacion del bloqueo desde AS/400 y procesamos la informacion para actualizar el aplicativo
    Public Shared Function procesaConciliacionBloqueo(ByVal numeroLote As String, ByVal returnMessage As String) As Boolean
        Dim ds As DataSet = Bloqueo.getInformacionBloqueo(numeroLote)
        Dim b As New Bloqueo()

        Try

            b.myConnection.Open()

            b.transaction = b.myConnection.BeginTransaction(IsolationLevel.ReadUncommitted)


            b.UpdateLoteBloqueo(numeroLote, returnMessage)
            Dim dt As DataTable = ds.Tables("Result")
            Dim dr As DataRow

            For Each dr In dt.Rows
                b.UpdateClienteCuota(numeroLote, dr("EDLNPGR"), IIf(returnMessage.Trim() = "" Or returnMessage.Trim() = "error", False, (dr("EDLFLG1").ToString.Trim = "")))
            Next

            b.ActualizaInformacionBloqueosCuotas(numeroLote)
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
