using System.Security.Cryptography;
using System.Text;

namespace Oms.Application.Contracts.CollaborationServices
{
    public static class SignUtility
    {
        public static string GetPostURL(string appKey, string appScret, string partitionId, string method, string postUrl, string Data)
        {
            IDictionary<String, String> parameters = new Dictionary<String, String>();
            parameters.Add("Method", method);
            parameters.Add("AppKey", appKey);
            parameters.Add("V", "1.0");
            parameters.Add("TimeStamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if (!string.IsNullOrEmpty(partitionId))
            {
                parameters.Add("PartitionID", partitionId);
            }
            string sign = SingToRequest(parameters, appScret, Data);
            parameters.Add("Sign", sign);

            StringBuilder data = new StringBuilder();
            foreach (String key in (IEnumerable<String>)parameters.Keys)
            {
                String value = parameters[key];
                if (value != null)
                {
                    data.Append(key);
                    data.Append('=');
                    data.Append(value);
                    data.Append('&');
                }
            }
            String result = data.Remove(data.Length - 1, 1).ToString();
            string url = postUrl;
            if (postUrl.IndexOf("?") < 0)
                url = postUrl + "?";
            url += result;

            return url;
        }

        public static string SingToRequest(IDictionary<string, string> parameters, string secret, string data)
        {
            IDictionary<string, string> sortParam = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortParam.GetEnumerator();

            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                string demKey = dem.Current.Key;
                string demValue = dem.Current.Value;
                if (demKey.Equals("sign"))
                {
                    continue;
                }
                else
                {
                    if (!string.IsNullOrEmpty(demKey) && !string.IsNullOrEmpty(demValue))
                    {
                        query.Append(demKey).Append(demValue);
                    }
                }
            }

            query.Append(data);
            query.Append(secret);

            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));
            md5.Clear();

            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }
            return result.ToString();
        }
    }
}
