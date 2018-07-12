using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace ResearchNow.SamplifyAPIClient
{
    internal static class Util
    {
        private const string SamplifyDateTimeFormat = "yyyy/MM/dd HH:mm:ss";

        internal static string Serialize(object obj)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    var ser = new DataContractJsonSerializer(obj.GetType());
                    ser.WriteObject(ms, obj);
                    ms.Position = 0;
                    StreamReader sr = new StreamReader(ms);
                    return sr.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
        }

        internal static object Deserialize(string json, Type t)
        {
            using (var ms = new MemoryStream(Encoding.Unicode.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(t);
                return serializer.ReadObject(ms);
            }
        }

        internal static DateTime? ConvertToDateTimeNullable(string datetime)
        {
            if (string.IsNullOrWhiteSpace(datetime)) { return null; }
            try
            {
                return DateTime.ParseExact(datetime,
                                           SamplifyDateTimeFormat, CultureInfo.InvariantCulture);
            }
            catch { return null; }
        }
    }
}
