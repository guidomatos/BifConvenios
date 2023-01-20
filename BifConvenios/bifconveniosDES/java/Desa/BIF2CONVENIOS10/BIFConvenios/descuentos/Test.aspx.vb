Imports System.Data
Imports System.Data.SqlClient
Imports BIFConvenios
Partial Class Test
    Inherits System.Web.UI.Page
    Protected ds As New DataSetCuotasPendientes()
    Protected orepCartaCobranzaLight As New repCartaCobranzaLight()

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
        Dim dsSign As DataSet
        ds = Seguimiento.GetDetailPagaresJoin("'233100163427', '233100165830', '233100126434', '233100144004', '233100113086', '233100146264', '233100123304', '233100192381', '233100127392', '233100163392'", "9", "2007")

        Dim proceso As String = "9661F4A3-D2D5-42DE-8FE6-614E741ED181"
        dsSign = DocumentoCobranza.getInformacionFirmantes(context.User.Identity.Name, Proceso)
        Dim dr As DataRow
        For Each dr In ds.Tables("DatosDescuentoHeader").Rows
            If dsSign.Tables(0).Rows.Count = 2 Then
                dr("SIGN1NAME") = dsSign.Tables(0).Rows(0).Item("EjecutivoNombre")      ' ""
                dr("SIGN2NAME") = dsSign.Tables(0).Rows(1).Item("EjecutivoNombre")
                dr("SIGN1POSITION") = dsSign.Tables(0).Rows(0).Item("EjecutivoCargo")
                dr("SIGN2POSITION") = dsSign.Tables(0).Rows(1).Item("EjecutivoCargo")

                dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(0).Item("EjecutivoImagePath"))  ' 
                dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(1).Item("EjecutivoImagePath"))  ' 

            Else
                dr("SIGN1NAME") = "" ' ""
                dr("SIGN2NAME") = ""  ' ""
                dr("SIGN1POSITION") = ""  ' ""
                dr("SIGN2POSITION") = ""  ' ""
                dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID0.bmp")   ' 
                dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID1.bmp")  ' 
            End If
        Next

        orepCartaCobranzaLight.SetDataSource(ds)
        CrystalReportViewer1.ReportSource = orepCartaCobranzaLight

    End Sub

End Class
