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

        public void Select(string option)
        {
            if (!Selected(option))
                Toggle(option);
        }

        public void Deselect(string option)
        {
            if (Selected(option))
                Toggle(option);
        }

        public IList<string> Options => OptionElements.Select(o => o.Text).ToList();

        private void Toggle(string option)
        {
            Scope.Select(option).From(Locator);
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
