using System;

namespace JwtDemoSite.Extension
{
    public static class MyExtension
    {
        /// <summary>
        /// 字串轉Enum
        /// </summary>
        /// <typeparam name="T">目標Enum</typeparam>
        /// <param name="value">來源字串</param>
        /// <returns></returns>
        /// <remarks>https://stackoverflow.com/questions/16100/convert-a-string-to-an-enum-in-c-sharp</remarks>
        public static T ParseEnum<T>(this string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}