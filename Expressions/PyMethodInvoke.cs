using CodeBuilder.Expressions;
using System.Collections.Generic;

namespace PythonCodeBuilder.Expressions
{
    public class PyMethodInvoke : MethodInvoke
    {
        public PyMethodInvoke(string name, params string[] args)
        {
            this.MethodName = name;
            this.WithArguments(args);
        }

        public PyMethodInvoke WithArgument(string arg)
        {
            this.Arguments.Add(arg);
            return this;
        }

        public PyMethodInvoke WithArguments(IEnumerable<string> args)
        {
            this.Arguments.AddRange(args);
            return this;
        }
    }
}
