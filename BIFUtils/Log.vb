Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Configuration
Imports Microsoft.VisualBasic.ApplicationServices

Public Class Log

    Dim objUtil As New WS.Utils
    Dim strWSCadenaConexion As String = BIFUtils.WS.Utils.CadenaConexion("ConnectionString")
    Public Enum Level
        Debug = 0
        Info = 1
        Warn = 2
        Errores = 3
        Fatal = 4
    End Enum

    Public Sub GrabarLog(ByVal pNivel As Level, ByVal pAccion As String, ByVal pMensaje As String, ByVal pExcepcion As String, ByVal pUsuario As String)
        'Dim conexionConvenios As String = ConfigurationManager.ConnectionStrings("BIFConveniosSQL").ConnectionString
        Dim cUtils As New WS.Utils
        'Dim conexionConvenios As String = cUtils.DescifrarCadenaConexion(ConfigurationManager.AppSettings("ConnectionString").ToString())
        Dim lconn As New SqlConnection(strWSCadenaConexion)
        Dim lcmd As SqlCommand
        Dim lUser As New User

        Try

            If pUsuario.Trim = "" Then
                pUsuario = lUser.Name
            End If

            Dim lstrsql As String = "INSERT INTO EventoSistema (Fecha,Hilo,Nivel,Accion,Mensaje,Excepcion, Usuario)" _
                                        & " VALUES ( GETDATE(), @Hilo, @Nivel, @Accion, @Mensaje, @Excepcion, @Usuario)"

            lcmd = New SqlCommand(lstrsql, lconn)
            lcmd.CommandType = CommandType.Text

            AgregarParametro(lcmd, "@Hilo", ParameterDirection.Input, DbType.String, "BIFConvenios")
            AgregarParametro(lcmd, "@Nivel", ParameterDirection.Input, DbType.String, pNivel.ToString)
            AgregarParametro(lcmd, "@Accion", ParameterDirection.Input, DbType.String, pAccion)
            AgregarParametro(lcmd, "@Mensaje", ParameterDirection.Input, DbType.String, Left(pMensaje, 8000))
            AgregarParametro(lcmd, "@Excepcion", ParameterDirection.Input, DbType.String, Left(pExcepcion, 4000))
            AgregarParametro(lcmd, "@Usuario", ParameterDirection.Input, DbType.String, pUsuario)

            lconn.Open()
            lcmd.ExecuteNonQuery()
        Catch ex As Exception
            'No se pudo grabar
        Finally
            If (lconn.State = ConnectionState.Open) Then
                lconn.Close()
            End If
            lconn.Dispose()
            lconn = Nothing
        End Try

    End Sub

    Private Sub AgregarParametro(ByRef cmd As SqlCommand, ByVal nombreParam As String, ByVal direccionParam As ParameterDirection, ByVal tipoParam As DbType, ByVal valorParam As Object)
        Dim param As IDbDataParameter = cmd.CreateParameter()
        param.ParameterName = nombreParam
        param.DbType = tipoParam
        param.Direction = direccionParam
        param.Value = valorParam

        cmd.Parameters.Add(param)
    End Sub

End Class
