﻿using AddressProcessing.Address;
using AddressProcessing.Address.v1;
using Moq;
using NUnit.Framework;
using System.IO;

namespace AddressProcessing.Tests
{
    [TestFixture]
    public class AddressFileProcessorTests
    {
        private Mock<IMailShot> mailShotMock;

        private const string TestInputFile = @"test_data\contacts.csv";

        private AddressFileProcessor processor;

        [SetUp]
        public void SetUp()
        {
            this.mailShotMock = new Mock<IMailShot>();
            this.processor = new AddressFileProcessor(mailShotMock.Object);
        }

        [Test]
        public void ShouldSendMailUsingMailshotService()
        {
            // Arrange.
            var counter = 0;
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);

            // Act.
            this.processor.Process(@"test_data\contacts.csv");

            // Assert.
            Assert.That(counter, Is.EqualTo(229));
        }

        [Test]
        public void ShouldNotSendAnyEmailWhenThereAreNoContacts()
        {
            // Arrange.
            var counter = 0;
            this.mailShotMock.Setup(p => p.SendMailShot(It.IsAny<string>(), It.IsAny<string>())).Callback(() => counter++);

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
            // Act.
            this.processor.Process(@"test_data\contacts1.csv");
        }
    }
}