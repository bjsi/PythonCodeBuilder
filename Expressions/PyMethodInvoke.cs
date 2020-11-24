using CodeBuilder.Expressions;
using PythonCodeBuilder.Helpers;
using System.Collections.Generic;

namespace PythonCodeBuilder.Expressions
{
    public class PyMethodInvoke : MethodInvoke
    {
        public PyMethodInvoke(string methodName, params string[] args)
        {
            methodName.ThrowIfNullOrEmpty("Failed to create method invoke expression because method name is null or empty");
            this.MethodName = methodName;
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
