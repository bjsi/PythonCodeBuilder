using PythonCodeBuilder.Objects;
using System;

namespace PythonCodeBuilder.Converter.Transformers.Interfaces
{
    public interface IConstructorTransformer
    {
        bool Matches(Type type);
        void Transform(PyClass klass);
    }
}
