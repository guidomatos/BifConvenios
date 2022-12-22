Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports BIFData.GOIntranet
Imports BroadcasterClass.GOIntranet
Imports System.Runtime.Remoting
Imports System.Data.SqlClient
Imports System.Threading

Imports LREM.Tools.Reporting.Excel


Public Class frmMonitor
    Inherits System.Windows.Forms.Form

    Public watcher As FileSystemWatcher
    'Variables para obter los codigos desde el nombre del archivo que se entrega al proceso
    Private mTipoArchivo As String
    Private mIdProcess As String
    Private mFormatoArchivo As String

    Dim objBroadcaster As New SubmitSuscription()                       'Clase para responder a los eventos de envio del servidor Web
    Private Shared asyncOpsAreDone As New AutoResetEvent(False)         'Para monitorear que las llamadas a los thread's hayan terminado
    Private Shared asyncOpsAreDoneFile As New AutoResetEvent(False)     'Para monitorear que las llamadas a los thread's - ARCHIVOS
    Private Shared asyncOpsAreDoneFileText As New AutoResetEvent(False) 'Para monitorear que las llamadas a los thread's - ARCHIVOS TEXTO

    Private Shared SyncRes As New SyncResource()


#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        txtStringPath.Text = AppSettings("Directorio")
        'Add any initialization after the InitializeComponent() call
        watcher = New FileSystemWatcher()
    End Sub

    'Form overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents lstLog As System.Windows.Forms.ListBox
    Friend WithEvents BlueHeaderControl1 As BlueHeaderControl.BlueHeaderControl
    Friend WithEvents txtStringPath As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents mxpbtnIniciar As MyXPButton.MyXPButton
    Friend WithEvents mxpbtnStop As MyXPButton.MyXPButton
    Friend WithEvents mxpbtnExit As MyXPButton.MyXPButton
    Friend WithEvents mxpbtnClear As MyXPButton.MyXPButton
    Friend WithEvents mxpbtnHide As MyXPButton.MyXPButton
    Friend WithEvents notifyIcon As System.Windows.Forms.NotifyIcon
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(frmMonitor))
        Me.lstLog = New System.Windows.Forms.ListBox()
        Me.BlueHeaderControl1 = New BlueHeaderControl.BlueHeaderControl()
        Me.txtStringPath = New System.Windows.Forms.TextBox()
        Me.label1 = New System.Windows.Forms.Label()
        Me.mxpbtnIniciar = New MyXPButton.MyXPButton()
        Me.mxpbtnStop = New MyXPButton.MyXPButton()
        Me.mxpbtnExit = New MyXPButton.MyXPButton()
        Me.mxpbtnClear = New MyXPButton.MyXPButton()
        Me.mxpbtnHide = New MyXPButton.MyXPButton()
        Me.notifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.SuspendLayout()
        '
        'lstLog
        '
        Me.lstLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lstLog.HorizontalScrollbar = True
        Me.lstLog.Location = New System.Drawing.Point(8, 104)
        Me.lstLog.Name = "lstLog"
        Me.lstLog.ScrollAlwaysVisible = True
        Me.lstLog.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.lstLog.Size = New System.Drawing.Size(768, 197)
        Me.lstLog.TabIndex = 55
        '
        'BlueHeaderControl1
        '
        Me.BlueHeaderControl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.BlueHeaderControl1.Message = "Detenido"
        Me.BlueHeaderControl1.Name = "BlueHeaderControl1"
        Me.BlueHeaderControl1.Size = New System.Drawing.Size(784, 64)
        Me.BlueHeaderControl1.Status = "Lectura de Archivos"
        Me.BlueHeaderControl1.TabIndex = 63
        Me.BlueHeaderControl1.UserName = "BIF Convenios"
        '
        'txtStringPath
        '
        Me.txtStringPath.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.txtStringPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStringPath.Location = New System.Drawing.Point(128, 72)
        Me.txtStringPath.Name = "txtStringPath"
        Me.txtStringPath.ReadOnly = True
        Me.txtStringPath.Size = New System.Drawing.Size(648, 20)
        Me.txtStringPath.TabIndex = 64
        Me.txtStringPath.Text = ""
        '
        'label1
        '
        Me.label1.BackColor = System.Drawing.SystemColors.Control
        Me.label1.Location = New System.Drawing.Point(8, 72)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(120, 22)
        Me.label1.TabIndex = 65
        Me.label1.Text = "Directorio de Escaneo "
        '
        'mxpbtnIniciar
        '
        Me.mxpbtnIniciar.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.mxpbtnIniciar.BtnShape = MyXPButton.emunType.BtnShape.Rectangle
        Me.mxpbtnIniciar.BtnStyle = MyXPButton.emunType.XPStyle.Default
        Me.mxpbtnIniciar.Location = New System.Drawing.Point(320, 312)
        Me.mxpbtnIniciar.Name = "mxpbtnIniciar"
        Me.mxpbtnIniciar.TabIndex = 66
        Me.mxpbtnIniciar.Text = "&Iniciar"
        '
        'mxpbtnStop
        '
        Me.mxpbtnStop.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.mxpbtnStop.BtnShape = MyXPButton.emunType.BtnShape.Rectangle
        Me.mxpbtnStop.BtnStyle = MyXPButton.emunType.XPStyle.Default
        Me.mxpbtnStop.Location = New System.Drawing.Point(400, 312)
        Me.mxpbtnStop.Name = "mxpbtnStop"
        Me.mxpbtnStop.TabIndex = 67
        Me.mxpbtnStop.Text = "&Detener"
        '
        'mxpbtnExit
        '
        Me.mxpbtnExit.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.mxpbtnExit.BtnShape = MyXPButton.emunType.BtnShape.Rectangle
        Me.mxpbtnExit.BtnStyle = MyXPButton.emunType.XPStyle.Default
        Me.mxpbtnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.mxpbtnExit.Location = New System.Drawing.Point(480, 312)
        Me.mxpbtnExit.Name = "mxpbtnExit"
        Me.mxpbtnExit.TabIndex = 68
        Me.mxpbtnExit.Text = "&Salir"
        '
        'mxpbtnClear
        '
        Me.mxpbtnClear.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.mxpbtnClear.BtnShape = MyXPButton.emunType.BtnShape.Rectangle
        Me.mxpbtnClear.BtnStyle = MyXPButton.emunType.XPStyle.Default
        Me.mxpbtnClear.Location = New System.Drawing.Point(560, 312)
        Me.mxpbtnClear.Name = "mxpbtnClear"
        Me.mxpbtnClear.Size = New System.Drawing.Size(224, 23)
        Me.mxpbtnClear.TabIndex = 69
        Me.mxpbtnClear.Text = "Borrar información de seguimiento"
        '
        'mxpbtnHide
        '
        Me.mxpbtnHide.AdjustImageLocation = New System.Drawing.Point(0, 0)
        Me.mxpbtnHide.BtnShape = MyXPButton.emunType.BtnShape.Rectangle
        Me.mxpbtnHide.BtnStyle = MyXPButton.emunType.XPStyle.Default
        Me.mxpbtnHide.Location = New System.Drawing.Point(192, 312)
        Me.mxpbtnHide.Name = "mxpbtnHide"
        Me.mxpbtnHide.Size = New System.Drawing.Size(120, 23)
        Me.mxpbtnHide.TabIndex = 72
        Me.mxpbtnHide.Text = "Ocultar Formulario"
        '
        'notifyIcon
        '
        Me.notifyIcon.Icon = CType(resources.GetObject("notifyIcon.Icon"), System.Drawing.Icon)
        Me.notifyIcon.Text = "BIF Convenios"
        Me.notifyIcon.Visible = True
        '
        'frmMonitor
        '
        Me.AcceptButton = Me.mxpbtnIniciar
        Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
        Me.CancelButton = Me.mxpbtnExit
        Me.ClientSize = New System.Drawing.Size(784, 343)
        Me.Controls.AddRange(New System.Windows.Forms.Control() {Me.mxpbtnHide, Me.mxpbtnClear, Me.mxpbtnExit, Me.mxpbtnStop, Me.mxpbtnIniciar, Me.txtStringPath, Me.label1, Me.BlueHeaderControl1, Me.lstLog})
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.Name = "frmMonitor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BIF Convenios"
        Me.ResumeLayout(False)

    End Sub

