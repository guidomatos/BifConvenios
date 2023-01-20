Imports System.Configuration
Imports System.IO
Imports System.Web.Mail

Public Class TestConexion

    Public Function DescifrarCadenaConexion(ByVal cadena_cifrada) As String
        Dim cadena_descifrada As String = ""
        Dim cadena_conexion_antes_password_cifrado As String
        Dim password_cifrado As String
        Dim cadena_conexion_despues_password_cifrado As String
        Dim temporal As String
        Dim password_descifrado As String
        'Dim c As New bifwebservices.DecodificarClave
        'Try
        '    cadena_conexion_antes_password_cifrado = cadena_cifrada.Substring(0, cadena_cifrada.IndexOf("Password") + 9)
        '    temporal = cadena_cifrada.Substring(cadena_cifrada.IndexOf("Password") + 9)
        '    cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"))
        '    password_cifrado = temporal.Substring(0, temporal.IndexOf(";"))
        '    password_descifrado = c.Decodifica(password_cifrado)
        '    password_descifrado = temporal.Substring(0, temporal.IndexOf(";"))
        '    cadena_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado
        '    Return cadena_descifrada
        'Catch ex As Exception
        '    Return ""
        'End Try
        'ADD NCA 25/10
        cadena_descifrada = cadena_cifrada
        If ConfigurationManager.AppSettings("cifrado").ToString() = 1 Then
            Dim c As New BIFUtils.WebReference.DecodificarClave
            Try
                cadena_conexion_antes_password_cifrado = cadena_cifrada.Substring(0, cadena_cifrada.IndexOf("Password") + 9)
                temporal = cadena_cifrada.Substring(cadena_cifrada.IndexOf("Password") + 9)
                cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"))
                password_cifrado = temporal.Substring(0, temporal.IndexOf(";"))
                password_descifrado = c.Decodifica(password_cifrado)
                'password_descifrado = temporal.Substring(0, temporal.IndexOf(";"))
                cadena_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado

            Catch ex As Exception
                cadena_descifrada = ""
            End Try
        End If
        Return cadena_descifrada
        'END ADD
    End Function

End Class
