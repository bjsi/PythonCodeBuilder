using PythonCodeBuilder.Objects;
using System;

namespace PythonCodeBuilder.Converter
{
    public interface IConstructorTransformer
    {
        bool Matches(Type type);
        void Transform(PyClass klass);
    }
}
