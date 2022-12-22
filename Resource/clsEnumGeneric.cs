using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Resource
{
    public enum enumGeneric
    {
        Default = 0,
        InfoMessage = 1,
        ErrorMessage = 2,
        ValidationMessage = 3,

        ServiceError = -200,
        DataBaseError = -300,
        DataBaseRaiseError = -350,
        NoRecords = -400,
        ProxyError = -500,
        SendMailError = -600,
    }

    public enum enumMessage
    {
        Information = 1,
        Warning = 2,
    }

    public enum enumEstadoLog
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Errores = 3,
        Fatal = 4,
    }

    public enum enumDialog
    {
        OK = 1,
        Cancel = 2,
    }

    public enum enumTipoEnvioCorreo
    {
        SinTipoEnvio = 0,
        EnvioAutomaticoNomina = 1,
        AlertaPrepago = 2,
        AlertaPostpago = 3,
        AlertaVencimiento = 4,
        ReporteAutomatico = 5,
        GeneracionPagoAutomatico = 6,
        ReportePagoAutomatico = 7,
    }

    public enum enumLogEnvioCorreo
    {
        Enviado = 1,
        Cancelado = 2,
        Error = 3,
    }

    public enum enumProcesosAutomaticos
    {
        Iniciado = 1,
        Terminado = 2,
        Errores = 3,
    }

    public enum enumPagosAutomaticos
    {
        Terminado = 1,
        EnProceso = 2,
        Error = 3,
    }

    public enum enumEstadoLogEnvio
    {
        Debug = 0,
        Info = 1,
        Warn = 2,
        Errores = 3,
        Fatal = 4,
    }

    public enum enumComboRecord
    {
        Select = 1,
        All = 2,
        None = 0
    }

    public enum enumProcessType
    {
        Ninguno = 0,
        Envio = 1,
        Recepcion = 2,
        Conciliacion = 3
    }

    public enum enumParametroEnvioMail
    {
        ModoPrueba = 0,
        RutaDescargaNominas = 1,
        RutaDescargaReportes = 2,
        RutaRecepcionNominas = 3,
        MailPorDefecto = 4,
        MailFuncionarios = 5,
        MailEnvio = 6,
        ListaMailTest = 7,
        ListaMailCopias = 8,
        AsuntoNomina = 9,
        CuerpoNomina = 10,
        AsuntoNominaAutomatica = 11,
        CuerpoNominaAutomatica = 12,
        AsuntoAlertaAutomatica = 13,
        CuerpoAlertaAutomatica = 14,
        AsuntoReporteAutomatico = 15,
        CuerpoReporteAutomatico = 16,
        AsuntoReportePagoAutomatico = 17,
        CuerpoReportePagoAutomatico = 18,
        ServidorSMTP = 19,
        HoraEjecucionPagos = 20,
        UsuarioEjecucionPagos = 21,
    }

    public static class clsConnections
    {
        public static string strAS400 = BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios");
    }

    public static class clsConstantsGeneric
    {
        public static string NoRecords = "Sin Información.";
        public static string NoRecordsFull = "La consulta no contiene registros para los parametros ingresados.";
        public static string ErrorValidation = "Error en Validación";
        public static string ServiceError = "Error de Servicio.";
        public static string DataBaseError = "Error en Base de datos.";
        public static string ProxyError = "El Servicio no Responde.";
        public static string RaiseError = "Error controlado de base de datos.";
        public static string UniqueKey = "Error, registro existente";
        public static string NoExisteRegistro = "Error, registro no existe";        
    }

    public static class clsMensajesGeneric
    {
        public static string ParametroVacio = "El valor &1 no puede ser vacio";
        public static string Seleccionar = "Debe seleccionar un valor para: &1";
        public static string RegistroExitoso = "Info: &1 guardado exitosamente";
        public static string BorradoExitoso = "Info: &1 eliminado exitosamente";
        public static string ActualizadoExitoso = "Info: &1 actualizado exitosamente";
        public static string CampoNoNumerico = "El valor &1 no es numérico";
        public static string ResultadoVacio = "La consulta del Proceso: &1, devolvio vacio para el campo: &2";
        public static string ProcesoYAGenerado = "El Cliente: &1, ya tiene generado un Proceso para el periodo: &2 - &3";
        public static string RegistroYaAsignado = "&1 ya esta asignada para &2";
        public static string ExcepcionControlada = "Excepcion controlada para el Proceso: &1. Tipo de error: &2, el mensaje fue: &3";

        public static string ProcesoMensajeEnviadoExitoso = "Info del Proceso: &1, del cliente: &2 - &3. Mensaje enviado exitosamente";
        public static string ProcesoMensajeEnviadoError = "Error del Proceso: &1, del cliente: &2 - &3. Error al enviar el Correo.";
        public static string ProcesoMensajeEnviadoFuncionarioExitoso = "El Reporte de Nóminas, se ha generado de forma automática a través del Sistema BIFConvenios";
        public static string ProcesoMensajeYaExiste = "Error del Proceso: &1, del cliente: &2 - &3. El Proceso ya existe";
        public static string ProcesoNoExisteCorreo = "Error del Proceso: &1, del cliente: &2 - &3. No existe correos asociados.";
        public static string ProcesoNoValidoPago = "Error: No se encuentra el Proceso para la Empresa con Codigo: &1, del periodo: &2 - &3";
        public static string ProcesandoArchivoDescuento = "Error: En estos momentos se encuentra procesando un archivo, se volvera a intentar en unos minutos";
        public static string NoTerminoCargaProcesoDescuento = "Error: Aun no termina el Proceso de Descuento para el Cliente con Codigo: &1";
        public static string ProcesoClienteError = "Error al obtener los datos del Cliente con tipo de documento: &1 y número de documento: &2";
        public static string ProcesoListaError = "Error: No se puede obtener la lista de &1 disponibles";
        public static string ProcesoCantidadRegistros = "Total de Registros: &1, cantidad de registros procesados: &2, cantidad de registros erroneos: &3. Para mas detalles de los errores, consultar el Visor de Envios Automaticos";
        public static string ProcesoNoSaldoEncontrado = "Error: No se encontró el Saldo disponible para el Cliente: &1, con Código: &2";
        public static string ProcesoNoEncontrado = "Info: El Cliente: &1, con Código: &2, no tiene un Proceso Activo.";

        public static string MensajeLimiteRegitros = "Observación: Para una mejor presentación de los datos, se limitará SOLO A MOSTRAR &1 registros para esta consulta. Para un mejor resultado, por favor, ingrese mas caractéres al valor de búsqueda.";
        public static string MensajeValidarClienteIBS = "Error: no se validó la empresa en IBS, por favor, Validar la empresa en la Opcion: '&1'";
        public static string MensajeExisteClienteBifConvenios = "Error: ya existe una Empresa registrada con ese Tipo y Numero de Documento";

        public static string MensajeNoAlertasEnviar = "Info: No hay Alertas a enviar para el Cliente: &1, con Código: &2";
        public static string MensajeNoRegistraCorreos = "Info: No se encontraron Correos Electrónicos de Coordinadores para el Cliente: &1, con Código: &2";
        public static string MensajeClienteNoEnvioAutomatico = "Info: El cliente: &1, con codigo: &2, no tiene marcada la casilla de envio automático.";
        public static string MensajeFuncionarioNoEnvioAutomatico = "Info: El Funcionario: &1, no tiene información a enviar para el dia: &2";

        public static string MensajeDirectoryNotExists = "Error: La ruta &1 no existe. Verificar con el Administrador de Red.";
        public static string MensajeDirectoryCreate = "Info: Directorio &1 no existe, se procede a crearlo";
        public static string MensajeDirectoryEmpty = "Info: Directorio &1 se encuentra vacío.";
        
        public static string RegistroNoExiste = "Error: no existe &1 en &2";
    }

    public static class clsTiposSystemParameters
    {
        public static string TipoDocumento = "TipoDocumento";
        public static string TipoEnvioCorreo = "TipoEnvioCorreo";
        public static string EstadoEnvioCorreo = "EstadoEnvioCorreo";
        public static string TipoArchivoEnvio = "TipoArchivoEnvio";
        public static string TipoAlerta = "TipoAlerta";
        public static string TipoMetadata = "TipoMetadata";
        public static string EstadoProcesoAutomatico = "EstadoProcesoAutomatico";
        public static string EstadoPagoAutomatico = "EstadoPagoAutomatico";
        public static string ParametroEnvioMail = "ParametroEnvioMail";
    }

    public class MailSource
    {
        private string _Mail;

        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }

        public MailSource(string pstrMail)
        {
            this._Mail = pstrMail;
        }
    }
}

