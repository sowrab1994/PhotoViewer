using Microsoft.Win32;
using System;
using System.Text;

namespace ProductConfig
{
    public class Config
    {
        #region GenericConstants
        public static string webPageUrl = "https://workspace.test/index.html";
        #endregion

        #region JSFunctions
        public static string LoadImages = "loadImages";
        public static string LoadNoInternet = "noInternetPage";
        public static string LoadingPage= "loadingPage";
        public static string LoadNoResultsPage = "noSearchResultPage";
        #endregion

        #region FlickerConstants
        public static string apiKey = "c098ab4dc93e9a203a007ad613d1c414";
        public static string urlPrefix = "https://www.flickr.com/services/rest/?";
        public static string searchMethodName = "flickr.photos.search";
        public static string imageUrlPrefix = "https://live.staticflickr.com/";

        #endregion

        #region HelperMethods
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

        
        public static string ReadRegValueString(string path, string name)
        {
            string value = string.Empty;
            try
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(path))
                {
                    if (key != null)
                    {
                        Object o = key.GetValue(name);
                        if (o != null)
                        {
                            value = o.ToString();
                        }
                    }
                }
            }
            catch (Exception)
            { 
            }
            return value;
        }

        #endregion
    }
}
