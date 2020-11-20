using System;
using System.Collections.Generic;
using System.IO;

namespace PythonCodeBuilder
{
    public static class PyConst
    {
        public static string TemplateFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Templates");
        public static Dictionary<Type, string> TypeMap = new Dictionary<Type, string>
        {
            [typeof(int)] = "int",
            [typeof(string)] = "str"
        };
    }
}
