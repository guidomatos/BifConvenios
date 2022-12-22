using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Reflection;
using System.ComponentModel;
using System.Data.OleDb;
using ADODB;

namespace DataAccess
{
    public class Deuda_Cliente_IBS
    {
        private string connexion;

        public Deuda_Cliente_IBS()
        {                         
            //connexion = ConfigurationManager.AppSettings["ConnectionString"].Trim();               
            connexion = BIFUtils.WS.Utils.CadenaConexion("ConnectionString"); 
        }

        public string DescifrarCadenaConexion(string cadena_conexion)
        {
            string cadena_conexion_antes_password_cifrado = string.Empty;
            string password_cifrado = string.Empty;
            string cadena_conexion_despues_password_cifrado = string.Empty;
            string temporal = string.Empty;
            string password_descifrado = string.Empty;
            string cadena_conexion_descifrada = string.Empty;

            try
            {
                cadena_conexion_antes_password_cifrado = cadena_conexion.Substring(0, cadena_conexion.IndexOf("Password") + 9);
                temporal = cadena_conexion.Substring(cadena_conexion.IndexOf("Password") + 9);
                cadena_conexion_despues_password_cifrado = temporal.Substring(temporal.IndexOf(";"));
                password_cifrado = temporal.Substring(0, temporal.IndexOf(";"));                
                //bifwebservices.DecodificarClave c = new bifwebservices.DecodificarClave();   
                //password_descifrado = c.Decodifica(password_cifrado);
                password_descifrado = password_cifrado;
                cadena_conexion_descifrada = cadena_conexion_antes_password_cifrado + password_descifrado + cadena_conexion_despues_password_cifrado;
            }

            catch (Exception ex)
            {
                cadena_conexion_descifrada = "";
            }

            return cadena_conexion_descifrada;
        }

        public List<int> lista_mes_DLCCR()
        {
            List<int> LISTA_mes_DLCCR = new List<int>();

            SqlConnection con = new SqlConnection(connexion);
         
            SqlCommand cmd = new SqlCommand("MES_DLCCR", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                LISTA_mes_DLCCR.Add(Convert.ToInt32(  reader.GetValue (reader.GetOrdinal("DLVCM"))));
            }

            return LISTA_mes_DLCCR;


        }

        public List<int> lista_anio_DLCCR()
        {
            List<int> LISTA_ANIO_DLCCR = new List<int>();

            SqlConnection con = new SqlConnection(connexion);
           

            SqlCommand cmd = new SqlCommand("ANIO_DLCCR", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
               LISTA_ANIO_DLCCR.Add(Convert.ToInt32(reader.GetValue(reader.GetOrdinal("DLVCA"))));
            }

            return LISTA_ANIO_DLCCR;

        }

