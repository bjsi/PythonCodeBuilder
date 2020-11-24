using PythonCodeBuilder.Expressions;
using PythonCodeBuilder.Objects;
using PythonCodeBuilder.Statements;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PythonCodeBuilder.Converter
{
    public interface IMethodTransformer
    {
        bool Matches(ParameterInfo paramInfo, Type CSType);
        PyMethodInvoke Transform(PyMethodInvoke methodInvoke);
    }
}
