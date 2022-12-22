using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;

namespace BusinessLogic
{
    public class DLEMP_logic
    {
     
        public bool actualizar_dlemp(string cliente_id, string anio, string mes)
        {
            bool result = false;
            try
            {

                DLEMP dlemp_acc = new DataAccess.DLEMP();
                result = dlemp_acc.Actualizar_datos_DLEMP(cliente_id, anio, mes);
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }        
    }
}
