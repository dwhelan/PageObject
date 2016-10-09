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
            get
            {
                return (from option in Options where option.Selected select option.Text).ToList();
            }
            set
            {
                foreach (var option in Options)
                {
                    if (value.Contains(option.Text) != option.Selected)
                        Browser.Select(option.Text).From(Locator);
                }
            }
        }

        private IEnumerable<SnapshotElementScope> Options => FindField().FindAllCss("option");
    }
}
