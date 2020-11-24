using PythonCodeBuilder.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PythonCodeBuilder.Objects
{
    /// <summary>
    /// import module
    /// </summary>
    public class PyModuleImport
    {
        public string ModuleName { get; }

        public PyModuleImport(string moduleName)
        {
            moduleName.ThrowIfNullOrEmpty("Failed to create py module import because module name was null");
            this.ModuleName = moduleName;
        }

        public override string ToString() => $"import {ModuleName}";
    }
}
