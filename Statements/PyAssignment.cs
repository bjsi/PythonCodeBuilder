using CodeBuilder.Statements;
using System.IO;

namespace PythonCodeBuilder.Statements
{
    public class PyAssignment : Assignment
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "PythonAssignment.Mustache");
        public PyAssignment(string lhs, string rhs)
            : base(TemplateFilePath)
        {
            this.LHS = lhs;
            this.RHS = rhs;
        }
    }
}
