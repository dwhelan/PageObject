# PageObject
A .Net implementation of the [PageObject pattern](http://martinfowler.com/bliki/PageObject.html) to support web testing.
The implementation is essentially a thin wrapper around [Coypu](https://github.com/featurist/coypu) which provides access to a browser and the underlying
elements on web pages.

## Installation
TBD

## Usage

### Creating Pages
You declare page objects by subclassing from the `Page` class using the `PageAt` attribute.
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

See more examples at the [Creating Pages](https://github.com/dwhelan/PageObject/wiki/Creating-Pages) wiki page.

## To Do
 - ability to check if the browser is on a specific page
   - path match
 - push to NuGet
 - support different .Net versions
    - support different Coypu versions (may be necessary for different .Net versions)
 - create PageObject.NUnit with page matchers and page object attributes
 - support MSTest
 - support xUnit  

