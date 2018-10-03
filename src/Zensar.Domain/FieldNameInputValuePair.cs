using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Domain
{
    public class FieldNameInputValuePair
    {
        public FieldNameInputValuePair(string fieldName, string fieldValue, string fieldExtensionValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            FieldExtensionValue = fieldExtensionValue;
        }

        public string FieldName { get; private set; }
        public string FieldValue { get; private set; }
        public string FieldExtensionValue { get; private set; }
    }
}
