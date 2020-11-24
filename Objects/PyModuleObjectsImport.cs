using PythonCodeBuilder.Helpers;
using System;
using System.Collections.Generic;

namespace PythonCodeBuilder.Objects
{
    /// <summary>
    /// from module import object1, object2
    /// </summary>
    public class PyModuleObjectsImport
    {
        public string ModuleName { get; }
        public HashSet<string> Objects { get; } = new HashSet<string>();

        public PyModuleObjectsImport(string moduleName, params string[] objects)
        {
            moduleName.ThrowIfNullOrEmpty("Failed to create py module objects import because module name was null");
            this.ModuleName = moduleName;
            WithObjects(objects);
        }

        public PyModuleObjectsImport WithObject(string obj)
        {
            Objects.Add(obj);
            return this;
        }

        public PyModuleObjectsImport WithObjects(IEnumerable<string> objs)
        {
            Objects.UnionWith(objs);
            return this;
        }

        public override string ToString() 
        {
            if (Objects.IsNullOrEmpty())
            {
                return null;
            }

            return $"from {ModuleName} import {string.Join(", ", Objects)}";
        } 
    }
}
