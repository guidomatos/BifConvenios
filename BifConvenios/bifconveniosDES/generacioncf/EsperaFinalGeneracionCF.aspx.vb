Imports System.Data.SqlClient

Namespace BIFConvenios

    Partial Class EsperaFinalGeneracionCF
        Inherits Page
        Protected oProceso As New Proceso()
        Protected Pid As String = ""
        Protected cantidad As String = ""
        Protected montosoles As String = ""
        Protected montodolares As String = ""
        Protected tipoNomina = "E"
        Protected indicadorFormato = "A"
        Protected tipoProceso = "O"
        Protected objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient

#Region " Web Form Designer Generated Code "

        'This call is required by the Web Form Designer.
        <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

        End Sub

        Private Sub Page_Init(sender As Object, e As EventArgs) Handles MyBase.Init
            'CODEGEN: This method call is required by the Web Form Designer
            'Do not modify it using the code editor.
            InitializeComponent()
        End Sub

#End Region

        Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
            'Put user code to initialize the page here
            If Request.Params("id") IsNot Nothing Then
                Pid = Request.Params("id").ToString()
                cantidad = CType(IIf(Request.Params("cantidad") Is Nothing, "", Request.Params("cantidad")), String)
                montosoles = CType(IIf(Request.Params("montosoles") Is Nothing, "", Request.Params("montosoles")), String)
                montodolares = CType(IIf(Request.Params("montodolares") Is Nothing, "", Request.Params("montodolares")), String)

                If cantidad.Equals("") And montosoles.Equals("") And montodolares.Equals("") Then
                    Dim dr As SqlDataReader = oProceso.obtenerResumenClienteCuota(Pid)
                    If dr.Read Then
                        cantidad = CType(dr("cantidad"), String)
                        montosoles = CType(dr("montosoles"), String)
                        montodolares = CType(dr("montodolares"), String)
                    End If
                End If

                If oProceso.EnviarMensajeGeneracionArchivo(Pid.Split("|")(0)) Then

                    Try
                        objWSConvenios.GeneraCronogramaFuturo(Pid.Split("|")(0), Pid.Split("|")(1), Pid.Split("|")(2), Context.User.Identity.Name)
                    Catch ex As Exception
                        Call MostrarMensajeGeneral(True, Utils.HandleError(ex))
                        Exit Sub
                    End Try

                    ''Realizamos la llamada al proceso remoting para poder generar el archivo 
                    ''--------Usado en la llamada remoting
                    'Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
                    'Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
                    'Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
                    'Dim serverUriSubmition As String
                    'Dim serverUriSink As String
                    'Dim args As Object() = {}
                    ''------fin de variables 
                    ''Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
                    'Try
                    '    serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                    '    serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                    '    objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                    '    objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                    '    AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                    '    objSender.Submit(Pid, "GenerarCronogramaFuturo")

                    '    RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                    '    'Response.Redirect("cargasprop.aspx")
                    'Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                    '    Call MostrarMensajeGeneral(True, Utils.HandleError(excp))
                    '    Exit Sub
                    'End Try
                End If
                'Esperamos el final de la generacion del archivo
                If oProceso.GetFinalGeneracionArchivo(Pid, Context.User.Identity.Name) Then
                    'pnlFinal.Visible = True
                    oProceso.InsertaNominaEntradaSalida(Pid, cantidad, montosoles, montodolares, tipoNomina, indicadorFormato, tipoProceso, Context.User.Identity.Name)
                    Response.Redirect(ResolveUrl("/generacioncf/AccesoArchivoGenerado.aspx?id=" + Pid))
                Else
                    pnlSwf.Visible = True
                End If
            End If
        End Sub

        'Rutina para mostrar un error generico
        Private Sub MostrarMensajeGeneral(b As Boolean, str As String)
            pnlMensaje.Visible = b
            pnlFinal.Visible = Not b
            pnlSwf.Visible = Not b
            lblMensaje.Text = str
        End Sub


    End Class
End Namespace
