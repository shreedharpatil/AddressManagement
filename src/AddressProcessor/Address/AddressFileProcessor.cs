using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;
using System.Linq;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;

        private readonly ICSVReaderWriter _csvReaderWriter;

        private readonly ICSVReader _csvReader;

        public AddressFileProcessor(IMailShot mailShot, ICSVReaderWriter csvReaderWriter, ICSVReader csvReader)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
            this._csvReaderWriter = csvReaderWriter;
            this._csvReader = csvReader;
        }

        public void Process(string inputFile)
        {
            // using old approach
            this._csvReaderWriter.Open(inputFile, CSVReaderWriter.Mode.Read);
            string column1, column2;
            while(this._csvReaderWriter.Read(out column1, out column2))
            {
                _mailShot.SendMailShot(column1, column2);
            }
            this._csvReaderWriter.Close();

            // using new approach
            using(this._csvReader)
            {
                this._csvReader.Read<EmailMailShot>(inputFile).ToList().ForEach(p => _mailShot.SendMailShot(p.Name, p.Email));
            } 
        }
    }
}
