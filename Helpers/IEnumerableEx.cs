﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PythonCodeBuilder.Helpers
{
    public static class IEnumerableEx
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> obj)
        {
            return obj == null || !obj.Any();
        }
    }
}
