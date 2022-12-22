Imports BIFConvenios.BE
Imports BIFConvenios.DO
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Diagnostics
Public Class clsCoordinadorClienteBL
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ChangeStatus(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create.ChangeStatus(pobjCoordinadorCliente)
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

    Public Function Insert(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create.Insert(pobjCoordinadorCliente)
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

    Public Function ObtieneCoordinadorPorCriterio(ByVal pintCodCoordinador As Integer, ByVal pintCodCliente As Integer, ByVal pintEstado As Integer) As DataTable
        Dim table As DataTable
        Try
            table = Singleton(Of clsCoordinadorClienteDO).Create.ObtieneCoordinadorPorCriterio(pintCodCoordinador, pintCodCliente, pintEstado)
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

    Public Function Update(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create.Update(pobjCoordinadorCliente)
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

    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodod para el cambio de estado del coordinador - debera copiarse para el pase
    Public Function ChangeStatusCoordinador(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create().ChangeStatusCoordinador(pobjCoordinadorCliente)
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Throw handledException
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Throw exception
        End Try
        Return num
    End Function
    '***************************************************************
    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodo insertar coordinadores por cliente - debera copiarse para el pase

    Public Function InsertCoordinador(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create().InsertCoordinadorPersona(pobjCoordinadorCliente)
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Throw handledException
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Throw exception
        End Try
        Return num
    End Function
    '***************************************************************

    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodo para obtener coordinadores por cliente - debera copiarse para el pase
    Public Function ObtieneCoordinadorClientePorCriterio(ByVal pintCodCoordinador As Integer, ByVal pintCodCliente As Integer, ByVal pintEstado As Integer) As System.Data.DataTable
        Dim dataTable As System.Data.DataTable
        Try
            dataTable = Singleton(Of clsCoordinadorClienteDO).Create().ObtieneCoordinadorClientePorCriterio(pintCodCoordinador, pintCodCliente, pintEstado)
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Throw handledException
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Throw exception
        End Try
        Return dataTable
    End Function
    '***************************************************************

    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodo actualizar coordinadores por cliente - debera copiarse para el pase
    Public Function UpdateCoordinador(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Try
            num = Singleton(Of clsCoordinadorClienteDO).Create().UpdateCoordinadorPersona(pobjCoordinadorCliente)
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Throw handledException
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Throw exception
        End Try
        Return num
    End Function
    '***************************************************************

End Class
