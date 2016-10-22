using NUnit.Framework;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextAreaTest : TextTest
    {
        protected override string ElementHtml => "<textarea name='name'>initial</textarea>";
    }
}
