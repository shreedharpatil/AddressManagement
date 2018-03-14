using System;
using System.Collections.Generic;

namespace AddressProcessing.CSV
{
    public interface ICSVReader : IDisposable
    {
        IEnumerable<T> Read<T>(string fileName) where T : MailShotBase;
    }
}
