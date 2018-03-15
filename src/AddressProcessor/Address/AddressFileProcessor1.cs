using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor1
    {
        private readonly IMailShot _mailShot;

        private readonly ICSVReaderWriter _csvReaderWriter;
                
        public AddressFileProcessor1(IMailShot mailShot, ICSVReaderWriter csvReaderWriter)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
            this._csvReaderWriter = csvReaderWriter;
        }
        
        public void Process(string inputFile)
        {
            // Slight better approach. Helps unit testing by mocking CSVReaderWriter dependency.
            // These methods are made obsolete just to enforce the callers to use refactored approach and can be removed in future.
            this._csvReaderWriter.Open(inputFile, CSVReaderWriter.Mode.Read);
            string column1, column2;
            while (this._csvReaderWriter.Read(out column1, out column2))
            {
                _mailShot.SendMailShot(column1, column2);
            }
            this._csvReaderWriter.Close();
        }        
    }
}
