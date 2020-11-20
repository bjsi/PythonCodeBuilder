using CodeBuilder.Statements;
using System.IO;

namespace PythonCodeBuilder.Statements
{
    public class PyAssignment : Assignment
    {
        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Assignment.Mustache");
        public override string ToString() => Generate();
        public PyAssignment(string lhs, string rhs)
            : base(TemplateFilePath)
        {
            this.LHS = lhs;
            this.RHS = rhs;
        }
    }
}
