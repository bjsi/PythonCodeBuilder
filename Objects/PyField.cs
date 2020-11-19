using CodeBuilder.Objects;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyField : Field
    {

        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "PythonField.Mustache");

        public PyField(string name, string type)
            : base(TemplateFilePath)
        {
            this.Name = name;
            this.Type = type;
        }

        public PyField WithComment(string comment)
        {
            this.Comment = (comment);
            return this;
        }

    }
}
