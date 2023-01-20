Imports BIFConvenios.BE
Imports DAL
Imports Microsoft.VisualBasic.CompilerServices
Imports Resource
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class clsCoordinadorClienteDO
    <DebuggerNonUserCode()> Public Sub New()
        MyBase.New()
    End Sub
    ' Methods
    Public Function ChangeStatus(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[CoordinadorClienteChangeStatus]")
            dasql.AddParameter(command, "@iCoordinadorId", pobjCoordinadorCliente.CodigoCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pobjCoordinadorCliente.EstadoCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjCoordinadorCliente.UsuarioModificacion, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            num = 1
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return num
    End Function

    Public Function Insert(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[CoordinadorClienteInsert]")
            dasql.AddParameter(command, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreCoordinador", pobjCoordinadorCliente.NombreCoordinador, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vEmailCoordinador", pobjCoordinadorCliente.EmailCoordinador, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono", pobjCoordinadorCliente.Telefono, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vAnexo", pobjCoordinadorCliente.Anexo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCelular", pobjCoordinadorCliente.Celular, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCargo", pobjCoordinadorCliente.Cargo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTipoPlanilla", pobjCoordinadorCliente.TipoPlanilla, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", pobjCoordinadorCliente.EstadoCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjCoordinadorCliente.UsuarioCreacion, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            num = 1
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return num
    End Function

    Public Function ObtieneCoordinadorPorCriterio(ByVal pintCodCoordinador As Integer, ByVal pintCodCliente As Integer, ByVal pintEstado As Integer) As DataTable
        Dim table As DataTable
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[CoordinadorClienteSelect]")
            dasql.AddParameter(command, "@iCoordinadorId", pintCodCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pintCodCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@iEstado", pintEstado, SqlDbType.Int)
            Dim table2 As New DataTable
            table2 = dasql.ExecuteReader(command)
            If (table2.Rows.Count = 0) Then
                Throw New HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            dasql.ConnectionClose()
            dasql = Nothing
            table = table2
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        End Try
        Return table
    End Function

    Public Function Update(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Dim dasql As New DASQL
        Dim command As New SqlCommand
        Try
            dasql.CommandProperties(command, "[dbo].[CoordinadorClienteUpdate]")
            dasql.AddParameter(command, "@iCoordinadorId", pobjCoordinadorCliente.CodigoCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            dasql.AddParameter(command, "@vNombreCoordinador", pobjCoordinadorCliente.NombreCoordinador, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vEmailCoordinador", pobjCoordinadorCliente.EmailCoordinador, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTelefono", pobjCoordinadorCliente.Telefono, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vAnexo", pobjCoordinadorCliente.Anexo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCelular", pobjCoordinadorCliente.Celular, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vCargo", pobjCoordinadorCliente.Cargo, SqlDbType.VarChar)
            dasql.AddParameter(command, "@vTipoPlanilla", pobjCoordinadorCliente.TipoPlanilla, SqlDbType.VarChar)
            dasql.AddParameter(command, "@iEstado", pobjCoordinadorCliente.EstadoCoordinador, SqlDbType.Int)
            dasql.AddParameter(command, "@vUsuario", pobjCoordinadorCliente.UsuarioModificacion, SqlDbType.VarChar)
            dasql.ExecuteNonQuery(command)
            dasql.ConnectionClose()
            dasql = Nothing
            num = 1
        Catch exception1 As HandledException
            Dim ex As HandledException = exception1
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
            Throw ex
        Catch exception3 As Exception
            Dim ex As Exception = exception3
            ProjectData.SetProjectError(ex)
            dasql.ConnectionClose()
            dasql = Nothing
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
        Dim oCon As DASQL = New DASQL()
        Dim oCommand As SqlCommand = New SqlCommand()
        Try
            oCon.CommandProperties(oCommand, "[dbo].[Coordinador_ClienteChangeStatus]")
            oCon.AddParameter(oCommand, "@iCoordinadorId", pobjCoordinadorCliente.CodigoCoordinador, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@iEstado", pobjCoordinadorCliente.EstadoCoordinador, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@vUsuario", pobjCoordinadorCliente.UsuarioModificacion, SqlDbType.VarChar)
            oCon.ExecuteNonQuery(oCommand)
            oCon.ConnectionClose()
            oCon = Nothing
            num = 1
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Dim ex1 As Resource.HandledException = handledException
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex1
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex2 As System.Exception = exception
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex2
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
        Dim oCon As DASQL = New DASQL()
        Dim oCommand As SqlCommand = New SqlCommand()
        Try
            oCon.CommandProperties(oCommand, "[dbo].[Coordinador_ClienteSelect]")
            oCon.AddParameter(oCommand, "@iCoordinadorId", pintCodCoordinador, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@iClienteId", pintCodCliente, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@iEstado", pintEstado, SqlDbType.Int)
            Dim _dt As System.Data.DataTable = New System.Data.DataTable()
            _dt = oCon.ExecuteReader(oCommand)
            If (_dt.Rows.Count = 0) Then
                Throw New Resource.HandledException(-400, clsConstantsGeneric.NoRecords, clsConstantsGeneric.NoRecordsFull)
            End If
            oCon.ConnectionClose()
            oCon = Nothing
            dataTable = _dt
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Dim ex1 As Resource.HandledException = handledException
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex1
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex2 As System.Exception = exception
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex2
        End Try
        Return dataTable
    End Function
    '***************************************************************

    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodo insertar coordinadores por cliente - debera copiarse para el pase
    Public Function InsertCoordinadorPersona(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Dim oCon As DASQL = New DASQL()
        Dim oCommand As SqlCommand = New SqlCommand()
        Try
            oCon.CommandProperties(oCommand, "[dbo].[Coordinador_ClienteInsert]")
            oCon.AddParameter(oCommand, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@vNombreCoordinador", pobjCoordinadorCliente.NombreCoordinador, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vEmailCoordinador", pobjCoordinadorCliente.EmailCoordinador, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vTelefono", pobjCoordinadorCliente.Telefono, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vAnexo", pobjCoordinadorCliente.Anexo, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vCelular", pobjCoordinadorCliente.Celular, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vCargo", pobjCoordinadorCliente.Cargo, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vTipoPlanilla", pobjCoordinadorCliente.TipoPlanilla, SqlDbType.VarChar)
            ''oCon.AddParameter(oCommand, "@iEstado", pobjCoordinadorCliente.Estado, SqlDbType.Bit)
            oCon.AddParameter(oCommand, "@vUsuario", pobjCoordinadorCliente.UsuarioCreacion, SqlDbType.VarChar)
            oCon.ExecuteNonQuery(oCommand)
            oCon.ConnectionClose()
            oCon = Nothing
            num = 1
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Dim ex1 As Resource.HandledException = handledException
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex1
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex2 As System.Exception = exception
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex2
        End Try
        Return num
    End Function
    '***************************************************************

    '***************************************************************
    'TI-EA2019-11648
    'Creado por: magno Sanchez
    'Fecha Creacion: 04/07/2019
    'Descripcion: se agrego metodo actualizar coordinadores por cliente - debera copiarse para el pase
    Public Function UpdateCoordinadorPersona(ByVal pobjCoordinadorCliente As clsCoordinadorCliente) As Integer
        Dim num As Integer
        Dim oCon As DASQL = New DASQL()
        Dim oCommand As SqlCommand = New SqlCommand()
        Try
            oCon.CommandProperties(oCommand, "[dbo].[Coordinador_ClienteUpdate]")
            oCon.AddParameter(oCommand, "@iCoordinadorId", pobjCoordinadorCliente.CodigoCoordinador, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@iClienteId", pobjCoordinadorCliente.CodigoCliente, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@vNombreCoordinador", pobjCoordinadorCliente.NombreCoordinador, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vEmailCoordinador", pobjCoordinadorCliente.EmailCoordinador, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vTelefono", pobjCoordinadorCliente.Telefono, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vAnexo", pobjCoordinadorCliente.Anexo, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vCelular", pobjCoordinadorCliente.Celular, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vCargo", pobjCoordinadorCliente.Cargo, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@vTipoPlanilla", pobjCoordinadorCliente.TipoPlanilla, SqlDbType.VarChar)
            oCon.AddParameter(oCommand, "@iEstado", pobjCoordinadorCliente.EstadoCoordinador, SqlDbType.Int)
            oCon.AddParameter(oCommand, "@vUsuario", pobjCoordinadorCliente.UsuarioModificacion, SqlDbType.VarChar)
            oCon.ExecuteNonQuery(oCommand)
            oCon.ConnectionClose()
            oCon = Nothing
            num = 1
        Catch handledException As Resource.HandledException
            ProjectData.SetProjectError(handledException)
            Dim ex1 As Resource.HandledException = handledException
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex1
        Catch exception As System.Exception
            ProjectData.SetProjectError(exception)
            Dim ex2 As System.Exception = exception
            oCon.ConnectionClose()
            oCon = Nothing
            Throw ex2
        End Try
        Return num
    End Function
    '***************************************************************

End Class
