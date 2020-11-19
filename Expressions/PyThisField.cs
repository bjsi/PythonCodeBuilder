using CodeBuilder.Expressions;

namespace PythonCodeBuilder.Expressions
{
    public class PyThisField : ThisFieldReference
    {
        public override string ThisName => "self";

        public PyThisField(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }
}
