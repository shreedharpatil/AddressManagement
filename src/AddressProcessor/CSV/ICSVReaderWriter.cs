using System;

namespace AddressProcessing.CSV
{
    public interface ICSVReaderWriter
    {
        [Obsolete("This method is obsolete, please use ICSVReader.Read() for reading and ICSVWriter.Write() for writing.")]
        void Open(string fileName, CSV.CSVReaderWriter.Mode mode);

        [Obsolete("This method is obsolete, please use ICSVWriter.Write() for writing.")]
        void Write(params string[] columns);

        [Obsolete("This method is obsolete, please use ICSVReader.Read() for reading.")]
        bool Read(out string column1, out string column2);

        [Obsolete("This method is obsolete")]
        void Close();
    }
}
