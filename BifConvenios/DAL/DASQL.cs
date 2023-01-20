using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Resource;

namespace DAL
{
    public class DASQL
    {
        protected internal string strConnectionName;
        private SqlTransaction oTransaccion;
        private SqlConnection con;

        public string DescrifarCadenaConexion(string pstrCadenaCifrada)
        {
            string strCadenaDescifrada = string.Empty;
            string strCadenaConexionAntesPasswordCifrado = string.Empty;
            string strPasswordCifrado = string.Empty;
            string strCadenaConexionDespuesPasswordCifrado = string.Empty;
            string strTemporal = string.Empty;            
            string strPasswordDescifrado = string.Empty; 
            
            //bifwebservices.DecodificarClave c = new DAL.bifwebservices.DecodificarClave();

            //try
            //{
            //    strCadenaConexionAntesPasswordCifrado = pstrCadenaCifrada.Substring(0, pstrCadenaCifrada.IndexOf("Password") + 9);
            //    strTemporal = pstrCadenaCifrada.Substring(pstrCadenaCifrada.IndexOf("Password") + 9);
            //    strCadenaConexionDespuesPasswordCifrado = strTemporal.Substring(strTemporal.IndexOf(";"));
            //    strPasswordCifrado = strTemporal.Substring(0, strTemporal.IndexOf(";"));
            //    strPasswordDescifrado = c.Decodifica(strPasswordCifrado);                
            //    strCadenaDescifrada = strCadenaConexionAntesPasswordCifrado + strPasswordDescifrado + strCadenaConexionDespuesPasswordCifrado;
            //    return strCadenaDescifrada;

            //}
            //catch (Exception ex)
            //{
            //    return "";
            //} 

            strCadenaDescifrada = pstrCadenaCifrada;
            if (ConfigurationManager.AppSettings["cifrado"].ToString() == "1")
            {
                bifwebservices.DecodificarClave c = new DAL.bifwebservices.DecodificarClave();

                try
                {
                    strCadenaConexionAntesPasswordCifrado = pstrCadenaCifrada.Substring(0, pstrCadenaCifrada.IndexOf("Password") + 9);
                    strTemporal = pstrCadenaCifrada.Substring(pstrCadenaCifrada.IndexOf("Password") + 9);
                    strCadenaConexionDespuesPasswordCifrado = strTemporal.Substring(strTemporal.IndexOf(";"));
                    strPasswordCifrado = strTemporal.Substring(0, strTemporal.IndexOf(";"));
                    strPasswordDescifrado = c.Decodifica(strPasswordCifrado);
                    strCadenaDescifrada = strCadenaConexionAntesPasswordCifrado + strPasswordDescifrado + strCadenaConexionDespuesPasswordCifrado;

                }
                catch (Exception ex)
                {
                    strCadenaDescifrada = "";
                }            
            }
            return strCadenaDescifrada;
            
        }

        public string ConnectionAS400()
        {
            return BIFUtils.WS.Utils.CadenaConexion("AS400-ConnectionString-Convenios");
            //return DescrifarCadenaConexion(ConfigurationManager.AppSettings["AS400-ConnectionString-Convenios"].ToString());
        }

        public DASQL()
        {
            strConnectionName = BIFUtils.WS.Utils.CadenaConexion("ConnectionString");
            //strConnectionName = DescrifarCadenaConexion(ConfigurationManager.AppSettings["ConnectionString"].ToString());
        }

        public DASQL(string ConnectionString)
        {
            strConnectionName = BIFUtils.WS.Utils.CadenaConexion(ConnectionString);
            //strConnectionName = ConfigurationManager.AppSettings[ConnectionString].ToString(); // Constants.NombreConexion;
        } 
       
        public void AddParameter(SqlCommand Command, string strName, object objValor, SqlDbType pt)
        {
            AddParameter(Command,strName,objValor,pt,ParameterDirection.Input);
        }