#End Region

    'True : Start
    'False: Stop
    Private Sub StartStop(ByVal bln As Boolean)
        mxpbtnIniciar.Enabled = Not bln
        mxpbtnStop.Enabled = bln

        If bln Then
            BlueHeaderControl1.Message = "Iniciado"
        Else
            BlueHeaderControl1.Message = "Detenido"
        End If
        notifyIcon.Text = Me.Text & " - " & BlueHeaderControl1.Message
    End Sub

    Public Sub OnFileEvent(ByVal source As Object, ByVal fsea As FileSystemEventArgs)
        Dim dt As New DateTime()
        Dim formatoCarga As String
        dt = System.DateTime.UtcNow

        Call AddLogMessage(lstLog, fsea.ChangeType.ToString() + " - " + fsea.FullPath)

        'Verificamos si el archivo tiene el nombre correcto para ser ejecutado
        If (fsea.Name.Trim() <> "") Then
            mTipoArchivo = fsea.Name.Substring(0, 2)

            mFormatoArchivo = fsea.Name.Substring(2, 3)
            mIdProcess = fsea.Name.Split(".")(0).Substring(5, fsea.Name.Split(".")(0).Length - 5)
            mIdProcess = Microsoft.VisualBasic.Right(mIdProcess, 36)
            formatoCarga = Cliente.getInformacionFormatoSalida(mIdProcess)

            If formatoCarga.Trim() = "default" Then
                If mTipoArchivo.Trim().ToUpper = "AD" Then
                    If mFormatoArchivo.Trim.ToUpper = "CSV" Then
                        SyncLock Me
                            Call ProcesaArchivoDescuentosEstandar(fsea.FullPath, mIdProcess)
                        End SyncLock
                    ElseIf mFormatoArchivo.Trim.ToUpper = "TXT" Then
                        SyncLock Me
                            Call ProcesaArchivoDescuentosEstandarTexto(fsea.FullPath, mIdProcess)
                        End SyncLock
                    End If
                End If
            Else

                If mFormatoArchivo.Trim.ToUpper = "CSV" Then
                    SyncLock Me
                        Call ProcesaArchivoDescuentosEstandar(fsea.FullPath, mIdProcess)
                    End SyncLock
                Else
                    'TODO: AHSP 20080325 - Formato de Lectura para DBF y varios
                    Select Case formatoCarga.Trim()
                        Case "SDBF"     'Formato para SIGA
                            'TODO: AHSP 20080327 - Obtenemos el mIdProcess del DBF
                            mIdProcess = fsea.Name.Split(".")(0).Substring(6, fsea.Name.Split(".")(0).Length - 6)

                            Dim ProcesaArchivoDBF As New BIFConvenios.ProcesaArchivoDBF()
                            ProcesaArchivoDBF.procesaInformacionDBFConveniosSIGA(fsea.FullPath, mIdProcess, "")
                        Case "VDBF"     'Formato para UNFV
                            'TODO: AHSP 20080327 - Obtenemos el mIdProcess del DBF
                            mIdProcess = fsea.Name.Split(".")(0).Substring(6, fsea.Name.Split(".")(0).Length - 6)

                            Dim ProcesaArchivoDBF As New BIFConvenios.ProcesaArchivoDBF()
                            ProcesaArchivoDBF.procesaInformacionDBFConveniosUNFV(fsea.FullPath, mIdProcess, "")
                        Case "UTES"
                            Dim procesaArchivoTexto As New BIFConvenios.ProcesaArchivoTextoGeneral()
                            procesaArchivoTexto.procesaInformacionArchivoConvenios_UTES(fsea.FullPath, mIdProcess, "")

                        Case Else       'Otros Formatos de texto MINSA, etc.
                            Dim procesaArchivoTexto As New BIFConvenios.ProcesaArchivoTextoGeneral()
                            procesaArchivoTexto.procesaInformacionArchivoConvenios(fsea.FullPath, mIdProcess, "")
                    End Select

                End If

            End If
        End If
    End Sub

    Private Function Salir() As Boolean
        Salir = True
        If MessageBox.Show("¿Desea salir de la aplicación?. No se realizará la lectura de archivos.", Me.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Stop) = DialogResult.Yes Then
            Call mxpbtnStop_Click(Nothing, New EventArgs())
            Salir = False
        End If
    End Function

    Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
        e.Cancel = Salir()
    End Sub

    Private Sub frmMonitor_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Call StartScan()
        Call InitializeRemoting()
        Call ShowHide(False)
    End Sub

    Private Sub mxpbtnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mxpbtnIniciar.Click
        Call StartScan()
    End Sub


    Private Sub StartScan()
        If (watcher.EnableRaisingEvents = True) Then
            Beep()
            Return
        End If

        lstLog.Items.Clear()
        Call AddLogMessage(lstLog, "Iniciando el escaneo en el directorio : " + txtStringPath.Text)

        watcher.Path = txtStringPath.Text
        watcher.NotifyFilter = NotifyFilters.FileName Or NotifyFilters.Attributes Or NotifyFilters.LastAccess _
            Or NotifyFilters.LastWrite Or NotifyFilters.Security Or NotifyFilters.Size

        'Solo revisaremos por los archivos que han sido creados en el directorio
        AddHandler watcher.Created, AddressOf OnFileEvent

        watcher.EnableRaisingEvents = True
        Call StartStop(True)
        Call AddLogMessage(lstLog, "Inicialización realizada con éxito : " + txtStringPath.Text)

    End Sub


    Private Sub mxpbtnStop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mxpbtnStop.Click
        AddLogMessage(lstLog, "Deteniendo el escaneo...")
        watcher.EnableRaisingEvents = False
        RemoveHandler watcher.Created, AddressOf OnFileEvent
        Call StartStop(False)
        AddLogMessage(lstLog, "Escaneo detenido.")
    End Sub

    Private Sub mxpbtnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mxpbtnExit.Click

        Me.Close()
    End Sub

    Private Sub mxpbtnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mxpbtnClear.Click
        If MessageBox.Show("¿Desea eliminar los mensajes de seguimiento?", Me.Text, MessageBoxButtons.YesNo) = DialogResult.Yes Then
            lstLog.Items.Clear()
        End If
    End Sub



