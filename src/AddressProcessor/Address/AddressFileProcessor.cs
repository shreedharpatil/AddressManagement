using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;

        public AddressFileProcessor(IMailShot mailShot)
        {
            // No need to check for null as it would be having insstance if its dependency injected.
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            this._mailShot = mailShot;
        }
        
        public void Process(string inputFile)
        {
            // using old approach.
            // These methods are made obsolete just to enforce the callers to use refactored approach and can be removed in future.
            // CSVReaderWriter dependency can be handled throgh interface rather than directly depending on it, as shown in Process1() method.
            var reader = new CSVReaderWriter();
            reader.Open(inputFile, CSVReaderWriter.Mode.Read);
            string column1, column2;
            while(reader.Read(out column1, out column2))
            {
                _mailShot.SendMailShot(column1, column2);
            }
            reader.Close();            
        }        
    }
}
