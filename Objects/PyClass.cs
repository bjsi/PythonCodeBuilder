﻿using CodeBuilder.Objects;
using PythonCodeBuilder.Helpers;
using System.Collections.Generic;
using System.IO;

namespace PythonCodeBuilder.Objects
{
    public class PyClass : Class
    {

        private static string TemplateFilePath = Path.Combine(PyConst.TemplateFolderPath, "Class.Mustache");

        public PyClass(string name)
            : base(TemplateFilePath)
        {
            name.ThrowIfNullOrEmpty("Failed to create argument because name was null or empty");
            this.Name = name;
        }

        public string BaseString => string.Join(", ", this.Bases);
        public string CommentString => string.Join("\n\t", this.Comments);

        public PyClass WithField(PyField field)
        {
            this.Fields.Add(field);
            return this;
        }

        public PyClass WithFields(IEnumerable<PyField> fields)
        {
            this.Fields.AddRange(fields);
            return this;
        }

        public PyClass WithBaseClasses(IEnumerable<string> klasses)
        {
            this.Bases.AddRange(klasses);
            return this;
        }

        public PyClass WithBaseClass(string klass)
        {
            this.Bases.Add(klass);
            return this;
        }

        public PyClass WithComment(string comment)
        {
            this.Comments.Add(comment);
            return this;
        }

        public PyClass WithConstructor(PyConstructor cons)
        {
            this.Constructor = cons;
            return this;
        }

        public PyClass WithMethod(PyMethod method)
        {
            this.Methods.Add(method);
            return this;
        }

        public override string ToString() => Generate();
    }
}