#Region "Procedimientos de llamadas para Remoting"
    Private Sub InitializeRemoting()
        Try
            AddLogMessage(lstLog, "Cargando archivo de configuración")
            'Cargamos la informacion de configuracion de los objetos que expone el Runtime
            RemotingConfiguration.Configure("BIFConveniosTextExport.exe.config")
            'Mostramos la lista de objetos cargados (WellKnow)
            Call ListWellKnownServiceTypes()
            'Mostramos la lista de servicios
            Call ListClientActivatedServiceTypes()
            Call CreateAndConnect()
        Catch exc As Exception
            AddLogMessage(lstLog, "Error de inicializacion /metodo: InitializeRemoting =" + exc.ToString)
        End Try
    End Sub

    'Esta funcion itera a travez de todos los tipos WellKnownService 
    ' que fueron cargados via RemotingConfiguration.Configure(Remoting.config)
    Private Sub ListWellKnownServiceTypes()
        Dim entry As WellKnownServiceTypeEntry
        For Each entry In RemotingConfiguration.GetRegisteredWellKnownServiceTypes()
            AddLogMessage(lstLog, entry.TypeName + " está disponible en " + entry.ObjectUri)
        Next
        entry = Nothing
    End Sub

    Private Sub ListClientActivatedServiceTypes()
        Dim entry As ActivatedServiceTypeEntry
        For Each entry In RemotingConfiguration.GetRegisteredActivatedServiceTypes()
            AddLogMessage(lstLog, "ActivatedServiceType Registrado: " + entry.TypeName)
        Next
        entry = Nothing
    End Sub

    'Creamos el objeto que enlaza con el evento para responder a las peticiones de la web
    Private Sub CreateAndConnect()
        Dim args As Object() = {}
        Dim ComputerName As String = SystemInformation.ComputerName
        If ComputerName = Nothing Then
            ComputerName = Configuration.ConfigurationSettings.AppSettings("ServerNameRemoting")
        End If

        AddLogMessage(lstLog, "Suscribiendose a evento en tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition, a las " + Now.ToString)

        objBroadcaster = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"), BroadcasterClass.GOIntranet.SubmitSuscription)
        AddHandler objBroadcaster.Submision, AddressOf SubmissionEvent
    End Sub


    'Desde aqui llamamos a todos los eventos que la aplicacion web desee realizar para liberarlo de carga
    Sub SubmissionEvent(ByVal sender As Object, ByVal submitArgs As SubmitEventArgs)
        'Creamos un objecto ThreadPoolState para pasarle la informacion del formulario actual

        'Si tenemos un valor Test, salimos para no interferir con los procesos
        If submitArgs.Contribution = "Test" And submitArgs.Contributor = "Test" Then
            'Hacemos el callback necesario para evitar el timeout
            Dim MethodCallback As New AsyncCallback(AddressOf Me.RemoteAsyncCallBack)
            Dim MD As New MethodDelegateDummy(AddressOf Me.Dummy)  '(ModuleId, lstMsgs))
            Dim RemAr As IAsyncResult = MD.BeginInvoke(MethodCallback, Nothing)
            Exit Sub
        End If

        Dim oThreadPoolState As New ThreadPoolState()
        oThreadPoolState.form = Me
        oThreadPoolState.submitArgs = submitArgs
        ThreadPool.QueueUserWorkItem(New WaitCallback(AddressOf SubmitionEventThreadFun), oThreadPoolState)
        asyncOpsAreDone.WaitOne()
    End Sub

    'Este metodo sirve solo para liberar la llamada de Test
    Delegate Sub MethodDelegateDummy()
    Sub Dummy()
        'No hacemos nada
    End Sub

    Delegate Sub MethodDelegate(ByVal PID As String, ByVal CustomerNumber As String, ByVal mes As String, ByVal anio As String, ByVal Fecha_ProcesoAS400 As String)
    Delegate Sub MethodDelegate2(ByVal PID As String, ByVal fileFormat As String, ByVal situacionTrabajador As String)
    Delegate Sub MethodDelegate3(ByVal PID As String)
    'Callback para recibir el metodo remoto
    Public Shared Sub RemoteAsyncCallBack(ByVal iar As IAsyncResult)

    End Sub
