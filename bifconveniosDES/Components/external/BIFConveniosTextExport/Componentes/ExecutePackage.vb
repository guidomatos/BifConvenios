Option Strict Off
Option Explicit On 

Imports System.Configuration.ConfigurationSettings


Public Class ExecutePackage

    Private goPackageOld As New DTS.Package()
    Private goPackage As DTS.Package2
    Private mserverName As String
    Private mServerUserName As String
    Private mPackageName As String
    Private mServerPassword As String
    Public Sub ExecutePackage(ByRef packageName As String)

        'Control de error en la ejecucion de un paquete de datos
        On Error GoTo PackageError

        goPackage = goPackageOld

        mserverName = AppSettings("ServerName") ' serverName
        mPackageName = packageName
        mServerPassword = AppSettings("DBUserPwd")
        mServerUserName = AppSettings("DBUser")

        goPackage.Name = packageName
        goPackage.WriteCompletionStatusToNTEventLog = False
        goPackage.FailOnError = False
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
        ' create package steps information
        '---------------------------------------------------------------------------

        Dim oStep As DTS.Step2
        Dim oPrecConstraint As DTS.PrecedenceConstraint

        '------------- a new step defined below

        oStep = goPackage.Steps.New

        oStep.Name = "DTSStep_DTSExecutePackageTask_1"
        oStep.Description = "Execute Package Task: undefined"
        oStep.ExecutionStatus = 1
        oStep.TaskName = "DTSTask_DTSExecutePackageTask_1"
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

        '---------------------------------------------------------------------------
        ' create package tasks information
        '---------------------------------------------------------------------------

        '------------- call Task_Sub1 for task DTSTask_DTSExecutePackageTask_1 (Execute Package Task: undefined)
        Call Task_Sub1(goPackage)

        '---------------------------------------------------------------------------
        ' Save or execute package
        '---------------------------------------------------------------------------

        'goPackage.SaveToSQLServer "(local)", "sa", ""
        goPackage.FailOnError = True
        goPackage.Execute()
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package line above and uncomment the saving package line
        'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackage = Nothing
        'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackageOld = Nothing

        frmMonitor.Add2LogMessage("Ejecución finalizada exitosamente: " + mPackageName)  'mPackageName
        Exit Sub
PackageError:
        If Err.Number = 0 Then Exit Sub
        Dim sMsg As String
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Steps.Item. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        sMsg = "Ha ocurrio un error en la ejecución: " & sErrorNumConv(Err.Number) & vbCrLf & Err.Description & vbCrLf & sAccumStepErrors(goPackage)
        frmMonitor.Add2LogMessage(sMsg + ": " + mPackageName)

        'RESPINOZA 20070821 - Se agrega esto para liberar los recursos del package y evitar reiniciar el app
        goPackage.UnInitialize()
        'to save a package instead of executing it, comment out the executing package line above and uncomment the saving package line
        'UPGRADE_NOTE: Object goPackage may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackage = Nothing
        'UPGRADE_NOTE: Object goPackageOld may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        goPackageOld = Nothing
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
                        sMessage = sMessage & vbCrLf & "Step " & oStep.Name & " failed, error: " & sErrorNumConv(lErrNum) & vbCrLf & sDescr & vbCrLf
                    End If
                End If
            Next oStep
        End If
        sAccumStepErrors = sMessage
    End Function

    Private Function sErrorNumConv(ByVal lErrNum As Integer) As String
        'Convierte el numero de error en formas leibles, ambos hexadecimal y decimal para las palabras de bajo orden.
        If lErrNum < 65536 And lErrNum > -65536 Then
            sErrorNumConv = "x" & Hex(lErrNum) & ",  " & CStr(lErrNum)
        Else
            sErrorNumConv = "x" & Hex(lErrNum) & ",  x" & Hex(lErrNum And -65536) & " + " & CStr(lErrNum And 65535)
        End If
    End Function

    '-----------------------------------------------------------------------------
    '------------- define Task_Sub1 for task DTSTask_DTSExecutePackageTask_1 (Execute Package Task: undefined)
    Private Sub Task_Sub1(ByVal goPackage As Object)

        Dim oTask As DTS.Task
        Dim oLookup As DTS.Lookup

        Dim oCustomTask1 As DTS.ExecutePackageTask
        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Tasks. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        oTask = CType(goPackage, DTS.Package).Tasks.New("DTSExecutePackageTask")
        oCustomTask1 = oTask.CustomTask

        oCustomTask1.Name = "DTSTask_DTSExecutePackageTask_1"
        oCustomTask1.Description = "Execute Package Task: Ejecucion de paquete"
        oCustomTask1.ServerName = mserverName '"TECNOLOGIA24"
        oCustomTask1.ServerUserName = mServerUserName '"sa"
        oCustomTask1.ServerPassword = mServerPassword
        oCustomTask1.UseTrustedConnection = False
        oCustomTask1.UseRepository = False
        oCustomTask1.PackageName = mPackageName ' "New Package"
        'oCustomTask1.PackageID = "{F7D0385C-D7F6-4D91-8A6F-3F553C647EE8}"

        'UPGRADE_WARNING: Couldn't resolve default property of object goPackage.Tasks. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1037"'
        goPackage.Tasks.Add(oTask)
        'UPGRADE_NOTE: Object oCustomTask1 may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oCustomTask1 = Nothing
        'UPGRADE_NOTE: Object oTask may not be destroyed until it is garbage collected. Click for more: 'ms-help://MS.VSCC/commoner/redir/redirect.htm?keyword="vbup1029"'
        oTask = Nothing
    End Sub
End Class
