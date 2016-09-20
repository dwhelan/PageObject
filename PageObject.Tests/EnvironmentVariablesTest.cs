using System;
using NUnit.Framework;
namespace PageObject.Tests
{ 
    [TestFixture]
    public class EnvironmentVariablesTest
    {
        private readonly string cd = Environment.CurrentDirectory;

        [Test]
        public void Should_expand_cd_with_current_directory()
        {
            Assert.That(EnvironmentVariables.Expand("{cd}"), Is.EqualTo(cd));
        }

        [Test]
        public void Should_expand_CD_with_current_directory()
        {
            Assert.That(EnvironmentVariables.Expand("{CD}"), Is.EqualTo(cd));
        }

        [Test]
        public void Should_expand_multiple_variables()
        {
            Assert.That(EnvironmentVariables.Expand("{cd}-{cd}"), Is.EqualTo(cd + "-" + cd));
        }
    }
}
