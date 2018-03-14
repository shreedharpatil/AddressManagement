using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVWriter : ICSVWriter
    {
        private StreamWriter _writerStream = null;

        public void Dispose()
        {
            if (this._writerStream != null)
            {
                this._writerStream = null;
                GC.SuppressFinalize(this);
            }            
        }

        ~CSVWriter()
        {
            if (this._writerStream != null)
            {             
                this._writerStream = null;
            }            
        }

        public void Write(string fileName, params string[] columns)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            this._writerStream = fileInfo.CreateText();

            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }

            this._writerStream.WriteLine(outPut);
            this._writerStream.Close();
        }
    }
}
