using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raya.Hrm.Shared.Library.Utilities.Converts
{
    public class JsonTypeHandlerObject : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = System.Text.Json.JsonSerializer.Serialize(value);
        }

        public object Parse(Type destinationType, object value)
        {
            return System.Text.Json.JsonSerializer.Deserialize(value as string, destinationType);
        }
    }
    public class JsonTypeHandlerObjectForList : SqlMapper.ITypeHandler
    {
        public void SetValue(IDbDataParameter parameter, object value)
        {
            parameter.Value = JsonConvert.SerializeObject(value);
        }

        public object Parse(Type destinationType, object value)
        {
            var raw = value as string;

            if (string.IsNullOrWhiteSpace(raw))
                return null;

            try
            {
                var token = JToken.Parse(raw);

                if (token.Type == JTokenType.Object)
                {
                    // If it's a single object, wrap it in a list
                    var singleDict = token.ToObject<Dictionary<string, object>>();
                    return new List<Dictionary<string, object>> { singleDict };
                }
                else if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<Dictionary<string, object>>>();
                }

                throw new JsonException("Unsupported JSON type for Units");
            }
            catch (Exception ex)
            {
                throw new DataException($"Failed to parse dynamic JSON into List<Dictionary<string, object>>: {raw}", ex);
            }
        }
    }


}
