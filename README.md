# PageObject
A .Net implementation of the PageObject pattern to support web testing.

## Installation
TBD

## Usage

You typically declare page objects by subclassing from the `Page` class using the `PageAt` attribute to describe its Url.
The class **must** have a public constructor that accepts a `PageSession` object which you should pass to the base constructor. 

```cs
using PageObject;

namespace PageObject.Examples
{
    [PageAt("http://www.google.com")]
    public class HomePage : Page
    {
        public HomePage(PageSession session) : base(session)
        {
        }
    }
}
```

