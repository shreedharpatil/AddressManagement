using System.Collections.Generic;

namespace AddressProcessing.CSV
{
    public interface ICSVReader
    {
        IEnumerable<T> Read<T>(string fileName) where T : MailShotBase;
    }
}
