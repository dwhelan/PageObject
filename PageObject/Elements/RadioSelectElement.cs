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
            var x = $"//*[(name() = \"input\") and " +
                    $"( (@id = //label[contains(normalize-space(),\"{Locator}\")]/@for)" +
                    $"or ancestor::label[contains(normalize-space(text()),\"{Locator}\")]" +
                    $"or (((@type = \"radio\")) and (@id = \"{Locator}\" or contains(@placeholder,\"{Locator}\"))) " +
                    $"or ((@type = \"radio\") and @value = \"{Locator}\" ))]";

            var xpath = $"//input[@type='radio' and @name='{Locator}' and (@id='{value}' or @value='{value}' or ancestor::label[contains(normalize-space(text()),'{value}')])]";
            return Browser.FindAllXPath(xpath);
        }

        public void Choose(string option)
        {
            Browser.Choose(option);
        }

        public bool Selected => FindField().Selected;

    }
}

/*
".//*[(name() = \"input\" ) and 
      (
        (@id = //label[contains(normalize-space(),\"radio field in a label container\")]/@for)  or ancestor::label[contains(normalize-space(text()),\"radio field in a label container\")] 
        or
        (((@type = \"text\" or @type = \"password\" or @type = \"radio\" or @type = \"checkbox\" or @type = \"file\" or @type = \"email\" or @type = \"tel\" or @type = \"url\" or @type = \"number\" or @type = \"datetime\" or @type = \"datetime-local\" or @type = \"date\" or @type = \"month\" or @type = \"week\" or @type = \"time\" or @type = \"color\" or @type = \"search\") or not(@type)) and (@id = \"radio field in a label container\"  or contains(@placeholder,\"radio field in a label container\"))) or (((@type = \"text\" or @type = \"password\" or @type = \"checkbox\" or @type = \"file\" or @type = \"email\" or @type = \"tel\" or @type = \"url\" or @type = \"number\" or @type = \"datetime\" or @type = \"datetime-local\" or @type = \"date\" or @type = \"month\" or @type = \"week\" or @type = \"time\" or @type = \"color\" or @type = \"search\") or not(@type)) and
@name = \"radio field in a label container\" ) or ((@type = \"checkbox\" or @type = \"radio\") and @value = \"radio field in a label container\" ))]"
 */
