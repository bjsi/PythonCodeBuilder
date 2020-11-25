using CodeBuilder.Objects;
using PythonCodeBuilder.Helpers;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyField : Field
    {

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Field.Mustache");

        public PyField(string name, string type, string value = null)
            : base(TemplateFilePath)
        {
            name.ThrowIfNullOrEmpty("Failed to create python field because the name was null or empty");
            type.ThrowIfNullOrEmpty("Failed to create python field because the type was null or empty");

            this.Name = name;
            this.Type = type;
            this.Value = value;
            if (value != null) HasValue = true;
        }

        public override string ToString() => Generate();

        public PyField WithComment(string comment)
        {
            this.Comment = (comment);
            return this;
        }
    }
}
