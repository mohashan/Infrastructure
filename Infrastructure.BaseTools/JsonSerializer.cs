using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.BaseTools
{
    public static class JsonSerializer
    {
        public static string Serialize(this object obj,int maxDepth = 0,JsonSerializerDefaults defaultSerializer = JsonSerializerDefaults.Web)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }

            JsonSerializerOptions options = new JsonSerializerOptions(defaultSerializer)
            {
                AllowTrailingCommas = true,
                MaxDepth = maxDepth,
                WriteIndented = true,
            };

            return System.Text.Json.JsonSerializer.Serialize(obj,options);
        }

        public static T? Deserialize<T>(this string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                throw new ArgumentException("Input string is empty");
            }

            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
        }
    }
}
