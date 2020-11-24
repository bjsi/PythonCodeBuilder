using PythonCodeBuilder.Expressions;
using PythonCodeBuilder.Helpers;
using PythonCodeBuilder.Objects;
using PythonCodeBuilder.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PythonCodeBuilder.Converter
{
    public class PyConverter
    {

        private Type CSType { get; }
        private PyClass Class { get; set; }
        private List<IMethodTransformer> MethodTransformers { get; }
        private PyTypeConverter TypeConverter { get; }

        public PyConverter(string name, Type csType, List<IMethodTransformer> methodTransformers, PyTypeConverter typeConverter)
        {
            name.ThrowIfArgumentNull("Failed to create converter because class name was empty");
            csType.ThrowIfArgumentNull("Failed to create converter because c# type was null");
            methodTransformers.ThrowIfArgumentNull("Failed to create converter because method transform map was null");
            typeConverter.ThrowIfArgumentNull("Failed to create converter because type converter was null");

            this.TypeConverter = typeConverter;
            this.CSType = csType;
            this.MethodTransformers = methodTransformers;
            Class = new PyClass(name);
        }

        public string Convert()
        {
            ConvertMethods();
            return Class.ToString();
        }

        private void ConvertMethods()
        {
            foreach (var m in CSType.GetMethods().Where(x => !x.IsSpecialName && x.DeclaringType == CSType))
            {
                var method = ConvertMethod(m);
                Class.WithMethod(method);
            }
        }

        private PyMethod ConvertMethod(MethodInfo method)
        {
            var name = method.Name;
            var pyReturnType = TypeConverter.Convert(method.ReturnType);
            var pyMethod = new PyMethod(name, true, pyReturnType);
            var thisFieldRef = new PyThisField("_server");
            var methodInvoke = new PyMethodInvoke($"{thisFieldRef}.{name}");

            foreach (var p in method.GetParameters())
            {
                bool handled = false;
                foreach (var t in MethodTransformers)
                {
                    if (t.Matches(p, CSType))
                    {
                        methodInvoke = t.Transform(methodInvoke);
                        handled = true;
                        break;
                    }
                }

                if (handled)
                    continue;

                var pyMethodName = p.Name;
                var pyParamType = TypeConverter.Convert(p.ParameterType);
                pyMethod.WithArgument(new PyArgument(pyMethodName, pyParamType));
                methodInvoke.WithArgument(pyMethodName);

            }

            pyMethod.WithStatement(new PyReturn(methodInvoke.ToString()));
            return pyMethod;
        }
    }
}
