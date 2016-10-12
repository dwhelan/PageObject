using System;
using System.Collections.Generic;
using System.Linq;
using Coypu;
using Coypu.Drivers;

namespace PageObject.Elements
{
    public class RadioSelectElement : InputElement
    {
        public RadioSelectElement(ElementAttribute attribute, BrowserSession browser) : base(attribute, browser)
        {
        }

        public string Value
        {
            get
            {
                foreach (var radioButton in FindAllRadioButtons())
                {
                    if (radioButton.Selected)
                        return radioButton.Value;
                }
                return "";
            }
            set
            {
                foreach (var radioButton in FindRadioButton(value))
                {
                    Browser.Driver.Choose(radioButton);
                }
            }
        }

        private IEnumerable<SnapshotElementScope> FindAllRadioButtons()
        {
            return Browser.FindAllXPath($"//input[@type='radio' and @name='{Locator}']");
        }

        private IEnumerable<SnapshotElementScope> FindRadioButton(string value)
        {
            var xpath = $@"//input[@type='radio' and @name='{Locator}' and 
                    (@id='{value}' or @value='{value}' 
                     or ancestor::label[contains(normalize-space(text()),'{value}')]
                     or @id = //label[contains(normalize-space(),'{value}')]/@for
                    )]";
            return Browser.FindAllXPath(xpath);
        }

        public void Choose(string option)
        {
            Browser.Choose(option);
        }
    }
}
