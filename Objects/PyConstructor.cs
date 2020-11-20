using CodeBuilder.Objects;
using CodeBuilder.Statements;
using PythonCodeBuilder.Statements;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PythonCodeBuilder.Objects
{
    public class PyConstructor : Constructor
    {
        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Constructor.Mustache");

        public PyConstructor(params PyArgument[] args)

            : base(TemplateFilePath)
        {
            this.WithArguments(args);
        }

        public string StatementsString => string.Join("\n\t", Statements);

        public string ArgumentsString => Arguments.Any()
            ? "self, " + string.Join(", ", Arguments)
            : "self";

        public override string ToString() => Generate();

        public PyConstructor WithArgument(PyArgument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public PyConstructor WithArguments(IEnumerable<PyArgument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public PyConstructor WithStatement(PyAssignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public PyConstructor WithStatement(PyReturn statement)
        {
            this.Statements.Add(statement);
            return this;
        }
    }
}
