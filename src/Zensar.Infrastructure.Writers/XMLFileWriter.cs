using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using AI.Domain;
using AI.Infrastructure.Writers.Infrastructure;

namespace AML.Infrastructure.Writers
{
    public class XMLExtractedFileWriter<T, U, V> : IDataWriterWithReturnType<IList<FieldNameInputValuePairObject>,IList<DjListOutputFieldsEntity>>
    {

        public XMLExtractedFileWriter()
        {

        }

        public IList<DjListOutputFieldsEntity> WriteData(IList<FieldNameInputValuePairObject> data)
        {
            throw new NotImplementedException();
        }

        public void Write(IList<FieldNameInputValuePairObject> values, string delimiter)
        {
            //var obj = new FICOFields();
            //Type type = obj.GetType();
            //PropertyInfo pi = type.GetProperty("FIRST_NAME");
            //pi.SetValue(obj, "Rajmeister", null);
            var list = new List<T>();
            var extList = new List<T>();
            foreach (var item in values)
            {
                var obj = Activator.CreateInstance(typeof(T));
                var extObj = Activator.CreateInstance(typeof(T));
                foreach (FieldNameInputValuePair pair in item.FieldNameInputValuePairList)
                {
                    Type type = obj.GetType();
                    Type extType = obj.GetType();
                    PropertyInfo pi = type.GetProperty(pair.FieldName);
                    PropertyInfo extPi = extType.GetProperty(pair.FieldName);
                    if (pi != null)
                    {
                        pi.SetValue(obj, pair.FieldValue, null);
                        if (extPi != null)
                        {
                            extPi.SetValue(extObj, pair.FieldExtensionValue, null);
                        }
                    }
                }
                list.Add((T)obj);
                extList.Add((T)extObj);
            }
 
        }


    }
}
