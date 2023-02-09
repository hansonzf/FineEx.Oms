using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CollaborationServiceTest
{
    public static class JsonReaderUtility
    {
        public static string ReadFileAsObject(string folder, string file)
        {
            string jsonfile = $"D:\\PracticeBook\\WebApplication2\\CollaborationServiceTest\\{folder}\\{file}.json";//JSON文件路径

            using StreamReader sr = File.OpenText(jsonfile);
            using JsonTextReader reader = new JsonTextReader(sr);
            JObject o = (JObject)JToken.ReadFrom(reader);
            var value = o.ToString();
            return value;
        }

        public static string ReadFileAsArray(string folder, string file)
        {
            string jsonfile = $"D:\\PracticeBook\\WebApplication2\\CollaborationServiceTest\\{folder}\\{file}.json";//JSON文件路径

            using StreamReader sr = File.OpenText(jsonfile);
            using JsonTextReader reader = new JsonTextReader(sr);
            JArray o = (JArray)JToken.ReadFrom(reader);
            var value = o.ToString();
            return value;
        }
    }
}
