using CodeBuilder.Objects;
using CodeBuilder.Statements;
using System.Collections.Generic;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyConstructor : Constructor
    {
        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "PythonConstructor.Mustache");

        public PyConstructor()

            : base(TemplateFilePath)
        {
        }

        public PyConstructor WithArgument(PyArgument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public PyConstructor WithArguments(IEnumerable<Argument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public PyConstructor WithStatement(Assignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public PyConstructor WithStatement(ReturnStatement statement)
        {
            this.Statements.Add(statement);
            return this;
        }
    }
}
