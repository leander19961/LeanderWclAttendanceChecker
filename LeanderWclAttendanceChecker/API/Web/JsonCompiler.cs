using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace LeanderWclAttendanceChecker.API.Web
{
    public static class JsonParser
    {
        public static List<string> GetReportsFromJson(string jsonText)
        {
            List<string> result = null;

            JsonDocument json = JsonDocument.Parse(jsonText);
            JsonArray root = JsonArray.Create(json.RootElement);

            foreach (JsonObject report in root)
            {
                foreach (KeyValuePair<string, JsonNode> key in report)
                {
                    string _tmp = null;
                    if (key.Key.Equals("id"))
                    {
                        if (key.Value.AsValue().TryGetValue(out _tmp))
                            result.Add(_tmp);
                    }
                }
            }

            return result;
        }

        public static List<string> GetCharactersFromJson(string jsonText)
        {
            List<string> result = null;

            JsonDocument json = JsonDocument.Parse(jsonText);
            JsonObject root = JsonObject.Create(json.RootElement);

            JsonObject friendlies = null;
            foreach (KeyValuePair<string, JsonNode> key in root)
            {
                if (key.Key.Equals("friendlies"))
                {
                    friendlies = key.Value.AsObject();
                    break;
                }
            }

            foreach (KeyValuePair<string, JsonNode> key in friendlies)
            {
                string _tmp = null;
                if (key.Key.Equals("name"))
                {
                    if (key.Value.AsValue().TryGetValue(out _tmp))
                        result.Add(_tmp);
                }
            }

            return result;
        }
    }
}
