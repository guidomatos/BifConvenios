Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports ADODB

Namespace BIFConvenios

    Public Class Bloqueo

        'Establece si el lote de bloqueo ha sido procesado 
        Public Shared Function isLoteBloqueoProcesado(ByVal numeroLote As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection,
                CType(MethodBase.GetCurrentMethod(), MethodInfo),
                New Object() {numeroLote})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, Boolean)
            myConnection.Close()
            Return returnValue
        End Function


#Region "Operaciones en AS/400"
        'Adicionar la informacion de los pagares a bloquear antes de procesarlo
        Public Shared Function addPagare(ByVal myConnection As Connection, ByVal numeroLote As String,
                                            ByVal numeroPagare As String,
                                            ByVal fechaProceso As String, ByVal horaProceso As String,
                                            ByVal usuario As String, ByVal flagBloqueo As String) As Integer

            Dim strQuery As String
            strQuery = "INSERT INTO EDL6376W (EDLLOTE, EDLNPGR, EDLFECR, EDLHORA, EDLUSER, EDLFLAG) " +
                                 "    VALUES ( '" + numeroLote + "', '" + numeroPagare + "', '" + fechaProceso + "', '" + horaProceso + "', '" + usuario.Substring(0, 10).ToUpper + "', '" + flagBloqueo + "' )"

            Dim cmd As New ADODB.CommandClass()
            cmd.ActiveConnection = myConnection
            cmd.CommandText = strQuery
            cmd.Prepared = True

            Dim ra As Object
            cmd.Execute(ra, System.Reflection.Missing.Value, ADODB.CommandTypeEnum.adCmdText + ADODB.ExecuteOptionEnum.adExecuteNoRecords)
        End Function


        'Public Shared Function adicionaPagares(ByVal lote As String, ByVal pagare As String, ByVal userName As String)
        '    Dim myConnection As New ADODB.Connection()

        '    Dim s As String() = pagare.Split(",")
        '    Try
        '        myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
        '        myConnection.BeginTrans()

        '        Dim str As String
        '        For Each str In s

        '            addPagare(myConnection, lote, str, Format(Now, "ddMMyy"), Format(Now, "hhmmss"), userName, "B")
        '        Next
        '    Catch e As Exception
        '        Throw e
        '        myConnection.RollbackTrans()
        '    Finally
        '        myConnection.CommitTrans()
        '    End Try
        'End Function




#End Region

#Region "Operaciones en SQL Server"
        Dim myConnection As New SqlConnection(GetDBConnectionString)
        Dim transaction As SqlTransaction
        'obtener un código de bloqueo para procesar los demas lotes
        Protected Function creaLoteBloqueo(ByVal Codigo_proceso As String, ByVal userId As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection,
                CType(MethodBase.GetCurrentMethod(), MethodInfo),
                New Object() {Codigo_proceso, userId})

            myCommand.Transaction = transaction

            returnValue = CType(myCommand.ExecuteScalar(), Integer)
            Return returnValue
        End Function

        'adiciona un bloqueo al grupo
        Protected Function addBloqueo(ByVal Codigo_proceso As String, ByVal codigoLote As Integer, ByVal dlnp As String) As Boolean
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection,
                CType(MethodBase.GetCurrentMethod(), MethodInfo),
                New Object() {Codigo_proceso, codigoLote, dlnp})

            myCommand.Transaction = transaction

            returnValue = myCommand.ExecuteScalar()
            Return returnValue
        End Function
#End Region

#Region "Proceso de la operación de bloqueo"
        'procesamos la informacion de los bloqueos
        Public Shared Function procesaBloqueo(ByVal codigo_proceso As String, ByVal pagares As String, ByVal userName As String) As Integer
            Dim b As New BIFConvenios.Bloqueo()
            Dim loteBloqueo As Integer

            Dim vpagares As String() = pagares.Split(",")

            Dim index As Integer = 1
            'Dim vbloqueos As String()

            Dim pagare As String = ""
            Dim bloqueo As Boolean
            Dim bloqueos As New System.Collections.Hashtable()

            'Procesamos y obtenemos los estados de los bloqueos de la base de datos SQL
            Try
                b.myConnection.Open()
                b.transaction = b.myConnection.BeginTransaction()

                loteBloqueo = b.creaLoteBloqueo(codigo_proceso, userName)

                'Recuperamos la informacion de pagares para procesarlos individualmente 
                For Each pagare In vpagares
                    bloqueo = b.addBloqueo(codigo_proceso, loteBloqueo, pagare)
                    bloqueos.Add(pagare, IIf(bloqueo, "B", ""))
                Next

                'No hacer el commit en SQL hasta que la transaccion en AS/400 este completa
                'b.transaction.Commit()
            Catch e As Exception
                b.transaction.Rollback()
                Throw e         ' Si ocurre un error cancelamos la operacion y salimos 
            End Try

            '------------- En este punto empezamos a procesar la información para AS/400
            Dim myConnectionAS400 As New ADODB.Connection()

            Dim s As String() = pagare.Split(",")
            Try
                'myConnectionAS400.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))               
                myConnectionAS400.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
                myConnectionAS400.BeginTrans()

                For Each pagare In vpagares
                    addPagare(myConnectionAS400, loteBloqueo, pagare, Format(Now, "ddMMyy"), Format(Now, "hhmmss"), userName, bloqueos.Item(pagare))      'vbloqueos.GetValue(index))
                Next

                myConnectionAS400.CommitTrans()
                b.transaction.Commit() 'Procesamos la transaccion en SQL 
            Catch e As Exception
                myConnectionAS400.RollbackTrans()
                b.transaction.Rollback() 'Hacemos un rollback cuando hay error en la transaccion en AS/400
                Throw e
            Finally
                myConnectionAS400.Close()
                b.myConnection.Close()
            End Try

            '20121016: AHSP(BANBIF) - Hacemos que vaya al Remoting
            ''Aqui enviamos el mensaje al motor .net para que procese la aplicacion 
            'Remoting.sendMessage("ProcesoBloqueo", CStr(loteBloqueo))

            Dim objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient
            objWSConvenios.ProcesaBloqueo(loteBloqueo.ToString, userName)

            Return loteBloqueo

            'Return loteBloqueo
        End Function
#End Region

    End Class
End Namespace
