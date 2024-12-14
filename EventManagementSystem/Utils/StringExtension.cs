using System.Text.RegularExpressions;

namespace Web.Utils
{
    public static class StringExtension
    {
        /// <summary>
        /// Split camel case string to array (e.g: ["Camel, "Case"])
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] SplitCamelCase(this string str)
        {
            string res = Regex.Replace(str, "(?<=[a-z])([A-Z])", " $1");
            return res.Split(' ');
        }
    }
}
