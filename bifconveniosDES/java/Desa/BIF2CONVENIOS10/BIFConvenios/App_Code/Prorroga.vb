Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports ADODB

Namespace BIFConvenios

    Public Class Prorroga
        'Establece si el lote de Prorroga ha sido procesado 
        Public Shared Function isLoteProrrogaProcesado(ByVal numeroLote As String) As Boolean
            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {numeroLote})

            myConnection.Open()
            returnValue = CType(myCommand.ExecuteScalar, Boolean)
            myConnection.Close()
            Return returnValue
        End Function


#Region "Operaciones en AS/400"

        'Adicionar la informacion de los pagares a bloquear antes de procesarlo
        'RESPINOZA 20070621 - SE ADICIONA LA INFORMACION DE PERIODO AL PROCESO 
        Public Shared Function addPagare(ByVal myConnection As ADODB.Connection, ByVal numeroLote As String, _
                                            ByVal numeroPagare As String, _
                                            ByVal fechaProceso As String, ByVal horaProceso As String, _
                                            ByVal usuario As String, ByVal anioPeriodo As String, ByVal mesPeriodo As String) As Integer ', ByVal flagProrroga As String

            Dim strQuery As String
            ', EDLFLAG) " + _
            strQuery = "INSERT INTO EDL6378W (EDLLOTE, EDLNPGR, EDLFECR, EDLHORA, EDLUSER, EDLAPER, EDLMPER)" + _
                                 "      VALUES ( '" + numeroLote + "', '" + numeroPagare + "', '" + fechaProceso + "', '" + horaProceso + "', '" + usuario.Substring(0, 10).ToUpper + "','" + anioPeriodo + "','" + mesPeriodo + "')"
            ', '" + flagProrroga + "' )"

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
        'obtener un c?digo de Prorroga para procesar los demas lotes
        Protected Function creaLoteProrroga(ByVal Codigo_proceso As String, ByVal userId As String) As Integer
            Dim returnValue As Integer
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, userId})

            myCommand.Transaction = transaction

            returnValue = CType(myCommand.ExecuteScalar(), Integer)
            Return returnValue
        End Function

        'adiciona un Prorroga al grupo
        Protected Function addProrroga(ByVal Codigo_proceso As String, ByVal codigoLote As Integer, ByVal dlnp As String) As Boolean
            Dim returnValue As Boolean
            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand(myConnection, _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {Codigo_proceso, codigoLote, dlnp})

            myCommand.Transaction = transaction

            returnValue = myCommand.ExecuteScalar()
            Return returnValue
        End Function

        'Obtener informacion del periodo de proceso 
        Protected Sub getPeriodoProceso(ByVal codigo_proceso As String, ByRef anioPeriodo As String, ByRef mesPeriodo As String)
            Dim dr As SqlDataReader
            anioPeriodo = ""
            mesPeriodo = ""

            dr = (New Proceso()).InformeProceso(codigo_proceso)

            If dr.Read Then
                anioPeriodo = dr("AnioPeriodo2S")
                mesPeriodo = dr("MesPeriodo2S")
            End If
        End Sub

#End Region

#Region "Proceso de la operaci?n de Prorroga"
        'procesamos la informacion de los Prorrogas
        Public Shared Function procesaProrroga(ByVal codigo_proceso As String, ByVal pagares As String, ByVal userName As String) As Integer
            Dim b As New BIFConvenios.Prorroga()
            Dim loteProrroga As Integer

            Dim vpagares As String() = pagares.Split(",")

            Dim index As Integer = 1
            'Dim vProrrogas As String()

            Dim pagare As String
            Dim Prorroga As Boolean
            Dim Prorrogas As New System.Collections.Hashtable()


            Dim anioPeriodo As String = ""
            Dim mesPeriodo As String = ""

            b.getPeriodoProceso(codigo_proceso, anioPeriodo, mesPeriodo)

            'Procesamos y obtenemos los estados de los Prorrogas de la base de datos SQL
            Try
                b.myConnection.Open()


                b.transaction = b.myConnection.BeginTransaction()

                loteProrroga = b.creaLoteProrroga(codigo_proceso, userName)

                'Recuperamos la informacion de pagares para procesarlos individualmente 
                For Each pagare In vpagares
                    Prorroga = b.addProrroga(codigo_proceso, loteProrroga, pagare)
                    'Prorrogas.Add(pagare, IIf(Prorroga, "B", ""))
                Next

                'No hacer el commit en SQL hasta que la transaccion en AS/400 este completa
                'b.transaction.Commit()
            Catch e As Exception
                b.transaction.Rollback()
                Throw e         ' Si ocurre un error cancelamos la operacion y salimos 
            End Try

            '------------- En este punto empezamos a procesar la informaci?n para AS/400
            Dim myConnectionAS400 As ADODB.Connection
            myConnectionAS400 = New ADODB.Connection()

            Dim s As String() = pagare.Split(",")
            Try
                'myConnectionAS400.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
                myConnectionAS400.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
                myConnectionAS400.BeginTrans()

                For Each pagare In vpagares
                    addPagare(myConnectionAS400, loteProrroga, pagare, Format(Now, "ddMMyy"), Format(Now, "hhmmss"), userName, anioPeriodo, mesPeriodo)
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
            'Remoting.sendMessage("ProcesoProrroga", CStr(loteProrroga))

            Dim objWSConvenios As New wsConvenios.WSBIFConvenios

            objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
            objWSConvenios.ProcesaProrroga(CStr(loteProrroga), userName)

            Return loteProrroga
        End Function
#End Region

    End Class
End Namespace
