using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;

namespace ConsoleVerification
{
    public class FakeValidator : ILinkValidator
    {
        public void Validate(string link)
        {
        }
    }

    [TestFixture]
    public class LinkTaskTests : AssertionHelper
    {
        [Test]
        public void validate_validlink()
        {
            var input = "test http://google.com";

            var task = new Task(input, default(DateTime), new FakeValidator());

            Expect(task.Link, Is.EqualTo("http://google.com"));
        }

        [Test]
        public void Validate_InvalidLink_throwsException()
        {
            var input = "http://dontexist.comm";

            Expect(() => new Task(input, default(DateTime), new LinkValidator()),
                Throws.Exception.With.Message.EqualTo($"Invalid Link {input}"));
        }
    }
}
