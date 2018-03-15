namespace AddressProcessing.CSV
{
    public interface ICSVWriter
    {
        void Write(string fileName, params string[] columns);
    }
}
