Imports BIFConvenios
Partial Class selectFileFormat
    Inherits Page

    Protected idP As String
    Protected nombre As String
    Protected anio As String
    Protected mes As String
    Protected fechaProcesoAS400 As String
    Protected oProceso As New Proceso()
    Protected formatoArchivo As String = ""
    Protected situacionTrabajador As String = ""
    Protected strTipoCliente As String = ""

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    Private Sub Page_Init(ByVal sender As Object, e As EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    Private Sub Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Put user code to initialize the page here
        'id='+ id +"&nombre=" + nombre + "&anio=" + anio + "&mes=" + mes + "&fechaProcesoAS400=" 
        If Not Page.IsPostBack Then
            idP = Request.Params("id")
            nombre = Request.Params("nombre")
            anio = Request.Params("anio")
            mes = Request.Params("mes")
            fechaProcesoAS400 = Request.Params("fechaProcesoAS400")

            'ADD JCHAVEZH 21/10/2014
            'lstModalidad.DataSource = oProceso.GetModalidad(idP)
            'lstModalidad.DataBind()
            'lstModalidad.Items.Insert(0, New ListItem("Todos", "-"))
            'END JCHAVEZH 21/10/2014

            Dim dr As SqlClient.SqlDataReader = Proceso.getFormatoExportacion(idP)

            Dim s As String

            Dim i As Integer = 0

            While dr.Read
                s = dr.Item("FormatoArchivo")
                Dim TipoFormatoArchivo = dr.Item("TipoFormatoArchivo")
                Dim DescripcionFormatoArchivo As String = dr.Item("DescripcionFormatoArchivo")
                'End If
                If i = 0 And s.Trim <> "default" Then
                    lstFormatFile.Items.Clear()
                End If

                If s <> "default" Then

                    If TipoFormatoArchivo.trim = "both" Then
                        lstFormatFile.Items.Add(New ListItem(s + " - " + "txt", "txt"))
                        lstFormatFile.Items.Add(New ListItem(s + " - " + "csv", "csv"))

                    ElseIf TipoFormatoArchivo.trim = "txt" Then
                        lstFormatFile.Items.Add(New ListItem(s + " - " + "txt", "txt"))
                    ElseIf TipoFormatoArchivo.trim = "xls" Then
                        lstFormatFile.Items.Add(New ListItem(s + " - " + "xls", "xls"))
                    ElseIf TipoFormatoArchivo.trim = "csv" Then
                        lstFormatFile.Items.Add(New ListItem(s + " - " + "csv", "csv"))
                        'ElseIf TipoFormatoArchivo.trim = "SANFERNAND" Then
                        '    lstFormatFile.Items.Add(New ListItem(s + " - " + "SANFERNAND", "SANFERNAND"))
                    Else
                        ''lstFormatFile.Items.Add(New ListItem("Delimitado por Comas(.csv)", "csv2"))
                        lstFormatFile.Items.Add(New ListItem(DescripcionFormatoArchivo, s + "=" + TipoFormatoArchivo))
                    End If

                    'lstFormatFile.Items.Add(New ListItem("Formato Estándar - MS Excel (.xls)", "defaultXls"))
                End If
                i += 1
            End While

            If lstFormatFile.Items.Count > 0 Then
                lstFormatFile.Items.Add(New ListItem("Formato Estándar - MS Excel (.xls)", "defaultXls"))
            End If

            Dim count As Integer = lstFormatFile.Items.Count - 1
            lstFormatFile.Items(count).Selected = True

            'lstFormatFile.Attributes.Add("onChange", "ShowModalidad()")

            'End If
            ' Me.cargarDatosMail()
            'If lstFormatFile.SelectedValue = "defaultXls" Then
            '    lstModalidad.Visible = True
            '    lblModalidad.Visible = True
            'Else
            '    lstModalidad.Visible = False
            '    lblModalidad.Visible = False
            'End If

        End If
    End Sub


End Class