#End Region

#Region "Funciones para manejar el servidor utilizando un ThreadPool"

    Class ThreadPoolState
        Public form As frmMonitor
        Public submitArgs As SubmitEventArgs
    End Class


    'El manejo del ThreadPool brindara mayor escalabilidad a la aplicacion puesto que esto permitira
    'que se ejecute varias tareas de manera paralela
    'Se realizan bloqueos para que dos instancias de Threads no ingresen a la misma seccion al mismo tiempo
    Public Shared Sub SubmitionEventThreadFun(ByVal O As Object)
        If Not TypeOf O Is ThreadPoolState Then Exit Sub ' Si el objeto no es del tipo que necesitamos
        Dim oThreadPoolState As ThreadPoolState = CType(O, ThreadPoolState)
        SyncRes.Access(oThreadPoolState)
        asyncOpsAreDone.Set()
    End Sub

#End Region

#Region "Generar archivos de cronograma futuro"
    Private Sub GeneraCronogramaFuturo(ByVal PID As String, ByVal fileFormat As String, ByVal situacionTrabajador As String)
        'Obtenemos la informacion del cliente del proceso, formatos de archivo y demas..
        Dim oProceso As New Proceso()
        Dim NombreArchivoProceso As String
        Dim PrefijoArchivoProceso As String
        Dim strTipoCliente As String
        Dim dr As SqlDataReader
        Try
            If situacionTrabajador.Trim <> "" Then
                If situacionTrabajador.Trim = "-" Then
                    strTipoCliente = "-Todos"
                Else
                    strTipoCliente = "-" + situacionTrabajador
                End If
            Else
                strTipoCliente = ""
            End If

            PrefijoArchivoProceso = oProceso.GetNombreArchivoProceso(PID)
            NombreArchivoProceso = PrefijoArchivoProceso + strTipoCliente

            AddLogMessage(lstLog, "Preparando la generación del archivo : " + AppSettings("Generacion") + NombreArchivoProceso + "." + fileFormat)
            If fileFormat.Trim = "csv" Then
                dr = oProceso.CronogramaFuturo(PID, situacionTrabajador)

                Dim oCSVReport As New CSVReport()
                'SyncLock oCSVReport
                'asyncOpsAreDoneCronogramaFuturoCSV.Set()
                oCSVReport.GenerateCSVReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + fileFormat)
                'asyncOpsAreDoneCronogramaFuturoCSV.WaitOne()
                'End SyncLock
                oCSVReport = Nothing
            ElseIf fileFormat.Trim = "csv2" Then
                ''CronogramaFuturoDefault
                dr = oProceso.CronogramaFuturoDefault(PID, situacionTrabajador)
                fileFormat = "csv"
                Dim oCSVReport As New CSVReport()
                'SyncLock oCSVReport
                'asyncOpsAreDoneCronogramaFuturoCSV.Set()
                oCSVReport.GenerateCSVReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + fileFormat)
                'asyncOpsAreDoneCronogramaFuturoCSV.WaitOne()
                'End SyncLock
                oCSVReport = Nothing

            ElseIf fileFormat.Trim = "txt" Then
                dr = oProceso.CronogramaFuturoTexto(PID, situacionTrabajador)

                Dim oTXTReport As New TXTReport()
                'SyncLock oTXTReport
                '   asyncOpsAreDoneCronogramaFuturoTXT.Set()
                oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + fileFormat)
                '  asyncOpsAreDoneCronogramaFuturoTXT.WaitOne()
                'End SyncLock
                oTXTReport = Nothing
            ElseIf fileFormat.Trim = "xls" Then
                dr = oProceso.CronogramaFuturoTexto(PID, situacionTrabajador)

                LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + fileFormat)
                'oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + fileFormat)

                'oTXTReport = Nothing
            ElseIf fileFormat.Trim = "defaultXls" Then
                dr = oProceso.CronogramaFuturoDefault(PID, situacionTrabajador)
                LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dr, AppSettings("Generacion"), NombreArchivoProceso + ".xls")

            Else
                'Cualquier formato customizado entra aqui 
                If fileFormat.Trim.IndexOf("=") > 0 Then
                    Dim format As String
                    Dim fileType As String
                    format = fileFormat.Split("=")(0)
                    fileType = fileFormat.Split("=")(1)

                    If fileType = "SDBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFSIGAReport(dr, NombreArchivoProceso + ".DBF")

                    ElseIf fileType = "xls2" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dr, AppSettings("Generacion"), NombreArchivoProceso + ".xls")
                    ElseIf fileType = "txt2" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        Dim oTXTReport As New TXTReport()
                        oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + ".txt")
                    ElseIf fileType = "SANFERNAND" Then

                        ''dr = oProceso.CronogramaFuturoTexto(PID, situacionTrabajador)
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        Dim oTXTReport As New TXTReport()
                        'SyncLock oTXTReport
                        '   asyncOpsAreDoneCronogramaFuturoTXT.Set()
                        oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + "txt")
                        '  asyncOpsAreDoneCronogramaFuturoTXT.WaitOne()
                        'End SyncLock
                        oTXTReport = Nothing

                    ElseIf fileType = "ADBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFAMAZONASReport(dr, situacionTrabajador + NombreArchivoProceso.Split("-")(0) + ".26")
                    ElseIf fileType = "JDBF" Then
                        If PrefijoArchivoProceso = "UNIDAD" Then
                            dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                            ''generateDBFChulucanasReport
                            DBFReport.generateDBFChulucanasReport(dr, situacionTrabajador + NombreArchivoProceso.Split("-")(0) + ".26")
                        Else
                            dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                            '' cambiar DBFReport.generateDBFCAJAMARCAReport(dr, situacionTrabajador + NombreArchivoProceso.Split("-")(0) + ".26")
                        End If
                    ElseIf fileType = "XDBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFJAENReport(dr, situacionTrabajador + NombreArchivoProceso.Split("-")(0) + ".26")
                    ElseIf fileType = "IDBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        ''DBFReport.generateDBFICAReport(dr, situacionTrabajador + NombreArchivoProceso + ".DBF")
                        ''cambiar DBFReport.generateDBFICAReport(dr, NombreArchivoProceso + ".DBF")
                        ''DBFReport.generateDBFICAReport(dr, NombreArchivoProceso + ".DBF")
                    ElseIf fileType = "BDBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFSanIgnacioReport(dr, situacionTrabajador + NombreArchivoProceso.Split("-")(0) + ".26")

                    ElseIf fileType = "UDBF" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFUNICAReport(dr, situacionTrabajador + NombreArchivoProceso + ".DBF")
                    ElseIf fileType = "VDBF" Then   'Todo: AHSP 20080205 - Para UNFV
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        DBFReport.generateDBFUNFVReport(dr, NombreArchivoProceso + ".DBF")
                    ElseIf fileType = "MINSA" Then   'generar archivos MINSA
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        Dim oTXTReport As New TXTReport()
                        'SyncLock oTXTReport
                        '   asyncOpsAreDoneCronogramaFuturoTXT.Set()
                        oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + "." + "txt")

                        ''DBFReport.generateDBFUNFVReport(dr, NombreArchivoProceso + ".txt")
                    ElseIf fileType = "defaultXls" Then
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        LREM.Tools.Reporting.Excel.ReportGenerator.Generate(dr, AppSettings("Generacion"), NombreArchivoProceso + ".xls")
                    ElseIf fileType = "ASDF" Then   'Todo: AHSP 20080213 - Para Amazonas generacion de Texto
                        dr = oProceso.CronogramaFuturoExecutor(PID, situacionTrabajador, format)
                        Dim oTXTReport As New TXTReport()
                        'SyncLock oTXTReport
                        '   asyncOpsAreDoneCronogramaFuturoTXT.Set()
                        oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), IIf(situacionTrabajador = "-", "T", situacionTrabajador) + PrefijoArchivoProceso + ".0205")
                        '  asyncOpsAreDoneCronogramaFuturoTXT.WaitOne()
                        'End SyncLock
                        oTXTReport = Nothing

                    End If
                Else
                    dr = oProceso.CronogramaFuturoTexto(PID, situacionTrabajador)
                    Dim oTXTReport As New TXTReport()
                    'SyncLock oTXTReport
                    '   asyncOpsAreDoneCronogramaFuturoTXT.Set()
                    oTXTReport.GenerateTXTReport(dr, AppSettings("Generacion"), NombreArchivoProceso + ".txt")
                    '  asyncOpsAreDoneCronogramaFuturoTXT.WaitOne()
                    'End SyncLock
                    oTXTReport = Nothing
                End If
            End If

            AddLogMessage(lstLog, "Archivo : " + AppSettings("Generacion") + NombreArchivoProceso + "." + fileFormat + " generado exitosamente.")
            oProceso.UpdEstadoGeneracionExito(PID, "Server")
            'End SyncLock

        Catch ex As Exception
            AddLogMessage(lstLog, "No se pudo generar el archivo debido al siguiente error:" + ex.ToString)
            oProceso.UpdEstadoGeneracionError(PID, "Server")
        End Try
        oProceso = Nothing
        dr = Nothing
    End Sub
