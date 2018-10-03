using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AI.Infrastructure.Connections.Interfaces;

namespace AI.Infrastructure.Connections
{
    public class CsvDataConnection<T> : IDataConnection<IList<T>> where T : class
    {
        private readonly FileHelperEngine<T> engine;
        private string path;
        public CsvDataConnection(string path)
        {
            engine = new FileHelperEngine<T>();
            this.path = path;
        }

        public virtual IList<T> LoadData()
        {
            var cntr = 0;
            using (var fileStream = new FileStream(path
                , FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            
            using (var streamReader = new StreamReader(fileStream))
            {
                Console.WriteLine("Reading Begun");
                return engine.ReadStream(streamReader).ToList();
            }
        }

    }
}
