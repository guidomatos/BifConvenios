Imports system.data.sqlclient
Imports System.Configuration.ConfigurationSettings

Namespace BIFConvenios

    Partial Class EnvioReporteMail
        Inherits System.Web.UI.Page
        Protected archivo As String = ""

        Protected strEmailCliente As String
        Protected PID As String = ""
        Protected oCliente As New BIFConvenios.Cliente()
        Protected oProceso As New Proceso()

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

            If Not Request.Params("idP") Is Nothing And Not Request.Params("Archivo") Is Nothing Then
                PID = CType(Request.Params("idP"), String)
                archivo = CType(Request.Params("Archivo"), String)
            End If

            If Not Page.IsPostBack Then
                If Not Request.Params("idP") Is Nothing And Not Request.Params("Archivo") Is Nothing Then
                    Dim Anio_periodo As String
                    Dim Mes_Periodo As String
                    Dim dr As SqlDataReader = oCliente.GetEmails(CType(Request.Params("idP"), String))
                    If dr.Read Then
                        lblCliente.Text = CType(dr("Nombre_Cliente"), String)
                        strEmailCliente = CType(dr("CorreoElectronico"), String)
                        Anio_periodo = CType(dr("Anio_periodo"), String)
                        Mes_Periodo = CType(dr("Mes_Periodo"), String)
                    End If

                    txtComentario.Text = CType(AppSettings("mailCFBody"), String).Replace("#1", Periodo.GetMonthByNumber(Mes_Periodo)).Replace("#2", Anio_periodo)

                    Dim ar As New ArrayList()

                    Dim str As Object
                    For Each str In strEmailCliente.Split(";")
                        ar.Add(New MailSource(str))
                    Next

                    dgGen.DataSource = ar
                    dgGen.DataBind()
                End If
            End If
        End Sub

        'Clase para funcionar como container para el origen de datos del DG
        Protected Class MailSource
            Private mmail As String

            Sub New(ByVal mail As String)
                mmail = mail
            End Sub

            Property Mail() As String
                Get
                    Return mmail
                End Get
                Set(ByVal Value As String)
                    mmail = Value
                End Set
            End Property
        End Class

        Private Sub lnkEnviarEmail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lnkEnviarEmail.Click
            Dim strMails As String
            'Si no este TEST hacemos el envio regular
            If CType(AppSettings("testOnly"), String).Trim = "0" Then
                strMails = GetCheckedMails()
            Else    ' en otro caso enviamos el correo a una direccion de prueba
                strMails = CType(AppSettings("mailBcc"), String)
            End If

            Dim strNombreArchivoProceso As String
            strNombreArchivoProceso = Server.MapPath("../") & "\" & archivo
            'CType(AppSettings("GenFolder"), String)(+ _
            '                           oProceso.GetNombreArchivoProceso(PID) + "." + archivo)
            'strMails, _
            '"respinoza@bif.com.pe", _
            'TODO: colocar el cuerpo del correo electronico con los atachados
            Utils.SendNotification(CType(AppSettings("mailSender"), String), strMails, _
                                    CType(AppSettings("mailBcc"), String), _
                                    CType(AppSettings("mailCFSubject"), String), _
                                    txtComentario.Text, strNombreArchivoProceso, notifyTo:=CType(AppSettings("mailSender"), String))

            oProceso.UpdateFechaObtencionArchivo(PID, True, context.User.Identity.Name)
            pnlClose.Visible = True
        End Sub
        'Obtiene la lista de correos electronicos que han sido seleccionados
        Private Function GetCheckedMails() As String
            Dim chk As CheckBox
            Dim strMails As String = ""
            Dim i As Integer

            For i = 0 To dgGen.Items.Count - 1
                chk = dgGen.Items(i).FindControl("chk")
                If chk.Checked Then
                    strMails = strMails & dgGen.Items(i).Cells(1).Text + ","
                End If
            Next

            Return strMails
        End Function
        'Obtenemos los nombres de los controles que se generan para verificar si
        'estan checkados en el lado del cliente o no
        Protected Function GetControlNames()
            Dim chk As CheckBox
            Dim strNames As String = ""
            Dim i As Integer

            For i = 0 To dgGen.Items.Count - 1
                chk = dgGen.Items(i).FindControl("chk")
                strNames = strNames + chk.ClientID + ","
            Next
            If strNames.Trim <> "" Then
                strNames = strNames.Substring(0, strNames.Length - 1)
            End If

            Return strNames
        End Function
    End Class
End Namespace
