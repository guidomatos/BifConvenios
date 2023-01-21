Imports System.ComponentModel
Imports System.Data
Imports System.Web.Services

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<CompilerServices.DesignerGenerated()>
<ToolboxItem(False)> _
Public Class WSBIFConvenios
    Inherits WebService

    <WebMethod()>
    Public Function AnulaEnvioCobranzaIBS(pCodigo_proceso As String, pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.AnulaEnvioCobranzaIBS(pCodigo_proceso, pUsuario)
    End Function

    <WebMethod()>
    Public Function EnviaInformacionIBS(pCodigo_proceso As String, pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.EnvioCobranzaIBS(pCodigo_proceso, pUsuario)
    End Function

    <WebMethod()>
    Public Function GeneraCronogramaFuturo(pCodigo_proceso As String, pTipoFormatoArchivo As String, pSituacionTrabajador As String, pUsuario As String) As Integer
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.GeneraCronogramaFuturo(pCodigo_proceso, pTipoFormatoArchivo, pSituacionTrabajador, pUsuario)
    End Function

    <WebMethod()>
    Public Function ImportaDescuentoEmpresa(pCodigo_proceso As String, pNombreArchivo As String, pUsuario As String) As String
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.ImportaDescuentosEmpresa(pCodigo_proceso, pNombreArchivo, pUsuario)
    End Function

    <WebMethod()>
    Public Function ImportaDescuentosEmpresa(pCodigo_proceso As String, pNombreArchivo As String, pTipoFormatoArchivo As String, pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.ImportaDescuentosEmpresa(pCodigo_proceso, pNombreArchivo, pTipoFormatoArchivo, pUsuario)
    End Function

    <WebMethod()>
    Public Function ConsultaPagaresDeIBS(pCodigo_ClienteIBS As String, pAnio As String, pMes As String, pFecha_ProcesoAS400 As String, pCodigo_Cliente As String, pUsuario As String) As DataTable
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ConsultarPagaresDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
    End Function

    <WebMethod()>
    Public Function ImportaPagaresDeIBS(pCodigo_ClienteIBS As String, pAnio As String, pMes As String, pFecha_ProcesoAS400 As String, pCodigo_Cliente As String, pUsuario As String) As String
        'Dim iCuota As New BIFConvenios.BL.CuotaBL
        'Return iCuota.ImportaPagaresDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
        Dim Cuota As New BIFConvenios.BL.clsCuotaBL
        Return Cuota.ImportaPagareDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
    End Function

    <WebMethod()>
    Public Function ProcesaBloqueo(pNumeroLote As String, pUsuario As String) As Integer
        Dim iBloqueo As New BIFConvenios.BL.BloqueoBL
        Return iBloqueo.ProcesoBloqueo(pNumeroLote, pUsuario)
    End Function

    <WebMethod()>
    Public Function ProcesaProrroga(pNumeroLote As String, pUsuario As String) As Integer
        Dim iProrroga As New BIFConvenios.BL.ProrrogaBL
        Return iProrroga.ProcesoProrroga(pNumeroLote, pUsuario)
    End Function

    <WebMethod()>
    Public Function ConsultarMotivoIBS(pCodigo_proceso As String, pCodigo_Cliente As String, pAnio As String, pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerMotivosDeIBS(pCodigo_proceso, pCodigo_Cliente, pAnio, pMes)
    End Function


    <WebMethod()>
    Public Function ObtenerCabeceraCasillero(pCodigo_Cliente As String, pAnio As String, pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerCabeceraCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function


    <WebMethod()>
    Public Function ObtenerDetalleCasillero(pCodigo_Cliente As String, pAnio As String, pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerDetalleCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function

End Class
