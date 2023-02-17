Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine

Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationSettings
Imports System.IO
Imports BIFConvenios.BL
Imports BIFConvenios.Utils
Imports Resource

Partial Class reportes_reporteCasillero
    Inherits System.Web.UI.Page

    Protected oproc As New BIFConvenios.Proceso()
    Protected oCuota As New BIFConvenios.Cuota()
    Protected dsCasilleroCabIBS As DataSet
    Protected dsCasilleroDetIBS As DataSet
    Protected oRepListadoCasillero As New RepListadoCasillero()
    Protected dsListadoCasillero As New DataSetReporteCasillero()


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        If Not Page.IsPostBack Then

            'Informacion del Año
            ddlAnio.DataSource = oproc.GetAnioCasillero()
            ddlAnio.DataBind()
            ddlAnio.SelectedIndex = ddlAnio.Items.IndexOf(ddlAnio.Items.FindByValue(Now.Year.ToString()))

            PnlReporte.Visible = False
            lblMensaje.Text = ""

            'Informacion del Mes
            'ddlMes.DataSource = BIFConvenios.Periodo.GetMonthsByReaderWithOutZero(oproc.GetMesesEnvioArchivoAS400(Now.Year.ToString))
            'ddlMes.DataBind()
            'ddlMes.SelectedIndex = ddlMes.Items.IndexOf(ddlMes.Items.FindByValue(Now.Month.ToString()))


        End If

    End Sub


    Private Sub ListarReporteCasillero(ByVal pEmpresa As String, ByVal pAnio As Integer, ByVal pMes As Integer)

        Dim ldsCabecera, ldsDetalle As DataSet
        
        Try

            Dim objWSConvenios As New wsConvenios.WSBIFConvenios
            objWSConvenios.Credentials = System.Net.CredentialCache.DefaultCredentials
            ldsCabecera = objWSConvenios.ObtenerCabeceraCasillero(pEmpresa, pAnio, pMes)
            Session("ldsCabecera") = ldsCabecera

            If ldsCabecera.Tables(0).Rows.Count > 0 Then

                PnlReporte.Visible = True
                lblMensaje.Text = String.Empty

                'Obtiene la Cabecera del reporte Casillero
                If ldsCabecera.Tables(0).Rows.Count > 0 Then
                    lblEmpresa.Text = ldsCabecera.Tables(0).Rows(0)("DLEDSC").ToString()
                    lblTotalPagar.Text = ldsCabecera.Tables(0).Rows(0)("DLSPAD").ToString()
                End If

                'Obtiene el Detalle del reporte Casillero
                ldsDetalle = objWSConvenios.ObtenerDetalleCasillero(pEmpresa, pAnio, pMes)
                Session("ldsDetalle") = ldsDetalle

                dgDetalleCasillero.DataSource = ldsDetalle
                dgDetalleCasillero.DataBind()


            Else
                lblMensaje.Text = "La consulta no devuelve datos."
                PnlReporte.Visible = False
            End If
            
        Catch ex As Exception
            'Call MostrarMensajeGeneral(True, Utils.HandleError(excp))
        End Try

        
    End Sub

    Protected Sub lnkBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBuscar.Click

        Dim pEmpresa, panio, pmes As String

        Try

            pEmpresa = Me.hdCodigoEmpresa.Value
            panio = ddlAnio.SelectedValue.Substring(2)
            pmes = Me.ddlMes.SelectedValue

            Call ListarReporteCasillero(pEmpresa, panio, pmes)


        Catch ex As Exception

        End Try

    End Sub

    Protected Sub lnkExportar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExportar.Click

        Dim ldsCabecera, ldsDetalle As DataSet
        Dim Empresa, TotalaPagar As String
        Dim Pagare, Cliente, Periodo, Mes As String
        Dim TarifaporPagar, TarifaPagado As Decimal

        Periodo = Me.ddlAnio.SelectedItem.Text
        Mes = Me.ddlMes.SelectedItem.Text

        'Inserta la cabecera del reporte al datatable Cabecera
        ldsCabecera = Session("ldsCabecera")
        For Each drC As DataRow In ldsCabecera.Tables(0).Rows

            Empresa = drC("DLEDSC")
            TotalaPagar = Format(CType(drC("DLSPAD"), Decimal), "##,##0.00")

            dsListadoCasillero.Cabecera.AddCabeceraRow(Empresa, TotalaPagar, Periodo, Mes)

        Next

        'Inserta el detalle del reporte al datatable Detalle
        ldsDetalle = Session("ldsDetalle")
        For Each drD As DataRow In ldsDetalle.Tables(0).Rows
            Cliente = drD("CUSNA1")
            TarifaporPagar = Format(CType(drD("DLSAMT"), Decimal), "##,##0.00")
            'otra opcion "0:00"
            TarifaPagado = Format(CType(drD("DLSPAD"), Decimal), "##,##0.00")
            Pagare = drD("DEAACC")

            dsListadoCasillero.Detalle.AddDetalleRow(Cliente, TarifaporPagar, TarifaPagado, Pagare)
        Next

        'Se añade el dataset al objeto set del crystal report
        oRepListadoCasillero.SetDataSource(dsListadoCasillero)

        'Se obtiene el nombre del archivo excel a generar
        Dim name As String = ConfigurationManager.AppSettings("NombreCasillero")
        'Dim strPathFile As String = ConfigurationManager.AppSettings("Casillero")

        'Dim name As String = getWebServerDateId() & ".xls"
        Dim filename As String = Server.MapPath(".") & "\export\" & name
        RemoveFiles(Server.MapPath(".") & "\export\", New TimeSpan(0, 6, 0, 0))


        'Obtiene la ruta completa, para guardar el archivo excel
        'Dim strFullName As String = strPathFile + "\\" + name

        'Procedimiento para el grabado del archivo excel
        With oRepListadoCasillero.ExportOptions
            .ExportDestinationType = ExportDestinationType.DiskFile
            .ExportFormatType = ExportFormatType.Excel
            .DestinationOptions = New CrystalDecisions.Shared.DiskFileDestinationOptions()
            .DestinationOptions.DiskFileName = filename
        End With

        oRepListadoCasillero.Export()

        Dim ms As System.IO.MemoryStream
        Dim fileS As FileStream = File.OpenRead(filename)

        ms = New MemoryStream(fileS.Length)
        Dim br As BinaryReader = New BinaryReader(fileS)
        Dim bytesRead As Byte() = br.ReadBytes(fileS.Length)

        ms.Write(bytesRead, 0, fileS.Length)

        With HttpContext.Current.Response
            .ClearContent()
            .ClearHeaders()
            .ContentType = "application/vnd.ms-excel"
            .AddHeader("Content-Disposition", "inline; filename=" & name)
            .BinaryWrite(ms.ToArray)

            .End()
        End With

    End Sub

    Protected Sub dgDetalleCasillero_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles dgDetalleCasillero.PageIndexChanging

        Dim pEmpresa, panio, pmes As String

        Try

            dgDetalleCasillero.PageIndex = e.NewPageIndex
            lblMensaje.Text = ""


            pEmpresa = Me.hdCodigoEmpresa.Value
            panio = ddlAnio.SelectedValue.Substring(2)
            pmes = Me.ddlMes.SelectedValue

            Call ListarReporteCasillero(pEmpresa, panio, pmes)


        Catch ex1 As HandledException
            lblMensaje.Text = ex1.ErrorMessage + ": " + ex1.ErrorMessageFull
        Catch ex2 As Exception
            lblMensaje.Text = "Error: " + ex2.Message
        End Try
    End Sub

    Protected Sub lnkBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkBack.Click
        Response.Redirect("../Default.aspx")
    End Sub
End Class
