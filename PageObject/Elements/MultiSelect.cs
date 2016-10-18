using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

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
            foreach (var option in options)
            {
                if (!Selected(option))
                    Toggle(option);
            }
        }

        public void Deselect(params string[] options)
        {
            foreach (var option in options)
            {
                if (Selected(option))
                    Toggle(option);
            }
        }

        public void Click(string option)
        {
            Select(option);
        }

        //public void ExtendedClick(string option)
        //{
        //    WithKey(Keys.Control, () => Click(option));
        //}

        public IList<string> Options => OptionElements.Select(o => o.Text).ToList();

        //protected void WithKey(string key, Action action)
        //{
        //    var actions = new Actions((IWebDriver)Driver.Native);
        //    actions.KeyDown(Keys.Control);
        //    try
        //    {
        //        action.Invoke();
        //    }
        //    finally
        //    {
        //        actions.KeyUp(Keys.Control);
        //    }
        //}

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
