using PythonCodeBuilder.Converter.Transformers.Interfaces;
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
        private Type GeneratedFrom { get; } // TODO: Generated from == the SMA type the service type was generated from
        private PyClass Class { get; set; }
        private List<IMethodParameterTransformer> MethodTransformers { get; }
        private IConstructorTransformer ConstructorTransformer { get; }
        private List<IMethodReturnTransformer> ReturnTransformers { get; }
        private PyTypeConverter TypeConverter { get; }

        public PyConverter(string className,
                           Type csServiceType,
                           Type generatedFrom,
                           List<IMethodParameterTransformer> methodTransformers,
                           List<IMethodReturnTransformer> returnTransformers,
                           PyTypeConverter typeConverter,
                           IConstructorTransformer consTransformer = null)
        {
            className.ThrowIfArgumentNull("Failed to create converter because class name was empty");
            csServiceType.ThrowIfArgumentNull("Failed to create converter because c# type was null");
            generatedFrom.ThrowIfArgumentNull("Failed to create converter because generated from type was null");
            methodTransformers.ThrowIfArgumentNull("Failed to create converter because method transform map was null");
            returnTransformers.ThrowIfArgumentNull("Failed to create converter because return transformers were null");
            typeConverter.ThrowIfArgumentNull("Failed to create converter because type converter was null");

            this.ConstructorTransformer = consTransformer;
            this.TypeConverter = typeConverter;
            this.GeneratedFrom = generatedFrom;
            this.CSType = csServiceType;
            this.ReturnTransformers = returnTransformers;
            this.MethodTransformers = methodTransformers;
            Class = new PyClass(className);
        }

        public PyConverter WithMethods()
        {
            ConvertMethods();
            return this;
        }

        /// <summary>
        /// Only for Enums
        /// </summary>
        /// <returns></returns>
        public PyConverter WithFields()
        {
            if (CSType.IsEnum)
            {
                Class.WithBaseClass("Enum");
                ConvertFields();
            }

            return this;
        }

        public PyConverter WithConstructor()
        {
            ConditionallyTransformConstructor();
            return this;
        }

        public PyConverter WithProperties()
        {
            ConvertProperties();
            return this;
        }

        public string Convert()
        { 
            return Class.ToString();
        }

        private void ConvertProperties()
        {
            var properties = GeneratedFrom.GetProperties().Where(x => !x.IsSpecialName);
            foreach (var prop in properties)
            {
                var name = prop.Name;
                var type = prop.PropertyType;
                var pyType = TypeConverter.Convert(type);
                var pyfield = new PyField(name, pyType);
                Class.WithField(pyfield);
            }
        }

        private void ConvertFields()
        {
            Type enumUnderlyingType = Enum.GetUnderlyingType(GeneratedFrom);

            var names = Enum.GetNames(GeneratedFrom);
            var values = Enum.GetValues(GeneratedFrom);
            if (names.Length != values.Length)
                throw new Exception("Unexpected length mismatch");

            for (int i = 0; i < values.Length; i++)
            {
                // Retrieve the value of the ith enum item.
                object value = values.GetValue(i);

                // Convert the value to its underlying type (int, byte, long, ...)
                object underlyingValue = System.Convert.ChangeType(value, enumUnderlyingType);

                string pyValue = underlyingValue.ToString();

                string name = names[i];

                string pyType = TypeConverter.Convert(enumUnderlyingType);

                var pyField = new PyField(name, pyType, pyValue);
                Class.WithField(pyField);
            }
        }

        private void ConditionallyTransformConstructor()
        {
            if (ConstructorTransformer != null && ConstructorTransformer.Matches(GeneratedFrom))
            {
                ConstructorTransformer.Transform(Class);
            }
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
            var methodInvoke = new PyMethodInvoke($"{thisFieldRef}.{name}", true);

            foreach (var p in method.GetParameters())
            {
                bool handled = false;
                foreach (var t in MethodTransformers)
                {
                    // Allows multiple transforms
                    if (t.Matches(p, GeneratedFrom))
                    {
                        methodInvoke = t.Transform(methodInvoke);
                        handled = true;
                    }
                }

                if (handled)
                    continue;

                var pyMethodName = p.Name;
                var pyParamType = TypeConverter.Convert(p.ParameterType);
                pyMethod.WithArgument(new PyArgument(pyMethodName, pyParamType));
                methodInvoke.WithArgument(pyMethodName);

            }

            var returnStatement = new PyReturn(methodInvoke.ToString());
            foreach (var rTrans in ReturnTransformers)
            {
                if (rTrans.Matches(method.ReturnType))
                {
                    rTrans.Transform(method.ReturnType, returnStatement);
                    break;
                }
            }

            pyMethod.WithStatement(returnStatement);
            return pyMethod;
        }
    }
}
