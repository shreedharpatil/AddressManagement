using AddressProcessing.CSV;
using NUnit.Framework;
using System.IO;
using System.Linq;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class CSVReaderTests
    {
        private ICSVReader csvReader;

        [SetUp]
        public void SetUp()
        {
            this.csvReader = new CSVReader();
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowFileNotFoundExceptionWhenGivenFilePathDoesNotExist()
        {
            // Act.
            var contacts = this.csvReader.Read<EmailShot>(@"test_data\contacts1.csv").ToList();
        }

        [Test]
        public void ShouldReadAndReturnsContactsFromGivenFile()
        {
            // Act.
            var contacts = this.csvReader.Read<EmailShot>(@"test_data\contacts.csv");

            // Assert.
            Assert.AreEqual(contacts.Count(), 229);
        }

        [Test]
        public void ShouldReturnNoRecordsWhenGivenFileHasNoContacts()
        {
            // Act.
            var contacts = this.csvReader.Read<EmailShot>(@"test_data\empty-contacts.csv");

            // Assert.
            Assert.AreEqual(contacts.Count(), 0);
        }
    }
}
