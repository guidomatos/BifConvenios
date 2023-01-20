Imports System.Configuration.ConfigurationSettings

Namespace BIFConvenios
    Partial Class ActualizarTablas
        Inherits System.Web.UI.Page

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
            'Put user code to initialize the page here
        End Sub

        Private Sub lnkEnviarPropuestas_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviarPropuestas.Click
            If Page.IsValid Then
                If Not Utils.TestServer Then 'Verificamos que el servidor esta disponible
                    lblMensaje.Text = Utils.SERVER_UNAVAILABLE
                    Exit Sub
                End If

                '--------Usado en el envio de informacion para el proceso
                Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
                Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
                Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
                Dim serverUriSubmition As String
                Dim serverUriSink As String
                Dim args As Object() = {}
                '------fin de variables usadas en la validacion
                Try
                    serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                    serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                    objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                    objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                    AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                    objSender.Submit("", "ActualizarTablas")

                    RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                    lblMensaje.Text = "Se ha iniciado el proceso ACTUALIZACION DE INFORMACION DE TABLAS sin problemas."
                    lblActualizacionPropuestas.Visible = False
                Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                    lblMensaje.Text = Utils.HandleError(excp)
                End Try
            End If
        End Sub
    End Class
End Namespace
