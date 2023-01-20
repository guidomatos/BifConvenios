Imports System.ComponentModel
Imports System.Data
Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

Imports BIFConvenios.BL
Imports BIFConvenios.BE

<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<ToolboxItem(False)> _
Public Class WSBIFConvenios
    Inherits System.Web.Services.WebService

    <WebMethod()> _
    Public Function AnulaEnvioCobranzaIBS(ByVal pCodigo_proceso As String, ByVal pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.AnulaEnvioCobranzaIBS(pCodigo_proceso, pUsuario)
    End Function

    <WebMethod()> _
    Public Function EnviaInformacionIBS(ByVal pCodigo_proceso As String, ByVal pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.EnvioCobranzaIBS(pCodigo_proceso, pUsuario)
    End Function

    <WebMethod()> _
    Public Function GeneraCronogramaFuturo(ByVal pCodigo_proceso As String, ByVal pTipoFormatoArchivo As String, ByVal pSituacionTrabajador As String, ByVal pUsuario As String) As Integer
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.GeneraCronogramaFuturo(pCodigo_proceso, pTipoFormatoArchivo, pSituacionTrabajador, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ImportaDescuentoEmpresa(ByVal pCodigo_proceso As String, ByVal pNombreArchivo As String, ByVal pUsuario As String) As String
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.ImportaDescuentosEmpresa(pCodigo_proceso, pNombreArchivo, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ImportaDescuentosEmpresa(ByVal pCodigo_proceso As String, ByVal pNombreArchivo As String, ByVal pTipoFormatoArchivo As String, ByVal pUsuario As String) As Integer
        Dim iCobranza As New BIFConvenios.BL.CobranzaBL
        Return iCobranza.ImportaDescuentosEmpresa(pCodigo_proceso, pNombreArchivo, pTipoFormatoArchivo, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ConsultaPagaresDeIBS(ByVal pCodigo_ClienteIBS As String, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String, ByVal pCodigo_Cliente As String, ByVal pUsuario As String) As DataTable
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ConsultarPagaresDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ImportaPagaresDeIBS(ByVal pCodigo_ClienteIBS As String, ByVal pAnio As String, ByVal pMes As String, ByVal pFecha_ProcesoAS400 As String, ByVal pCodigo_Cliente As String, ByVal pUsuario As String) As String
        'Dim iCuota As New BIFConvenios.BL.CuotaBL
        'Return iCuota.ImportaPagaresDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
        Dim Cuota As New BIFConvenios.BL.clsCuotaBL
        Return Cuota.ImportaPagareDeIBS(pCodigo_ClienteIBS, pAnio, pMes, pFecha_ProcesoAS400, pCodigo_Cliente, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ProcesaBloqueo(ByVal pNumeroLote As String, ByVal pUsuario As String) As Integer
        Dim iBloqueo As New BIFConvenios.BL.BloqueoBL
        Return iBloqueo.ProcesoBloqueo(pNumeroLote, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ProcesaProrroga(ByVal pNumeroLote As String, ByVal pUsuario As String) As Integer
        Dim iProrroga As New BIFConvenios.BL.ProrrogaBL
        Return iProrroga.ProcesoProrroga(pNumeroLote, pUsuario)
    End Function

    <WebMethod()> _
    Public Function ConsultarMotivoIBS(ByVal pCodigo_proceso As String, ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerMotivosDeIBS(pCodigo_proceso, pCodigo_Cliente, pAnio, pMes)
    End Function


    <WebMethod()> _
    Public Function ObtenerCabeceraCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerCabeceraCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function


    <WebMethod()> _
    Public Function ObtenerDetalleCasillero(ByVal pCodigo_Cliente As String, ByVal pAnio As String, ByVal pMes As String) As DataSet
        Dim iCuota As New BIFConvenios.BL.CuotaBL
        Return iCuota.ObtenerDetalleCasillero(pCodigo_Cliente, pAnio, pMes)
    End Function

End Class
