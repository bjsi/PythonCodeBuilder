using PythonCodeBuilder.Converter.Transformers.Interfaces;
using PythonCodeBuilder.Statements;
using System;
using System.Collections.Generic;

namespace PythonCodeBuilder.Converter.Transformers
{
    public class RegMemMethodReturnTransformer : IMethodReturnTransformer
    {
        public HashSet<Type> RegistryTypes { get; }

        public RegMemMethodReturnTransformer(HashSet<Type> regTypes)
        {
            this.RegistryTypes = regTypes;
        }

        public bool Matches(Type returnType) => RegistryTypes.Contains(returnType);

        public void Transform(Type returnType, PyReturn pyReturn)
        {
            pyReturn.RHS = returnType.Name + "(" + pyReturn.RHS + ")";
        }
    }
}
