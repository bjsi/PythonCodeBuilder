using CodeBuilder.Objects;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyArgument : Argument
    {
        public PyArgument(string name, string type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