#End Region

#Region "Proceso de carga de información de cronograma futuro"
    Private Sub ProcesaImportacionCronogramaFuturo(ByVal PID As String, ByVal CustomerNumber As String, ByVal mes As String, ByVal anio As String, ByVal Fecha_ProcesoAS400 As String)

        Dim obtenerCronogramaFuturo2 As New obtenerCronogramaFuturo2()
        'obtenerCronogramaFuturo2.ExecuteImport(PID, CustomerNumber, mes, anio, Fecha_ProcesoAS400)
        'Return

        'Dim oObtenerCronogramaFuturo As New ObtenerCronogramaFuturo() 'Ejecuta la importacion de datos desde AS/400
        Dim oCargaCronogamaFuturo As New CargaCronogamaFururo()
        Dim blnProcesoExitoso As Boolean

        Try

            Call AddLogMessage(lstLog, "Cargando información de cronograma futuro desde AS/400 para el cliente " + CustomerNumber & ", mes = " & mes & ", año = " & anio & ", fecha de proceso en AS/400 = " & Fecha_ProcesoAS400 & ". Proceso = " & PID)
            'blnProcesoExitoso = oObtenerCronogramaFuturo.ExecuteImport(PID, CustomerNumber, mes, anio, Fecha_ProcesoAS400)
            blnProcesoExitoso = obtenerCronogramaFuturo2.ExecuteImport(PID, CustomerNumber, mes, anio, Fecha_ProcesoAS400)
            If blnProcesoExitoso Then
                Call AddLogMessage(lstLog, "La carga de cronograma futuro para proceso = " & PID & " fue exitosa")
                Call AddLogMessage(lstLog, "Enviando información de cronograma futuro a tabla ClienteCuota para proceso = " & PID & ".")
                oCargaCronogamaFuturo.EnviaEspacioTrabajo(PID, "Server")
                Call AddLogMessage(lstLog, "Información de cronograma futuro procesada correctamente. Proceso = " & PID & ".")
            Else
                Call AddLogMessage(lstLog, "Ocurrió un error cargando cronograma futuro para el cliente " & ", mes = " & mes & ", año = " & anio & CustomerNumber + ".")
                Call AddLogMessage(lstLog, "Error en la importación de datos /Metodo: oObtenerCronogramaFuturo.ExecuteImport")
                oCargaCronogamaFuturo.EstableceErrorProceso(PID, "Server")
            End If

        Catch e As Exception
            Call AddLogMessage(lstLog, "Ocurrió un error cargando cronograma futuro para el cliente " & ", mes = " & mes & ", año = " & anio & CustomerNumber + ".")
            Call AddLogMessage(lstLog, e.ToString())
            oCargaCronogamaFuturo.EstableceErrorProceso(PID, "Server")
        End Try

        Call AddLogMessage(lstLog, "Información de cronograma futuro finalizada. Proceso = " & PID & ".")


        'oObtenerCronogramaFuturo = Nothing
        obtenerCronogramaFuturo2 = Nothing
        oCargaCronogamaFuturo = Nothing
    End Sub
