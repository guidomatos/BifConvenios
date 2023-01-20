Imports System.Configuration.ConfigurationSettings

Namespace BIFConvenios
    Partial Class reporteProcesoDescuento
        Inherits System.Web.UI.Page
        Protected oProc As New Proceso()
        Protected idProcess As String = ""
#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
        Private Sub ddlMes_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlMes.SelectedIndexChanged
            Call BindGrid()
        End Sub

        Private Sub ddlAnio_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlAnio.SelectedIndexChanged
            Call GetMonths()
        End Sub

        Protected Function MostrarEnlace(ByVal codigo_proceso As Guid, ByVal b As Boolean, _
                        ByVal nombrecliente As String, ByVal anio As String, ByVal mes As String, ByVal MensajeEnvio As Object, ByVal Fecha_ProcesoAS400 As String) As String
            Dim returnValue As String = ""
            If b Then
                'returnValue = "<a href=""JavaScript:Procesar('" & codigo_proceso.ToString & "', '" & nombrecliente & "','" & anio & "','" & mes & "');"">Enviar información a AS/400</a>"
                returnValue = "<a href=""JavaScript:Procesar('" & codigo_proceso.ToString & "', '" & nombrecliente.Replace("'", "") & "','" & anio & "','" & mes & "','" + BIFConvenios.Utils.GetFechaCanonica(Fecha_ProcesoAS400) + _
                                "');"">" & MensajeEnvio & "</a>"
            Else

                returnValue = IIf(IsDBNull(MensajeEnvio), "", MensajeEnvio)
            End If
            Return returnValue
        End Function

        ' Funcion que devuelve la cadena conteniendo el enlace para realizar la anulacion del proceso
        ' del archivo de descuentos
        Protected Function MostrarEnlaceAnulacionProceso(ByVal codigo_proceso As Guid, ByVal CanProcess As Boolean, _
                ByVal nombrecliente As String, ByVal anio As String, ByVal mes As String, ByVal Fecha_ProcesoAS400 As String)
            Dim returnValue As String = ""
            If CanProcess Then
                returnValue = "<a href=""JavaScript:AnularProceso('" & codigo_proceso.ToString & "', '" & _
                                nombrecliente.Replace("'", "") & "','" & anio & "','" & mes & "','" + BIFConvenios.Utils.GetFechaCanonica(Fecha_ProcesoAS400) + _
                                "');"">Anular proceso de Archivo de cuotas</a>"
            Else
                returnValue = ""
            End If
            Return returnValue
        End Function


        Private Sub lnkEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviar.Click

            '--------Usado en la llamada remoting
            Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
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

                serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

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

        Private Sub lnkAnularProceso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkAnularProceso.Click
            '--------Usado en la llamada remoting
            'Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            'Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            'Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
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

                Dim objWSConvenios As New WSConvenios.WSBIFConvenios
                objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
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