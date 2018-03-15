using AddressProcessing.Address;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;
using Moq;
using NUnit.Framework;
using System.IO;

namespace AddressProcessing.Tests
{
    [TestFixture]
    public class AddressFileProcessor1Tests
    {
        private Mock<IMailShot> mailShotMock;

        private Mock<ICSVReaderWriter> csvReaderWriterMock;

        private AddressFileProcessor1 processor;

        [SetUp]
        public void SetUp()
        {
            this.mailShotMock = new Mock<IMailShot>();
            this.csvReaderWriterMock = new Mock<ICSVReaderWriter>();
            this.processor = new AddressFileProcessor1(mailShotMock.Object, this.csvReaderWriterMock.Object);
        }

        [Test]
        public void ShouldSendMailUsingMailshotService()
        {
            // Arrange.
            var counter = 0;
            int numberOfContacts = 10, currentNumberOfContacts = 0;
            string column1, column2;
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);
            this.csvReaderWriterMock.Setup(p => p.Read(out column1, out column2))
                .Returns(() =>
                {
                    return !(currentNumberOfContacts == numberOfContacts);
                })
                .Callback<string, string>((c1, c2) =>
                {
                    currentNumberOfContacts++;
                    if (currentNumberOfContacts == numberOfContacts)
                    {
                        c1 = null;
                        c2 = null;
                    }
                    else
                    {
                        c1 = "user name"; c2 = "user address";
                    }
                });

            // Act.
            this.processor.Process(@"test_data\contacts.csv");

            // Assert.
            Assert.That(counter, Is.EqualTo(10));
        }

        [Test]
        public void ShouldNotSendAnyEmailWhenThereAreNoContacts()
        {
            // Arrange.
            var counter = 0;
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);
            string column1, column2;
            this.csvReaderWriterMock.Setup(p => p.Read(out column1, out column2))
                .Returns(false)
                .Callback<string, string>((c1, c2) =>
                {
                    c1 = null;
                    c2 = null;
                });

            // Act.
            this.processor.Process(@"test_data\empty-contacts.csv");

            // Assert.
            this.mailShotMock.Verify(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>()), Times.Never());
            Assert.That(counter, Is.EqualTo(0));
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void ShouldThrowFileNotFoundExceptionWhenGivenFilePathDoesNotExist()
        {
            // Arrange.
            string column1, column2;
            this.csvReaderWriterMock.Setup(p => p.Read(out column1, out column2)).Throws<FileNotFoundException>();

            // Act.
            this.processor.Process(@"test_data\contacts1.csv");
        }
    }
}