#End Region

#Region "Adicionar información al log"
    Public Shared Sub Add2LogMessage(ByVal message As String)
        Dim dt As New DateTime()
        dt = System.DateTime.UtcNow

        SyncLock GetType(frmMonitor)
            If TypeOf ActiveForm Is frmMonitor Then
                Dim oWatcher As frmMonitor = CType(ActiveForm, frmMonitor)
                oWatcher.lstLog.Items.Add(dt.ToLocalTime() + " " + message)
            End If
            Utils.add2log(Format(Now, "yyyyMMdd"), dt.ToLocalTime() + " " + message)
        End SyncLock
    End Sub
#End Region

#Region "Codigo para ocultar o visualizar el formulario de proceso"
    'Oculta o muestra el formulario
    Public Sub ShowHide(ByVal b As Boolean)
        Me.ShowInTaskbar = b
        Me.Visible = b

        If b Then
            Me.WindowState = FormWindowState.Normal
            Me.Activate()
        End If
    End Sub

    Private Sub mxpbtnHide_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mxpbtnHide.Click
        Call ShowHide(False)
    End Sub

    Private Sub notifyIcon_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles notifyIcon.DoubleClick
        Call ShowHide(True)
    End Sub
#End Region

#Region "Proceso de archivo de descuentos"
    Private Sub ProcesaArchivoDescuentosEstandar(ByVal ruta As String, ByVal idProcess As String)

        Dim oProcesaArchivoDescuentosEstandar As New ProcesaArchivoDescuentosEstandar()
        SyncLock oProcesaArchivoDescuentosEstandar
            'TODO: Continuar proceso de informacion de descuentos
            asyncOpsAreDoneFile.Set()
            AddLogMessage(lstLog, "Iniciando proceso carga de información de archivo de descuentos: Archivo=" & ruta & ", Proceso = " & idProcess & ".")
            oProcesaArchivoDescuentosEstandar.ExecuteProcesoArchivoDescuentosEstandard(ruta, idProcess)
            AddLogMessage(lstLog, "Proceso de carga de archivo de descuentos finalizado: " & idProcess)
            asyncOpsAreDoneFile.WaitOne()
        End SyncLock
        oProcesaArchivoDescuentosEstandar = Nothing
    End Sub

    Private Sub ProcesaArchivoDescuentosEstandarTexto(ByVal ruta As String, ByVal idProcess As String)
        Dim oProcesaArchivoDescuentosEstandar As New ProcesaArchivoDescuentosTexto()
        'TODO: Continuar proceso de informacion de descuentos
        SyncLock oProcesaArchivoDescuentosEstandar
            asyncOpsAreDoneFileText.Set()
            AddLogMessage(lstLog, "Iniciando proceso carga de información de archivo de descuentos: Archivo=" & ruta & ", Proceso = " & idProcess & ".")
            oProcesaArchivoDescuentosEstandar.ExecuteProcesoArchivoDescuentosEstandardTexto(ruta, idProcess)
            AddLogMessage(lstLog, "Proceso de carga de archivo de descuentos finalizado: " & idProcess)
            asyncOpsAreDoneFileText.WaitOne()
        End SyncLock
        oProcesaArchivoDescuentosEstandar = Nothing
    End Sub
#End Region

#Region "Actualizacion de Tablas de informacion de los clientes de convenios y de los contactos"
    'Ejecutar la actualizacion de la informacion de los estados de las tarjetas de credito
    Public Sub ProcesoActualizarTablas()
        Dim oExecutePackage As New ExecutePackage()
        'Bloquear los archivo que vamos a procesar
        oExecutePackage.ExecutePackage(AppSettings("DTSName-ActualizarTablas"))
    End Sub
#End Region

