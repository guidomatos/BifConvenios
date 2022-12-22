using System;
using System.Collections.Generic;
using System.Text;

namespace Resource
{
    public class Anios
    {
        private int _value;

        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Anios(int pintValue)
        {
            _value = pintValue;
        }
    }

    public class Meses
    {
        private string _monthName;
        private string _monthOrder;
        private bool _wZero;

        public string MonthName
        {
            get { return _monthName; }
            set { _monthName = value; }
        }

        public string MonthOrder
        {
            get 
            {
                if (_wZero)
                {
                    return _monthOrder.Length == 1 ? "0" + _monthOrder : _monthOrder;
                }
                else
                {
                    return _monthOrder.Length == 1 ? _monthOrder : _monthOrder;
                }
            }
            set { _monthOrder = value; }
        }

        public Meses(string pstrName, string pstrOrder)
        {
            _monthName = pstrName;
            _monthOrder = pstrOrder;
        }

        public Meses(string pstrName, string pstrOrder, bool pboolWzero)
        {
            _monthName = pstrName;
            _monthOrder = pstrOrder;
            _wZero = pboolWzero;
        }
    }

    public static class clsPeriodo
    {
        public static string NombreMes(int pintMonth)
        {
            switch (pintMonth)
            {
                case 1:
                    return "Enero";                    
                case 2:
                    return "Febrero";                    
                case 3:
                    return "Marzo";                    
                case 4:
                    return "Abril";
                case 5:
                    return "Mayo";                    
                case 6:
                    return "Junio";                    
                case 7:
                    return "Julio";                    
                case 8:
                    return "Agosto";
                case 9:
                    return "Septiembre";
                case 10:
                    return "Octubre";
                case 11:
                    return "Noviembre";
                case 12:
                    return "Diciembre";
                default:
                    return "";
            }
        }
    }
}

