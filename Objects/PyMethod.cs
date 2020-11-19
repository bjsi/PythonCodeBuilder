using CodeBuilder.Objects;
using CodeBuilder.Statements;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PythonCodeBuilder.Objects
{
    public class PyMethod : Method
    {

        private static string TemplateFilePath = Path.Combine(Const.TemplateFolderPath, "PythonMethod.Mustache");
        public bool IsAsync { get; }

        public PyMethod(string name, bool async, string returnType) 
            : base(TemplateFilePath)
        {
            this.Name = name;
            this.IsAsync = async;
            this.ReturnType = returnType;
        }

        public string ArgumentsString => string.Join(", ", Arguments.Select(x => $"{x.Name}: {x.Type}"));
        public string CommentsString => string.Join("\n\t", Comments);

        public PyMethod WithComment(string comment)
        {
            this.Comments.Add(comment);
            return this;
        }

        public PyMethod WithArgument(Argument arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public PyMethod WithArguments(IEnumerable<Argument> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }

        public PyMethod WithStatement(Assignment statement)
        {
            this.Statements.Add(statement);
            return this;
        }

        public PyMethod WithStatement(ReturnStatement statement)
        {
            this.Statements.Add(statement);
            return this;
        }
    }
}
