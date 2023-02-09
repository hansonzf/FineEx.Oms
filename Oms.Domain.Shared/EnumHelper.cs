using System.ComponentModel;

namespace Oms.Domain.Shared
{
    public static class EnumHelper
    {
        public static string GetDescription(this Enum obj)
        {
            string objName = obj.ToString();
            Type t = obj.GetType();
            var fi = t.GetField(objName);
            var desc = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>();

            if (desc.Any())
                return desc.First().Description;
            else
                return string.Empty;
        }

        public static string GetDescription<T>(this T obj)
            where T : Enum
        {
            string objName = obj.ToString();
            Type t = typeof(T);
            var fi = t.GetField(objName);
            var desc = fi.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>();

            if (desc.Any())
                return desc.First().Description;
            else
                return string.Empty;
        }

        public static IEnumerable<string> GetDescriptions<T>()
            where T : Enum
        {
            List<string> descriptions = new List<string>();
            Type t = typeof(T);
            var fis = t.GetFields();
            if (!fis.Any())
                return Array.Empty<string>();

            foreach (var item in fis)
            {
                var desc = item.GetCustomAttributes(typeof(DescriptionAttribute), false).OfType<DescriptionAttribute>();
                if (desc.Any())
                    descriptions.Add(desc.First().Description);
            }


            return descriptions;
        }

        public static void GetEnum<T>(string a, ref T t)
            where T : Enum
        {
            foreach (T item in Enum.GetValues(typeof(T)))
            {
                if (GetDescription(item as Enum) == a)
                    t = item;
            }
        }
    }
}
