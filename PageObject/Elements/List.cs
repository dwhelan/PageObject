using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Coypu;
using OpenQA.Selenium;

namespace PageObject.Elements
{
    public class List : Field
    {
        public List(ElementAttribute attribute, BrowserSession browser, Coypu.Element element = null) : base(attribute, browser, element) { }

        public string Value
        {
            get { return CoypuElement.SelectedOption; }
            set { Click(value); }
        }

        public void Select(string value)
        {
            Click(value);
        }

        private SnapshotElementScope ElementScope
        {
            get
            {
                const BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                var culture = CultureInfo.InvariantCulture;
                var parameters = new object[] { CoypuElement, SearchScope, new Options() };

                return (SnapshotElementScope) Activator.CreateInstance(typeof(SnapshotElementScope), flags, null, parameters, culture);
            }
        }

        public IList<string> Options => ElementScope.FindAllXPath(".//option").Select(option => option.Text).ToList();

        public void Click(string value)
        {
            SearchScope.Select(value).From(Locator);
        }
    }
}
