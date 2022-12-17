using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductConfig
{
    public class Config
    {
        public static string webPageUrl = "https://workspace.test/index.html";

        #region JSFunctions
        public static string LoadImages = "loadImages";
        public static string LoadNoInternet = "noInternetPage";
        public static string LoadingPage= "loadingPage";
        public static string EmptyResults = "noSearchResultPage";
        #endregion


        public static string BuildJavacriptFunction(string methodName, object[] args)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(methodName);
            stringBuilder.Append("(");
            if (args.Length > 0)
            {
                for (int i = 0; i < args.Length; i++)
                {
                    var parameter = args[i];
                    if (IsNumeric(parameter))
                    {
                        stringBuilder.Append(Convert.ToString(args[i]));
                    }
                    else if (parameter is bool)
                    {
                        stringBuilder.Append(args[i].ToString().ToLowerInvariant());
                    }
                    else
                    {
                        stringBuilder.Append("'");
                        stringBuilder.Append(EncodeScriptParam(parameter.ToString()));
                        stringBuilder.Append("'");
                    }
                    stringBuilder.Append(", ");
                }
                stringBuilder.Remove(stringBuilder.Length - 2, 2);
            }
            stringBuilder.Append(");");
            return stringBuilder.ToString();
        }

        private static string EncodeScriptParam(string str)
        {
            return str.Replace("\\", "\\\\")
                .Replace("'", "\\'")
                .Replace("\t", "\\t")
                .Replace("\r", "\\r")
                .Replace("\n", "\\n");
        }

        private static bool IsNumeric(object value)
        {
            return value is sbyte
                   || value is byte
                   || value is short
                   || value is ushort
                   || value is int
                   || value is uint
                   || value is long
                   || value is ulong
                   || value is float
                   || value is double
                   || value is decimal;
        }
    }
}
