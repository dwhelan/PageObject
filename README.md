# PageObject
A .Net implementation of the [PageObject pattern](http://martinfowler.com/bliki/PageObject.html) to support web testing.
The implementation is essentially a thin wrapper around [Coypu](https://github.com/featurist/coypu) which provides access to a browser and the underlying
elements on web pages.

## Principles
A key intent of the page object pattern is to support maintable web tests through an object-oriented
representation of web pages. The following principles are adopted to support this intent: 
* A `page object` focuses on what users can see and interact with on the
page itself.
* An `element` focuses on what users can see and interact with on a
a specific element.
* A `browser` focuses on how users interact with the browser itself.

For all of the above there is a mechanism to support the developer in cases where they
need to break these principles.

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
 - push to NuGet
 - support different .Net versions
    - support different Coypu versions (may be necessary for different .Net versions)
 - create PageObject.NUnit with page matchers and page object attributes
 - support MSTest
 - support xUnit  

