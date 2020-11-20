using CodeBuilder.Helpers;
using CodeBuilder.Objects;

namespace PythonCodeBuilder.Objects
{
    public class PyArgument : Argument
    {
        public PyArgument(string name, string type)
        {
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
