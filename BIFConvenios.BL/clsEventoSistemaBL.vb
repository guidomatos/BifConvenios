Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Public Class clsEventoSistemaBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function DevolverObjeto(ByVal pstrHilo As String, ByVal pstrNivel As String, ByVal pstrAccion As String, ByVal pstrMensaje As String, ByVal pstrExcepcion As String, ByVal pstrUsuario As String) As clsEventoSistema
        Dim sistema2 As New clsEventoSistema
        sistema2.Hilo = pstrHilo
        sistema2.Nivel = pstrNivel
        sistema2.Accion = pstrAccion
        sistema2.Mensaje = pstrMensaje
        sistema2.Excepcion = pstrExcepcion
        sistema2.Usuario = pstrUsuario
        Return sistema2
    End Function

    Public Function Insertar(ByVal pobjEventoSistema As clsEventoSistema) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsEventoSistemaDO).Create.Insertar(pobjEventoSistema)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return num
    End Function
End Class
