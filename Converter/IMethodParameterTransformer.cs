﻿using PythonCodeBuilder.Expressions;
using System;
using System.Reflection;

namespace PythonCodeBuilder.Converter
{
    public interface IMethodParameterTransformer
    {
        bool Matches(ParameterInfo currentParam, Type CSType);
        PyMethodInvoke Transform(PyMethodInvoke methodInvoke);
    }
}
