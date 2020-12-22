using PythonCodeBuilder.Converter.Transformers.Interfaces;
using PythonCodeBuilder.Expressions;
using PythonCodeBuilder.Objects;
using PythonCodeBuilder.Statements;
using System;
using System.Collections.Generic;

namespace PythonCodeBuilder.Converter.Transformers
{
    public class ConstructorTransformer : IConstructorTransformer
    {

        private const string IDField = "_id";
        public HashSet<Type> RegistryTypes { get; }

        public ConstructorTransformer(HashSet<Type> regTypes)
        {
            this.RegistryTypes = regTypes;
        }

        public bool Matches(Type type)
        {
            return RegistryTypes.Contains(type);
        }

        public void Transform(PyClass klass)
        {
            var consArgName = "json_rpc_dict";
            klass.WithField(new PyField(IDField, "int"));
            var consArg = new PyArgument(consArgName, "Dict[Any, Any]");
            var cons = new PyConstructor(consArg);
            var idFieldRef = new PyThisField(IDField).ToString();
            cons.WithStatement(new PyAssignment(idFieldRef, $"{consArgName}[\"Id\"]"));
            klass.WithConstructor(cons);
        }
    }
}
