
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.Infrastructure.Readers.Interfaces
{
    public interface IDataReader<T>
    {
        T GetData();
    }

    public interface IDataReaderPathAndFieldsParametric<T,U>
    {
        T GetData(string path,U fields);
        IList<string> GetInStringFormat(char delimiter, T list);
    }

    public interface IGeneralDataReader<T>
    {
        T GetData(string tableName, int noOfColumns, string connectionString);
        T GetData(string tableName, int noOfColumns, int noOfRowsYouIntendToRetrieveForInsert, int rowStartIndex, string connectionString);
    }
}
