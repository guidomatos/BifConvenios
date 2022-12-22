using System;
using System.Collections.Generic;
using System.Text;

namespace Resource
{
    public class Singleton<T> where T : new()
    {
        static T _t;
        public static T Create()
        {
            if (_t == null)
            {
                _t = new T();
            }

            return _t;
        }
    }
}
