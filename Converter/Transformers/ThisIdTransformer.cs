using PythonCodeBuilder.Converter.Transformers.Interfaces;
using PythonCodeBuilder.Expressions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PythonCodeBuilder.Converter.Transformers
{
    public class ThisIdTransformer : IMethodParameterTransformer
    {
        public HashSet<Type> RegistryTypes { get; }

        public ThisIdTransformer(HashSet<Type> regTypes)
        {
            this.RegistryTypes = regTypes;
        }

        public bool Matches(ParameterInfo paramInfo, Type CSType)
        {
            return (paramInfo.Name == "thisId" && RegistryTypes.Contains(CSType));
        }

        public PyMethodInvoke Transform(PyMethodInvoke pyMethodInvoke)
        {
            var fieldRef = new PyThisField("_id");
            pyMethodInvoke.WithArgument(fieldRef.ToString());
            return pyMethodInvoke;
        }
    }
}