#Region "Envio de información de descuentos a AS/400"
    Private Sub EnvioInformacionAS400(ByVal idProcess As String)
        Dim oProceso As New Proceso()
        Dim oEliminacionErrorEnvio As New EliminacionErrorEnvio()

        Dim mes As String = ""
        Dim anio As String = ""
        Dim Fecha_ProcesoAS400 As String = ""
        Dim TipoDocumento As String
        Dim NumeroDocumento As String
        Dim CustomerNumber As String
        Try
            SyncLock Me
                ''Dim oEnvioInformacionActualizada As New EnvioInformacionActualizada()
                Dim oEnvioInformacionActualizada As New Envio_informacion_cobranza_IBS()
                oProceso.InicioEnvioInformacionAS400(idProcess, "Server")
                oProceso.getProcesoMesAnio(idProcess, mes, anio, Fecha_ProcesoAS400, _
                                            TipoDocumento, NumeroDocumento)
                CustomerNumber = oProceso.GetCustomerNumber(TipoDocumento, NumeroDocumento)
                AddLogMessage(lstLog, "Cliente = " & CustomerNumber & ", Año = " & anio & ", Mes=" & mes & ", Fecha Proceso AS400 =" & Fecha_ProcesoAS400)

                ''oEnvioInformacionActualizada.EjecutaEnvioAS400(idProcess, CustomerNumber, anio, mes, Fecha_ProcesoAS400)
                oEnvioInformacionActualizada.DataEnvioAS400(idProcess)

                AddLogMessage(lstLog, "Proceso de carga de archivo de descuentos finalizado, PID =" & idProcess)

                ''''''''''''''    JAvier : Este proceso fue descartado ya que si ocurre un error
                '''''''''''''     en la inserciòn de algun registro , simplemente no marcarà a èste como A4 (envìo correcto al IBS)

                ' 20040316 - Verificamos si los numeros de registros son iguales en ambas 
                ' tablas.... si no lo son entonces realizacion la eliminacion 
                ' y lo marcamos como error
                '' '' ''If Not oProceso.MatchCountRecords(idProcess, CustomerNumber, anio, mes, Fecha_ProcesoAS400) Then
                '' '' ''    oProceso.ErrorEnvioInformacionAS400(idProcess, "Server")
                '' '' ''    oEliminacionErrorEnvio.Execute(CustomerNumber, anio, mes, Fecha_ProcesoAS400)
                '' '' ''    AddLogMessage(lstLog, "El número de registros en el PID " + idProcess + " no coincide, procediendo a eliminar.")
                '' '' ''End If

                oEnvioInformacionActualizada = Nothing
            End SyncLock

        Catch ex As Exception
            AddLogMessage(lstLog, "Ocurrio un error en el envió de información a AS/400, PID =" & idProcess)
            AddLogMessage(lstLog, "Descripción del error =" & ex.ToString)
            oEliminacionErrorEnvio.Execute(CustomerNumber, anio, mes, Fecha_ProcesoAS400)
        Finally
            oProceso = Nothing
            oEliminacionErrorEnvio = Nothing
        End Try
    End Sub
#End Region

#Region "Anulación del envió del archivo de descuentos"

    Private Sub AnularEnvioArchivoDescuentos(ByVal PID As String)
        Dim oProceso As New Proceso()
        Dim oEliminacionErrorEnvio As New EliminacionErrorEnvio()

        Dim mes As String = ""
        Dim anio As String = ""
        Dim Fecha_ProcesoAS400 As String = ""
        Dim TipoDocumento As String
        Dim NumeroDocumento As String
        Dim CustomerNumber As String

        Try
            SyncLock Me
                oProceso.getProcesoMesAnio(PID, mes, anio, Fecha_ProcesoAS400, _
                                            TipoDocumento, NumeroDocumento)
                CustomerNumber = oProceso.GetCustomerNumber(TipoDocumento, NumeroDocumento)
                'Realizamos la eliminacion del proceso del envio de descuentos
                'del AS/400 
                oEliminacionErrorEnvio.Execute(CustomerNumber, anio, _
                                        mes, Fecha_ProcesoAS400)
                AddLogMessage(lstLog, "Procesado eliminación en AS/400, PID =" & PID)
                'Anulamos la informacion del proceso del archivo de descuentos dentro 
                'del servidor SQL Server
                oProceso.AnulaProcesoArchivoDescuentos(PID, "Server")
                AddLogMessage(lstLog, "Procesado anulación en SQL Server, PID =" & PID)
            End SyncLock

        Catch ex As Exception
            AddLogMessage(lstLog, "Al anular el envió del PID =" & PID)
            AddLogMessage(lstLog, "Descripción del error =" & ex.ToString)
        Finally
            oProceso = Nothing
            oEliminacionErrorEnvio = Nothing
        End Try
    End Sub