        public DataTable  lista_deuda_empresa_DLCCR(string mes , string anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTA_TOTAL_MES_DEUDA_EMPRES", con);
            cmd.CommandType = CommandType.StoredProcedure;

            
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
                        
            con.Open();
          
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("Lista_Deuda_Empresa");

                DataColumn dc_codigo_empresa = new DataColumn("Codigo");
                DataColumn dc_nombre_empresa = new DataColumn("Nombre");
                DataColumn dc_mes = new DataColumn("mes");
                DataColumn dc_anio = new DataColumn("anio");
                DataColumn dc_nro_clientes_mes = new DataColumn("N_Clientes_Mes");
                DataColumn dc_deuda_mes = new DataColumn("DEUDA_MES");
                DataColumn dc_nro_total_clientes = new DataColumn("N_total_clientes");
                DataColumn dc_deuda_total = new DataColumn("DEUDA_TOTAL");

                dt.Columns.Add(dc_codigo_empresa);
                dt.Columns.Add(dc_nombre_empresa);
                dt.Columns.Add(dc_mes);
                dt.Columns.Add(dc_anio);
                dt.Columns.Add(dc_nro_clientes_mes);
                dt.Columns.Add(dc_deuda_mes);
                dt.Columns.Add(dc_nro_total_clientes);
                dt.Columns.Add(dc_deuda_total);
              
                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();
                                                         
                    rs[0] = reader.GetString(reader.GetOrdinal("DLECUN"));
                    rs[1] = reader.GetString(reader.GetOrdinal("DLEDSC"));
                    rs[2] = reader.GetString(reader.GetOrdinal("MES"));
                    rs[3] = reader.GetString(reader.GetOrdinal("ANIO"));
                    rs[4] = Class_Formato.formatodecimal(reader.GetInt32(reader.GetOrdinal("CANTIDAD_TOTAL_PAGARES_MES")));
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("DEUDA_MES")));
                    rs[6] = Class_Formato.formatodecimal(reader.GetInt32(reader.GetOrdinal("CANTIDAD_TOTAL_PAGARES_DEUDA")));
                    rs[7] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("DEUDA_TOTAL")));

                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }


        public DataTable   LISTAR_RESULTADO_DEUDA_TOTAL(int codigo_cliente, string anio, string mes)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTAR_RESULTADO_DEUDA_TOTAL", con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@codigo_cliente", codigo_cliente));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("Lista_Deuda_detalle_total");

                DataColumn dc_DLEDSC = new DataColumn("DLEDSC");
                DataColumn dc_total_deuda = new DataColumn("total_deuda");
                DataColumn dc_total_cantidad = new DataColumn("total_cantidad");

                dt.Columns.Add(dc_DLEDSC);
                dt.Columns.Add(dc_total_deuda);
                dt.Columns.Add(dc_total_cantidad);
              

                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetString(reader.GetOrdinal("DLEDSC"));
                    rs[1] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("total_deuda")));
                    rs[2] = reader.GetInt32(reader.GetOrdinal("total_cantidad"));
                   
                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }


        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_IBS(int codigo_cliente, string anio, string mes)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTA_DETALLE_CUOTA_EMPRESA_IBS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@codigo_cliente", codigo_cliente));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("Lista_Deuda_detalle_total");

                DataColumn dc_PAGARE = new DataColumn("PAGARE");
                DataColumn dc_NOMBRE = new DataColumn("NOMBRE");
                DataColumn dc_DOCUMENTO = new DataColumn("DOCUMENTO");
                DataColumn dc_ANIO = new DataColumn("ANIO");
                DataColumn dc_MES = new DataColumn("MES");
                DataColumn dc_SALDO_DEUDA = new DataColumn("SALDO_DEUDA");

                dt.Columns.Add(dc_PAGARE);
                dt.Columns.Add(dc_NOMBRE);
                dt.Columns.Add(dc_DOCUMENTO);
                dt.Columns.Add(dc_ANIO);
                dt.Columns.Add(dc_MES);
                dt.Columns.Add(dc_SALDO_DEUDA);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetDecimal(reader.GetOrdinal("PAGARE"));
                    rs[1] = reader.GetString(reader.GetOrdinal("NOMBRE"));
                    rs[2] = reader.GetString(reader.GetOrdinal("DOCUMENTO"));
                    rs[3] = reader.GetString(reader.GetOrdinal("ANIO"));
                    rs[4] = reader.GetString(reader.GetOrdinal("MES"));
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("SALDO_DEUDA")));

                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }


        public DataTable LISTAR_RESULTADO_DEUDA_MES(int codigo_cliente,string mes, string anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTAR_RESULTADO_DEUDA_MES", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@codigo_cliente", codigo_cliente));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("LISTAR_RESULTADO_DEUDA_MES");

                DataColumn dc_DLEDSC = new DataColumn("DLEDSC");
                DataColumn dc_total_deuda = new DataColumn("total_deuda_mes");
                DataColumn dc_total_cantidad = new DataColumn("cantidad_pagares");

                dt.Columns.Add(dc_DLEDSC);
                dt.Columns.Add(dc_total_deuda);
                dt.Columns.Add(dc_total_cantidad);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetString(reader.GetOrdinal("DLEDSC"));
                    rs[1] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("total_deuda_mes")));
                    rs[2] =  Class_Formato.formatodecimal(reader.GetInt32(reader.GetOrdinal("cantidad_pagares")));

                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;


        }



        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS(int codigo_cliente,string mes, string anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@codigo_cliente", codigo_cliente));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS");

                DataColumn dc_PAGARE = new DataColumn("PAGARE");
                DataColumn dc_NOMBRE = new DataColumn("NOMBRE");
                DataColumn dc_DOCUMENTO = new DataColumn("DOCUMENTO");
                DataColumn dc_ANIO = new DataColumn("ANIO");
                DataColumn dc_MES = new DataColumn("MES");
                DataColumn dc_SALDO_DEUDA = new DataColumn("SALDO_DEUDA");


                dt.Columns.Add(dc_PAGARE);
                dt.Columns.Add(dc_NOMBRE);
                dt.Columns.Add(dc_DOCUMENTO);
                dt.Columns.Add(dc_ANIO);
                dt.Columns.Add(dc_MES);
                dt.Columns.Add(dc_SALDO_DEUDA);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetDecimal(reader.GetOrdinal("PAGARE"));
                    rs[1] = reader.GetString(reader.GetOrdinal("NOMBRE"));
                    rs[2] = reader.GetString(reader.GetOrdinal("DOCUMENTO"));
                    rs[3] = reader.GetString(reader.GetOrdinal("ANIO"));
                    rs[4] = reader.GetString(reader.GetOrdinal("MES"));
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("SALDO_DEUDA")));
                    

                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }



        public DataTable LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS_otros(int codigo_cliente, string mes, string anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTA_DETALLE_CUOTA_EMPRESA_IBS_OTROS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@codigo_cliente", codigo_cliente));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("LISTA_DETALLE_CUOTA_EMPRESA_MES_IBS");

                DataColumn dc_PAGARE = new DataColumn("PAGARE");
                DataColumn dc_NOMBRE = new DataColumn("NOMBRE");
                DataColumn dc_DOCUMENTO = new DataColumn("DOCUMENTO");
                DataColumn dc_ANIO = new DataColumn("ANIO");
                DataColumn dc_MES = new DataColumn("MES");
                DataColumn dc_SALDO_DEUDA = new DataColumn("SALDO_DEUDA");


                dt.Columns.Add(dc_PAGARE);
                dt.Columns.Add(dc_NOMBRE);
                dt.Columns.Add(dc_DOCUMENTO);
                dt.Columns.Add(dc_ANIO);
                dt.Columns.Add(dc_MES);
                dt.Columns.Add(dc_SALDO_DEUDA);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetDecimal(reader.GetOrdinal("PAGARE"));
                    rs[1] = reader.GetString(reader.GetOrdinal("NOMBRE"));
                    rs[2] = reader.GetString(reader.GetOrdinal("DOCUMENTO"));
                    rs[3] = reader.GetString(reader.GetOrdinal("ANIO"));
                    rs[4] = reader.GetString(reader.GetOrdinal("MES"));
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("SALDO_DEUDA")));


                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }


        public DataTable detalle_pagare(string  pagare , string mes, string anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("DETALLE_PAGARE", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            cmd.Parameters.Add(new SqlParameter("@PAGARE", pagare));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("DETALLE_PAGARE");

                DataColumn dc_MONEDA = new DataColumn("MONEDA");
                DataColumn dc_ANIO = new DataColumn("ANIO");
                DataColumn dc_MES = new DataColumn("MES");
                DataColumn dc_MONTO_MES = new DataColumn("MONTO_MES");
                DataColumn dc_MONTO_PAGADO = new DataColumn("MONTO_PAGADO");
                DataColumn dc_ITF = new DataColumn("ITF");


                dt.Columns.Add(dc_MONEDA);
                dt.Columns.Add(dc_ANIO);
                dt.Columns.Add(dc_MES);
                dt.Columns.Add(dc_MONTO_MES);
                dt.Columns.Add(dc_MONTO_PAGADO);
                dt.Columns.Add(dc_ITF);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetString(reader.GetOrdinal("MONEDA"));
                    rs[1] = (reader.GetDecimal(reader.GetOrdinal("ANIO"))+ 2000).ToString() ;
                    rs[2] = reader.GetDecimal(reader.GetOrdinal("MES")).ToString() ;
                    rs[3] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("MONTO_MES")));
                    rs[4] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("MONTO_PAGADO")));
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("ITF")));


                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }



        public DataTable cLIENTES_CUOTAS_NEGATIVAS (int mes, int anio)
        {

            DataTable dt = null;
            try
            {
               

                SqlConnection con = new SqlConnection(connexion);
                SqlCommand cmd = new SqlCommand("LISTA_CUOTAS_NEGATIVAS", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add(new SqlParameter("@MES", mes));
                cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
                //cmd.Parameters.Add(new SqlParameter("@PAGARE", pagare));

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    dt = new DataTable("LISTA_CUOTAS_NEGATIVAS");

                    DataColumn dc_NOMBRE_EMPRESA = new DataColumn("DLNCC");
                    DataColumn dc_PAGARE = new DataColumn("DEAACC");
                    DataColumn dc_CLIENTE = new DataColumn("DLNCL");
                    DataColumn dc_CAPITAL = new DataColumn("DEAPRI");
                    DataColumn dc_INTERES = new DataColumn("DEAMEI");
                    DataColumn dc_MORA = new DataColumn("DEAMEM");


                    dt.Columns.Add(dc_NOMBRE_EMPRESA);
                    dt.Columns.Add(dc_PAGARE);
                    dt.Columns.Add(dc_CLIENTE);
                    dt.Columns.Add(dc_CAPITAL);
                    dt.Columns.Add(dc_INTERES);
                    dt.Columns.Add(dc_MORA);


                    while (reader.Read())
                    {
                        DataRow rs = dt.NewRow();

                        rs[0] = reader.GetString(reader.GetOrdinal("DLNCC"));
                        rs[1] = (reader.GetString(reader.GetOrdinal("DEAACC")));
                        rs[2] = reader.GetString(reader.GetOrdinal("DLNCL"));
                        rs[3] = (reader.GetDecimal(reader.GetOrdinal("DEAPRI"))).ToString();
                        rs[4] = (reader.GetDecimal(reader.GetOrdinal("DEAMEI"))).ToString();
                        rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("DEAMEM")));


                        dt.Rows.Add(rs);
                        rs = null;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }


        public DataTable cLIENTES_CUOTAS_NEGATIVAS_IBS(int mes, int anio)
        {

            DataTable dt = null;

            SqlConnection con = new SqlConnection(connexion);
            SqlCommand cmd = new SqlCommand("LISTA_CUOTAS_NEGATIVAS_IBS", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add(new SqlParameter("@MES", mes));
            cmd.Parameters.Add(new SqlParameter("@ANIO", anio));
            //cmd.Parameters.Add(new SqlParameter("@PAGARE", pagare));

            con.Open();

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                dt = new DataTable("LISTA_CUOTAS_NEGATIVAS_IBS");

                DataColumn dc_DLEDSC = new DataColumn("DLEDSC");
                DataColumn dc_DLNP = new DataColumn("DLNP");
                DataColumn dc_NOMBRE = new DataColumn("NOMBRE");
                DataColumn dc_ANIO = new DataColumn("ANIO");
                DataColumn dc_MES = new DataColumn("MES");
                DataColumn dc_CUOTA_COBRAR = new DataColumn("CUOTA_COBRAR");


                dt.Columns.Add(dc_DLEDSC);
                dt.Columns.Add(dc_DLNP);
                dt.Columns.Add(dc_NOMBRE);
                dt.Columns.Add(dc_ANIO);
                dt.Columns.Add(dc_MES);
                dt.Columns.Add(dc_CUOTA_COBRAR);


                while (reader.Read())
                {
                    DataRow rs = dt.NewRow();

                    rs[0] = reader.GetString(reader.GetOrdinal("DLEDSC"));
                    rs[1] = (reader.GetDecimal(reader.GetOrdinal("DLNP"))).ToString();
                    rs[2] = reader.GetString(reader.GetOrdinal("NOMBRE"));
                    rs[3] = (reader.GetDecimal(reader.GetOrdinal("ANIO")) + 2000).ToString();
                    rs[4] = (reader.GetDecimal(reader.GetOrdinal("MES"))).ToString();
                    rs[5] = Class_Formato.formatodecimal(reader.GetDecimal(reader.GetOrdinal("CUOTA_COBRAR")));


                    dt.Rows.Add(rs);
                    rs = null;
                }
            }

            return dt;

        }



    }
}
