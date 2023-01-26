Option Strict Off
Option Explicit On 
Imports System.Configuration.ConfigurationSettings

Public Class EnvioInformacionActualizada
    '****************************************************************
    'Microsoft SQL Server 2000
    'Visual Basic file generated for DTS Package
    'File Name: c:\BIFConvenios - Envio de informacion de convenios actualizada.bas
    'Package Name: BIFConvenios - Envio de informacion de convenios actualizada
    'Package Description:
    'Generated Date: 19/02/2004
    'Generated Time: 03:10:00 p.m.
    '****************************************************************

    Private WithEvents goPackageOld As New DTS.Package()
    Private WithEvents goPackage As DTS.Package2

    Private mruta As String
    Public bCancel As Boolean

    Private mIdProceso As String
    Private mAnio As String
    Private mMes As String
    Private mFecha_ProcesoAS400 As String
    Private mCustomerNumber As String
    Private oProceso As New Proceso()


    Public Sub EjecutaEnvioAS400(ByVal id As String, ByVal CustomerNumber As String, _
                                ByVal anio As String, ByVal mes As String, _
                                ByVal Fecha_ProcesoAS400 As String)

        On Error GoTo PackageError

        goPackage = goPackageOld

        mIdProceso = id
        mAnio = anio
        mMes = mes
        mFecha_ProcesoAS400 = Fecha_ProcesoAS400
        mCustomerNumber = CustomerNumber

        '        Try
        goPackage.Name = "BIFConvenios - Envio de informacion de convenios actualizada - V1"
        goPackage.WriteCompletionStatusToNTEventLog = False
        goPackage.FailOnError = True 'False
        goPackage.PackagePriorityClass = 2
        goPackage.MaxConcurrentSteps = 4
        goPackage.LineageOptions = 0
        goPackage.UseTransaction = True
        goPackage.TransactionIsolationLevel = 4096
        goPackage.AutoCommitTransaction = True
        goPackage.RepositoryMetadataOptions = 0
        goPackage.UseOLEDBServiceComponents = True
        goPackage.LogToSQLServer = False
        goPackage.LogServerFlags = 0
        goPackage.FailPackageOnLogFailure = False
        goPackage.ExplicitGlobalVariables = False
        goPackage.PackageType = 0



        '---------------------------------------------------------------------------
        ' create package connection information
        '---------------------------------------------------------------------------

        Dim oConnection As DTS.Connection2

        '------------- a new connection defined below.
        'For security purposes, the password is never scripted

        oConnection = goPackage.Connections.New("SQLOLEDB")

        oConnection.ConnectionProperties.Item("Persist Security Info").Value = True
        oConnection.ConnectionProperties.Item("User ID").Value = AppSettings("DBUser") ' "sa"
        oConnection.ConnectionProperties.Item("Initial Catalog").Value = AppSettings("DBName")  '"BIFConveniosDesa"
        oConnection.ConnectionProperties.Item("Data Source").Value = AppSettings("ServerName")  '"TECNOLOGIA24"
        oConnection.ConnectionProperties.Item("Application Name").Value = "DTS Designer"

        oConnection.Name = "Conexión a base de datos BIFConvenios"
        oConnection.ID = 1
        oConnection.Reusable = True
        oConnection.ConnectImmediate = False
        oConnection.DataSource = AppSettings("ServerName") '"TECNOLOGIA24"
        oConnection.UserID = AppSettings("DBUser") '"sa"
        oConnection.Password = AppSettings("DBUserPwd")
        oConnection.ConnectionTimeout = 60
        oConnection.Catalog = AppSettings("DBName") '"BIFConveniosDesa"
        oConnection.UseTrustedConnection = False
        oConnection.UseDSL = False

        'If you have a password for this connection, please uncomment and add your password below.
        'oConnection.Password = "<put the password here>"

        'UPGRADE_WARNING: Couldn't resolve default property of object oConnection. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Connections.Add(oConnection)
        'UPGRADE_NOTE: Object oConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oConnection = Nothing

        '------------- a new connection defined below.
        'For security purposes, the password is never scripted

        oConnection = goPackage.Connections.New("IBMDA400")

        oConnection.ConnectionProperties.Item("Persist Security Info").Value = True
        oConnection.ConnectionProperties.Item("User ID").Value = AppSettings("AS400-Conv-UserId") '"PLAZO"
        oConnection.ConnectionProperties.Item("Data Source").Value = AppSettings("AS400-Conv-Server")  '"10.1.100.181"

        oConnection.ConnectionProperties.Item("Prompt").Value = 4
        oConnection.ConnectionProperties.Item("Protection Level").Value = 0
        oConnection.ConnectionProperties.Item("Initial Catalog").Value = AppSettings("AS400-Conv-Catalog") '"SISTEMA2"
        oConnection.ConnectionProperties.Item("OLE DB Services").Value = -1
        oConnection.ConnectionProperties.Item("Transport Product").Value = "Client Access"
        oConnection.ConnectionProperties.Item("SSL").Value = "DEFAULT"
        oConnection.ConnectionProperties.Item("Force Translate").Value = 65535
        oConnection.ConnectionProperties.Item("Default Collection").Value = AppSettings("AS400-Conv-Collection")  '"PLAZO"
        oConnection.ConnectionProperties.Item("Convert Date Time To Char").Value = "TRUE"
        oConnection.ConnectionProperties.Item("Cursor Sensitivity").Value = 3

        oConnection.Name = "Conexion a AS/400"
        oConnection.ID = 2
        oConnection.Reusable = True
        oConnection.ConnectImmediate = False
        oConnection.DataSource = AppSettings("AS400-Conv-Server") '"10.1.100.181"
        oConnection.UserID = AppSettings("AS400-Conv-UserId") ' "PLAZO"
        oConnection.Password = AppSettings("AS400-Conv-Password")

        oConnection.ConnectionTimeout = 0
        oConnection.Catalog = AppSettings("AS400-Conv-Catalog") '  "SISTEMA2"
        oConnection.UseTrustedConnection = False
        oConnection.UseDSL = False

        'If you have a password for this connection, please uncomment and add your password below.
        'oConnection.Password = "<put the password here>"

        'UPGRADE_WARNING: Couldn't resolve default property of object oConnection. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Connections.Add(oConnection)
        'UPGRADE_NOTE: Object oConnection may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oConnection = Nothing

        '---------------------------------------------------------------------------
        ' create package steps information
        '---------------------------------------------------------------------------

        Dim oStep As DTS.Step2
        Dim oPrecConstraint As DTS.PrecedenceConstraint

        '------------- a new step defined below

        oStep = goPackage.Steps.New

        oStep.Name = "DTSStep_DTSDataPumpTask_1"
        oStep.Description = "Enviar informacion de convenios actualizada-v3"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSDataPumpTask_1"
        oStep.CommitSuccess = False
        oStep.RollbackFailure = False
        oStep.ScriptLanguage = "VBScript"
        oStep.AddGlobalVariables = True
        oStep.RelativePriority = 3
        oStep.CloseConnection = False
        oStep.ExecuteInMainThread = True
        oStep.IsPackageDSORowset = False
        oStep.JoinTransactionIfPresent = False
        oStep.DisableStep = False
        oStep.FailPackageOnError = False

        'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Steps.Add(oStep)
        'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oStep = Nothing

        '------------- a new step defined below

        oStep = goPackage.Steps.New

        oStep.Name = "DTSStep_DTSExecuteSQLTask_1"
        oStep.Description = "Eliminación de información de AS/400"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSExecuteSQLTask_1"
        oStep.CommitSuccess = False
        oStep.RollbackFailure = False
        oStep.ScriptLanguage = "VBScript"
        oStep.AddGlobalVariables = True
        oStep.RelativePriority = 3
        oStep.CloseConnection = False
        oStep.ExecuteInMainThread = True
        oStep.IsPackageDSORowset = False
        oStep.JoinTransactionIfPresent = False
        oStep.DisableStep = False
        oStep.FailPackageOnError = False

        'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Steps.Add(oStep)
        'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oStep = Nothing

        '------------- a new step defined below

        oStep = goPackage.Steps.New

        oStep.Name = "DTSStep_DTSExecuteSQLTask_2"
        oStep.Description = "Exito"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSExecuteSQLTask_2"
        oStep.CommitSuccess = False
        oStep.RollbackFailure = False
        oStep.ScriptLanguage = "VBScript"
        oStep.AddGlobalVariables = True
        oStep.RelativePriority = 3
        oStep.CloseConnection = False
        oStep.ExecuteInMainThread = True
        oStep.IsPackageDSORowset = False
        oStep.JoinTransactionIfPresent = False
        oStep.DisableStep = False
        oStep.FailPackageOnError = False

        'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Steps.Add(oStep)
        'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oStep = Nothing

        '------------- a new step defined below

        'oStep = goPackage.Steps.New

        'oStep.Name = "DTSStep_DTSExecuteSQLTask_3"
        'oStep.Description = "Error"
        'oStep.ExecutionStatus = 1
        'oStep.TaskName = "DTSTask_DTSExecuteSQLTask_3"
        'oStep.CommitSuccess = False
        'oStep.RollbackFailure = False
        'oStep.ScriptLanguage = "VBScript"
        'oStep.AddGlobalVariables = True
        'oStep.RelativePriority = 3
        'oStep.CloseConnection = False
        'oStep.ExecuteInMainThread = True
        'oStep.IsPackageDSORowset = False
        'oStep.JoinTransactionIfPresent = False
        'oStep.DisableStep = False
        'oStep.FailPackageOnError = False

        'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        'goPackage.Steps.Add(oStep)
        'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        'oStep = Nothing '

        '------------- a new step defined below

        oStep = goPackage.Steps.New

        oStep.Name = "DTSStep_DTSExecuteSQLTask_4"
        oStep.Description = "Estableciendo Estado Envio de Información"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSExecuteSQLTask_4"
        oStep.CommitSuccess = False
        oStep.RollbackFailure = False
        oStep.ScriptLanguage = "VBScript"
        oStep.AddGlobalVariables = True
        oStep.RelativePriority = 3
        oStep.CloseConnection = False
        oStep.ExecuteInMainThread = False
        oStep.IsPackageDSORowset = False
        oStep.JoinTransactionIfPresent = False
        oStep.DisableStep = False
        oStep.FailPackageOnError = False

        'UPGRADE_WARNING: Couldn't resolve default property of object oStep. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Steps.Add(oStep)
        'UPGRADE_NOTE: Object oStep may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oStep = Nothing

        '------------- a precedence constraint for steps defined below

        oStep = goPackage.Steps.Item("DTSStep_DTSDataPumpTask_1")
        oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSExecuteSQLTask_4")
        oPrecConstraint.StepName = "DTSStep_DTSExecuteSQLTask_4"
        oPrecConstraint.PrecedenceBasis = 1
        oPrecConstraint.Value = 0

        oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oPrecConstraint = Nothing

        '------------- a precedence constraint for steps defined below

        oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_2")
        oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSDataPumpTask_1")
        oPrecConstraint.StepName = "DTSStep_DTSDataPumpTask_1"
        oPrecConstraint.PrecedenceBasis = 1
        oPrecConstraint.Value = 0

        oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oPrecConstraint = Nothing

        '------------- a precedence constraint for steps defined below

        'oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_3")
        'oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSDataPumpTask_1")
        'oPrecConstraint.StepName = "DTSStep_DTSDataPumpTask_1"
        'oPrecConstraint.PrecedenceBasis = 1
        'oPrecConstraint.Value = 1

        'oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        'oPrecConstraint = Nothing

        '------------- a precedence constraint for steps defined below

        oStep = goPackage.Steps.Item("DTSStep_DTSExecuteSQLTask_4")
        oPrecConstraint = oStep.PrecedenceConstraints.New("DTSStep_DTSExecuteSQLTask_1")
        oPrecConstraint.StepName = "DTSStep_DTSExecuteSQLTask_1"
        oPrecConstraint.PrecedenceBasis = 1
        oPrecConstraint.Value = 0

        oStep.PrecedenceConstraints.Add(oPrecConstraint)
        'UPGRADE_NOTE: Object oPrecConstraint may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oPrecConstraint = Nothing
        ' Only call the following when developing the application. You
        ' can comment out the call when you build your application.
        Call ChangeAllStepsToRunOnMainThread(goPackage)
        '---------------------------------------------------------------------------
        ' create package tasks information
        '---------------------------------------------------------------------------

        '------------- call Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Enviar informacion de convenios actualizada)
        Call Task_Sub1(goPackage)

        '------------- call Task_Sub2 for task DTSTask_DTSExecuteSQLTask_1 (Eliminación de información de AS/400)
        Call Task_Sub2(goPackage)

        '------------- call Task_Sub3 for task DTSTask_DTSExecuteSQLTask_2 (Exito)
        Call Task_Sub3(goPackage)

        '------------- call Task_Sub4 for task DTSTask_DTSExecuteSQLTask_3 (Error)
        'Call Task_Sub4(goPackage)

        '------------- call Task_Sub5 for task DTSTask_DTSExecuteSQLTask_4 (Estableciendo Estado Envio de Información)
        Call Task_Sub5(goPackage)

        '---------------------------------------------------------------------------
        ' Save or execute package
        '---------------------------------------------------------------------------
        goPackage.FailOnError = True
        'goPackage.SaveToSQLServer("(local)", "sa", "webdev")
        goPackage.Execute()
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        tracePackageError(goPackage)
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package lines above and uncomment the saving package line
        'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackage = Nothing

        'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackageOld = Nothing

        'Catch ex As Exception
        '    Dim sMsg0 As String
        '    frmMonitor.Add2LogMessage("Ha ocurrio un error en la ejecución : ")
        '    sMsg0 = ex.ToString
        '    Call HandleError(sMsg0)
        'End Try
        Exit Sub
