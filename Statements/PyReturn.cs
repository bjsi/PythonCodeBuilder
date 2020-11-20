using CodeBuilder.Statements;
using System.IO;

namespace PythonCodeBuilder.Statements
{
    public class PyReturn : ReturnStatement
    {

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Return.Mustache");

        public PyReturn(string rhs)
            :base(TemplateFilePath)
        {
            this.RHS = rhs;
        }

        public override string ToString() => Generate();
    }
}
