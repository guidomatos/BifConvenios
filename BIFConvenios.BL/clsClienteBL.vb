Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Runtime.CompilerServices
Public Class clsClienteBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ActualizarCliente(ByVal pobjCliente As clsCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsClienteDO).Create.ActualizarCliente(pobjCliente)
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

    Public Function EliminarCliente(ByVal pintCodigoCliente As Integer, ByVal pstrUsuario As String) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsClienteDO).Create.EliminarCliente(pintCodigoCliente, pstrUsuario)
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

    Public Function ExisteClienteBifConvenio(ByVal pstrTipoDocumento As Object, ByVal pstrNumeroDocumento As Object) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ExisteClienteBifConvenio(pstrTipoDocumento, pstrNumeroDocumento)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ExisteClienteDesdeAS400PorCodIBS(ByVal pintCodIBS As Integer) As Integer
        Dim intContar As Integer
        Try
            intContar = Singleton(Of clsClienteDO).Create.ExisteClienteDesdeAS400PorCodIBS(pintCodIBS)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return intContar
    End Function

    Public Function ObtenerClienteDesdeAS400PorCodIBS(ByVal pintCodIBS As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerClienteDesdeAS400PorCodIBS(pintCodIBS)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerClienteDesdeAS400PorDocumento(ByVal pstrTipoDocumento As String, ByVal pstrNumeroDocumento As String) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerClienteDesdeAS400PorDocumento(pstrTipoDocumento, pstrNumeroDocumento)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerClientePorCodigo(ByVal pintCodigoCliente As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerClientePorCodigo(pintCodigoCliente)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerEmailsEnviosClientes(ByVal pintCodigoCliente As String) As String
        Dim str As String
        Try
            str = Singleton(Of clsClienteDO).Create.ObtenerEmailsEnviosClientes(pintCodigoCliente)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return str
    End Function

    Public Function ObtenerFuncionarioConvenioPorCodigoIBSDesdeAS400(ByVal pintCodIBS As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerFuncionarioConvenioPorCodigoIBSDesdeAS400(pintCodIBS)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerGestorConvenioPorCodigoIBSDesdeAS400(ByVal pintCodIBS As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerGestorConvenioPorCodigoIBSDesdeAS400(pintCodIBS)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaClienteDesdeAS400(ByVal pstrCliente As String, ByVal pstrCodIBS As String, ByVal pstrNumDocumento As String, ByVal pstrCantidadRegistros As String) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaClienteDesdeAS400(pstrCliente, pstrCodIBS, pstrNumDocumento, pstrCantidadRegistros)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaClientePorCriterio(ByVal objCliente As clsCliente, ByVal intStartRowIndex As Integer, ByVal intMaxRows As Integer, ByRef intTotalRows As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaClientePorCriterio(objCliente, intStartRowIndex, intMaxRows, intTotalRows)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaClientesByDiaEnvio(ByVal pintDiaEnvio As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaClientesByDiaEnvio(pintDiaEnvio)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaDocumentosClientesRegistrados() As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaDocumentosClientesRegistrados
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaFuncionarios() As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaFuncionarios
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaFuncionariosConveniosDesdeAS400() As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaFuncionariosConveniosDesdeAS400
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerListaGestoresConvenioDesdeAS400() As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerListaGestoresConvenioDesdeAS400
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

    Public Function ObtenerSaldoContablePorCodigoIBS(ByVal pstrCodIBS As String) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsClienteDO).Create.ObtenerSaldoContablePorCodigoIBS(pstrCodIBS)
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            Throw ex
        End Try
        Return table
    End Function

End Class