PackageError:
        If Err.Number = 0 Then Exit Sub
        Dim sMsg As String
        frmMonitor.Add2LogMessage("Ha ocurrio un error en la ejecución : " + sErrorNumConv(Err.Number) & vbCrLf & Err.Description)
        sMsg = "Trace: " & vbCrLf & sAccumStepErrors(goPackage)
        Call HandleError(sMsg)
    End Sub

    'Manipular el error generado
    Private Sub HandleError(ByVal smsg As String)
        frmMonitor.Add2LogMessage(sMsg)
        oProceso.ErrorEnvioInformacionAS400(mIdProceso, "Server")
        Dim oEliminacionErrorEnvio As New EliminacionErrorEnvio()
        oEliminacionErrorEnvio.Execute(mCustomerNumber, mAnio, mMes, mFecha_ProcesoAS400)
    End Sub


    '-------------------------------- Control de errores -------------------------
    Private Function sAccumStepErrors(ByVal objPackage As DTS.Package) As String
        'Acumula los pasos de error dentro de la información de mensaje de error
        Dim oStep As DTS.Step
        Dim sMessage As String
        Dim lErrNum As Integer
        Dim sDescr As String
        Dim sSource As String

        'Revisa los pasos que se han completado y fallado
        If Not objPackage Is Nothing Then
            For Each oStep In objPackage.Steps
                If oStep.ExecutionStatus = DTS.DTSStepExecStatus.DTSStepExecStat_Completed Then
                    If oStep.ExecutionResult = DTS.DTSStepExecResult.DTSStepExecResult_Failure Then

                        'Obtiene la informacion de error del paso y adiciona este al mensaje
                        oStep.GetExecutionErrorInfo(lErrNum, sSource, sDescr)
                        'MIGRAR INNOVA FALTA
                        'sMessage = sMessage & vbCrLf & "Step " & oStep.Name & " failed, error: " & sErrorNumConv(lErrNum) & vbCrLf & sDescr & vbCrLf
                    End If
                End If
            Next oStep
        End If
        sAccumStepErrors = sMessage
    End Function

    Private Function sErrorNumConv(ByVal lErrNum As Integer) As String
        'Convierte el numero de error en formas leibles, ambos hexadecimal y decimal para las palabras de bajo orden.
        'MIGRAR INNOVA FALTA
        If lErrNum < 65536 And lErrNum > -65536 Then
            sErrorNumConv = "x" & Hex(lErrNum) & ",  " & CStr(lErrNum)
        Else
            sErrorNumConv = "x" & Hex(lErrNum) & ",  x" & Hex(lErrNum And -65536) & " + " & CStr(lErrNum And 65535)
        End If
    End Function
    '-----------------------------------------------------------------------------
    ' error reporting using step.GetExecutionErrorInfo after execution
    '-----------------------------------------------------------------------------
    Private Sub tracePackageError(ByRef oPackage As DTS.Package)
        Dim ErrorCode As Integer
        Dim ErrorSource As String
        Dim ErrorDescription As String
        Dim ErrorHelpFile As String
        Dim ErrorHelpContext As Integer
        Dim ErrorIDofInterfaceWithError As String
        Dim i As Short

        For i = 1 To oPackage.Steps.Count
            If oPackage.Steps.Item(i).ExecutionResult = DTS.DTSStepExecResult.DTSStepExecResult_Failure Then
                oPackage.Steps.Item(i).GetExecutionErrorInfo(ErrorCode, ErrorSource, ErrorDescription, ErrorHelpFile, ErrorHelpContext, ErrorIDofInterfaceWithError)
                frmMonitor.Add2LogMessage(oPackage.Steps.Item(i).Name & " failed" & vbCrLf & ErrorSource & vbCrLf & ErrorDescription)
            End If
        Next i

    End Sub

    '------------- define Task_Sub1 for task DTSTask_DTSDataPumpTask_1 (Enviar informacion de convenios actualizada)
    Private Sub Task_Sub1(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        'Dim oLookup As DTS.Lookup

        Dim oCustomTask1 As DTS.DataPumpTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSDataPumpTask")
        oTask.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1 = oTask.CustomTask

        oCustomTask1.Name = "DTSTask_DTSDataPumpTask_1"
        oCustomTask1.Description = "Enviar informacion de convenios actualizada"
        oCustomTask1.SourceConnectionID = 1
        oCustomTask1.SourceSQLStatement = "EXEC EnvioDescuentos_AS400 N'" & mIdProceso & "'" 'N'6bc32ef1-152c-4afa-8bed-b3d71317d7d6'"
        oCustomTask1.DestinationConnectionID = 2
        oCustomTask1.DestinationObjectName = AppSettings("AS400-Conv-Catalog") & "." & AppSettings("AS400-Conv-Collection") & ".DLREC"    '"SISTEMA2.PLAZO.DLREC"
        oCustomTask1.ProgressRowCount = 1000
        oCustomTask1.MaximumErrorCount = 0
        oCustomTask1.FetchBufferSize = 100
        oCustomTask1.UseFastLoad = True
        oCustomTask1.InsertCommitSize = 0
        oCustomTask1.ExceptionFileColumnDelimiter = "|"
        oCustomTask1.ExceptionFileRowDelimiter = vbCrLf
        oCustomTask1.AllowIdentityInserts = False
        oCustomTask1.FirstRow = "0"
        oCustomTask1.LastRow = "0"
        oCustomTask1.FastLoadOptions = 2
        oCustomTask1.ExceptionFileOptions = 1
        oCustomTask1.DataPumpOptions = 0

        Call oCustomTask1_Trans_Sub1(oCustomTask1)
        Call oCustomTask1_Trans_Sub2(oCustomTask1)
        Call oCustomTask1_Trans_Sub3(oCustomTask1)
        Call oCustomTask1_Trans_Sub4(oCustomTask1)
        Call oCustomTask1_Trans_Sub5(oCustomTask1)
        Call oCustomTask1_Trans_Sub6(oCustomTask1)
        Call oCustomTask1_Trans_Sub7(oCustomTask1)
        Call oCustomTask1_Trans_Sub8(oCustomTask1)
        Call oCustomTask1_Trans_Sub9(oCustomTask1)
        Call oCustomTask1_Trans_Sub10(oCustomTask1)
        Call oCustomTask1_Trans_Sub11(oCustomTask1)
        Call oCustomTask1_Trans_Sub12(oCustomTask1)
        Call oCustomTask1_Trans_Sub13(oCustomTask1)
        Call oCustomTask1_Trans_Sub14(oCustomTask1)
        Call oCustomTask1_Trans_Sub15(oCustomTask1)
        Call oCustomTask1_Trans_Sub16(oCustomTask1)



        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask1 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub1(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__1"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLCC", 1)
        oColumn.Name = "DLCC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 9
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRCC", 1)
        oColumn.Name = "DLRCC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 9
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub2(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__2"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLAN", 1)
        oColumn.Name = "DLAN"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRAN", 1)
        oColumn.Name = "DLRAN"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub3(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__3"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLAG", 1)
        oColumn.Name = "DLAG"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRAG", 1)
        oColumn.Name = "DLRAG"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 3
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub4(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__4"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLCO", 1)
        oColumn.Name = "DLCO"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRCO", 1)
        oColumn.Name = "DLRCO"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub5(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__5"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLMO", 1)
        oColumn.Name = "DLMO"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRMO", 1)
        oColumn.Name = "DLRMO"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 3
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub6(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__6"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLNP", 1)
        oColumn.Name = "DLNP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRNP", 1)
        oColumn.Name = "DLRNP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 12
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub7(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__7"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLCM", 1)
        oColumn.Name = "DLCM"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRCM", 1)
        oColumn.Name = "DLRCM"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub8(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__8"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLNE", 1)
        oColumn.Name = "DLNE"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 75
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRNE", 1)
        oColumn.Name = "DLRNE"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 75
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub9(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__9"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLCR", 1)
        oColumn.Name = "DLCR"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRCR", 1)
        oColumn.Name = "DLRCR"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 20
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub10(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__10"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLAP", 1)
        oColumn.Name = "DLAP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRAP", 1)
        oColumn.Name = "DLRAP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 4
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub11(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__11"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLMP", 1)
        oColumn.Name = "DLMP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRMP", 1)
        oColumn.Name = "DLRMP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 2
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub12(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__12"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLIC", 1)
        oColumn.Name = "DLIC"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 2
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRIC", 1)
        oColumn.Name = "DLRIC"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub13(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__13"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLST", 1)
        oColumn.Name = "DLST"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRST", 1)
        oColumn.Name = "DLRST"
        oColumn.Ordinal = 1
        oColumn.Flags = 4
        oColumn.Size = 1
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub14(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__14"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLID", 1)
        oColumn.Name = "DLID"
        oColumn.Ordinal = 1
        oColumn.Flags = 120
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 2
        oColumn.Nullable = True

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRID", 1)
        oColumn.Name = "DLRID"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 14
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub15(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__15"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLFP", 1)
        oColumn.Name = "DLFP"
        oColumn.Ordinal = 1
        oColumn.Flags = 24
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 8
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRFP", 1)
        oColumn.Name = "DLRFP"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 8
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    Private Sub oCustomTask1_Trans_Sub16(ByVal oCustomTask1 As Object)

        Dim oTransformation As DTS.Transformation2
        Dim oTransProps As DTS.Properties
        Dim oColumn As DTS.Column
        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTransformation = CType(oCustomTask1, DTS.DataPumpTask2).Transformations.New("DTS.DataPumpTransformCopy")
        oTransformation.Name = "DTSTransformation__16"
        oTransformation.TransformFlags = 63
        oTransformation.ForceSourceBlobsBuffered = 0
        oTransformation.ForceBlobsInMemory = False
        oTransformation.InMemoryBlobSize = 1048576
        oTransformation.TransformPhases = 4

        oColumn = oTransformation.SourceColumns.New("DLER", 1)
        oColumn.Name = "DLER"
        oColumn.Ordinal = 1
        oColumn.Flags = 8
        oColumn.Size = 8
        oColumn.DataType = 129
        oColumn.Precision = 0
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.SourceColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oColumn = oTransformation.DestinationColumns.New("DLRER", 1)
        oColumn.Name = "DLRER"
        oColumn.Ordinal = 1
        oColumn.Flags = 20
        oColumn.Size = 0
        oColumn.DataType = 131
        oColumn.Precision = 8
        oColumn.NumericScale = 0
        oColumn.Nullable = False

        oTransformation.DestinationColumns.Add(oColumn)
        'UPGRADE_NOTE: Object oColumn may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oColumn = Nothing

        oTransProps = oTransformation.TransformServerProperties


        'UPGRADE_NOTE: Object oTransProps may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransProps = Nothing

        'UPGRADE_WARNING: Couldn't resolve default property of object oCustomTask1.Transformations. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oCustomTask1.Transformations.Add(oTransformation)
        'UPGRADE_NOTE: Object oTransformation may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTransformation = Nothing

    End Sub

    '------------- define Task_Sub2 for task DTSTask_DTSExecuteSQLTask_1 (Eliminación de información de AS/400)
    Private Sub Task_Sub2(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        'Dim oLookup As DTS.Lookup

        Dim oCustomTask2 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2 = oTask.CustomTask

        oCustomTask2.Name = "DTSTask_DTSExecuteSQLTask_1"
        oCustomTask2.Description = "Eliminación de información de AS/400"
        oCustomTask2.SQLStatement = "delete from DLREC where DLRCC =" & mCustomerNumber & " AND DLRAP = " & mAnio & " AND DLRMP = " & mMes '& " AND DLRFP = " & mFecha_ProcesoAS400
        oCustomTask2.ConnectionID = 2
        oCustomTask2.CommandTimeout = 0
        oCustomTask2.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask2 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask2 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

    '------------- define Task_Sub3 for task DTSTask_DTSExecuteSQLTask_2 (Exito)
    Private Sub Task_Sub3(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        'Dim oLookup As DTS.Lookup

        Dim oCustomTask3 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_2"
        oCustomTask3 = oTask.CustomTask

        oCustomTask3.Name = "DTSTask_DTSExecuteSQLTask_2"
        oCustomTask3.Description = "Exito"
        oCustomTask3.SQLStatement = "FinalizadoEnvioInformacionAS400 '" & mIdProceso & "', 'Server'"
        oCustomTask3.ConnectionID = 1
        oCustomTask3.CommandTimeout = 0
        oCustomTask3.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask3 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask3 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub

    '------------- define Task_Sub4 for task DTSTask_DTSExecuteSQLTask_3 (Error)
    'Private Sub Task_Sub4(ByVal goPackage As Object)

    '        Dim oTask As DTS.Task
    '       Dim oLookup As DTS.Lookup
    '
    '       Dim oCustomTask4 As DTS.ExecuteSQLTask2
    '
    '       oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
    '      oTask.Name = "DTSTask_DTSExecuteSQLTask_3"
    '     oCustomTask4 = oTask.CustomTask
    '
    '       oCustomTask4.Name = "DTSTask_DTSExecuteSQLTask_3"
    '      oCustomTask4.Description = "Error"
    '     oCustomTask4.SQLStatement = "EXECUTE ErrorEnvioInformacionAS400 '" & mIdProceso & "', 'Server'"
    '    oCustomTask4.ConnectionID = 1
    '   oCustomTask4.CommandTimeout = 0
    '  oCustomTask4.OutputAsRecordset = False
    '
    '
    '       goPackage.Tasks.Add(oTask)
    '      'UPGRADE_NOTE: Object oCustomTask4 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
    '     oCustomTask4 = Nothing
    '    'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
    '   oTask = Nothing
    '
    '   End Sub

    '------------- define Task_Sub5 for task DTSTask_DTSExecuteSQLTask_4 (Estableciendo Estado Envio de Información)
    Private Sub Task_Sub5(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        'Dim oLookup As DTS.Lookup

        Dim oCustomTask5 As DTS.ExecuteSQLTask2

        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecuteSQLTask")
        oTask.Name = "DTSTask_DTSExecuteSQLTask_4"
        oCustomTask5 = oTask.CustomTask

        oCustomTask5.Name = "DTSTask_DTSExecuteSQLTask_4"
        oCustomTask5.Description = "Estableciendo Estado Envio de Información"
        oCustomTask5.SQLStatement = "InicioEnvioInformacionAS400 '" & mIdProceso & "', 'Server'"
        oCustomTask5.ConnectionID = 1
        oCustomTask5.CommandTimeout = 0
        oCustomTask5.OutputAsRecordset = False


        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask5 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask5 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing

    End Sub


    'REspinoza 20070731 - Los siguientes metodos sirven para eliminar el error 800400005 - Provider generated code execution exception
    'http://support.microsoft.com/kb/221193/en-us
    Public Sub ChangeAllStepsToRunOnMainThread(ByVal oPkg As DTS.Package)
        Dim nStepCount As Integer
        For nStepCount = 1 To oPkg.Steps.Count
            oPkg.Steps.Item(nStepCount).ExecuteInMainThread = True
            frmMonitor.Add2LogMessage("Estableciendo la ejecución en el main thread" + oPkg.Steps.Item(nStepCount).Name) '
        Next nStepCount
    End Sub

    Private Sub oPackage_OnError(ByVal EventSource As String, _
                                    ByVal ErrorCode As Integer, _
                                    ByVal Source As String, _
                                    ByVal Description As String, _
                                    ByVal HelpFile As String, _
                                    ByVal HelpContext As Integer, _
                                    ByVal IDofInterfaceWithError As String, _
                                    ByRef pbCancel As Boolean) Handles goPackageOld.OnError
        frmMonitor.Add2LogMessage("oPackage_OnError Fired")
    End Sub

    Private Sub oPackage_OnFinish(ByVal EventSource As String) Handles goPackageOld.OnFinish
        frmMonitor.Add2LogMessage("oPackage_OnFinish Fired")
    End Sub

    Private Sub oPackage_OnProgress(ByVal EventSource As String, _
                                    ByVal ProgressDescription As String, _
                                    ByVal PercentComplete As Integer, _
                                    ByVal ProgressCountLow As Integer, _
                                    ByVal ProgressCountHigh As Integer) Handles goPackageOld.OnProgress
        ' The DTS Package will trigger this event at certain intervals
        ' to report the progress of the package. This can be controlled
        ' by setting the DTS.Package.ProgressRowCount property.
        frmMonitor.Add2LogMessage("oPackage_OnProgress Fired")
    End Sub

    Private Sub oPackage_OnQueryCancel(ByVal EventSource As String, _
                                        ByRef pbCancel As Boolean) Handles goPackageOld.OnQueryCancel
        ' The DTS package will trigger this event at certain intervals to check
        ' whether the execution of the package should quit. Set
        ' pbCancel to true to cancel the execution of the package.
        frmMonitor.Add2LogMessage("oPackage_OnQueryCancel Fired")
        If bCancel Then
            pbCancel = True
            frmMonitor.Add2LogMessage("Canceling package execution.")
        Else
            pbCancel = False
        End If
    End Sub

    Private Sub oPackage_OnStart(ByVal EventSource As String) Handles goPackageOld.OnStart
        frmMonitor.Add2LogMessage("oPackage_OnStart Fired")
    End Sub

End Class