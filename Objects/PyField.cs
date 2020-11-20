using CodeBuilder.Objects;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyField : Field
    {

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Field.Mustache");

        public PyField(string name, string type)
            : base(TemplateFilePath)
        {
            this.Name = name;
            this.Type = type;
        }

        public override string ToString() => Generate();

        public PyField WithComment(string comment)
        {
            this.Comment = (comment);
            return this;
        }
    }
}
