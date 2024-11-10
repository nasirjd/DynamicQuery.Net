using System;
using System.CodeDom;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DynamicQuery.Net.Dto.Input;
using DynamicQuery.Net.Enums;
using DynamicQuery.Net.Services;
using Newtonsoft.Json.Linq;

namespace DynamicQuery.Net.Utility
{
    public static class JArrayUtil
    {
        public static bool IsJArray(object value)
        {
            return value.GetType() == typeof(JArray);
        }
    }
}
