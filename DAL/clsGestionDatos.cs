using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DAL
{
    public class clsGestionDatos
    {
        #region Singleton

        private static volatile clsGestionDatos ogestor = new clsGestionDatos();

        /// <summary>
        /// Obtiene una Unica Instancia de la clase.
        /// </summary>
        public static clsGestionDatos Instancia
        {
            get { return ogestor; }
        }

        /// <summary>
        /// Obtiene una Unica Instancia de la clase.
        /// </summary>
        public static clsGestionDatos ObtenerInstancia()
        {
            return ogestor;
        }

        public static clsGestionDatos ObtenerInstancia(String claveConexion)
        {
            ogestor = new clsGestionDatos(claveConexion);

            return ogestor;
        }

        #endregion

        #region Campos

        private String _claveCadenaConexion = "CadenaConexion";
        private SqlConnection _oConexion;
        private SqlTransaction _oTransaccion;

        private String _cadenaConexion = BIFUtils.WS.Utils.CadenaConexion("Conexion"); // Constants.NombreConexion;
        private Hashtable _comandoSQL = new Hashtable();

        #endregion

        #region Constructor

        /// <summary>
        /// Retorna una instancia de la clase.
        /// En el archivo de configuración debe haber una entrada con nombre "CadenaConexion"
        /// que guarda la cadena de conexión.
        /// </summary>
        public clsGestionDatos()
        {
            EstablecerCadenaConexion(_claveCadenaConexion);
        }

        /// <summary>
        /// Retorna una instancia de la clase.
        /// Se envía como parámetro el nombre de la clave que en el Archivo de Configuración
        /// guarda la cadena de conexión.
        /// </summary>
        /// <param name="claveConexion"></param>
        public clsGestionDatos(String claveConexion)
        {
            EstablecerCadenaConexion(claveConexion);
        }

        #endregion

        private void EstablecerCadenaConexion(String claveConexion)
        {
            _cadenaConexion = ConfigurationManager.ConnectionStrings[claveConexion].ToString();
        }

        public void AbrirConexion()
        {
            AbrirConexion(EstablecerConexion());
        }

        public void AbrirConexion(SqlConnection oConnection)
        {
            if (oConnection.State == ConnectionState.Closed)
                oConnection.Open();
        }

        public void IniciarTransaccion()
        {
            if (_oConexion == null)
                throw new Exception("No existe conexión Aperturada para Iniciar Transacción.");

            _oTransaccion = _oConexion.BeginTransaction();
        }

        public void ConfirmarTransaccion()
        {
            _oTransaccion.Commit();
        }

        public SqlTransaction ObtenerTransaccion()
        {
            return _oTransaccion;
        }

        public void CancelarTransaccion()
        {
            if (_oTransaccion != null && _oTransaccion.Connection != null)
                _oTransaccion.Rollback();
        }

        public void CerrarConexion()
        {
            CerrarConexion(EstablecerConexion());
        }

        public void CerrarConexion(SqlConnection oConnection)
        {
            if (oConnection.State == ConnectionState.Open)
                oConnection.Close();
        }

        public void Ejecutar(String entidad, Hashtable parametro)
        {
            Boolean conexionActiva = false;

            using (SqlCommand com = EstablecerComando(entidad, parametro))
            {
                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                com.ExecuteNonQuery();

                if (!conexionActiva)
                    com.Connection.Close();
            }
        }

        public void Ejecutar(SqlConnection oconexion, String entidad, Hashtable parametro)
        {
            Boolean conexionActiva = false;

            using (SqlCommand com = EstablecerComando(oconexion, entidad, parametro))
            {
                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                com.ExecuteNonQuery();

                if (!conexionActiva)
                    com.Connection.Close();
            }
        }

        public void Ejecutar(String comandoSQL, SqlConnection conexion)
        {
            Boolean conexionActiva = false;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = comandoSQL;
                com.CommandType = CommandType.Text;
                com.Connection = conexion;

                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                com.ExecuteNonQuery();

                if (!conexionActiva)
                    com.Connection.Close();
            }
        }

        public void Ejecutar(String comandoSQL)
        {
            Boolean conexionActiva = false;

            using (SqlCommand com = new SqlCommand())
            {
                com.CommandText = comandoSQL;
                com.CommandType = CommandType.Text;
                com.Connection = EstablecerConexion();

                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                com.ExecuteNonQuery();

                if (!conexionActiva)
                    com.Connection.Close();
            }
        }

        public Object EjecutarEscalar(String entidad, Hashtable parametro)
        {
            Boolean conexionActiva = false;
            Object resultado = null;

            using (SqlCommand com = EstablecerComando(entidad, parametro))
            {
                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                resultado = com.ExecuteScalar();

                if (!conexionActiva)
                    com.Connection.Close();
            }

            return resultado;
        }

        public Object EjecutarEscalar(SqlConnection oconexion, String entidad, Hashtable parametro)
        {
            Boolean conexionActiva = false;
            Object resultado = null;

            using (SqlCommand com = EstablecerComando(oconexion, entidad, parametro))
            {
                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                resultado = com.ExecuteScalar();

                if (!conexionActiva)
                    com.Connection.Close();
            }

            return resultado;
        }

        public DataRow ObtenerDataRow(String entidad, Hashtable parametro)
        {
            DataTable dt = ObtenerDataTable(entidad, parametro);

            return dt != null && dt.Rows.Count > 0 ? dt.Rows[0] : (DataRow)null;
        }

        public DataTable ObtenerDataTable(String entidad, Hashtable parametro)
        {
            DataTable oDataTable = new DataTable();
            Boolean conexionActiva = false;

            using (SqlCommand com = EstablecerComando(entidad, parametro))
            {
                conexionActiva = com.Connection.State == ConnectionState.Open;

                if (!conexionActiva)
                    com.Connection.Open();

                if (_oTransaccion != null)
                    com.Transaction = _oTransaccion;

                oDataTable.Load(com.ExecuteReader());

                if (!conexionActiva)
                    com.Connection.Close();
            }

            return oDataTable;
        }

        public SqlDataReader ObtenerDataReader(String entidad)
        {
            return ObtenerDataReader(EstablecerConexion(), entidad, new Hashtable());
        }

        public SqlDataReader ObtenerDataReader(String entidad, Hashtable parametros)
        {
            return ObtenerDataReader(EstablecerConexion(), entidad, parametros);
        }

        public SqlDataReader ObtenerDataReader(SqlConnection oConnection, String entidad, Hashtable parametros)
        {
            if (oConnection == null)
                throw new Exception("Objeto Conexion no existe.");

            if (oConnection.State == ConnectionState.Closed)
                throw new Exception("No existe conexión activa disponible.");

            SqlCommand com = EstablecerComando(oConnection, entidad, parametros);

            return com.ExecuteReader();
        }

        #region Establecer Comando en caché

        private SqlCommand EstablecerComando(String comando)
        {
            return EstablecerComando(comando, new Hashtable());
        }

        private SqlCommand EstablecerComando(String comando, Hashtable parametro)
        {
            String cadenaComandoSQL = comando.Trim();

            if (!_comandoSQL.Contains(cadenaComandoSQL))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = EstablecerConexion();
                    com.CommandText = cadenaComandoSQL;
                    com.CommandType = CommandType.StoredProcedure;

                    _comandoSQL[cadenaComandoSQL] = com;
                }
            }

            ((SqlCommand)_comandoSQL[cadenaComandoSQL]).Parameters.Clear();

            foreach (DictionaryEntry param in parametro)
            {
                ((SqlCommand)_comandoSQL[cadenaComandoSQL]).Parameters.AddWithValue(param.Key.ToString().ToLower(), param.Value);
            }

            return ((SqlCommand)_comandoSQL[cadenaComandoSQL]);
        }

        private SqlCommand EstablecerComando(SqlConnection oConexion, String comando, Hashtable parametro)
        {
            String cadenaComandoSQL = comando.Trim();

            if (!_comandoSQL.Contains(cadenaComandoSQL))
            {
                using (SqlCommand com = new SqlCommand())
                {
                    com.Connection = oConexion;
                    com.CommandText = cadenaComandoSQL;
                    com.CommandType = CommandType.StoredProcedure;

                    _comandoSQL[cadenaComandoSQL] = com;
                }
            }
            else
            {
                ((SqlCommand)_comandoSQL[cadenaComandoSQL]).Connection = oConexion;
            }

            ((SqlCommand)_comandoSQL[cadenaComandoSQL]).Parameters.Clear();

            foreach (DictionaryEntry param in parametro)
            {
                ((SqlCommand)_comandoSQL[cadenaComandoSQL])
                    .Parameters.AddWithValue(param.Key.ToString().ToLower(), param.Value);
            }

            return ((SqlCommand)_comandoSQL[cadenaComandoSQL]);
        }

        private SqlConnection EstablecerConexion()
        {
            if (_oConexion == null)
                _oConexion = new SqlConnection(_cadenaConexion);

            return _oConexion;
        }

        #endregion

        #region Manejar Conexion

        /// <summary>
        /// Devuelve una conexion SQL.
        /// </summary>
        /// <returns></returns>
        public SqlConnection ObtenerConexion()
        {
            return ObtenerConexion(_cadenaConexion);
        }

        public SqlConnection ObtenerConexion(String cadenaConexion)
        {
            SqlConnection sqlCon = null;

            if (!String.IsNullOrEmpty(cadenaConexion))
                sqlCon = new SqlConnection(cadenaConexion);

            return sqlCon;
        }

        public SqlConnection ObtenerConexionActiva()
        {
            if (_oConexion == null || _oConexion.State == ConnectionState.Closed)
                throw new Exception("No existe conexión activa.");

            return _oConexion;
        }

        #endregion

        /// <summary>
        /// Enumerador para los Estados de la Conexión.
        /// </summary>
        public enum EstadoConexion
        {
            Abierto, Cerrado, Conectando, Ejecutando, Interrumpido, Indefinido,
        }

        /// <summary>
        /// Devuelve el estado actual de la conexión.
        /// </summary>
        public EstadoConexion EstadoConexionActual
        {
            get
            {
                EstadoConexion estado = EstadoConexion.Abierto;

                switch (_oConexion.State)
                {
                    case ConnectionState.Broken:
                        estado = EstadoConexion.Interrumpido;
                        break;
                    case ConnectionState.Closed:
                        estado = EstadoConexion.Cerrado;
                        break;
                    case ConnectionState.Connecting:
                        estado = EstadoConexion.Conectando;
                        break;
                    case ConnectionState.Executing:
                        estado = EstadoConexion.Ejecutando;
                        break;
                    case ConnectionState.Fetching:
                        estado = EstadoConexion.Conectando;
                        break;
                    case ConnectionState.Open:
                        estado = EstadoConexion.Abierto;
                        break;
                    default:
                        estado = EstadoConexion.Indefinido;
                        break;
                }

                return estado;
            }
        }
    }
}
