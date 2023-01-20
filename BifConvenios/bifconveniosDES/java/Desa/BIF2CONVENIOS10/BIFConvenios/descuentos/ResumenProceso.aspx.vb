Imports System.Configuration.ConfigurationSettings
Imports System.Data.SqlClient
Imports System.Data.OleDb


'************** rhj**************************************
' Reemplazo del Remoting
Imports System.Data
'Imports System.Data.SqlClient
Imports ADODB
Imports System.Configuration
Imports System.Collections.Generic
Imports SolpartWebControls
Imports BIFData
Imports BusinessLogic
Imports DTS
Imports LREM
Imports Microsoft
Imports System
'****************************************************

Namespace BIFConvenios

    Partial Class ResumenProceso
        Inherits System.Web.UI.Page
        Protected oproc As New Proceso()
        Protected Pid As String = ""


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
            If Not Request.Params("id") Is Nothing Then
                Dim dr As SqlDataReader

                Pid = CType(Request.Params("id"), String)
                dr = oproc.InformeProceso(Pid)
                If dr.Read Then
                    ltrlCliente.Text = CType(dr("Nombre_Cliente"), String)
                    ltrlDocumento.Text = CType(dr("TipoDocumento"), String) + " - " + CType(dr("NumeroDocumento"), String)
                    ltrlEstado.Text = CType(dr("CodigoNombre"), String)
                    ltrlPeriodo.Text = MonthName(CType(dr("Mes_Periodo"), Integer)) + " " + dr("Anio_periodo")
                End If

                Dim dr2 As SqlDataReader
                dr2 = oproc.GetResumenProceso(Pid)

                dgInformacionResumen.DataSource = dr2
                dgInformacionResumen.DataBind()


                dr2 = oproc.GetResumenProceso(Pid)
                dr2.NextResult()



                If dr2.Read Then
                    lblTotal.Text = CType(dr2("TotalRegistros"), String)
                End If
            End If
        End Sub

        Private Sub lnkEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviar.Click
            '*********************************************************************
            '--------Usado en la llamada remoting
            'Dim objSender As BroadcasterClass.GOIntranet.SubmitSuscription
            Dim idProcess As String
            'Dim objEventSink As BroadcasterClass.GOIntranet.EventSink
            'Dim ComputerName As String = System.Configuration.ConfigurationSettings.AppSettings("RemotingServer")
            'Dim serverUriSubmition As String
            'Dim serverUriSink As String
            'Dim args As Object() = {}
            ''------fin de variables 
            ''Llamada remoting para procesar el requerimiento de carga de informacion de cronograma futuro al servidor
            Dim codigo_clienteIBS As String = ""
            Dim fecha As String = ""
            Try
                '    'Desde este punto realizaremos la llamada al procedimiento remoto para enviar la informacion de los descuentos 
                '    'realizados al AS/400
                idProcess = hdIdEnvio.Value
                '    'Realizamos la llamada remoting

                'serverUriSubmition = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingSubmition"
                'serverUriSink = "tcp://" & ComputerName & ":" + AppSettings("ipPort") + "/BIFRemotingEventSink"

                'objSender = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.SubmitSuscription), serverUriSubmition), BroadcasterClass.GOIntranet.SubmitSuscription)
                'objEventSink = CType(Activator.GetObject(GetType(BroadcasterClass.GOIntranet.EventSink), serverUriSink), BroadcasterClass.GOIntranet.EventSink)

                'AddHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver

                ''    ''objSender.Submit(idProcess, "EnvioInformacionActualizada")
                'objSender.Submit(idProcess, "EnvioInformacionActualizada")


                'RemoveHandler objSender.Submision, AddressOf objEventSink.SubmissionReceiver
                oproc.ActualizaFlagCargaAutomatica("L", 1)
                Dim objWSConvenios As New wsConvenios.WSBIFConvenios

                objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
                objWSConvenios.EnviaInformacionIBS(idProcess, Context.User.Identity.Name)

                pnlControl.Visible = False
                pnlMensaje.Visible = True
                lblMensaje.Text = "Ejecutando el envió de cuotas a IBS... espere unos momentos."
                hdIdEnvio.Value = ""
            Catch excp As Exception  'TODO: Enviar un mensaje si ocurre un error
                oproc.ActualizaFlagCargaAutomatica("L", 0)
                Response.Write(excp.ToString)
                lblMensajeError.Text = Utils.HandleError(excp)
            End Try
            'se obtiene el codigo de proceso
            Dim dr As SqlDataReader
            Pid = CType(Request.Params("id"), String)
            dr = oproc.getDatosPagosIBSOnline(Pid)
            If dr.Read Then
                codigo_clienteIBS = CType(dr("codigo_IBS"), String)
                fecha = CType(dr("FECHA"), String)
            End If

            ' se invoca al programa que graba los pagos en IBS
            ActivarPagoIBSOnline("L", codigo_clienteIBS.PadLeft(9, "0"), fecha)
            oproc.ActualizaFlagCargaAutomatica("L", 0)
        End Sub

        Public Function ActivarPagoIBSOnline(ByVal strTipoOperacion As String, _
                                          ByVal strCodigoClienteIBS As String, _
                                          ByVal strFecha As String) As Integer

            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim oAdapter As New OleDbDataAdapter()
            Dim oDs As New DataSet()
            Dim strSQL As String = ""
            Dim strComando As String = ConfigurationManager.AppSettings("ProgramaPagoOnline")
            Dim returnValue As String = ""
            Dim s As String = ""

            Try
                myConnection.CursorLocation = CursorLocationEnum.adUseClient

                s = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
                myConnection.Open(s)

                strComando = strComando.Replace("TipoOperacion", strTipoOperacion)
                strComando = strComando.Replace("CodigoClienteIBS", strCodigoClienteIBS)
                strComando = strComando.Replace("Fecha", strFecha)

                result = myConnection.Execute(strComando)

                result.ActiveConnection = Nothing
                myConnection.Close()
                myConnection = Nothing

            Catch ex As Exception
                Throw ex
            End Try
        End Function


    End Class
End Namespace
