using System.Data;
using System.Data.OleDb;
using ADODB;
using BIFUtils.WS;

namespace DataAccess
{
    public class DLEMP
    {
        private string connexion;

        public DLEMP()
        {
            //connexion = ConfigurationManager.AppSettings["AS400_ConnectionString_Convenios"].Trim();   
            //connexion = ConfigurationManager.AppSettings["ConnectionString"].Trim();   
            //connexion = DescifrarCadenaConexion(ConfigurationManager.AppSettings["AS400_ConnectionString_Convenios"].Trim());            

            connexion = Utils.CadenaConexion("AS400-ConnectionString-Convenios");
        }

        public DataSet dDLEMP()
        {
            Connection conn = new Connection();
            Recordset result = new Recordset();

            conn.CursorLocation = CursorLocationEnum.adUseClient;
            conn.Open(connexion, "", "", -1);

            string str = " SELECT     DLECUN, DLEDSC, DLEAEN, DLEMEN, DLEDEN, DLEAPA, DLEMPA, DLEDPA " +
                         " FROM       DLEMP " +
                         " ORDER BY DLEDSC";

            result = conn.Execute(str, out object gg, 0);

            result.ActiveConnection = null;

            OleDbDataAdapter da = new OleDbDataAdapter();
            DataSet ds = new DataSet("dlemp");
            da.Fill(ds, result, "data_dlemp");

            conn.Close();
            conn = null;

            return ds;
        }

        public bool Actualizar_datos_DLEMP(string Cliente_ID, string anio, string mes)
        {
            bool result_bool = false;

            try
            {
                Connection conn = new Connection();
                Recordset result = new Recordset();

                conn.CursorLocation = CursorLocationEnum.adUseClient;
                conn.Open(connexion, "", "", -1);

                //string str = " UPDATE  BIFCYFILES.DLEMP SET DLEAEN = " + anio.Trim() + ", DLEMEN = " + mes.Trim() +
                //             " WHERE  DLECUN =" + Cliente_ID.Trim();

                string str = " UPDATE DLEMP SET DLEAEN = " + anio.Trim() + ", DLEMEN = " + mes.Trim() +
                             " WHERE  DLECUN =" + Cliente_ID.Trim();

                result = conn.Execute(str, out object gg, 0);
                result_bool = true;
            }
            catch
            {
                result_bool = false;
            }

            return result_bool;
        }
    }
}