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
        public void Should_expand_process_environment_variable()
        {
            // Use environment variables with angle brackets to reduce liklihood of affecting
            // the environment variables that could be used elsewhere.
            try
            {
                Environment.SetEnvironmentVariable("<test-process>", "test-process", EnvironmentVariableTarget.Process);
                Assert.That(EnvironmentVariables.Expand("{<test-process>}"), Is.EqualTo("test-process"));
            }
            finally
            {
                Environment.SetEnvironmentVariable("<test-process>", null, EnvironmentVariableTarget.Process);
            }
        }

        [Test]
        public void Should_expand_user_environment_variable()
        {
            Assert.That(EnvironmentVariables.Expand("{Temp}"), Is.EqualTo(Environment.GetEnvironmentVariable("Temp", EnvironmentVariableTarget.User)));
        }

        [Test]
        public void Should_expand_machine_environment_variable()
        {
            Assert.That(EnvironmentVariables.Expand("{OS}"), Is.EqualTo(Environment.GetEnvironmentVariable("OS", EnvironmentVariableTarget.Machine)));
        }

        [Test]
        public void Should_expand_multiple_variables()
        {
            Assert.That(EnvironmentVariables.Expand("{cd}-{cd}"), Is.EqualTo(cd + "-" + cd));
        }
    }
}