#End Region

    'Clase que contiene las llamadas a las operaciones que deben ser realizadas
    'por el motor Remoting
    Class SyncResource
        Public Sub Access(ByVal oThreadPoolState As ThreadPoolState)
            'SyncLock Me
            Dim form As frmMonitor = oThreadPoolState.form
            Dim submitArgs As SubmitEventArgs = oThreadPoolState.submitArgs

            If submitArgs.Contributor = "ObtenerCronogramaFuturo" Then
                Dim PID As String
                Dim NumeroCliente As String
                Dim mes As String
                Dim anio As String
                Dim Fecha_ProcesoAS400 As String

                'Para que la llamada al evento no resulte en timeout del servidor debemos 
                'realizar esta llamada de manera asincrona
                'SyncLock Me '(GetType(frmMonitor))

                Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)

                PID = CType(submitArgs.Contribution, String).Split("|")(0)
                NumeroCliente = CType(submitArgs.Contribution, String).Split("|")(1)
                mes = CType(submitArgs.Contribution, String).Split("|")(2)
                anio = CType(submitArgs.Contribution, String).Split("|")(3)
                Fecha_ProcesoAS400 = CType(submitArgs.Contribution, String).Split("|")(4)

                SyncLock Me
                    AddLogMessage(form.lstLog, "Iniciando ObtenerCronogramaFuturo para # cliente " & NumeroCliente & ", mes = " & mes & ", año = " & anio & " en " & Now.ToString & "...")
                    Dim MD As New MethodDelegate(AddressOf form.ProcesaImportacionCronogramaFuturo)  '(ModuleId, lstMsgs))
                    Dim RemAr As IAsyncResult = MD.BeginInvoke(PID, NumeroCliente, mes, anio, Fecha_ProcesoAS400, MethodCallback, Nothing)
                End SyncLock
                'End SyncLock

            ElseIf submitArgs.Contributor = "GenerarCronogramaFuturo" Then
                Dim PID As String = ""
                Dim fileFormat As String = ""
                Dim situacionTrabajador As String = ""

                Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)

                PID = submitArgs.Contribution.Split("|")(0)
                fileFormat = submitArgs.Contribution.Split("|")(1)
                situacionTrabajador = submitArgs.Contribution.Split("|")(2)

                AddLogMessage(form.lstLog, "Iniciando GenerarCronogramaFuturo para PID " & PID & "...")
                SyncLock Me '(GetType(frmMonitor))
                    Dim MD As New MethodDelegate2(AddressOf form.GeneraCronogramaFuturo)  '(ModuleId, lstMsgs))
                    Dim RemAr As IAsyncResult = MD.BeginInvoke(pid, fileFormat, situacionTrabajador, MethodCallback, Nothing)
                End SyncLock

            ElseIf submitArgs.Contributor = "EnvioInformacionActualizada" Then
                'SyncLock Me '(GetType(frmMonitor))
                Dim pid As String = submitArgs.Contributor
                AddLogMessage(form.lstLog, "Iniciando EnvioInformacionActualizada para el proceso " & submitArgs.Contribution & "...")
                Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)
                Dim MD As New MethodDelegate3(AddressOf form.EnvioInformacionAS400)  '(ModuleId, lstMsgs))
                Dim RemAr As IAsyncResult = MD.BeginInvoke(submitArgs.Contribution, MethodCallback, Nothing)
                'End SyncLock

            ElseIf submitArgs.Contributor = "AnulaProcesoArchivoDescuentos" Then
                'SyncLock Me '(GetType(frmMonitor))
                'Procedimientos para realizar la anulacion del envio del archivo de descuentos
                Dim pid As String = submitArgs.Contributor
                AddLogMessage(form.lstLog, "Iniciando AnulaProcesoArchivoDescuentos para el proceso " & submitArgs.Contribution & "...")
                Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)
                Dim MD As New MethodDelegate3(AddressOf form.AnularEnvioArchivoDescuentos)  '(ModuleId, lstMsgs))
                Dim RemAr As IAsyncResult = MD.BeginInvoke(submitArgs.Contribution, MethodCallback, Nothing)
                ' End SyncLock
            ElseIf submitArgs.Contributor = "ActualizarTablas" Then
                'REspinoza 20040902 - Se adiciono el procedimiento de actualización de tablas
                AddLogMessage(form.lstLog, "Iniciando ActualizarTablas (ejecución de DTS de proceso de actualización de información de Convenios) en " & Now.ToString & "...")
                SyncLock Me
                    'Para que la llamada al evento no resulte en timeout del servidor debemos 
                    'realizar esta llamada de manera asincrona
                    Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)
                    Dim MD As New MethodDelegateDummy(AddressOf form.ProcesoActualizarTablas)
                    Dim RemAr As IAsyncResult = MD.BeginInvoke(MethodCallback, Nothing)
                End SyncLock
                ' AddLogMessage(form.lstLog, "Finalizando ProcesaActualizacionPropuestas.")

            ElseIf submitArgs.Contributor = "ProcesoBloqueo" Then '20060602 - Procesa informacion de bloqueo
                AddLogMessage(form.lstLog, "Número Lote :" + submitArgs.Contribution)

                SyncLock Me
                    'Para que la llamada al evento no resulte en timeout del servidor debemos 
                    'realizar esta llamada de manera asincrona
                    Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)
                    Dim MD As New MethodDelegate3(AddressOf form.ProcesoBloqueo)
                    Dim RemAr As IAsyncResult = MD.BeginInvoke(submitArgs.Contribution, MethodCallback, Nothing)
                End SyncLock
            ElseIf submitArgs.Contributor = "ProcesoProrroga" Then '20060602 - Procesa informacion de bloqueo
                AddLogMessage(form.lstLog, "Número Lote :" + submitArgs.Contribution)

                SyncLock Me
                    'Para que la llamada al evento no resulte en timeout del servidor debemos 
                    'realizar esta llamada de manera asincrona
                    Dim MethodCallback As New AsyncCallback(AddressOf form.RemoteAsyncCallBack)
                    Dim MD As New MethodDelegate3(AddressOf form.ProcesoProrroga)
                    Dim RemAr As IAsyncResult = MD.BeginInvoke(submitArgs.Contribution, MethodCallback, Nothing)
                End SyncLock


            End If
            AddLogMessage(form.lstLog, "Finalizando " + submitArgs.Contributor)

        End Sub

    End Class

#Region "procesar la informacion del bloqueo"
    'Procesar la informacion de bloqueo en el AS/400
    Private Sub ProcesoBloqueo(ByVal numeroLote As String)
        Try
            Dim str As String
            str = TCPClient.SendReceive(AppSettings("ipJavaApp"), AppSettings("portJavaApp"), "Bloqueo:" + numeroLote)
            Bloqueo.procesaConciliacionBloqueo(numeroLote, str)
        Catch e As Exception
            Bloqueo.UpdateLoteBloqueo2(numeroLote, e.ToString)
            AddLogMessage(Me.lstLog, e.ToString)
        End Try
    End Sub


    Private Sub ProcesoProrroga(ByVal numeroLote As String)
        Try
            Dim str As String
            str = TCPClient.SendReceive(AppSettings("ipJavaApp"), AppSettings("portJavaApp"), "Prorroga:" + numeroLote)
            Prorroga.procesaConciliacionProrroga(numeroLote, str)
        Catch e As Exception
            Prorroga.UpdateLoteProrroga2(numeroLote, e.ToString)
            AddLogMessage(Me.lstLog, e.ToString)
        End Try

    End Sub
#End Region
End Class