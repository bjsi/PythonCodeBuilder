using System;
using System.Collections.Generic;
using System.Text;

namespace PythonCodeBuilder.Helpers
{
    public static class ExceptionEx
    {
        public static void ThrowIfNullOrEmpty(this string obj, string errMsg)
        {
            if (string.IsNullOrEmpty(obj))
                throw new ArgumentNullException(errMsg);
        }


        /// <summary>
        /// Throws a <see cref="ArgumentNullException"/> if <paramref name="obj"/> is null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="errMsg"></param>
        public static void ThrowIfArgumentNull(this object obj, string errMsg)
        {
            if (obj == null)
                throw new ArgumentNullException(errMsg);
        }

        /// <summary>
        /// Throws a <see cref="NullReferenceException"/> if <paramref name="obj"/> is null
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="errMsg"></param>
        public static void ThrowIfNull(this object obj, string errMsg)
        {
            if (obj == null)
                throw new NullReferenceException(errMsg);
        }
    }
}
