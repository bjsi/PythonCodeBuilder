using CodeBuilder.Objects;
using CodeBuilder.Statements;
using PythonCodeBuilder.Statements;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PythonCodeBuilder.Objects
{
    public class PyMethod : Method
    {

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Method.Mustache");
        public bool IsAsync { get; }

        public PyMethod(string name, bool async, string returnType) 
            : base(TemplateFilePath)
        {
            this.Name = name;
            this.IsAsync = async;
            this.ReturnType = returnType;
        }

        public string ArgumentsString => string.Join(", ", Arguments);
        public string CommentsString => string.Join("\n\t", Comments);

        public PyMethod WithComment(string comment)
        {
            this.Comments.Add(comment);
            return this;
        }

        public PyMethod WithArgument(PyArgument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public PyMethod WithArguments(IEnumerable<PyArgument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public PyMethod WithStatement(PyAssignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public PyMethod WithStatement(PyReturn statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public override string ToString() => Generate();
    }
}
