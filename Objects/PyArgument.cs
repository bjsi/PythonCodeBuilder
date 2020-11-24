using CodeBuilder.Helpers;
using CodeBuilder.Objects;
using PythonCodeBuilder.Helpers;

namespace PythonCodeBuilder.Objects
{
    public class PyArgument : Argument
    {
        public PyArgument(string name, string type)
        {
            name.ThrowIfNullOrEmpty("Failed to create argument because name was null or empty");
            type.ThrowIfNullOrEmpty("Failed to create argument because type was null or empty");
            this.Name = name;
            this.Type = type;
        }

        public override string ToString()
        {
            return Type.IsNullOrEmpty()
                ? $"{Name}"
                : $"{Name}: {Type}";
        } 
    }
}
