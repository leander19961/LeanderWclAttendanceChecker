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
            List<string> result = new List<string>();

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
            List<string> result = new List<string>();

            JsonDocument json = JsonDocument.Parse(jsonText);
            JsonObject root = JsonObject.Create(json.RootElement);

            JsonArray exportedCharacters = null;
            foreach (KeyValuePair<string, JsonNode> key in root)
            {
                if (key.Key.Equals("exportedCharacters"))
                {
                    exportedCharacters = key.Value.AsArray();
                    break;
                }
            }

            foreach (JsonObject exportedCharacter in exportedCharacters)
            {
                foreach (KeyValuePair<string, JsonNode> key in exportedCharacter)
                {
                    string _tmp = null;
                    if (key.Key.Equals("name"))
                    {
                        if (key.Value.AsValue().TryGetValue(out _tmp))
                            result.Add(_tmp);
                    }
                }
            }

            return result;
        }
    }
}
