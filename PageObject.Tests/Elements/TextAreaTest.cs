using NUnit.Framework;
using PageObject.Elements;
using static OpenQA.Selenium.Keys;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextAreaTest : InputTest<TestPage<TextArea>, TextArea>
    {
        private TextArea TextArea => Element;

        protected override string ElementHtml => "<textarea name='name'>initial</textarea>";

        [Test]
        public void Should_get_initial_value()
        {
            Assert.That(TextArea.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void Should_set_value()
        {
            TextArea.Value = "new";
            Assert.That(Element.Value, Is.EqualTo("new"));
        }

        [Test]
        public void Sendkeys_of_Control_a_should_select_all_text()
        {
            TextArea.SendKeys(Control + "a");
            TextArea.SendKeys(Backspace);
            Assert.That(Element.Value, Is.EqualTo(""));
        }

        [Test]
        public void Sendkeys_of_Home_should_position_cursor_at_start()
        {
            TextArea.SendKeys(Home);
            TextArea.SendKeys("XXX");
            Assert.That(Element.Value, Is.EqualTo("XXXinitial"));
        }

        [Test]
        public void Sendkeys_of_End_should_position_cursor_at_end()
        {
            TextArea.SendKeys(Home);
            TextArea.SendKeys(End);
            TextArea.SendKeys("XXX");
            Assert.That(Element.Value, Is.EqualTo("initialXXX"));
        }
    }
}
