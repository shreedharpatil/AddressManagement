using System.Collections.Generic;
using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVReader : ICSVReader
    {
        private StreamReader _readerStream = null;
        
        public IEnumerable<T> Read<T>(string fileName) where T : MailShotBase
        {
            this._readerStream = File.OpenText(fileName);
            string line;
            while ((line = this._readerStream.ReadLine()) != null)
            {
                yield return Map(line) as T;
            }

            this._readerStream.Close();
            this._readerStream = null;
        }

        private EmailShot Map(string line)
        {
            var columns = line.Split('\t');
            return new EmailShot
            {
                Name = columns[0],
                Address = columns[1]
            };
        }
    }
}
