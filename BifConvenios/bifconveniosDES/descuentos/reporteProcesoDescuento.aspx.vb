Namespace BIFConvenios
    Partial Class reporteProcesoDescuento
        Inherits Page
        Protected oProc As New Proceso()
        Protected idProcess As String = ""
        Protected objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            If Not Page.IsPostBack Then
                '-------Informacion del año
                ddlAnio.DataSource = oProc.GetAniosProcesoDescuentosCompletado()
                ddlAnio.DataBind()
                ddlAnio.Items.Insert(0, New ListItem("-- Todos los años --", ""))
                ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

                'Informacion del mes
                Call GetMonths()

                Call BindGrid()
            End If
        End Sub


        'Enlaza la informacion a datos
        Private Sub BindGrid()
            dgDatos.DataSource = oProc.GetProcesosDescuentoCompletado(ddlAnio.SelectedItem.Value, ddlMes.SelectedItem.Value)
            dgDatos.DataBind()
            lblNumReg.Text = dgDatos.Items.Count.ToString
            '            If dgDatos.Items.Count = 0 Then
            'TODO: Codigo para hacer invisible las dos ultimas columnas cuando no hay datos
            '           End If
        End Sub

        Private Sub GetMonths()
            ddlMes.DataSource = Periodo.GetMonthsByReader(oProc.GetMesesProcesoDescuentosCompletado(ddlAnio.SelectedItem.Value))
            ddlMes.DataBind()
            ddlMes.Items.Insert(0, New ListItem("-- Todos los meses --", ""))
            ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))
        End Sub
        Private Sub ddlMes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMes.SelectedIndexChanged
            Call BindGrid()
        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAnio.SelectedIndexChanged
            Call GetMonths()
        End Sub

        Protected Function MostrarEnlace(codigo_proceso As Guid, b As Boolean,
                        nombrecliente As String, anio As String, mes As String, MensajeEnvio As Object, Fecha_ProcesoAS400 As String) As String
            Dim returnValue As String = ""
            If b Then
                'returnValue = "<a href=""JavaScript:Procesar('" & codigo_proceso.ToString & "', '" & nombrecliente & "','" & anio & "','" & mes & "');"">Enviar información a AS/400</a>"
                returnValue = "<a href=""JavaScript:Procesar('" & codigo_proceso.ToString & "', '" & nombrecliente.Replace("'", "") & "','" & anio & "','" & mes & "','" + BIFConvenios.Utils.GetFechaCanonica(Fecha_ProcesoAS400) +
                                "');"">" & MensajeEnvio & "</a>"
            Else

                returnValue = IIf(IsDBNull(MensajeEnvio), "", MensajeEnvio)
            End If
            Return returnValue
        End Function

        ' Funcion que devuelve la cadena conteniendo el enlace para realizar la anulacion del proceso
        ' del archivo de descuentos
        Protected Function MostrarEnlaceAnulacionProceso(codigo_proceso As Guid, CanProcess As Boolean,
                nombrecliente As String, anio As String, mes As String, Fecha_ProcesoAS400 As String)
            Dim returnValue As String

            If CanProcess Then
                returnValue = "<a href=""JavaScript:AnularProceso('" & codigo_proceso.ToString & "', '" &
                                nombrecliente.Replace("'", "") & "','" & anio & "','" & mes & "','" + Utils.GetFechaCanonica(Fecha_ProcesoAS400) +
                                "');"">Anular proceso de Archivo de cuotas</a>"
            Else
                returnValue = ""
            End If
            Return returnValue
        End Function


        Private Sub lnkEnviar_Click(sender As Object, e As EventArgs) Handles lnkEnviar.Click

            '--------Usado en la llamada remoting
            Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            Dim ComputerName As String = ConfigurationManager.AppSettings("RemotingServer")
            Dim serverUriSubmition As String
            Dim serverUriSink As String
            Dim args As Object() = {}
            '------fin de variables 
            'Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
            Try
                'Desde este punto realizaremos la llamada al procedimiento remoto para enviar la informacion de los descuentos 
                'realizados al AS/400
                idProcess = hdIdEnvio.Value
                'Realizamos la llamada remoting

                serverUriSubmition = "tcp://" & ComputerName & ":" + ConfigurationManager.AppSettings("ipPort") + "/BIFRemotingSubmition"
                serverUriSink = "tcp://" & ComputerName & ":" + ConfigurationManager.AppSettings("ipPort") + "/BIFRemotingEventSink"

                objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                objSender.Submit(idProcess, "EnvioInformacionActualizada")

                RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                pnlControl.Visible = False
                pnlMensaje.Visible = True
                lblMensaje.Text = "Ejecutando el envio de información a IBS... espere unos momentos."
                hdIdEnvio.Value = ""
            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                'Response.Write(excp.ToString)
                lblMensajeError.Text = Utils.HandleError(excp)
            End Try

        End Sub

        Private Sub lnkAnularProceso_Click(sender As Object, e As EventArgs) Handles lnkAnularProceso.Click
            '--------Usado en la llamada remoting
            'Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            'Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            'Dim ComputerName As String = Configuration.ConfigurationSettings.AppSettings("RemotingServer")
            'Dim serverUriSubmition As String
            'Dim serverUriSink As String
            'Dim args As Object() = {}
            '------fin de variables 
            'Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
            Try
                'Desde este punto realizaremos la llamada al procedimiento remoto para enviar la informacion de los descuentos 
                'realizados al AS/400

                idProcess = hdIdAnulacionProceso.Value
                'Realizamos la llamada remoting

                'serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                'serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                'objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                'objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                'AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                'objSender.Submit(idProcess, "AnulaProcesoArchivoDescuentos")

                'RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                objWSConvenios.AnulaEnvioCobranzaIBS(idProcess, Context.User.Identity.Name)


                pnlControl.Visible = False
                pnlAnulacionProcesoDescuentos.Visible = True
                lblMensajeAnulacion.Text = "Anulación del proceso de archivo de cuotas... espere un momento."
                hdIdAnulacionProceso.Value = ""

            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                'Response.Write(excp.ToString)
                lblMensajeError.Text = Utils.HandleError(excp)
            End Try
        End Sub
    End Class
End Namespace