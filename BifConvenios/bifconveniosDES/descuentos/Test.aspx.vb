Imports System.Data
Imports System.Data.SqlClient
Imports BIFConvenios
Imports Microsoft.Reporting.WebForms

Partial Class Test
    Inherits System.Web.UI.Page
    Protected ds As New DataSetCuotasPendientes()
    'Protected orepCartaCobranzaLight As New repCartaCobranzaLight()

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

            'Put user code to initialize the page here
            Dim dsSign As DataSet
            Dim DS As DataSet
            Dim rdsDataset As New ReportDataSource()


            'provisional
            'ds = Seguimiento.GetDetailPagaresJoin("'233100163427', '233100165830', '233100126434', '233100144004', '233100113086', '233100146264', '233100123304', '233100192381', '233100127392', '233100163392'", "9", "2007")
            If Request.Params("rep") Is Nothing Then
                rdsDataset.Name = "DataSet1"
                DS = GetTableRepCartaCobranzaLigth()
                ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLight.rdlc"
            Else
                Dim idp As String = Request.Params("rep")

                Select Case idp
                    Case "repCartaCobranzaLight"
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranzaLigth()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLight.rdlc"
                    Case "repCartaCobranza"
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranza()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranza.rdlc"
                    Case "repCartaCobranzaLightLluvia"
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranzaLigthLluvia()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLightLluvia.rdlc"
                    Case "repCartaCobranzaLightLluvia2"
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranzaLigthLluvia()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLightLluvia2.rdlc"
                    Case "repCartaCobranzaLluvia"
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranzaLigthLluvia()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLluvia.rdlc"
                    Case "repCartaNotarial"
                        rdsDataset.Name = "DatosCobranza"
                        DS = GetTableRepCartaNotarial()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaNotarial.rdlc"
                    Case "repNotaCobranza"
                        rdsDataset.Name = "DatosCobranza"
                        DS = GetTableRepNotaCobranza()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranza.rdlc"
                    Case "repNotaCobranzaLluvia"
                        rdsDataset.Name = "DatosCobranza"
                        DS = GetTableRepNotaCobranza()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranzaLluvia.rdlc"
                    Case "repNotaCobranzaLluvia2"
                        rdsDataset.Name = "DatosCobranza"
                        DS = GetTableRepNotaCobranza()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repNotaCobranzaLluvia2.rdlc"
                    Case Else
                        rdsDataset.Name = "DataSet1"
                        DS = GetTableRepCartaCobranzaLigth()
                        ReportViewer1.LocalReport.ReportPath = "descuentos/repCartaCobranzaLight.rdlc"
                End Select
            End If


            Dim proceso As String = "9661F4A3-D2D5-42DE-8FE6-614E741ED181"
            dsSign = DocumentoCobranza.getInformacionFirmantes(Context.User.Identity.Name, proceso)
            Dim dr As DataRow
            For Each dr In DS.Tables(0).Rows ' DS.Tables("DatosDescuentoHeader").Rows
                If dsSign.Tables(0).Rows.Count = 2 Then
                    dr("SIGN1NAME") = dsSign.Tables(0).Rows(0).Item("EjecutivoNombre")      ' ""
                    dr("SIGN2NAME") = dsSign.Tables(0).Rows(1).Item("EjecutivoNombre")
                    dr("SIGN1POSITION") = dsSign.Tables(0).Rows(0).Item("EjecutivoCargo")
                    dr("SIGN2POSITION") = dsSign.Tables(0).Rows(1).Item("EjecutivoCargo")

                    dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\images\firmatest1.jpg")  'Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(0).Item("EjecutivoImagePath"))  ' 
                    dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\images\firmatest2.jpg")  'Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\" & dsSign.Tables(0).Rows(1).Item("EjecutivoImagePath"))  ' 

                Else
                    dr("SIGN1NAME") = "" ' ""
                    dr("SIGN2NAME") = ""  ' ""
                    dr("SIGN1POSITION") = ""  ' ""
                    dr("SIGN2POSITION") = ""  ' ""
                    dr("SIGN1DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID0.bmp")   ' 
                    dr("SIGN2DATA") = Utils.getImageAsByteArray(Request.PhysicalApplicationPath & "\sign\VOID1.bmp")  ' 
                End If
            Next


            'orepCartaCobranzaLight.SetDataSource(ds)
            'MIGRAR INNOVA FALTA
            'CrystalReportViewer1.ReportSource = orepCartaCobranzaLight
            rdsDataset.Value = DS.Tables(0)
            ReportViewer1.LocalReport.DataSources.Add(rdsDataset)

        End If
    End Sub


    Function GetTableRepCartaCobranzaLigth() As DataSet
        ' Create new DataTable instance.
        Dim ds As New DataSet
        Dim table As New DataTable

        ' Create 3 typed columns in the DataTable.
        table.Columns.Add("DatosDescuentoHeader_DLACC", GetType(String))
        table.Columns.Add("CUSNA1", GetType(String))
        table.Columns.Add("DIRECCION", GetType(String))
        table.Columns.Add("CUSCTY", GetType(String))
        table.Columns.Add("CUSZPC", GetType(String))
        table.Columns.Add("BRNNUM", GetType(String))
        table.Columns.Add("BRNNME", GetType(String))
        table.Columns.Add("BRNADR", GetType(String))
        table.Columns.Add("BRNPHN", GetType(String))
        table.Columns.Add("DLNCC", GetType(String))
        table.Columns.Add("CIUDAG", GetType(String))
        table.Columns.Add("PROVAG", GetType(String))
        table.Columns.Add("DEPTAG", GetType(String))
        table.Columns.Add("HORAAG", GetType(String))
        table.Columns.Add("DATA", GetType(String))
        table.Columns.Add("CUSFNA", GetType(String))
        table.Columns.Add("CUSLN1", GetType(String))
        table.Columns.Add("CUSLN2", GetType(String))
        table.Columns.Add("CUSSEX", GetType(String))
        table.Columns.Add("SIGN1NAME", GetType(String))
        table.Columns.Add("SIGN2NAME", GetType(String))
        table.Columns.Add("SIGN1POSITION", GetType(String))
        table.Columns.Add("SIGN2POSITION", GetType(String))
        table.Columns.Add("SIGN1DATA", GetType(String))
        table.Columns.Add("SIGN2DATA", GetType(String))

        table.Columns.Add("DLACC", GetType(String))
        table.Columns.Add("DLVCA", GetType(Integer))
        table.Columns.Add("DLVCM", GetType(Integer))
        table.Columns.Add("DLVCD", GetType(Integer))
        table.Columns.Add("DLCCY", GetType(String))
        table.Columns.Add("DLEIC", GetType(Integer))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("44127820", "Juan Perez", "San medina 465, San Miguel", "Lima", "Perú", "", "Principal", "Las Begonias 1045", "01 395 4525", "Grupo Miraflores SAC", "Lima", "", "", "", "", "", "", "", "M", "", "", "", "", "", "", "44127820", 10, 10, 11, "SOL", 1500)
        ds.Tables.Add(table)

        'Dim table2 As New DataTable
        'table2.Columns.Add("DLACC", GetType(String))
        'table2.Columns.Add("DLVCA", GetType(Integer))
        'table2.Columns.Add("DLVCM", GetType(Integer))
        'table2.Columns.Add("DLVCD", GetType(Integer))
        'table2.Columns.Add("DLCCY", GetType(String))
        'table2.Columns.Add("DLEIC", GetType(Integer))
        'table2.Rows.Add("", 1500, 10, 11, "SOL", 15)
        'ds.Tables.Add(table2)
        Return ds
    End Function

    Function GetTableRepCartaCobranza() As DataSet
        ' Create new DataTable instance.
        Dim ds As New DataSet
        Dim table As New DataTable

        ' Create 3 typed columns in the DataTable.
        table.Columns.Add("DatosDescuentoHeader_DLACC", GetType(String))
        table.Columns.Add("CUSNA1", GetType(String))
        table.Columns.Add("DIRECCION", GetType(String))
        table.Columns.Add("CUSCTY", GetType(String))
        table.Columns.Add("CUSZPC", GetType(String))
        table.Columns.Add("BRNNUM", GetType(String))
        table.Columns.Add("BRNNME", GetType(String))
        table.Columns.Add("BRNADR", GetType(String))
        table.Columns.Add("BRNPHN", GetType(String))
        table.Columns.Add("DLNCC", GetType(String))
        table.Columns.Add("CIUDAG", GetType(String))
        table.Columns.Add("PROVAG", GetType(String))
        table.Columns.Add("DEPTAG", GetType(String))
        table.Columns.Add("HORAAG", GetType(String))
        table.Columns.Add("DATA", GetType(String))
        table.Columns.Add("CUSFNA", GetType(String))
        table.Columns.Add("CUSLN1", GetType(String))
        table.Columns.Add("CUSLN2", GetType(String))
        table.Columns.Add("CUSSEX", GetType(String))
        table.Columns.Add("SIGN1NAME", GetType(String))
        table.Columns.Add("SIGN2NAME", GetType(String))
        table.Columns.Add("SIGN1POSITION", GetType(String))
        table.Columns.Add("SIGN2POSITION", GetType(String))
        table.Columns.Add("SIGN1DATA", GetType(String))
        table.Columns.Add("SIGN2DATA", GetType(String))
        table.Columns.Add("DLEAEN", GetType(String))
        table.Columns.Add("DLEMEN", GetType(String))
        table.Columns.Add("DLEDEN", GetType(String))

        table.Columns.Add("DLACC", GetType(String))
        table.Columns.Add("DLVCA", GetType(Integer))
        table.Columns.Add("DLVCM", GetType(Integer))
        table.Columns.Add("DLVCD", GetType(Integer))
        table.Columns.Add("DLCCY", GetType(String))
        table.Columns.Add("DLEIC", GetType(Integer))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("44127820", "Juan Perez", "San medina 465, San Miguel", "Lima", "Perú", "", "Principal", "Las Begonias 1045", "01 395 4525", "Grupo Miraflores SAC", "Lima", "", "", "", "", "", "", "", "M", "", "", "", "", "", "", "", "", "", "44127820", 10, 10, 11, "SOL", 1500)
        ds.Tables.Add(table)

        'Dim table2 As New DataTable
        'table2.Columns.Add("DLACC", GetType(String))
        'table2.Columns.Add("DLVCA", GetType(Integer))
        'table2.Columns.Add("DLVCM", GetType(Integer))
        'table2.Columns.Add("DLVCD", GetType(Integer))
        'table2.Columns.Add("DLCCY", GetType(String))
        'table2.Columns.Add("DLEIC", GetType(Integer))
        'table2.Rows.Add("", 1500, 10, 11, "SOL", 15)
        'ds.Tables.Add(table2)
        Return ds
    End Function

    Function GetTableRepCartaCobranzaLigthLluvia() As DataSet
        ' Create new DataTable instance.
        Dim ds As New DataSet
        Dim table As New DataTable

        ' Create 3 typed columns in the DataTable.
        table.Columns.Add("DatosDescuentoHeader_DLACC", GetType(String))
        table.Columns.Add("CUSNA1", GetType(String))
        table.Columns.Add("DIRECCION", GetType(String))
        table.Columns.Add("CUSCTY", GetType(String))
        table.Columns.Add("CUSZPC", GetType(String))
        table.Columns.Add("DLNCC", GetType(String))
        table.Columns.Add("CIUDAG", GetType(String))
        table.Columns.Add("CUSSEX", GetType(String))
        table.Columns.Add("SIGN1NAME", GetType(String))
        table.Columns.Add("SIGN2NAME", GetType(String))
        table.Columns.Add("SIGN1POSITION", GetType(String))
        table.Columns.Add("SIGN2POSITION", GetType(String))
        table.Columns.Add("SIGN1DATA", GetType(String))
        table.Columns.Add("SIGN2DATA", GetType(String))

        table.Columns.Add("DLACC", GetType(String))
        table.Columns.Add("DLVCA", GetType(Integer))
        table.Columns.Add("DLVCM", GetType(Integer))
        table.Columns.Add("DLVCD", GetType(Integer))
        table.Columns.Add("DLCCY", GetType(String))
        table.Columns.Add("DLEIC", GetType(Integer))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("44127820", "Juan Perez", "San medina 465, San Miguel", "Lima", "Perú", "Grupo Miraflores SAC", "Lima", "M", "", "", "", "", "", "", "44127820", 10, 10, 11, "SOL", 1500)
        ds.Tables.Add(table)

        'Dim table2 As New DataTable
        'table2.Columns.Add("DLACC", GetType(String))
        'table2.Columns.Add("DLVCA", GetType(Integer))
        'table2.Columns.Add("DLVCM", GetType(Integer))
        'table2.Columns.Add("DLVCD", GetType(Integer))
        'table2.Columns.Add("DLCCY", GetType(String))
        'table2.Columns.Add("DLEIC", GetType(Integer))
        'table2.Rows.Add("", 1500, 10, 11, "SOL", 15)
        'ds.Tables.Add(table2)
        Return ds
    End Function

    Function GetTableRepCartaNotarial()
        Dim table As New DataTable

        ' Create 3 typed columns in the DataTable.
        table.Columns.Add("DLACC", GetType(String))
        table.Columns.Add("CUSNA1", GetType(String))
        table.Columns.Add("DIRECCION", GetType(String))
        table.Columns.Add("CUSCTY", GetType(String))
        table.Columns.Add("CUSZPC", GetType(String))
        table.Columns.Add("BRNNME", GetType(String))
        table.Columns.Add("BRNADR", GetType(String))
        table.Columns.Add("BRNPHN", GetType(String))
        table.Columns.Add("DLNCC", GetType(String))

        table.Columns.Add("HORAAG", GetType(String))


        table.Columns.Add("DLVCA", GetType(Integer))
        table.Columns.Add("DLVCM", GetType(Integer))
        table.Columns.Add("DLVCD", GetType(Integer))
        table.Columns.Add("DLCCY", GetType(String))
        table.Columns.Add("DLEIC", GetType(Integer))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("44127820", "Juan Perez", "San medina 465, San Miguel", "Lima", "Perú", "Principal", "Las Begonias 1045", "01 395 4525", "Grupo Miraflores SAC", "", 10, 10, 11, "SOL", 2000)
        ds.Tables.Add(table)

        'Dim table2 As New DataTable
        'table2.Columns.Add("DLACC", GetType(String))
        'table2.Columns.Add("DLVCA", GetType(Integer))
        'table2.Columns.Add("DLVCM", GetType(Integer))
        'table2.Columns.Add("DLVCD", GetType(Integer))
        'table2.Columns.Add("DLCCY", GetType(String))
        'table2.Columns.Add("DLEIC", GetType(Integer))
        'table2.Rows.Add("", 1500, 10, 11, "SOL", 15)
        'ds.Tables.Add(table2)
        Return ds
    End Function



    Function GetTableRepNotaCobranza() As DataSet
        ' Create new DataTable instance.
        Dim ds As New DataSet
        Dim table As New DataTable

        ' Create 3 typed columns in the DataTable.
        table.Columns.Add("DatosDescuentoHeader_DLACC", GetType(String))
        table.Columns.Add("CUSNA1", GetType(String))
        table.Columns.Add("DIRECCION", GetType(String))
        table.Columns.Add("CUSCTY", GetType(String))
        table.Columns.Add("BRNADR", GetType(String))
        table.Columns.Add("DLNCC", GetType(String))

        table.Columns.Add("CIUDAG", GetType(String))
        table.Columns.Add("HORAAG", GetType(String))
        table.Columns.Add("DATA", GetType(String))
        table.Columns.Add("CUSFNA", GetType(String))
        table.Columns.Add("CUSLN1", GetType(String))
        table.Columns.Add("CUSSEX", GetType(String))
        table.Columns.Add("SIGN1NAME", GetType(String))
        table.Columns.Add("SIGN2NAME", GetType(String))
        table.Columns.Add("SIGN1POSITION", GetType(String))
        table.Columns.Add("SIGN2POSITION", GetType(String))
        table.Columns.Add("SIGN1DATA", GetType(String))
        table.Columns.Add("SIGN2DATA", GetType(String))
        table.Columns.Add("DLEAEN", GetType(String))
        table.Columns.Add("DLEMEN", GetType(String))
        table.Columns.Add("DLEDEN", GetType(String))

        table.Columns.Add("DLACC", GetType(String))
        table.Columns.Add("DLVCA", GetType(Integer))
        table.Columns.Add("DLVCM", GetType(Integer))
        table.Columns.Add("DLCCY", GetType(String))

        ' Add five rows with those columns filled in the DataTable.
        table.Rows.Add("44127820", "Juan Perez", "San medina 465, San Miguel", "Lima", "Las Begonias 1045", "Grupo Miraflores SAC", "Lima", "", "", "", "", "M", "", "", "", "", "", "", "", "", "", "44127820", 10, 10, "SOL")
        ds.Tables.Add(table)

        'Dim table2 As New DataTable
        'table2.Columns.Add("DLACC", GetType(String))
        'table2.Columns.Add("DLVCA", GetType(Integer))
        'table2.Columns.Add("DLVCM", GetType(Integer))
        'table2.Columns.Add("DLVCD", GetType(Integer))
        'table2.Columns.Add("DLCCY", GetType(String))
        'table2.Columns.Add("DLEIC", GetType(Integer))
        'table2.Rows.Add("", 1500, 10, 11, "SOL", 15)
        'ds.Tables.Add(table2)
        Return ds
    End Function

End Class
