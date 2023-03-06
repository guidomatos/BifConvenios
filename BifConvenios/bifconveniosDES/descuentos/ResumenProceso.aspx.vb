Imports System.Configuration.ConfigurationSettings
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Web.UI


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
'*****************pase - copiar - Ticket 59416*************************
Imports BIFConvenios.Logica
'**********************************************************************
'****************************************************

Namespace BIFConvenios

    Partial Class ResumenProceso
        Inherits System.Web.UI.Page
        Protected oproc As New Proceso()
        Protected oprocesoBL As New BIFConvenios.Logica.ProcesoBL
        Protected Pid As String = ""
        Protected objWSConvenios As New wsBIFConvenios.WSBIFConveniosClient

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
                objWSConvenios.EnviaInformacionIBS(idProcess, Context.User.Identity.Name)


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
            Dim flag As String = " "
            ActivarPagoIBSOnline("L", codigo_clienteIBS.PadLeft(9, "0"), fecha, flag)
            If (flag = "1") Then
                oproc.UpdateEstadoProceso(idProcess, "G1", Context.User.Identity.Name) 'LM1
                '*****************pase - copiar - Ticket 59416*************************
                oprocesoBL.ActualizaFlagEstadoCargaAutomatica("L", 0, Pid)

                '**********************************************************************
                ScriptManager.RegisterStartupScript(Me.Page, Me.GetType(), "Alerta", "MsgAlerta();", True)

                Exit Sub
            Else
                pnlControl.Visible = False
                pnlMensaje.Visible = True
                lblMensaje.Text = "Ejecutando el envi� de cuotas a IBS... espere unos momentos."
                hdIdEnvio.Value = ""
            End If
            oproc.ActualizaFlagCargaAutomatica("L", 0)
        End Sub

        Public Function ActivarPagoIBSOnline(ByVal strTipoOperacion As String, _
                                          ByVal strCodigoClienteIBS As String, _
                                          ByVal strFecha As String, _
										  ByRef strFlag As String) As Integer

            Dim myConnection As New ADODB.Connection()
            Dim result As New ADODB.Recordset()
            Dim strResultado As Integer
            Dim oAdapter As New OleDbDataAdapter()
            'Dim oCmd As New OleDbCommand()
            Dim oCmd As New ADODB.Command()

            Dim oDs As New DataSet()
            Dim strSQL As String = ""
            Dim strComando As String = ConfigurationManager.AppSettings("ProgramaPagoOnline2")
            'Dim strComando As String = ConfigurationManager.AppSettings("ProgramaPagoOnline")
            Dim returnValue As String = ""
            Dim s As String = ""


            Try
                myConnection.CursorLocation = CursorLocationEnum.adUseClient

                's = ConfigurationManager.AppSettings("AS400-ConnectionString-Convenios")
                s = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios")
                myConnection.Open(s)

                strComando = strComando.Replace("TipoOperacion", strTipoOperacion)
                strComando = strComando.Replace("CodigoClienteIBS", strCodigoClienteIBS)
                strComando = strComando.Replace("Fecha", strFecha)
                strComando = strComando.Replace("Flag", "?")
                oCmd.CommandText = strComando '"{CALL IBSUSRLIB.SP_VALSOBREGIRO_CONVENIO(?,?,?,?)}"

                oCmd.CommandType = CommandType.Text
                oCmd.ActiveConnection = myConnection

                oCmd.Parameters.Append(oCmd.CreateParameter("P1", DataTypeEnum.adChar, ParameterDirectionEnum.adParamInput, 1, strTipoOperacion))
                oCmd.Parameters.Append(oCmd.CreateParameter("P2", DataTypeEnum.adChar, ParameterDirectionEnum.adParamInput, 9, strCodigoClienteIBS))
                oCmd.Parameters.Append(oCmd.CreateParameter("P3", DataTypeEnum.adChar, ParameterDirectionEnum.adParamInput, 6, strFecha))
                oCmd.Parameters.Append(oCmd.CreateParameter("P4", DataTypeEnum.adInteger, ParameterDirectionEnum.adParamInputOutput, 1, strResultado))

                oCmd.Execute()
                'result = oCmd.Execute()

                strResultado = oCmd.Parameters(3).Value.ToString()
                'result = myConnection.Execute(strComando)
                'strResultado = myConnection.Execute(strComando)
                strFlag = strResultado

                result.ActiveConnection = Nothing
                myConnection.Close()
                myConnection = Nothing

            Catch ex As Exception
                Throw ex
            End Try
        End Function


    End Class
End Namespace
