using CodeBuilder.Generator;
using PythonCodeBuilder.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PythonCodeBuilder.Objects
{
    public class PyFile : CodeGenerator
    {
        private string FileName { get; }

        public List<PyClass> Classes { get; } = new List<PyClass>();
        public List<PyModuleImport> ModuleImports { get; } = new List<PyModuleImport>();
        public List<PyModuleObjectsImport> ModuleObjectsImports { get; } = new List<PyModuleObjectsImport>();

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "File.Mustache");

        public PyFile(string fileName)
            : base(TemplateFilePath)
        {
            fileName.ThrowIfNullOrEmpty("Failed to create python file because filename was null or empty");
            this.FileName = fileName;
        }

        public PyFile WithClass(PyClass klass)
        {
            if (Classes.Any(c => c.Name == klass.Name))
                throw new Exception($"Failed to add class because there is already a class called {klass.Name} in the file");

            Classes.Add(klass);
            return this;
        }

        public PyFile WithModuleImport(PyModuleImport import)
        {
            if (!ModuleImports.Any(x => x.ModuleName == import.ModuleName))
            {
                ModuleImports.Add(import);
            }
            return this; 
        }

        public PyFile WithModuleImport(PyModuleObjectsImport import)
        {
            // Check duplication
            foreach (var m in ModuleObjectsImports)
            {
                if (m.ModuleName == import.ModuleName)
                {
                    m.Objects.UnionWith(import.Objects);
                    return this;
                }
            }

            ModuleObjectsImports.Add(import);
            return this;
        }

        public override string ToString() => Generate();
    }
}
