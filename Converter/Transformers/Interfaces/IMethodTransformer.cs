using PythonCodeBuilder.Statements;
using System;

namespace PythonCodeBuilder.Converter.Transformers.Interfaces
{
    public interface IMethodReturnTransformer
    {
        bool Matches(Type returnType);
        void Transform(Type returnType, PyReturn pyReturn);
    }
}
