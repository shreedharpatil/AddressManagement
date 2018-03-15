using AddressProcessing.Address;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AddressProcessing.Tests
{
    [TestFixture]
    public class AddressFileProcessor2Tests
    {
        private Mock<IMailShot> mailShotMock;

        private Mock<ICSVReader> csvReaderMock;

        private AddressFileProcessor2 processor;

        [SetUp]
        public void SetUp()
        {
            this.mailShotMock = new Mock<IMailShot>();
            this.csvReaderMock = new Mock<ICSVReader>();
            this.processor = new AddressFileProcessor2(mailShotMock.Object, this.csvReaderMock.Object);
        }

        [Test]
        public void ShouldSendMailUsingMailshotService()
        {
            // Arrange.
            var counter = 0;
            var contacts = new List<EmailShot>
            {
                new EmailShot { Name = "username1", Address = "address1" },
                new EmailShot { Name = "username2", Address = "address2" },
                new EmailShot { Name = "username3", Address = "address3" },
                new EmailShot { Name = "username4", Address = "address4" },
                new EmailShot { Name = "username5", Address = "address5" }
            };
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);
            this.csvReaderMock.Setup(p => p.Read<EmailShot>(@"test_data\contacts.csv")).Returns(contacts);

            // Act.
            this.processor.Process(@"test_data\contacts.csv");

            // Assert.
            Assert.That(counter, Is.EqualTo(5));
        }

        [Test]
        public void ShouldNotSendAnyEmailWhenThereAreNoContacts()
        {
            // Arrange.
            var counter = 0;
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);
            this.csvReaderMock.Setup(p => p.Read<EmailShot>(@"test_data\contacts.csv")).Returns(Enumerable.Empty<EmailShot>());

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
            this.csvReaderMock.Setup(p => p.Read<EmailShot>(@"test_data\contacts1.csv")).Throws<FileNotFoundException>();

            // Act.
            this.processor.Process(@"test_data\contacts1.csv");
        }
    }
}