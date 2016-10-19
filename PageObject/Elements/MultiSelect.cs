using System.Collections.Generic;
using System.Linq;
using Coypu;

namespace PageObject.Elements
{
    public class MultiSelect : Input
    {
        public MultiSelect(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public IList<string> Value
        {
            get { return (from option in OptionElements where option.Selected select option.Text).ToList(); }
            set
            {
                foreach (var option in OptionElements)
                {
                    if (value.Contains(option.Text) != option.Selected)
                        Toggle(option.Text);
                }
            }
        }

        public void Select(params string[] options)
        {
            SetOptions(options, true);
        }

        public void Deselect(params string[] options)
        {
            SetOptions(options, false);
        }

        public void Click(string option)
        {
            Select(option);
        }

        public IList<string> Options => OptionElements.Select(option => option.Text).ToList();

        private void SetOptions(IEnumerable<string> options, bool selected)
        {
            foreach (var option in options)
            {
                if (Selected(option) != selected)
                    Toggle(option);
            }
        }

        private void Toggle(string option)
        {
            SearchScope.Select(option).From(Locator);
        }

        private bool Selected(string option)
        {
            return OptionFor(option).Selected;
        }

        private Coypu.Element OptionFor(string option)
        {
            return FindXPath($".//option[{WithText(option)}]");
        }

        private IEnumerable<ElementScope> OptionElements => FindAllXPathOrThrow(".//option", "option");
    }
}
