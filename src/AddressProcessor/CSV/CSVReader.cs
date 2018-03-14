using System;
using System.Collections.Generic;
using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVReader : ICSVReader
    {
        private StreamReader _readerStream = null;

        ~CSVReader()
        {
            if (this._readerStream != null)
            {
                this._readerStream = null;
            }
        }

        public void Dispose()
        {
            if (this._readerStream != null)
            {
                this._readerStream = null;
                GC.SuppressFinalize(this);
            }
        }

        public IEnumerable<T> Read<T>(string fileName) where T : MailShotBase
        {
            this._readerStream = File.OpenText(fileName);
            string line;
            while ((line = this._readerStream.ReadLine()) != null)
            {
                yield return Map(line) as T;
            }

            this._readerStream.Close();
        }

        private EmailMailShot Map(string line)
        {
            var columns = line.Split('\t');
            return new EmailMailShot
            {
                Name = columns[0],
                Email = columns[1]
            };
        }
    }
}
