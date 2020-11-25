using PythonCodeBuilder.Statements;
using System;

namespace PythonCodeBuilder.Converter
{
    public interface IMethodReturnTransformer
    {
        bool Matches(Type returnType);
        void Transform(Type returnType, PyReturn pyReturn);
    }
}
