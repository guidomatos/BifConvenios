Imports System.Data
Imports System.Reflection
Imports System.Data.SqlClient
Imports BIFData.GOIntranet
Imports ADODB
Imports System.Data.OleDb

Namespace BIFConvenios

    Public Class Cuota


        Dim connexion As String
        Public Sub cuota()
            ''connexion = ConfigurationManager.AppSettings["AS400_ConnectionString_Convenios"].Trim();   
            ''myConnection.Open(BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Generales"))
            'connexion = ConfigurationManager.AppSettings("AS400_ConnectionString_Convenios").Trim()
            connexion = BIFUtils.WS.Utils.CadenaConexion("AS400_ConnectionString_Convenios")
        End Sub



        Public Function GetTipoCuota() As DataTable

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim oDS As New DataSet()
            Dim oAdapter As New SqlDataAdapter()

            'Dim returnValue As Integer
            'Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand((myConnection, _
            '    CType(MethodBase.GetCurrentMethod(), MethodInfo), _
            '    New Object() {Nombre_Cliente})

            Dim myCommand As SqlCommand = SqlCommandGenerator.GenerateCommand((myConnection), _
                CType(MethodBase.GetCurrentMethod(), MethodInfo), _
                New Object() {})


            oAdapter.SelectCommand = myCommand
            myConnection.Open()
            oAdapter.Fill(oDS, "Cuota")
            myConnection.Close()
            Return oDS.Tables(0)
        End Function


        'Public Sub Actualizar_datos_DLEMP(ByVal Cliente_ID As String, ByVal anio As String, ByVal mes As String)

        '    Dim conn As ADODB.Connection = New Connection()
        '    Dim result As ADODB.Recordset = New Recordset()

        '    conn.CursorLocation = CursorLocationEnum.adUseClient
        '    conn.Open(connexion, "", "", -1)

        '    Dim str As String = " UPDATE  BIFCYFILES.DLEMP SET DLEAEN = " & anio.Trim() & ", DLEMEN = " & mes.Trim() & " WHERE  DLECUN =" & Cliente_ID.Trim()


        '    Dim gg As Object = Nothing
        '    result = conn.Execute(str, gg, 0)

        'End Sub


        Public Function Actualizar_CUOTA(ByVal Cliente_ID As Integer, ByVal anio As Integer, ByVal mes As Integer, ByVal tipo_cuota As Integer) As Boolean

            Dim myConnection As New SqlConnection(GetDBConnectionString)
            Dim result As Boolean = False

            Try

                myConnection.Open()

                Dim myCommand As New SqlCommand()
                myCommand.CommandType = CommandType.StoredProcedure
                myCommand.Connection = myConnection
                myCommand.CommandText = "ACTUALIZAR_CUOTA"

                myCommand.Parameters.Add("@CLIENTE_ID", SqlDbType.Int)
                myCommand.Parameters.Add("@ANIO", SqlDbType.Int)
                myCommand.Parameters.Add("@MES", SqlDbType.Int)
                myCommand.Parameters.Add("@TIPO_CUOTA", SqlDbType.Int)

                myCommand.Parameters("@CLIENTE_ID").Value = Cliente_ID
                myCommand.Parameters("@ANIO").Value = anio
                myCommand.Parameters("@MES").Value = mes
                myCommand.Parameters("@TIPO_CUOTA").Value = tipo_cuota

                myCommand.ExecuteNonQuery()

                result = True

            Catch ex As Exception

                result = False
                myConnection.Close()
                myConnection = Nothing

            End Try

            Return result

        End Function

    End Class
End Namespace

