using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;
using System.Linq;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor2
    {
        private readonly IMailShot _mailShot;

        private readonly ICSVReader _csvReader;                      
        
        public AddressFileProcessor2(IMailShot mailShot, ICSVReader csvReader)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
            this._csvReader = csvReader;
        }
                
        public void Process(string inputFile)
        {
            // using new approach
            this._csvReader.Read<EmailShot>(inputFile).ToList().ForEach(p => _mailShot.SendMailShot(p.Name, p.Address));
        }
    }
}
