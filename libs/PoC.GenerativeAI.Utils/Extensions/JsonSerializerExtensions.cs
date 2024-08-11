using System.Text.Json;

namespace PoC.GenerativeAI.Utils.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static bool TryDeserialize<T>(this string json, out T result) where T : class
        {
            try
            {
                json = ExtractJson(json);
                result = JsonSerializer.Deserialize<T>(json);
                return true;
            }
            catch (JsonException)
            {
                result = null;
                return false;
            }
        }

        private static string ExtractJson(string input)
        {
            // Find the start index of the JSON content
            int startIndex = input.IndexOf("```json", StringComparison.OrdinalIgnoreCase);
            if (startIndex == -1)
                return input;

            // Find the end index of the JSON content
            int endIndex = input.IndexOf("```", startIndex + 1, StringComparison.OrdinalIgnoreCase);
            if (endIndex == -1)
                return input;

            // Extract the JSON content
            int jsonStartIndex = startIndex + "```json".Length;
            int jsonLength = endIndex - jsonStartIndex;
            string extractedJson = input.Substring(jsonStartIndex, jsonLength);

            return extractedJson.Trim();
        }
    }
}