        public void AddParameter(SqlCommand Command, string strName, object objValor, SqlDbType pt, ParameterDirection paramDirection)
        {
            SqlParameter p = new SqlParameter();
            switch (pt)
            {
                case SqlDbType.DateTime:
                    p = new SqlParameter(strName, SqlDbType.DateTime);
                    p.Value = (objValor != null) ? objValor : null;
                    break;
                case SqlDbType.Float:
                    p = new SqlParameter(strName, SqlDbType.Float);
                    p.Value = (objValor != null) ? objValor : 0;
                    break;
                case SqlDbType.Int:
                    p = new SqlParameter(strName, SqlDbType.Int);
                    p.Value = (objValor != null) ? objValor : 0;
                    break;
                case SqlDbType.Bit:
                    p = new SqlParameter(strName, SqlDbType.Bit);
                    p.Value = (objValor != null) ? objValor : 0;
                    break;
                case SqlDbType.VarBinary:
                    p = new SqlParameter(strName, SqlDbType.VarBinary);
                    p.Value = (objValor != null) ? objValor : new Byte[] { };
                    break;
                case SqlDbType.VarChar:
                    p = new SqlParameter(strName, SqlDbType.VarChar);
                    p.Value = (objValor != null) ? objValor : "";
                    break;
                case SqlDbType.Xml:
                    p = new SqlParameter(strName, SqlDbType.Xml);
                    p.Value = (objValor != null) ? objValor : "";
                    break;
                default:
                    p = new SqlParameter(strName, SqlDbType.VarChar);
                    p.Value = (objValor != null) ? objValor : "";
                    break;
            }
            p.Direction = paramDirection;
            Command.Parameters.Add(p);
        }

        public String OutputParameter(SqlCommand Command, string strName)
        {
            if (Command.Parameters[strName].Value == null)
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, "Error al Traer el Parametro de Salida: " + strName);
            else if (Command.Parameters[strName].Value.ToString() == "")
                return "";
            else
                return Command.Parameters[strName].Value.ToString();
        }

        public void CommandProperties(ref SqlCommand Command, System.String strCommandName)
        {
            if (con == null)
            {
                ConnectionOpen();
            }

            Command = (oTransaccion != null) ? new SqlCommand(strCommandName, con, oTransaccion) : new SqlCommand(strCommandName, con);
            Command.Transaction = oTransaccion;
            Command.CommandType = CommandType.StoredProcedure;
            Command.CommandTimeout = 0;
        }

        public DataTable ExecuteReader(SqlCommand Command)
        {
            return ExecuteReader(Command,"Datatable");
        }

        public DataTable ExecuteReader(SqlCommand Command, string StrNameDT)
        {
            try
            {
                SqlDataReader fila;
                fila = Command.ExecuteReader();
                DataTable dstable = new DataTable(StrNameDT);
                dstable.Load(fila);
                fila.Close();
                return dstable;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)// si es un error controlado por sql (RAISEERROR)
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, ex.Message);
                }
                else if (ex.Number == 2627)// si es un error duplicado por llave Unique
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, clsConstantsGeneric.UniqueKey, ex.Message);
                }
                else
                {
                    throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public DataSet ExecuteReaderDS(SqlCommand Command)
        {
            return ExecuteReaderDS(Command,"Datatable");
        }

        public DataSet ExecuteReaderDS(SqlCommand Command, string StrNameDT)
        {
            try
            {
                SqlDataAdapter fila = new SqlDataAdapter();
                DataSet ds = new DataSet(StrNameDT);
                fila.SelectCommand = Command;
                fila.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)// si es un error controlado por sql (RAISEERROR)
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, ex.Message);
                }
                else if (ex.Number == 2627)// si es un error duplicado por llave Unique
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, clsConstantsGeneric.UniqueKey, ex.Message);
                }
                else
                {
                    throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public void ExecuteNonQuery(SqlCommand Command)
        {
            try
            {
                Command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 50000)// si es un error controlado por sql (RAISEERROR)
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, ex.Message);
                }
                else if (ex.Number == 2627)// si es un error duplicado por llave Unique
                {
                    throw new HandledException((int)enumGeneric.DataBaseRaiseError, clsConstantsGeneric.UniqueKey, ex.Message);
                }
                else
                {
                    throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
                }
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public void ConnectionOpen()
        {
            ConnectionOpen(EstablecerConexion());
        }

        private void ConnectionOpen(SqlConnection oConnection)
        {
            try
            {
                if (oConnection.State == ConnectionState.Closed)
                    oConnection.Open();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public void ConnectionClose()
        {
            ConnectionClose(EstablecerConexion());
        }

        private void ConnectionClose(SqlConnection oConnection)
        {
            try
            {
                if (oConnection.State == ConnectionState.Open)
                    oConnection.Close();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        private SqlConnection EstablecerConexion()
        {
            if (con == null)
                con = new SqlConnection(strConnectionName);
            return con;
        }

        public void TransactionStart()
        {
            try
            {
                oTransaccion = con.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public void TransactionConfirm()
        {
            try
            {
                oTransaccion.Commit();
            }
            catch (Exception ex)
            {
                throw new HandledException((int)enumGeneric.DataBaseError, clsConstantsGeneric.DataBaseError, ex.Message);
            }
        }

        public SqlTransaction TransactionGet()
        {
            return oTransaccion;
        }

        public void TransactionCancel()
        {
            if (oTransaccion != null && oTransaccion.Connection != null)
                oTransaccion.Rollback();
        }
    }
}
