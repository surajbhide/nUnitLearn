using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForgetTheMilk.Controllers;

namespace ConsoleVerification
{
    [TestFixture]
    public class LinkValidatorTests : AssertionHelper
    {
        [Test]
        public void Validate_InvalidLink_throwsException()
        {
            var invalidLink = "http://dontexist.comm";

            Expect(() => new LinkValidator().Validate(invalidLink),
                Throws.Exception.With.Message.EqualTo($"Invalid Link {invalidLink}"));
        }

        [Test]
        public void Validate_ValidLink_throwsnothing()
        {
            var invalidLink = "http://www.google.com";

            Expect(() => new LinkValidator().Validate(invalidLink),
                Throws.Nothing);
        }
    }
}
