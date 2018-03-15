using AddressProcessing.CSV;
using NUnit.Framework;
using System.Linq;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVWriterTests
    {
        private ICSVWriter csvWriter;

        private const string fileName = @"test_data\dummy.csv";

        [SetUp]
        public void SetUp()
        {
            this.csvWriter = new CSVWriter();
        }

        [Test]
        public void ShouldWriteGivenContactDetailsToFile()
        {
            // Arrange.
            var line = "Shelby Macias	3027 Lorem St.|Kokomo|Hertfordshire|L9T 3D5|Finland	1 66 890 3865-9584	et@eratvolutpat.ca";

            // Act.
            this.csvWriter.Write(fileName, line);

            // Assert.
            var csvReader = new CSVReader();
            var contacts = csvReader.Read<EmailShot>(fileName).ToList();
            Assert.AreEqual(contacts.Count(), 1);            
        }

        [TearDown]
        public void CleanUp()
        {
            System.IO.File.Delete(fileName);
        }
    }
}
