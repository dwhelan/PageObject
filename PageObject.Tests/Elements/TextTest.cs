using NUnit.Framework;
using NUnit.Framework.Constraints;
using static OpenQA.Selenium.Keys;
using Text = PageObject.Elements.Text;

namespace PageObject.Tests.Elements
{
    [TestFixture]
    public class TextTest : InputTest<TestPage<Text>, Text>
    {
        protected override string ElementHtml => "<input name='name' value='initial' type='text'>";
        protected Text Text => Element;

        [Test]
        public void Should_get_initial_value()
        {
            Assert.That(Text.Value, Is.EqualTo("initial"));
        }

        [Test]
        public void Should_set_value()
        {
            Text.Value = "new";
            Assert.That(Element.Value, Is.EqualTo("new"));
        }

        [Test]
        public void Sendkeys_of_Control_a_should_select_all_text()
        {
            Text.SendKeys(Control + "a");
            Text.SendKeys(Backspace);
            Assert.That(Element.Value, Is.EqualTo(""));
        }

        [Test]
        public void Sendkeys_of_Home_should_position_cursor_at_start()
        {
            Text.SendKeys(Home);
            Text.SendKeys("XXX");
            Assert.That(Element.Value, Is.EqualTo("XXXinitial"));
        }

        [Test]
        public void Sendkeys_of_End_should_position_cursor_at_end()
        {
            Text.SendKeys(Home);
            Text.SendKeys(End);
            Text.SendKeys("XXX");
            Assert.That(Element.Value, Is.EqualTo("initialXXX"));
        }

        [Test]
        public void Click_should_position_cursor_at_end_of_text()
        {
            Text.Click();
            Text.SendKeys("XXX");
            Assert.That(Element.Value, new EndsWithConstraint("XXX"));
        }
    }
}
