using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Services;
using Newtonsoft.Json.Linq;

namespace DynamicQuery.Net.Utility
{
    public class JArrayUtil
    {
        public static bool IsJArray(object value)
        {
            return value.GetType() == typeof(JArray);
        }
        public static JArray GetJArray(object value)
        {
            return (JArray)value;
        }
        public static List<object> GetValues(object value)
        {
            return GetJArray(value).Select(p => ((JValue)p).Value).ToList();
        }

    }
}
