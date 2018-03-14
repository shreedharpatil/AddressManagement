using System;

namespace AddressProcessing.CSV
{
    public interface ICSVWriter : IDisposable
    {
        void Write(string fileName, params string[] columns);
    }
}
