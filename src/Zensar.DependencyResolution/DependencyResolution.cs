using System.Collections.Generic;
using System.Configuration;
using Unity;
using Unity.Injection;
using AI.Calculators;
using AI.Calculators.Interfaces;
using AI.Domain;
using AI.Infrastructure.Connections;
using AI.Infrastructure.Connections.Interfaces;
using AI.Infrastructure.Data;
using AI.Infrastructure.Readers;
using AI.Infrastructure.Readers.Interfaces;
using AI.Infrastructure.Writers;
using AI.Infrastructure.Writers.Infrastructure;

namespace AI.DependencyResolution
{
    public class DependencyResolution // This object should be Static!!
    {
        public UnityContainer container { get; private set; }
        public DependencyResolution()
        {
            
        }

        public void ConstructContainer()
        {
            container = new UnityContainer();
        }

        public  IAMLFormat GetAMLFormatCalculator()
        {
            return new DowJonesFormatCalculator(GetXMLReaderPerson(), GetXMLReaderEntity());
        }

        public  ICalculate<IList<string>> GetInsertCalculator(
            string readTableName, string writeTableName, int noOfColumns, string connectionString, string numericInsertFieldIndices,bool hasIndex)
        {
            return new CalculateInsertStatements(GetGeneralDataReader(), readTableName, writeTableName,
                noOfColumns, connectionString, numericInsertFieldIndices, hasIndex);
        }

        public ICalculate<IList<string>> GetORACLEInsertCalculator(
    string readTableName, string writeTableName, int noOfColumns, string connectionString, string numericInsertFieldIndices, bool hasIndex)
        {
            return new CalculateInsertStatements(GetORACLEGeneralDataReader(), readTableName, writeTableName,
                noOfColumns, connectionString, numericInsertFieldIndices, hasIndex);
        }

        public ICalculate<IList<string>> GetFileInsertCalculator(bool isFile,
            string readFileNameLocation, string writeTableName, int noOfColumns, string connectionString, string numericInsertFieldIndices, bool hasIndex)
        {
            return new CalculateInsertStatements(RegisterLowLevelReader(readFileNameLocation),isFile, readFileNameLocation, writeTableName,
                noOfColumns, connectionString, numericInsertFieldIndices, hasIndex);
        }

        public IDataWriter<IList<string>> GetGeneralDataWriter(string connectionString)
        {
            return new GeneralDataWriter(connectionString);
        }

        public IGeneralDataReader<IList<string>> GetGeneralDataReader()
        {
            return new GeneralTableReader();
        }

        public IGeneralDataReader<IList<string>> GetORACLEGeneralDataReader()
        {
            return new ORACLETableReader();
        }

        public IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsEntity>, IList<string>> GetXMLReaderEntity()
        {
            return new XMLEntityReader();
        }

        public IDataReaderPathAndFieldsParametric<IList<DjListOutputFieldsPerson>, IList<string>> GetXMLReaderPerson()
        {
            return new XMLPersonReader();
        }

        public IDataReader<IList<string>> RegisterLowLevelReader(string location)
        {
            var connection = new FlatFileLowLevelConnection(location);
            var reader = new LowLevelFlatFileReader(connection);
            return reader;
        }

        public IDataWriter<IList<string>> RegisterLowLevelWriter(string tableName, char delimiter)
        {
            var writer = new LowLevelFlatFileWriter(tableName);// Resolving using constructor DI.
            return writer;
        }

        public IDataReader<IList<Entity>> RegisterBPOEntityReader()
        {
            string path = ConfigurationManager.AppSettings["PathEntity"];
            container.RegisterType<IDataConnection<IList<EntityDto>>, CsvDataConnection<EntityDto>>
                (new InjectionConstructor(path));
            var reader = container.Resolve<BPOEntityReader>();
            return reader;
        }

        public IDataReader<IList<Person>> RegisterBPOPersonReader()
        {
            string path = ConfigurationManager.AppSettings["PathPerson"];
            container.RegisterType<IDataConnection<IList<PersonDto>>, CsvDataConnection<PersonDto>>
                (new InjectionConstructor(path));
            var reader = container.Resolve<BPOPersonReader>();
            return reader;
        }
    }
}
